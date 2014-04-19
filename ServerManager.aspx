<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServerManager.aspx.cs" Inherits="StudentsServerWeb.ServerManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 463px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <h1 style="text-align: center">Server manager</h1>
    <div>
    
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Status:</td>
                <td>
        <asp:Label ID="lblStatus" runat="server" Text="Server not running." Font-Bold="True" Font-Names="Arial" ForeColor="Red"></asp:Label>
    
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2" valign="top">
                    Server type:</td>
                <td>
                    <asp:RadioButtonList ID="rblServerType" runat="server">
                        <asp:ListItem>Echo</asp:ListItem>
                        <asp:ListItem Value="Students data">Students data</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlClasses" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
            <asp:Button ID="bStart" runat="server" Text="Start" OnClick="bStart_Click" />
                </td>
                <td>
            <asp:Button ID="bStop" runat="server" Text="Stop" OnClick="bStop_Click" />
                </td>
            </tr>
        </table>
    
    </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
