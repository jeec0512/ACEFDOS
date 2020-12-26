<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="activarUsuario.aspx.cs"
     Inherits="Catalogo_activarUsuario"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    
    <asp:Panel ID="pnActualizacion" runat="server">
       <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="sucursal">
                <legend>Activar usuario(a)</legend>
                <asp:Label ID="lblUserName" CssClass="lblForm" runat="server" Text="Usuario"></asp:Label>
                <asp:TextBox ID="txtUserName" CssClass="txtForm" runat="server" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                <asp:Label ID="lblApellidos" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
                <asp:TextBox ID="txtApellidos" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server"  Enabled="false"></asp:TextBox>

                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Activar"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstado TextAlign=Left runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click"/>
                    <asp:HyperLink ID=blRegresar runat="server" style ="width:7rem;height:2rem;background:white; font-size:large;font-weight:bold; color:blue;" CssClass="" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

