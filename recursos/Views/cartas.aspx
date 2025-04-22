<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="cartas.aspx.cs" Inherits="recursos.Views.plantillas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:UpdatePanel ID="UpdatePanel_plantillas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True"> 
            <ContentTemplate>
                
        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="Titulo" ClientIDMode="Static" runat="server" Text="Plantillas Predeterminadas" Font-Bold="true" Font-Size="Large"></asp:Label>

                <asp:Table ID="tabla_editarTractor" runat="server" CssClass="tabla_registrar">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label runat="server" Text="Tipo de trámite:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlTramite" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTramite_SelectedIndexChanged" Width="100%">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                <asp:ListItem Text="Baja gafete aduana mexicana" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Baja gafete unico" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Guardería" Value="3"></asp:ListItem>
                                <asp:ListItem Text="IMSS" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Beca" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Juzgado familiar" Value="6"></asp:ListItem>
                                <asp:ListItem Text="FONACOT" Value="7"></asp:ListItem>
                                <asp:ListItem Text="VISA" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Préstamo personal" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Trabajo activos" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Trámite FAST" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Trabajo exempleados" Value="12"></asp:ListItem>
                                <asp:ListItem Text="Exámen apto" Value="13"></asp:ListItem>
                                <asp:ListItem Text="Procesar aduana" Value="14"></asp:ListItem>
                                <asp:ListItem Text="Permiso EU" Value="15"></asp:ListItem>
                                <asp:ListItem Text="Solicitud gafete aduana" Value="16"></asp:ListItem>

                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowEmpleado" runat="server" Visible="false" >
                        <asp:TableCell><asp:Label ID="lblEmpleado" runat="server" Text="Empleado: "></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlEmpleado" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpleado_SelectedIndexChanged" Width="100%">
                                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowCapacitador" runat="server" Visible="false"   >
                        <asp:TableCell><asp:Label runat="server" Text="Chofer capacitador:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                        <asp:TableCell><asp:Label ID="lblCapacitador" runat="server"></asp:Label></asp:TableCell>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="rowFecha" runat="server" Visible="false">
                        <asp:TableCell><asp:Label runat="server" Text="Fecha:"></asp:Label></asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tb_fecha_solicitud" runat="server" TextMode="Date"></asp:TextBox>
                            <asp:Label ID="lbl_fecha" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
    
                    </asp:TableRow>

                </asp:Table>
                <br />
                <asp:Button ID="btn_guardar" runat="server"  Text="Generar" ValidationGroup="Guardar" CssClass="btn_guardarCancelar" Visible="false" OnClick="btn_guardar_Click" />

                <asp:Button ID="btn_cancelar" runat="server"  Text="Cancelar" CssClass="btn_guardarCancelar" OnClick="btn_cancelar_Click" />

                </ContentTemplate>

            <Triggers>
                <asp:PostBackTrigger ControlID="btn_guardar" />
            </Triggers>

        </asp:UpdatePanel>
            
        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>
