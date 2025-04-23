<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="incidencias.aspx.cs" Inherits="recursos.Views.incidencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>
     
    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_incidencias" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Incidencias"></asp:Label>

                <asp:GridView ID="grid_incidencias" CssClass="grid_incidencias" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" ShowFooter="false" DataKeyNames="id_incidencia" EmptyDataText="No existen datos registrados."
                    OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound">
                    <HeaderStyle CssClass="grid_buscar_header" />
                    <RowStyle CssClass="grid_buscar_row" />
                    <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                    <PagerStyle CssClass="grid_buscar_pager" />
                    <Columns>

                        <asp:TemplateField HeaderText="Incidencia" ItemStyle-Width="180">
                            <ItemTemplate>
                                <asp:Label ID="lblIncidencia" runat="server" Text='<%# Eval("descripcion") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIncidencia" runat="server" Text='<%# Eval("descripcion") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Departamento" ItemStyle-Width="180">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartamento" runat="server" Text='<%# Eval("departamento") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlDepartamento" runat="server"  Width="140"></asp:DropDownList> <%--SelectedValue='<%# Bind("departamento") %>'--%>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Número de permitidas" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblPermitidas" runat="server" Text='<%# Eval("no_permitidas") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPermitidas" runat="server" Text='<%# Eval("no_permitidas") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ButtonType="Link" ShowEditButton="true"  ShowDeleteButton="true"
                            DeleteImageUrl="~/images/delete_image.png" ItemStyle-Width="150"
                            EditText="Editar" DeleteText="Eliminar" UpdateText="Actualizar" CancelText="Cancelar" />
                    </Columns>

                    <EmptyDataTemplate>
                        No se encontraron datos.
                    </EmptyDataTemplate>
                </asp:GridView>

                <table class="footer_incidencias" width="52%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="width: 150px">Incidencia:<br />
                            <asp:TextBox ID="txtIncidencia" runat="server" Width="140" />
                            <asp:RequiredFieldValidator ID="RFVIncidencia" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtIncidencia" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>

                        </td>
                        <td style="width: 150px">Departamento:<br />
                            <asp:DropDownList ID="ddlDepartamento" runat="server" Width="140" />
                            <asp:RequiredFieldValidator ID="RFVDepartamento" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="ddlDepartamento" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>

                        </td>
                        <td style="width: 150px">No. de permitidas:<br />
                            <asp:TextBox ID="txtPermitidas" runat="server" Width="140" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPermitidas" runat="server" FilterType="Numbers" TargetControlID="txtPermitidas" />
                            <asp:RequiredFieldValidator ID="RFVPermitidas" runat="server" ErrorMessage="Campo vacio" ValidationGroup="editar" ControlToValidate="txtPermitidas" ForeColor="Red" BackColor="Transparent"></asp:RequiredFieldValidator>

                        </td>
                        <td style="width: 130px">
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" OnClick="btnAdd_Click" ValidationGroup="editar" />
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>