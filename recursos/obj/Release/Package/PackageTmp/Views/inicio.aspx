<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="recursos.Views.inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/jQuery/v3.2.1.js"></script>
    <script src="../Content/js/inicio.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">
        
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <div id="menuDiv">
            <asp:ImageButton ID="b1" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_1.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b1_Click" />
            <asp:ImageButton ID="b2" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_2.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b2_Click" />
            <asp:ImageButton ID="b3" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_3.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b3_Click"/>
            <asp:ImageButton ID="b4" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_4.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b4_Click" />
            <asp:ImageButton ID="b5" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_5.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b5_Click" />
            <asp:ImageButton ID="b6" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_6.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b6_Click" />
            <asp:ImageButton ID="b7" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_7.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b7_Click" />
            <asp:ImageButton ID="b8" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_8.png" CssClass="menuBtn_disabled" Enabled="false" />
            <asp:ImageButton ID="b9" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_9.png" CssClass="menuBtn_disabled" Enabled="false" />
            <asp:ImageButton ID="b10" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_10.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b10_Click" />
            <asp:ImageButton ID="b11" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_11.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b11_Click" />
            <asp:ImageButton ID="b12" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_12.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b12_Click" />
            <asp:ImageButton ID="b13" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_13.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b13_Click" />
            <asp:ImageButton ID="b14" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_14.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b14_Click" />
            <asp:ImageButton ID="b15" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_15.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b15_Click" />           
            <asp:ImageButton ID="b16" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_16.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b16_Click" />
            <asp:ImageButton ID="b17" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_17.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b17_Click" />
            <asp:ImageButton ID="b18" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_18.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b18_Click" />
            <asp:ImageButton ID="b19" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_19.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b19_Click" />
            <asp:ImageButton ID="b20" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_20.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b20_Click" />
            <asp:ImageButton ID="b21" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/menuBtn_recursos_disabled_21.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b21_Click"/>

        </div>

        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Menú Principal" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

        <asp:HiddenField ID="a" runat="server" ClientIDMode="Static" />

    </div>

</asp:Content>