<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion_cursos_planAnual.aspx.cs" Inherits="recursos.Views.capacitacion_cursos_planAnual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Table runat="server" CssClass="table_cursoNuevo">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Año"></asp:Label></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="drop_planAnual_anio" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_planAnual_anio_SelectedIndexChanged">
                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        
        <div class="div_planAnual">

            <asp:Table runat="server" CssClass="table_cursoNuevo">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <h3>Crear Nuevo Curso</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label runat="server" Text="Nombre:"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txt_cursoNuevo_descripcion" runat="server" Text=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="required_cursoNuevo_descripcion" runat="server" ControlToValidate="txt_cursoNuevo_descripcion" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoNuevo"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label runat="server" Text="Modalidad:"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="drop_cursoNuevo_modalidad" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="required_cursoNuevo_modalidad" runat="server" ControlToValidate="drop_cursoNuevo_modalidad" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoNuevo"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label runat="server" Text="Objetivo Interno:"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txt_cursoNuevo_objetivoInterno" runat="server" Text="" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="required_cursoNuevo_objetivoInterno" runat="server" ControlToValidate="txt_cursoNuevo_objetivoInterno" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoNuevo"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label runat="server" Text="Objetivo:"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="drop_cursoNuevo_objetivo" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="required_cursoNuevo_objetivo" runat="server" ControlToValidate="drop_cursoNuevo_objetivo" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoNuevo"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label runat="server" Text="Duración (hrs.):"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txt_cursoNuevo_duracion" runat="server" TextMode="Number" min="0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="required_cursoNuevo_duracion" runat="server" ControlToValidate="txt_cursoNuevo_duracion" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoNuevo"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell><asp:Label runat="server" Text="Área Temática:"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="drop_cursoNuevo_areaTematica" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="required_cursoNuevo_areaTematica" runat="server" ControlToValidate="drop_cursoNuevo_areaTematica" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoNuevo"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="lbl_cursoNuevo_mensaje" runat="server" Text="" CssClass="mensaje_error"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Button ID="btn_cursoNuevo_guardar" runat="server" Text="Guardar" CssClass="btn_guardarCancelar" Visible="false" ValidationGroup="cursoNuevo" OnClick="btn_cursoNuevo_guardar_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

        </div>

        <div class="div_planAnual">
            
            <div class="headerDivider">

                <asp:Table runat="server" CssClass="table_cursoNuevo">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="1">
                            <h3>Cursos</h3>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:CheckBoxList ID="checklist_cursosActivos" runat="server"></asp:CheckBoxList>
                <br/>
                <asp:Button ID="btn_cursosActivos" runat="server" Text="Guardar" CssClass="btn_guardarCancelar" Visible="false" OnClick="checklist_cursosActivos_Click" />

            </div>
        
        </div>

        <div class="div_planAnual">

            <asp:Table runat="server" CssClass="table_cursoNuevo">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <h3>Asignar Horario</h3>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <asp:DropDownList ID="drop_cursoFechas_curso" runat="server" AppendDataBoundItems="true" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="drop_cursoFechas_curso_SelectedIndexChanged">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="required_cursoFechas_curso" runat="server" ControlToValidate="drop_cursoFechas_curso" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoFechas"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Inicio"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="drop_cursoFechas_inicio" runat="server" AppendDataBoundItems="true" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="drop_cursoFechas_inicio_SelectedIndexChanged">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="required_cursoFechas_inicio" runat="server" ControlToValidate="drop_cursoFechas_inicio" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoFechas"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label runat="server" Text="Fin"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="drop_cursoFechas_fin" runat="server" AppendDataBoundItems="true" Enabled="false">
                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="required_cursoFechas_fin" runat="server" ControlToValidate="drop_cursoFechas_fin" ErrorMessage="*Vacío" CssClass="RequiredValidator" Display="Dynamic" ValidationGroup="cursoFechas"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <asp:Button ID="btn_cursoFechas_agregar" runat="server" Text="+" CssClass="btn_guardarCancelar" OnClick="btn_cursoFechas_agregar_Click" ValidationGroup="cursoFechas" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4">
                        <asp:Label ID="lbl_cursoFechas_mensaje" runat="server" Text="" CssClass="mensaje_error" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

        </div>
        
        <asp:GridView ID="grid_planAnual_horarios" runat="server" OnRowDeleting="grid_planAnual_horarios_RowDeleting" AllowPaging="true" CssClass="grid_cursosActivos" OnPageIndexChanging="grid_planAnual_horarios_PageIndexChanging" OnRowDataBound="grid_planAnual_horarios_RowDataBound">
            <HeaderStyle CssClass="grid_cursosActivos_header" />
            <RowStyle CssClass="grid_cursosActivos_row" />
            <AlternatingRowStyle CssClass="grid_cursosActivos_altrow" />
            <PagerStyle CssClass="grid_cursosActivos_pager" />
            <Columns>
                <asp:CommandField SelectText="Asignar" ButtonType="Button" ShowDeleteButton="True" ControlStyle-CssClass="btn_gridview_eliminar"></asp:CommandField>
            </Columns>
        </asp:GridView>
        
        <asp:Label ID="mensaje_error" runat="server" Text=""></asp:Label>

        <asp:Button ID="btn_planAnual_cierre" runat="server" Text="" CssClass="btn_planAnual_cierre_cerrado" Visible="false" OnClick="btn_planAnual_cierre_Click" />

        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

    </div>

</asp:Content>
