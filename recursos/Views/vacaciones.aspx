<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="vacaciones.aspx.cs" Inherits="recursos.Views.vacaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_vacaciones" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Vacaciones"></asp:Label>

                <asp:Table ID="tabla_vacaciones" runat="server" CssClass="tabla_vacaciones">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="No. empleado:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="data_empleado" runat="server" SelectCommand="select no_empleado from GCDM_rh.dbo.empleados where fecha_baja is null and fecha_egreso is null" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                            <asp:DropDownList ID="ddlEmpleado" runat="server" DataSourceID="data_empleado" DataTextField="no_empleado" DataValueField="no_empleado" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowAnio" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Año:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlAnio" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"  AutoPostBack="true">
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
                    <asp:TableRow ID="rowIngreso" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Fecha ingreso:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblIngreso" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowAntiguedad" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Antigüedad:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblAntiguedad" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowPeriodo" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Periodo actual de vacaciones:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblPeriodo" runat="server" Text=""></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:HiddenField ID="HFInicioPeriodo" runat="server" /></asp:TableCell>
                        <asp:TableCell><asp:HiddenField ID="HFFinPeriodo" runat="server" /></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDiasOtorgados" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Días de vacaciones otorgados:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblOtorgados" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDiasTomados" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Días de vacaciones tomados:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblTomados" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDiasPagados" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Días de vacaciones pagados:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblPagados" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDiasRestantes" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Días restantes de tomar:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblRestantes" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowTiempoRestante" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Tiempo restante para tomar las vacaciones:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblTiempoRestante" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowTramite" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Trámite:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlTramite" runat="server" OnSelectedIndexChanged="ddlTramite_SelectedIndexChanged" AutoPostBack="true">
<%--                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                <asp:ListItem Text="Vacaciones" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Vacaciones y pago" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Sólo pago" Value="3"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowFecha" Visible="false">

                        <asp:TableCell>
                            <asp:Label runat="server" Text="Día de inicio:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaInicio" runat="server" Text="" placeholder="Fecha" TextMode="Date" AutoPostBack="true" OnTextChanged="txtFechaInicio_TextChanged"></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Label ID="lblDiasAprobados" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txt_dias" runat="server" Text="" placeholder="Días" TextMode="Number" Min="1" AutoPostBack="true" Enabled="false" OnTextChanged="txt_dias_TextChanged"></asp:TextBox>
                        </asp:TableCell>


                        <asp:TableCell>
                            <asp:Label runat="server" Text="Día de finalización:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaFin" runat="server" Text="" placeholder="Fecha" TextMode="Date" Enabled="false"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>




                    <asp:TableRow ID="rowDiasPagar" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Días a pagar:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtDiasPagar" runat="server" Text="" placeholder="Días a pagar" TextMode="Number" Enabled="false" ></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowFechaPago" Visible="false">
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Fecha de pago:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtFechaPago" runat="server" Text="" placeholder="Fecha de pago" TextMode="Date" ></asp:TextBox>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:Label runat="server" Text="Año:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtAnio" runat="server" placeholder="Año" TextMode="Number" Enabled="false"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RFVTramite" runat="server" ControlToValidate="ddlTramite" CssClass="RequiredValidator"
                                Display="Dynamic" ErrorMessage="-- Seleccione trámite" ValidationGroup="Guardar">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RFVFechaIni" runat="server" ControlToValidate="txtFechaInicio" CssClass="RequiredValidator" 
                                Display="Dynamic" ErrorMessage="-- Día de inicio en blanco" ValidationGroup="Guardar">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RFVFechaFin" runat="server" ControlToValidate="txt_dias" CssClass="RequiredValidator"
                                Display="Dynamic" ErrorMessage="-- Días aprobados en blanco" ValidationGroup="Guardar">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>



                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RFVDiasPagar" runat="server" ControlToValidate="txtDiasPagar" CssClass="RequiredValidator"
                                Display="Dynamic" ErrorMessage="-- Días a pagar en blanco" ValidationGroup="Guardar">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RFVFechaPago" runat="server" ControlToValidate="txtFechaPago" CssClass="RequiredValidator"
                                Display="Dynamic" ErrorMessage="-- Fecha pago en blanco" ValidationGroup="Guardar">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>

                        <asp:TableCell>
                            <asp:RequiredFieldValidator ID="RFVAño" runat="server" ControlToValidate="txtAnio" CssClass="RequiredValidator"
                                Display="Dynamic" ErrorMessage="-- Año en blanco" ValidationGroup="Guardar">
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>



                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="5">
                            <asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtComentarios" TextMode="MultiLine" Columns="50" Rows="5" runat="server" Visible="false"/>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>

                <div id="botones_vacaciones">

                <asp:Label ID="mensaje_error" ClientIDMode="Static" runat="server" CssClass="mensaje_error"></asp:Label>
                
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" ValidationGroup="Guardar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_guardar_Click"  />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn_guardarCancelar" OnClick="btn_cancelar_Click" />

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>
