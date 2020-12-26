<%@ Page Title="" Language="C#" MasterPageFile="~/Herramienta/mpHerramienta.master" AutoEventWireup="true"
    CodeFile="cajaIngreso.aspx.cs" Inherits="Herramienta_cajaIngreso" EnableEventValidation="false" %>

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
            <legend>Control de cajas de ingresos</legend>
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
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnTodos" runat="server" CssClass="btnProceso" Text="Estado de cajas de la sucursal"
                    OnClick="btnTodos_Click" />
                <asp:Button ID="btnConsolidado" runat="server" CssClass="btnProceso" Text="Estado de cajas de las sucursales"
                    OnClick="btnConsolidado_Click" Visible="true" />
                <asp:Button ID="btnFacturasAneta" runat="server" CssClass="btnProceso" Text="Facturas emitidas ANETA"
                    Visible="false" OnClick="btnFacturasAneta_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsListado" class="fieldset-principal">
            <legend>Listado de cajas por fechas y su estado</legend>

            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvListadoFac" runat="server" Width="90%" HorizontalAlign="Center" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" OnRowDataBound="grvListadoFac_RowDataBound">

                <Columns>
                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" SortExpression="sucursal" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="sucursal" DataFormatString="{0:D}" />
                    <asp:BoundField DataField="numfacturas" HeaderText="#Facturas" SortExpression="sucursal" />
                    
                    <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="sucursal"  />
                </Columns>
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </fieldset>

    </asp:Panel>

    <asp:Panel ID="pnConsolidado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsConsolidado" class="fieldset-principal">
            <legend></legend>

            <asp:Button ID="btnExcelC" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelC_Click" />

            <asp:GridView ID="grvConsolidado" runat="server"
                Width="90%" HorizontalAlign="Center" BorderStyle="None"
                BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                OnRowDataBound="grvConsolidado_RowDataBound">

                <Columns>
                    
                    <asp:BoundField DataField="sucursal" HeaderText="Código" SortExpression="sucursal" Visible="false" />
                    <asp:BoundField DataField="nom_suc" HeaderText="Nombre" SortExpression="nom_suc" />
                    <asp:BoundField DataField="cajasproc" HeaderText="#Cajasprocesadas" />
                    <asp:BoundField DataField="cajasnoregistradas" HeaderText="#CajasNOProcesadas" />
                    <asp:BoundField DataField="numcajas" HeaderText="Total de cajas" ReadOnly="True"
                        SortExpression="numcajas" />
                    <asp:BoundField DataField="numfac" HeaderText="#Fact.Autorizadas" SortExpression="nom_suc" />
                    <asp:BoundField DataField="verde" HeaderText="#CajasDepositadas" ReadOnly="True" SortExpression="verde" />
                    <asp:BoundField DataField="amarillo" HeaderText="#CajasPendientes" ReadOnly="True" SortExpression="amarillo" />
                    <asp:BoundField DataField="rojo" HeaderText="#CajasAbiertas" ReadOnly="True" SortExpression="rojo" />
                    
                    <asp:BoundField DataField="pasa" HeaderText="Estado" ReadOnly="True" SortExpression="pasa" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                </Columns>
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
            </asp:GridView>

        </fieldset>
    </asp:Panel>

</asp:Content>

