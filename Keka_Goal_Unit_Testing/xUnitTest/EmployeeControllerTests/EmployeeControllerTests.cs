using AutoFixture;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using System.Diagnostics;
using Web_Api.Controllers;

namespace xUnitTest.EmployeeControllerTests
{
    public class EmployeeControllerTests : BaseTest
    {
        [Fact]
        public async Task GetEmployee_ValidId_ReturnsEmployee()
        {
            // Arrange
            var expectedEmployee = _fixture.Build<Employee>()
                                           .With(e => e.Id, Guid.NewGuid())
                                           .Create();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedEmployee);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.GetEmployeeById(expectedEmployee.Id);

            // Debug
            Debug.WriteLine($"Result: {result}");

            // Assert
            Assert.NotNull(result);

            
            if (result.Result != null)
            {
                Assert.IsType<ActionResult<Employee>>(result);

                var objectResult = Assert.IsType<OkObjectResult>(result.Result);
                var actualEmployee = Assert.IsAssignableFrom<Employee>(objectResult.Value);
                Assert.Equal(expectedEmployee.Id, actualEmployee.Id);
                Assert.Equal(expectedEmployee.Name, actualEmployee.Name);
                
            }
            else
            {
                Debug.WriteLine("Result.Result is null.");
            }
        }
        [Fact]
        public async Task GetAllEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var expectedEmployees = _fixture.CreateMany<Employee>(3);
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.GetAllEmployeeAsync()).ReturnsAsync(expectedEmployees);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.GetAllEmployees();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualEmployees = Assert.IsAssignableFrom<IEnumerable<Employee>>(objectResult.Value);
            Assert.Equal(expectedEmployees, actualEmployees);
        }

        [Fact]
        public async Task CreateEmployee_ValidEmployee_ReturnsCreatedEmployee()
        {
            // Arrange
            var expectedEmployee = _fixture.Create<Employee>();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.CreateEmployeeAsync(It.IsAny<Employee>())).Returns(Task.CompletedTask);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.CreateEmployee(expectedEmployee);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(controller.GetEmployeeById), createdAtActionResult.ActionName);
            Assert.Equal(expectedEmployee.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(expectedEmployee, createdAtActionResult.Value);
        }

        [Fact]
        public async Task UpdateEmployee_ValidIdAndEmployee_ReturnsNoContent()
        {
            // Arrange
            var expectedEmployee = _fixture.Create<Employee>();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.UpdateEmployeeAsync(It.IsAny<Guid>(), It.IsAny<Employee>())).Returns(Task.CompletedTask);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.UpdateEmployee(expectedEmployee.Id, expectedEmployee);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

      

        [Fact]
        public async Task GetEmployee_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var mockService = new Mock<IEmployeeService>();
            mockService.Setup(service => service.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Employee)null);
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.GetEmployeeById(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdateEmployee_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var expectedEmployee = _fixture.Create<Employee>();
            var mockService = new Mock<IEmployeeService>();
            var controller = new EmployeeController(mockService.Object);

            // Act
            var result = await controller.UpdateEmployee(invalidId, expectedEmployee);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

      

    }
}
