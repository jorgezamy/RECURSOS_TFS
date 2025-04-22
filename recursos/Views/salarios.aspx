<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="salarios.aspx.cs" Inherits="recursos.Views.salarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script>
        function Func_nuevosSalariosBuscar() {
            var input = $('#<%= tb_NuevosSalarios.ClientID %>');
            var len = input.val().length;
            input[0].focus();
            input[0].setSelectionRange(len, len);
        }
    </script>

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <div id="blanket" style="display:none">
        </div>

        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>

                <asp:Button ID="b1" Text="Salarios IMSS" runat="server" CssClass="initial" OnClick="b1_Click" />
                <asp:Button ID="b2" Text="Salarios" runat="server" CssClass="initial" OnClick="b2_Click" /> 

                <br />

                <asp:MultiView ID="multi_view" runat="server">
                    <asp:View ID="view_salarios_imss" runat="server">

                        <div class="div_nuevosSalarios" style="width:30%">

                            <asp:Table runat="server" CssClass="table_nuevosSalarios">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label runat="server" Text="Buscar:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_NuevosSalarios" ClientIDMode="Static" runat="server" onkeypress="return TextBox_PresionarBtnEnter(event);" placeholder="Palabra clave"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:ImageButton ID="bt_btnbuscar" ClientIDMode="Static" runat="server" ImageUrl="~/images/buscar.png" OnMouseOver="src='/images/buscar_white.png';" OnMouseOut="src='/images/buscar.png';" OnClick="bt_btnbuscar_Click" /></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <div id="aprobado_mensajes_confirmacion" runat="server" visible="false">
                                <asp:Label runat="server" ForeColor="Green" Font-Bold="true" Text="Los cambios han sido aplicados exitosamente."></asp:Label>
                            </div>

                        </div>

                        <div class="div_nuevosSalarios">
                            <asp:GridView ID="grid_nuevosSalarios" runat="server" CssClass="grid_salariosImss" PageSize="8" AllowPaging="true" AutoGenerateColumns="true" OnRowCommand="grid_nuevosSalarios_RowCommand" OnPageIndexChanging="grid_nuevosSalarios_PageIndexChanging">
                                <HeaderStyle CssClass="grid_salariosImss_header" />
                                <RowStyle CssClass="grid_salariosImss_row" />
                                <AlternatingRowStyle CssClass="grid_salariosImss_altrow" />
                                <PagerStyle CssClass="grid_salariosImss_pager" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btn_aprobado" runat="server" Text="Aprobado" CssClass="btn_guardarCancelar" CommandName="aprobado" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' OnClientClick="popup('popUp_nuevosSalarios_aprobado')" CausesValidation="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron datos.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>

                    </asp:View>
                    <asp:View ID="view_salarios" runat="server">

                        <section>

                            <br />

                            <asp:Label runat="server" Text="Empleado:"></asp:Label>
            
                            <asp:DropDownList ID="drop_empleados_salarios" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_empleados_salarios_SelectedIndexChanged">
                                <asp:ListItem Text="-- Selecccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>

                            <asp:Table ID="table_salarios" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label runat="server" Text="No. Empleado:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_noEmpleado" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label runat="server" Text="Nombre:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_nombre" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label runat="server" Text="Forma de pago:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="drop_formaPago" runat="server" AppendDataBoundItems="true" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="drop_formaPago_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_formaPago" runat="server" ControlToValidate="drop_formaPago" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="*Vacio" ValidationGroup="salarios"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="row_banco" Visible="false">
                                    <asp:TableCell><asp:Label runat="server" Text="Banco:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="drop_banco" runat="server" AppendDataBoundItems="true" Enabled="false">
                                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="required_banco" runat="server" ControlToValidate="drop_banco" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="*Vacio" ValidationGroup="salarios"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="row_noCuenta" Visible="false">
                                    <asp:TableCell><asp:Label runat="server" Text="No. Cuenta:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_noCuenta" runat="server" Text="" placeholder="No. Cuenta" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="required_noCuenta" runat="server" ControlToValidate="tb_noCuenta" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="*Vacio" ValidationGroup="salarios"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="row_salario" Visible="false">
                                    <asp:TableCell><asp:Label runat="server" Text="Salario:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_salario" runat="server" Text="" placeholder="Salario" Enabled="false"></asp:TextBox>
                                        <asp:HiddenField ID="hidden_salario" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <asp:Label ID="mensaje_error" ClientIDMode="Static" runat="server" Text="" CssClass="mensaje_error"></asp:Label>

                            <asp:Button ID="btn_update" runat="server" Text="Actualizar" CssClass="btn_guardarCancelar" Visible="false" ValidationGroup="salarios" OnClick="btn_update_Click" />

                        </section>

                     </asp:View> 
                </asp:MultiView>

            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="popUp_nuevosSalarios_aprobado" style="display:none">

            <a href="#" id="cerrar_agregar" class="cerrar_PopUp" onclick="popup('popUp_nuevosSalarios_aprobado')"></a>

            <asp:UpdatePanel ID="UpdatePanel_aprobado" runat="server">
                <ContentTemplate>

                    <asp:Table CssClass="table_salariosNuevos_aprobado" runat="server">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="No. Empleado"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_noEmpleado" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Nombre:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_nombre" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Departamento:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_departamento" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_puesto" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Salario antes:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_antes" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Salario después:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_despues" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Tipo:"></asp:Label></asp:TableCell>
                            <asp:TableCell><asp:Label ID="lb_aprobado_tipo" runat="server" Text=""></asp:Label></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <div id="aprobado_mensajes_alerta" runat="server">
                        <asp:Label runat="server" Text="El salario se actualizara en FORTIA." CssClass="mensaje_error"></asp:Label>
                        <asp:Label runat="server" Text="Al dar click en ACEPTAR debes actualizar su salario tambien en el IMSS." CssClass="mensaje_error"></asp:Label>
                    </div>

                    <asp:Button ID="btn_nuevosSalarios_aprobar" runat="server" Text="Aceptar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_nuevosSalarios_aprobar_Click" />

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        
        <asp:ImageButton ID="b0" runat="server" ClientIDMode="Static" ImageUrl="~/images/menuPrincipal/salarios/menuBtn_recursos_salarios_1.png" CssClass="menuBtn menubtn_salarios" Enabled="true" OnClick="b0_Click" />

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>
