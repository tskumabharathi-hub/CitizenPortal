using CitizenPortal.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CitizenPortal.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
            {
            
            }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ComplaintCategory> ComplaintCategories  { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ComplaintCategory>()
                .HasOne(c => c.Department)
                .WithMany(d => d.ComplaintCategories)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.ComplaintCategory)
                .WithMany(cc => cc.Complaints)
                .HasForeignKey(c =>c.ComplaintCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Complaint>()
                .HasOne(c =>c.User)
                .WithMany(u  => u.Complaints)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
