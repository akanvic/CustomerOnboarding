using Microsoft.EntityFrameworkCore;
using OnBoarding.Repo.Data.GenericRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnBoarding.Repo.Data.GenericRepository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected OnBoardingContext _onBoardingContext;
 


        public GenericRepository(OnBoardingContext onBoardingContext)
        {
            _onBoardingContext = onBoardingContext;
        }

        public async Task<IQueryable<T>> FindAllAsync(bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _onBoardingContext.Set<T>().AsNoTracking()) : await Task.Run(() => _onBoardingContext.Set<T>());

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _onBoardingContext.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => _onBoardingContext.Set<T>().Where(expression));

        public Task<T> CreateAsync(T entity) => Task.Run(() => _onBoardingContext.Set<T>().Add(entity).Entity);

    }
}
