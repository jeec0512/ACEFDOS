<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="resumenEgresos.aspx.cs"
    Inherits="Egreso_resumenEgresos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
        <asp:Label ID="lblTipoConsulta" runat="server" Text="" Visible="false"></asp:Label>
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Resumen de egresos</legend>
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
            <asp:Panel ID="pnBotones" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnEgresosoAneta" runat="server" CssClass="btnProceso" Text="Egresos ANETA" OnClick="btnEgresosoAneta_Click" />
                <asp:Button ID="btnEgreosxSuc" runat="server" CssClass="btnProceso" Text="Resumen de egresos por sucursal"
                    Visible="true" OnClick="btnEgreosxSuc_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDetalleEgr" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Detalle de egresos</legend>

            <asp:Button ID="btnExcelCA" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelCA_Click" />

            <asp:GridView ID="grvDetalleEgr" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None"
                BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center" Width="90%" AllowPaging="false"
                AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("numero") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Valor de la factura">
                        <ItemTemplate>
                            <%# Eval("valorfactura","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA">
                        <ItemTemplate>
                            <%# Eval("totaliva","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="valor retenido">
                        <ItemTemplate>
                            <%# Eval("valorretencion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor pagado">
                        <ItemTemplate>
                            <%# Eval("apagar","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <%# Eval("concepto") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Documento" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("descripcion") %>
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
    <asp:Panel ID="pnAnetaEgresos" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Egresos por sucursal</legend>

            <asp:Button ID="btnExcelAn" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelAn_Click" />

            <asp:GridView ID="grvAnetaEgresos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None"
                BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center" Width="90%" AllowPaging="false"
                AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Cod.">
                        <ItemTemplate>
                            <%# Eval("SUCURSAL") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("NOM_SUC") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Egreso">
                        <ItemTemplate>
                            <%# Eval("totalEgreso","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA">
                        <ItemTemplate>
                            <%# Eval("totalIva","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retencion">
                        <ItemTemplate>
                            <%# Eval("totalRetencion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pagado">
                        <ItemTemplate>
                            <%# Eval("totalPagado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Devolución">
                        <ItemTemplate>
                            <%# Eval("totalDevolucion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Permisos">
                        <ItemTemplate>
                            <%# Eval("totalPermisos","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Matriculación">
                        <ItemTemplate>
                            <%# Eval("totalMatriculacion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RevVehicular">
                        <ItemTemplate>
                            <%# Eval("totalRevisionVehicular","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MultasTransito">
                        <ItemTemplate>
                            <%# Eval("totalMultasTransito","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AntProveedor">
                        <ItemTemplate>
                            <%# Eval("totalAnticipoPagoProveedor","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PrestamoAnticipoNomina">
                        <ItemTemplate>
                            <%# Eval("totalPrestamoAnticipoNomina","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CargoEmpleado">
                        <ItemTemplate>
                            <%# Eval("totalCargoEmpleado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RepCajaChica">
                        <ItemTemplate>
                            <%# Eval("totalReposiscionCajaChica","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EgresoSinDocContable">
                        <ItemTemplate>
                            <%# Eval("totalEgresoSinDocContable","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AutoConsumo">
                        <ItemTemplate>
                            <%# Eval("totalAutoConsumo","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ArriendoLocal">
                        <ItemTemplate>
                            <%# Eval("totalArriendoLocal","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Combustible">
                        <ItemTemplate>
                            <%# Eval("totalCombustible","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("totalRepuestoVehicular","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RepuestoVehicular">
                        <ItemTemplate>
                            <%# Eval("totalMovilizacion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ServiciosBasicos">
                        <ItemTemplate>
                            <%# Eval("totalServiciosBasicos","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IntructorFac">
                        <ItemTemplate>
                            <%# Eval("totalPagoIntructorFacLiq","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InstructorNtaVta">
                        <ItemTemplate>
                            <%# Eval("totalPagoInstructorNtaVta","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ServiciosBancarios">
                        <ItemTemplate>
                            <%# Eval("totalServiciosBancarios","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PagadoOtros">
                        <ItemTemplate>
                            <%# Eval("totalPagadoOtros","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DevolucionCur.">
                        <ItemTemplate>
                            <%# Eval("totalDevolucionCursoClientes","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Viáticos">
                        <ItemTemplate>
                            <%# Eval("totalViaticos","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NotaVenta">
                        <ItemTemplate>
                            <%# Eval("totalNotaVenta","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FacturaSinRet">
                        <ItemTemplate>
                            <%# Eval("totalFacturaSinRet","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FacturaConRet">
                        <ItemTemplate>
                            <%# Eval("totalfacturaConRet","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FacturaConRet">
                        <ItemTemplate>
                            <%# Eval("totalLiqSinRet","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LiqConRet">
                        <ItemTemplate>
                            <%# Eval("totalLiqConRet","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ReparacionVehicular">
                        <ItemTemplate>
                            <%# Eval("totalReparacionVehicular","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tarifa0">
                        <ItemTemplate>
                            <%# Eval("totalTarifa0","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Otros">
                        <ItemTemplate>
                            <%# Eval("totalOtros","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bienes">
                        <ItemTemplate>
                            <%# Eval("totalBienes","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Servicios">
                        <ItemTemplate>
                            <%# Eval("totalServicios","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BSubtotal">
                        <ItemTemplate>
                            <%# Eval("totalBsubtotal","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BTarifa0">
                        <ItemTemplate>
                            <%# Eval("totalBtarifa0","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Botros">
                        <ItemTemplate>
                            <%# Eval("totalBotros","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SSubtotal">
                        <ItemTemplate>
                            <%# Eval("totalSsubtotal","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STarifa0">
                        <ItemTemplate>
                            <%# Eval("totalStarifa0","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SOtros">
                        <ItemTemplate>
                            <%# Eval("totalSotros","{0:#,##0.##}".ToString()) %>
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

