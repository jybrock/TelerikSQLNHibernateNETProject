<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs"  MasterPageFile="~/Site.master" Inherits="WebApplication.Account.UserProfile" %>

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
        User Profile
    </h2>
    <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" Font-Bold="true" />
    <table width="80%" border="0"  >
                    <tr>
	                    <td class="Col1">
                            UserName: <font color="red">*</font>
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="UserNameTextBox" ReadOnly="true" runat="server" CssClass="reqd" />
				            <asp:RequiredFieldValidator 
                                    ID="UserNameRFV" 
                                    Enabled="false"
                                    runat="Server"
                                    ControlToValidate="UserNameTextBox" 
                                    ErrorMessage="Please enter a UserName" 
                                    Display="dynamic" />      
	                    </td>
	                </tr>
                    <tr id="ActiveTR" runat="server" visible="false">
	                    <td class="Col1">
                            Active: <font color="red">*</font>
	                    </td>
	                    <td>
	                    	<asp:CheckBox ID="ActiveCheckBox" runat="server" />
	                    </td>
	                </tr>
	                <tr >
	                    <td class="Col1">
                            Password:
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="PasswordTextBox" TextMode="Password" runat="server" CssClass="reqd" />
				            <asp:RequiredFieldValidator 
                                    ID="PasswordRFV" 
                                    runat="Server"
                                    ControlToValidate="PasswordTextBox" 
                                    ErrorMessage="Please enter a UserName" 
                                    Display="dynamic" Enabled="false" />      
	                    </td>
	                </tr>
	                <tr>
	                    <td class="Col1">
	                        First Name: <font color="red">*</font>
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="FirstNameTextBox" runat="server" CssClass="reqd" />
				            <asp:RequiredFieldValidator 
                                    ID="FirstNameRequiredFieldValidator" 
                                    runat="Server"
                                    ControlToValidate="FirstNameTextBox" 
                                    ErrorMessage="Please enter your First Name" 
                                    Display="dynamic" />      
	                    </td>
	                </tr>
	                <tr>
	                    <td class="Col1">
	                        Last Name: <font color="red">*</font>
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="LastNameTextBox" runat="server" CssClass="reqd"></asp:TextBox>
				            <asp:RequiredFieldValidator 
                                    ID="LastNameRequiredFieldValidator" 
                                    runat="Server"
                                    ControlToValidate="LastNameTextBox" 
                                    ErrorMessage="Please enter your Last Name" 
                                    Display="dynamic" />   
	                    </td>
	                </tr>
                    <tr>
	                    <td class="Col1">
	                    	Email: <font color="red">*</font>
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="EmailTextBox" runat="server" CssClass=""></asp:TextBox>
			                <asp:RegularExpressionValidator 
                                    ID="EmailRegularExpressionValidator" 
                                    runat="server" 
                                    ControlToValidate="EmailTextBox"
                                    ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
                                    ErrorMessage="Please enter a valid E-mail address" 
                                    Display="dynamic" />
                                <asp:RequiredFieldValidator 
                                    ID="EmailRequiredFieldValidator" 
                                    runat="Server"
                                    ControlToValidate="EmailTextBox" 
                                    ErrorMessage="Please enter your E-mail address" 
                                    Display="dynamic" />
	                    </td>
	                </tr>
                    <tr>
	                    <td class="Col1">
	                        Address: 
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="AddressTextBox" runat="server" CssClass=""></asp:TextBox>
	                    </td>
	                </tr>
	                <tr>
	                    <td class="Col1">
	                        City: 
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="CityTextBox" runat="server" CssClass=""></asp:TextBox>
	                    </td>
	                </tr>
	                <tr>
                        
	                    <td class="Col1">
	                        State:
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="StateTextBox" runat="server" CssClass=""></asp:TextBox>
	                    </td>
	                </tr>
                    <tr>
                        
	                    <td class="Col1">
	                        ZipCode:
	                    </td>
	                    <td>
	                    	<asp:TextBox ID="ZipCodeTextbox" runat="server" CssClass=""></asp:TextBox>
	                    </td>
	                </tr>
	                <tr>
	                    <td class="Col1">
	                    	Phone Number: <font color="red">*</font>
	                    </td>
	                    <td>
                            <telerik:RadMaskedTextBox ID="PhoneRadMaskedTextBox" 
                                runat="server"
                                Mask="(###) ###-####"
                                Width="100px" />
                                <asp:RegularExpressionValidator 
                                    ID="PhoneRegularExpressionValidator" 
                                    runat="server" 
                                    ControlToValidate="PhoneRadMaskedTextBox"
                                    ValidationExpression="^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$"
                                    ErrorMessage="Please enter a valid Phone Number" 
                                    Display="dynamic" />
                                <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator1" 
                                    runat="Server"
                                    ControlToValidate="PhoneRadMaskedTextBox" 
                                    ErrorMessage="Please enter your Phone Number" 
                                    Display="dynamic" />
	                    </td>
	                </tr>
                    <tr>
	                    <td class="Col1">
	                    	Extension: 
	                    </td>
	                    <td>
                            <telerik:RadMaskedTextBox  ID="ExtensionRadMaskedTextBox" 
                                runat="server"
                                Mask="########"
                                Width="65px" />
	                    </td>
	                </tr>
                    <tr id="RoleTR" runat="server" visible="false">
	                    <td class="Col1">
	                    	Role: 
	                    </td>
	                    <td>
                            <asp:DropDownList ID="RoleDDL" runat="server">
                                <asp:ListItem Text="Please Select" Value="-1" />
                                <asp:ListItem Text="Admin" Value="Admin" />
                                <asp:ListItem Text="Developer" Value="Developer" />
                                <asp:ListItem Text="Client" Value="Client" />
                            </asp:DropDownList>
	                    </td>
	                </tr>
	                
			        <tr>
	                    <td>
	                    	&nbsp;
	                    </td>
	                    <td>
	                    	 <asp:Button ID="SubmitButton" runat="server" Text="Submit" Width="200px" UseSubmitBehavior="true" OnClick="Submit_OnClick" />
	                    </td>
	                    
	                </tr>
	             </table>

</asp:Content>