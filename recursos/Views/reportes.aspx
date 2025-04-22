<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="reportes.aspx.cs" Inherits="recursos.Views.reportes"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



    <div style="text-align: center; width:85%; margin: 0 auto;" >
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="Titulo" ClientIDMode="Static" runat="server" Text="Reportes" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br /> <br />

        <asp:Button ID="tab1" Text="Altas" runat="server" OnClick="tab1_Click" CssClass="initial" />
        <asp:Button ID="tab2" Text="Bajas" runat="server" OnClick="tab2_Click" CssClass="initial" />       
        <asp:Button ID="tab7" Text="Modificaciones" runat="server" OnClick="tab7_Click" CssClass="initial" />                 

        <asp:Button ID="tab3" Text="Activos" runat="server" OnClick="tab3_Click" CssClass="initial" />
        <asp:Button ID="tab4" Text="Aniversario" runat="server" OnClick="tab4_Click" CssClass="initial" />
        <asp:Button ID="tab5" Text="Cumpleaños" runat="server" OnClick="tab5_Click" CssClass="initial" />
        <asp:Button ID="tab6" Text="Vencimientos" runat="server" OnClick="tab6_Click" CssClass="initial" />

        <asp:Button ID="tab8" Text="Incapacidades" runat="server" OnClick="tab8_Click" CssClass="initial" />
        <asp:Button ID="tab9" Text="Suspensiones" runat="server" OnClick="tab9_Click" CssClass="initial" />
        <asp:Button ID="tab10" Text="Vacaciones" runat="server" OnClick="tab10_Click" CssClass="initial" />

        <asp:Button ID="tab11" Text="Permisos" runat="server" OnClick="tab11_Click" CssClass="initial" />
        <asp:Button ID="tab12" Text="Hrs Extra" runat="server" OnClick="tab12_Click" CssClass="initial" />

        <asp:Button ID="tab13" Text="Extras" runat="server" OnClick="tab13_Click" CssClass="initial" />
        <asp:Button ID="tab14" Text="Día festivo" runat="server" OnClick="tab14_Click" CssClass="initial" />

        <asp:Button ID="tab15" Text="Avanzado" runat="server" OnClick="tab15_Click" CssClass="initial" />


        <br />
        <div runat="server" visible="false" id="div_reporte" style="border:outset; width:100%; margin: 0 auto"  >
            <asp:Table ID="tabla_contenido" runat="server" style="margin:0 auto" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:MultiView ID="multi_view" runat="server">
                            <asp:View ID="altas" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de altas" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table ID="tb_add_emp" runat="server" ClientIDMode="Static">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_busqueda_alta" runat="server" onkeypress="return TextBox_Buscar_Altas(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_inicio_altas" runat="server" TextMode="Date" ClientIDMode="Static" ></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_fin_altas" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View runat="server" ID="bajas">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de bajas" Font-Bold="true"></asp:Label>
                                 <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_busqueda_baja" runat="server" onkeypress="return TextBox_Buscar_Bajas(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar"></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_inicio_bajas" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_fin_bajas" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View runat="server" ID="activos">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de empleados activos" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_busqueda_activo" runat="server" onkeypress="return TextBox_Buscar_Activos(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar"></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    
                                </asp:Table>
                            </asp:View>
                            <asp:View runat="server" ID="aniversario">
                              <br />
                                <asp:Label  runat="server" Text="Reporte de aniversario" Font-Bold="true"></asp:Label>
                                 <br /><br />
                                <asp:DropDownList ID="drop_fecha_aniversario" runat="server" AppendDataBoundItems="true" CssClass="dropdown_buscar" >
                                    <asp:ListItem Selected="True" Text="Todos" Value="all"></asp:ListItem>
                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </asp:View>
                            <asp:View runat="server" ID="cumpleaños">
                              <br />
                                <asp:Label  runat="server" Text="Reporte de cumpleaños" Font-Bold="true"></asp:Label>
                                  <br /><br />
                                    <asp:DropDownList ID="drop_down_cumpleaños" runat="server" AppendDataBoundItems="true" CssClass="dropdown_buscar">
                                        <asp:ListItem Selected="True" Text="Todos" Value="all"></asp:ListItem>
                                        <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />                                   
                            </asp:View>
                            <asp:View runat="server" ID="vencimientos">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de vencimientos" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table ID="tabla_vencimientos" runat="server" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_busqueda_vencimiento" runat="server" onkeypress="return TextBox_Buscar_Vencimientos(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <asp:Label runat="server" Text="Documento:"></asp:Label>
                                <asp:DropDownList ID="drop_documento" runat="server" AppendDataBoundItems="true" CssClass="dropdown_buscar" ClientIDMode="Static" >
                                    <asp:ListItem Selected="True" Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Apto" Value="apto"></asp:ListItem>
                                    <asp:ListItem Text="Gafete único" Value="gafete"></asp:ListItem>
                                    <asp:ListItem Text="Carta penal" Value="penal"></asp:ListItem>
                                    <asp:ListItem Text="Licencia" Value="licencia"></asp:ListItem>
                                    <asp:ListItem Text="Constancia" Value="constancia"></asp:ListItem>
                                    <asp:ListItem Text="Comprobante de domicilio" Value="comprobante"></asp:ListItem>
                                    <asp:ListItem Text="VISA" Value="visa"></asp:ListItem>

                                    <asp:ListItem Text="Todos" Value="all"></asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </asp:View>
                            <asp:View ID="cambios" runat="server">
                                 <br />
                                <asp:Label  runat="server" Text="Reporte de cambios" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_busqueda_cambios" runat="server" onkeypress="return TextBox_Buscar_Bajas(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar"></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_inicio_cambios" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_fin_cambios" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="incapacidades" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de incapacidades" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtBuscarIncapacidades" runat="server" onkeypress="return TextBox_Buscar_Incapacidades(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtInicioIncapacidades" runat="server" TextMode="Date" ClientIDMode="Static" ></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtFinIncapacidades" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="suspensiones" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de suspensiones" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtBuscarSuspensiones" runat="server" onkeypress="return TextBox_Buscar_Suspensiones(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtInicioSuspensiones" runat="server" TextMode="Date" ClientIDMode="Static" ></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtFinSuspensiones" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="vacaciones" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de vacaciones" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtBuscarVacaciones" runat="server" onkeypress="return TextBox_Buscar_Vacaciones(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtInicioVacaciones" runat="server" TextMode="Date" ClientIDMode="Static" ></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtFinVacaciones" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="permisos" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de permisos" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtBuscarPermisos" runat="server" onkeypress="return TextBox_Buscar_Permisos(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtInicioPermisos" runat="server" TextMode="Date" ClientIDMode="Static" ></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtFinPermisos" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="hrsExtra" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de horas extra" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtBuscarHrsExtra" runat="server" onkeypress="return TextBox_Buscar_HrsExtra(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtInicioHrsExtra" runat="server" TextMode="Date" ClientIDMode="Static" ></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtFinHrsExtra" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="extras" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de bonos extra" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtBuscarExtras" runat="server" onkeypress="return TextBox_Buscar_Extras(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar" ></asp:TextBox>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Tipo de bono:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList ID="ddlExtras" runat="server" ClientIDMode="Static">
                                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                                <asp:ListItem Text="* Todos *" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Bono de chofer extra" Value="31"></asp:ListItem>
                                                <asp:ListItem Text="Bono de chofer maestro" Value="33"></asp:ListItem>
                                                <asp:ListItem Text="Bono de jefe de patio" Value="37"></asp:ListItem>
                                                <asp:ListItem Text="Bono de jefe de ruta" Value="38"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="diaFestivo" runat="server">
                                <br />
                                <asp:Label  runat="server" Text="Reporte de día festivo trabajado" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Tipos de empleado:"></asp:Label>
                                            <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:SqlDataSource ID="data_empleado" runat="server" SelectCommand="select id_tipo, desc_esp from GCDM_RH.dbo.tipo_empleado" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                            <asp:DropDownList ID="ddlFestivo" runat="server" ClientIDMode="Static" AppendDataBoundItems="true" DataSourceID="data_empleado" DataValueField="id_tipo" DataTextField="desc_esp">
                                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                                <asp:ListItem Text="* Todos *" Value="0"></asp:ListItem>

                                            </asp:DropDownList>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>

                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtInicioFestivo" runat="server" TextMode="Date" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtInicioFestivo_TextChanged" ></asp:TextBox>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkDiaFestivo" runat="server" AutoPostBack="true" OnCheckedChanged="chkDiaFestivo_CheckedChanged" Text="¿Sólo este día?" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtFinFestivo" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>

                            <asp:View ID="avanzado" runat="server">
                                 <br />
                                <asp:Label  runat="server" Text="Reporte Avanzado" Font-Bold="true"></asp:Label>
                                <br /><br />
                                <asp:Table  runat="server" ClientIDMode="Static" HorizontalAlign="Center">
                                    <asp:TableRow>
                                        <asp:TableCell >
                                           <asp:Label runat="server" Text="Busqueda:"></asp:Label>
                                           <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_busqueda_avanzada" runat="server" onkeypress="return TextBox_Buscar_Avanzado(event);"  placeholder="Ingresa número ó nombre" CssClass="tb_buscar"></asp:TextBox>

<%--                                            <asp:TextBox ID="tb_busqueda_avanzada" runat="server" onkeypress="return TextBox_Buscar_Activos(event);" placeholder="Ingresa número ó nombre" CssClass="tb_buscar"></asp:TextBox>--%>
                                            <br /><br />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:CheckBox ID="checkbox_activos" runat="server" />
                                            <asp:Label ID="lbl_activos" runat="server" Text="Empleados Activos"></asp:Label>
                                            <br /><br />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                             <div style="text-align: right">
                                                <asp:CheckBoxList ID="cheboxlist_avanzado" runat="server" CellPadding="5"
                                                    CellSpacing="20"
                                                       RepeatColumns="3"
                                                       RepeatDirection="Vertical"
                                                       RepeatLayout="Table"
                                                       TextAlign="Left">

                                                    <asp:ListItem Value ="0">No. Empleado</asp:ListItem>
                                                    <asp:ListItem Value ="1">Nombre</asp:ListItem>
                                                    <asp:ListItem Value ="2">Departamento</asp:ListItem>
                                                    <asp:ListItem Value ="3">Puesto</asp:ListItem>
                                                    <asp:ListItem Value ="4">Tipo</asp:ListItem>
                                                    <asp:ListItem Value ="5">Cliente</asp:ListItem>
                                                    <asp:ListItem Value ="6">Dirección</asp:ListItem>
                                                    <asp:ListItem Value ="7">Colonia</asp:ListItem>
                                                    <asp:ListItem Value ="8">Codigo Postal</asp:ListItem>
                                                    <asp:ListItem Value ="9">Sexo</asp:ListItem>
                                                    <asp:ListItem Value ="10">Fecha Nacimiento</asp:ListItem>
                                                    <asp:ListItem Value ="11">Lugar Nacimiento</asp:ListItem>
                                                    <asp:ListItem Value ="12">Estado Civil</asp:ListItem>
                                                    <asp:ListItem Value ="13">Telefono</asp:ListItem>
                                                    <asp:ListItem Value ="14">Número emergencia</asp:ListItem>
                                                    <asp:ListItem Value ="15">RFC</asp:ListItem>
                                                    <asp:ListItem Value ="16">CURP</asp:ListItem>
                                                    <asp:ListItem Value ="17">IMSS</asp:ListItem>
                                                    <asp:ListItem Value ="18">No. Cuenta</asp:ListItem>
                                                    <asp:ListItem Value ="19">INFONAVIT</asp:ListItem>
                                                    <asp:ListItem Value ="20">FONACOT</asp:ListItem>
                                                    <asp:ListItem Value ="21">Pensi&#243;n Alimenticia</asp:ListItem>
                                                    <asp:ListItem Value ="22">Clabe</asp:ListItem>
                                                    <asp:ListItem Value ="23">Salario Diario</asp:ListItem>
                                                    <asp:ListItem Value ="24">SDI</asp:ListItem>
                                                    <asp:ListItem Value ="25">Fecha Alta</asp:ListItem>
                                                    <asp:ListItem Value ="26">Fecha Egreso</asp:ListItem>
                                                    <asp:ListItem Value="27">Escolaridad</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>

                        </asp:MultiView>
                        <br />
                        <asp:Label ID="lbl_cantidad" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lb_alerta" runat="server" Text="" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                        <br />
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn_guardarCancelar" ClientIDMode="Static" OnClick="btn_buscar_Click"/>
                        <asp:Button ID="btn_descargar" runat="server" Text="Descargar" CssClass="btn_guardarCancelar" Visible="false"  OnClick="ExportToExcel"/>
                    </asp:TableCell>
                    <asp:TableCell>
                        <br />
                        <asp:GridView ID="gridview_reporte" runat="server"  CssClass="grid_buscar" AllowPaging="True" PageSize="10" HorizontalAlign="Center" OnPageIndexChanging="gridview_reporte_PageIndexChanging" OnRowDataBound="gridview_reporte_RowDataBound" OnDataBound="gridview_reporte_DataBound" Visible="false">
                                <HeaderStyle CssClass="grid_buscar_header" />
                                <RowStyle CssClass="grid_buscar_row" />
                                <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                                <PagerStyle CssClass="grid_buscar_pager" />
                            <EmptyDataTemplate>
                                No se encontraron datos.
                            </EmptyDataTemplate>
                        </asp:GridView>   
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />
    </div>
</asp:Content>
