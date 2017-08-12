using AngularWebAPI.Abstractions.Interface;
using System.Threading.Tasks;
using System.Web.Http;

namespace AngularWebAPI.WEBAPI.Controllers
{
    [RoutePrefix("api/EmployeeImage")]
    public class EmployeeImageController : ApiController
    {
        private IEmployeeImageRepository EmployeeImage;
        public EmployeeImageController(IEmployeeImageRepository employeeImage)
        {
            EmployeeImage = employeeImage;
        }
        
        //[Route("{id}")]
        //public async Task<IHttpActionResult> GET(int id)
        //{
        //    var query = EmployeeImage.GetItemAsync(id);
        //}
    }
}
