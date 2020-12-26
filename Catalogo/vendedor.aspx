<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="vendedor.aspx.cs" Inherits="Catalogo_vendedor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblmensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="sucursal">
                <legend>Datos del vendedor(a)</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server"></asp:TextBox>
                    <asp:ImageButton ID=ibConsultar runat="server" ImageUrl="~/images/iconos/219_2.png" 
                        OnClick="ibConsultar_Click" />
                </asp:Panel>
                
                <asp:Label ID="lblVEN_APELLIDO" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
                <asp:TextBox ID="txtVEN_APELLIDO" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblVEN_NOMBRE" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtVEN_NOMBRE" CssClass="txtForm" runat="server"></asp:TextBox>
                
                
                <asp:Label ID="lblTPC_ID" CssClass="lblForm" runat="server" Text="Tipo de comisión"></asp:Label>
                <asp:Panel ID="pnTPC_ID" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlTPC_ID" DataTextField=TPC_NOMBRE DataValueField=TPC_ID runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal"></asp:Label>
                <asp:Panel ID="Panel1" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblSUC_ID" CssClass="lblForm" runat="server" Text="ID Sucursal"></asp:Label>
                <asp:Panel ID="pnSUC_ID" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlSUC_ID" DataTextField=PVE_SUCURSAL DataValueField=PVE_ID runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnActivo" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkActivo TextAlign=Left runat="server" />
                </asp:Panel>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" 
                        OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID=blRegresar runat="server"   Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

</asp:Content>

