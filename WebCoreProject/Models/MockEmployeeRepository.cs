using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreProject.Models
{
    public class MockEmployeeRepository 
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@gmail.com" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@gmail.com" },
                new Employee() { Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@gmail.com" }
            };
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.Find(e => e.Id == id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }
        public Employee AddEmployee(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id + 1);
            _employeeList.Add(employee);
            return employee;
        }
        public Employee UpdateEmployee(Employee employee)
        {
            Employee changedEmployee = _employeeList.FirstOrDefault(e => e.Id == employee.Id);
            if(changedEmployee != null)
            {
                changedEmployee.Name = employee.Name;
                changedEmployee.Email = employee.Email;
                changedEmployee.Department = employee.Department;
            }
            return changedEmployee;
        }
        public Employee DeleteEmployee(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

    }
}
