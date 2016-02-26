<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication4.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="ID"></asp:Label>
        <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="NAME"></asp:Label>
        <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="EMAIL"></asp:Label>
        <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="PHONE"></asp:Label>
        <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonSave" runat="server" onclick="ButtonSave_Click" 
            Text="Save" />
        <asp:Button ID="ButtonUpdateDB" runat="server" onclick="ButtonUpdateDB_Click" 
            Text="UpdateDB" />
        <asp:Button ID="Undo" runat="server" onclick="Undo_Click" Text="Undo" />
        <br />
        <br />
        <asp:Label ID="LabelMessage" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="StudentId" onrowcancelingedit="GridView1_RowCancelingEdit" 
            onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
            onrowupdating="GridView1_RowUpdating">
            <Columns>
                <asp:BoundField DataField="StudentId" HeaderText="AIUB ID" ReadOnly="True" />
                <asp:BoundField DataField="StudentName" HeaderText="NAME" />
                <asp:BoundField DataField="Email" HeaderText="EMAIL" />
                <asp:BoundField DataField="Phone" HeaderText="PHONE" />
                <asp:CommandField HeaderText="OPTIONS" ShowDeleteButton="True" 
                    ShowEditButton="True" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
    </form>
</body>
</html>
