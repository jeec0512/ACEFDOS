<%@ Page Title="" Language="C#" MasterPageFile="~/Ingreso/mpIngreso.master" AutoEventWireup="true" CodeFile="resumenFacturacion.aspx.cs"
    Inherits="Ingreso_resumenFacturacion" EnableEventValidation="false" %>

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
            <legend></legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnFacxFecxSuc" runat="server" CssClass="btnProceso" Text="Facturas autorizadas" OnClick="btnFacxFecxSuc_Click" />
                <asp:Button ID="btnFacturasEmitidas" runat="server" CssClass="btnProceso" Text="Facturas emitidas" OnClick="btnFacturasEmitidas_Click" />

                <asp:Button ID="btnItemVendidos" runat="server" CssClass="btnProceso" Text="Facturación por items" OnClick="btnItemVendidos_Click" />
                <asp:Button ID="btnContabilizacion" runat="server" CssClass="btnProceso" Text="Contabilización" Visible="true"
                    OnClick="btnContabilizacion_Click" />
                 <asp:Button ID="btnImprimir" runat="server" CssClass="btnProceso"  Text="Imprimir asiento" OnClick="btnImprimir_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDetalleFac" runat="server" CssClass="" Visible="true">
        <fieldset id="fsListado" class="fieldset-principal">
            <legend>Facturas autorizadas</legend>
            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvDetalleFac" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowPaging="false" AllowSorting="True" PageSize="150">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <%# Eval("FECHA","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Factura" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FACTURA") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Identidad">
                        <ItemTemplate>
                            <%# Eval("IDENTIDAD") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombres">
                        <ItemTemplate>
                            <%# Eval("NOMBRES") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venta Bruta">
                        <ItemTemplate>
                            <%# Eval("VENTABRUTA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descuento">
                        <ItemTemplate>
                            <%# Eval("DESCUENTO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venta neta">
                        <ItemTemplate>
                            <%# Eval("VENTANETA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A. 14">
                        <ItemTemplate>
                            <%# Eval("IVA14","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A. 12">
                        <ItemTemplate>
                            <%# Eval("IVA12","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A. 0">
                        <ItemTemplate>
                            <%# Eval("IVA0","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <%# Eval("TOTAL","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Concepto">
                        <ItemTemplate>
                            <%# Eval("FAC_TIPOCONCEPTO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Curso">
                        <ItemTemplate>
                            <%# Eval("FAC_CURSO") %>
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


    <asp:Panel ID="pnTotalesFacturacion" runat="server" CssClass="" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Totales</legend>
            <asp:GridView ID="grvTotalesFacturacion" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowPaging="false" AllowSorting="True" PageSize="1">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Venta Bruta">
                        <ItemTemplate>
                            <%# Eval("VENTABRUTA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descuento">
                        <ItemTemplate>
                            <%# Eval("DESCUENTO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venta neta">
                        <ItemTemplate>
                            <%# Eval("VENTANETA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A. 14">
                        <ItemTemplate>
                            <%# Eval("IVA14","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A. 12">
                        <ItemTemplate>
                            <%# Eval("IVA12","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A. 0">
                        <ItemTemplate>
                            <%# Eval("IVA0","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <%# Eval("TOTAL","{0:#,##0.##}".ToString()) %>
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

    <asp:Panel ID="pnResulItem" runat="server" CssClass="" ScrollBars="Vertical" Visible="true">
        <fieldset id="Fieldset3" class="fieldset-principal">
            <legend>Facturación por items</legend>

            <asp:Button ID="btnExcelRf" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelRf_Click" />

            <asp:GridView ID="grvResulItem" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Producto" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("CURPRODUCTO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("PRODUCTO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("CANTIDAD","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Val/Unit">
                        <ItemTemplate>
                            <%# Eval("VALORUNIT","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <%# Eval("PRECIO","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descuento">
                        <ItemTemplate>
                            <%# Eval("DESCUENTO","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venta neta">
                        <ItemTemplate>
                            <%# Eval("TOTALASINIMP","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA 14">
                        <ItemTemplate>
                            <%# Eval("IVA14","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA 12">
                        <ItemTemplate>
                            <%# Eval("IVA12","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA 0">
                        <ItemTemplate>
                            <%# Eval("IVA0","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CXP">
                        <ItemTemplate>
                            <%# Eval("CXP","{0:#,##0.00}".ToString()) %>
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

    <asp:Panel ID="pnTotalProductos" runat="server" CssClass="pnTotalesGrid" ScrollBars="Vertical"
        Visible="true">
        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Totales por items</legend>
            <asp:GridView ID="grvTotalProductos" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="2" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="True" AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("CANTIDAD","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <%# Eval("PRECIO","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descuento">
                        <ItemTemplate>
                            <%# Eval("DESCUENTO","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Venta neta">
                        <ItemTemplate>
                            <%# Eval("TOTALASINIMP","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA 14">
                        <ItemTemplate>
                            <%# Eval("IVA14","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA 12">
                        <ItemTemplate>
                            <%# Eval("IVA12","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA 0">
                        <ItemTemplate>
                            <%# Eval("IVA0","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CXP">
                        <ItemTemplate>
                            <%# Eval("CXP","{0:#,##0.00}".ToString()) %>
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

    <asp:Panel ID="pnContabilizacion" runat="server" CssClass="" ScrollBars="Vertical" Visible="true">
        <fieldset id="fsContabilizacion" class="fieldset-principal">
            <legend>Contabilización</legend>
            <asp:Button ID="btnExcelC" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelC_Click" />
            <asp:GridView ID="grvContabilizacion" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("cod_cta") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("nom_cta") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DEBE" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("deb_mov","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HABER">
                        <ItemTemplate>
                            <%# Eval("cre_mov","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Referencia">
                        <ItemTemplate>
                            <%# Eval("doc_ref") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <%# Eval("des_mov") %>
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
    <asp:Panel ID="pnCuadre" runat="server" CssClass="" ScrollBars="Vertical" Visible="true">
        <fieldset id="Fieldset4" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvCuadre" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="True" AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="DEBE" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("deb_mov","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HABER">
                        <ItemTemplate>
                            <%# Eval("cre_mov","{0:#,##0.00}".ToString()) %>
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


    <asp:Panel ID="pnFacturasEmitidas" CssClass="" runat="server" Visible="true">
        <asp:Button ID="btnImpFacturasEmitidas" runat="server" CssClass="btnProceso"  Text="Imprimir facturas emitidas" OnClick="btnImpFacturasEmitidas_Click" />
                <fieldset id="fdFacturasEmitidas" class="fieldset-principal">
                    <legend>Facturas emitidas</legend>
                    <asp:GridView ID="grvFacturasEmitidas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                        Width="90%"
                        AllowSorting="True" PageSize="50" 
                        OnRowDataBound="FacturasEmitidas_RowDataBound" ShowFooter="True" style="font-size:0.7rem">
                        <Columns>
                            <asp:BoundField DataField="row" HeaderText="#" Visible="TRUE" />
                            <asp:BoundField DataField="factura" HeaderText="#Fcatura" Visible="TRUE" />
                            <asp:BoundField DataField="fecha" HeaderText="Fecha" Visible="TRUE" DataFormatString="{0:d}"/>
                                <asp:BoundField DataField="identidad" HeaderText="#Identidad" Visible="TRUE" />
                            <asp:BoundField DataField="nombres" HeaderText="Nombres" Visible="TRUE" />
                            <asp:BoundField DataField="ventabruta" HeaderText="Venta bruta" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="descuento" HeaderText="Descuento" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="ventaneta" HeaderText="Venta neta" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="baseimponible" HeaderText="Base imponible" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="iva0" HeaderText="I.V.A. 0" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="iva12" HeaderText="I.V.A. 12" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="cxp" HeaderText="CXP" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N}" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </fieldset>
            </asp:Panel>

</asp:Content>





