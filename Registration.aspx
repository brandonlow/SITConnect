<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SITConnect.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
     <title>My Registration</title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <legend>Registration</legend>
                    <br />
                    <p>
                        First Name
                        <asp:TextBox ID="tb_firstname" runat="server"></asp:TextBox>
                    </p>

                    <p>
                        Last Name
                        <asp:TextBox ID="tb_lastname" runat="server"></asp:TextBox>
                    </p>

                    <p>
                        Credit Card info
                        <asp:TextBox ID="tb_creditcard" runat="server"></asp:TextBox>
                    </p>

                    <p>
                        Email Address
                        <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                    </p>

                    <p>
                        Password
                        <asp:TextBox ID="tb_password" runat="server" OnTextChanged="tb_password_TextChanged" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lbl_pwdchecker" runat="server" Text="pwdchecker" onkeyup="javascript:validate()"></asp:Label>
                    </p>

                    <p>
                        Date of Birth
                        <asp:TextBox ID="tb_DOB" runat="server" TextMode="Date"></asp:TextBox>
                    </p>

                    <p>
                        Photo
                        <asp:FileUpload ID="tb_photo" runat="server" />
                    </p>
                    <br />
                    <p>
                        <asp:Button ID="Button1" runat="server" OnClick="btn_Submit_Click" Text="Submit" />
                    </p>
                    <br />
            </fieldset>
        </div>
    </form>
</body>

    <script type ="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Must be at least 6 characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 lower case";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 upper case";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[^a-zA-Z0-9\-\/]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password require at least 1 special characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_specialcharacter");
            }
            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent";
            document.getElementById("lbl_pwdchecker").style.color = "Blue"
        }
    </script>

</html>
