using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreProject.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public Employee AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee UpdateEmployee(Employee employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }

        public Employee DeleteEmployee(int Id)
        {
            Employee employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        // From Phone Tabeles
        public IEnumerable<Phone> GetAllPhones()
        {
            return context.Phones;
        }

        public Phone GetPhone(int id){
            return context.Phones.Find(id);
        }

        public Phone AddPhone(Phone phone)
        {
            context.Phones.Add(phone);
            context.SaveChanges();
            return phone;
        }

        public Phone UpdatePhone(Phone phoneChanges)
        {
            var phone = context.Phones.Attach(phoneChanges);
            phone.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return phoneChanges;
        }

        public Phone DeletePhone(int id) {
            Phone phone = context.Phones.Find(id);
            if(phone != null)
            {
                context.Phones.Remove(phone);
                context.SaveChanges();
            }
            return phone;
        }

    }
}
