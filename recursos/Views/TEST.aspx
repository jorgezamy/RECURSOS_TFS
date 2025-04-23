<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="TEST.aspx.cs" Inherits="recursos.Views.TEST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div id="contenedor">

        <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/seleccionar.png" runat="server" Width="20%" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" Width="40" Minimum="0" TargetButtonUpID="ImageButton1" TargetControlID="TextBox1"  runat="server" />

    </div>

</asp:Content>