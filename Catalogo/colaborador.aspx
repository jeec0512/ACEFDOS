<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="colaborador.aspx.cs" Inherits="Catalogo_colaborador" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblmensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnColaborador" runat="server">
            <fieldset id="sucursal">
                <legend>Datos del colaborador(a)</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server"></asp:TextBox>
                    <asp:ImageButton ID=ibConsultar runat="server" ImageUrl="~/images/iconos/219_2.png"
                        OnClick="ibConsultar_Click" />
                </asp:Panel>
                <asp:Label ID="lblApellidos" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
                <asp:TextBox ID="txtApellidos" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblCcosto" CssClass="lblForm" runat="server" Text="C. Costo"></asp:Label>
                <asp:Panel ID="pnCcosto" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCcosto" DataTextField=nom_cco DataValueField=mae_cco runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                
                <asp:Label ID="lblPro_cuentacontable" CssClass="lblForm" runat="server" Text="Cuenta contable"></asp:Label>
                <asp:Panel ID="pnCtaCble" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCtaCble" DataTextField="nom_cta" DataValueField="mae_cue" runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                
                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnActivo" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkActivo TextAlign=Left runat="server" />
                </asp:Panel>

                

                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

</asp:Content>

