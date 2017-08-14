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
        public async Task<IHttpActionResult> GET(int employeeId)
        {
            var query = EmployeeImage.GetItemAsync(employeeId);
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

        [Route("UploadImage")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> PostUserImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return BadRequest();
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return BadRequest();
                        }
                        else
                        {



                            var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                            postedFile.SaveAs(filePath);

                        }
                    }

                    var message1 = string.Format("Image Updated Successfully.");
                    return Ok(message1); ;
                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return NotFound();
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return NotFound();
            }
        }
    }
}
