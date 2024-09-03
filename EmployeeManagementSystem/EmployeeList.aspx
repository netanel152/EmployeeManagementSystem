<%@ Page Title="Employee List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="EmployeeManagementSystem.EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Employee List</h2>

        <div class="form-inline mt-4 mb-4">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mr-2" Placeholder="Search by first name or email"></asp:TextBox>
            <div class="form-inline mt-4">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mr-2" OnClick="BtnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="BtnClear_Click" />
            </div>
        </div>

        <!-- GridView with Paging, Sorting enabled, and Sort Direction Indicator -->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
            PageSize="10" CssClass="table table-striped table-bordered text-center"
            EmptyDataText="No employees found." AllowPaging="True" AllowSorting="True"
            OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting">
            <Columns>
                <asp:TemplateField SortExpression="EmployeeID">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkSortEmployeeID" runat="server" CommandName="Sort" CommandArgument="EmployeeID">Id <%# GetSortDirection("EmployeeID") %></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="d-block" style="color: black; text-align: center;"><%# Eval("EmployeeID") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="FirstName">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkSortFirstName" runat="server" CommandName="Sort" CommandArgument="FirstName">First Name <%# GetSortDirection("FirstName") %></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="d-block" style="color: black; text-align: center;"><%# Eval("FirstName") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="LastName">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkSortLastName" runat="server" CommandName="Sort" CommandArgument="LastName">Last Name <%# GetSortDirection("LastName") %></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="d-block" style="color: black; text-align: center;"><%# Eval("LastName") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="Email">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkSortEmail" runat="server" CommandName="Sort" CommandArgument="Email">Email <%# GetSortDirection("Email") %></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="d-block" style="color: black; text-align: center;"><%# Eval("Email") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="Phone">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkSortPhone" runat="server" CommandName="Sort" CommandArgument="Phone">Phone <%# GetSortDirection("Phone") %></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="d-block" style="color: black; text-align: center;"><%# Eval("Phone") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField SortExpression="HireDate">
                    <HeaderTemplate>
                        <asp:LinkButton ID="lnkSortHireDate" runat="server" CommandName="Sort" CommandArgument="HireDate">Hire Date <%# GetSortDirection("HireDate") %></asp:LinkButton>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <span class="d-block" style="color: black; text-align: center;"><%# Eval("HireDate", "{0:yyyy-MM-dd}") %></span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" Text="Update" CommandName="UpdateEmployee" CommandArgument='<%# Eval("EmployeeID") %>'></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteEmployee" CommandArgument='<%# Eval("EmployeeID") %>' OnClientClick="return confirm('Are you sure you want to delete this employee?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblSuccess" runat="server" CssClass="text-success" Visible="False"></asp:Label>
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="False"></asp:Label>

        <asp:Button ID="btnAddNew" runat="server" Text="Add New Employee" CssClass="btn btn-primary mt-3" OnClick="BtnAddNew_Click" />
    </div>
</asp:Content>
