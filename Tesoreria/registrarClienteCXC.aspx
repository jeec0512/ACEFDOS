<%@ Page Title="" Language="C#" MasterPageFile="~/Tesoreria/mpTesoreria.master" AutoEventWireup="true" CodeFile="registrarClienteCXC.aspx.cs"
    Inherits="Tesoreria_registrarClienteCXC" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="sucursal">
                <legend>REGISTRO DE CXC AL CLIENTE</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                    <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                    </asp:DropDownList>
                    <asp:Label ID="lblRuc" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtRuc" CssClass="txtForm" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ibConsultar" runat="server" ImageUrl="~/images/iconos/219_2.png" OnClick="ibConsultar_Click" />
                </asp:Panel>
                <asp:Label ID="lblNombreCliente" CssClass="lblForm" runat="server" Text="Nombre del cliente"></asp:Label>
                <asp:TextBox ID="txtNombreCliente" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                
                <asp:Label ID="lblDocumento" CssClass="lblForm" runat="server" Text="# Documento"></asp:Label>
                <asp:TextBox ID="txtDocumento" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>

                <asp:Label ID="lblTotal" CssClass="lblForm" runat="server" Text="Valor"></asp:Label>
                <asp:TextBox ID="txtTotal" CssClass="txtForm" runat="server" Enabled="true" Mask="999999999" OnTextChanged="txtTotal_TextChanged" ></asp:TextBox>
                
                
                <asp:Label ID="lblDescripcion" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

