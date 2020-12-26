<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="cierreEgresos.aspx.cs"
    Inherits="Egreso_cierreEgresos" EnableEventValidation="false" %>

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
            <legend>Cierre de egresos</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                 <asp:Panel ID="pnCaja" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCaja" runat="server">
                        <asp:ListItem Value="S">S</asp:ListItem>
                        <asp:ListItem Value="K">K</asp:ListItem>
                        <asp:ListItem Value="P">P</asp:ListItem>
                        <asp:ListItem Value="X">X</asp:ListItem>
                        <asp:ListItem Value="Y">Y</asp:ListItem>
                        <asp:ListItem Value="Z">Z</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

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



               
               

                 <asp:Panel ID="pnImprimir" CssClass="" runat="server" Visible="true">
                      <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnConsultar_Click" />
                <asp:Button ID="btnImprimir" runat="server" CssClass="btnProceso"  Text="Imprimir documentos SRI" OnClick="btnImprimir_Click" />
                <asp:Button ID="btnImprimirAuto" runat="server" CssClass="btnProceso" Text="Imprimirautoconsumos" OnClick="btnImprimirAuto_Click"  />
                <asp:Button ID="btnMemos" runat="server" CssClass="btnProceso"  Text="Imprimir documentos sin validez tributaria" OnClick="btnMemos_Click" />
               
                     </asp:Panel>




            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCajas" CssClass="" runat="server" Visible="true">

        <fieldset id="fdCajas" class="fieldset-principal">
            <legend>Cajas</legend>
            <asp:GridView ID="grvEgresosCabecera" runat="server" AutoGenerateColumns="False" BackColor="blue"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="20%" AllowSorting="True" PageSize="100" DataKeyNames="numero" AutoGenerateSelectButton="True"
                Height="157px" OnSelectedIndexChanged="grvEgresosCabecera_SelectedIndexChanged">

                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

                    <asp:TemplateField HeaderText="Documento" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("numero") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("fecha","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("estado") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Documento" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("totalEgreso","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Retenido" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("totalRetencion","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total en efectivo" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("totalPagado","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total no efectivo" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("totalPagadoOtros","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción" FooterStyle-Wrap="false" ItemStyle-Wrap="False">
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

    <asp:Panel ID="pnCabeceraCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="fdCabeceraCaja" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnDatosCierre" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9" >
                <asp:Label ID="lblNumero" CssClass="lblPeq" runat="server" Text="Código" Visible="true"></asp:Label>
                <asp:TextBox ID="txtNumero" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" 
                    AutoPostBack="True"  placeholder="Código"></asp:TextBox>
                <asp:Label ID="lblFecha" CssClass="lblPeq" runat="server" Text="Fecha" Visible="true"></asp:Label>
                <asp:TextBox ID="txtFecha" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblEstado" CssClass="lblPeq" runat="server" Text="Estado" Visible="true"></asp:Label>
                <asp:TextBox ID="txtEstado" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblDescripcion" CssClass="lblPeq" runat="server" Text="Descripcion" Visible="true"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="pnSubtotales1" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                <asp:Label ID="lblTdocuemnto" CssClass="lblPeq" runat="server" Text="Documento" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTdocumento" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblTretenido" CssClass="lblPeq" runat="server" Text="Retenido" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTretenido" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblTefectivo" CssClass="lblPeq" runat="server" Text="Efectivo" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTefectivo" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblTnoefectivo" CssClass="lblPeq" runat="server" Text="No efectivo" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTnoefectivo" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="Panel1" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                <asp:Button ID="btnCerrar" runat="server" CssClass="btnProceso" Text="Cerrar caja" 
                    OnClick="btnCerrar_Click"  />
                <asp:Button ID="btnRegresar" runat="server" CssClass="btnProceso" Text="regresar" 
                    OnClick="btnRegresar_Click"  />

            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDetalleCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleCaja" class="fieldset-principal">
            <legend></legend>
            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvEgresosDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center" Width="90%"
            AllowSorting="True" PageSize="50" >
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="# Documento" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("numeroDocumento") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.U.C./C.C." ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("ruc") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Autorización" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("autorizacion") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor factura" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("valorFactura","{0:F2}".ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor retención" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("valorRetencion","{0:F2}".ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A pagar" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("apagar","{0:F2}".ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Concepto" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("concepto") %>
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

