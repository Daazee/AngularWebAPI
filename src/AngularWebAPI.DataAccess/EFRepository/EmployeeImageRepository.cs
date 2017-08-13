using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.DataAccess.EFRepository
{
    public class EmployeeImageRepository: GenericRepository<EmployeeImage>, IEmployeeImageRepository
    {
    }
}
