using Microsoft.EntityFrameworkCore;

using OnBoarding.Core.DTO;
using OnBoarding.Core.Entities;
using OnBoarding.Repo.Data.GenericRepository.Implementations;
using OnBoarding.Repo.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Repo.Data.Repository.Implementations
{
    public class CustomerOnBoardRepo : GenericRepository<Customer>, ICustomerOnBoardRepo
    {
        private readonly OnBoardingContext _dbContext;
        public CustomerOnBoardRepo(OnBoardingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OnboardedCustomersDto>> GetExistingCustomers()
        {
            var existingCustomers = await (from customer in _dbContext.Customers
                                           where customer.Status == true
                                           join state in _dbContext.States on customer.StateId equals state.StateId
                                           join lg in _dbContext.LGAs on customer.LgaId equals lg.LgaId
                                           select new
                                           {
                                               CustomerId = customer.CustomerId,
                                               PhoneNumber = customer.PhoneNumber,
                                               Email = customer.Email,
                                               StateName = state.StateName,
                                               LgaName = lg.LgaName
                                           }).Select(a => new OnboardedCustomersDto
                                           {
                                               CustomerId = a.CustomerId,
                                               PhoneNumber = a.PhoneNumber,
                                               Email = a.Email,
                                               StateName = a.StateName,
                                               LgaName = a.LgaName
                                           }).ToListAsync();

            return existingCustomers;
        }


        public async Task UpdateCustomerStatus(string phoneNumber)
        {
            var customerFromDB = await _dbContext.Customers.FirstOrDefaultAsync(c=>c.PhoneNumber == phoneNumber);

            if (customerFromDB != null)
            {
                customerFromDB.Status = true;
            }
        }
    }
}
