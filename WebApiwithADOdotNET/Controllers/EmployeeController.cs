using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApiwithADOdotNET.Models;
using WebApiwithADOdotNET.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiwithADOdotNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // GET: api/<EmployeeController>
        [HttpGet("Get-All-Employee")]
        public List<Employee> Get()
        {
            List<Employee> employees = new List<Employee>();
            employees = _employeeRepository.GetAllEmployees();
            return employees;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("Get-Employee-By-Id")]
        public Employee Get([FromQuery]int id)
        {
            Employee employee = new Employee();
            employee = _employeeRepository.GetEmployeeById(id);
            return employee;
        }

        // POST api/<EmployeeController>
        [HttpPost("Add-Employee")]
        public string Post([FromQuery] Employee employee)
        {
            string msg = _employeeRepository.AddEmployee(employee);
            return msg;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("Update-Employee-City")]
        public string Put([FromQuery]int EmployeeId, string City)
        {
            string message = _employeeRepository.UpdateEmployeeCity(EmployeeId, City);
            return message;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("Delete-Employee")]
        public string Delete([FromQuery]int id)
        {
            string message = _employeeRepository.DeleteEmployee(id);
            return message;
        }
    }
}
