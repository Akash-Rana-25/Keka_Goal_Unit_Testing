using AutoFixture;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using Web_Api.Controllers;

namespace MSTest.EmployeeControllerTests
{
    [TestClass]
    public class EmployeeControllerTests : BaseTest
    {
       


        [TestMethod]
        public async Task CreateEmployee_ValidEmployee_ReturnsCreatedEmployee()
        {
            // Arrange
            var expectedEmployee = _fixture.Create<Employee>();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.CreateEmployeeAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.CreateEmployee(expectedEmployee) as ActionResult<Employee>;

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.AreEqual(nameof(controller.GetEmployeeById), createdAtActionResult.ActionName);
            Assert.AreEqual(expectedEmployee.Id, createdAtActionResult.RouteValues["id"]);
            Assert.AreEqual(expectedEmployee, createdAtActionResult.Value);
        }

        [TestMethod]
        public async Task UpdateEmployee_ValidIdAndEmployee_ReturnsNoContent()
        {
            // Arrange
            var expectedEmployee = _fixture.Create<Employee>();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.UpdateEmployeeAsync(It.IsAny<Guid>(), It.IsAny<Employee>())).Returns(Task.CompletedTask);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.UpdateEmployee(expectedEmployee.Id, expectedEmployee) as IActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

    

        [TestMethod]
        public async Task GetEmployee_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Employee)null);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.GetEmployeeById(invalidId) as ActionResult<Employee>;

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
       



        [TestMethod]
        public async Task UpdateEmployee_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var expectedEmployee = _fixture.Create<Employee>();
            var mockService = new Mock<IEmployeeService>();
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.UpdateEmployee(invalidId, expectedEmployee) as IActionResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

 
    }
}
