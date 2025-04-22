<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="entradas_salidas.aspx.cs" Inherits="recursos.Views.entradas_salidas"  EnableEventValidation = "false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div id="contenedor" >
        
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Entradas y salidas"></asp:Label>
        
        <asp:UpdatePanel ID="UpdatePanel_editarTractor" runat="server">
            <ContentTemplate>

        <asp:Table ID="table_entradas" runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Departamento:"></asp:Label>    
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drop_depto" runat="server" AppendDataBoundItems="true" AutoPostBack="true" Visible="true" OnSelectedIndexChanged="drop_depto_SelectedIndexChanged">
                        <asp:ListItem Text="-- seleccionar --" Value=""></asp:ListItem>
                    </asp:DropDownList>        
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="Cliente:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drop_cliente" runat="server" AppendDataBoundItems="true" AutoPostBack="true" Visible="true" Enabled="false" OnSelectedIndexChanged="drop_cliente_SelectedIndexChanged">
                        <asp:ListItem Text="-- seleccionar --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat="server" Text="No. Empleado:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drop_noEmpleado" runat="server" AppendDataBoundItems="true" AutoPostBack="false" Visible="true" Enabled="false" OnSelectedIndexChanged="drop_noEmpleado_SelectedIndexChanged">
                        <asp:ListItem Text="-- seleccionar --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

        <br />
        
        <b>Fecha Entrada:</b> 
        
        <asp:TextBox ID="fecha_inicial" runat="server" TextMode="Date"></asp:TextBox>

        &nbsp; &nbsp; &nbsp; &nbsp;
        
        <b>Fecha Salida:</b>
        
        <asp:TextBox ID="fecha_final" runat="server" TextMode="Date"></asp:TextBox>
        
        &nbsp;
        
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        
        <br />
        
        <asp:Label ID="txtAlerta" runat="server" Text="" Font-Bold="True" ForeColor="#CC0000"></asp:Label>
        
        <br />
        <br />

        <asp:GridView ID="grid_entradas" ClientIDMode="Static" runat="server" AllowPaging="true" PageSize="10" ShowFooter="false" HorizontalAlign="Center" CssClass="grid_entradas" OnPageIndexChanging="grid_entradas_PageIndexChanging">
            <HeaderStyle CssClass="grid_entradas_header" />
            <RowStyle CssClass="grid_entradas_row" />
            <AlternatingRowStyle CssClass="grid_entradas_altrow" />
            <PagerStyle CssClass="grid_entradas_pager" />

            <EmptyDataTemplate>
                No se encontrarón datos.
            </EmptyDataTemplate>
        </asp:GridView>

        <br />

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btnExportar" ClientIDMode="Static" runat="server" Text="Exportar" Enabled="true" OnClick="btnExportar_Click" />

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>
