<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="fortia_altas.aspx.cs" Inherits="recursos.Views.Fortia.fortia_altas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="grid_altas" runat="server">
<%--         <HeaderStyle CssClass="grid_buscar_header" />
                        <RowStyle CssClass="grid_buscar_row" />
                        <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                        <PagerStyle CssClass="grid_buscar_pager" />
                    
                        <EmptyDataTemplate>
                            No se encontraron datos.
                        </EmptyDataTemplate>--%>
    </asp:GridView>

</asp:Content>
