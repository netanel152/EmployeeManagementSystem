using System.Collections.Generic;
using EmployeeManagementSystem.Repositories;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _repository;

        public EmployeeService(string connectionString)
        {
            _repository = new EmployeeRepository(connectionString);
        }

        public List<Employee> GetEmployees(string searchTerm, string sortExpression, string sortDirection)
        {
            return _repository.GetAllEmployees(searchTerm, sortExpression, sortDirection);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _repository.GetEmployeeById(employeeId);
        }

        public void SaveEmployee(Employee employee)
        {
            _repository.SaveEmployee(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            _repository.DeleteEmployee(employeeId);
        }
    }
}
