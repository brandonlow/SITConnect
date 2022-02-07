<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SITConnect.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>
             <script src="https://www.google.com/recaptcha/api.js?render=6LeOb2EeAAAAAB0F3yPY1ZwwtddvxnOLVPET5Ijc"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Login</legend>
                    <p>Email : <asp:TextBox ID="tb_userid" runat="server" Height="25px" Width="137px"></asp:TextBox></p>
              
                    <p>Password : <asp:TextBox ID="tb_pwd" runat="server" Height="25px" Width="137px"></asp:TextBox></p>
               
                    <p><asp:Button ID="btn_login" runat="server" Text="Login" OnClick="LoginMe" Height="27px" Width="133px"/></p>
                    <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
                    <asp:Label ID="lbl_gScore" runat="server" EnableViewState="False">Error message here(lblMessage)</asp:Label>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="False">Error message here(lblMessage)</asp:Label>
                    <br />
                    <br />
                
            </fieldset>
        </div>
    </form>
    <script>
     grecaptcha.ready(function () {
         grecaptcha.execute('6LeOb2EeAAAAAB0F3yPY1ZwwtddvxnOLVPET5Ijc', { action: 'Login' }).then(function (token) {
         document.getElementById("g-recaptcha-response").value = token;
         });
     });
    </script>
</body>
</html>