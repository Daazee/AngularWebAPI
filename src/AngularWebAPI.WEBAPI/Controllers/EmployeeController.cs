using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;
using AngularWebAPI.WEBAPI.Models;
using AngularWebAPI.DataAccess.Models;
using AngularWebAPI.WEBAPI.Services;

namespace AngularWebAPI.WEBAPI.Controllers
{
    [RoutePrefix("api/Employee")]
    [Authorize]
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository Employees;
        private readonly IAppUserService _userAccountService;
        public EmployeeController(IEmployeeRepository employees)
        {
            Employees = employees;
            _userAccountService =new DefaultAppUserService();

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


        [Route("EmployeeDependant")]
        public IHttpActionResult GETEmployeeDependant()
        {
            try
            {

                var employee = Employees.GetEmployeesWithDependant().ToList()
                                        .Select(f => new EmployeeModel()
                                        {
                                            Firstname = f.Firstname,
                                            Lastname = f.Lastname,
                                            Gender = f.Gender,
                                            Position = f.Position,
                                            EmployeeID = f.EmployeeID,
                                            DateOfBirth = f.DateOfBirth,
                                            Dependants = f.Dependants
                                            .Select(d => new DependantModel()
                                            {
                                                EmployeeID = d.EmployeeID,
                                                ID = d.ID,
                                                Firstname = d.Firstname,
                                                Lastname = d.Lastname,
                                                Gender = d.Gender,
                                                Relationship = d.Relationship
                                            }).ToList()
                                        });

                if (employee != null)
                    return Ok(employee);
            }
            catch (Exception ex)
            {
                //throw;
                return BadRequest();
            }
            return NotFound();
        }



        [Route("EmployeeDependant/{id}")]
        public IHttpActionResult GETEmployeeDependant(int id)
        {
            try
            {

                var employee = Employees.GetEmployeeWithDependant(id);
                var model = new EmployeeModel()
                {
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    DateOfBirth = employee.DateOfBirth,
                    Gender = employee.Gender,
                    EmployeeID = employee.EmployeeID,
                    Position = employee.Position,
                    Dependants = employee.Dependants.Select(d => new DependantModel()
                    {
                        EmployeeID = d.EmployeeID,
                        ID = d.ID,
                        Firstname = d.Firstname,
                        Lastname = d.Lastname,
                        Gender = d.Gender,
                        Relationship = d.Relationship
                    }).ToList()
                };

                if (model != null)
                    return Ok(model);
            }
            catch (Exception ex)
            {
                //throw;
                return BadRequest();
            }
            return NotFound();
        }

        [HttpPost]
        [Route("AddEmployee")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> POST(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var newUserData = CreateApplicationUserFromEmployeData(employee);
                var createdUserAccount = _userAccountService.Create(newUserData, "Users", "password");
                if (createdUserAccount != null)
                {
                    employee.AppUserId = createdUserAccount.Id;
                    var employeeCreated = (await Employees.AddItemAsync(employee)) == 1;
                    return Ok(employee);
                }
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
    }
}



[HttpPut]
[Route("UpdateEmployee/{id}")]
public async Task<IHttpActionResult> PUT(int id, Employee employee)
{
    try
    {
        var query = await Employees.GetItemAsync(id);
        if (query != null && ModelState.IsValid)
        {
            query.Firstname = employee.Firstname;
            query.Lastname = employee.Lastname;
            query.Gender = employee.Gender;
            query.Position = employee.Position;
            await Employees.UpdateItemAsync(query);
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

[HttpDelete]
[Route("DeleteEmployee/{id}")]
public async Task<IHttpActionResult> DELETE(int id)
{
    try
    {

        await Employees.RemoveItemAsync(id);
        return Ok();
    }
    catch (Exception ex)
    {
        return BadRequest();
    }
}



#region privates
private ApplicationUser CreateApplicationUserFromEmployeData(Employee employeeinfo)
{
    return new ApplicationUser
    {
        FirstName = employeeinfo.Firstname,
        LastName = employeeinfo.Lastname,
        Email = employeeinfo.EmailAddress,
        UserName = employeeinfo.EmailAddress
    };
}
        #endregion
    }
}
