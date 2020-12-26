<%@ Page Title="" Language="C#" MasterPageFile="~/Ingreso/mpIngreso.master" AutoEventWireup="true" CodeFile="facturacionElectronica.aspx.cs" Inherits="Ingreso_facturacionElectronica" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true" style="width:110%;overflow:auto;">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Facturación electrónica</legend>

            <!--<asp:Button ID="btnNotas" runat="server" Text="Facturación electrónica" CssClass="btnProceso" 
                    OnClick="btnNotas_Click" />-->

            <iframe id="ifFacturacion" src=" http://www.aneta.org.ec:9095/escuelaweb/site/home.an?prmusrid=64" runat="server" width="100%" height="600px" frameborder="0"></iframe>

        </fieldset>

        <!-- <iframe width="90%" height="315" src="https://www.youtube.com/embed/hzbG0JSX6Z0" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen id="ifnotas" name="ifNotas"></iframe> -->

    </asp:Panel>



</asp:Content>

