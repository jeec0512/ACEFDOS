<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="impresionTitulos.aspx.cs" Inherits="Escuela_impresionTitulos" EnableEventValidation="false" %>

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

            <!--<asp:Button ID="btnNotas" runat="server" Text="Facturación electrónica" CssClass="btnProceso" 
                    OnClick="btnNotas_Click" />-->

            <iframe id="ifFacturacion" src="  http://192.168.1.124:8080/escuelaweb/control/titulo.an?prmusrid=166" runat="server" width="100%" height="800px" frameborder="0"></iframe>

        </fieldset>

        <!-- <iframe width="90%" height="315" src="https://www.youtube.com/embed/hzbG0JSX6Z0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen id="ifnotas" name="ifNotas"></iframe> -->

    </asp:Panel>
</asp:Content>

