<%@ Page Title="" Language="C#" MasterPageFile="~/Contabilidad/mpContabilidad.master" AutoEventWireup="true" CodeFile="AWAContabilidad.aspx.cs" 
    Inherits="Contabilidad_AWAContabilidad"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>
    <h2>Aneta Web Accounting</h2>
    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true" Style="width: 100%; overflow: auto;">
            <iframe id="ifControlEst" src="  http://www.aneta.org.ec:7070/awa/1" runat="server" width="100%" height="800px;" frameborder="0"></iframe>
    </asp:Panel>
</asp:Content>

