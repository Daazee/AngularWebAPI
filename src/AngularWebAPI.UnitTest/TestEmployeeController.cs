using AngularWebAPI.Abstractions.Interface;
using AngularWebAPI.Domain.Entities;
using AngularWebAPI.WEBAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace AngularWebAPI.UnitTest
{

    //[TestClass]
    public class TestEmployeeController
    {

        Mock<IEmployeeRepository> mock = new Mock<IEmployeeRepository>();
         
        //[TestMethod]   
        public async Task GetEmployee()
        {
            // Arrange            
            mock.Setup(x => x.GetItemAsync(20))
                .ReturnsAsync(new Employee { EmployeeID = 20, Firstname ="Chukwuma" });
            var controller = new EmployeeController(mock.Object);

            // Act
            IHttpActionResult actionResult = await controller.GET(20);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.EmployeeID);
            Assert.AreSame("Chukwuma", contentResult.Content.Firstname);
                  
        }


        //[TestMethod]
        public async Task PostEmployee_Mock()
        {
            var mockRepo = new Mock<IEmployeeRepository>();
            var controller = new EmployeeController(mockRepo.Object);

            // Act
            IHttpActionResult actionResult =  await controller.POST(new Employee
            {
                EmployeeID = 10,
                Firstname = "Chukwuma",
                Lastname = "Chris",
                Gender ="Male",
                Position ="Intern",
                DateOfBirth = new DateTime(1990, 02, 01)
            });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Employee>;

            // Assert
             Assert.IsNotNull(createdResult);
            //Assert.AreEqual("DefaultApi", createdResult.RouteName);
            //Assert.AreEqual(10, createdResult.RouteValues["EmployeeID"]);
           // Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }


        //[TestMethod]
        public async Task Delete()
        {
            // Arrange
            var controller = new EmployeeController(mock.Object);

            // Act
            IHttpActionResult actionResult = await controller.DELETE(20);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
        
    }
}
