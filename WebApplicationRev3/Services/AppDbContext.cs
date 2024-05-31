using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplicationRev3.Models;

namespace WebApplicationRev3.Services
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            

            var admin = new IdentityRole("admin");
			admin.NormalizedName = "admin";

			var customer = new IdentityRole("customer");
			admin.NormalizedName = "customer";

			var manager = new IdentityRole("manager");
			admin.NormalizedName = "manager";

			builder.Entity<IdentityRole>().HasData(admin, customer, manager);

            
        }
	}
}
