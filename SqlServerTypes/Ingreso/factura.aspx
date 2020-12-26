<%@ Page Title="" Language="C#" MasterPageFile="~/Ingreso/mpIngreso.master" AutoEventWireup="true" CodeFile="factura.aspx.cs"
    Inherits="Ingreso_factura" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Factura</legend>
            <asp:Panel ID="pnCabeceraFactura" CssClass="pnAccionGrid" runat="server">
                <asp:Label ID="lblRuc" runat="server" Text="RUC" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaRuc" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblSecuencial" runat="server" Text="Secuencial" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaSecuencial" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Button ID="btnTraerFactura" runat="server" Text="Traer factura"
                    OnClick="btnTraerFactura_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <fieldset id="Fieldset1" class="fieldset-principal">
        <legend>Detalle de la factura</legend>
        <asp:GridView ID="grvListadoFac" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
            Width="90%" AllowSorting="True" PageSize="5" DataKeyNames="FAC_SECUENCIAL">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:CommandField ShowSelectButton="false" />
                <asp:TemplateField HeaderText="Secuencial" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_SECUENCIAL") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_FECHAEMISION") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_RAZONCOMPRADOR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_TOTALSINIMP") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descuento" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_TOTALDESCUENTO") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Precio" FooterStyle-Wrap="False" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_BASEIMPONIBLE") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="I.V.A." FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_VALORIMPUESTO") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Factura" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_IMPORTETOTAL") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recauda" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_RECAUDADO") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RetIVA" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_RETENIDOIVA") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RetFTE." FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_RETENIDOFUENTE") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Saldo" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_SALDO") %>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <asp:Panel ID="pnOpcion" CssClass="pnAccion" runat="server">
            <asp:Button ID="btnEnviarFac" CssClass="btnForm" runat="server" Text="Enviar" 
                Visible="False" />
            <asp:Button ID="btnBorrarFac" CssClass="btnForm" runat="server" Text="Borrar" OnClick="btnBorrarFac_Click"
                Visible="False" />
            <asp:Button ID="btnAnularFac" CssClass="btnForm" runat="server" Text="Anular" OnClick="btnAnularFac_Click"
                Visible="False" />
            <asp:Button ID="btnCancelarFac" CssClass="btnForm" runat="server" Text="Regresar" OnClick="btnCancelarFac_Click" />
        </asp:Panel>
    </fieldset>


</asp:Content>

