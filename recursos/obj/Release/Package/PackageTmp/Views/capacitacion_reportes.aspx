<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion_reportes.aspx.cs" Inherits="recursos.Views.capacitacion_reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function fechasPlanAnual(chkFechasPlanAnual) {
            var tb_fecha_inicio_planAnual = document.getElementById("tb_fecha_inicio_planAnual");
            var tb_fecha_fin_planAnual = document.getElementById("tb_fecha_fin_planAnual");
            tb_fecha_inicio_planAnual.disabled = chkFechasPlanAnual.checked ? true : false;
            tb_fecha_fin_planAnual.disabled = chkFechasPlanAnual.checked ? true : false;
            if (!tb_fecha_inicio_planAnual.disabled) {
                tb_fecha_inicio_planAnual.focus();
            }
            else {
                tb_fecha_inicio_planAnual.value = "";
                tb_fecha_fin_planAnual.value = "";
            }
        }

        function fechasCurso(chkFechasCurso) {
            var tb_fecha_inicio_curso = document.getElementById("tb_fecha_inicio_curso");
            var tb_fecha_fin_curso = document.getElementById("tb_fecha_fin_curso");
            tb_fecha_inicio_curso.disabled = chkFechasCurso.checked ? true : false;
            tb_fecha_fin_curso.disabled = chkFechasCurso.checked ? true : false;
            if (!tb_fecha_inicio_curso.disabled) {
                tb_fecha_inicio_curso.focus();
            }
            else {
                tb_fecha_inicio_curso.value = "";
                tb_fecha_fin_curso.value = "";
            }
        }

        function fechasEmpleado(chkFechasEmpleado) {
            var tb_fecha_inicio_empleado = document.getElementById("tb_fecha_inicio_empleado");
            var tb_fecha_fin_empleado = document.getElementById("tb_fecha_fin_empleado");
            tb_fecha_inicio_empleado.disabled = chkFechasEmpleado.checked ? true : false;
            tb_fecha_fin_empleado.disabled = chkFechasEmpleado.checked ? true : false;
            tb_fecha_inicio_empleado.value = "";
            tb_fecha_fin_empleado.value = "";

            if (!tb_fecha_inicio_empleado.disabled) {
                tb_fecha_inicio_empleado.focus();
            }
            else {
                //tb_fecha_inicio_empleado.value = "";
                //tb_fecha_fin_empleado.value = "";
                tb_fecha_inicio_empleado.value = new Date().getFullYear();
                tb_fecha_fin_empleado.value = new Date().getFullYear();
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="Titulo" ClientIDMode="Static" runat="server" Text="Reportes capacitaciones" Font-Bold="true" Font-Size="Large"></asp:Label>
        <br />
        <br />

        <asp:Button ID="tab1" Text="Plan anual" runat="server" OnClick="tab1_Click" CssClass="initial" />
        <asp:Button ID="tab2" Text="Curso" runat="server" OnClick="tab2_Click" CssClass="initial" />
        <asp:Button ID="tab3" Text="Empleado" runat="server" OnClick="tab3_Click" CssClass="initial" />

        <br />
        <div runat="server" visible="false" id="div_reporte" style="border: outset; width: 100%; margin: 0 auto">
            <asp:Table ID="tabla_contenido" runat="server" Style="margin: 0 auto" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:MultiView ID="multi_view" runat="server">
                            <asp:View ID="planAnual" runat="server">
                                <br />
                                <asp:Label runat="server" Text="Reporte plan anual" Font-Bold="true"></asp:Label>
                                <br />
                                <br />
                                <asp:Table ID="tableReportes" runat="server" ClientIDMode="Static">
                                    <asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_inicio_planAnual" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_fin_planAnual" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkFechasPlanAnual" runat="server" Text="Todas las fechas" onclick="fechasPlanAnual(this)" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="curso" runat="server">
                                <br />
                                <asp:Label runat="server" Text="Reporte por curso" Font-Bold="true"></asp:Label>
                                <br />
                                <br />
                                <asp:Table ID="tableCurso" runat="server" ClientIDMode="Static">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Curso:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:SqlDataSource ID="data_cursos" runat="server" SelectCommand="select id_capacitacion_cursos, descripcion from TNCH_RH.dbo.capacitacion_cursos where activo = 1" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                            <asp:DropDownList ID="ddlCurso" runat="server" AppendDataBoundItems="true" DataSourceID="data_cursos" DataValueField="id_capacitacion_cursos" DataTextField="descripcion">
                                                <asp:ListItem Text="-- Todos --" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Asistencia:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList ID="ddlAsistencia" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-- Ambos --" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Asistieron" Value="SI"></asp:ListItem>
                                                <asp:ListItem Text="No asistieron" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_inicio_curso" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:TextBox ID="tb_fecha_fin_curso" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkFechasCurso" runat="server" Text="Todas las fechas" onclick="fechasCurso(this)" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:View>
                            <asp:View ID="empleado" runat="server">
                                <br />
                                <asp:Label runat="server" Text="Reporte por empleado" Font-Bold="true"></asp:Label>
                                <br />
                                <br />
                                <asp:Table ID="tableEmpleados" runat="server" ClientIDMode="Static">
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Empleado:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:SqlDataSource ID="data_empleados" runat="server" SelectCommand="select no_empleado from tnch_rh.dbo.empleados where fecha_baja is null order by no_empleado asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                            <asp:DropDownList ID="ddlEmpleados" ClientIDMode="Static" runat="server" DataSourceID="data_empleados" DataTextField="no_empleado" DataValueField="no_empleado" AppendDataBoundItems="true">
                                                <asp:ListItem Text="-- Todos --" Value=""></asp:ListItem>
                                            </asp:DropDownList>

                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha inicio:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <%--<asp:TextBox ID="tb_fecha_inicio_empleado" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>--%>
                                            <link href="https://netdna.bootstrapcdn.com/bootstrap/2.3.2/css/bootstrap.min.css" rel="stylesheet">
                                            <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.2.0/css/datepicker.min.css" rel="stylesheet">



                                            <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
                                            <script src="https://netdna.bootstrapcdn.com/bootstrap/2.3.2/js/bootstrap.min.js"></script>
                                            <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.2.0/js/bootstrap-datepicker.min.js"></script>
                                            <input type="text" class="form-control" name="datepickerIni" id="tb_fecha_inicio_empleado" />

                                            <script>
                                                $(document).ready(function () {
                                                    $("#tb_fecha_inicio_empleado").datepicker({
                                                        format: "yyyy",
                                                        viewMode: "years",
                                                        minViewMode: "years",
                                                        autoclose: true
                                                    });
                                                })
                                                </script>


                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:Label runat="server" Text="Fecha fin:"></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <%--<asp:TextBox ID="tb_fecha_fin_empleado" runat="server" TextMode="Date" ClientIDMode="Static"></asp:TextBox>--%>
                                            <%--<input type="text" class="form-control" name="datepicker" id="datepickerFin" />--%>
                                            <input type="text" class="form-control" name="datepickerFin" id="tb_fecha_fin_empleado" />

                                            <script>
                                                $(document).ready(function () {
                                                    $("#tb_fecha_fin_empleado").datepicker({
                                                        format: "yyyy",
                                                        viewMode: "years",
                                                        minViewMode: "years",
                                                        autoclose: true
                                                    });
                                                })
                                                </script>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <%--<asp:TableRow Height="50px">
                                        <asp:TableCell>
                                            <asp:CheckBox ID="chkFechasEmpleado" runat="server" onclick="fechasEmpleado(this)" />
                                        </asp:TableCell>
                                    </asp:TableRow>--%>
                                </asp:Table>
                            </asp:View>
                        </asp:MultiView>
                        <br />
                        <asp:Label ID="lbl_cantidad" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lb_alerta" runat="server" Text="" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                        <br />
                        <asp:Button ID="btn_buscar" runat="server" Text="Buscar" CssClass="btn_guardarCancelar" ClientIDMode="Static" OnClick="btn_buscar_Click" />
                        <asp:Button ID="btn_descargar" runat="server" Text="Descargar" CssClass="btn_guardarCancelar" Visible="false" OnClick="ExportToExcel" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <br />
                        <asp:GridView ID="gridview_reporte" runat="server" CssClass="grid_buscar" AllowPaging="True" PageSize="10" HorizontalAlign="Center" OnRowDataBound="gridview_reporte_RowDataBound" OnPageIndexChanging="gridview_reporte_PageIndexChanging">
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

        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

        <asp:HiddenField ID="a" runat="server" ClientIDMode="Static" />

    </div>

</asp:Content>
