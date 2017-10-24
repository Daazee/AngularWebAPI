using AngularWebAPI.DataAccess.Models;
using AngularWebAPI.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.DataAccess.DataAccess
{
    public class AngularWebAPIDataContext: IdentityDbContext<ApplicationUser>
    {
        public AngularWebAPIDataContext() : base("AppContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
            
        }

        public static AngularWebAPIDataContext Create()
        {
            return new AngularWebAPIDataContext();
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Dependant> Dependant { get; set; }
        public DbSet<EmployeeImage> EmployeeImage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
