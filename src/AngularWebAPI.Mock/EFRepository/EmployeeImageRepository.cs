using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Mock.EFRepository
{
    public class EmployeeImageRepository : GenericRepository<EmployeeImage>, IEmployeeImageRepository
    {

        private List<EmployeeImage> Images;
        public EmployeeImageRepository()
        {
            SetupEmployeeImage();
        }

        void SetupEmployeeImage()
        {
            Images = new List<EmployeeImage>
            {
                        new EmployeeImage { ID = 1,  EmployeeId=1},
                        new EmployeeImage { ID = 2,  EmployeeId=1 },
                        new EmployeeImage { ID = 3,  EmployeeId=3}

            };
        }

        public async override Task<int> AddItemAsync(EmployeeImage item)
        {

            Images.Add(item);
            return await Task.FromResult(item.ID);
        }

        public async override Task<IEnumerable<EmployeeImage>> GetItemsAsync()
        {
            return await Task.FromResult(Images);
        }

        public async override Task<EmployeeImage> GetItemAsync(int id)
        {
            return await Task.FromResult(Images.SingleOrDefault(c => c.ID == id));
        }
        public IEnumerable<Dependant> GetDependants(int EmployeeID)
        {
            throw new NotImplementedException();
        }
        public EmployeeImage GetImageByEmployeeID(int EmployeeID)
        {
            throw new NotImplementedException();
        }
    }
}
