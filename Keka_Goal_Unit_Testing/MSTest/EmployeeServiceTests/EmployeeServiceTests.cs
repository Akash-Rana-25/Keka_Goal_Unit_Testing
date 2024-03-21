using AutoFixture;
using Domain.Entity;
using Infrastructure.Repository;
using Moq;
using Services;

namespace MSTest.EmployeeServiceTests
{
    [TestClass]
    public class EmployeeServiceTests : BaseTest
    {
        [TestMethod]
        public async Task GetByIdAsync_ValidId_ReturnsEmployee()
        {
            // Arrange
            var expectedEmployee = _fixture.Create<Employee>();
            var mockRepository = new Mock<IRepository<Employee>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedEmployee);
            var service = new EmployeeService(mockRepository.Object);

            // Act
            var result = await service.GetByIdAsync(expectedEmployee.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmployee, result);
        }

        [TestMethod]
        public async Task GetAllEmployeeAsync_ReturnsListOfEmployees()
        {
            // Arrange
            var expectedEmployees = _fixture.CreateMany<Employee>(3);
            var mockRepository = new Mock<IRepository<Employee>>();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedEmployees);
            var service = new EmployeeService(mockRepository.Object);

            // Act
            var result = await service.GetAllEmployeeAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmployees, result);
        }

        [TestMethod]
        public async Task CreateEmployeeAsync_ValidEmployee_CreatesEmployee()
        {
            // Arrange
            var expectedEmployee = _fixture.Create<Employee>();
            var mockRepository = new Mock<IRepository<Employee>>();
            var service = new EmployeeService(mockRepository.Object);

            // Act
            await service.CreateEmployeeAsync(expectedEmployee);

            // Assert
            mockRepository.Verify(repo => repo.AddAsync(expectedEmployee), Times.Once);
        }

     

       
    }
}
