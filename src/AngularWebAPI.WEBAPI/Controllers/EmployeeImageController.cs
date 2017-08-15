using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        [Route("{id}")]        
        public async Task<IHttpActionResult> GET(int id)
        {
            var query = await EmployeeImage.GetItemAsync(id);
            if (query != null)
            {
                return Ok(query);
            }
            else
                return NotFound();
        }

        [Route("UploadImage")]
        public async Task<IHttpActionResult> POST([FromBody] EmployeeImage image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var img = await EmployeeImage.AddItemAsync(image);
                    return Ok(img);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("DeleteImage/{id}")]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            try
            {
                var query = await EmployeeImage.GetItemAsync(id);
                if (query != null)
                {
                    await EmployeeImage.RemoveItemAsync(query.EmployeeId);
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

        [Route("GETImageByEmployeeID/{id}")]
        public async Task<IHttpActionResult> GETImageByEmployeeID(int id)
        {
            var image = EmployeeImage.GetImageByEmployeeID(id);
            var result = Convert.ToBase64String(image.Image);
            if (image != null)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
