using CitizenPortal.DAL.Data;
using CitizenPortal.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CitizenPortal.API.Seed
{
    public class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(
            IServiceProvider serviceProvider)
        {
            var roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roles =
            {
                "Admin",
                "User"
            };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            string adminEmail = "admin@citizenportal.com";

            var admin =
                await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    Surname = "Administrator",
                    City = "Chennai",
                    Address = "Citizen Portal",
                    Gender = "Male",
                    Pincode = "600001",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(
                    admin,
                    "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department
                    {
                        DepartmentName = "Public Work Department",
                        Icon = "publicwork.png"
                    },
                    new Department
                    {
                        DepartmentName = "Department of Buildings",
                        Icon = "buildings.png"
                    },
                    new Department
                    {
                        DepartmentName = "Water Department",
                        Icon = "water.png"
                    },
                    new Department
                    {
                        DepartmentName = "Department of Sanitation",
                        Icon = "sanitation.png"
                    });
                await context.SaveChangesAsync();
            }
            if (!context.ComplaintCategories.Any())
            {
                var publicWork = context.Departments.First(d => d.DepartmentName == "Public Work Department");
                var buildings = context.Departments.First(d => d.DepartmentName == "Department of Buildings");
                var water = context.Departments.First(d => d.DepartmentName == "Water Department");
                var sanitation = context.Departments.First(d => d.DepartmentName == "Department of Sanitation");

                context.ComplaintCategories.AddRange(

                    // Public Work Department
                    new ComplaintCategory
                    {
                        CategoryName = "Pothole Repair",
                        Icon = "pothole.png",
                        DepartmentId = publicWork.DepartmentId
                    },
                    new ComplaintCategory
                    {
                        CategoryName = "Broken Street Light",
                        Icon = "streetlight.png",
                        DepartmentId = publicWork.DepartmentId
                    },

                    // Department of Buildings
                    new ComplaintCategory
                    {
                        CategoryName = "Code Violation",
                        Icon = "code.png",
                        DepartmentId = buildings.DepartmentId
                    },
                    new ComplaintCategory
                    {
                        CategoryName = "Public Housing",
                        Icon = "housing.png",
                        DepartmentId = buildings.DepartmentId
                    },

                    // Water Department
                    new ComplaintCategory
                    {
                        CategoryName = "Water Leak",
                        Icon = "waterleak.png",
                        DepartmentId = water.DepartmentId
                    },
                    new ComplaintCategory
                    {
                        CategoryName = "Unsafe Drinking Water",
                        Icon = "drinkingwater.png",
                        DepartmentId = water.DepartmentId
                    },

                    // Department of Sanitation
                    new ComplaintCategory
                    {
                        CategoryName = "Recycling",
                        Icon = "recycle.png",
                        DepartmentId = sanitation.DepartmentId
                    },
                    new ComplaintCategory
                    {
                        CategoryName = "Garbage Collection",
                        Icon = "garbage.png",
                        DepartmentId = sanitation.DepartmentId
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
    
}
