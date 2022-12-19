namespace RestAPIDemo.Migrations
{
    using RestAPI_project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestAPIDemo.Data.RestAPIDemoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RestAPIDemo.Data.RestAPIDemoContext context)
        {
            context.Employees.AddOrUpdate(x => x.Id,
                new Employee() { Id = 1, Name = "Test 1" },
                new Employee() { Id = 2, Name = "Test 2" },
                new Employee() { Id = 3, Name = "Test 3" },
                new Employee() { Id = 4, Name = "Test 4" },
                new Employee() { Id = 5, Name = "Test 5" }
                );

            context.Offices.AddOrUpdate(x => x.Id,
                new Office()
                {
                    Id = 101,
                    Location = "Fairfax",
                    EmployeeId = 1
                },
                new Office()
                {
                    Id = 102,
                    Location = "Arlington",
                    EmployeeId = 2
                },
                new Office()
                {
                    Id = 103,
                    Location = "Herndon",
                    EmployeeId = 3
                },
                new Office()
                {
                    Id = 104,
                    Location = "New York",
                    EmployeeId = 4
                },
                new Office()
                {
                    Id = 105,
                    Location = "Seattle",
                    EmployeeId = 5
                }
                );
                
        }
    }
}
