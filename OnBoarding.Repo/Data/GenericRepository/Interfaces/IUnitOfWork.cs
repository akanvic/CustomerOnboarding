using OnBoarding.Repo.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Repo.Data.GenericRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerOnBoardRepo CustomerOnBoardRepo { get; }
        void Save();
    }
}
