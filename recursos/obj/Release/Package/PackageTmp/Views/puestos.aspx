<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="puestos.aspx.cs" Inherits="recursos.Views.puestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <div id="contenedorPuestos">

        <div id="blanket" style="display:none">
        </div>

        <asp:UpdatePanel ID="UpdatePanel_puestos" runat="server">
            <ContentTemplate>
            
                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <div id="contenedor">

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Editar puestos"></asp:Label>

                <asp:Table ID="tabla_puestos" ClientIDMode="Static" runat="server">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Compañía:" Font-Bold="true"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="data_compania" runat="server" SelectCommand="select id_compania, nombreComercial from tnch_rh.dbo.compania where estatus='1' " ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                            <asp:DropDownList ID="drop_compania" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="data_compania" DataTextField="nombreComercial" DataValueField="id_compania" OnSelectedIndexChanged="drop_compania_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Compañía seleccionada:" Font-Bold="true"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lbl_compania" runat="server"></asp:Label></asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="mensaje_error" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Table ID="table_btnBuscar" ClientIDMode="Static" runat="server" Visible="false">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="titulo_buscarNumero" ClientIDMode="Static" runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="tb_buscarNumero" ClientIDMode="Static" runat="server" onkeypress="return TextBox_PresionarBtnEnter(event);" placeholder="Ingrese puesto o depto"></asp:TextBox></asp:TableCell>
                        <asp:TableCell><asp:ImageButton ID="bt_btnbuscar" ClientIDMode="Static" runat="server" ImageUrl="~/images/buscar.png" OnMouseOver="src='/images/buscar_white.png';" OnMouseOut="src='/images/buscar.png';" OnClick="bt_btnbuscar_Click" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:GridView ID="grid_buscar" runat="server" CssClass="grid_buscar" AutoGenerateColumns="false" AllowPaging="true" Width="100%" PageSize="15" ShowFooter="false" DataKeyNames="id" OnPageIndexChanging="grid_buscar_PageIndexChanging" >
                    <HeaderStyle CssClass="grid_buscar_header" />
                    <RowStyle CssClass="grid_buscar_row" />
                    <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                    <PagerStyle CssClass="grid_buscar_pager" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btn_edit" CausesValidation="false" OnClientClick="popup('popUp_editar_puestos')" runat="server" Text="Editar" CssClass="btn_select" Enabled="true" OnClick="btn_edit_Click"  />  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Puesto">
                            <ItemTemplate>
                                <asp:Label ID="lblPuesto" runat="server" Text='<%# Eval("puesto") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate> 
                                <asp:TextBox ID="txtPuesto" runat="server" ValidationGroup="insertar" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo vacio" ValidationGroup="insertar" ControlToValidate="txtPuesto" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Position">
                            <ItemTemplate>
                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("position") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtPosition" runat="server" ValidationGroup="insertar" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Campo vacio" ValidationGroup="insertar" ControlToValidate="txtPosition" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>               
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Departamento">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartamento" runat="server" Text='<%# Eval("depto") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Bonos" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblBonos" runat="server" Text='<%# Eval("bonos") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Cantidad" ItemStyle-Wrap="true" >
                            <ItemTemplate>
                                <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("cantidad") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salario" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSalario" runat="server" Text='<%# Eval("salario") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salario Integrado" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSalarioIntegrado" runat="server" Text='<%# Eval("salario_integrado") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bono despensa" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblBonoDespensa" runat="server" Text='<%# Bind("bono_despensa") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bono puntualidad" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblBonoPuntualidad" runat="server" Text='<%# Eval("bono_puntualidad") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bono asistencia" ItemStyle-Wrap="true">
                            <ItemTemplate>
                                <asp:Label ID="lblBonoAsistencia" runat="server" Text='<%# Eval("bono_asistencia") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                                <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("tipo") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Turno">
                            <ItemTemplate>
                                <asp:Label ID="lblTurno" runat="server" Text='<%# Eval("turno") %>' BackColor="Transparent" ForeColor="Black"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" OnClick="Add" Text="Agregar puesto" ValidationGroup="insertar" CommandName = "Footer" CausesValidation="true" /> 
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EmptyDataTemplate>
                        No se encontraron datos.
                    </EmptyDataTemplate>
                </asp:GridView>

<%--                <asp:Button ID="btn_guardar" ClientIDMode="Static" runat="server" Text="Guardar" CssClass="btn_guardarCancelar"  /> OnClick="btn_guardar_Click"
                <asp:Button ID="btn_cancelar" ClientIDMode="Static" runat="server" Text="Cancelar" CssClass="btn_guardarCancelar" />  OnClick="btn_cancelar_Click"--%>

                <%--<input type="button" id="button" onclick="test()" value="probar" />--%>
             </div>

            </ContentTemplate>
        </asp:UpdatePanel>

         <%---------------- Inicio POPUP editar puestos ----------------%>

        <div id="popUp_editar_puestos" style="display:none">

            <a href="#" id="cerrar_editar" class="cerrar_PopUp" onclick="popup('popUp_editar_puestos')"></a>
            
            <asp:UpdatePanel ID="UpdatePanelEditar" runat="server">
                <ContentTemplate>

                    <asp:Label ID="lb5" runat="server" CssClass="titulo_editar" Text="Editar puesto"></asp:Label>
                    

                    <asp:MultiView ID="MultiView_editar" runat="server">
                        <asp:View ID="View1_editar" runat="server">

                            <asp:Table ID="table_Editar" ClientIDMode="Static" runat="server">

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_puestoEdit" ClientIDMode="Static" runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPuestoEdit" runat="server"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_positionEdit" ClientIDMode="Static" runat="server" Text="Position:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblPositionEdit" runat="server" ValidationGroup="editar"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <%--<asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_bonosEdit" ClientIDMode="Static" runat="server" Text="Bonos:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtBonosEdit" runat="server" ValidationGroup="editar"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderBonosEdit" runat="server" FilterType="Numbers" TargetControlID="txtBonosEdit" />
                                        <asp:RequiredFieldValidator ID="RFVedit3" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtBonosEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>--%>

                                <%--<asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_cantidadEdit" ClientIDMode="Static" runat="server" Text="Cantidad:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtCantidadEdit" runat="server" ValidationGroup="editar"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCantidadEdit" runat="server" FilterType="Numbers" TargetControlID="txtCantidadEdit" />
                                        <asp:RequiredFieldValidator ID="RFVedit4" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtCantidadEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>--%>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_salarioEdit" ClientIDMode="Static" runat="server" Text="Salario diario:" onkeypress="myFunction()"></asp:Label></asp:TableCell>
                                    <asp:TableCell style="text-align:center; align-content:center">
                                        <asp:TextBox ID="txtSalarioEdit" runat="server" ValidationGroup="editar"  onkeypress="return PresionarBtnEnter(event);" ClientIDMode="Static" ></asp:TextBox>
                                        <asp:Button ID="btn_calcular" ClientIDMode="Static" runat="server" CssClass="btn_guardarCancelar" Text="Calcular" OnClick="btn_calcular_Click" />
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderSalarioEdit" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtSalarioEdit" />
                                        <asp:RequiredFieldValidator ID="RFVedit5" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtSalarioEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_salarioIntegrado" ClientIDMode="Static" runat="server" Text="Salario Integrado:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtSalarioIntegrado" runat="server" Enabled="false" ValidationGroup="editar"></asp:TextBox>
<%--                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="RFVeditIntegrado" />
                                        <asp:RequiredFieldValidator ID="RFVeditIntegrado" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtSalarioIntegrado" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>--%>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <%--<asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_despensaEdit" ClientIDMode="Static" runat="server" Text="Bono de despensa:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtDespensaEdit" runat="server" ValidationGroup="editar"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderDespensaEdit" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtDespensaEdit" />
                                        <asp:RequiredFieldValidator ID="RFV8" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtDespensaEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_puntualidadEdit" ClientIDMode="Static" runat="server" Text="Bono de puntualidad:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPuntualidadEdit" runat="server" ValidationGroup="editar"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPuntualidadEdit" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtPuntualidadEdit" />
                                        <asp:RequiredFieldValidator ID="RFV9" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtPuntualidadEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_asistenciaEdit" ClientIDMode="Static" runat="server" Text="Bono de asistencia:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtAsistenciaEdit" runat="server" ValidationGroup="editar"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderAsistenciaEdit" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtAsistenciaEdit" />
                                        <asp:RequiredFieldValidator ID="RFV10" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtAsistenciaEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>--%>

                                <%--<asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_tipoEdit" ClientIDMode="Static" runat="server" Text="Tipo de empleado:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlTipoEdit" runat="server" ValidationGroup="editar" DataSourceID="SqlDataSource4" DataTextField="desc_esp" DataValueField="id_tipo"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="SELECT id_tipo, desc_esp, desc_eng FROM TNCH_RH.dbo.tipo_empleado" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RFVedit6" runat="server" ErrorMessage="Seleccione uno" ValidationGroup="editar" ControlToValidate="ddlTipoEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>--%>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lbl_turnoEdit" ClientIDMode="Static" runat="server" Text="Turno:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlTurnoEdit" runat="server" ValidationGroup="editar" DataSourceID="SqlDataSource5" DataTextField="turno" DataValueField="turno"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="SELECT turno FROM TNCH_RH.dbo.turnos WHERE estatus = '1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="RFVedit7" runat="server" ErrorMessage="Seleccione uno" ValidationGroup="editar" ControlToValidate="ddlTurnoEdit" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lblCompaniaEdit" ClientIDMode="Static" runat="server" Text="Compañía:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCompaniaEditVer" runat="server"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>

                            <asp:HiddenField ID="HFeditar" runat="server" />

                            <asp:Button ID="btn_guardarEdit" runat="server" Text="Editar" Enabled="false" CssClass="btn_guardarCancelar_disabled" ValidationGroup="editar" OnClick="btn_guardarEdit_Click" /> <%--OnClick="btn_guardarEdit_Click"--%>

                            <asp:Label ID="errorEditar" CssClass="mensaje_error" runat="server"></asp:Label>

                        </asp:View>
                        <asp:View ID="View2_editar" runat="server">

                            <asp:Label ID="editar_mensaje" ClientIDMode="Static" runat="server" ></asp:Label>

                            <asp:Button ID="editar_continuar" ClientIDMode="Static" runat="server" Text="Continuar" OnClientClick="popup('popUp_editar_puestos')" OnClick="editar_continuar_Click" /> <%--OnClick="editar_continuar_Click"--%>
                           

                        </asp:View>
                    </asp:MultiView>

                    
                    
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <%---------------- Fin POPUP editar puestos ----------------%>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />
        

    </div>

</asp:Content>
