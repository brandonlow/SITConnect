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
        <asp:Image ID="Image1" runat="server" Height="200px" Width="200px" />
        <h2>User Name : <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;<asp:Label ID="Label2" runat="server"></asp:Label>
        <h2>User Email : <asp:Label ID="lbl_userID" runat="server"></asp:Label>
        </h2>
        <h2>Credit Card Information :&nbsp;
            <asp:Label ID="lbl_card" runat="server"></asp:Label>
        </h2>

    </div>
    </form>

</body>

</html>

