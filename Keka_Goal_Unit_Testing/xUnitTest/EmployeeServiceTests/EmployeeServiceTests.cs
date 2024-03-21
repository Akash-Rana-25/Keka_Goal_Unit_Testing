using AutoFixture;
using Domain.Entity;
using Infrastructure.Repository;
using Moq;
using Services;

namespace xUnitTest.EmployeeServiceTests
{
    public class EmployeeServiceTests : BaseTest
    {
        [Fact]
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
            Assert.NotNull(result);
            Assert.Equal(expectedEmployee, result);
        }

        [Fact]
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
            Assert.NotNull(result);
            Assert.Equal(expectedEmployees, result);
        }

        [Fact]
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
