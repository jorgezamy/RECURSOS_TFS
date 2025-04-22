<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion.aspx.cs" Inherits="recursos.Views.capacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/jQuery/v3.2.1.js"></script>
    <script src="../Content/js/capacitacion.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">
        
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <div id="menuDiv">
            <%--<asp:ImageButton ID="b0" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/menuBtn_recursos_capacitacion_disabled_1.png" CssClass="menuBtn_disabled" Enabled="false"  />--%>
            <asp:ImageButton ID="b1" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/menuBtn_recursos_capacitacion_disabled_1.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b1_Click" />
            <asp:ImageButton ID="b2" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/menuBtn_recursos_capacitacion_disabled_2.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b2_Click" />
            <asp:ImageButton ID="b3" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/menuBtn_recursos_capacitacion_disabled_3.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b3_Click" />
            <asp:ImageButton ID="b4" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/menuBtn_recursos_capacitacion_disabled_4.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b4_Click" />
        </div>

        <asp:HiddenField ID="a" runat="server" ClientIDMode="Static" />
         
        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

    </div>

</asp:Content>