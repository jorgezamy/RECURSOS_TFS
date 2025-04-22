<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="empleados_ver.aspx.cs" Inherits="recursos.Views.empleados_ver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function getFlickerSolved()
            {
                document.getElementById('<%=pnlModalCheckUser.ClientID%>').style.display = 'none';
            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor_empleados_ver">     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="titulo" runat="server" Text="Datos Empleado" CssClass="ver_titulo"></asp:Label> 
                <asp:Table ID="tabla_principal" ClientIDMode="Static" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                                    <%-- INICIO TABLA DATOS LABORALES --%>
                                <asp:Table ID="tabla_ver" ClientIDMode="Static" runat="server">
                                        <asp:TableRow>
                                            <asp:TableCell ColumnSpan="2"><asp:Label ID="lb_laborales" runat="server" Text="Datos Laborales" CssClass="ver_titulo"></asp:Label></asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="No. Reloj:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_noempleado" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Nombre(s):"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_nombre" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Apellido paterno:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_apepat" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Apellido materno:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_apemat" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Departamento:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_depto" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Puesto:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_puesto" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow ID="row_cliente" runat="server">
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Cliente:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_cliente" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow ID="row_cliente_puesto" runat="server">
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Tipo movimiento:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_puestocliente" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Supervisor:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_supervisor" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Fecha ingreso:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_fechaingreso" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableRow>
                                            <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Tipo contrato:"></asp:Label></asp:TableCell>
                                            <asp:TableCell><asp:Label ID="lbl_contrato" runat="server" Text=""></asp:Label></asp:TableCell>
                                        </asp:TableRow>

                                    </asp:Table>
                                    </asp:TableCell>
                                <%-- FIN TABLA DATOS LABORALES --%>

                        <asp:TableCell>
                               <%-- INICIO TABLA DATOS PRINCIPALES --%>

                            <asp:Table ID="tabla_ver_principales" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="principales" runat="server" Text="Datos Principales" CssClass="ver_titulo"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Teléfono:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_telefono" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Correo:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_correo" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Zona eco.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_zonaeco" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="País:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_pais" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Estado:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_edo" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Ciudad:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_ciudad" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Calle:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_calle" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="No. Ext:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_noext" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="No. Int:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_noint" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="C.P.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_cp" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Colonia:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_colonia" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Talla C:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_tallaC" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Talla P:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_tallaP" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>
                                <%-- FIN TABLA DATOS PRINCIPALES --%>

                        <asp:TableCell>
                                <%-- INICIO TABLA DATOS PERSONALES --%>

                            <asp:Table ID="tabla_ver_personales" ClientIDMode="Static" runat="server">

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="personales" runat="server" Text="Datos Personales" CssClass="ver_titulo"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Fecha Nac.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_fechanac" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Sexo:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_sexo" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Edo. Civil:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_edocivil" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="No. Hijos:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_nohijos" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="País Nac.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_paisnac" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Estado Nac.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_edonac" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Ciudad Nac.:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_cdnac" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Carrera:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_carrera" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Ingles:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_ingles" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Escolaridad:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_escolaridad" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                <asp:TableCell>
                                        <asp:Label CssClass="ver_subtitulo" ID="Label1" runat="server" Text="Documento:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_documento" runat="server" Text=""></asp:Label></asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label CssClass="ver_subtitulo" ID="Label7" runat="server" Text="Educación (Tipo):"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_escolaridad_institucion" runat="server" Text=""></asp:Label></asp:TableCell>

                                </asp:TableRow>



                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Nom. Padre:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_padre" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Nom. Madre:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_madre" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell  CssClass="ver_subtitulo"><asp:Label runat="server" Text="Contacto de</br>Emergencia:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_contacto" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell  CssClass="ver_subtitulo"><asp:Label runat="server" Text="Teléfono:"></asp:Label></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_contacto_telefono" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <%-- FIN TABLA DATOS PRINCIPALES --%>

                        </asp:TableCell>

                        <asp:TableCell>
                            <%-- INICIO TABLA DATOS SECUNDARIOS --%>

                            <asp:Table ID="tabla_ver_secundarios" ClientIDMode="Static" runat="server">

                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="secundarios" runat="server" Text="Datos Secundarios" CssClass="ver_titulo"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="Servicio:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_servicio" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Numero:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_noservicio" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_fechaservicio" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="CURP:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_curp" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="RFC:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_rfc" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo"><asp:Label runat="server" Text="IMSS:"></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_imss" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Visible="false">
                                    <asp:TableCell ColumnSpan="2" CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Pasaporte:"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Visible="false">
                                    <asp:TableCell><asp:Label ID="lbl_pasaporte" runat="server" Text=""></asp:Label></asp:TableCell>
                                    <asp:TableCell><asp:Label ID="lbl_fechapasaporte" runat="server" Text=""></asp:Label></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Visa:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_visa" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_fechavisa" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Licencia:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_licencia" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_fechalicencia" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="server" ID="row_tipo_licencia" Visible="false">
                                    <asp:TableCell  CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Tipo:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_tipo_licencia" runat="server" Text=""> </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Fast:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_fast" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_fastfecha" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Penal:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_penal" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_penalfecha" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Gafete Unico:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_gafete_unico" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_gafete_unicoVig" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Policial USA:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_policialfecha" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Apto:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_Apto" runat="server" Text=""></asp:Label> | <asp:Label ID="lbl_Apto_Vigencia" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                        
                               <%-- <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2" CssClass="ver_subtitulo">
                                        <asp:Label runat="server" Text="Antidoping:"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="antidoping_1" runat="server" Text="2020-12-05"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="antidoping_2" runat="server" Text="2030-12-05"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>--%>
                        
                            </asp:Table>
                            <%-- FIN TABLA DATOS SECUNDARIOS --%>

                        </asp:TableCell>

                        <asp:TableCell >
                             <asp:Table ID="tabla_ver_foto" ClientIDMode="Static" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell>
                                            <asp:Label ID="TituloFoto" runat="server" Text="Fotografía" CssClass="ver_titulo"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Image ID="marco_foto" runat="server" CssClass="marco_foto" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <div style="text-align:center">
                                            <asp:Button ID="btn_equipo" CssClass="btn_guardarCancelar" runat="server" Text="Equipo Asignado" OnClick="btn_equipo_Click" />

                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br /><br />
                            <asp:Table ID="tabla_ver_baja" runat="server" ClientIDMode="Static" Visible="false" >
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label runat="server" Text="Datos Baja" CssClass="ver_titulo"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label  runat="server" Text="Motivo baja: " CssClass="ver_subtitulo" Style ="text-align : left;"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_baja_motivo" runat="server" Text="" Style="text-align : left;"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label  runat="server" Text="Recontratable: " CssClass="ver_subtitulo" Style ="text-align : left;"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_baja_recontratable" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label  runat="server" Text="Fecha Baja: " CssClass="ver_subtitulo" Style ="text-align : left;"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lbl_baja_fecha" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label  runat="server" Text="Comentarios: " CssClass="ver_subtitulo"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Label ID="lbl_baja_comentarios" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </asp:TableCell>

                    </asp:TableRow>
                </asp:Table>

            </ContentTemplate>
        </asp:UpdatePanel>


        <%--POP UP EQUIPO ASIGNADO--%>
        

         <asp:Button ID="pop_up_equipo" runat="server" Text="Button" Style="display:none"  />

        <ajaxToolkit:ModalPopupExtender TargetControlID="pop_up_equipo" ID="modal_equipo" runat="server" PopupControlID="pnlModalCheckUser"  BackgroundCssClass="modalBackground" CancelControlID="btnClose_equipo">
            <Animations>
                <OnShown>
                    <FadeIn duration="0.50" Fps="100" />
                </OnShown>
            </Animations>
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlModalCheckUser" runat="server" style="display:none">
            <div id="popUp_equipo" >

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <br />
                        <div style="text-align:center">
                            <asp:Label ID="Label2" runat="server" Text="Equipo Asignado" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </div>
                        <br />
                        <asp:GridView ID="datagridview_equipo" CssClass="grid_buscar"  runat="server" AllowPaging="True" PageSize="10"  HorizontalAlign="Center" >
                            <HeaderStyle CssClass="grid_buscar_header" />
                            <RowStyle CssClass="grid_buscar_row" />
                            <AlternatingRowStyle CssClass="grid_buscar_altrow" />
                            <PagerStyle CssClass="grid_buscar_pager" />
                    
                            <EmptyDataTemplate>
                                No se encontraron datos.
                            </EmptyDataTemplate>
                        </asp:GridView>

                        <asp:Button ID="btnClose_equipo" runat="server" Text="Cerrar" CssClass="btn_guardarCancelar" OnClick="btnClose_equipo_Click"  />

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </asp:Panel>

        


        <%--POP UP EQUIPO ASIGNADO--%>



        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>
