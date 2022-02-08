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
              
                    <p>Password : <asp:TextBox ID="tb_pwd" runat="server" Height="25px" Width="137px" TextMode="Password" onkeyup="javascript:validate()" required ="required"></asp:TextBox></p>
               
                    <p><asp:Button ID="btn_submit" runat="server" Text="Login" OnClick="btn_Submit_Click" Height="27px" Width="133px"/></p>
                    <br />
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="False">Error message here(lblMessage)</asp:Label>

            </fieldset>
        </div>
    </form>
</body>
    <script type ="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_pwd.ClientID %>').value;

            if (str.length < 12) {
                document.getElementById("lblMessage").innerHTML = "Must be at least 12 characters";
                document.getElementById("lblMessage").style.color = "Red";
                return ("too short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lblMessage").innerHTML = "Password require at least 1 number";
                document.getElementById("lblMessage").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lblMessage").innerHTML = "Password require at least 1 lower case";
                document.getElementById("lblMessage").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lblMessage").innerHTML = "Password require at least 1 upper case";
                document.getElementById("lblMessage").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[^a-zA-Z0-9\-\/]/) == -1) {
                document.getElementById("lblMessage").innerHTML = "Password require at least 1 special characters";
                document.getElementById("lblMessage").style.color = "Red";
                return ("no_specialcharacter");
            }
            document.getElementById("lblMessage").innerHTML = "Excellent";
            document.getElementById("lblMessage").style.color = "Blue"
        }
       
    </script>
</html>
