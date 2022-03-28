using OnBoarding.Core.DTO;
using OnBoarding.Core.Entities;
using OnBoarding.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Service.Interface
{
    public interface ICustomerOnBoardService
    {
        Task<ResponseModel> OnboardCustomer(CustomerDTO customer);
        Task<ResponseModel> ValidateCustomer(string phone, string otp);
        Task<IEnumerable<OnboardedCustomersDto>> GetExistingCustomers();
        bool DoesUserExist(string email, string phoneNumber);
       // Task<ResponseModel> GetUserAsync(string email, string phoneNumber, bool trackChanges);

    }
}
