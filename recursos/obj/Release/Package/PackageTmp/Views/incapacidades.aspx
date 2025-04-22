<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="incapacidades.aspx.cs" Inherits="recursos.Views.incapacidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_incapacidades" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Incapacidades"></asp:Label>

                <asp:Table ID="tabla_incapacidades" runat="server" CssClass="tabla_registrar">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="No. empleado:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="data_empleado" runat="server" SelectCommand="select no_empleado from tnch_rh.dbo.empleados where fecha_baja is null and fecha_egreso is null" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                            <asp:DropDownList ID="ddlEmpleado" runat="server" DataSourceID="data_empleado" DataTextField="no_empleado" DataValueField="no_empleado" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowNombre" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Nombre:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblNombre" runat="server" Width="100%"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDepartamento" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Departamento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblDepartamento" runat="server" Width="100%"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowPuesto" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Puesto:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblPuesto" runat="server" Width="100%"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowFolio" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Folio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_folio" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVFolio" runat="server" ControlToValidate="tb_folio" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="*Folio en blanco" ValidationGroup="Guardar"></asp:RequiredFieldValidator>

                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowFecha" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Día de inicio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaInicio" runat="server" Text="" placeholder="Fecha" TextMode="Date" OnTextChanged="txtFechaInicio_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVFechaIni" runat="server" ControlToValidate="txtFechaInicio" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="*Seleccione día de inicio" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDias" Visible="false">

                        <asp:TableCell>
                            <asp:Label runat="server" Text="Días otorgados:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tbDias" runat="server" Text="" placeholder="Días" TextMode="Number" Min="1" Enabled="false" OnTextChanged="tbDias_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVDias" runat="server" ControlToValidate="tbDias" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="*Días en blanco" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>

                    </asp:TableRow>
                    <asp:TableRow ID="rowFechaFin" Visible="false">

                        <asp:TableCell>
                            <asp:Label runat="server" Text="Día de finalización:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaFin" runat="server" Text="" placeholder="Fecha" TextMode="Date" Enabled="False"></asp:TextBox>
                        </asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow ID="rowCausa" Visible="false">
                        <asp:TableCell>
                            <asp:Label ID="lblCausa" runat="server" Text="Causa:" ></asp:Label> <%--Visible="false"--%>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="SQLDSCausa" runat="server" SelectCommand="select id_causa_incapacidad, descripcion from tnch_rh.dbo.incapacidades_causa where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>

                            <asp:DropDownList ID="ddlCausa" runat="server" DataSourceID="SQLDSCausa" DataTextField="descripcion" DataValueField="id_causa_incapacidad" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCausa_SelectedIndexChanged" AutoPostBack="true" > <%--Visible="false"--%>
                                <asp:ListItem Text="-- Seleccionar --" Value="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RFVCausa" runat="server" ControlToValidate="ddlCausa" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Seleccione causa" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowRiesgo" Visible="false">
                        <asp:TableCell>
                            <asp:Label ID="lblRiesgo" runat="server" Text="Tipo de riesgo:" ></asp:Label> <%--Visible="false"--%>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="SQLDSRiesgo" runat="server" SelectCommand="select id_tipo_incapacidad, descripcion from tnch_rh.dbo.incapacidades_tipo where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>

                            <asp:DropDownList ID="ddlRiesgo" runat="server" DataSourceID="SQLDSRiesgo" DataTextField="descripcion" DataValueField="id_tipo_incapacidad" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlRiesgo_SelectedIndexChanged" AutoPostBack="true" > <%--Visible="false"--%>
                                <asp:ListItem Text="-- Seleccionar --" Value="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RFVRiesgo" runat="server" ControlToValidate="ddlRiesgo" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Seleccione tipo riesgo" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowSecuela" Visible="false">
                        <asp:TableCell>
                            <asp:Label ID="lblSecuela" runat="server" Text="Tipo de secuela:" ></asp:Label> <%--Visible="false"--%>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="SQLDSSecuela" runat="server" SelectCommand="select id_secuela_incapacidad, descripcion from tnch_rh.dbo.incapacidades_secuela where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>

                            <asp:DropDownList ID="ddlSecuela" runat="server" DataSourceID="SQLDSSecuela" DataTextField="descripcion" DataValueField="id_secuela_incapacidad" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSecuela_SelectedIndexChanged" AutoPostBack="true" > <%--Visible="false"--%>
                                <asp:ListItem Text="-- Seleccionar --" Value="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RFVSecuela" runat="server" ControlToValidate="ddlSecuela" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Seleccione tipo secuela" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowControl" Visible="false">
                        <asp:TableCell>
                            <asp:Label ID="lblControl" runat="server" Text="Control de incapacidad:" ></asp:Label> <%--Visible="false"--%>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<asp:SqlDataSource ID="SQLDSControl" runat="server" SelectCommand="select id_control_incapacidad, descripcion from tnch_rh.dbo.incapacidades_control where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>--%>


                            <asp:DropDownList ID="ddlControl" runat="server"  AppendDataBoundItems="true" Visible="true" > <%--DataSourceID="SQLDSControl" DataTextField="descripcion" DataValueField="id_control_incapacidad"--%>
                                <asp:ListItem Text="-- Seleccionar --" Value="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>

                            <asp:RequiredFieldValidator ID="RFVControl" runat="server" ControlToValidate="ddlControl" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Seleccione control" ValidationGroup="Guardar"></asp:RequiredFieldValidator>

                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowInicial" Visible="false">
                        <asp:TableCell>
                            <asp:Label ID="lblInicial" runat="server" Text="¿Es incapacidad inicial?" ></asp:Label> <%--Visible="false"--%>
                        </asp:TableCell>
                        <asp:TableCell>

                            <%--<asp:DropDownList ID="ddlInicial" runat="server" AppendDataBoundItems="true" >
                                <asp:ListItem Text="-- Seleccionar --" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Inicial" Value="1"></asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:RadioButtonList ID="RBLInicial" runat="server">
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Sí" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RFVInicial" runat="server" ControlToValidate="RBLInicial" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- ¿Es inicial?" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowComentarios" runat="server" Visible="false">
                        <asp:TableCell>
                            <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" ></asp:Label> <%--Visible="false"--%>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtComentarios" TextMode="MultiLine" Columns="50" Rows="5" runat="server" /> <%--Visible="false"--%>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <br />
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" ValidationGroup="Guardar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_guardar_Click" />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn_guardarCancelar" OnClick="btn_cancelar_Click" />
                <br />
                <asp:Label ID="mensaje_error" ClientIDMode="Static" runat="server" Text=""></asp:Label>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>