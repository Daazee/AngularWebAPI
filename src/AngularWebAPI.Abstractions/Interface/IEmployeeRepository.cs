using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Abstractions.Interface
{
    public interface IEmployeeRepository: IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetEmployeesWithDependant();
        Employee GetEmployeeWithDependant(int id);
    }
}
