<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="Login_TFS.Views.menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="../../Content/js/menu.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server"></asp:Label>

        <div id="menuDiv">
            <asp:ImageButton ID="b0" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_0.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b0_Click" />
            <asp:ImageButton ID="b1" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_1.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b1_Click" />
            <asp:ImageButton ID="b2" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_2.png" CssClass="menuBtn_disabled" Enabled="false" />
            <asp:ImageButton ID="b3" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_3.png" CssClass="menuBtn_disabled" Enabled="false" />
            <asp:ImageButton ID="b4" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_4.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b4_Click" />
            <asp:ImageButton ID="b5" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_5.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b5_Click" />
            <asp:ImageButton ID="b6" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_6.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b6_Click" />
            <asp:ImageButton ID="b7" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_7.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b7_Click" />     
            <asp:ImageButton ID="b8" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_8.png" CssClass="menuBtn_disabled" Enabled="false" />
            <asp:ImageButton ID="b9" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_9.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b9_Click" />
            <asp:ImageButton ID="b10" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_10.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b10_Click" />
            <asp:ImageButton ID="b11" ClientIDMode="Static" runat="server" ImageUrl="~/images/menu/menuBtn_login_disabled_11.png" CssClass="menuBtn_disabled" Enabled="true" OnClick="b11_Click" />

        </div>        

        <asp:HiddenField ID="a" runat="server" ClientIDMode="Static" />

        <asp:Label ID="mensaje_error" runat="server" Text="" CssClass="mensaje_error"></asp:Label>

        <asp:Button ID="btn_regresar" runat="server" Text="Cerrar Sesión" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>