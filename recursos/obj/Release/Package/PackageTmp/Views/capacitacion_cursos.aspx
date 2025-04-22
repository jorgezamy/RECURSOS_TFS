<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion_cursos.aspx.cs" Inherits="recursos.Views.capacitacion_cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/jQuery/v3.2.1.js"></script>
    <script src="../Content/js/capacitacion_cursos.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">
        
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <div id="menuDiv">
            <asp:ImageButton ID="b1" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/cursos/menuBtn_recursos_capacitacion_cursos_disabled_1.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b1_Click" />
            <asp:ImageButton ID="b2" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/capacitacion/cursos/menuBtn_recursos_capacitacion_cursos_disabled_2.png" CssClass="menuBtn_disabled" Enabled="false" OnClick="b2_Click" />
        </div>

        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

        <asp:HiddenField ID="a" runat="server" ClientIDMode="Static" />

    </div>

</asp:Content>
