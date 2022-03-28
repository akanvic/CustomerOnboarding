using OnBoarding.Repo.Data.GenericRepository.Interfaces;
using OnBoarding.Repo.Data.Repository.Implementations;
using OnBoarding.Repo.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnBoarding.Repo.Data.GenericRepository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnBoardingContext _context;

        public UnitOfWork(OnBoardingContext context)
        {
            _context = context;
            CustomerOnBoardRepo = new CustomerOnBoardRepo(_context);
        }

        public ICustomerOnBoardRepo CustomerOnBoardRepo { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
