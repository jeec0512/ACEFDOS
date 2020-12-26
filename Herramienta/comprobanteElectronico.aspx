<%@ Page Title="" Language="C#" MasterPageFile="~/Herramienta/mpHerramienta.master" AutoEventWireup="true"
    CodeFile="comprobanteElectronico.aspx.cs" Inherits="Herramienta_comprobanteElectronico" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">

    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Comprobantes electrónicos</legend>
            <asp:Panel ID="pnComprobantes" CssClass="" runat="server" Wrap="False">

                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Impresión de facturas" CssClass="btnProceso" />
                <asp:Button ID="Button2" runat="server" Text="Impresión de retención ON LINE" CssClass="btnProceso"
                    OnClick="Button2_Click" />
                <asp:Button ID="Button4" runat="server" Text="Impresión de retención OFF LINE" CssClass="btnProceso"
                    OnClick="Button4_Click" />

                <asp:Button ID="Button3" runat="server" Text="Impresión de Notas de Crédito" CssClass="btnProceso"
                    OnClick="Button3_Click" />
            </asp:Panel>

           

            <asp:Panel ID="pnSicotecnicos" CssClass="pnPeq" runat="server" Wrap="False">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://190.63.17.119:8087/REPOPSICOTECNICOS/"
                    Target="_blank" Text="De un click para ir a Evaluaciones psicotécnicas"
                    ForeColor="DarkBlue" BorderColor="black" BorderWidth="2"></asp:HyperLink>
            </asp:Panel>
        </fieldset>
    </asp:Panel>
</asp:Content>

