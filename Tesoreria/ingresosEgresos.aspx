<%@ Page Title="" Language="C#" MasterPageFile="~/Tesoreria/mpTesoreria.master" AutoEventWireup="true"
    CodeFile="ingresosEgresos.aspx.cs" Inherits="Tesoreria_ingresosEgresos" EnableEventValidation="false" %>

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
            <legend>Cierre diario de ventas</legend>
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
                <asp:Button ID="btnEstadoSuc" runat="server" CssClass="btnProceso" Text="Consultar" Visible="true"
                    OnClick="btnEstadoSuc_Click" />

            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCierreCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Cierre diario de ventas</legend>

            <asp:Button ID="btnExceSuc" runat="server" CssClass="btnLargoForm " Text="A Excel" OnClick="btnExceSuc_Click" />
            <asp:Button ID="btnImprimir" runat="server" CssClass="btnLargoForm " Text="Imprimir" OnClick="btnImprimir_Click" />

            <asp:GridView ID="grvCierreCaja" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="100%" AllowSorting="false"
                PageSize="5" AutoGenerateSelectButton="false" Height="157px">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

                    <asp:TemplateField HeaderText="Descripción" FooterStyle-Wrap="true">
                        <ItemTemplate>
                            <%# Eval("descripcion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Concepto" FooterStyle-Wrap="true">
                        <ItemTemplate>
                            <%# Eval("concepto") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="false" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("valor","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retención" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("retencion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parcial" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("parcial","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("total","{0:#,##0.##}".ToString()) %>
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

