using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.DataAccess.EFRepository
{
    public class EmployeeDependantRepository : GenericRepository<Dependant>, IEmployeeDependantRepository
    {


        public IEnumerable<Dependant> GetDependants(int EmployeeID)
        {
            var result = _db.Dependant.Where(c=>c.EmployeeID==EmployeeID).ToList();
            return result;
        }
    }
}
