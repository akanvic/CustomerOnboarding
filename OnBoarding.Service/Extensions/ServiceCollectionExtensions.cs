using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnBoarding.Core.DTO;
using OnBoarding.Repo.Data;
using OnBoarding.Repo.Data.GenericRepository.Implementations;
using OnBoarding.Repo.Data.GenericRepository.Interfaces;
using OnBoarding.Repo.Data.Repository.Implementations;
using OnBoarding.Repo.Data.Repository.Interfaces;
using OnBoarding.Service.Implementation;
using OnBoarding.Service.Interface;
using OnBoarding.Service.Validators;

namespace OnBoarding.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<OnBoardingContext>(
        opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ICustomerOnBoardRepo, CustomerOnBoardRepo>();
            services.AddScoped<ICustomerOnBoardService, CustomerOnBoardService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBankService, BankService>();
            services.AddHttpClient();
            return services;
        }
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidationService, ValidationService>();
            services.AddSingleton<IValidator<CustomerDTO>, CustomerValidator>();
        }
    }
}
