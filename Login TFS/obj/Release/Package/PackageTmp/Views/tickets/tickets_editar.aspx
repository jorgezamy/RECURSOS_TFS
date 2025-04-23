<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="tickets_editar.aspx.cs" Inherits="Login_TFS.Views.tickets.tickets_editar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:UpdatePanel ID="UpdatePanel_editarTickets" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbl_titulo" CssClass="crear_ticket_titulo" runat="server" Text="Información Ticket"></asp:Label>

                <asp:Table ID="table_tickets_requerimientos" ClientIDMode="Static" runat="server" HorizontalAlign="Center" >
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="true">
                            <asp:Label ID="Label2" runat="server" Text="Folio:" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="no_folio" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="true">
                            <asp:Label ID="Label3" runat="server" Text="Departamento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="info_departamento" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="true">
                            <asp:Label ID="Label4" runat="server" Text="Asunto:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="info_asunto" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Bold="true">
                            <asp:Label ID="Label5" runat="server" Text="Requerimiento:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="info_requerimiento" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />

                <asp:GridView ID="grid_editarTickets" runat="server"  AllowPaging="True" PageSize="4" OnPageIndexChanging="grid_editarTickets_PageIndexChanging"   AutoGenerateColumns="false" CssClass="gridtickets_editar"  ShowFooter="true" OnRowDataBound="grid_editarTickets_RowDataBound" >
                    <HeaderStyle CssClass="gridtickets_editar_header" />
                    <RowStyle CssClass="gridtickets_editar_row" />
                    <AlternatingRowStyle CssClass="grid_tickets_altrow" />
                    <FooterStyle CssClass="gridtickets_editar_footer" />
                    <PagerStyle CssClass="gridtickets_editar_pager" />
                    <Columns>
                        <asp:TemplateField HeaderText="Estatus">
                            <ItemTemplate>
                                <asp:Label id="lbl_estatus" runat="server" Text='<%# Eval("estatus") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:SqlDataSource ID="data_estatus" runat="server" SelectCommand="select id_tickets_estatus, descripcion from tnch.dbo.tickets_estatus where activo='1' and id_tickets_estatus != 1" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                <asp:DropDownList ID="footer_ddlEstatus" AutoPostBack="true" runat="server" AppendDataBoundItems="true" DataSourceID="data_estatus" DataValueField="id_tickets_estatus" DataTextField="descripcion" OnSelectedIndexChanged="footer_ddlEstatus_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_add_estatus" runat="server" CssClass="campoVacio" ErrorMessage="*Campo Vacio" Display="Dynamic" ControlToValidate="footer_ddlEstatus" ValidationGroup="add_ticket"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirigido a:">
                            <ItemTemplate>
                                <asp:Label id="lbl_depto" runat="server" Text='<%# Eval("departamento") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:SqlDataSource ID="data_deptos" runat="server" SelectCommand="select id_depto, desc_esp from tnch_rh.dbo.departamento where estatus='1' order by desc_esp asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                <asp:DropDownList ID="footer_ddlDepto" Enabled="false" runat="server" AppendDataBoundItems="true" DataSourceID="data_deptos" DataValueField="id_depto" DataTextField="desc_esp">
                                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_add_depto" runat="server" CssClass="campoVacio" ErrorMessage="*Campo Vacio" Display="Dynamic" ControlToValidate="footer_ddlDepto" ValidationGroup="add_ticket"></asp:RequiredFieldValidator>
<%--                                <asp:Label ID="lbl_departamento_origen" runat="server" ></asp:Label>--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolución:">
                            <ItemTemplate>
                                <asp:Label id="lbl_resolucion" runat="server" Text='<%# Eval("resolucion") %>'></asp:Label>
                            </ItemTemplate>
                        <FooterTemplate>
                                <asp:TextBox ID="txt_resolucion" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox></br>
                                <asp:RequiredFieldValidator ID="RFV_txt_resolucion" runat="server" ControlToValidate="txt_resolucion" ErrorMessage="*Campo Vacio" CssClass="campoVacio" ValidationGroup="add_ticket"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Revisado por:">
                            <ItemTemplate>
                                <asp:Label ID="lbl_revisado" runat="server" Text='<%# Eval("revisado") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creado por:">
                            <ItemTemplate>
                                <asp:Label id="lbl_usuario" runat="server" Text='<%# Eval("usuario") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha">
                            <ItemTemplate>
                                <asp:Label ID="lbl_fecha" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btn_actualizar" runat="server" CssClass="btn_guardarCancelar_editar" Text="Actualizar" ValidationGroup="add_ticket" OnClick="btn_actualizar_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EmptyDataTemplate>
                        No se encontrarón datos.
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:Label ID="txt_mensaje" runat="server" Text="" CssClass="mensaje_error"></asp:Label>


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
                        <asp:Button ID="btn_cerrarPopUp_guardar_si" Visible="true" runat="server" Text="Guardar" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_cerrarPopUp_guardar_si_Click"/>
                        <asp:Button ID="btn_cerrarPopUp_guardar_no" runat="server" Text="Regresar" CssClass="btn_cerrarPopUp_cancelarGuardar"/>
                
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                       <asp:Button ID="btn_registro_folio" runat="server" Text="Button" Style="display:none"  />

                                        <ajaxToolkit:ModalPopupExtender TargetControlID="btn_registro_folio"  ID="modal_exito" runat="server" PopupControlID="popUp_guardar_ticket_exito" BackgroundCssClass="modalBackground" >
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
                            <asp:Label ID="Label1" CssClass="crear_ticket_titulo" ClientIDMode="Static" runat="server" Text="Ticket actualizado exitosamente"></asp:Label>
                            <br /><br />
                            <asp:Image ID="Image1" Width="100" Height="100" runat="server" ImageUrl="~/images/exito.gif" />
                            <br /><br />
                            <br /><br />
                            <asp:Button ID="btn_aceptar" Visible="true" Width="200" runat="server" Text="Aceptar" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_aceptar_Click"  />              
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>


            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" OnClick="btn_regresar_Click" CssClass="cerrar_sesion" />

    </div>

</asp:Content>
