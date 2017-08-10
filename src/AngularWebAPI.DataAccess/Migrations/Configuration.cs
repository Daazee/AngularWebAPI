namespace AngularWebAPI.DataAccess.Migrations
{
    using Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularWebAPI.DataAccess.DataAccess.AngularWebAPIDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngularWebAPI.DataAccess.DataAccess.AngularWebAPIDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Employee.AddOrUpdate(
                p => p.EmployeeID,
                    new Employee {EmployeeID = 1, Firstname = "Dayo", Lastname = "Adebayo", Gender = "Male", Position = "Assistant Manager", DateOfBirth = new DateTime(1992, 02, 12) },
                    new Employee {EmployeeID = 2, Firstname = "Chukwuma", Lastname = "Joy", Gender = "Female", Position = "Database Administrator", DateOfBirth = new DateTime(1992, 02, 12) }
                );

            context.Dependant.AddOrUpdate(
                d => d.ID,
                new Dependant { ID = 1, EmployeeID = 1, Firstname = "Joy", Lastname = "Adebayo", Gender = "Female", Relationship = "Wife" }
                );
        }
    }
}
