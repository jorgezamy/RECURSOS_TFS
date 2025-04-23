<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="confirmacion_registro.aspx.cs" Inherits="recursos.Views.confirmacion_registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">
         
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="mensaje_confirmacion" ClientIDMode="Static" runat="server" Text="Los datos han sido actualizados exitosamente."></asp:Label>

        <br /><br />

        <asp:Button ID="btn_continuar" runat="server" Text="Continuar" CssClass="btn_guardarCancelar" OnClick="btn_continuar_Click" />

    </div>

</asp:Content>
