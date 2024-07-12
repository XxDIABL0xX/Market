using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Market.Models
{
    // DbContext for the application, inheriting from IdentityDbContext for Identity management
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        // Constructor for AppDbContext, accepting DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet for Product entities
        public DbSet<Product> Products { get; set; }

        // Method to ensure predefined roles are created
        public static async Task EnsureRolesCreated(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Predefined roles
            string[] roleNames = { "Admin", "User" };

            // Iterate through role names and create roles if they don't exist
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
