using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystem
{
    public partial class EmployeeList : Page
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployees();
            }
        }

        private void LoadEmployees(string searchTerm = "")
        {
            string query = "SELECT * FROM Employees";
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += " WHERE (FirstName = @SearchTerm) OR (Email = @SearchTerm)";
            }

            DataTable employees = GetData(query, searchTerm);
            if (employees.Rows.Count > 0)
            {
                GridView1.DataSource = employees;
                GridView1.DataBind();
                lblSuccess.Visible = false;
                lblError.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }

        private DataTable GetData(string query, string searchTerm = "")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    }

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while fetching data.");
            }
            return dt;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadEmployees(searchTerm);
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadEmployees();
        }

        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeForm.aspx");
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int employeeId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "UpdateEmployee")
            {
                Response.Redirect($"EmployeeForm.aspx?EmployeeID={employeeId}");
            }
            else if (e.CommandName == "DeleteEmployee")
            {
                DeleteEmployee(employeeId);
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
            ExecuteQuery(query, employeeId);
            ShowSuccess("Employee deleted successfully.");
            LoadEmployees();
        }

        private void ExecuteQuery(string query, int employeeId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while processing the request.");
            }
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
