<%@ Page Language="C#" AutoEventWireup="true" CodeFile="primeraVez.aspx.cs" Inherits="primeraVez" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">   
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ACEF</title>
    <link href="https://fonts.googleapis.com/css?family=Chivo|Vollkorn" rel="stylesheet"/>
    <link rel="stylesheet" href="path/to/font-awesome/css/font-awesome.min.css"/>


    <link href="App_Themes/Estilos/estiloPrincipal.css" rel="stylesheet" />
    <link href="App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Panel ID="pnActualizacion" runat="server">
                <asp:Label ID="lblmensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server">
                    <fieldset id="sucursal">
                        <legend>Datos del usuario(a)</legend>
                        <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                            <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                            <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server" AutoPostBack="True" 
                                OnTextChanged="txtCedula_TextChanged" Enabled="false"></asp:TextBox>
                            <asp:ImageButton ID="ibConsultar" runat="server" ImageUrl="~/images/iconos/219_2.png" 
                                OnClick="ibConsultar_Click" />
                        </asp:Panel>
                        <asp:Label ID="lblApellidos" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
                        <asp:TextBox ID="txtApellidos" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                        <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                        <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                        <asp:Label ID="lblUserName" CssClass="lblForm" runat="server" Text="Usuario"></asp:Label>
                        <asp:TextBox ID="txtUserName" CssClass="txtForm" runat="server"></asp:TextBox>
                        <asp:Label ID="lblContrasena" CssClass="lblForm" runat="server" Text="Contraseña"></asp:Label>
                        <asp:TextBox ID="txtContrasena" CssClass="txtForm" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblConfirma" CssClass="lblForm" runat="server" Text="Confirme contraseña"></asp:Label>
                        <asp:TextBox ID="txtConfirma" CssClass="txtForm" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                        <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>
                        <asp:Label ID="lblMaquina" CssClass="lblForm" runat="server" Text="Máquina"></asp:Label>
                        <asp:TextBox ID="txtMaquina" CssClass="txtForm" runat="server"></asp:TextBox>

                        <asp:Label ID="lblNivel" CssClass="lblForm" runat="server" Text="Nivel"></asp:Label>
                        <asp:Panel ID="pnIva" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlNivel" runat="server">
                                <asp:ListItem Value="1">Uno</asp:ListItem>
                                <asp:ListItem Value="2">Dos</asp:ListItem>
                                <asp:ListItem Value="3">Tres</asp:ListItem>
                               <asp:ListItem Value="4">Cuatro</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>



                        <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal"></asp:Label>
                        <asp:Panel ID="Panel1" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Label ID="lblGrupo" CssClass="lblForm" runat="server" Text="Grupo"></asp:Label>
                        <asp:Panel ID="pnGrupo" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlGrupo" DataTextField="descripcion" DataValueField="codigo" runat="server">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Label ID="lblTipo" CssClass="lblForm" runat="server" Text="Tipo"></asp:Label>
                        <asp:Panel ID="pnpTipo" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlTipo" runat="server">
                                <asp:ListItem Value="1">A</asp:ListItem>
                                <asp:ListItem Value="2">B</asp:ListItem>
                                <asp:ListItem Value="3">C</asp:ListItem>
                               <asp:ListItem Value="4">D</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>


                        <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                            <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm" 
                                OnClick="btnGuardar_Click" />
                            <asp:HyperLink ID="blRegresar" runat="server" CssClass="" Text="Regresar" NavigateUrl="~/ingresar.aspx" style="margin:20px;position:relative;top-10px;border: 1px solid black; "></asp:HyperLink>
                        </asp:Panel>

                    </fieldset>
                </asp:Panel>
            </asp:Panel>

        </div>
    </form>
</body>
</html>
