<%@ Page Title="" Language="C#" MasterPageFile="~/Tesoreria/mpTesoreria.master" AutoEventWireup="true"
    CodeFile="cierreVentas.aspx.cs" Inherits="Tesoreria_cierreVentas" EnableEventValidation="false" %>

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
            <asp:Panel ID="pnBotones" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnListar" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnListar_Click"
                    UseSubmitBehavior="False" />
                <br />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCabecerarecaudacion" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Listado de cajas</legend>
            <asp:GridView ID="grvRecaudacionCabecera" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="20%" AllowSorting="True"
                PageSize="5" DataKeyNames="id_DetRecaudacion" AutoGenerateSelectButton="True" Height="157px" OnSelectedIndexChanged="grvRecaudacionCabecera_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

                    <asp:TemplateField HeaderText="Documento" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("NUMERO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FECHA","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción" FooterStyle-Wrap="false" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("DESCRIPCION") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total ingresos" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("TOTALINGRESOS","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total retenciones IVA" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("TOTALRETENCIONIVA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total retenciones fuente" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("TOTALRETENCIONFUENTE","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gasto autorizado" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("GTOAUTORIZADO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total provisión" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("TOTALPROVISION","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("ESTADO") %>
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

    <asp:Panel ID="pnAltas" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Facturas emitidas</legend>

            <asp:Panel ID="Panel1" CssClass="pnPeq" runat="server" Visible="true">
                <asp:Label ID="lblTitulo" CssClass="lblPeq" runat="server" Text="# Documento"></asp:Label>
                <asp:TextBox ID="txtTitulo" CssClass="txtPeq" runat="server" Enabled="false" BorderWidth="0px"></asp:TextBox>
                <asp:Label ID="lblFecha" CssClass="lblPeq" runat="server" Text="Fecha"></asp:Label>
                <asp:TextBox ID="txtFecha" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblDescripcion" CssClass="lblPeq" runat="server" Text="Descripción"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtPeq" runat="server" Enabled="true"></asp:TextBox>
                <asp:Label ID="lblTotalIngresos" CssClass="lblPeq" runat="server" Text="T.Ingresos"></asp:Label>
                <asp:TextBox ID="txtTotalIngresos" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalRetencionIva" CssClass="lblPeq" runat="server" Text="Ret.IVA"></asp:Label>
                <asp:TextBox ID="txtTotalRetencionIva" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalRetencionFuente" CssClass="lblPeq" runat="server" Text="Ret.Fuente"></asp:Label>
                <asp:TextBox ID="txtTotalRetencionFuente" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblValorDeposito" CssClass="lblPeq" runat="server" Text="Valor/depósito"></asp:Label>
                <asp:TextBox ID="txtValorDeposito" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblGtoAutorizado" CssClass="lblPeq" runat="server" Text="Gasto/autoriz."></asp:Label>
                <asp:TextBox ID="txtGtoAutorizado" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblProvision" CssClass="lblPeq" runat="server" Text="Provisión"></asp:Label>
                <asp:TextBox ID="txtProvision" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblEstado" CssClass="lblPeq" runat="server" Text="Estado"></asp:Label>
                <asp:TextBox ID="txtEstado" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="Panel2" CssClass="pnPeq" runat="server" Visible="true">
                <asp:Label ID="lblTotalEfectivo" CssClass="lblPeq" runat="server" Text="T. efectivo"></asp:Label>
                <asp:TextBox ID="txtTotalEfectivo" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalCheques" CssClass="lblPeq" runat="server" Text="T. cheques"></asp:Label>
                <asp:TextBox ID="txtTotalCheques" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTarjetas" CssClass="lblPeq" runat="server" Text="T. tarjetas"></asp:Label>
                <asp:TextBox ID="txtTarjetas" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalAutoconsumo" CssClass="lblPeq" runat="server" Text="T.ANETA"></asp:Label>
                <asp:TextBox ID="txtTotalAutoconsumo" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblNotaCredito" CssClass="lblPeq" runat="server" Text="T. N/C"></asp:Label>
                <asp:TextBox ID="txtNotaCredito" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblNomina" CssClass="lblPeq" runat="server" Text="T. nómina"></asp:Label>
                <asp:TextBox ID="txtNomina" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTransferencia" CssClass="lblPeq" runat="server" Text="T.Transfer"></asp:Label>
                <asp:TextBox ID="txtTransferencia" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblDebito" CssClass="lblPeq" runat="server" Text="Tarj/débito"></asp:Label>
                <asp:TextBox ID="txtDebito" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblcxc" CssClass="lblPeq" runat="server" Text="T. CXC"></asp:Label>
                <asp:TextBox ID="txtcxc" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblVarios" CssClass="lblPeq" runat="server" Text="T. varios"></asp:Label>
                <asp:TextBox ID="txtVarios" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="pnDepositos" CssClass="pnPeq" runat="server" Visible="true">
                <asp:Label ID="lblBanco" CssClass="lblPeq" runat="server" Text="Banco/TC"></asp:Label>

                <asp:DropDownList ID="ddlBanco" runat="server" DataTextField="descripcion" DataValueField="id" CssClass="ddlForm"
                    AutoPostBack="True">
                </asp:DropDownList>

                <asp:Label ID="lblNumeroDeposito" CssClass="lblPeq" runat="server" Text="Ref/Depós/TC"></asp:Label>
                <asp:TextBox ID="txtNumeroDeposito" CssClass="txtPeq" runat="server" Enabled="true"></asp:TextBox>
                <asp:Label ID="lblValorDepositado" CssClass="lblPeq" runat="server" Text="Valor"></asp:Label>
                <asp:TextBox ID="txtValorDepositado" CssClass="txtPeq" runat="server" Enabled="true"></asp:TextBox>
                <asp:Label ID="lblDescripcionDeposito" CssClass="lblPeq" runat="server" Text="Observación"></asp:Label>
                <asp:TextBox ID="txtDescripcionDeposito" CssClass="txtPeq" runat="server" Enabled="true"></asp:TextBox>
                <asp:Button ID="btnAniadirDeposito" runat="server" CssClass="lblPeq" Text="Añadir depósito" Visible="true"
                    OnClick="btnAniadirDeposito_Click" />
                <asp:Panel ID="Panel5" CssClass="pnPeq" runat="server" Visible="true" Wrap="False" GroupingText="Registro de depósitos">
                    <asp:Panel ID="pnMensajeDeposito" CssClass="pnABCdivision" runat="server" Visible="true">
                        <asp:GridView ID="grvDepositos" runat="server" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5" DataKeyNames="id_DepositoBancario"
                            GridLines="Vertical" Height="157px" HorizontalAlign="Center" OnSelectedIndexChanged="grvDepositos_SelectedIndexChanged"
                            PageSize="5" Width="20%">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <Columns>
                                <asp:TemplateField FooterStyle-Wrap="False" HeaderText="Código">
                                    <ItemTemplate>
                                        <%# Eval("id_depositoBancario") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Wrap="False" HeaderText="# de cuenta">
                                    <ItemTemplate>
                                        <%# Eval("cuenta") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Wrap="False" HeaderText="# de depósito">
                                    <ItemTemplate>
                                        <%# Eval("deposito") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Wrap="False" HeaderText="Valor">
                                    <ItemTemplate>
                                        <%# Eval("valor","{0:#,##0.##}".ToString()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Wrap="False" HeaderText="Observación">
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
                        <asp:Panel ID="pnEliminarDeposito" runat="server" Visible="false">
                            <asp:Label ID=Label1 runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnEliminarDeposito" CssClass="btnForm" runat="server" Text="Eliminar" OnClick="btnEliminarDeposito_Click" />
                            <asp:Button ID="btnCancelarDeposito" CssClass="btnForm" runat="server" Text="Regresar" OnClick="btnCancelarDeposito_Click" />
                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnMedio" runat="server">
                <fieldset id="Fieldset4" class="fieldset-principal">
                    <legend></legend>


                    <asp:Button ID="btnVerificar" CssClass="btnProceso" runat="server" Text="Verificar" Visible="true" OnClick="btnVerificar_Click" />
                    <asp:Button ID="btnCierreParcial" CssClass="btnProceso" runat="server" Text="Cierre parcial" Visible="true"
                        OnClick="btnCierreParcial_Click" />
                    <asp:Button ID="btnCierreTotal" CssClass="btnProceso" runat="server" Text="Cierre total" Visible="true"
                        OnClick="btnCierreTotal_Click" />
                    <asp:Button ID="btnCancelarRegistro" CssClass="btnProceso" runat="server" Text="Regresar" OnClick="btnCancelarRegistro_Click" />
                    <asp:Label ID="lblStatus" CssClass="lblPeq" runat="server" Text=""></asp:Label>

                </fieldset>
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnRecaudacion" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset3" class="fieldset-principal">
            <legend>Pagos registrados</legend>
            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />
            <asp:GridView ID="grvRecaudacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowSorting="True" PageSize="50" DataKeyNames="id_DetRecaudacion">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Factura" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("factura") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("valor","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret. IVA" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("retencionIVA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ret. FTE." FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("retencionFUENTE","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("descripcionTipoPago") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detalle" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("descripcionTipoDetalle") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Documento" FooterStyle-Wrap="false" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("numeroDocumento") %>
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

