<%@ Page Title="" Language="C#" MasterPageFile="~/Tesoreria/mpTesoreria.master" AutoEventWireup="true"
    CodeFile="reportesTesoreria.aspx.cs" Inherits="Tesoreria_reportesTesoreria" EnableEventValidation="false" %>

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
            <legend>Recaudación</legend>
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
                <asp:Button ID="btnRecTotal" runat="server" CssClass="btnLargoForm" Text="Recaudación ANETA" Visible="true"
                    OnClick="btnRecTotal_Click" />
                <asp:Button ID="btnRecxSuc" runat="server" CssClass="btnLargoForm " Text="Recaudación por sucursal" OnClick="btnRecxSuc_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>


    <asp:Panel ID="pnConsolidado" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset3" class="fieldset-principal">
            <legend>Consolidaddo</legend>
            <asp:GridView ID="grvConsolidado" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowPaging="True" AllowSorting="True" PageSize="1">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Ing.Netos">
                        <ItemTemplate>
                            <%# Eval("INGRESOSNETOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depósito">
                        <ItemTemplate>
                            <%# Eval("DEPOSITADO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gastos">
                        <ItemTemplate>
                            <%# Eval("GASTOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CXC">
                        <ItemTemplate>
                            <%# Eval("CXC","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ingresos">
                        <ItemTemplate>
                            <%# Eval("INGRESOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret.IVA">
                        <ItemTemplate>
                            <%# Eval("RETENCIOIVA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret.Fuente">
                        <ItemTemplate>
                            <%# Eval("RETENCIOFUENTE","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Efectivo">
                        <ItemTemplate>
                            <%# Eval("EFECTIVO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheques">
                        <ItemTemplate>
                            <%# Eval("CHEQUES","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="T/Crédito">
                        <ItemTemplate>
                            <%# Eval("TARJETAS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Autoconsumo">
                        <ItemTemplate>
                            <%# Eval("AUTOCONSUMO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="N/Crédito">
                        <ItemTemplate>
                            <%# Eval("NOTADECREDITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nómina">
                        <ItemTemplate>
                            <%# Eval("NOMINA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transferencia">
                        <ItemTemplate>
                            <%# Eval("TRANSFERENCIA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="T/Débito">
                        <ItemTemplate>
                            <%# Eval("DEBITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Varios">
                        <ItemTemplate>
                            <%# Eval("VARIOS","{0:#,##0.##}".ToString()) %>
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

    <asp:Panel ID="pnRecaudacion" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Recaudación ANETA</legend>

            <asp:Button ID="btnExcelRA" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelRA_Click" />

            <asp:GridView ID="grvRecaudacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("SUCURSAL") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("NOMBRE") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ing.Netos">
                        <ItemTemplate>
                            <%# Eval("INGRESOSNETOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depósito">
                        <ItemTemplate>
                            <%# Eval("POSIBLEDEPOSITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gastos">
                        <ItemTemplate>
                            <%# Eval("GASTOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CXC">
                        <ItemTemplate>
                            <%# Eval("CXC","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ingresos">
                        <ItemTemplate>
                            <%# Eval("INGRESOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret.IVA">
                        <ItemTemplate>
                            <%# Eval("RETENCIOIVA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret.Fuente">
                        <ItemTemplate>
                            <%# Eval("RETENCIOFUENTE","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Efectivo">
                        <ItemTemplate>
                            <%# Eval("EFECTIVO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheques">
                        <ItemTemplate>
                            <%# Eval("CHEQUES","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="T/Crédito">
                        <ItemTemplate>
                            <%# Eval("TARJETAS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Autoconsumo">
                        <ItemTemplate>
                            <%# Eval("AUTOCONSUMO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="N/Crédito">
                        <ItemTemplate>
                            <%# Eval("NOTADECREDITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nómina">
                        <ItemTemplate>
                            <%# Eval("NOMINA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transferencia">
                        <ItemTemplate>
                            <%# Eval("TRANSFERENCIA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="T/Débito">
                        <ItemTemplate>
                            <%# Eval("DEBITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Varios">
                        <ItemTemplate>
                            <%# Eval("VARIOS","{0:#,##0.##}".ToString()) %>
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

    <asp:Panel ID="pnDetalleSuc" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Recaudación de la sucursal</legend>

            <asp:Button ID="btnExcelRS" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelRS_Click" />


            <asp:GridView ID="grvDetalleSuc" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

                    <asp:TemplateField HeaderText="Código">
                        <ItemTemplate>
                            <%# Eval("SUCURSAL") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("NOMBRE") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FECHA","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ing.Netos">
                        <ItemTemplate>
                            <%# Eval("INGRESOSNETOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Depósito">
                        <ItemTemplate>
                            <%# Eval("POSIBLEDEPOSITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gastos">
                        <ItemTemplate>
                            <%# Eval("GASTOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CXC">
                        <ItemTemplate>
                            <%# Eval("CXC","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ingresos">
                        <ItemTemplate>
                            <%# Eval("INGRESOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret.IVA">
                        <ItemTemplate>
                            <%# Eval("RETENCIOIVA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret.Fuente">
                        <ItemTemplate>
                            <%# Eval("RETENCIOFUENTE","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Efectivo">
                        <ItemTemplate>
                            <%# Eval("EFECTIVO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheques">
                        <ItemTemplate>
                            <%# Eval("CHEQUES","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="T/Crédito">
                        <ItemTemplate>
                            <%# Eval("TARJETAS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Autoconsumo">
                        <ItemTemplate>
                            <%# Eval("AUTOCONSUMO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="N/Crédito">
                        <ItemTemplate>
                            <%# Eval("NOTADECREDITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nómina">
                        <ItemTemplate>
                            <%# Eval("NOMINA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transferencia">
                        <ItemTemplate>
                            <%# Eval("TRANSFERENCIA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="T/Débito">
                        <ItemTemplate>
                            <%# Eval("DEBITO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Varios">
                        <ItemTemplate>
                            <%# Eval("VARIOS","{0:#,##0.##}".ToString()) %>
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

