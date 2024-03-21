

using Domain.Entity;
using Infrastructure.Repository;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Guid id, Employee employee)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            employee.Id = id;
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found");
            }

            await _employeeRepository.DeleteAsync(existingEmployee);
        }
    }
}
