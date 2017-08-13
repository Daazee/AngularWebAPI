using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.DataAccess.DataAccess
{
    public class AngularWebAPIDataContext: DbContext
    {
        public AngularWebAPIDataContext() : base("AngularWebAPIDataContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Dependant> Dependant { get; set; }
        public DbSet<EmployeeImage> EmployeeImage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
