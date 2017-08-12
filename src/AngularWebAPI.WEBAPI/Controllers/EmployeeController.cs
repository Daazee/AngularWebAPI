using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using AngularWebAPI.WEBAPI.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AngularWebAPI.WEBAPI.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository Employees;
        public EmployeeController(IEmployeeRepository employees)
        {
            Employees = employees;
        }

        [Route("")]
        public async Task<IHttpActionResult> GET()
        {
            var employee = await Employees.GetItemsAsync();
            if (employee != null)
                return Ok(employee);
            else
                return NotFound();
        }

       
        [Route("{id}")]
        public async Task<IHttpActionResult> GET(int id)
        {
            try
            {
                var employee = await Employees.GetItemAsync(id);
                if (employee != null)
                    return Ok(employee);               
            }
            catch (Exception)
            {
                //throw;
                return BadRequest();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddEmployee")]
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
            catch (Exception)
            {
                // throw new HttpRequestException();
                return BadRequest();
            }            
        }



        [HttpPut]
        [Route("UpdateEmployee/{id}")]
        public async Task<IHttpActionResult> PUT(int id, [FromBody]Employee employee)
        {
            try
            {
                var query = await Employees.GetItemAsync(id);
                if (query != null && ModelState.IsValid)
                {                    
                    await Employees.UpdateItemAsync(employee);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();

            }          
        }

        [Route("DeleteEmployee")]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            try
            {
                var query = await Employees.GetItemAsync(id);
                if(query != null)
                {
                    await Employees.RemoveItemAsync(query.EmployeeID);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }      

    }
}
