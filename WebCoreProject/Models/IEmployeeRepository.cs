using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreProject.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        Employee DeleteEmployee(int id);

        //From Phone Table
        IEnumerable<Phone> GetAllPhones();
        Phone GetPhone(int id);
        Phone AddPhone(Phone phone);
        Phone UpdatePhone(Phone phone);
        Phone DeletePhone(int id);
    }

}
