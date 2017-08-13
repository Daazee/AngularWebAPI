using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AngularWebAPI.WEBAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:6285", "*", "*")]
    [RoutePrefix("api/EmployeeDependant")]
    public class EmployeeDependantController : ApiController
    {
        private IEmployeeDependantRepository EmployeeDependant;
        public EmployeeDependantController(IEmployeeDependantRepository employeeDependant)
        {
            EmployeeDependant = employeeDependant;
        }


        [Route("")]
        public async Task<IHttpActionResult> GET()
        {
            var dependant = await EmployeeDependant.GetItemsAsync();
            if (dependant != null)
                return Ok(dependant);
            else
                return BadRequest();
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> GET(int id)
        {
            var dependant = await EmployeeDependant.GetItemAsync(id);
            if (dependant != null)
                return Ok(dependant);
            else
                return BadRequest();
        }

        [Route("GetDependantsByEmployeeID/{id}")]
        public async Task<IHttpActionResult> GETDependantsByEmployeeID(int id)
        {
            var dependant = EmployeeDependant.GetDependants(id);
            if (dependant != null)
                return Ok(dependant);
            else
                return BadRequest();
        }



        [HttpPost]
        [Route("AddDependant")]
        public async Task<IHttpActionResult> POST([FromBody]Dependant dependant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await EmployeeDependant.AddItemAsync(dependant);
                    return Ok(dependant);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception)
            {
                //throw new HttpRequestException();
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UpdateDependant/{id}")]
        public async Task<IHttpActionResult> PUT(int id, [FromBody]Dependant dependant)
        {
            try
            {
                var query = await EmployeeDependant.GetItemAsync(id);
                if (query != null)
                {
                    query = new Dependant
                    {
                        EmployeeID = dependant.EmployeeID,
                        Firstname = dependant.Firstname,
                        Lastname = dependant.Lastname,
                        Gender = dependant.Gender,
                        Relationship = dependant.Relationship
                    };
                    await EmployeeDependant.UpdateItemAsync(query);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                //throw new HttpRequestException();
                return BadRequest();
            }

        }

        [HttpDelete]
        [Route("DeleteDependant/{id}")]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            try
            {
                var query = await EmployeeDependant.GetItemAsync(id);
                if (query != null)
                {
                    await EmployeeDependant.RemoveItemAsync(query.EmployeeID);
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
