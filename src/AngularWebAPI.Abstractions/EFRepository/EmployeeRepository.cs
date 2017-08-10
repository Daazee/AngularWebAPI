using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Abstractions.EFRepository
{
    public class EmployeeRepository: GenericRepository<Employee>, IEmployeeRepository
    {
    }
}
