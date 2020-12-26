<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="aula.aspx.cs" Inherits="Catalogo_aula" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>
     
<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAula" runat="server">
            <fieldset id="fsAula">
                <legend>Aulas</legend>
                <asp:TextBox ID="txtAul_id" CssClass="txtForm" runat="server" visible="false"></asp:TextBox>

                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server" 
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblEscuela" CssClass="lblForm" runat="server" Text="Escuela"></asp:Label>
                <asp:TextBox ID="txtEscuela" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblDescripcion" CssClass="lblForm" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblCapacidad" CssClass="lblForm" runat="server" Text="Capacidad"></asp:Label>
                <asp:TextBox ID="txtCapacidad" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstado TextAlign=Left runat="server" />
                </asp:Panel>
                 <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click"/>
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnDetalleAula" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleAula" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvAulaDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvAulaDetalle_RowCommand" 
                OnRowDataBound="grvAulaDetalle_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:ButtonField HeaderText="Eliminar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/garbage.png"
                        CommandName="eliReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />

                    <asp:BoundField DataField="aul_escuela" HeaderText="Escuela" Visible="true" />
                    <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción" Visible="true" />
                    <asp:BoundField DataField="aul_capacidad" HeaderText="Capacidad" Visible="true" />
                    <asp:BoundField DataField="aul_activo" HeaderText="Estado" Visible="true" />
                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />

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

