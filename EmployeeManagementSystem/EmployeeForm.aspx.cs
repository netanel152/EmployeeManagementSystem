using System;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using System.Web.UI;
using System.Configuration;

namespace EmployeeManagementSystem
{
    public partial class EmployeeForm : Page
    {
        private EmployeeService _employeeService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _employeeService = new EmployeeService(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString);
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeID"] != null)
                {
                    int employeeId = Convert.ToInt32(Request.QueryString["EmployeeID"]);
                    hiddenEmployeeID.Value = employeeId.ToString();
                    LoadEmployee(employeeId);
                }
            }
        }

        private void LoadEmployee(int employeeId)
        {
            try
            {
                Employee employee = _employeeService.GetEmployeeById(employeeId);
                if (employee != null)
                {
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtEmail.Text = employee.Email;
                    txtPhone.Text = employee.Phone;
                    txtHireDate.Text = employee.HireDate.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while loading the employee details.");
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Employee employee = new Employee
                {
                    EmployeeID = string.IsNullOrEmpty(hiddenEmployeeID.Value) ? 0 : Convert.ToInt32(hiddenEmployeeID.Value),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    HireDate = DateTime.Parse(txtHireDate.Text)
                };

                _employeeService.SaveEmployee(employee);
                ClearForm();
                ShowSuccess(employee.EmployeeID > 0 ? "Employee updated successfully." : "Employee created successfully.");
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while saving the employee details. Please ensure all information is valid and that no duplicate employee records exist.");
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }

        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtHireDate.Text = string.Empty;
            hiddenEmployeeID.Value = string.Empty;
            lblError.Text = string.Empty;
            lblSuccess.Text = string.Empty;
            lblError.Visible = false;
            lblSuccess.Visible = false;
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
            lblSuccess.Visible = false;
        }

        private void ShowSuccess(string message)
        {
            lblSuccess.Text = message;
            lblSuccess.Visible = true;
            lblError.Visible = false;
        }
    }
}
