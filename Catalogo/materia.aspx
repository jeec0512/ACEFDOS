<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="materia.aspx.cs" Inherits="Catalogo_materia" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
     <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
       <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnMateria" runat="server">
            <fieldset id="fsMateria">
                <legend>Materias</legend>
                 <asp:TextBox ID="txtMat_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="false"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl"  Visible="false">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server">
                    </asp:DropDownList>
                </asp:Panel >
                 <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl"  Visible="false">
                    <asp:DropDownList ID="ddlCurso" DataTextField=nom_suc DataValueField=cur_id runat="server">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblDescripcion" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblValor" CssClass="lblForm" runat="server" Text="Valor"></asp:Label>
                <asp:TextBox ID="txtValor" CssClass="txtForm" runat="server"></asp:TextBox>
                 <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click"/>
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnDetalleMateria" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleMateria" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvMateriaDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvMateriaDetalle_RowCommand"
                OnRowDataBound="grvMateriaDetalle_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:ButtonField HeaderText="Eliminar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/garbage.png"
                        CommandName="eliReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="mat_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="mat_descripcion" HeaderText="Descripción" Visible="true" />
                    <asp:BoundField DataField="mat_valor" HeaderText="Valor" Visible="true" />


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

