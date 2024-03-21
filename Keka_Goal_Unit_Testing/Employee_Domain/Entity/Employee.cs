using System;
namespace Employee_Domain.Entity
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }

}
