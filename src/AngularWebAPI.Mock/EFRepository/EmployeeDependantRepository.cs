using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWebAPI.Mock.EFRepository
{
    public class EmployeeDependantRepository : GenericRepository<Dependant>, IEmployeeDependantRepository
    {
        private List<Dependant> Dependants;
        public EmployeeDependantRepository()
        {
            SetupDependant();
        }

        void SetupDependant()
        {
            Dependants = new List<Dependant>
            {
                        new Dependant { ID = 1, Lastname="John", Firstname= "Doe", EmployeeID=1,  Gender="Male", Relationship="Spouse" },
                        new Dependant { ID = 2, Lastname="Kate", Firstname= "Mary", EmployeeID=1, Gender="Female",  Relationship="Child"},
                        new Dependant { ID = 3, Lastname="Bolt", Firstname= "Adams", EmployeeID=3, Gender="Male", Relationship="Spouse"}

            };
        }

        public async override Task<int> AddItemAsync(Dependant item)
        {

            Dependants.Add(item);
            return await Task.FromResult(item.EmployeeID);
        }

        public async override Task<IEnumerable<Dependant>> GetItemsAsync()
        {
            return await Task.FromResult(Dependants);
        }

        public async override Task<Dependant> GetItemAsync(int id)
        {
            return await Task.FromResult(Dependants.SingleOrDefault(c => c.ID == id));
        }
        public IEnumerable<Dependant> GetDependants(int EmployeeID)
        {
            throw new NotImplementedException();
        }
    }
}
