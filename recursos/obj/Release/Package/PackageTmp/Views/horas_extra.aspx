<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="horas_extra.aspx.cs" Inherits="recursos.Views.horas_extra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_editarTractor" runat="server">
            <ContentTemplate>

                <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Horas extra"></asp:Label>

                <asp:Table ID="tabla_hrsExtra" runat="server" CssClass="tabla_registrar">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="No. empleado:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:SqlDataSource ID="data_empleado" runat="server" SelectCommand="select a.no_empleado
                                                                                                from tnch_rh.dbo.empleados a 
                                                                                                left join
                                                                                                TNCH_RH.dbo.puesto b
                                                                                                on b.id_puesto = a.id_puesto
                                                                                                where a.fecha_baja is null and a.fecha_egreso is null and
	                                                                                                  b.id_tipo != 1
                                                                                                order by no_empleado" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                            <asp:DropDownList ID="ddlEmpleado" runat="server" DataSourceID="data_empleado" DataTextField="no_empleado" DataValueField="no_empleado" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowNombre" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Nombre:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowDepartamento" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Departamento:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblDepartamento" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowPuesto" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblPuesto" runat="server" Text=""></asp:Label></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowFechaInicio" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Inicio de semana (Lunes):"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="txtFechaInicio" runat="server" placeholder="Lunes" TextMode="Date" OnTextChanged="txtFechaInicio_TextChanged" AutoPostBack="true"></asp:TextBox></asp:TableCell>
                        <asp:TableCell><asp:RequiredFieldValidator ID="RFVFechaInicio" runat="server" ControlToValidate="txtFechaInicio" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Lunes en blanco" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowFechaFin" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Fin de semana (Domingo):"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="txtFechaFin" runat="server" placeholder="Domingo" TextMode="Date" Enabled="false"></asp:TextBox></asp:TableCell>
                        <asp:TableCell><asp:RequiredFieldValidator ID="RFVFechaFin" runat="server" ControlToValidate="txtFechaFin" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Domingo en blanco" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow ID="rowHoras" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Número de horas:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="txtHoras" runat="server" TextMode="Number" Step="Any" Min="0.1" Max="9"></asp:TextBox></asp:TableCell>
                        <asp:TableCell><asp:RequiredFieldValidator ID="RFVHoras" runat="server" ControlToValidate="txtHoras" CssClass="RequiredValidator" 
                                        Display="Dynamic" ErrorMessage="-- Horas en blanco" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredHoras" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtHoras"></ajaxToolkit:FilteredTextBoxExtender>                          
                        </asp:TableCell>
                    </asp:TableRow>


                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="lblComentarios" runat="server" Text="Comentarios:" Visible="false"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="txtComentarios" TextMode="MultiLine" Columns="100" Rows="5" runat="server" Visible="false"/></asp:TableCell>
                    </asp:TableRow>


                </asp:Table>

<%--                <asp:TextBox ID="TextBox1" runat="server" TextMode="Date" AutoPostBack="false" OnTextChanged="TextBox1_TextChanged" ></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Debe seleccionar un Lunes"
            ClientValidationFunction="validateMonday"  ControlToValidate="TextBox1"
            >


        </asp:CustomValidator>
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
    <script>
  
        function validateMonday(a, b, c) {
            var date = new Date(b.Value.replace(/-/g, "/"));
           
            if (date.getDay() != 1) {  // get current day , if it is not monday , return false
               b.IsValid = false;
            }  
            return b;
            
        }

    </script>
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" Enabled="false"  ></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="Validar lunes" OnClick="Button2_Click" />--%>

                <asp:Label ID="mensaje_error" ClientIDMode="Static" runat="server" Text="" CssClass="mensaje_error"></asp:Label>
                
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" ValidationGroup="Guardar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_guardar_Click" />
                <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn_guardarCancelar" OnClick="btn_cancelar_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>