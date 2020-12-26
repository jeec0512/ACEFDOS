<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="consultas.aspx.cs" Inherits="Escuela_consultas" 
     EnableEventValidation="false" %>

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
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true" Style="width: 110%; overflow: auto;">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>CONSULTAS</legend>
            <iframe id="ifControlEst" src=" http://www.aneta.org.ec:9095/escuelaweb/consultas/home.an?prmusrid=94465" runat="server" width="100%" height="800px" frameborder="0"></iframe>
        </fieldset>
    </asp:Panel>
</asp:Content>

