<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="SITConnect.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>User Profile</h2>
        <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" />
        <h2>User ID : <asp:Label ID="lbl_userID" runat="server"></asp:Label>
        </h2>
        <h2>Credit Card Information :&nbsp;
            <asp:Label ID="lbl_card" runat="server"></asp:Label>
        </h2>

    </div>
    </form>

</body>

</html>

