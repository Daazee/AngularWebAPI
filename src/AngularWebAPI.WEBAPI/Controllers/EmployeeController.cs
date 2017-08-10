using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AngularWebAPI.WEBAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository Employees;
        public EmployeeController(IEmployeeRepository employees)
        {
            Employees = employees;
        }             

        public async Task<IHttpActionResult> GET()
        {
            var employee = await Employees.GetItemsAsync();
            if (employee != null)
                return Ok(employee);
            else
                return NotFound();
        }

        public async Task<IHttpActionResult> GET(int Id)
        {
            try
            {
                var employee = await Employees.GetItemAsync(Id);
                if (employee != null)
                    return Ok(employee);               
            }
            catch (Exception)
            {
                throw;
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> POST([FromBody]Employee Employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = await Employees.AddItemAsync(Employee);
                    return Ok(employee);
                }
                else
                {
                    ModelState.AddModelError("", "Unable to Create Employee");
                    return BadRequest();
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<IHttpActionResult> PUT(int id, [FromBody]Employee employee)
        {
            try
            {
                var query = await Employees.GetItemAsync(id);
                if (query != null)
                {
                    var result = await Employees.UpdateItemAsync(query);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BadRequest();
        }

    }
}
