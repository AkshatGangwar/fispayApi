using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FISPAYProject.Data.Context
{
    public class FISPAYContext : IdentityDbContext<ApplicationUser> //IdentityDbContext<IdentityUser> //DbContext
    {
        private readonly IConfiguration _configuration;

        public FISPAYContext(DbContextOptions<FISPAYContext> options, IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<VendorAccountRegistration> VendorAccountRegistration { get; set; }
        public DbSet<PersonWalletRegistration> PersonWalletRegistration { get; set; }
        public DbSet<PassbookSummary> PassbookSummary { get; set; }
        public DbSet<VendorStore> VendorStore { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
