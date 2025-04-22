<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="retardos.aspx.cs" Inherits="recursos.Views.retardos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_retardos" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Retardos"></asp:Label>

                <asp:Table ID="tabla_retardos" runat="server" CssClass="tabla_registrar">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="No. empleado:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="data_empleado" runat="server" SelectCommand="select no_empleado from GCDM_rh.dbo.empleados where fecha_baja is null and fecha_egreso is null" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                            <asp:DropDownList ID="ddlEmpleado" runat="server" DataSourceID="data_empleado" DataTextField="no_empleado" DataValueField="no_empleado" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowNombre" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Nombre:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDepartamento" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Departamento:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblDepartamento" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowPuesto" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowFecha" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Día y hora de retardo:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="txtFecha" runat="server" Text="" placeholder="Fecha" TextMode="DateTimeLocal"></asp:TextBox></asp:TableCell>
                        <asp:TableCell><asp:RequiredFieldValidator ID="RFVFecha" runat="server" ControlToValidate="txtFecha" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Día en blanco" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" Visible="false"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="txtComentarios" TextMode="MultiLine" Columns="50" Rows="5" runat="server" Visible="false"/></asp:TableCell>
                    </asp:TableRow>


                </asp:Table>

                <asp:Label ID="mensaje_error" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" ValidationGroup="Guardar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_guardar_Click" />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn_guardarCancelar" OnClick="btn_cancelar_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>