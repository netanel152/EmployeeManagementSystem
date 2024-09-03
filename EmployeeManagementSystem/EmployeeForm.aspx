<%@ Page Title="Employee Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeForm.aspx.cs" Inherits="EmployeeManagementSystem.EmployeeForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2><%= string.IsNullOrEmpty(Request.QueryString["EmployeeID"]) ? "Add Employee" : "Edit Employee" %></h2>

        <asp:HiddenField ID="hiddenEmployeeID" runat="server" />

        <div class="form-group mt-3">
            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="txtFirstName"></asp:Label>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group mt-3">
            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName"></asp:Label>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group mt-3">
            <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group mt-3">
            <asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group mt-3">
            <asp:Label ID="lblHireDate" runat="server" Text="Hire Date:" AssociatedControlID="txtHireDate"></asp:Label>
            <asp:TextBox ID="txtHireDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mt-3">
            <asp:Label ID="lblSuccess" runat="server" CssClass="text-success" Visible="False"></asp:Label>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="False"></asp:Label>
        </div>

        <div class="mt-3">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success mr-2" OnClick="BtnSave_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-warning mr-2" OnClick="BtnReset_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="BtnCancel_Click" />
        </div>
    </div>
</asp:Content>
