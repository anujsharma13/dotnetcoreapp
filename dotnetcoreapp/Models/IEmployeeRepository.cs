using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoreapp.Models
{
  public  interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetEmployees();
        Employee Add(Employee employee);
        Employee Update(Employee employeechanges);
        Employee Delete(int id);
    }
}
