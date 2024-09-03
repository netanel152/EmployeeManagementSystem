using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Data
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetAllEmployees(string searchTerm, string sortExpression, string sortDirection)
        {
            List<Employee> employees = new List<Employee>();
            string query = "SELECT * FROM Employees";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE (FirstName = @SearchTerm) OR (Email = @SearchTerm)";
            }

            query += $" ORDER BY {sortExpression} {sortDirection}";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    cmd.Parameters.Add(new SqlParameter("@SearchTerm", SqlDbType.NVarChar, 100) { Value = searchTerm });
                }

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            HireDate = Convert.ToDateTime(reader["HireDate"])
                        });
                    }
                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int) { Value = employeeId });

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            HireDate = Convert.ToDateTime(reader["HireDate"])
                        };
                    }
                }
            }

            return employee;
        }

        public void SaveEmployee(Employee employee)
        {
            string query = employee.EmployeeID > 0
                ? "UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, HireDate=@HireDate WHERE EmployeeID=@EmployeeID"
                : "INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate) VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 100) { Value = employee.FirstName });
                cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 100) { Value = employee.LastName });
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = employee.Email });
                cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 15) { Value = employee.Phone });
                cmd.Parameters.Add(new SqlParameter("@HireDate", SqlDbType.Date) { Value = employee.HireDate });

                if (employee.EmployeeID > 0)
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int) { Value = employee.EmployeeID });
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int) { Value = employeeId });

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
