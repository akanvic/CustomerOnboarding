using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Onboarding.Service.Helper;
using OnBoarding.Core.DTO;
using OnBoarding.Core.Entities;
using OnBoarding.Core.Responses;
using OnBoarding.Repo.Data;
using OnBoarding.Repo.Data.GenericRepository.Interfaces;
using OnBoarding.Service.Interface;
using OnBoarding.Service.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Service.Implementation
{
    public class CustomerOnBoardService : ICustomerOnBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CustomerDTO> _customerValidator;
        private readonly IValidationService _validationService;
        public CustomerOnBoardService(IUnitOfWork unitOfWork, IValidator<CustomerDTO> customerValidator, IValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _customerValidator = customerValidator ?? throw new ArgumentNullException(nameof(customerValidator));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }
        public async Task<IEnumerable<OnboardedCustomersDto>> GetExistingCustomers()
        {
            try
            {
                var ret = await _unitOfWork.CustomerOnBoardRepo.GetExistingCustomers();
                return ret;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public async Task<ResponseModel> OnboardCustomer(CustomerDTO customer)
        {
            _validationService.Validate(_customerValidator.Validate(customer), new List<(bool, string, object, string)>{
                (false, "Benefit Name", "", " Already Exists")
            });

            var findUser = DoesUserExist(customer.Email,customer.PhoneNumber);
            if(findUser == false)
            {
                return new ResponseModel { State = 0, Msg = "The Customer with the email address already exist", Data = "" };
            }
            using var hasher = new HMACSHA1();
            var user = new Customer
            {
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                StateId = customer.StateId,
                LgaId = customer.LgaId,
                Otp = OTP.GenerateOtp(),
                PasswordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(customer.Password)),
                PasswordSalt = hasher.Key
            };
            await _unitOfWork.CustomerOnBoardRepo.CreateAsync(user);
            _unitOfWork.Save();
            return new ResponseModel { State = 1, Msg ="Customer has been OnBoarded successfully", Data = user };
        }
        //public async Task<ResponseModel> GetUserAsync(string email,string phoneNumber, bool trackChanges)
        //{
        //   var ret=  _unitOfWork.CustomerOnBoardRepo.FindByConditionAsync(
        //        c => c.Email.Equals(email) || c.PhoneNumber.Equals(phoneNumber), trackChanges).Result.SingleOrDefaultAsync();

        //}
        public bool DoesUserExist(string email, string phoneNumber)
        {
            var ret =  _unitOfWork.CustomerOnBoardRepo
                        .FindByConditionAsync(c => c.Email.Equals(email) || c.PhoneNumber.Equals(phoneNumber), false);
            if(ret == null)
                return false;
            return true;
        }
        //=> await _unitOfWork.CustomerOnBoardRepo.FindByConditionAsync(
        //    c => c.Email.Equals(email) || c.PhoneNumber.Equals(phoneNumber), trackChanges).Result.SingleOrDefaultAsync();

        public async Task<ResponseModel> ValidateCustomer(string phone, string otp)
        {
            var ret = await _unitOfWork.CustomerOnBoardRepo.
                FindByConditionAsync(c => c.PhoneNumber.Equals(phone) && c.Otp.Equals(otp), true).Result.SingleOrDefaultAsync();
            if (ret == null)
                return new ResponseModel { State = 0, Msg="The Customer's OnBoarding has not been confirmed",Data=ret};

            await _unitOfWork.CustomerOnBoardRepo.UpdateCustomerStatus(phone);
            _unitOfWork.Save();
            return new ResponseModel { State = 1, Msg = "The Customer's OnBoarding been confirmed Successfully", Data = ret };
        }
    }
}
