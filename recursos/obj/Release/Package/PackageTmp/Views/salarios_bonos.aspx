<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="salarios_bonos.aspx.cs" Inherits="recursos.Views.bonos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <div id="contenedor">
         
        <asp:UpdatePanel ID="UpdatePanel_bonos" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Bonos operadores"></asp:Label>

                <asp:GridView ID="grid_bonos" runat="server" CssClass="grid_bonos" AutoGenerateColumns="false" AllowPaging="true" ShowFooter="false" PageSize="10" OnPageIndexChanging="grid_bonos_PageIndexChanging" DataKeyNames="id_aportacion_deduccion_concepto" EmptyDataText="No existen datos registrados."
                    OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound">
                    <HeaderStyle CssClass="grid_bonos_header" />
                    <RowStyle CssClass="grid_bonos_row" />
                    <AlternatingRowStyle CssClass="grid_bonos_altrow" />
                    <PagerStyle CssClass="grid_bonos_pager" />
                    <FooterStyle CssClass="grid_bonos_footer" />
                    <Columns>

                        <asp:TemplateField HeaderText="Bono" ItemStyle-Width="140">
                            <ItemTemplate>
                                <asp:Label ID="lblBono" runat="server" Text='<%# Eval("descripcion") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBono" runat="server" Text='<%# Eval("descripcion") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor para 5ta rueda" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblQuinta" runat="server" Text='<%# Eval("valor_quinta") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQuinta" runat="server" Text='<%# Eval("valor_quinta") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor para rabón" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblRabon" runat="server" Text='<%# Eval("valor_rabon") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRabon" runat="server" Text='<%# Eval("valor_rabon") %>' Width="140"></asp:TextBox>
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

                <table class="footer_bonos" style="border-collapse: collapse">
                    <tr>
                        <td style="width: 150px">Bono:<br />
                            <asp:TextBox ID="txtBono" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">Valor para 5ta rueda:<br />
                            <asp:TextBox ID="txtQuinta" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">Valor para rabón:<br />
                            <asp:TextBox ID="txtRabon" runat="server" Width="140" />
                        </td>
                        <td style="width: 130px">
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn_guardarCancelar" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>