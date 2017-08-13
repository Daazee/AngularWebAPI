using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.DataAccess.EFRepository
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployeesWithDependant()
        {
            var result = _db.Set<Employee>()
                        .Include("Dependants")
                        .ToList();
            return result;
        }

        public Employee GetEmployeeWithDependant(int id)
        {
            var result = _db.Set<Employee>()
                            .Include("Dependants")
                            .Where(d => d.EmployeeID == id)
                            .FirstOrDefault();
            return result;
                                            
        }
    }
}
