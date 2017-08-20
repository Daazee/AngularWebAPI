using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Mock.EFRepository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private List<Employee> Employees;
        public EmployeeRepository()
        {
            SetupEmployee();
        }

        void SetupEmployee()
        {
            Employees = new List<Employee>
            {
                        new Employee { Lastname="John", Firstname= "Doe", EmployeeID=1, DateOfBirth=DateTime.Now, Gender="Male", Position="Programmer"},
                        new Employee { Lastname="Kate", Firstname= "Mary", EmployeeID=2, DateOfBirth=DateTime.Now, Gender="Female", Position="Accountant"},
                        new Employee { Lastname="Bolt", Firstname= "Adams", EmployeeID=3, DateOfBirth=DateTime.Now, Gender="Male", Position="Manager"}

            };
        }

        public async override Task<int> AddItemAsync(Employee item)
        {

            Employees.Add(item);
            return await Task.FromResult(item.EmployeeID);
        }

        public async override Task<IEnumerable<Employee>> GetItemsAsync()
        {
            return await Task.FromResult(Employees);
        }

        public async override Task<Employee> GetItemAsync(int id)
        {
            return await Task.FromResult(Employees.SingleOrDefault(c => c.EmployeeID == id));
        }

        public IEnumerable<Employee> GetEmployeesWithDependant()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeWithDependant(int id)
        {
            throw new NotImplementedException();
        }
    }
}
