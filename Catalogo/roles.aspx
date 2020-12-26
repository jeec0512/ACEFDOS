<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" 
    CodeFile="roles.aspx.cs" Inherits="Catalogo_roles" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
     <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnRol" runat="server">
            <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="false"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="false">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server">
                    </asp:DropDownList>
                </asp:Panel>
            <fieldset id="fsRol">
                <legend>Rol</legend>
                <asp:Label ID="lblCodigo" CssClass="lblForm" runat="server" Text="Código" Visible="true"></asp:Label>
                <asp:TextBox ID="txtCodigo" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblDescripcion" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtForm" runat="server"></asp:TextBox>
            </fieldset>
             <asp:Panel ID="Panel3" runat="server" CssClass="pnFormBotonera">
                <asp:Button ID="btnNuevoRol" runat="server" Text="Nuevo Rol" CssClass="btnForm" OnCommand="btnNuevoRol_Command" />
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx" Style="border: solid; font-size: 1rem; font-weight: 700"></asp:HyperLink>
            </asp:Panel>

        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="pnDetalleCurso" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleRol" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvRolDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvCursoDetalle_RowCommand"
                OnRowDataBound="grvCursoDetalle_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="id_UsuarioGrupo" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="codigo" HeaderText="Nomenclatura" Visible="true" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" />
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

