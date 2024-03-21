
using Domain.Entity;

namespace Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetByIdAsync(Guid id); 
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Guid id, Employee employee);
        Task DeleteEmployeeAsync(Guid id);
        
    }
}
