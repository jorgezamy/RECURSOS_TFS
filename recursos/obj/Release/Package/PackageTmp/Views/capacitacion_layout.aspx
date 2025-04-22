<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion_layout.aspx.cs" Inherits="recursos.Views.capacitacion_layout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div id="contenedor_layout">
        
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Carga Masiva"></asp:Label>

        <asp:UpdatePanel ID="UpdatePanel_cargaMasiva" runat="server">
            <ContentTemplate>

                <b>Curso:</b>
                
                <asp:DropDownList ID="drop_curso" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                </asp:DropDownList>
                
                <div id="div_capacitacion_contenedor">
                    <div class="div_capacitacion_contenedor_fechas">
                        <asp:Label runat="server">Fecha Inicio:</asp:Label>
                        <asp:TextBox ID="tb_fecha_inicial" runat="server" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="div_capacitacion_contenedor_fechas">
                        <asp:Label runat="server">Fecha Fin:</asp:Label>
                        <asp:TextBox ID="tb_fecha_final" runat="server" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="div_capacitacion_contenedor_fechas">
                        <asp:ImageButton ID="btnbuscar" ClientIDMode="Static" runat="server" ImageUrl="~/images/buscar.png" OnMouseOver="src='/images/buscar_white.png';" OnMouseOut="src='/images/buscar.png';" OnClick="btnbuscar_Click" />
                    </div>
                </div>

                <asp:GridView ID="grid_cargaMasiva" runat="server" AutoGenerateColumns="true" AllowPaging="true" PageSize="10" CssClass="grid_layout" OnPageIndexChanging="grid_cargaMasiva_PageIndexChanging">
                    <HeaderStyle CssClass="grid_layout_header" />
                    <RowStyle CssClass="grid_layout_row" />
                    <AlternatingRowStyle CssClass="grid_layout_altrow" />
                    <PagerStyle CssClass="grid_layout_pager" />
                    <Columns></Columns>

                    <EmptyDataTemplate>
                        No se encontraron datos.
                    </EmptyDataTemplate>
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_descargar" runat="server" Text="Descargar" CssClass="btn_guardarCancelar" Visible="true"  OnClick="btn_descargar_Click"/>
        
        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

        <asp:HiddenField ID="a" runat="server" ClientIDMode="Static" />

    </div>

</asp:Content>