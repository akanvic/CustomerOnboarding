using Microsoft.EntityFrameworkCore;
using OnBoarding.Core.Entities;

namespace OnBoarding.Repo.Data
{
    public class OnBoardingContext : DbContext
    {
        public OnBoardingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<LGA> LGAs { get; set; }
    }
}
