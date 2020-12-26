﻿<%@ Page Title="" Language="C#" MasterPageFile="~/secretariaAcademica/mpSecretariaAcademica.master" AutoEventWireup="true" CodeFile="impresionTitulos.aspx.cs"
     Inherits="secretariaAcademica_impresionTitulos" EnableEventValidation="false" %>

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
            <legend>Impresión de títulos</legend>

            <iframe id="ifFacturacion" src="  http://www.aneta.org.ec:9095/escuelaweb/control/titulo.an?prmusrid=166" runat="server" width="100%" height="800px" frameborder="0"></iframe>

        </fieldset>


    </asp:Panel>
</asp:Content>

