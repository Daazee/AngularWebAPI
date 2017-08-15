using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Abstractions.Interface
{
    public interface IEmployeeImageRepository: IRepositoryBase<EmployeeImage>
    {
        EmployeeImage GetImageByEmployeeID(int EmployeeID);
    }
}
