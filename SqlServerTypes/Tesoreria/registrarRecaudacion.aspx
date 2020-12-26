<%@ Page Title="" Language="C#" MasterPageFile="~/Tesoreria/mpTesoreria.master" AutoEventWireup="true"
    CodeFile="registrarRecaudacion.aspx.cs" Inherits="Tesoreria_registrarRecaudacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true" Style="color: red; font-size: 2rem;"></asp:Label>
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
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
                <asp:Label ID="lblNumfactura" runat="server" CssClass="lblPeq" Text="# factura" Visible="false"></asp:Label>
                <asp:TextBox ID="txtNumFactura" runat="server" CssClass="" placeholder="# factura"></asp:TextBox>

            </asp:Panel>
            <asp:Panel ID="pnBotones" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnListar" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnListar_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListadoFactura" CssClass="" runat="server" Visible="true">

        <fieldset id="fsListadoFactura" class="fieldset-principal">
            <legend>Facturas autorizadas</legend>
            <asp:GridView ID="grvListadoFac" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="5" GridLines="Vertical" HorizontalAlign="Center" Width="90%"
                AllowSorting="True" PageSize="5"
                OnSelectedIndexChanged="grvListadoFac_SelectedIndexChanged"
                DataKeyNames="FAC_SECUENCIAL">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="true" />
                    <asp:TemplateField HeaderText="Secuencial" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_SECUENCIAL") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_FECHAEMISION","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cliente" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_RAZONCOMPRADOR") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_TOTALSINIMP","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descuento" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_TOTALDESCUENTO","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio" FooterStyle-Wrap="False" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_BASEIMPONIBLE","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="I.V.A." FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_VALORIMPUESTO","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Factura" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_IMPORTETOTAL","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Recauda" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_RECAUDADO","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retención IVA" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_RETENIDOIVA","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retencion FTE." FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_RETENIDOFUENTE","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saldo" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FAC_SALDO","{0:F2}".ToString()) %>
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

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Registrar pago</legend>
            <asp:Label ID="lblTitulo" CssClass="lblForm" runat="server" Text="# Factura" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="TextBox14" CssClass="txtForm" runat="server" BorderWidth="0px"></asp:TextBox>
            <asp:Label ID="lblValorFactura" CssClass="lblForm" runat="server" Text="Valor factura"></asp:Label>
            <asp:TextBox ID="txtValorFactura" CssClass="txtForm" runat="server" placeholder="Valor de la factura"></asp:TextBox>
            <asp:Label ID="lblValorRetencionFUENTE" CssClass="lblForm" runat="server" Text="Valor retención FTE"></asp:Label>
            <asp:TextBox ID="txtValorRetencionFUENTE" CssClass="txtForm" runat="server" Enabled="true" placeholder="retención en la Fuente"></asp:TextBox>
            <asp:Label ID="lblValorRetencionIVA" CssClass="lblForm" runat="server" Text="Valor retención IVA"></asp:Label>
            <asp:TextBox ID="txtValorRetencionIVA" CssClass="txtForm" runat="server" Enabled="true" placeholder="Retención IVA"></asp:TextBox>
            <asp:Label ID="lblTipoPago" CssClass="lblForm" runat="server" Text="Tipo de pago"></asp:Label>
            <asp:Panel runat="server">
                <asp:DropDownList ID="ddlTipoPago" runat="server" DataTextField="descripcion" DataValueField="id" CssClass="ddlForm"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPago_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>

            <div ID="Panel1" runat="server" Style="display:flex;">
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" Visible="true" CssClass="lblForm"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server" Visible="True" placeholder="Descripción" AutoPostBack="True"></asp:TextBox>
            </div>

            <div ID="pnDescripcionpago" runat="server" Style="display: flex;">
                <asp:Label ID="lblDescripcion2" runat="server" Text="Descripción" Visible="false"  CssClass="lblForm"></asp:Label>
                <asp:Panel ID="pnDescripcion" runat="server">
                    <asp:DropDownList ID="ddlDescripcion" runat="server" DataTextField="descripcion" DataValueField="id"
                        CssClass="ddlForm" Visible="False" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlDescripcion_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
            </div>

            <div id="pnNumDocumento" runat="server" style="display: flex;">
                <asp:Label ID="lblNumDocumento" CssClass="lblForm" runat="server" Text="# Documento"></asp:Label>
                <asp:TextBox ID="txtNumDocumento" CssClass="txtForm" runat="server" placeholder="# Documento" AutoPostBack="True"></asp:TextBox>
            </div>
            <asp:Panel ID="pnOpcion" CssClass="pnAccion" runat="server">
                <asp:Button ID="btnretencion" CssClass="btnForm" runat="server" Text="Retención" OnClick="btnretencion_Click"
                    Visible="false" />
                <asp:Button ID="btnRegistrar" CssClass="btnForm" runat="server" Text="Registrar pago" OnClick="btnRegistrar_Click" />
                <asp:Button ID="btnCancelarRegistro" CssClass="btnForm" runat="server" Text="Regresar" OnClick="btnCancelarRegistro_Click" />
                <asp:Label ID="lblRegistro" CssClass="lblForm" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnRetencion" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Retención</legend>


            <asp:Label ID="Label1" CssClass="lblForm" runat="server" Text="Retención" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="TextBox15" CssClass="txtForm" runat="server" BorderWidth="0px"></asp:TextBox>

            <asp:Label ID="Label3" CssClass="lblForm" runat="server" Text="Serie"></asp:Label>
            <asp:TextBox ID="TextBox1" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label4" CssClass="lblForm" runat="server" Text="# Retención"></asp:Label>
            <asp:TextBox ID="TextBox2" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label5" CssClass="lblForm" runat="server" Text="B.I. Bien"></asp:Label>
            <asp:TextBox ID="TextBox3" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label6" CssClass="lblForm" runat="server" Text="B.I Servicio"></asp:Label>
            <asp:TextBox ID="TextBox4" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label7" CssClass="lblForm" runat="server" Text="% Fuente servicios"></asp:Label>
            <asp:TextBox ID="TextBox5" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label8" CssClass="lblForm" runat="server" Text="Valor Retenido"></asp:Label>
            <asp:TextBox ID="TextBox6" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="Label9" CssClass="lblForm" runat="server" Text="% IVA servicios"></asp:Label>
            <asp:TextBox ID="TextBox7" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label10" CssClass="lblForm" runat="server" Text="Valor retenido"></asp:Label>
            <asp:TextBox ID="TextBox8" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="Label11" CssClass="lblForm" runat="server" Text="% Fuente bienes"></asp:Label>
            <asp:TextBox ID="TextBox9" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label12" CssClass="lblForm" runat="server" Text="Valor retenido"></asp:Label>
            <asp:TextBox ID="TextBox10" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="Label13" CssClass="lblForm" runat="server" Text="% IVA bienes"></asp:Label>
            <asp:TextBox ID="TextBox11" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="Label14" CssClass="lblForm" runat="server" Text="Valor retenido"></asp:Label>
            <asp:TextBox ID="TextBox12" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="Label15" CssClass="lblForm" runat="server" Text="Total retenido"></asp:Label>
            <asp:TextBox ID="TextBox13" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Panel ID="Panel3" CssClass="pnAccion" runat="server">
                <asp:Button ID="btnGuardarRetencion" CssClass="btnForm" runat="server" Text="Guardar" />
                <asp:Button ID="btnCancelarRetencion" CssClass="btnForm" runat="server" Text="Regresar" OnClick="btnCancelarRetencion_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnRecaudacion" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset3" class="fieldset-principal">
            <legend>Pagos registrados</legend>

            <asp:GridView ID="grvRecaudacion" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowSorting="True" PageSize="5" OnSelectedIndexChanged="grvRecaudacion_SelectedIndexChanged"
                OnRowDeleting="grvRecaudacion_RowDeleting" DataKeyNames="id_DetRecaudacion">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="true" />
                    <asp:TemplateField HeaderText="Código" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("id_DetRecaudacion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Factura" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("factura") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("valor","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retención IVA" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("retencionIVA","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retención FTE." FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("retencionFUENTE","{0:F2}".ToString()) %>
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
                    <asp:TemplateField HeaderText="# Documento" FooterStyle-Wrap="False">
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

    <asp:Panel ID="pnEliminar" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset4" class="fieldset-principal">
            <legend>Eliminar pago</legend>
            <asp:Label ID="lblEliminarPago" CssClass="lblForm" runat="server" Text="" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="TextBox16" CssClass="txtForm" runat="server" BorderWidth="0px"></asp:TextBox>

            <asp:Label ID="lblFactura" CssClass="lblForm" runat="server" Text="# Factura"></asp:Label>
            <asp:TextBox ID="txtFactura" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblValor" CssClass="lblForm" runat="server" Text="Valor"></asp:Label>
            <asp:TextBox ID="txtValor" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblRetencionFUENTE" CssClass="lblForm" runat="server" Text="RetenciónFTE"></asp:Label>
            <asp:TextBox ID="txtRetencionFUENTE" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblRetencionIVA" CssClass="lblForm" runat="server" Text="RetenciónIVA"></asp:Label>
            <asp:TextBox ID="txtRetencionIVA" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblTipoDescripcion" CssClass="lblForm" runat="server" Text="Tipo de pago"></asp:Label>
            <asp:TextBox ID="txtTipoDescripcion" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblTipoDetalle" CssClass="lblForm" runat="server" Text="Descrpción"></asp:Label>
            <asp:TextBox ID="txtTipoDetalle" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:Label ID="lblDocumento" CssClass="lblForm" runat="server" Text="# Documento"></asp:Label>
            <asp:TextBox ID="txtDocumento" CssClass="txtForm" runat="server" Enabled="False"></asp:TextBox>
            <asp:TextBox ID="txtIdDetalle" CssClass="txtForm" runat="server" Enabled="False" Visible="false"></asp:TextBox>
            <asp:Panel ID="Panel2" CssClass="pnAccion" runat="server">
                <asp:Button ID="btnEliminarPago" CssClass="btnForm" runat="server" Text="Eliminar pago" OnClick="btnEliminarPago_Click" />
                <asp:Button ID="btnCancelarEliminar" CssClass="btnForm" runat="server" Text="Regresar" OnClick="btnCancelarEliminar_Click" />

            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnCXPProveedor" CssClass="" runat="server" Visible="false">
        <fieldset id="fsProveedor" class="fieldset-principal">
            <legend>Cruce cuentas con proveedores</legend>
            <asp:Panel runat="server" ID="pnproveedor">
                <asp:TextBox ID="txtProveedor" CssClass="txtForm" runat="server" Enabled="true" AutoPostBack="True"></asp:TextBox>
                <asp:Button runat="server" ID="btnproveedor" Text="Buscar" OnClick="btnproveedor_Click" />
                <asp:Button runat="server" ID="btnRegresar" Text="Regresar" OnClick="btnRegresar_Click" />
            </asp:Panel>

            <asp:Panel runat="server" ID="pnFacturasProveedor">
                <asp:GridView ID="grvFacturasProveedor" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="id_detegresos"
                    ShowHeaderWhenEmpty="True"
                    OnRowCommand="grvFacturasProveedor_RowCommand"
                    OnRowEditing="grvFacturasProveedor_RowEditing"
                    OnRowCancelingEdit="grvFacturasProveedor_RowCancelingEdit"
                    OnRowUpdating="grvFacturasProveedor_RowUpdating"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />

                    <Columns>

                        <asp:BoundField DataField="RAZONSOCIAL" HeaderText="PROVEEDOR" ReadOnly="True" />
                        <asp:BoundField DataField="NOM_SUC" HeaderText="NOMBRE/SUCURSAL" ReadOnly="True" />
                        <asp:BoundField DataField="NUMERODOCUMENTO" HeaderText="# DOCUMENTO" ReadOnly="True" />
                        <asp:BoundField DataField="SALDOCXP" HeaderText="SALDO" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="VALORFACTURA" HeaderText="VALOR/DOCUMENTO" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <asp:BoundField DataField="VALORFACTURA" HeaderText="VALOR" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" Visible="false" />
                        <asp:BoundField DataField="TOTALPAGADO" HeaderText="TOTAL PAGADO" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="APAGAR" HeaderText="TOTAL A PAGAR" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="SUCURSAL" HeaderText="ESTABLECIMIENTO" ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="RUC" HeaderText="IDENTIDAD" ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" ReadOnly="True" Visible="false" />



                        <asp:TemplateField HeaderText="ABONO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("abono") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtAbono" Text='<%# Eval("abono","{0:F2}".ToString()) %>' ItemStyle-HorizontalAlign="Right"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/images/iconos/086.ico" ID="imgbEditar" runat="server" CommandName="Edit" ToolTip="Editar" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/images/iconos/grabar.ico" ID="imgbGrabar" runat="server" CommandName="Update" ToolTip="Grabar" Width="20px" Height="20px" />
                                <asp:ImageButton ImageUrl="~/images/iconos/cancelar.jpg" ID="imgbCancelar" runat="server" CommandName="Cancel" ToolTip="Cancelar" Width="20px" Height="20px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>



        </fieldset>
    </asp:Panel>




    <asp:Panel ID="pnClienteCXC" CssClass="" runat="server" Visible="false">
        <fieldset id="fsClienteCXC" class="fieldset-principal">
            <legend>Cruce cuentas con clientes (Convenios)</legend>
            <asp:Panel runat="server" ID="Panel5">
                <asp:TextBox ID="txtClienteCXC" CssClass="txtForm" runat="server" Enabled="true" AutoPostBack="True"></asp:TextBox>
                <asp:Button runat="server" ID="btnClienteCXC" Text="Buscar" OnClick="btnClienteCXC_Click" />
                <asp:Button runat="server" ID="btnRegresarCXC" Text="Regresar" OnClick="btnRegresarCXC_Click" />
            </asp:Panel>

            <asp:Panel runat="server" ID="pnClientesCXC">
                <asp:GridView ID="grvClientesCXC" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="id_AbonoClienteCabecera"
                    ShowHeaderWhenEmpty="True"
                    OnRowCommand="grvClientesCXC_RowCommand"
                    OnRowEditing="grvClientesCXC_RowEditing"
                    OnRowCancelingEdit="grvClientesCXC_RowCancelingEdit"
                    OnRowUpdating="grvClientesCXC_RowUpdating"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />

                    <Columns>
                        <asp:BoundField DataField="NOMBRE" HeaderText="Cliente" ReadOnly="True" />
                        <asp:BoundField DataField="NOM_SUC" HeaderText="NOMBRE/SUCURSAL" ReadOnly="True" />
                        <asp:BoundField DataField="DOCUMENTOUTILIZADO" HeaderText="# DOCUMENTO" ReadOnly="True" />
                        <asp:BoundField DataField="SALDOCXC" HeaderText="SALDO" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TOTALFACTURA" HeaderText="VALOR/DOCUMENTO" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" Visible="true" />
                        <asp:BoundField DataField="TOTALABONO" HeaderText="TOTAL PAGADO" ReadOnly="True" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="SUCURSAL" HeaderText="ESTABLECIMIENTO" ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="RUC" HeaderText="IDENTIDAD" ReadOnly="True" Visible="false" />
                        <asp:BoundField DataField="FECHA" HeaderText="FECHA" ReadOnly="True" Visible="false" />



                        <asp:TemplateField HeaderText="ABONO">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("abonoCXC") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="txtAbonoCXC" Text='<%# Eval("abonoCXC","{0:F2}".ToString()) %>' ItemStyle-HorizontalAlign="Right"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/images/iconos/086.ico" ID="imgcEditar" runat="server" CommandName="Edit" ToolTip="EditarCXC" Width="20px" Height="20px" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/images/iconos/grabar.ico" ID="imgcGrabar" runat="server" CommandName="Update" ToolTip="Grabar" Width="20px" Height="20px" />
                                <asp:ImageButton ImageUrl="~/images/iconos/cancelar.jpg" ID="imgcCancelar" runat="server" CommandName="Cancel" ToolTip="Cancelar" Width="20px" Height="20px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>


        </fieldset>
    </asp:Panel>

</asp:Content>

