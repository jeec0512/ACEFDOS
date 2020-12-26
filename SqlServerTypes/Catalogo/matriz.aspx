<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="matriz.aspx.cs" Inherits="Catalogo_matriz" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblmensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="sucursal">
                <legend>Datos de la Matriz</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblRuc" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtRuc" CssClass="txtForm" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="ibConsultar" runat="server" ImageUrl="~/images/iconos/219_2.png" OnClick="ibConsultar_Click" />
                </asp:Panel>
                <asp:Label ID="lblrazonsocial" CssClass="lblForm" runat="server" Text="Razón Social"></asp:Label>
                <asp:TextBox ID="txtrazonsocial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblnombreComercial" CssClass="lblForm" runat="server" Text="Nombre comercial"></asp:Label>
                <asp:TextBox ID="txtnombreComercial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lbldirMatriz" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
                <asp:TextBox ID="txtdirMatriz" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblcontribuyenteEspecial" CssClass="lblForm" runat="server" Text="# Contribuyente especial"></asp:Label>
                <asp:TextBox ID="txtcontribuyenteEspecial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblobligadoContabilidad" CssClass="lblForm" runat="server" Text="Obligado a llevar Contabilidad"></asp:Label>
                <asp:Panel ID="pnObligado" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlObligado" runat="server">
                        <asp:ListItem Value="SI">SI</asp:ListItem>
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblTel" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTel" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

