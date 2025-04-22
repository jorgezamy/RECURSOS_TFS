<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="empleados_fotos.aspx.cs" Inherits="recursos.Views.empleados_fotos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="/Content/js/fotos/jquery.webcam.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pageUrl = "<%=ResolveUrl("~/Views/empleados_fotos.aspx") %>";
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: "<%=ResolveUrl("~/Content/js/fotos/jscam.swf") %>",
                debug: function (type, status) {
                    $('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    $.ajax({
                        type: "POST",
                        url: pageUrl + "/GetCapturedImage",
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $("[id*=imgCapture]").css("visibility", "visible");
                            $("[id*=imgCapture]").attr("src", r.d);
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }
            });
        });
        function Capture() {
            webcam.capture();
            return false;
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="contenedor">

        <asp:Label ID="nombreUsuario" ClientIDMode="Static" runat="server" Text=""></asp:Label>

        <asp:Label ID="titulo" ClientIDMode="Static" runat="server" Text="Foto de empleado"></asp:Label>

<%--        <asp:UpdatePanel ID="update_fotos" runat="server">
            <ContentTemplate>--%>

<%--                <asp:Label ID="lblNoFoto" runat="server" Text="Empleado:"></asp:Label>
                <asp:DropDownList ID="drop_numero_empleado" runat="server" CssClass="drop_numero_empleado_fotos" AppendDataBoundItems="true" >
                    <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                </asp:DropDownList>
                <br />--%>

                
                <asp:Label ID="lblNoFoto" runat="server" Text="Empleado sin fotografía:"></asp:Label>
                <asp:DropDownList ID="drop_numero_empleado_sin_foto" runat="server" CssClass="drop_numero_empleado_fotos" AppendDataBoundItems="true" >
                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                    <asp:Label ID="lblConFoto" runat="server" Text="Actualizar Fotografía:"></asp:Label>
                <asp:DropDownList ID="drop_numero_empleado_con_foto" runat="server" CssClass="drop_numero_empleado_fotos" AppendDataBoundItems="true"  >
                <asp:ListItem Text="-- Seleccionar --" Value=""></asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="labelAlerta" runat="server" Text="" Font-Bold="true"></asp:Label>
                <br />
                <br />

<%--                <asp:Button ID="btn_selectEmpleado" ClientIDMode="Static" runat="server" Text="Seleccionar" CssClass="btn_selectFoto" OnClick="btn_selectEmpleado_Click" />--%>
                <asp:Button ID="btn_selectEmpleado" ClientIDMode="Static" runat="server" Text="Seleccionar" CssClass="btn_selectFoto" OnClick="btn_selectEmpleado_Click" />


                <asp:Table ID="table_empleadosFotos" ClientIDMode="Static" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <u>Ultima foto registrada</u>
                        </asp:TableCell>
                        <asp:TableCell>
                            <u>Cámara en vivo</u>
                        </asp:TableCell>
                        <asp:TableCell>
                            <u>Foto capturada</u>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="fotoActual" runat="server" Style=" width: 320px; height: 240px" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <div id="webcam">
                            </div>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Image ID="imgCapture" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Button ID="btn_capture" runat="server" Text="Capturar Foto" CssClass="btn_captureFoto"  OnClientClick="return Capture();" />
                <asp:Button ID="btn_captureNew" runat="server" Text="Actualizar Foto" CssClass="btn_captureFoto" OnClick="btn_captureNew_Click" />
                <asp:Button ID="btn_empleadoNew" runat="server" Text="Cambiar Empleado" CssClass="btn_captureFoto" OnClick="btn_empleadoNew_Click" />

                <br />

                <%--<span id="camStatus"></span>--%>

<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>

        <asp:Button ID="btn_regresar" runat="server" Text="Regresar" CssClass="cerrar_sesion" OnClick="btn_regresar_Click" />

    </div>

</asp:Content>