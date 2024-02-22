using System.Collections.Generic;
using WebApiwithADOdotNET.Models;

namespace WebApiwithADOdotNET.Services
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        string DeleteEmployee(int employeeId);
        string AddEmployee(Employee employee);
        string UpdateEmployeeCity(int EmployeeId, string City);
    }
}
