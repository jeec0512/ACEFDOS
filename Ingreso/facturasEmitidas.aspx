<%@ Page Title="" Language="C#" MasterPageFile="~/Ingreso/mpIngreso.master" AutoEventWireup="true" CodeFile="facturasEmitidas.aspx.cs"
    Inherits="Ingreso_facturasEmitidas" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
            <legend>Facturas emitidas</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">
                
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999 00:00:00"
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
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnTodos" runat="server" CssClass="btnProceso" Text="Facturas emitidas"
                    OnClick="btnTodos_Click" />
                <asp:Button ID="btnConsolidado" runat="server" CssClass="btnProceso" Text="Total facturado"
                    OnClick="btnConsolidado_Click" Visible="true" />
                <asp:Button ID="btnFacturasAneta" runat="server" CssClass="btnProceso" Text="Facturas emitidas ANETA"
                    Visible="false" OnClick="btnFacturasAneta_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsListado" class="fieldset-principal">
            <legend>Facturas emitidas</legend>

            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvListadoFac" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%" AllowPaging="True"
                AllowSorting="True" PageSize="200" OnPageIndexChanged="grvListadoFac_PageIndexChanged" 
                OnPageIndexChanging="grvListadoFac_PageIndexChanging" 
                OnSelectedIndexChanging="grvListadoFac_SelectedIndexChanging">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="fac_fechaemision" HeaderText="Fecha" Visible="TRUE" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="fac_secuencial" HeaderText="#Factura" Visible="TRUE" />
                    <asp:BoundField DataField="fac_ruccomprador" HeaderText="#Identidad" Visible="TRUE" />
                    <asp:BoundField DataField="fac_razoncomprador" HeaderText="Nombres" Visible="TRUE" />
                    <asp:BoundField DataField="fde_descripcion" HeaderText="Descripción" Visible="TRUE" />
                    <asp:BoundField DataField="fac_observacion" HeaderText="Observación" Visible="TRUE" />
                    <asp:BoundField DataField="fac_estado" HeaderText="Estado" Visible="TRUE" />
                    <asp:BoundField DataField="fac_sri" HeaderText="Obs./SRI" Visible="TRUE" />
                    <asp:BoundField DataField="fac_importetotal" HeaderText="Total Factura" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="fac_totaldescuento" HeaderText="Descuento" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="fac_valorimpuesto" HeaderText="Impuesto" DataFormatString="{0:N}" />
                   <asp:BoundField DataField="fac_recaudado" HeaderText="Recaudado" DataFormatString="{0:N}" />

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
                            <%# Eval("Sucursal") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("Nombre") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Número">
                        <ItemTemplate>
                            <%# Eval("Numero") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <%# Eval("Precio") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descuento">
                        <ItemTemplate>
                            <%# Eval("Descuento") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor">
                        <ItemTemplate>
                            <%# Eval("Valor") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A.">
                        <ItemTemplate>
                            <%# Eval("Iva") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <%# Eval("Total") %>
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

