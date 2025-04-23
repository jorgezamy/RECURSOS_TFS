<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="horarios.aspx.cs" Inherits="recursos.Views.horarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>

    <script type="text/javascript">
        function ShowLabel() {
            document.getElementById('myPopup').style.display = 'block';
            document.getElementById('myPopup').style.Visible = true;
            //return false;
        }

        function hide() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("myPopup").style.display = "none";
            }, seconds * 1000);
        }

        function popup() {
            var popup = document.getElementById("myPopup");
            popup.classList.toggle("show");
        }
    </script>--%>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Asignación de horarios"></asp:Label>

        <asp:UpdatePanel ID="UpdatePanel_horarios" runat="server">
            <ContentTemplate>

                <asp:Table ID="table_horarios" ClientIDMode="Static" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="No. Empleado: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="4">
                            <asp:DropDownList ID="drop_noEmpleado" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="drop_noEmpleado_SelectedIndexChanged">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="required_noEmpleado" runat="server" ControlToValidate="drop_noEmpleado" ErrorMessage="*Vacio" CssClass="mensaje_error" Display="Dynamic"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Nombre: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label ID="lbl_nombre" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Departamento: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label ID="lbl_depto" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Puesto: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell ColumnSpan="4">
                            <asp:Label ID="lbl_puesto" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="5">
                            <asp:Label ID="sub_horarios" ClientIDMode="Static" runat="server" Text="Dias Laborados"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_dom" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Domingo" Value="1"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioDom_entrada" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioDom_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_lun" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Lunes" Value="2"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioLun_entrada" runat="server" Text="" TextMode="Time" AutoPostBack="true" OnTextChanged="tb_horarioLun_entrada_TextChanged"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<asp:Label ID="lb_horarioLun_salida" runat="server" Text=""></asp:Label>--%>
                            <asp:TextBox ID="tb_horarioLun_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_mar" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Martes" Value="3"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioMar_entrada" runat="server" Text="" TextMode="Time" AutoPostBack="true" OnTextChanged="tb_horarioMar_entrada_TextChanged"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<asp:Label ID="lb_horarioMar_salida" runat="server" Text=""></asp:Label>--%>
                            <asp:TextBox ID="tb_horarioMar_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_mie" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Miercoles" Value="4"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioMie_entrada" runat="server" Text="" TextMode="Time" AutoPostBack="true" OnTextChanged="tb_horarioMie_entrada_TextChanged"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<asp:Label ID="lb_horarioMie_salida" runat="server" Text=""></asp:Label>--%>
                            <asp:TextBox ID="tb_horarioMie_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_jue" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Jueves" Value="5"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioJue_entrada" runat="server" Text="" TextMode="Time" AutoPostBack="true" OnTextChanged="tb_horarioJue_entrada_TextChanged"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<asp:Label ID="lb_horarioJue_salida" runat="server" Text=""></asp:Label>--%>
                            <asp:TextBox ID="tb_horarioJue_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_vie" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Viernes" Value="6"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioVie_entrada" runat="server" Text="" TextMode="Time" AutoPostBack="true" OnTextChanged="tb_horarioVie_entrada_TextChanged"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida:"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <%--<asp:Label ID="lb_horarioVie_salida" runat="server" Text=""></asp:Label>--%>
                            <asp:TextBox ID="tb_horarioVie_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:CheckBoxList ID="chbx_sab" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Sábado" Value="7"></asp:ListItem>
                            </asp:CheckBoxList>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Entrada: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioSab_entrada" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label runat="server" Text="Hora Salida: "></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_horarioSab_salida" runat="server" Text="" TextMode="Time"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn_guardar_horarios" OnClientClick="popup('popUp_editar_guardar'); return false" />
<%--            <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn_guardar_horarios" OnClick="btn_guardar_Click" />--%>


            <%---------------- POPUP BLANKET ----------------%>
             <div id="blanket" style="display:none">
             </div>

             <%---------------- POPUP GUARDAR ----------------%>
             <div id="popUp_editar_guardar" style="display:none">
                            <asp:Label ID="lb_cerrarPopUp_guardar" ClientIDMode="Static" runat="server" Text="¿Está seguro que desea GUARDAR los cambios?"></asp:Label>

                            <asp:Button ID="btn_cerrarPopUp_guardar_si" runat="server" Text="Si" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_guardar_Click" />
                            <asp:Button ID="btn_cerrarPopUp_guardar_no" runat="server" Text="No" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClientClick="popup('popUp_editar_guardar'); return false" />
             </div>


            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="popup">
            <span class="popuptext" id="myPopup" style="display:none;">Los cambios han sido registrados exitosaménte.</span>
        </div>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>