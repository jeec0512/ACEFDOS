<%@ Page Title="" Language="C#" MasterPageFile="~/Cartera/mpCartera.master" AutoEventWireup="true" CodeFile="carteraIngresos.aspx.cs" Inherits="Cartera_carteraIngresos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
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
            <legend>Facturas emitidas</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">
                
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnTodos" runat="server" CssClass="btnProceso" Text="Cuentas por cobrar por sucursal"
                    OnClick="btnTodos_Click" />
                <asp:Button ID="btnConsolidado" runat="server" CssClass="btnProceso" Text="Cuentas por cobrar en las surcursales"
                    OnClick="btnConsolidado_Click" Visible="true" />
                <asp:Button ID="btnFacturasAneta" runat="server" CssClass="btnProceso" Text="Facturas emitidas ANETA"
                    Visible="false" OnClick="btnFacturasAneta_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsListado" class="fieldset-principal">
            <legend>Cuentas por cobrar por sucursal</legend>

            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvListadoFac" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%" AllowPaging="True"
                AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="False" />

                    <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fac_fechaemision","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Factura" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fac_secuencial") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fac_importetotal","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Anticipo" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("pagado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Saldo" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("diferencia","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cliente" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fac_razoncomprador") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Identidad" FooterStyle-Wrap="False" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fac_ruccomprador") %>
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
        </fieldset>

    </asp:Panel>
    <asp:Panel ID="pnConsolidado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsConsolidado" class="fieldset-principal">
            <legend>Total facturado por sucursal</legend>

            <asp:Button ID="btnExcelC" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelC_Click" />

            <asp:GridView ID="grvConsolidado" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%"
                AllowPaging="True" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("fac_sucursal") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("nom_suc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TotalFacturado">
                        <ItemTemplate>
                            <%# Eval("totalfacturado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TotalCancelado">
                        <ItemTemplate>
                            <%# Eval("totalPagado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saldo">
                        <ItemTemplate>
                            <%# Eval("diferencia","{0:#,##0.##}".ToString()) %>
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
        </fieldset>
    </asp:Panel>
</asp:Content>

