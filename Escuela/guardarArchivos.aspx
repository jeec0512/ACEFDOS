﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="guardarArchivos.aspx.cs" 
    Inherits="Escuela_guardarArchivos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
     <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
     <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text="aviso"></asp:Label>
    <div>
        <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged" >
                    </asp:DropDownList>
                </asp:Panel>
    </div>
    <div>
        <asp:FileUpload ID="ulCarga" runat="server" />
        <asp:Button ID="btnCargar" runat="server" Text="Cargar archivo" OnClick="btnCargar_Click" />
    </div>

    <div>
        <asp:GridView ID="grvArchivos" runat="server" AutoGenerateColumns="False" BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Horizontal" OnRowCommand="grvArchivos_RowCommand">
            <AlternatingRowStyle BackColor="#F7F7F7" />
            <Columns>
                <asp:TemplateField HeaderText="Archivo">
                    <ItemTemplate>
                        <asp:LinkButton ID="lkBoton" runat="server" CommandArgument='<%# Eval("archivo") %>'
                            CommandName="Download"
                            ForeColor="Blue" Text='<%# Eval("archivo") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="dimension" HeaderText="Bytes" />
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <SortedAscendingCellStyle BackColor="#F4F4FD" />
            <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
            <SortedDescendingCellStyle BackColor="#D8D8F0" />
            <SortedDescendingHeaderStyle BackColor="#3E3277" />
        </asp:GridView>
    </div>
    <asp:Panel ID="pnImagenes" runat="server" Width="440px" BorderColor="Blue" BorderStyle="Dotted">

    </asp:Panel>

    <asp:Panel ID="Panel1" runat="server">

    </asp:Panel>
</asp:Content>

