<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="tickets.aspx.cs" Inherits="Login_TFS.Views.tickets.tickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function Func_Calle() {
            var input = $('#<%= txt_buscar.ClientID %>');
            var len = input.val().length;
            input[0].focus();
            input[0].setSelectionRange(len, len);
        }
    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor1">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Tickets"></asp:Label>

        <asp:UpdatePanel ID="UpdatePanel_tickets" runat="server">
            <ContentTemplate>

                <asp:DropDownList ID="drop_tickets_departamentos" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_tickets_departamentos_SelectedIndexChanged">
                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                </asp:DropDownList>

                <div id="div_tickets" runat="server" class="div_tickets" visible="false">

                    <asp:Label runat="server" Text="Buscar un ticket" CssClass="buscarTicket_titulo"></asp:Label>

                    <div id="div_radio" class="table_btnBuscar">
                        <asp:RadioButtonList ID="radio_buscar" ClientIDMode="Static" runat="server" AppendDataBoundItems="true" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="radio_buscar_SelectedIndexChanged">
                            <asp:ListItem Text="Por palabra clave" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Por fecha" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                    <div id="div_fecha" runat="server" class="table_btnBuscar" visible="false">
                        <asp:Table ID="table_buscarXFecha" ClientIDMode="Static" runat="server">
                            <asp:TableRow>
                                <asp:TableCell><asp:TextBox ID="txt_fecha_inicio" runat="server" Text="" TextMode="Date"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="txt_fecha_fin" runat="server" Text="" TextMode="Date"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:ImageButton ID="bt_btnBuscarFecha" runat="server" CssClass="bt_btnbuscar" ImageUrl="~/images/buscar.png" OnMouseOver="src='/images/buscar_white.png';" OnMouseOut="src='/images/buscar.png';" OnClick="bt_btnBuscarFecha_Click" /></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>

                    <div id="div_palabraClave" class="table_btnBuscar">
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="bt_btnbuscar" Visible="false">
                            <asp:Table ID="table_buscarDatos" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label runat="server" Text="Buscar:" CssClass="titulo_buscar_movimientos"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="txt_buscar" ClientIDMode="Static" runat="server" onkeypress="return TextBox_PresionarBtnEnter(event);" placeholder="Palabra clave" ValidationGroup="buscarCajas_event"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:ImageButton ID="bt_btnbuscar" runat="server" CssClass="bt_btnbuscar" ImageUrl="~/images/buscar.png" OnMouseOver="src='/images/buscar_white.png';" OnMouseOut="src='/images/buscar.png';" OnClick="bt_btnbuscar_Click"/></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>
                    </div>

                    <asp:GridView ID="grid_tickets" runat="server" CssClass="grid_tickets" Visible="false" PageSize="7" AllowPaging="true" OnPageIndexChanging="grid_tickets_PageIndexChanging" OnRowDataBound="grid_tickets_RowDataBound" OnDataBound="grid_tickets_DataBound">
                        <HeaderStyle CssClass="grid_tickets_header" />
                        <RowStyle CssClass="grid_tickets_row" />
                        <AlternatingRowStyle CssClass="grid_tickets_altrow" />
                        <PagerStyle CssClass="grid_tickets_pager" />
                        <FooterStyle CssClass="grid_tickets_footer" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btn_ver_ticket" ClientIDMode="Static" runat="server" Text="Ver" CssClass="btn_guardarCancelar" OnClick="btn_ver_ticket_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No Se encontraron datos.
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:Button ID="btn_crearTIcket" runat="server" Text="Generar Ticket" CssClass="btn_guardarCancelar" OnClick="btn_crearTIcket_Click" />

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>