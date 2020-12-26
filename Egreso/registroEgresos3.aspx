<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="registroEgresos3.aspx.cs" 
    Inherits="Egreso_registroEgresos3" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server" style="width: 98vw; background: #FEF5EB;">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>
    <h2>Registro de egresos</h2>
    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true" Style="width: 100%; overflow: auto;">
            <iframe id="ifControlEst" src=" http://192.168.1.124:8080/app/site/egresos.an?prmusrid=315" runat="server" width="100%" height="800px;" frameborder="0"></iframe>
    </asp:Panel>
</asp:Content>

