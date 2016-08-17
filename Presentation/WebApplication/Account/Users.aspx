<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" MasterPageFile="~/Site.master"  Inherits="WebApplication.Account.Users" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <h2>
        Users
    </h2>

    <telerik:RadButton ID="AddUserButton" runat="server" OnClick="AddUserButton_Click" Visible="false" Text="Add User" Skin="WebBlue" /><br />
    <telerik:RadGrid ID="RadGrid1" OnNeedDataSource="RadGrid1_NeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="UserID">
                    <ItemTemplate>
                        <asp:HyperLink ID="UserProfileHL" runat="server" Text='<%#  DataBinder.Eval(Container, "DataItem.UserID") %>' NavigateUrl='<%# "UserProfile.aspx?UserID=" + DataBinder.Eval(Container, "DataItem.UserID") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn UniqueName="UserName" HeaderText="UserName" DataField="UserName" />
                <telerik:GridBoundColumn UniqueName="Email" HeaderText="Email" DataField="Email" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>

</asp:Content>