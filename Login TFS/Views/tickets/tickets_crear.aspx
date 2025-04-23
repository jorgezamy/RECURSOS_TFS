<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="tickets_crear.aspx.cs" Inherits="Login_TFS.Views.tickets.tickets_crear" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="contenedor">
                <asp:Label ID="lbl_titulo" CssClass="crear_ticket_titulo" runat="server" Text="Crear Ticket"></asp:Label>
                <br /><br />
                <asp:Table ID="tabla_guardar" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lbl_departamento" runat="server" Text="Departamento"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:DropDownList ID="dropdown_departamento" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="requiredfield_departamento" ControlToValidate="dropdown_departamento" Font-Bold="true" CssClass="required_field_crear_ticket" InitialValue="" runat="server" ErrorMessage="*" ValidationGroup="registrar"></asp:RequiredFieldValidator>
                        </asp:TableCell>             
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lbl_asunto" runat="server" Text="Asunto"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_asunto" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredfield_asunto" ControlToValidate="tb_asunto" Font-Bold="true" CssClass="required_field_crear_ticket"  runat="server" ErrorMessage="*" ValidationGroup="registrar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lbl_requerimentos" runat="server" Text="Requerimentos"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_requerimentos" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredfield_requerimentos" ControlToValidate="tb_requerimentos" Font-Bold="true" CssClass="required_field_crear_ticket" runat="server" ErrorMessage="*" ValidationGroup="registrar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <asp:Button ID="btn_generar" runat="server" CssClass="btn_guardarCancelar" Text="Generar Ticket" OnClick="btn_generar_Click" ValidationGroup="registrar" />
                <br /> <br />

                <%--<asp:GridView ID="grid_tickets" CssClass="grid_tickets" runat="server"></asp:GridView>--%>
                    <asp:Label ID="lbl_titulo_gridview" ClientIDMode="Static" runat="server" CssClass="crear_ticket_titulo" Text="Mis Tickets creados"></asp:Label>
   
                
                    <asp:GridView ID="grid_tickets" runat="server" CssClass="grid_tickets" HorizontalAlign="Center" PageSize="5" AllowPaging="true" OnPageIndexChanging="grid_tickets_PageIndexChanging">
                        <HeaderStyle CssClass="grid_tickets_header" />
                        <RowStyle CssClass="grid_tickets_row" />
                        <AlternatingRowStyle CssClass="grid_tickets_altrow" />
                        <PagerStyle CssClass="grid_tickets_pager" />
                        <FooterStyle CssClass="grid_tickets_footer" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btn_ver_ticket" runat="server" Text="Ver" CssClass="btn_guardarCancelar" OnClick="btn_ver_ticket_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Se encontraron datos.
                        </EmptyDataTemplate>
                    </asp:GridView>
                
                   <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />


            </div>


            <asp:Button ID="BtnActivar" runat="server" Text="Button" Style="display:none"  />

                                        <ajaxToolkit:ModalPopupExtender TargetControlID="BtnActivar" ID="popUp_editar" runat="server" PopupControlID="popUp_guardar_ticket" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrarPopUp_guardar_no">
                                            <Animations>
                                                    <OnShown>
                                                        <FadeIn duration="1.30" Fps="100" />
                                                    </OnShown>
                                            </Animations>
                                        </ajaxToolkit:ModalPopupExtender>

        
                                <div id="popUp_guardar_ticket" style="display:none">

                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <asp:Label ID="lb_cerrarPopUp_guardar" CssClass="crear_ticket_titulo" ClientIDMode="Static" runat="server" Text="¿Está seguro que desea GUARDAR los cambios?"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Button ID="btn_cerrarPopUp_guardar_si" Visible="true" runat="server" Text="Guardar" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_cerrarPopUp_guardar_si_Click" />
                                        <asp:Button ID="btn_cerrarPopUp_guardar_no" runat="server" Text="Regresar" CssClass="btn_cerrarPopUp_cancelarGuardar"/>
                
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>

                       <asp:Button ID="btn_registro_folio" runat="server" Text="Button" Style="display:none"  />

                                        <ajaxToolkit:ModalPopupExtender TargetControlID="btn_registro_folio" ID="modal_exito" runat="server" PopupControlID="popUp_guardar_ticket_exito" BackgroundCssClass="modalBackground" >
                                            <Animations>
                                                    <OnShown>
                                                        <FadeIn duration="1.30" Fps="100" />
                                                    </OnShown>
                                            </Animations>
                                        </ajaxToolkit:ModalPopupExtender>

        
                                <div id="popUp_guardar_ticket_exito" style="display:none">

                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <br />
                                        <asp:Label ID="Label1" CssClass="crear_ticket_titulo" ClientIDMode="Static" runat="server" Text="Ticket creado exitosamente"></asp:Label>
                                        <br /><br />
                                        <asp:Image ID="Image1" Width="100" Height="100" runat="server" ImageUrl="~/images/exito.gif" />
                                        <br /><br />

                                        <asp:Label ID="lbl_info" runat="server" Font-Bold="true" Text="Folio Generado:"></asp:Label>
                                        <asp:Label ID="no_folio" runat="server" Text=""></asp:Label>
                                        <br />
                                        <asp:Label ID="lb_departamento" runat="server" Font-Bold="true" Text="Departamento:"></asp:Label>
                                        <asp:Label ID="departamento" runat="server" Text=""></asp:Label>
                                        <br />
                                        <asp:Label ID="lb_asunto" runat="server" Font-Bold="true" Text="Asunto:"></asp:Label>
                                        <br />
                                        <asp:Label ID="asunto" runat="server" Text=""></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_requerimento" runat="server" Font-Bold="true" Text="Requerimento:"></asp:Label>
                                        <br />
                                        <asp:Label ID="requerimento" runat="server" Text=""></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_fecha" runat="server" Font-Bold="true" Text="Fecha creación:"></asp:Label>
                                        <br />
                                        <asp:Label ID="fecha" runat="server" Text=""></asp:Label>


                                        <br /><br />
                                        <asp:Button ID="btn_generar_folio" Visible="true" Width="200" runat="server" Text="Generar otro ticket" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_generar_folio_Click" />
                                        <%--<asp:Button ID="btn_revisar_folio" runat="server" Width="200" Text="Revisar Ticket" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_revisar_folio_Click"/>--%>
                
                                        </ContentTemplate>
                                    </asp:UpdatePanel>




                                </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
