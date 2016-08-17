<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication.Account.Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>vSupport</title>
     <style type="text/css">
    html, body, form
    {
        height: 100%;
        margin: 0px;
        padding: 0px;
    }  
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table cellpadding="0" cellspacing="0" width="100%" >
        <tr>
            <td colspan="2" style="border-bottom: 1px; border-bottom-style: solid; border-bottom-color: Black; text-align:right; padding: 5px;" >
                <div style="float:left">
                    eSmartChart
                </div>
                <div style=" border: 1px; border-style: solid; border-color:Black; padding:3px; width: 200px; float: right; text-align:left " >
                    <b>User Name:</b> <asp:Label ID="LoggedInUserLabel" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <div id="ParentDivElement" style="height: 100%;"> 
        <telerik:RadSplitter ID="RadSplitter1" skin="WebBlue" Height="100%" Width="100%" runat="server">
            <telerik:RadPane ID="MiddlePane1" runat="server" Scrolling="None">
                <telerik:RadSplitter ID="Radsplitter2" runat="server" Orientation="Horizontal" VisibleDuringInit="false">
                    <telerik:RadPane ID="Radpane2" runat="server">
                        <div style="padding: 0px">
                              <table width="0px">
                                <tr>
                                    <td style="width: 10px;">
                                        <telerik:RadUpload ID="FileUpload1" Width="280px" AllowedFileExtensions=".tiff,.jpg,.tif,.pdf" runat="server" Skin="WebBlue" /> 
                                    </td>
                                    <td style="text-align:left; vertical-align:top" >
                                        <telerik:RadButton ID="UploadButton" runat="server" OnClick="UploadButton_OnClick" Text="Upload" Skin="WebBlue" />
                                    </td>
                                </tr>
                              </table>
                              <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource" ShowGroupPanel="true"
                                OnItemDataBound="RadGrid1_ItemDataBound" SelectedItemStyle-CssClass="SelectedStyle" OnItemCommand="RadGrid1_ItemCommand"
                                ShowStatusBar="true" Skin="WebBlue"   Width="100%" 
                                AllowSorting="True" AllowMultiRowSelection="False"
                                AllowPaging="True" PageSize="50" >
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                    <Selecting AllowRowSelect="True" />  
                                </ClientSettings>
                                <MasterTableView PageSize="50" CommandItemDisplay="Top" DataKeyNames="ID">
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"  ShowExportToPdfButton="true"
                                        ShowExportToCsvButton="true"  />
                                    <Columns>
                                        
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
            <telerik:RadSplitBar ID="RadSplitBar1" runat="server">
            </telerik:RadSplitBar>
             <telerik:RadPane ID="EndPane" Collapsed="false"  runat="server" Width="22px" Scrolling="None">
            <telerik:RadSlidingZone ID="Radslidingzone1" runat="server" Width="22px" ClickToOpen="true" 
                SlideDirection="Left">
                <telerik:RadSlidingPane ID="Radslidingpanel1" Title="File" runat="server" Width="700px" 
                    MinWidth="100">
                        <div style='overflow-x:scroll;overflow-y:hidden;width:700px;'>
                            <asp:Image ID="Image1" runat="server" />
                        </div>
                </telerik:RadSlidingPane>
            </telerik:RadSlidingZone>
        </telerik:RadPane>
        </telerik:RadSplitter>
    </div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Skin="Outlook" runat="server" />
    </form>
</body>
</html>
