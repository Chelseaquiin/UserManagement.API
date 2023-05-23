using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models.Entities;

namespace UserManagement.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, IdentityUserLogin<string>, ApplicationRoleClaim,
        IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>(u =>
            {
                u.HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

                u.HasMany(s => s.Logins)
                .WithOne()
                .HasForeignKey(us => us.UserId)
                .IsRequired();

                u.HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(use => use.UserId)
                .IsRequired();

                u.HasMany(r => r.UserRoles)
                .WithOne(r => r.User)
                .HasForeignKey(user => user.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(r =>
            {
                r.HasMany(r => r.UserRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(role => role.RoleId)
                .IsRequired();
            });

            modelBuilder.Entity<ApplicationUserClaim>(b =>
            {
                
                b.HasKey(uc => uc.Id);

                b.ToTable("AspNetUserClaims");
            });
            modelBuilder.Entity<ApplicationRoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);

                b.ToTable("AspNetRoleClaims");
            });
            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });

                b.ToTable("AspNetUserRoles");
            });



        }
    }
}
