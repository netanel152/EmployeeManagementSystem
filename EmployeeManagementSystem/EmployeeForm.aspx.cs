using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace EmployeeManagementSystem
{
    public partial class EmployeeForm : Page
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
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
            string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtFirstName.Text = reader["FirstName"].ToString();
                            txtLastName.Text = reader["LastName"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                            txtPhone.Text = reader["Phone"].ToString();
                            txtHireDate.Text = Convert.ToDateTime(reader["HireDate"]).ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("An error occurred while loading the employee details.");
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            bool isUpdate = !string.IsNullOrEmpty(hiddenEmployeeID.Value);
            string query = isUpdate
                ? "UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, HireDate=@HireDate WHERE EmployeeID=@EmployeeID"
                : "INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate) VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate)";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@HireDate", DateTime.Parse(txtHireDate.Text));

                    if (isUpdate)
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(hiddenEmployeeID.Value));
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearForm();
                ShowSuccess(isUpdate ? "Employee updated successfully." : "Employee created successfully.");
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
