<%@ Page Title="" Language="C#" MasterPageFile="~/awmMembresias/mpAwmMembresia.master" AutoEventWireup="true" CodeFile="regMembresia.aspx.cs" 
    Inherits="awmMembresias_regMembresia"  EnableEventValidation="false" %>

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

        <fieldset id="fdTitulos" style="border:0">
            <legend>Registro de membresias contratos</legend>

            <iframe id="ifFacturacion" src=" http://www.aneta.org.ec:7070/awm/1" runat="server" width="1800px" height="1000px" frameborder="0"></iframe>

        </fieldset>


    </asp:Panel>
</asp:Content>

