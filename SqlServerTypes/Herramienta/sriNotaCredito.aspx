<%@ Page Title="" Language="C#" MasterPageFile="~/Herramienta/mpHerramienta.master" AutoEventWireup="true" CodeFile="sriNotaCredito.aspx.cs"
     Inherits="Herramienta_sriNotaCredito" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">
        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Imresión de Notas de Crédito del SRI</legend>
            <iframe id="ifFacturacion" src=" http://aneta.org.ec:9094/AnetaComprobantesNc/site/home.jsf" runat="server" width="100%" height="800px" frameborder="0"></iframe>
        </fieldset>
    </asp:Panel>
</asp:Content>

