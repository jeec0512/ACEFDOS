<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="titulo.aspx.cs" Inherits="Catalogo_titulo" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
       <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnTitulo" runat="server">
            <fieldset id="fsTitulo">
                <legend>Titulos</legend>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl"  Visible="false">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server">
                    </asp:DropDownList>
                </asp:Panel >


                 <asp:TextBox ID="txtTit_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblDel" CssClass="lblForm" runat="server" Text="Del"></asp:Label>
                <asp:TextBox ID="txtDel" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblAl" CssClass="lblForm" runat="server" Text="Al"></asp:Label>
                <asp:TextBox ID="txtAl" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblAlterno" CssClass="lblForm" runat="server" Text="Alterno"></asp:Label>
                <asp:TextBox ID="txtAlterno" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblUlimoNum" CssClass="lblForm" runat="server" Text="ültimo número"></asp:Label>
                <asp:TextBox ID="txtUlimoNum" CssClass="txtForm" runat="server"></asp:TextBox>


                 <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click"/>
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnDetalleTitulo" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleTitulo" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvTitulos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvTitulos_RowCommand"
                OnRowDataBound="grvTitulos_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="tit_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="serie_del" HeaderText="Del" Visible="true" />
                    <asp:BoundField DataField="serie_al" HeaderText="Al" Visible="true" />
                    <asp:BoundField DataField="alterno" HeaderText="Alterno" Visible="true" />
                    <asp:BoundField DataField="ultnumasignado" HeaderText="#Asignado" Visible="true" />


                </Columns>
                <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                    Font-Strikeout="False" />
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </fieldset>
    </asp:Panel>
</asp:Content>

