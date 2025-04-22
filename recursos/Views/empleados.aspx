<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="empleados.aspx.cs" Inherits="recursos.Views.empleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

    function IsOneDecimalPoint(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode; // restrict user to type only one . point in number
                    var parts = evt.srcElement.value.split('.');
                    if(parts.length > 1 && charCode==46)
                        return false;
                    return true;
                }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
   
    <div id="contenedor" >

        <%--<asp:TextBox ID="datepicker4" CssClass="datepicker" ClientIDMode="Static" runat="server" placeholder="Seleccionar fecha.."></asp:TextBox>--%>
        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="ca" runat="server" />--%>
        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="ca" runat="server" OnClientShown="ChangeCalendarView" />--%>

        <asp:Label runat="server" ID="nombreUsuario" Text="" ClientIDMode="Static"></asp:Label>

        <div id="blanket" style="display:none">
        </div>

        <%---------------- POPUP agregar empleados ----------------%>
        <div id="popUp_agregar_empleados" style="display:none">

            <a href="#" id="cerrar_agregar" class="cerrar_PopUp" onclick="popup('popUp_agregar_empleados')"></a>
            
            <asp:UpdatePanel ID="UpdatePanelAgregar" runat="server">
                <ContentTemplate>

                    <br />
                    
                    <asp:ImageButton ID="guardar_empleado" ClientIDMode="Static" runat="server" ImageUrl="~/images/guardar_green.png" OnMouseOver="src='/images/guardar_red.png';" OnMouseOut="src='/images/guardar_green.png';" CssClass="btn_guardar" ValidationGroup="UP_agregar"  OnClick="ImageButton1_Click1"/>

                    <asp:Button ID="tab1" Text="Personales" runat="server" OnClick="tab1_Click" CssClass="clicked" ValidationGroup="UP_agregar" />
                    <asp:Button ID="tab2" Text="Secundarios" runat="server" OnClick="tab2_Click" CssClass="initial" ValidationGroup="UP_agregar" />                 
                    <asp:Button ID="tab3" Text="Prestaciones" runat="server" OnClick="tab3_Click" CssClass="initial" ValidationGroup="UP_agregar" />
                    <asp:Button ID="tab4" Text="Laborales" runat="server" OnClick="tab4_Click" CssClass="initial" ValidationGroup="UP_agregar" />

                    <asp:MultiView ID="MultiView" runat="server">
                        <asp:View ID="View1" runat="server">
                            <asp:Label ID="lb1" runat="server" Text="Datos Personales - Principales" CssClass="titulo_agregar"></asp:Label>
                            
                            <asp:Table runat="server" ID="tb_add_emp" ClientIDMode="Static">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_nombre" runat="server" Text="Nombre(s):"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_nombre" ClientIDMode="Static" runat="server" placeholder="Nombres" ValidationGroup="UP_agregar"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_apepat" runat="server" Text="Apellido Paterno:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_apepat" runat="server" placeholder="Apellido Paterno" ValidationGroup="UP_agregar"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_apemat" runat="server" Text="Apellido Materno:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_apemat" runat="server" placeholder="Apellido Materno" ValidationGroup="UP_agregar"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_fecnac" runat="server" Text="Fecha de Nacimiento:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_fecnac" TextMode="Date" runat="server" Text="" ValidationGroup="UP_agregar"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_sexo" runat="server" Text="Sexo:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RadioButtonList ID="SexoList" runat="server" ValidationGroup="UP_agregar" AutoPostBack="false" OnSelectedIndexChanged="SexoList_SelectedIndexChanged">
                                            <asp:ListItem Text="Masculino" Value="M" />
                                            <asp:ListItem Text="Femenino" Value="F" />
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_civil" runat="server" Text="Estado civil:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="civil_data" runat="server" SelectCommand="select id_edo_civil, desc_esp from GCDM_rh.dbo.civil" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_civil" runat="server" DataSourceID="civil_data" DataTextField="desc_esp" DataValueField="id_edo_civil"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_tieneNinos" runat="server" Text="¿Tiene hijos?:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RadioButtonList ID="NinosList" ClientIDMode="Static" runat="server" AutoPostBack="false">
                                            <asp:ListItem Text="Si" Value="si"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="no"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_paisNac" runat="server" Text="País de nacimiento:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="paisNac_data" runat="server" SelectCommand="select id_pais, desc_esp from GCDM.dbo.paises" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_paisNac" runat="server" DataSourceID="paisNac_data" DataTextField="desc_esp" DataValueField="id_pais" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="Drop_paisNac_SelectedIndexChanged">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_NoNinos" runat="server" Text="No. Niños"></asp:Label></asp:TableCell>
                                     <asp:TableCell>
                                        <asp:DropDownList ID="Drop_NoNinos" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_edoNac" runat="server" Text="Estado de nacimiento:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="edoNac_data" runat="server" SelectCommand="select id_estado, desc_esp from GCDM.dbo.estados" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_edoNac" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="Drop_edoNac_SelectedIndexChanged"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_cdNac" runat="server" Text="Ciudad de nacimiento:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="cdNac_data" runat="server" SelectCommand="select id_ciudad, descripcion from GCDM.dbo.ciudades" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_cdNac" runat="server" Enabled="false"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_nompat" runat="server" Text="Nombre del padre:"></asp:Label></asp:TableCell>
                                    <asp:TableCell ColumnSpan="30"><asp:TextBox ID="tb_nompat" ClientIDMode="Static" runat="server" Text="" placeholder="Nombre del padre"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_nommat" runat="server" Text="Nombre de la madre"></asp:Label></asp:TableCell>
                                    <asp:TableCell ColumnSpan="3"><asp:TextBox ID="tb_nommat" ClientIDMode="Static" runat="server" Text="" placeholder="Nombre de la madre"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <asp:RegularExpressionValidator ID="RV_nombre" runat="server" ControlToValidate="tb_nombre" CssClass="RegularValidator" Display="Dynamic" ValidationExpression="^[A-Za-z ñÑáéíóúÁÉÍÓÚ\s]*$" ErrorMessage="Nombre" ValidationGroup="UP_agregar"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RV_apepat" runat="server" ControlToValidate="tb_apepat" CssClass="RegularValidator" Display="Dynamic" ValidationExpression="^[A-Za-z ñÑáéíóúÁÉÍÓÚ\s]*$" ErrorMessage=" *Apellido paterno " ValidationGroup="UP_agregar"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RV_apemat" runat="server" ControlToValidate="tb_apemat" CssClass="RegularValidator" Display="Dynamic" ValidationExpression="^[A-Za-z ñÑáéíóúÁÉÍÓÚ\s]*$" ErrorMessage=" *Apellido materno "  ValidationGroup="UP_agregar"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RV_nompat" runat="server" ControlToValidate="tb_nompat" CssClass="RegularValidator" Display="Dynamic" ValidationExpression="^[A-Za-z ñÑáéíóúÁÉÍÓÚ\s]*$" ErrorMessage=" *Nombre del padre " ValidationGroup="UP_agregar"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="RV_nommat" runat="server" ControlToValidate="tb_nommat" CssClass="RegularValidator" Display="Dynamic" ValidationExpression="^[A-Za-z ñÑáéíóúÁÉÍÓÚ\s]*$" ErrorMessage=" *Nombre de la madre " ValidationGroup="UP_agregar"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="NoNull_nombre" runat="server" ControlToValidate="tb_nombre" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" *Nombre en blanco " ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="NoNull_apepat" runat="server" ControlToValidate="tb_apepat" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" *Apellido en blanco " ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="NoNull_apemat" runat="server" ControlToValidate="tb_apemat" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" *Apellido en blanco " ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="NoNull_fecnac" runat="server" ControlToValidate="tb_fecnac" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" *Fecha nacimiento en blanco " ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="NoNull_sexo" runat="server" ControlToValidate="SexoList" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" *Sexo en blanco " ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                        </asp:View>

                        <asp:View ID="View2" runat="server">
                            <asp:Label ID="lb2" runat="server" Text="Datos Personales - Secundarios" CssClass="titulo_agregar"></asp:Label>

                            <asp:Table ID="tb_add_emp2" ClientIDMode="Static" runat="server"   Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_zona" runat="server" Text="Zona:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:SqlDataSource ID="zona_data" runat="server" SelectCommand="select id_zona_economica, zona_economica from GCDM_rh.dbo.zonas" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_zona" runat="server" DataSourceID="zona_data" DataValueField="id_zona_economica" DataTextField="zona_economica"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                         <asp:RegularExpressionValidator ID="RegExp1" runat="server" CssClass="RegularValidator" ErrorMessage="Debe ser en formato +52(656)123-4567" ControlToValidate="tb_telefono" ValidationExpression="\d{15,16}$" />  
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" 
                                        runat="server" TargetControlID="tb_telefono"
                                        Mask="+99(999)999-9999"
                                        MaskType="None"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="editmask"
                                        OnInvalidCssClass="invalidmask"
                                        InputDirection="LeftToRight"
                                        ClearMaskOnLostFocus="false"
                                        AutoComplete="false"/>                                    
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_telefono" runat="server" Text="Teléfono:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell >
                                        <asp:TextBox ID="tb_telefono" runat="server" Text="" placeholder="Teléfono" ></asp:TextBox>
                                        <br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Label ID="lb_correo" runat="server" Text="Correo electrónico:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:TextBox ID="tb_correo" ClientIDMode="Static" runat="server" Text="" placeholder="Correo electrónico"></asp:TextBox>
                                    </asp:TableCell>
                                   
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_escolaridad" runat="server" Text="Escolaridad:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="escolaridad_data" runat="server" SelectCommand="select id_escolaridad, desc_esp from GCDM_rh.dbo.escolaridad order by id_escolaridad asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_escolaridad" runat="server" DataSourceID="escolaridad_data" DataTextField="desc_esp" DataValueField="id_escolaridad"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="Label6" runat="server" Text="Documento:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="escolaridad_documento" runat="server" SelectCommand="select * from GCDM_RH.dbo.escolaridad_documentos order by id_escolaridad_documento desc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="drop_escolaridad_documento" runat="server" DataSourceID="escolaridad_documento" DataTextField="descripcion" DataValueField="id_escolaridad_documento"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="Label7" runat="server" Text="Escolaridad Institución:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="escolaridad_institucion" runat="server" SelectCommand="select id_escolaridad_instituciones as id, descripcion from GCDM_RH.dbo.escolaridad_instituciones order by id_escolaridad_instituciones asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="drop_escolaridad_institucion" runat="server" DataSourceID="escolaridad_institucion" DataTextField="descripcion" DataValueField="id"></asp:DropDownList>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell >
                                        <asp:Label ID="lb_carrera" runat="server" Text="Carrera:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan ="3">
                                        <asp:TextBox ID="tb_carrera" runat="server" Text="" placeholder="Carrera" Width="100%"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell >
                                        <asp:Label ID="lb_ingles" runat="server" Text="Ingles(%):"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell >
                                        <asp:TextBox ID="tb_ingles" ClientIDMode="Static" runat="server" Text="" MaxLength="3" TextMode="Number" min="0" max="100"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_paisDir" runat="server" Text="Pais:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_paisDir" runat="server" DataSourceID="paisNac_data" DataTextField="desc_esp" DataValueField="id_pais" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="Drop_paisDir_SelectedIndexChanged">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_edoDir" runat="server" Text="Estado:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_edoDir" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="Drop_edoDir_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_cdDir" runat="server" Text="Ciudad:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_cdDir" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="Drop_cdDir_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_codpostDir" runat="server" ClientIDMode="Static" Text="Código<br />Postal:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_codpostDir" ClientIDMode="Static" runat="server" placeholder="#" Visible="false"></asp:TextBox>
                                          <asp:DropDownList ID="Drop_codPostDir" runat="server"  Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="Drop_codPostDir_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        </asp:DropDownList>
<%--                                        <ajaxToolkit:MaskedEditExtender ID="mask_codPos" runat="server" TargetControlID="tb_codpostDir" Mask="99999" />--%>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_colonia" runat="server" Text="Colonia:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_colonia" ClientIDMode="Static" runat="server" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="Drop_colonia" runat="server" AutoPostBack="true"  DataTextField="descripcion" DataValueField="id_colonia" Enabled="false" OnSelectedIndexChanged="Drop_colonia_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_calle" runat="server" Text="Calle:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                          <asp:TextBox ID="tb_calle" ClientIDMode="Static" runat="server"  Visible="false" Width="100px"></asp:TextBox>
                                          <asp:DropDownList ID="Drop_calleDir" runat="server"  Enabled="false" AutoPostBack="true" style="float: left" AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Text="-- Seleccionar --" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        -
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_numDir" ClientIDMode="Static" runat="server" Text="" placeholder="#"></asp:TextBox>  
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_numInt" runat="server" Text="# Interior:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_numInt" placeholder="#" runat="server" Width="80px"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell >
                                        <asp:Label ID="lb_curp" runat="server" Text="Curp:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_curp" ClientIDMode="Static" runat="server" Text="" placeholder="Curp" MaxLength="18" ValidationGroup="UP_agregar"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_rfc" runat="server" Text="RFC:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_rfc" ClientIDMode="Static" runat="server" Text="" placeholder="RFC" MaxLength="13" Width="130"></asp:TextBox>                                        
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Label ID="lb_tallaC" runat="server" Text="Camisa:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="Data_tallaC" runat="server" SelectCommand="select id_tallaC, desc_esp from GCDM_rh.dbo.tallaC where activo = '1'" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_tallaC" runat="server" DataSourceID="data_tallaC" DataValueField="id_tallaC" DataTextField="desc_esp" AppendDataBoundItems="true">
                                            <asp:ListItem Value="" Text="-- Seleccionar --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_tallaP" runat="server" Text="Pantalón:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_tallaP" runat="server"></asp:DropDownList>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3">
                                        <asp:Label runat="server" Text="Contacto de emergencia:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_contacto_nombre" runat="server" placeholder="Nombre"></asp:TextBox>
                                    </asp:TableCell>

                                    <asp:TableCell>
                                        <asp:Label ID="Label3" runat="server" Text="Teléfono:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_contacto_telefono" runat="server" placeholder="Teléfono"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCalle" runat="server" InitialValue="" ControlToValidate="Drop_calleDir" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Calle en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatornumDir" runat="server" ControlToValidate="tb_numDir" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="# Casa en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RequiredFieldValidator ID="NoNull_curp" runat="server" ControlToValidate="tb_curp" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="CURP en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                        </asp:View>

                        <asp:View ID="View3" runat="server">
                            <asp:Label ID="Label1" runat="server" Text="Prestaciones laborales" CssClass="titulo_agregar"></asp:Label>

                            <asp:Table ID="tb_add_emp03" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lblIMSS" runat="server" Text="IMSS:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_imss" ClientIDMode="Static" runat="server" placeholder="No. IMSS"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderIMSS" runat="server" FilterType="Numbers" TargetControlID="tb_imss" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="RFVIMSS" runat="server" ControlToValidate="tb_imss" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="IMSS en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow>
                                <asp:TableCell ColumnSpan="8"><hr /></asp:TableCell>
                                    </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lblINFONAVIT" runat="server" Text="INFONAVIT:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:CheckBox ID="chkINFONAVIT" runat="server" OnCheckedChanged="chkINFONAVIT_CheckedChanged" AutoPostBack="true" /></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblNoCredito" runat="server" Text="No. Crédito" Visible="false"></asp:Label></asp:TableCell>
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
                                    <asp:TableCell><asp:Label ID="lblTipoDescuento" runat="server" Text="Tipo descuento:" Visible="false"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlINFONAVIT" runat="server" Visible="false" OnSelectedIndexChanged="ddlINFONAVIT_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="-- Seleccione tipo --" Selected="True" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Porcentaje aplicable al SBC" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Cuota fija monetaria" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Factor VSM" Value="3"></asp:ListItem>
                                        </asp:DropDownList></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblFactorINFONAVIT" runat="server" Visible="false"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtFactorINFONAVIT" runat="server" Visible="false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderFactorInfonavit" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtFactorINFONAVIT" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="8"><hr /></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lblFONACOT" runat="server" Text="FONACOT:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:CheckBox ID="chkFONACOT" runat="server" AutoPostBack="true" OnCheckedChanged="chkFONACOT_CheckedChanged" /></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblNoFONACOT" runat="server" Text="No. Crédito: " Visible="false"></asp:Label></asp:TableCell>
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
                                    <asp:TableCell><asp:Label ID="lblRetencionFONACOT" runat="server" Text="Retención mensual ($): " Visible="false"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtRetencionFONACOT" runat="server" Visible="false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderRetencionFonacot" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtRetencionFONACOT" />
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblTotalFONACOT" runat="server" Text="Total crédito ($): " Visible="false"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtTotalFONACOT" runat="server" Visible="false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTotalFonacot" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtTotalFONACOT" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2"><asp:Label ID="lblFechaFONACOT" runat="server" Text="Inicio de crédito: " Visible="false"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="txtFechaFONACOT" runat="server" Visible="false" TextMode="Date"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="8"><hr /></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lblPension" runat="server" Text="Pensión alimenticia:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:CheckBox ID="chkPension" runat="server" AutoPostBack="true" OnCheckedChanged="chkPension_CheckedChanged" /></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lblNoPension" runat="server" Text="Porcentaje a retener: (%)" Visible="false"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtPension" runat="server" Visible="false"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPension" runat="server" FilterType="Numbers, Custom" ValidChars="." TargetControlID="txtPension" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="RFVPension" runat="server" ControlToValidate="txtPension" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Pensión en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:View>

                        <asp:View ID="View4" runat="server">
                            <asp:Label ID="lb02" runat="server" Text="Datos Laborales" CssClass="titulo_agregar"></asp:Label>

                            <asp:Table ID="tb_add_emp02" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_depa" runat="server" Text="Departamento:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="depto_data" runat="server" SelectCommand="select id_depto, desc_esp from GCDM_rh.dbo.departamento" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_depto" runat="server" DataSourceID="depto_data" DataTextField="desc_esp" DataValueField="id_depto" OnSelectedIndexChanged="Drop_depto_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" ValidationGroup="UP_agregar">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_puesto" runat="server" Text="Puesto:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_puesto" runat="server" OnSelectedIndexChanged="Drop_puesto_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" Enabled="false"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Salario:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_salario" runat="server" Text="" Enabled="false" onkeypress="return IsOneDecimalPoint(event);"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"  ValidChars="01234567890." TargetControlID="tb_salario" />
                                        
                                        <asp:HiddenField ID="hidden_salario" runat="server" Value="" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Visible="true">
                                    <asp:TableCell>
                                        <asp:Label ID="lb_cliente" runat="server" Text="Cliente:" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_cliente" runat="server" OnSelectedIndexChanged="Drop_cliente_SelectedIndexChanged" AutoPostBack="true" Enabled="true" Visible="false">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lb_puestoCliente" runat="server" Text="Tractor:" Visible="false"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_puestoCliente" runat="server" Enabled="false" Visible="false">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Center" runat="server">
                                        <asp:SqlDataSource ID="SQLDS_Bonos" runat="server" SelectCommand="select id_aportacion_deduccion_concepto, descripcion from GCDM_CONTABILIDAD.dbo.aportaciones_deducciones_conceptos where activo = '1' and id_aportacion_deduccion = '3' and descripcion = 'Bono de jefe de ruta' or descripcion = 'Bono de jefe de patio' or descripcion = 'Bono de chofer maestro' or descripcion = 'Bono de chofer extra' or descripcion =  'Bono chofer Instructor' " ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:CheckBoxList ID="chklBonos" RepeatDirection="Horizontal" ClientIDMode="Static" runat="server" DataSourceID="SQLDS_Bonos" DataTextField="descripcion" DataValueField="id_aportacion_deduccion_concepto" Visible="false"></asp:CheckBoxList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="6">
                                        <div class="linea_Div"></div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_servicio" runat="server" Text="Servicio:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="servicio_data" runat="server" SelectCommand="select id_servicio, descripcion from GCDM_rh.dbo.servicios" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="Drop_servicio" runat="server" DataSourceID="servicio_data" DataTextField="descripcion" DataValueField="id_servicio"></asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_NumServicio" runat="server" Text="Núm.:"></asp:Label></asp:TableCell>
                                     <asp:TableCell><asp:TextBox ID="tb_NumServicio" runat="server" Text="" placeholder="No. Servicio"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_ServicioVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="date_ServicioVig" runat="server" TextMode="Date" ></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_penal" runat="server" Text="Penal:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_penal" runat="server" Text="" placeholder="Número"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_penalVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_penalVig" runat="server" TextMode="Date" ></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_licencia" runat="server" Text="Licencia:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_licencia" runat="server" Text="" placeholder="Número"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_licenciaVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_licenciaVig" runat="server" TextMode="Date"></asp:TextBox></asp:TableCell>
                                </asP:TableRow>

                                <asp:TableRow runat="server" Visible="true">
                                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Center">
                                        <div id="dvScroll" style="overflow-y: scroll; height: 30px; width: 550px;">
                                            <asp:CheckBoxList ID="checkBox_tipo_licencia" runat="server" Width="100%" Style="margin:auto"  AppendDataBoundItems="true" RepeatColumns="5"></asp:CheckBoxList>
                                         </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" Visible="true">
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_constancia" runat="server" Text="Folio Constancia:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="tb_constancia" runat="server" Text="" placeholder="Folio"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="Label2" runat="server" Text="Inicio:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell >
                                        <asp:TextBox ID="tb_constancia_inicio" runat="server" Text="" TextMode="Date"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="Label4" runat="server" Text="Vig.:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell >
                                        <asp:TextBox ID="tb_constancia_fin" runat="server" Text="" TextMode="Date" ></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_apto" runat="server" Text="Apto:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_apto" runat="server" Text="" placeholder="Número"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="Label5" runat="server" Text="Inicio:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_aptoInicio" runat="server" TextMode="Date"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_aptoVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_aptoVig" runat="server" TextMode="Date" ></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_pasaporte" runat="server" Text="Pasaporte:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_pasaporte" runat="server" Text="" placeholder="Pasaporte"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_pasaporteVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_pasaporteVig" runat="server" TextMode="Date" ></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_visa" runat="server" Text="Visa:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_visa" runat="server" Text="" placeholder="Número:"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_visaVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_visaVig" runat="server" TextMode="Date" ></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_fast" runat="server" Text="Fast:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_fast" runat="server" Text="" placeholder="Número"></asp:TextBox></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_fastVig" runat="server" Text="Vig.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_fastVig" runat="server" TextMode="Date" ></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2"><asp:Label ID="lb_policialVig" runat="server" Text="Carta Policial USA:"></asp:Label></asp:TableCell>
                                    <asp:TableCell ColumnSpan="2"><asp:TextBox ID="tb_policialVig" runat="server" TextMode="Date"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_supervisor" runat="server" Text="Supervisor:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_supervisor" runat="server">
                                            <asp:ListItem Text="--Seleccionar--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_contrato" runat="server" Text="Contrato:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="Drop_contrato" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="-- Seleccionar --" Selected="True" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Permanente" Value="Permanente"></asp:ListItem>
                                            <asp:ListItem Text="Temporal" Value="Temporal"></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:RequiredFieldValidator ID="NoNull_depto" runat="server" ControlToValidate="Drop_depto" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Departamento en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="NoNull_puesto" runat="server" ControlToValidate="Drop_puesto" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Puesto en blanco" ValidationGroup="UP_agregar"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:View>

                        <asp:View ID="View5" runat="server">
                            <asp:Label ID="lb4" runat="server" Text="" CssClass="empleado_exito"></asp:Label>
                            <asp:Button ID="ContinuarAdd_empleado" runat="server" Text="Continuar" CssClass="empleado_exito_btn" OnClick="ContinuarAdd_empleado_Click" />
                        </asp:View>
                    </asp:MultiView>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <%---------------- End ----------------%>

         <%---------------- POPUP alta empleados ----------------%>
        <asp:Button ID="Button4" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="Button4" ID="modalpop_Alta" runat="server" PopupControlID="popUp_alta_empleados" BackgroundCssClass="modalBackground" CancelControlID="btn_close_modal_agregar" >
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>


        <div id="popUp_alta_empleados" style="display:none" class="popUp_alta_empleados">   
            
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>

                    <asp:Panel runat="server" ID="pnlSelect" >
                        <br />
                        <asp:Label  runat="server" Text="¿Agregar al empleado?" Font-Bold="true" Font-Size="Large"></asp:Label>
                        <br /><br />

                        <asp:Table ID="tabla_datos" runat="server" HorizontalAlign="Center">
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="Nombre: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_nombre"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="Apellido Paterno: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_apellido_paterno"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="Apellido Materno: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_apellido_materno"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="Fecha Nacimiento: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_fecha_nacimiento"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="CURP: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_curp"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="RFC: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_rfc"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="IMSS: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_imss"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="Departamento: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_departamento"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    <asp:Label runat="server" Text="Puesto: " Font-Bold="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label runat="server"  ID="lbl_puesto"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <br />
                        <asp:Label ID="lbl_alerta" runat="server" Text="" Font-Bold="true" ForeColor="#cc0000"></asp:Label>
                    </asp:Panel>

                    <br />
                    <asp:Button ID="btn_save_modal_agregar" runat="server"  CssClass="btn_guardarCancelar_wide" Text="Agregar" OnClick="guardar_empleado_Click" Visible="false"/>
                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_save_modal_agregar" EventName="Click"/> 
                </Triggers>
            </asp:UpdatePanel>

            <asp:Button ID="btn_close_modal_agregar" runat="server" CssClass="btn_guardarCancelar_wide" Text="Regresar" />

        </div>
         <%---------------- end ----------------%>

         <%---------------- POPUP baja empleados ----------------%>
        <asp:Button ID="Button5" runat="server" Text="Button" Style="display:none"   />

        <ajaxToolkit:ModalPopupExtender TargetControlID="Button5" ID="modal_baja" runat="server" PopupControlID="popUp_baja_empleados" BackgroundCssClass="modalBackground" CancelControlID="cerrar_baja" >
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>

        <div id="popUp_baja_empleados" style="display:none">
            <a href="#" id="cerrar_baja" class="cerrar_PopUp" ></a>
            
            <asp:UpdatePanel ID="UpdatePanelBaja" runat="server">
                <ContentTemplate>

                    <asp:ImageButton ID="bajar_empleado" ClientIDMode="Static" runat="server" ImageUrl="~/images/guardar_green.png" OnMouseOver="src='/images/guardar_red.png';" OnMouseOut="src='/images/guardar_green.png';" CssClass="btn_guardar" ValidationGroup="UP_baja" OnClick="bajar_empleado_Click"/>

                    <asp:Label ID="lb5" runat="server" CssClass="titulo_baja" Text="Baja de empleado"></asp:Label>

                    <asp:MultiView ID="MultiView_baja" runat="server">
                        <asp:View ID="View1_baja" runat="server">

                            <asp:Table ID="table_Baja" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_baja" ClientIDMode="Static" runat="server" Text="Número de reloj:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:SqlDataSource ID="data_drop_numBaja" runat="server" SelectCommand="select no_empleado from GCDM_rh.dbo.empleados where fecha_baja is null order by no_empleado asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:DropDownList ID="drop_numBaja" ClientIDMode="Static" runat="server" DataSourceID="data_drop_numBaja" DataTextField="no_empleado" DataValueField="no_empleado" AppendDataBoundItems="true" AutoPostBack="true" ValidationGroup="UP_baja" OnSelectedIndexChanged="drop_numBaja_SelectedIndexChanged">
                                            <asp:ListItem Text="--Número--" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_NombreBaja" runat="server" Text="Nombre:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_NombreCompletoBaja" runat="server" Text="" CssClass="lb_datoBaja"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_depaBaja" runat="server" Text="Departamento:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_depaCompletoBaja" runat="server" Text="" CssClass="lb_datoBaja"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_puestoBaja" runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lb_puesCompletoBaja" runat="server" Text="" CssClass="lb_datoBaja"></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_causa" runat="server" Text="Causa de baja:"></asp:Label></asp:TableCell>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:SqlDataSource ID="data_cb_causa" runat="server" SelectCommand="select id_causaBaja, descripcion from GCDM_rh.dbo.causaBaja where activo = 1 order by descripcion asc" ConnectionString="<% $ConnectionStrings:db %>"></asp:SqlDataSource>
                                        <asp:CheckBoxList ID="cb_causa" ClientIDMode="Static" runat="server" DataSourceID="data_cb_causa" DataTextField="descripcion" DataValueField="id_causaBaja" ValidationGroup="UP_baja"></asp:CheckBoxList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_descBaja" runat="server" Text="Descripción de la baja:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_descBaja" ClientIDMode="Static" runat="server" TextMode="MultiLine" placeholder="Agrega un comentario breve.." ValidationGroup="UP_baja"></asp:TextBox></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell><asp:Label ID="lb_fechaegreso" runat="server" Text="Último día trabajado: (24 hrs)"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:TextBox ID="tb_egreso" ClientIDMode="Static" runat="server" TextMode="DateTimeLocal" Text="" ValidationGroup="UP_baja"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="lb_cb_causa_aviso" runat="server" Text="" Visible="false" CssClass="RequiredValidator" ></asp:Label>
                                        <asp:RequiredFieldValidator ID="NoNull_numBaja" runat="server" ControlToValidate="Drop_numBaja" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage="Número de Reloj" ValidationGroup="UP_baja"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="NoNull_descBaja" runat="server" ControlToValidate="tb_descBaja" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" -- Descripción" ValidationGroup="UP_baja"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="NoNull_egreso" runat="server" ControlToValidate="tb_egreso" CssClass="RequiredValidator" Display="Dynamic" ErrorMessage=" -- Fecha de egreso" ValidationGroup="UP_baja"></asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="Label8" runat="server" Text="Equipo Asignado" Font-Bold="true"></asp:Label>    
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                            <asp:GridView ID="GV_equipo" runat="server" HorizontalAlign="Center" CssClass="grid_conteo">
                                <HeaderStyle CssClass="grid_buscar_header" />
                                <RowStyle CssClass="grid_buscar_row" />
                                <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                                <PagerStyle CssClass="grid_buscar_pager" />
                                
                                <EmptyDataTemplate>
                                    Sin equipo registrado.
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="View2_baja" runat="server">
                            <asp:Label ID="baja_mensaje" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                            <asp:Button ID="baja_continuar" ClientIDMode="Static" runat="server" Text="Continuar" OnClick="baja_continuar_Click" />
                        </asp:View>
                    </asp:MultiView>
                    
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <%---------------- End ----------------%>

        <%---------------- POPUP buscar empleados ----------------%>
        <asp:UpdatePanel ID="UpdatePanelBuscar" runat="server">
            <ContentTemplate>

                <asp:Table ID="table_btnBuscar" ClientIDMode="Static" runat="server">
                    <asp:TableRow>
                        <asp:TableCell><asp:Label ID="titulo_buscarNumero" ClientIDMode="Static" runat="server" Text="Empleado:"></asp:Label></asp:TableCell>
                        <asp:TableCell><asp:TextBox ID="tb_buscarNumero" ClientIDMode="Static" runat="server" onkeypress="return TextBox_PresionarBtnEnter(event);" placeholder="Ingresa número ó nombre"></asp:TextBox></asp:TableCell>
                        <asp:TableCell><asp:ImageButton ID="bt_btnbuscar" ClientIDMode="Static" runat="server" ImageUrl="~/images/buscar.png" OnMouseOver="src='/images/buscar_white.png';" OnMouseOut="src='/images/buscar.png';" OnClick="bt_btnbuscar_Click" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:GridView ID="grid_buscar" runat="server" AutoGenerateColumns="true" AllowPaging="true" PageSize="5" CssClass="grid_buscar" OnPageIndexChanging="grid_buscar_PageIndexChanging" OnRowCommand="grid_buscar_RowCommand" DataKeyNames="Número" OnDataBound="grid_buscar_DataBound" >
                    <HeaderStyle CssClass="grid_buscar_header" />
                    <RowStyle CssClass="grid_buscar_row" />
                    <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                    <PagerStyle CssClass="grid_buscar_pager" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btn_select" ClientIDMode="Static" runat="server" Text="Seleccionar" CssClass="btn_select" CommandName="select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btn_edit" ClientIDMode="Static" runat="server" Text="Editar" CssClass="btn_select_disabled" Enabled="false" CommandName="edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron datos.
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:GridView ID="gridAlertas" runat="server" CssClass="grid_conteo" ShowHeader="false" OnRowDataBound="gridAlertas_RowDataBound" OnSelectedIndexChanging="gridAlertas_SelectedIndexChanging">
                    <HeaderStyle CssClass="grid_conteo_header" />
                    <RowStyle CssClass="grid_conteo_row" />
                    <AlternatingRowStyle CssClass="grid_conteo_altrow" />
                    <PagerStyle CssClass="grid_conteo_pager" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="Ver" ButtonType="Button" ControlStyle-Width="50px" ControlStyle-CssClass="btn_select"></asp:CommandField>
                    </Columns>
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>
        <%---------------- End ----------------%>

        <div class="div_tableMenu">
            <asp:Table ID="tableMenu" ClientIDMode="Static" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageButton ID="Button1" runat="server" OnClientClick="popup('popUp_agregar_empleados'); return false" ImageUrl="~/images/empleados/menuBtn_empleados_disabled_1.png" CssClass="menuBtn_empleados_disabled" Enabled="false" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="Button2" runat="server" OnClick="Button2_Click" ImageUrl="~/images/empleados/menuBtn_empleados_disabled_2.png" CssClass="menuBtn_empleados_disabled" Enabled="false" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="Button3" runat="server" ImageUrl="~/images/empleados/menuBtn_empleados_5.png" CssClass="menuBtn_empleados_disabled" Enabled="true" OnClick="Button3_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <asp:Button ID="BtnActivar" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="BtnActivar" ID="ModalDatos" runat="server" PopupControlID="popUp_alertas" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
            <Animations>
                <OnShown>
                    <FadeIn duration="1.30" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>

        <div id="popUp_alertas" style="display:none">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <br />
                    <asp:Label ID="label_alerta" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <br /><br />
                    <asp:HiddenField ID="idOpcion" runat="server" />                       

                    <asp:GridView ID="grid_alertas_display" CssClass="grid_buscar"  runat="server" AllowPaging="True" PageSize="10" OnPageIndexChanging="grid_alertas_display_PageIndexChanging" HorizontalAlign="Center" >
                        <HeaderStyle CssClass="grid_buscar_header" />
                        <RowStyle CssClass="grid_buscar_row" />
                        <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                        <PagerStyle CssClass="grid_buscar_pager" />
                    
                        <EmptyDataTemplate>
                            No se encontraron datos.
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:Button ID="btnClose" runat="server" Text="Cerrar" CssClass="btn_guardarCancelar" OnClick="btnClose_Click"  />

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <asp:Button ID="btn_menuPrincipal" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_menuPrincipal_Click" />

    </div>

</asp:Content>