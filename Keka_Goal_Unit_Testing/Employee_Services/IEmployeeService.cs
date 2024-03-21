using Employee_Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Services
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
