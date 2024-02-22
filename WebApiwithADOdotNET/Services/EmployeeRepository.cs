using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiwithADOdotNET.Models;

namespace WebApiwithADOdotNET.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _conn;
        private readonly string conectionstring;
        public EmployeeRepository(IConfiguration Conn)
        {
            _conn = Conn;
            conectionstring = _conn.GetSection("ConnectionStrings:ADOConnect").Value;
        }

        public string AddEmployee(Employee employee)
        {
            string message = string.Empty;
            if(employee != null)
            {
                SqlConnection connection = new SqlConnection(conectionstring);
                SqlCommand cmd = new SqlCommand("spAddEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@CurrentAddress", employee.CurrentAddress);
                cmd.Parameters.AddWithValue("@PermanentAddress", employee.PermanentAddress);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Nationality", employee.Nationality);
                cmd.Parameters.AddWithValue("@PINCode", employee.PINCode);
                connection.Open();
                int msg = cmd.ExecuteNonQuery();
                connection.Close();
                if(msg > 0)
                {
                    message = "Error";
                }
                else
                {
                    message = "Employee has been added succesfully";
                }
            }
            return message;
        }
   
        public string DeleteEmployee(int employeeId)
        {
            string message = string.Empty;
            SqlConnection connection = new SqlConnection(conectionstring);
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                message = "Error";
            }
            else
            {
                message = $"Employee with EmployeeId: {employeeId} has been Deleted";
            }
            return message;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            SqlConnection conn = new SqlConnection(conectionstring);
            SqlDataAdapter da = new SqlDataAdapter("spGetAllEmployee", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee1 = new Employee();
                    employee1.EmployeeId = Convert.ToInt32(dt.Rows[i]["EmployeeId"]);
                    employee1.EmployeeName = dt.Rows[i]["EmployeeName"].ToString();
                    employee1.DateOfBirth = dt.Rows[i]["DateOfBirth"].ToString();
                    employee1.Gender = dt.Rows[i]["Gender"].ToString();
                    employee1.CurrentAddress = dt.Rows[i]["CurrentAddress"].ToString();
                    employee1.PermanentAddress = dt.Rows[i]["PermanentAddress"].ToString();
                    employee1.City = dt.Rows[i]["City"].ToString();
                    employee1.Nationality = dt.Rows[i]["Nationality"].ToString();
                    employee1.PINCode = dt.Rows[i]["PINCode"].ToString();
                    employees.Add(employee1);
                }
            }
            if (employees.Count > 0)
            {
                return employees;
            }
            else
            {
                return null;
            }
            conn.Close();
        }

        public Employee GetEmployeeById(int EmployeeId)
        {
            SqlConnection conn = new SqlConnection(conectionstring);
            SqlDataAdapter dataAdapter = new SqlDataAdapter("spGetEmployeeById", conn);
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            //dataAdapter.SelectCommand.Parameters.AddWithValue("@EmployeeName", EmployeeName);
            DataTable dataTable = new DataTable();
            conn.Open();
            dataAdapter.Fill(dataTable);
            Employee employee = new Employee();
            if (dataTable.Rows.Count > 0)
            {
                employee.EmployeeId = Convert.ToInt32(dataTable.Rows[0]["EmployeeId"]);
                employee.EmployeeName = dataTable.Rows[0]["EmployeeName"].ToString();
                employee.DateOfBirth = dataTable.Rows[0]["DateOfBirth"].ToString();
                employee.Gender = dataTable.Rows[0]["Gender"].ToString();
                employee.CurrentAddress = dataTable.Rows[0]["CurrentAddress"].ToString();
                employee.PermanentAddress = dataTable.Rows[0]["PermanentAddress"].ToString() ;
                employee.City = dataTable.Rows[0]["City"].ToString();
                employee.Nationality = dataTable.Rows[0]["Nationality"].ToString();
                employee.PINCode = dataTable.Rows[0]["PINCode"].ToString();

            }

            conn.Close();
            if (employee != null)
            {
                return employee;
            }
            else
            {
                return null;
            }
        }

        string IEmployeeRepository.UpdateEmployeeCity(int EmployeeId, string City)
        {
            string message = string.Empty;
            SqlConnection connection = new SqlConnection(conectionstring);
            SqlCommand cmd = new SqlCommand("spUpdateEmployeeCity", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i > 0)
            {
                message = "Error";
            }
            else
            {
                message = $"The Employee with EmployeeId: {EmployeeId} have changed City to {City}";
            }
            return message;
        }
    }
}
