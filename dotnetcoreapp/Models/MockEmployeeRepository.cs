using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeelist;
        public MockEmployeeRepository()
        {
            _employeelist = new List<Employee>()
            {
                new Employee(){Id=1,Name="Anuj",Department=Dept.IT,Email="anuj@gmail.com"},
                new Employee(){Id=2,Name="Abhinav",Department=Dept.IT,Email="abhinav@gmail.com"},
                new Employee(){Id=3,Name="Akshat",Department=Dept.IT,Email="akshat@gmail.com"}
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeelist.Max(e => e.Id) + 1;
            _employeelist.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
          Employee employee= _employeelist.FirstOrDefault(e => e.Id == id);
            if(employee!=null)
            {
                _employeelist.Remove(employee);
            }
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return _employeelist.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeelist;
        }

        public Employee Update(Employee employeechanges)
        {
            Employee employee = _employeelist.FirstOrDefault(e => e.Id ==employeechanges.Id);
            if (employee != null)
            {
                employee.Name = employeechanges.Name;
                employee.Email = employeechanges.Email;
                employee.Department = employeechanges.Department;
            }
            return employee;
        }
    }
}
