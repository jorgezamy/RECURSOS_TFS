<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="capacitacion_cursos_asignacion.aspx.cs" Inherits="recursos.Views.capacitacion_cursos_asignacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function setCBL(sender) {
            var cbl = document.getElementById('<%=chkListPuestos.ClientID %>').getElementsByTagName("input");
            for (i = 0; i < cbl.length; i++) cbl[i].checked = sender.checked;
        }
    </script>

    <link rel="stylesheet" type="text/css" href="../Content/css/capacitacion_cursos_asignacion.css" />


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_editarTractor" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Asignación Cursos"></asp:Label>

                <%--<h2><span class="line-center">Curso</span></h2>--%>

                <asp:SqlDataSource ID="SQLDSCursos" runat="server" SelectCommand="select a.id_capacitacion_cursos, a.descripcion from GCDM_RH.dbo.capacitacion_cursos a where activo = 1 order by a.descripcion" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                <asp:DropDownList ID="ddlCursos" runat="server" AppendDataBoundItems="true" DataSourceID="SQLDSCursos" DataValueField="id_capacitacion_cursos" DataTextField="descripcion" AutoPostBack="true" OnSelectedIndexChanged="ddlCursos_SelectedIndexChanged">
                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                </asp:DropDownList>

                <div id="divPuestos" runat="server" visible="false">

                    <h2><span class="line-center">Departamento - Puesto</span></h2>
                    <asp:CheckBox ID="chkSelect" runat="server" onclick="setCBL(this)" Text="Seleccionar todos" TextAlign="Left" Style="float: left; width: 100px; font-weight: bold" />
                    <asp:CheckBoxList ID="chkListPuestos" runat="server" RepeatColumns="10"></asp:CheckBoxList>
                    <br />
                    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" ValidationGroup="Guardar" CssClass="btn_guardarCancelar" OnClick="btn_guardar_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true';" />
                    <asp:Image ID="ImgLoading" runat="server" ImageUrl="~/images/tenor.gif" Visible="false" Width="5%" />

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_cerrarSesion" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_cerrarSesion_Click" />

    </div>

     <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="modal_popup" runat="server" PopupControlID="panel_registro" TargetControlID="btnShow"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="panel_registro" runat="server" CssClass="pop_up" align="center" Style="display: none">    
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label4" runat="server" Text="Cambios Realizados Correctamente" ForeColor="Green" Font-Bold="true" Font-Size="Large"></asp:Label>
                <br /><br />
                <asp:Button ID="btnClose" runat="server" Text="Regresar" CssClass="btn_guardarCancelar" />


            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_guardar" />
            </Triggers>
        </asp:UpdatePanel>
    
    </asp:Panel>              

</asp:Content>
