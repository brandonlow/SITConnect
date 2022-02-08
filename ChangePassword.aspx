<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SITConnect.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Change Password</legend>
              
                    <p>Password : <asp:TextBox ID="tb_pwd" runat="server" Height="25px" Width="137px" TextMode="Password"></asp:TextBox></p>
               
                    <p><asp:Button ID="btn_submit" runat="server" Text="Login" OnClick="btn_Submit_Click" Height="27px" Width="133px"/></p>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="False">Error message here(lblMessage)</asp:Label>

            </fieldset>
        </div>
    </form>
</body>
</html>
