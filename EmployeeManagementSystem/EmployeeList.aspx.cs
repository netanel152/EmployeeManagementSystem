using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.Models;
using System.Collections.Generic;
using System.Configuration;

namespace EmployeeManagementSystem
{
    public partial class EmployeeList : Page
    {
        private EmployeeService _employeeService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _employeeService = new EmployeeService(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString);
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "EmployeeID";
                ViewState["SortDirection"] = "ASC";
                LoadEmployees();
            }
        }

        private void LoadEmployees(string searchTerm = "")
        {
            string sortExpression = ViewState["SortExpression"].ToString();
            string sortDirection = ViewState["SortDirection"].ToString();

            List<Employee> employees = _employeeService.GetEmployees(searchTerm, sortExpression, sortDirection);
            if (employees.Count > 0)
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
            if (e.CommandArgument != null && int.TryParse(e.CommandArgument.ToString(), out int employeeId))
            {
                if (e.CommandName == "UpdateEmployee")
                {
                    Response.Redirect($"EmployeeForm.aspx?EmployeeID={employeeId}");
                }
                else if (e.CommandName == "DeleteEmployee")
                {
                    _employeeService.DeleteEmployee(employeeId);
                    LoadEmployees();
                }
            }
            else
            {
                ShowError("Invalid employee ID. Please try again.");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadEmployees(txtSearch.Text.Trim());
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string sortDirection = ViewState["SortDirection"].ToString();

            if (ViewState["SortExpression"].ToString() == sortExpression)
            {
                sortDirection = (sortDirection == "ASC") ? "DESC" : "ASC";
            }
            else
            {
                sortDirection = "ASC";
            }

            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            LoadEmployees(txtSearch.Text.Trim());
        }

        protected string GetSortDirection(string column)
        {
            string sortExpression = ViewState["SortExpression"].ToString();
            string sortDirection = ViewState["SortDirection"].ToString();

            if (sortExpression == column)
            {
                return sortDirection == "ASC" ? "▲" : "▼";
            }
            return string.Empty;
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
