<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="desarrollo_urbano.aspx.cs" Inherits="recursos.Views.desarrollo_urbano" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Call()
        {
            var clickButton = document.getElementById("<%= btnBuscar.ClientID %>");
            clickButton.click();

            var txtControl = document.getElementById('<%= tb_Colonia.ClientID %>');
            txtControl.focus();  
            $('#<%= tb_Colonia.ClientID %>').focus();
        }

        function Call_Calle()
        {
            var clickButton = document.getElementById("<%= btn_buscar_calle.ClientID %>");
            clickButton.click();

<%--            var txtControl = document.getElementById('<%= tb_calle.ClientID %>');
            txtControl.focus();  
            $('#<%= tb_calle.ClientID %>').focus();--%>
        }

        function Call_Codigo()
        {
            var clickButton = document.getElementById("<%= btn_buscar_codigo.ClientID %>");
            clickButton.click();
        }

        function Func() {
                        var input = $('#<%= tb_Colonia.ClientID %>');
            var len = input.val().length;
            input[0].focus();
            input[0].setSelectionRange(len, len);
        }

        function Func_Calle() {
                        var input = $('#<%= tb_calle.ClientID %>');
            var len = input.val().length;
            input[0].focus();
            input[0].setSelectionRange(len, len);
        }

        function Func_Codigo() {
                        var input = $('#<%= tb_codigo_postal.ClientID %>');
            var len = input.val().length;
            input[0].focus();
            input[0].setSelectionRange(len, len);
        }

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Desarrollo Urbano"></asp:Label>

        <asp:UpdatePanel ID="update_calles" runat="server">
            <ContentTemplate>

                <asp:HiddenField ID="Hidden_Valor" runat="server" />

                <asp:Button ID="tab1" Text="Agregar Colonia" runat="server" OnClick="tab1_Click"  CssClass="initial_desarrollo"  />
                <asp:Button ID="tab2" Text="Agregar Calle" runat="server" OnClick="tab2_Click"  CssClass="initial_desarrollo"  />                 
                <asp:Button ID="tab3" Text="Agregar Codigo Postal" runat="server" OnClick="tab3_Click"  CssClass="initial_desarrollo"  />
                <asp:Button ID="tab4" Text="Editar Prefijo Codigo Postal" runat="server" OnClick="tab4_Click"  CssClass="initial_desarrollo"  />

                <asp:MultiView ID="MultiView" runat="server">
                    <asp:View ID="Add_Codigo_Colonia" runat="server">
                        <br />
                        <br />
                        <asp:Label ID="lbl_Colonia" runat="server" Text="Agregar Colonia" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <br />
                        <asp:Table ID="tabla_colonia" runat="server"  HorizontalAlign="Center">
                            <asp:TableRow>
                                <asp:TableCell>
                                    País
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_pais_colonia" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="data_pais" DataValueField="id_pais" DataTextField="desc_esp" OnSelectedIndexChanged="drop_pais_colonia_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_drop_pais" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_pais_colonia" InitialValue="" ValidationGroup="Guardar_Colonia"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Estado
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_estado_colonia" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_estado_colonia_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_drop_estado" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_estado_colonia" InitialValue="" ValidationGroup="Guardar_Colonia"></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Ciudad
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_ciudad_colonia" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_ciudad_colonia_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_drop_ciudad" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_ciudad_colonia" InitialValue="" ValidationGroup="Guardar_Colonia"></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Codigo Postal
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_codigoPostal_colonia" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_codigoPostal_colonia_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_drop_codigo" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_codigoPostal_colonia" InitialValue="" ValidationGroup="Guardar_Colonia"></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Nombre de Colonia:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="tb_Colonia" runat="server" AutoPostBack="true" onkeyup="Call()" Enabled="false"></asp:TextBox>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" style="display:none" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                        <br />
                        <asp:GridView ID="gridview_colonias" runat="server" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="Sin Coincidencias" AllowPaging="true" PageSize="10" OnPageIndexChanging="Gridview1_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#FDF5AC"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#4D0000"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#FCF6C0"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#820000"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                        <asp:Label ID="lbl_alerta_colonia" runat="server" Text="" Font-Bold="true"></asp:Label>
                        <br />
                        <br />
                        <asp:Button ID="btn_guardar_colonia" runat="server" Text="Guardar Colonia" CssClass="btn_guardarCancelar_disabled" OnClick="btn_guardar_colonia_Click" ValidationGroup="Guardar_Colonia" />
                       
                        <br />
                        <asp:Label runat="server" Text="Colonias con nombre parecido"></asp:Label>
                    
                        <asp:GridView ID="gridview_colonias_parecidas" runat="server" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gridview_colonias_parecidas_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#FDF5AC"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#4D0000"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#FCF6C0"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#820000"></SortedDescendingHeaderStyle>
                        </asp:GridView> 

                    </asp:View>
                    <asp:View ID="Add_Calle" runat="server">
                        <br />
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="Agregar Calle" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <br />
                        <asp:Table ID="tabla_calle" runat="server"  HorizontalAlign="Center">
                            <asp:TableRow>
                                <asp:TableCell>
                                    País
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_pais_calle" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="data_pais" DataValueField="id_pais" DataTextField="desc_esp" OnSelectedIndexChanged="drop_pais_calle_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_pais_calle" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_pais_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Estado
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_estado_calle" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_estado_calle_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_estado_calle" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_estado_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Ciudad
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_ciudad_calle" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_ciudad_calle_SelectedIndexChanged"  >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_ciudad_calle" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_ciudad_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Codigo Postal
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_codigo_postal_calle" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_codigo_postal_calle_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_codigo_postal_calle" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_codigo_postal_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Colonia
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_colonia_calle" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_colonia_calle_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_colonia_calle" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_colonia_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Calle
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="tb_calle" runat="server" Enabled="false"  onkeyup="Call_Calle()"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="required_calle" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>
                                    <asp:Button ID="btn_buscar_calle" runat="server" Text="Buscar" OnClick="btn_buscar_calle_Click" style="display:none" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell>
                                    Tipo
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_down_tipo_calle" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        <asp:ListItem Text="CONDOMINIO" Value="CONDOMINIO"></asp:ListItem>
                                        <asp:ListItem Text="COLONIA" Value="COLONIA"></asp:ListItem>
                                        <asp:ListItem Text="FRACCIONAMIENTO" Value="FRACCIONAMIENTO"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_down_tipo_calle" InitialValue="" ValidationGroup="GuardarCalle"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                         </asp:Table>       
                        <br />
                        <asp:Label ID="lbl_alerta_calle" runat="server" Text="" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:GridView ID="gridview_calle" runat="server" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="Se puede agregar la calle" AllowPaging="true" PageSize="10" OnPageIndexChanging="gridview_calle_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#FDF5AC"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#4D0000"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#FCF6C0"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#820000"></SortedDescendingHeaderStyle>
                        </asp:GridView>    
                        <br />
                        <asp:Button ID="btn_guardar_calle" runat="server" Enabled="false" Text="Guardar Calle" ValidationGroup="GuardarCalle" CssClass="btn_guardarCancelar_disabled" OnClick="btn_guardar_calle_Click" />
                        <br />
                        <br />
                        Calles con el mismo nombre
                        <asp:GridView ID="gridview_parecido" runat="server" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="true" PageSize="10" OnPageIndexChanging="gridview_parecido_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#FDF5AC"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#4D0000"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#FCF6C0"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#820000"></SortedDescendingHeaderStyle>
                        </asp:GridView> 
                    
                    </asp:View>
                    <asp:View ID="Add_Codigo" runat="server">
                        <br />
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Agregar Codigo Postal" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <asp:Table ID="tabla_codigo_postal" runat="server"  HorizontalAlign="Center">
                            <asp:TableRow>
                                <asp:TableCell>
                                    País
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_down_pais_codigo" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="data_pais" DataValueField="id_pais" DataTextField="desc_esp" OnSelectedIndexChanged="drop_down_pais_codigo_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_field_pais" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_down_pais_codigo" InitialValue="" ValidationGroup="GuardarCodigoPostal"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Estado
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_down_estado_codigo" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_down_estado_codigo_SelectedIndexChanged" >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_field_estado" runat="server" ValidationGroup="GuardarCodigoPostal" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_down_estado_codigo" InitialValue="" ></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Ciudad
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_down_ciudad_codigo" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_down_ciudad_codigo_SelectedIndexChanged"  >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_field_ciudad" runat="server" ValidationGroup="GuardarCodigoPostal" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_down_ciudad_codigo" InitialValue="" ></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Codigo Postal
                                    <br />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="tb_codigo_postal" runat="server" Enabled="false" onkeyup="Call_Codigo()" MaxLength="5" ></asp:TextBox>                                     
                                        <asp:RegularExpressionValidator ID="rev" runat="server"    ControlToValidate="tb_codigo_postal"
                                            ErrorMessage="*"
                                            ForeColor="Red"
                                            ValidationExpression="^\d+$">    
                                          </asp:RegularExpressionValidator>
                                    <br />
                                    <asp:Button ID="btn_buscar_codigo" runat="server" Text="Buscar" OnClick="btn_buscar_codigo_Click" style="display:none"/>

                                </asp:TableCell>
                            </asp:TableRow>
                         </asp:Table>       
                        <br />

                        <asp:GridView ID="gridview_codigos" runat="server" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="Sin Coincidencias" AllowPaging="true" PageSize="10" OnPageIndexChanging="gridview_codigos_PageIndexChanging">
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

                            <PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#FDF5AC"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#4D0000"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#FCF6C0"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#820000"></SortedDescendingHeaderStyle>
                        </asp:GridView>

                        <asp:Label ID="lbl_alerta_codigoPostal" runat="server" Text="" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Button ID="btn_guardar_codigo" runat="server" Enabled="false" Text="Guardar Codigo Postal" ValidationGroup="GuardarCodigoPostal" CssClass="btn_guardarCancelar_disabled"  OnClick="btn_guardar_codigo_Click" />
                    </asp:View>
                    <asp:View ID="Editar_Codigo" runat="server">
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Editar Prefijo" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <asp:Table ID="tabla_prefijo" HorizontalAlign="Center" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                    Pais:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_pais_prefijo" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="data_pais" DataValueField="id_pais" DataTextField="desc_esp" OnSelectedIndexChanged="drop_pais_prefijo_SelectedIndexChanged">
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="required_pais_prefijo" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_pais_prefijo" InitialValue="" ValidationGroup="GuardarPrefijo"></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Estado:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_estado_prefijo" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_estado_prefijo_SelectedIndexChanged"  >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="GuardarPrefijo" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_estado_prefijo" InitialValue="" ></asp:RequiredFieldValidator>

                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Ciudad:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList ID="drop_ciudad_prefijo" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_ciudad_prefijo_SelectedIndexChanged"  >
                                        <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="GuardarPrefijo" ErrorMessage="*" ForeColor="Red" ControlToValidate="drop_ciudad_prefijo" InitialValue="" ></asp:RequiredFieldValidator>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    Prefijo:
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:TextBox ID="tb_prefijo" runat="server" Enabled="false" MaxLength="2" ></asp:TextBox>
                                </asp:TableCell>
                             </asp:TableRow>
                        </asp:Table>
                        <asp:Label ID="lbl_alerta_prefijo" runat="server" Text="" Font-Bold="true"></asp:Label>
                        <br /><br />
                        <asp:Button ID="btn_guardar_prefijo" runat="server" Enabled="false" Text="Editar Prefijo" ValidationGroup="GuardarPrefijo" CssClass="btn_guardarCancelar_disabled" OnClick="btn_guardar_prefijo_Click" />

                    </asp:View>
                </asp:MultiView>



                <asp:Table ID="ubicacion_table" runat="server" Visible="false">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Pais:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="data_pais" runat="server" SelectCommand="select id_pais, desc_esp from GCDM.dbo.paises where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                            <asp:DropDownList ID="drop_pais" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataSourceID="data_pais" DataValueField="id_pais" DataTextField="desc_esp" OnSelectedIndexChanged="drop_pais_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Estado:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="drop_estado" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_estado_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Ciudad:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="drop_ciudad" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_ciudad_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Codigo Postal:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="drop_codigoPostal" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_codigoPostal_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Colonia:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="drop_colonia" runat="server" Enabled="false" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_colonia_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Calles:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="drop_calles" runat="server" Enabled="false" AppendDataBoundItems="true">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Button ID="btn_guardar" runat="server" Enabled="false" Visible="false" Text="Guardar" CssClass="btn_guardarCancelar" OnClick="btn_guardar_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
<%--    MODAL GUARDAR COLONIA--%>
        <asp:Button ID="btn_colonia" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="btn_colonia" ID="modalpop_guardar_colonia" runat="server" PopupControlID="pop_up_guardar_colonia" BackgroundCssClass="modalBackground" CancelControlID="btn_close_modal_agregar" >
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>


        <div id="pop_up_guardar_colonia" style="display:none" class="pop_up_guardar_colonia">
            <br />
            <asp:Label ID="lbl_guardar_colonia" runat="server" Text="Guardar Colonia" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label  runat="server" Text="¿Registrar colonia?"></asp:Label>
            <br /><br/>
            <asp:Button ID="btn_colonia_modal_agregar" runat="server" CssClass="btn_guardarCancelar" Text="Guardar Colonia" OnClick="btn_colonia_modal_agregar_Click" />
            <asp:Button ID="btn_close_modal_agregar" runat="server" CssClass="btn_guardarCancelar" Text="Regresar" />
        </div>
<%--    MODAL GUARDAR COLONIA--%>


<%--    MODAL GUARDAR CALLE--%>
        <asp:Button ID="btn_calle" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="btn_calle" ID="modal_popup_guardar_calle" runat="server" PopupControlID="pop_up_guardar_calle" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrar_popUp_Calle" >
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>


        <div id="pop_up_guardar_calle" style="display:none" class="pop_up_guardar_calle">
            <br />
            <asp:Label runat="server" Text="Guardar Calle" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label  runat="server" Text="¿Registrar calle?"></asp:Label>
            <br /><br/>
            <asp:Button ID="btn_guardar_popUp_Calle" runat="server" CssClass="btn_guardarCancelar" Text="Guardar Calle" OnClick="btn_guardar_popUp_Calle_Click" />
            <asp:Button ID="btn_cerrar_popUp_Calle" runat="server" CssClass="btn_guardarCancelar" Text="Regresar" />
        </div>
<%--    MODAL GUARDAR CALLE--%>


<%--    MODAL GUARDAR CODIGO POSTAL--%>
        <asp:Button ID="btn_codigo" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="btn_codigo" ID="modal_popup_guardar_codigoPostal" runat="server" PopupControlID="pop_up_guardar_codigoPostal" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrar_popUp_codigoPostal" >
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>


        <div id="pop_up_guardar_codigoPostal" style="display:none" class="pop_up_guardar_codigoPostal">
            <br />
            <asp:Label runat="server" Text="Codigo Postal" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label  runat="server" Text="¿Registrar codigo postal?"></asp:Label>
            <br /><br/>
            <asp:Button ID="btn_guardar_popUp_codigoPostal" runat="server" CssClass="btn_guardarCancelar" Text="Guardar Codigo Postal" OnClick="btn_guardar_popUp_codigoPostal_Click"/>
            <asp:Button ID="btn_cerrar_popUp_codigoPostal" runat="server" CssClass="btn_guardarCancelar" Text="Regresar" />
        </div>
<%--    MODAL GUARDAR CODIGO POSTAL--%>

<%--    MODAL GUARDAR PREFIJO--%>
        <asp:Button ID="btn_prefijo" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="btn_prefijo" ID="modal_popup_guardar_prefijo" runat="server" PopupControlID="pop_up_guardar_prefijo" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrar_popUp_prefijo" >
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>


        <div id="pop_up_guardar_prefijo" style="display:none" class="pop_up_guardar_prefijo">
            <br />
            <asp:Label runat="server" Text="Prefijo" Font-Bold="true"></asp:Label>
            <br />
            <asp:Label  runat="server" Text="¿Registrar prefijo al codigo postal?"></asp:Label>
            <br /><br/>
            <asp:Button ID="btn_guardar_popUp_prefijo" runat="server" CssClass="btn_guardarCancelar" Text="Guardar Prefijo" OnClick="btn_guardar_popUp_prefijo_Click"/>
            <asp:Button ID="btn_cerrar_popUp_prefijo" runat="server" CssClass="btn_guardarCancelar" Text="Regresar" />
        </div>
<%--    MODAL GUARDAR PREFIJO--%>

    <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

</asp:Content>