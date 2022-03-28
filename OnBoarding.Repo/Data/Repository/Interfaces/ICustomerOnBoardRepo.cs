using OnBoarding.Core.DTO;
using OnBoarding.Core.Entities;
using OnBoarding.Repo.Data.GenericRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Repo.Data.Repository.Interfaces
{
    public interface ICustomerOnBoardRepo : IGenericRepository<Customer>
    {
        Task<IEnumerable<OnboardedCustomersDto>> GetExistingCustomers();

        Task UpdateCustomerStatus(string phoneNumber);
        bool DoesUserExist(string phoneNumber, string email);
    }
}
