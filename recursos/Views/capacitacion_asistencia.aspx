<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion_asistencia.aspx.cs" Inherits="recursos.Views.capacitacion_asistencia" EnableEventValidation = "false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/css/capacitacion_asistencia.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

    <div id="contenedor">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />
                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Asistencia Cursos"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="Curso:" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="dropdown_curso" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="dropdown_curso_SelectedIndexChanged"></asp:DropDownList>
                <br /><br />
                <asp:Table ID="tabla_informacion" runat="server" HorizontalAlign="Center" style="width: 600px;overflow: auto; text-align: left">
                    <asp:TableRow>
                        <asp:TableCell >
                            <asp:Label ID="Label2" runat="server" Text="Tematica:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lbl_tematica" runat="server" Text="N/A"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell >
                            <asp:Label ID="Label5" runat="server" Text="Modalidad:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lbl_modalidad" runat="server" Text="N/A"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell >
                            <asp:Label ID="Label3" runat="server" Text="Duración:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lbl_duracion" runat="server" Text="N/A"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell >
                            <asp:Label ID="Label6" runat="server" Text="Objetivos:" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lbl_objetivos" runat="server" Text="N/A"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <br />
                <div style="width: 100%; height: 400px; overflow: auto; text-align: left">
                    <asp:CheckBoxList ID="chbox_empleados" runat="server" Width="100%" Style="margin: auto" AppendDataBoundItems="true" RepeatColumns="4"  AutoPostBack="false">
                    </asp:CheckBoxList>
                </div>
                <br /><br />
                <asp:Button ID="btnRegistrar_Asistencia" runat="server" Text="Registrar Asistencia" CssClass="btn_guardarCancelar" Visible ="false" OnClick="btnRegistrar_Asistencia_Click"/>
                <br />
                <asp:Label ID="lbl_registro" Visible="false" runat="server" Text="Asistencia Registrada" Font-Bold="true" ForeColor="Green" Font-Size="X-Large"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:Button ID="btn_descargar" runat="server" Text="Descargar Formato" CssClass="btn_guardarCancelar" Visible ="false" OnClick="btn_descargar_Click"/>                


    </div>

    <!-- ModalPopupExtender -->

    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="modal_popup" runat="server" PopupControlID="panel_asistencia" TargetControlID="btnShow"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="panel_asistencia" runat="server" CssClass="capacitacion_asistencia_pop_up" align="center" Style="display: none">    
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label4" runat="server" Text="Registrar Asistencia" Font-Bold="true" Font-Size="Large"></asp:Label>
                <br /><br />
                <asp:GridView ID="datagridview_asistencia" CssClass="grid_buscar datagridview_font" OnRowDataBound="datagridview_asistencia_RowDataBound" runat="server" AllowPaging="True" PageSize="10" HorizontalAlign="Center" OnPageIndexChanging="datagridview_asistencia_PageIndexChanging" >
                    <HeaderStyle CssClass="grid_buscar_header" />
                    <RowStyle CssClass="grid_buscar_row" />
                    <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                    <PagerStyle CssClass="grid_buscar_pager" />
                    
                    <EmptyDataTemplate>
                        No se selecciono ningun empleado.
                    </EmptyDataTemplate>
                </asp:GridView>
                <br />

                <asp:Button ID="btnRegistrar" runat="server" Text="Guardar" CssClass="btn_guardarCancelar" OnClick="btnRegistrar_Click"/>
                <asp:Button ID="btnClose" runat="server" Text="Regresar" CssClass="btn_guardarCancelar_disabled" OnClick="btnClose_Click" />


            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnRegistrar" />
            </Triggers>
        </asp:UpdatePanel>
    
    </asp:Panel>              


</asp:Content>
