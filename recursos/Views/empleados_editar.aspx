<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="empleados_editar.aspx.cs" Inherits="recursos.Views.empleados_editar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div id="contenedor_empleados_editar">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:ScriptManager runat="server"></asp:ScriptManager>
         


        <asp:UpdatePanel ID="UpdatePanelEditar" runat="server">
            <ContentTemplate>

                <asp:MultiView ID="MV_EditarEmpleados" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div id="contenedor_empleados_ver">     

                        <asp:Label ID="editar_titulo" ClientIDMode="Static" runat="server" Text="EDITAR EMPLEADO"></asp:Label>

                        <asp:Table ID="tabla_principal" ClientIDMode="Static" runat="server" HorizontalAlign="Center">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <!------------------------------------------------------ tabla de datos laborales ------------------------------------------------------>
                                    <asp:Table ID="tabla_editar" ClientIDMode="Static" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell><asp:Label ID="lb_laborales" runat="server" Text="Datos Laborales" CssClass="editar_subTitulo_laboral"></asp:Label></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell><asp:Label ID="lb_empleado" runat="server" Text="No. Empleado" ></asp:Label></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_numero" runat="server" Text=""></asp:Label>
                                                <br /><br />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_nombre" runat="server" Text="Nombre:"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="nombre" runat="server" Text=""></asp:Label>
                                                <br /><br />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell >
                                                <asp:Label ID="lb_curp" runat="server" Text="Curp:"></asp:Label>
                                                <br />
                                                <asp:Label ID="lb_curpNo" runat="server" Text=""></asp:Label>
                                                <br /><br />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_rfc" runat="server" Text="RFC:"></asp:Label>
                                                <br />
                                                <asp:Label ID="tb_rfc" runat="server" Text=""></asp:Label>
                                                <br /><br />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_depto" runat="server" Text="Departamento"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_depto" runat="server" SelectCommand="select id_depto, desc_esp from GCDM_rh.dbo.departamento a where estatus = '1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_depto" runat="server" AutoPostBack="true" AppendDataBoundItems="true" DataSourceID="data_depto" DataTextField="desc_esp" DataValueField="id_depto" OnSelectedIndexChanged="drop_depto_SelectedIndexChanged">
                                                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_puesto" runat="server" Text="Puesto"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow >
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_puesto" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Enabled="false" OnSelectedIndexChanged="drop_puesto_SelectedIndexChanged">
                                                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="row_cliente_lb" runat="server" >
                                            <asp:TableCell>
                                                <asp:Label ID="lb_cliente" runat="server" Text="Cliente:"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="row_cliente_drop" runat="server" >
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_cliente" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="drop_cliente_SelectedIndexChanged"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="row_movimiento_lb" runat="server">
                                            <asp:TableCell>
                                                <asp:Label ID="lb_puestoCliente" runat="server" Text="Tipo Movimiento:"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow ID="row_movimiento_drop" runat="server">
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_puestoCliente" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="drop_puestoCliente_SelectedIndexChanged"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_supervisor" runat="server" Text="Supervisor:"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_supervisor" runat="server" SelectCommand="select no_empleado from GCDM_rh.dbo.empleados where fecha_egreso is null and fecha_baja is null and contrato = 'permanente'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_supervisor" runat="server" DataSourceID="data_supervisor" DataTextField="no_empleado" DataValueField="no_empleado">
                                                    <asp:ListItem Text="--- Sin Asignar ---" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow ID="rowBonos" runat="server" Visible="false" HorizontalAlign="Justify">
                                            <asp:TableCell>
                                                <%--<asp:CheckBox ID="CheckBox1" Text="Bono de disponibilidad y desempeño" runat="server" />
                                                <br />
                                                <asp:CheckBox ID="CheckBox2" Text="Bono de disponibilidad y desempeño" runat="server" />
                                                <br />
                                                <asp:CheckBox ID="CheckBox3" Text="Bono de disponibilidad y desempeño" runat="server" />
                                                <br />
                                                <asp:CheckBox ID="CheckBox4" Text="Bono de disponibilidad y desempeño" runat="server" />
                                                <br />
                                                <asp:CheckBox ID="CheckBox5" Text="Bono de disponibilidad y desempeño" runat="server" />--%>
            <%--                                    <asp:SqlDataSource ID="SQLDS_Bonos" runat="server" SelectCommand="select id_bono_operador, descripcion from GCDM_RH.dbo.bonos_operador_menu where activo = '1' and id_bono_operador = '4' or id_bono_operador = '5' or id_bono_operador = '6' or id_bono_operador = '10'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>--%>
                                                <asp:CheckBoxList ID="chklBonos" ClientIDMode="Static" runat="server"  DataTextField="descripcion" DataValueField="id_aportacion_deduccion_concepto"></asp:CheckBoxList> <%--DataSourceID="SQLDS_Bonos"--%>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <!------------------------------------------------------ tabla de datos principales ------------------------------------------------------>
                                    <asp:Table ID="tabla_editar_principales" ClientIDMode="Static" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2">
                                                <asp:Label ID="principales" runat="server" Text="Datos Principales" CssClass="editar_subTitulo_principales"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_telefono" runat="server" Text="Teléfono:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_telefono" runat="server" Text="" ></asp:TextBox><%--pattern=".{0}|.{12,12}"--%>
                                                <br />
<%--                                                <asp:RegularExpressionValidator ID="RegExp1" runat="server"    
                                                ErrorMessage="Ejemplo: +526567851623"
                                                CssClass="RegularValidator"
                                                ControlToValidate="tb_telefono"    
                                                ValidationExpression="^[+]\d{10,12}$" />  --%>  
                                                
<%--                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" 
                                                runat="server" TargetControlID="tb_telefono"
                                                Mask="+(999)999-9999"
                                                MaskType="None"
                                                MessageValidatorTip="true"
                                                OnFocusCssClass="editmask"
                                                OnInvalidCssClass="invalidmask"
                                                InputDirection="LeftToRight"
                                                ClearMaskOnLostFocus="false"
                                                AutoComplete="false"/>--%>

                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" 
                                                runat="server" TargetControlID="tb_telefono"
                                                Mask="+99(999)999-9999"
                                                MaskType="None"
                                                MessageValidatorTip="true"
                                                OnFocusCssClass="editmask"
                                                OnInvalidCssClass="invalidmask"
                                                InputDirection="LeftToRight"
                                                ClearMaskOnLostFocus="false"
                                                AutoComplete="false"
                                                />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_correo" runat="server" Text="Correo:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_correo" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_zonas" runat="server" Text="Zona Eco:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_zonas" runat="server" SelectCommand="select id_zona_economica, zona_economica from GCDM_rh.dbo.zonas where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_zonas" runat="server" DataSourceID="data_zonas" DataTextField="zona_economica" DataValueField="id_zona_economica" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_paisDir" runat="server" Text="Pais:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_paisDir" runat="server" SelectCommand="select id_pais, desc_esp from GCDM.dbo.paises where activo = '1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_paisDir" runat="server" DataSourceID="data_paisDir" DataTextField="desc_esp" DataValueField="id_pais" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="drop_paisDir_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_edoDir" runat="server" Text="Estado:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_edoDir" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_edoDir_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                    <asp:RequiredFieldValidator id="RequiredEdo" CssClass="RegularValidator" Display="Dynamic" Text="*" InitialValue="na" ControlToValidate="drop_edoDir" Runat="server" ValidationGroup="Validar" />                                                                                    
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_cdDir" runat="server" Text="Ciudad:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_cdDir" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_cdDir_SelectedIndexChanged"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_codPos" runat="server" Text="C.P.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_codPos" runat="server" Text="" placeholder="#" Visible="false"></asp:TextBox>
<%--                                                <ajaxToolkit:MaskedEditExtender ID="mask_codPos" runat="server" TargetControlID="tb_codPos" Mask="99999" />--%>
                                                <asp:DropDownList ID="Drop_codPostDir" runat="server"  AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="Drop_codPostDir_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Text="-- Seleccionar --" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_colonia" runat="server" Text="Colonia:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_colonia" runat="server" Text=""  Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="Drop_colonia" runat="server"  DataTextField="descripcion" DataValueField="id_colonia" AutoPostBack="true" OnSelectedIndexChanged="Drop_colonia_SelectedIndexChanged"></asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator id="RequiredColonia" CssClass="RegularValidator" Display="Dynamic" Text="*Seleccionar colonia" InitialValue="na" ControlToValidate="Drop_colonia" Runat="server" ValidationGroup="Validar" />                                            
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_calle" runat="server" Text="Calle:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_calle" runat="server" Text="" Visible="false"></asp:TextBox>
<%--                                                <br />--%>
                                                  <asp:DropDownList ID="Drop_calleDir" runat="server"  Enabled="false" AutoPostBack="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Text="-- Seleccionar --" Value=""></asp:ListItem>
                                                </asp:DropDownList>
<%--                                                <asp:RequiredFieldValidator id="RequiredFieldValidator1" CssClass="RegularValidator" Display="Dynamic" Text="*Seleccionar calle" ControlToValidate="Drop_calleDir" Runat="server" ValidationGroup="Validar" />                                            --%>

                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_CalleNo" runat="server" Text="No. Ext.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_CalleNo" runat="server" Text=""></asp:TextBox>
                                                <asp:RequiredFieldValidator id="RequiredFieldValidator2" CssClass="RegularValidator" Display="Dynamic" Text="**" InitialValue="" ControlToValidate="tb_CalleNo" Runat="server" ValidationGroup="Validar" />                                            

                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_calleInt" runat="server" Text="No. Int.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_calleInt" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_tallaC" runat="server" Text="Talla C."></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_tallaC" runat="server" SelectCommand="select id_tallaC, desc_esp from GCDM_rh.dbo.tallaC where activo = '1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_tallaC" runat="server" DataSourceID="data_tallaC" DataValueField="id_tallaC" DataTextField="desc_esp" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="" Text="-- Seleccionar --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_tallaP" runat="server" Text="Talla P:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_tallaP" runat="server">
                                                    <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                                <asp:TableCell>
                                        <!------------------------------------------------------ tabla de datos personales ------------------------------------------------------>
                                    <asp:Table ID="tabla_editar_personales" ClientIDMode="Static" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2">
                                                <asp:Label ID="personales" runat="server" Text="Datos Personales" CssClass="editar_subTitulo_personal"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_fecNac" runat="server" Text="Fec. Nac.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_fecNac" runat="server" TextMode="Date" Text="" Enabled="false" ClientIDMode="Static"></asp:TextBox>
<%--                                                <asp:Label ID="tb_fecNac" runat="server" Text=""></asp:Label>--%>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell><asp:Label ID="lb_sexo" runat="server" Text="Sexo:"></asp:Label></asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RadioButtonList ID="rb_sexo" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rb_sexo_SelectedIndexChanged" Enabled="false">
                                                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </asp:TableCell> 
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_civil" runat="server" Text="Edo. Civil:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_civil" runat="server" SelectCommand="select id_edo_civil, desc_esp from GCDM_rh.dbo.civil where activo='1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_civil" runat="server" DataSourceID="data_civil" DataTextField="desc_esp" DataValueField="id_edo_civil"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_noNinos" runat="server" Text="No. Niños:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_noNinos" runat="server" TextMode="Number" min="0"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_paisNac" runat="server" Text="Pais Nac.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_paisNac" runat="server" DataSourceID="data_paisDir" DataTextField="desc_esp" DataValueField="id_pais" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="drop_paisNac_SelectedIndexChanged">
                                                    <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_edoNac" runat="server" Text="Estado Nac.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_edoNac" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drop_edoNac_SelectedIndexChanged"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_cdNac" runat="server" Text="Ciudad Nac.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="drop_cdNac" runat="server"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_carrera" runat="server" Text="Carrera:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_carrera" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_ingles" runat="server" Text="Ingles:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_ingles" runat="server" TextMode="Number" min="0" max="100"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_escolaridad" runat="server" Text="Escolaridad:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_escolaridad" runat="server" SelectCommand="select id_escolaridad, desc_esp from GCDM_rh.dbo.escolaridad" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_escolaridad" runat="server" DataSourceID="data_escolaridad" DataTextField="desc_esp" DataValueField="id_escolaridad" AppendDataBoundItems="true">
                                                    <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="Label6" runat="server" Text="Documento:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="escolaridad_documento" runat="server" SelectCommand="select * from GCDM_RH.dbo.escolaridad_documentos order by id_escolaridad_documento desc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_escolaridad_documento" runat="server" DataSourceID="escolaridad_documento" DataTextField="descripcion" DataValueField="id_escolaridad_documento"></asp:DropDownList>
                                            </asp:TableCell>
                        
                                        </asp:TableRow>
                                        <asp:TableRow>
                                              <asp:TableCell>
                                                <asp:Label ID="Label7" runat="server" Text="Escolaridad Ins.:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="escolaridad_institucion" runat="server" SelectCommand="select id_escolaridad_instituciones as id, descripcion from GCDM_RH.dbo.escolaridad_instituciones order by id_escolaridad_instituciones asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_escolaridad_institucion" runat="server" DataSourceID="escolaridad_institucion" DataTextField="descripcion" DataValueField="id"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_nomPat" runat="server" Text="Nom. padre:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_nomPat" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_nomMat" runat="server" Text="Nom. madre:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_nomMat" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label  runat="server" Text="Contacto de</br>emergencia:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_contacto" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label runat="server" Text="Teléfono:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_contacto_telefono" runat="server" Text=""></asp:TextBox> 
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <!------------------------------------------------------ tabla de datos secundarios ------------------------------------------------------>
                                    <asp:Table ID="tabla_editar_secundarios" ClientIDMode="Static" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="3">
                                                <asp:Label ID="secundarios" runat="server" Text="Datos Secundarios" CssClass="editar_subTitulo_secundarios"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_servicio" runat="server" Text="Servicio:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:SqlDataSource ID="data_servicioTipo" runat="server" SelectCommand="select id_servicio, descripcion from GCDM_rh.dbo.servicios" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                                <asp:DropDownList ID="drop_servicioTipo" runat="server" DataSourceID="data_servicioTipo" DataTextField="descripcion" DataValueField="id_servicio"></asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_servNo" runat="server" Text="Número"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_servNo" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_servNoVig" runat="server" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>                        
<%--                                        <asp:TableRow>
                                            <asp:TableCell><asp:Label ID="lb_imss" runat="server" Text="IMSS"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:TextBox ID="tb_imss" runat="server" Text=""></asp:TextBox></asp:TableCell>
                                        </asp:TableRow>--%>
                                        <asp:TableRow Visible="false">
                                            <asp:TableCell>
                                                <asp:Label ID="lb_pasaporte" runat="server" Text="Pasaporte:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_pasaporte" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_pasaporteVig" runat="server" Text="" TextMode="Date">
                                                </asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_visa" runat="server" Text="Visa:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_visa" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_visaVig" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_licencia" runat="server" Text="Licencia"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_licencia" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_licenciaVig" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                                 <div id="dvScroll" style="overflow-y: scroll; height: 28px; width: 360px;">
                                                     <asp:CheckBoxList ID="checkBox_tipo_licencia" runat="server" Width="100%" Style="margin:auto"  AppendDataBoundItems="true" RepeatColumns="5"></asp:CheckBoxList>
                                                  </div>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl_constancia" runat="server" Text="Folio Constancia:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_constancia" runat="server" Text="" ></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell >
                                                <asp:TextBox ID="tb_constancia_inicio" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                                <br />                              
                                                <asp:TextBox ID="tb_constancia_fin" runat="server" Text="" TextMode="Date" ></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_fast" runat="server" Text="Fast:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_fast" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_fastVig" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_penal" runat="server" Text="Penal:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_penal" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_penalVig" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lb_gafete_unico" runat="server" Text="Gafete Unico:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_gafete_unico" runat="server" Text=""></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_gafete_unicoVig" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="1">
                                                <asp:Label ID="lb_policial" runat="server" Text="Policial USA:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="1" >
                                                <asp:TextBox ID="tb_policialVig" runat="server" Text="" TextMode="Date">
                                                </asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl_Apto" runat="server" Text="Apto:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="tb_apto" runat="server" Text="" ></asp:TextBox>
                                            </asp:TableCell>
                                            <asp:TableCell >
                                                <asp:TextBox ID="tb_apto_inicio" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                                <br />                             
                                                <asp:TextBox ID="tb_apto_vigencia" runat="server" Text="" TextMode="Date" ></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                                <asp:TableCell CssClass="celda_final">
                                    <!------------------------------------------------------ tabla prestaciones ------------------------------------------------------>
                                    <asp:Table ID="tabla_editar_prestaciones" ClientIDMode="Static" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="3">
                                                <asp:Label ID="Label1" runat="server" Text="Prestaciones" CssClass="editar_subTitulo_prestaciones"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblIMSS" runat="server" Text="IMSS:"></asp:Label></asp:TableCell>
                                            <asp:TableCell>
<%--                                                <asp:TextBox ID="txtIMSS" ClientIDMode="Static" runat="server" placeholder="No. IMSS" Enabled="false"></asp:TextBox>--%>
                                                <asp:Label ID="txtIMSS" runat="server" Text=""></asp:Label>
<%--                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderIMSS" runat="server" FilterType="Numbers" TargetControlID="txtIMSS" />--%>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2"><hr /></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblINFONAVIT" runat="server" Text="INFONAVIT:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:CheckBox ID="chkINFONAVIT" runat="server" AutoPostBack="true" OnCheckedChanged="chkINFONAVIT_CheckedChanged" OnPreRender="chkINFONAVIT_CheckedChanged" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblNoCredito" runat="server" Text="No. Crédito" Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtNoCredito" runat="server" Visible="false" placeholder="No. crédito"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderNoInfonavit" runat="server" FilterType="Numbers" TargetControlID="txtNoCredito" />
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RequiredFieldValidator ID="RFVNoCredito" runat="server" ControlToValidate="txtNoCredito" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="No. crédito en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RFVTipoInfonavit" runat="server" ControlToValidate="ddlINFONAVIT" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Seleccione tipo" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RFVFactorInfonavit" runat="server" ControlToValidate="txtFactorINFONAVIT" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Factor en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>                             
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblTipoDescuento" runat="server" Text="Tipo descuento:" Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlINFONAVIT" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlINFONAVIT_SelectedIndexChanged">
                                                <asp:ListItem Text="-- Seleccione tipo --" Selected="True" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Porcentaje aplicable al SBC" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Cuota fija monetaria" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Factor VSM" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblFactorINFONAVIT" runat="server" Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtFactorINFONAVIT" runat="server" Visible="false" ></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderFactorInfonavit" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtFactorINFONAVIT" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="8"><hr /></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblFONACOT" runat="server" Text="FONACOT:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:CheckBox ID="chkFONACOT" runat="server" AutoPostBack="true" OnCheckedChanged="chkFONACOT_CheckedChanged" OnPreRender="chkFONACOT_CheckedChanged" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblNoFONACOT" runat="server" Text="No. Crédito: " Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtNoFONACOT" runat="server" Visible="false"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderNoFonacot" runat="server" FilterType="Numbers" TargetControlID="txtNoFONACOT" />
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RequiredFieldValidator ID="RFVNoFonacot" runat="server" ControlToValidate="txtNoFONACOT" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Número en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RFVRetencionFonacot" runat="server" ControlToValidate="txtRetencionFONACOT" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Retención en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RFVTotalFonacot" runat="server" ControlToValidate="txtTotalFONACOT" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Total en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RFVFechaFonacot" runat="server" ControlToValidate="txtFechaFONACOT" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Fecha en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblRetencionFONACOT" runat="server" Text="Retención mensual ($): " Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtRetencionFONACOT" runat="server" Visible="false"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderRetencionFonacot" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtRetencionFONACOT" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblTotalFONACOT" runat="server" Text="Total crédito ($): " Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtTotalFONACOT" runat="server" Visible="false"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTotalFonacot" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTotalFONACOT" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblFechaFONACOT" runat="server" Text="Inicio de crédito: " Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtFechaFONACOT" runat="server" Visible="false" TextMode="Date"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="8"><hr /></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblPension" runat="server" Text="Pensión alimenticia:"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:CheckBox ID="chkPension" runat="server" AutoPostBack="true" OnCheckedChanged="chkPension_CheckedChanged" OnPreRender="chkPension_CheckedChanged" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lblNoPension" runat="server" Text="Porcentaje a retener: (%)" Visible="false"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtPension" runat="server" Visible="false"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPension" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtPension" />
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:RequiredFieldValidator ID="RFVPension" runat="server" ControlToValidate="txtPension" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Pensión en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                    <br /><br />
                                    <asp:Table ID="tabla_editar_baja" runat="server" Visible="false" CssClass="tabla_editar_baja">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2">
                                                <asp:Label runat="server" Text="Datos Baja" Font-Bold="true"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                Motivo Baja:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl_baja_motivo" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                               Reecontratable:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl_baja_recontratable" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                               Fecha Baja:
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lbl_baja_fecha" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2">
                                               Comentarios:
                                                <br />
                                                <asp:Label ID="lbl_baja_comentarios" runat="server" Text=""></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                       </asp:Table>

                        <!------------------------------------------------------ tabla botones GUARDAR y CANCELAR ------------------------------------------------------>
                        <asp:Table ID="tabla_editar_guardarCancelar" ClientIDMode="Static" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>
                                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn_cancelar_guardar" Visible="false" OnClick="btn_guardar_Click" ValidationGroup="Validar" />
<%--                                    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="btn_cancelar_guardar" Visible="false" OnClientClick="popup('popUp_editar_guardar'); return false" OnClick="btn_guardar_Click" />--%>
                                    <asp:Button ID="btn_reactivar" runat="server" Text="Reactivar" CssClass="btn_cancelar_guardar" Visible="false" OnClientClick="popup('popUp_editar_reactivar'); return false" />
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" CssClass="btn_cancelar_guardar" OnClientClick="popup('popUp_editar_cancelar'); return false" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <asp:Label ID="lb_mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                        </div>
                        <%---------------- POPUP BLANKET ----------------%>
                        <div id="blanket" style="display:none">
                        </div>

                        <%---------------- POPUP GUARDAR ----------------%>

                        <asp:Button ID="BtnActivar" runat="server" Text="Button" Style="display:none"  />

                                        <ajaxToolkit:ModalPopupExtender TargetControlID="BtnActivar" ID="popUp_editar" runat="server" PopupControlID="popUp_editar_guardar" BackgroundCssClass="modalBackground" CancelControlID="btn_cerrarPopUp_guardar_no">
                                            <Animations>
                                                    <OnShown>
                                                        <FadeIn duration="1.30" Fps="100" />
                                                    </OnShown>
                                            </Animations>
                                        </ajaxToolkit:ModalPopupExtender>

        
                                <div id="popUp_editar_guardar" style="display:none">

                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lb_cerrarPopUp_guardar" ClientIDMode="Static" runat="server" Text="¿Está seguro que desea GUARDAR los cambios?"></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_alerta_editar" runat="server" Text="" Font-Bold="true" ForeColor="#ff0000"></asp:Label>
                                        <br />
                                        <asp:Button ID="btn_cerrarPopUp_guardar_si" Visible="false" runat="server" Text="Guardar" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_cerrarPopUp_guardar_si_Click" />
                                        <asp:Button ID="btn_cerrarPopUp_guardar_no" runat="server" Text="Regresar" CssClass="btn_cerrarPopUp_cancelarGuardar"/>
                
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>


<%--                        <div id="popUp_editar_guardar" style="display:none">
                            <asp:Label ID="lb_cerrarPopUp_guardar" ClientIDMode="Static" runat="server" Text="¿Está seguro que desea GUARDAR los cambios?"></asp:Label>
                            <br />
                            <asp:Label ID="lbl_alerta_editar" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btn_cerrarPopUp_guardar_si" Visible="false" runat="server" Text="Si" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_cerrarPopUp_guardar_si_Click" />
                            <asp:Button ID="btn_cerrarPopUp_guardar_no" runat="server" Text="No" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClientClick="popup('popUp_editar_guardar'); return false" />
                        </div>--%>

                        <%---------------- POPUP REACTIVAR ----------------%>
                        <div id="popUp_editar_reactivar" style="display:none">
                            <asp:Label ID="lb_cerrarPopUp_reactivar" ClientIDMode="Static" runat="server" Text="¿Está seguro que desea reactivar el empleado?"></asp:Label>

                            <asp:Button ID="btn_cerrarPopUp_reactivar_si" runat="server" Text="Si" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_cerrarPopUp_reactivar_si_Click" />
                            <asp:Button ID="btn_cerrarPopUp_reactivar_no" runat="server" Text="No" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClientClick="popup('popUp_editar_reactivar'); return false" />
                        </div>

                        <%---------------- POPUP CANCELAR ----------------%>
                        <div id="popUp_editar_cancelar" style="display:none">
                            <asp:Label ID="lb_cerrarPopUp_cancelar" ClientIDMode="Static" runat="server" Text="¿Está seguro que desea CANCELAR los cambios?"></asp:Label>
                           
                            <asp:Button ID="btn_cerrarPopUp_cancelar_si" runat="server" Text="Si" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClick="btn_cerrarPopUp_cancelar_si_Click" />
                            <asp:Button ID="btn_cerrarPopUp_cancelar_no" runat="server" Text="No" CssClass="btn_cerrarPopUp_cancelarGuardar" OnClientClick="popup('popUp_editar_cancelar'); return false" />
                        </div>

                    </asp:View>

                    <asp:View ID="View2" runat="server">

                        <asp:Label ID="titulo_confirmacion" ClientIDMode="Static" runat="server" Text=""></asp:Label>

                        <asp:Button ID="btn_confirmacion" ClientIDMode="Static" runat="server" Text="Continuar" OnClick="btn_confirmacion_Click" />

                    </asp:View>
                </asp:MultiView>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>