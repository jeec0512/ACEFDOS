<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="controlCajas.aspx.cs"
    Inherits="Egreso_controlCajas" EnableEventValidation="false" %>

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
            <legend>Control de cajas</legend>
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



                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar"
                    OnClick="btnConsultar_Click" />




            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCajas" CssClass="" runat="server" Visible="true">

        <fieldset id="fdCajas" class="fieldset-principal">
            <legend>Cajas</legend>
            <asp:GridView ID="grvEgresosCabecera" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5"
                GridLines="Vertical" HorizontalAlign="Center"
                Width="20%" AllowSorting="True" PageSize="2" DataKeyNames="numero" AutoGenerateSelectButton="false"
                Height="157px"
                OnRowCommand="grvEgresosCabecera_RowCommand">

                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Ver caja" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/219_2.png"
                        CommandName="modDoc" ItemStyle-Width="60" />

                    <asp:BoundField DataField="id_CabEgresos" HeaderText="id" Visible="true" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone"/>
                    <asp:BoundField DataField="numero" HeaderText="Documento" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="totalEgreso" HeaderText="Total Documento" />
                    <asp:BoundField DataField="totalRetencion" HeaderText="Total Retenido" />
                    <asp:BoundField DataField="totalPagado" HeaderText="Total en efectivo" />
                    <asp:CheckBoxField DataField="revisado" HeaderText="Revisado" />
                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                        CommandName="Rev" ItemStyle-Width="60" />



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
            <asp:Button ID="Button_responder" runat="server" Text="Responder" CssClass="boton_general" Width="140px"
                CommandName="boton"
                OnRowCommand="<%# ((GridViewRow) Container).RowIndex %>" />
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCabeceraCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="fdCabeceraCaja" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnDatosCierre" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                <asp:Label ID="lblNumero" CssClass="lblPeq" runat="server" Text="Código" Visible="true"></asp:Label>
                <asp:TextBox ID="txtNumero" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"
                    AutoPostBack="True" placeholder="Código"></asp:TextBox>
                <asp:Label ID="lblFecha" CssClass="lblPeq" runat="server" Text="Fecha" Visible="true"></asp:Label>
                <asp:TextBox ID="txtFecha" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblEstado" CssClass="lblPeq" runat="server" Text="Estado" Visible="true"></asp:Label>
                <asp:TextBox ID="txtEstado" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblDescripcion" CssClass="lblPeq" runat="server" Text="Descripcion" Visible="true"></asp:Label>
                <asp:TextBox ID="txtDescripcion" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="pnSubtotales1" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                <asp:Label ID="lblTdocuemnto" CssClass="lblPeq" runat="server" Text="Documento" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTdocumento" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblTretenido" CssClass="lblPeq" runat="server" Text="Retenido" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTretenido" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblTefectivo" CssClass="lblPeq" runat="server" Text="Efectivo" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTefectivo" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>
                <asp:Label ID="lblTnoefectivo" CssClass="lblPeq" runat="server" Text="No efectivo" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTnoefectivo" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="Panel1" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                <asp:Button ID="btnCerrar" runat="server" CssClass="btnProceso" Text="Cerrar caja"
                    OnClick="btnCerrar_Click" />
                <asp:Button ID="btnRegresar" runat="server" CssClass="btnProceso" Text="regresar"
                    OnClick="btnRegresar_Click" />

            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDetalleCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleCaja" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvEgresosDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvEgresosDetalle_RowCommand">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:ButtonField HeaderText="Ver retención" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/ohist.ico"
                        CommandName="verRet" ItemStyle-Width="60" />

                    <asp:BoundField DataField="id_detEgresos" HeaderText="id" Visible="true" />
                    <asp:BoundField DataField="ruc" HeaderText="R.U.C." Visible="true" />
                    <asp:BoundField DataField="nombres" HeaderText="Razón social" Visible="false" />
                    <asp:BoundField DataField="concepto" HeaderText="Concepto" Visible="true" />
                    <asp:BoundField DataField="numeroDocumento" HeaderText="# Documento" Visible="true" />
                    <asp:BoundField DataField="valorFactura" HeaderText="Valor Doc." Visible="true" />
                    <asp:BoundField DataField="ivaBien" HeaderText="IVA Bien" Visible="true" />
                    <asp:BoundField DataField="ivaServicio" HeaderText="IVA servicio" Visible="true" />
                    <asp:BoundField DataField="valorRetencion" HeaderText="Total retenido" Visible="true" />
                    <asp:BoundField DataField="apagar" HeaderText="Valor pagado" Visible="true" />
                    <asp:BoundField DataField="numRetencion" HeaderText="# retención" Visible="true" />
                    <asp:BoundField DataField="doc_autorizacion" HeaderText="# Autoriz/Doc" Visible="true" />
                    <asp:BoundField DataField="numAutorizacionRetencion" HeaderText="# Aut/retención" Visible="true" />
                    <asp:BoundField DataField="var_gen" HeaderText="Bien CodBle" Visible="true" />
                    <asp:BoundField DataField="mae_gas" HeaderText="Bien CodGasto" Visible="true" />
                    <asp:BoundField DataField="subtotalBien" HeaderText="SubTBien" Visible="true" />
                    <asp:BoundField DataField="svar_gen" HeaderText="Serv. CodBle" Visible="true" />
                    <asp:BoundField DataField="smae_gas" HeaderText="Serv. CodGasto" Visible="true" />
                    <asp:BoundField DataField="subtotalServicio" HeaderText="SubTBien" Visible="true" />



                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="true" />

                   <asp:CheckBoxField DataField="justificado" HeaderText="Justificado" />
                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                        CommandName="Jus" ItemStyle-Width="60" />

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


    <asp:Panel ID="pnRetencion" CssClass="" runat="server" Visible="false">

        <fieldset id="fdRetencion" class="fieldset-principal">
            <legend>Retención</legend>
            <asp:Panel ID="pnAneta" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" BorderStyle="Solid"
                ForeColor="#1d92e9">
                <asp:TextBox ID="txtRazonSocial" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                <asp:Label ID="lblDirMatriz" CssClass="lblPeq" runat="server" Text="Dir.Matriz" Visible="true"></asp:Label>
                <asp:TextBox ID="txtDirMatriz" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblDirSucursal" CssClass="lblPeq" runat="server" Text="Dir.Sucursal" Visible="true"></asp:Label>
                <asp:TextBox ID="txtDirSucursal" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblContEspecial" CssClass="lblPeq" runat="server" Text="Contribuyente especial Nro." Visible="true"></asp:Label>
                <asp:TextBox ID="txtContEspecial" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblObligado" CssClass="lblPeq" runat="server" Text="obligado a llevar Contabilidad" Visible="true"></asp:Label>
                <asp:TextBox ID="txtObligado" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnProveedor" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" BorderStyle="Solid"
                ForeColor="#1d92e9">
                <asp:TextBox ID="txtRuc" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                <asp:Label ID="lblNumRetencion" CssClass="lblPeq" runat="server" Text="# Retención" Visible="true"></asp:Label>
                <asp:TextBox ID="txtNumRetencion" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                <asp:Label ID="lblProveedor" CssClass="lblPeq" runat="server" Text="Razón Social/Nombres y apellidos"
                    Visible="true"></asp:Label>
                <asp:TextBox ID="txtProveedor" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblRucproveedor" CssClass="lblPeq" runat="server" Text="RUC/CI:" Visible="true"></asp:Label>
                <asp:TextBox ID="txtRucproveedor" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"
                    AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblFechaEmision" CssClass="lblPeq" runat="server" Text="Fecha Emisión" Visible="true"></asp:Label>
                <asp:TextBox ID="txtFechaEmision" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

            </asp:Panel>

            <asp:Panel ID="pnAdicional" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" BorderStyle="Solid"
                ForeColor="#1d92e9">
                <asp:Label ID="lblDireccionproveedor" CssClass="lblPeq" runat="server" Text="Dirección:" Visible="true"></asp:Label>
                <asp:TextBox ID="txtDireccionproveedor" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                <asp:Label ID="lblTelefonoProveedor" CssClass="lblPeq" runat="server" Text="Teléfono:" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTelefonoProveedor" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"
                    AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblEmailProveedor" CssClass="lblPeq" runat="server" Text="E-mail" Visible="true"></asp:Label>
                <asp:TextBox ID="txtEmailProveedor" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"
                    AutoPostBack="True"></asp:TextBox>

                <asp:Label ID="lblAdicional" CssClass="lblPeq" runat="server" Text="Descripción" Visible="true"></asp:Label>
                <asp:TextBox ID="txtAdicional" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

            </asp:Panel>

        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDetalleRetencion" CssClass="" runat="server" Visible="true">
        <fieldset id="fdDetalleRetencion" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvDetalleRetenciones" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5"
                GridLines="Vertical" HorizontalAlign="Center"
                Width="20%" AllowSorting="True" PageSize="2" DataKeyNames="numero" AutoGenerateSelectButton="false"
                Height="157px"
                OnRowCommand="grvEgresosCabecera_RowCommand">

                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="codDocSustento" HeaderText="id" Visible="true" />
                    <asp:BoundField DataField="numDocSustento" HeaderText="Documento" />
                    <asp:BoundField DataField="fechaEmision" HeaderText="Fecha" />
                    <asp:BoundField DataField="periodoFiscal" HeaderText="Estado" />
                    <asp:BoundField DataField="baseImponible" HeaderText="Total Documento" />
                    <asp:BoundField DataField="codigo" HeaderText="Total Retenido" />
                    <asp:BoundField DataField="valorRetenido" HeaderText="Total en efectivo" />
                    <asp:CheckBoxField DataField="valor" HeaderText="Revisado" />



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

