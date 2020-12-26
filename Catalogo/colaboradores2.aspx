<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="colaboradores2.aspx.cs" Inherits="Catalogo_colaboradores2" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
    </asp:Panel>
    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
        DataValueField="sucursal">
    </asp:DropDownList>

    <asp:GridView ID="grvColaboradores" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="sucursal" HeaderText="Sucursal" />
            <asp:BoundField DataField="cedula" HeaderText="Cédula" />
            <asp:BoundField DataField="nombres" HeaderText="Nombres" />
            <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
            <asp:BoundField DataField="activo" HeaderText="Activo" />
         
            <asp:ImageField DataImageUrlField="foto" HeaderText="Foto" ControlStyle-Width="100px" ControlStyle-Height="100px" AlternateText="Foto no encontrada" DataAlternateTextField="textoAlterno" NullImageUrl="~/admArchivos/sinImagen.jpg" NullDisplayText="Path de imagen es NULL">
            </asp:ImageField>
        </Columns>
    </asp:GridView>
</asp:Content>

