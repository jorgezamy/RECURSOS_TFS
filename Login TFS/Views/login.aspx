<%@ Page Title="" Language="C#" MasterPageFile="~/index.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Login_TFS.Views.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel_Login" runat="server">
        <ContentTemplate>

            <div id="contenedor">

                <div id="login_div">
                    <asp:Label ID="login_titulo" runat="server" Text="Iniciar Sesión" ClientIDMode="Static"></asp:Label>
                </div>
                
                <div id="login_div2">

                    <asp:Table ID="login_table" runat="server" ClientIDMode="Static">
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Usuario:"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="usertxt" runat="server" placeholder="Escriba su usuario" CssClass="usuario"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Contraseña:"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="passtxt" runat="server" TextMode="Password" placeholder="Escriba su contraseña" CssClass="contrasena"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="Empresa ID:"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="empresa" runat="server" TextMode="Password" placeholder="ID Empresa" CssClass="contrasena"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell><asp:Label runat="server" Text="¿Es cliente?"></asp:Label></asp:TableCell>
                            <asp:TableCell>
                                <asp:CheckBox ID="chkCliente" runat="server" CssClass="cliente" AutoPostBack="true" OnCheckedChanged="chkCliente_CheckedChanged" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>

                    <asp:Button ID="entrar" runat="server" Text="Entrar" ClientIDMode="Static" OnClick="entrar_Click" />

                    <br />

                    <asp:Label ID="Lbloqueado" runat="server" Text="" ClientIDMode="Static"></asp:Label>

                </div>

            </div>

            <div id="privacidad">
                <asp:HyperLink ID="t_privacidad" ClientIDMode="Static" NavigateUrl="http://localhost:57414/Views/inicio.aspx" Target="_blank" runat="server">Aviso de privacidad</asp:HyperLink>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
