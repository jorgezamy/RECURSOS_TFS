<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="actualizar_fortia.aspx.cs" Inherits="recursos.Views.actualizar_fortia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div id="contenedor">
            
        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Actualizar Fortia"></asp:Label>

        <%--OnPageIndexChanging="grid_buscar_PageIndexChanging" OnRowCommand="grid_buscar_RowCommand" DataKeyNames="Número" OnDataBound="grid_buscar_DataBound" --%>
            
        <asp:UpdatePanel ID="update_fortia" runat="server">
            <ContentTemplate>

                <asp:GridView ID="gridview_tipoReporte" runat="server"  CssClass="grid_buscar" OnRowCommand="grid_buscar_RowCommand">
                    <HeaderStyle CssClass="grid_buscar_header" />
                    <RowStyle CssClass="grid_buscar_row" />
                    <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                    <PagerStyle CssClass="grid_buscar_pager" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btn_select" ClientIDMode="Static" runat="server" Text="Seleccionar" CssClass="btn_select" CommandName="select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron datos.
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:Button ID="BtnActivar" runat="server" Text="Button" Style="display:none"  />

                <ajaxToolkit:ModalPopupExtender TargetControlID="BtnActivar" ID="ModalDatos" runat="server" PopupControlID="popUp_fortia" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
                    <Animations>
                        <OnShown>
                            <FadeIn duration="0.50" Fps="100" />
                        </OnShown>
                    </Animations>
                </ajaxToolkit:ModalPopupExtender>

                <div id="popUp_fortia" style="display:none">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br />
                            <asp:Label ID="label_alerta" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                            <br /><br />

                        <asp:GridView ID="grid_alertas_display" CssClass="grid_popup_display_fortia"  runat="server" AllowPaging="True" PageSize="10" HorizontalAlign="Center" >
                                <HeaderStyle CssClass="grid_buscar_header" />
                                <RowStyle CssClass="grid_buscar_row" />
                                <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                                <PagerStyle CssClass="grid_buscar_pager" />
                    
                                <EmptyDataTemplate>
                                    No se encontraron datos.
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <br /><br />
                            <asp:Button ID="btnClose" runat="server" Text="Cerrar" CssClass="btn_guardarCancelar" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <div class="divs_finiquitos">
                    <asp:Table ID="table_finiquito_datos" ClientIDMode="Static" runat="server">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2"><asp:Label runat="server" Text="Precesar Finiquito"></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Empleado:"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="drop_empleados" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_empleados_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="No. Empleado | Nombre:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lbl_finiquito_nombre" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Departamento | Puesto:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lbl_finiquito_puesto" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                <asp:Label ID="mensaje_error" runat="server" Text="" CssClass="mensaje_error"></asp:Label>
                                <asp:Label ID="mensaje_permitirFiniquitos" runat="server" Text="" CssClass="mensaje_error"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2"><asp:Button ID="btn_finiquitoSinProcesar" runat="server" Text="Agregar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_finiquitoSinProcesar_Click" /></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

                <div class="divs_finiquitos">
                
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:GridView ID="grid_finiquitos_sinProcesar" ClientIDMode="Static" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="true">
                                    <EmptyDataTemplate>
                                        No se encontraron registros.
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:CheckBox ID="check_finiquitos" runat="server" Text="Acepto que se procesarán los siguientes finiquitos." Checked="false" AutoPostBack="true" Visible="false" OnCheckedChanged="check_finiquitos_CheckedChanged" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Button ID="btn_finiquitos" runat="server" Text="Procesar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_finiquitos_Click" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>