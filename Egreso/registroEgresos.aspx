<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="registroEgresos.aspx.cs"
    Inherits="Egreso_registroEgresos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">

    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>



    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false"
            OnClick="btnIngresaProv_Click" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHA  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="cabEgresos" class="fieldset-principal">
            <legend>Registro de egresos</legend>

            <asp:Panel ID="pnPrincipal" CssClass="pnBtnProcesos" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label>
                <asp:TextBox runat="server" ID="txtFecha"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFecha" Format="dd/MM/yyyy">
                </act1:CalendarExtender>


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

                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFecha" Mask="99/99/9999" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar"
                    OnClick="btnConsultar_Click" />

            </asp:Panel>

        </fieldset>


    </asp:Panel>

    <!-- LISTADO DE PAGOS!-->
    <asp:Panel ID="pnDetallePagos" runat="server" CssClass="" Visible="true">
        <fieldset id="listados" class="fieldset-principal">
            <legend>Listado de pagos</legend>

            <asp:GridView ID="grvDetallePagos" runat="server"
                AutoGenerateColumns="False"
                CellPadding="5"
                GridLines="Vertical"
                HorizontalAlign="Center"
                Width="100%"
                AllowPaging="True"
                AllowSorting="True"
                PageSize="100"
                DataKeyNames="id_DetEgresos" OnPageIndexChanged="grvDetallePagos_PageIndexChanged"
                OnPageIndexChanging="grvDetallePagos_PageIndexChanging"
                OnSelectedIndexChanged="grvDetallePagos_SelectedIndexChanged">

                <AlternatingRowStyle
                    BackColor="#DCDCDC" />

                <Columns>
                    <asp:CommandField
                        ShowSelectButton="true" />
                    <asp:TemplateField HeaderText="Código" Visible="false">
                        <ItemTemplate>
                            <%# Eval("id_DetEgresos") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Documento">
                        <ItemTemplate>
                            <%# Eval("descripcion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Documento">
                        <ItemTemplate>
                            <%# Eval("numeroDocumento") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="R.U.C./C.C.">
                        <ItemTemplate>
                            <%# Eval("ruc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Autorización">
                        <ItemTemplate>
                            <%# Eval("autorizacion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor factura">
                        <ItemTemplate>
                            <%# Eval("valorFactura","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor retención">
                        <ItemTemplate>
                            <%# Eval("valorRetencion","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="A pagar">
                        <ItemTemplate>
                            <%# Eval("apagar","{0:F2}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Concepto">
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
            <asp:Panel ID="pnMenu" CssClass="pnFormHijo" runat="server" Visible="true">
                <!-- BOTON PARA CREAR REGISTRO DE EGRESOS!-->
                <asp:Button ID="btnNuevoRegistro" runat="server" CssClass="btnProceso" Text="Nuevo Registro"
                    Visible="true" OnClick="btnNuevoRegistro_Click" />
                <asp:Button ID="btnRegresar" runat="server" CssClass="btnProceso" Text="Regresar" Visible="true" OnClick="btnRegresar_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <!-- INGRESO DE REGISTRO DE EGRESOS  !-->
    <asp:Panel ID="pnPagos" runat="server" CssClass="" Visible="true">
        <fieldset id="registrar" class="fieldset-principal">
            <legend>Registrar egreso</legend>


            <!-- se utiliza para procesos internos no se visializa  !-->
            <asp:Panel ID="pnDatosGenerales" CssClass="pnPeq" runat="server" Visible="false" GroupingText="" ForeColor="#1d92e9">
                <asp:Label ID="lblSucursal" CssClass="lblPeq" runat="server" Text="Sucursal" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSucursal" CssClass="txtPeq" runat="server" Visible="true"></asp:TextBox>
                <asp:Label ID="lblIFecha" CssClass="lblPeq" runat="server" Text="Fecha" Visible="true"></asp:Label>
                <asp:TextBox ID="txtIFecha" CssClass="txtPeq" runat="server" Visible="true"></asp:TextBox>

            </asp:Panel>

            <fieldset id="fsCaja" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="pnBienes" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblnumero" CssClass="lblPeq" runat="server" Text="Cód/caja" Visible="true"></asp:Label>!-->
                    <asp:TextBox ID="txtNumero" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>


                    <!--<asp:Label ID="lblTipoDocumento" CssClass="lblPeq" runat="server" Text="Tip/Doc."></asp:Label>!-->
                    <asp:Panel ID="pnTipoDoc" runat="server" CssClass="pnPeqDdl">
                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="peqDdl" DataTextField="descripcion"
                            DataValueField="id"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDocumento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>


                </asp:Panel>
                <asp:Panel ID="pnProveedor" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblColaborador" CssClass="lblPeq" runat="server" Text="Colaborador" Visible="true"></asp:Label>!-->
                    <asp:Panel ID="pnColaborador" runat="server" CssClass="pnPeqDdl" Visible="true">
                        <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="peqDdl" DataTextField="nombres" DataValueField="cedula"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlColaborador_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>
                    <!--<asp:Label ID="lblRuc" CssClass="lblPeq" runat="server" Text="R.U.C./C.C."></asp:Label>!-->
                    <asp:TextBox ID="txtRuc" CssClass="txtPeq" runat="server" AutoPostBack="True"
                        OnTextChanged="txtRuc_TextChanged" placeHolder="R.U.C./C.C."></asp:TextBox>
                    <!--<asp:Label ID="lblNombres" CssClass="lblPeq" runat="server" Text="Nombres" Visible="true"></asp:Label>!-->
                    <asp:TextBox ID="txtNombres" CssClass="txtTitPeq" runat="server" Enabled="false" placeHolder="Nombres"></asp:TextBox>
                </asp:Panel>

                <asp:Panel ID="pnDocumento" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblSerie" CssClass="lblPeq" runat="server" Text="Serie"></asp:Label>!-->
                    <asp:TextBox ID="txtSerie" CssClass="txtPeq" runat="server" AutoPostBack="True" placeHolder="Serie"></asp:TextBox>
                    <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtSerie" AutoCompleteValue="0"
                        AutoComplete="true" Mask="999-999" MaskType="None" />
                    <!--<asp:Label ID="lblNumDocumento" CssClass="lblPeq" runat="server" Text="#/Doc."></asp:Label>!-->
                    <asp:TextBox ID="txtNumDocumento" CssClass="txtPeq" runat="server" AutoPostBack="True" OnTextChanged="txtNumDocumento_TextChanged"
                        placeHolder="#/Doc."></asp:TextBox>

                    <!--<asp:Label ID="lblDocRet" CssClass="lblPeq" runat="server" Text="#/Doc"></asp:Label>!-->
                    <asp:Panel ID="pnDocRet" runat="server" CssClass="pnPeqDdl" Visible="true">
                        <asp:DropDownList ID="ddlDocRet" runat="server" CssClass="peqDdl" DataTextField="numDocSustento" DataValueField="id_infotributaria"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlDocRet_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>

                    <asp:TextBox ID="txtDocAutorizacion" CssClass="txtPeq" runat="server" AutoPostBack="True" Enabled="true"
                        placeHolder="#Autorización/Documento"></asp:TextBox>

                    <asp:TextBox ID="txtFechaEmisionDoc" CssClass="txtPeq" runat="server" AutoPostBack="True" Enabled="true" placeHolder="FechaEmisionDoc"></asp:TextBox>
                    <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaEmisionDoc" Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <asp:TextBox ID="txtFechaCaducDoc" CssClass="txtPeq" runat="server" AutoPostBack="True" Enabled="true" placeHolder="FechaCaducDoc"></asp:TextBox>
                    <act1:CalendarExtender ID="CalendarExtender2" PopupButtonID="" runat="server" TargetControlID="txtFechaCaducDoc" Format="dd/MM/yyyy"></act1:CalendarExtender>



                </asp:Panel>

                <asp:Panel ID="pnAfecta" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblDocumento" CssClass="lblPeq" runat="server" Text="Documento" Enabled="false"></asp:Label>!-->
                    <asp:TextBox ID="txtDocumento" CssClass="txtPeq" runat="server" AutoPostBack="True" Enabled="false" placeHolder="Documento"></asp:TextBox>

                    <!--<asp:Label ID="lblAfectaSucursal" CssClass="lblPeq" runat="server" Text="Afec/Suc."></asp:Label>!-->
                    <asp:Panel ID="pnAfectaSuc" runat="server" CssClass="pnPeqDdl">
                        <asp:DropDownList ID="ddlAfectaSucursal" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal"
                            Visible="True">
                        </asp:DropDownList>
                    </asp:Panel>

                    <!--<asp:Label ID="lblAfectaCcosto" CssClass="lblPeq" runat="server" Text="Afec/CCosto"></asp:Label>!-->
                    <asp:Panel ID="pnCcosto" runat="server" CssClass="pnPeqDdl">
                        <asp:DropDownList ID="ddlAfectaCcosto" runat="server" CssClass="peqDdl" DataTextField="nom_cco" DataValueField="mae_cco"
                            Visible="True">
                        </asp:DropDownList>
                    </asp:Panel>
                </asp:Panel>




            </fieldset>


            <fieldset id="fsDatos" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="pnDescripcion" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Descripción"
                    ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblAutorizacion" CssClass="lblPeq" runat="server" Text="Aut/por:"></asp:Label>!-->
                    <asp:TextBox ID="txtAutorizacion" CssClass="txtTitPeq" runat="server" placeHolder="Autorizado por"></asp:TextBox>
                    <!--<asp:Label ID="lblDescripcion" CssClass="lblPeq" runat="server" Text="Descrip/Justif."></asp:Label>!-->
                    <asp:TextBox ID="txtDescripcion" CssClass="txtTitPeq" runat="server" placeHolder="Descripción"></asp:TextBox>
                    <!--<asp:Label ID="lblTipoPago" CssClass="lblPeq" runat="server" Text="Tipo/Pago"></asp:Label>!-->
                    <asp:Panel ID="pnTipoPago" runat="server" CssClass="pnPeqDdl" Visible="true">
                        <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="peqDdl" DataTextField="nombres" DataValueField="mae_gas">
                            <asp:ListItem Value=-1>Sel.tipo de pago</asp:ListItem>
                            <asp:ListItem Value=1>Efectivo</asp:ListItem>
                            <asp:ListItem Value=2>Cheque</asp:ListItem>
                            <asp:ListItem Value=3>Cruce</asp:ListItem>
                            <asp:ListItem Value=4>Transferencia</asp:ListItem>
                            <asp:ListItem Value=5>Justificación</asp:ListItem>
                            <asp:ListItem Value=6 Enabled="false">Justificación provisión</asp:ListItem>

                        </asp:DropDownList>
                    </asp:Panel>
                </asp:Panel>

                <asp:Panel ID="pnBienIva" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Bienes" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblBiGastos" CssClass="lblPeq" runat="server" Text="Tipo" Visible="true"></asp:Label> !-->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                        <ContentTemplate>
                            <asp:Panel ID="pnBiGastos" runat="server" CssClass="pnPeqDdl">
                                <asp:DropDownList ID="ddlBiGastos" CssClass="peqDdl" DataTextField=nombre DataValueField=mae_gas
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </asp:Panel>

                            <!--<asp:Label ID="lblBiCodCble" CssClass="lblPeq" runat="server" Text="CodCble" Visible="true"></asp:Label>!-->
                            <asp:Panel ID="pnBiCodCble" runat="server" CssClass="pnPeqDdl">
                                <asp:DropDownList ID="ddlBiCodCble" CssClass="peqDdl" DataTextField=nom_ic DataValueField=var_gen
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </asp:Panel>
                        </ContentTemplate>


                    </asp:UpdatePanel>
                    <!-- <asp:Label ID="lblBien" CssClass="lblPeq" runat="server" Text="Descrip/Bien" Visible="true"></asp:Label>!-->
                    <asp:TextBox ID="txtBien" CssClass="txtTitPeq" runat="server" Visible="TRUE" Enabled="true" placeHolder="Descripción del bien"></asp:TextBox>




                    <asp:Label ID="lblBsubtotal" CssClass="lblPeq" runat="server" Text="Subtotal" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBsubtotal" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true"
                        AutoPostBack="True" OnTextChanged="txtBsubtotal_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblBtarifa0" CssClass="lblPeq" runat="server" Text="Tarifa 0" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBtarifa0" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true"
                        AutoPostBack="True" OnTextChanged="txtBsubtotal_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblBotros" CssClass="lblPeq" runat="server" Text="Otros" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBotros" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true"
                        AutoPostBack="True" OnTextChanged="txtBsubtotal_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblBIva" CssClass="lblPeq" runat="server" Text="I.V.A." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBIva" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>


                    <asp:Label ID="lblBtotal" CssClass="lblPeq" runat="server" Text="Total Bienes" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBtotal" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                </asp:Panel>

                <asp:Panel ID="pnServicioIva" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Servicios"
                    ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblSiGastos" CssClass="lblPeq" runat="server" Text="Tipo" Visible="true"></asp:Label>!-->
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                        <ContentTemplate>
                            <asp:Panel ID="pnSiGastos" runat="server" CssClass="pnPeqDdl">
                                <asp:DropDownList ID="ddlSiGastos" CssClass="peqDdl" DataTextField=nombre DataValueField=mae_gas
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </asp:Panel>
                            <!--<asp:Label ID="lblSiCodCble" CssClass="lblPeq" runat="server" Text="CodCble" Visible="true"></asp:Label>!-->
                            <asp:Panel ID="pnSiCodCble" runat="server" CssClass="pnPeqDdl">
                                <asp:DropDownList ID="ddlSiCodCble" CssClass="peqDdl" DataTextField=nom_ic DataValueField=var_gen
                                    runat="server" AutoPostBack="True">
                                </asp:DropDownList>

                            </asp:Panel>
                        </ContentTemplate>


                    </asp:UpdatePanel>
                    <asp:TextBox ID="txtServicio" CssClass="txtTitPeq" runat="server" Visible="TRUE" Enabled="true" placeHolder="Descripción del servicio"></asp:TextBox>

                    <asp:Label ID="lblSsubtotal" CssClass="lblPeq" runat="server" Text="Subtotal" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSsubtotal" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true"
                        AutoPostBack="True" OnTextChanged="txtSsubtotal_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblStarifa0" CssClass="lblPeq" runat="server" Text="Tarifa 0" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtStarifa0" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true"
                        AutoPostBack="True" OnTextChanged="txtSsubtotal_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblSotros" CssClass="lblPeq" runat="server" Text="Otros" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSotros" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true"
                        AutoPostBack="True" OnTextChanged="txtSsubtotal_TextChanged"></asp:TextBox>

                    <asp:Label ID="lblSIva" CssClass="lblPeq" runat="server" Text="I.V.A." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSIva" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>

                    <asp:Label ID="lblStotal" CssClass="lblPeq" runat="server" Text="Total servicios" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtStotal" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false" AutoPostBack="True"></asp:TextBox>

                </asp:Panel>

                <asp:Panel ID="pnTotales" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Totales" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblNumAutorizacion" CssClass="lblPeq" runat="server" Text="#Autoriz.Ret."></asp:Label>!-->
                    <asp:TextBox ID="txtNumretencion" CssClass="txtTitPeq" runat="server" placeHolder="# Retención"></asp:TextBox>
                    <asp:TextBox ID="txtNumAutorizacion" CssClass="txtTitPeq" runat="server" placeHolder="# de Aut./Retención"></asp:TextBox>

                    <asp:Label ID="lblValorRetencion" CssClass="lblPeq" runat="server" Text="Retención"></asp:Label>
                    <asp:TextBox ID="txtValorRetencion" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>


                    <asp:Label ID="lblIva" CssClass="lblPeq" runat="server" Text="I.V.A."></asp:Label>
                    <asp:TextBox ID="txtIva" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>

                    <asp:Label ID="lblValorFactura" CssClass="lblPeq" runat="server" Text="Factura"></asp:Label>
                    <asp:TextBox ID="txtValorFactura" CssClass="txtPeq" runat="server" Enabled="false"></asp:TextBox>

                    <asp:Label ID="lblaPagar" CssClass="lblPeq" runat="server" Text="Pagado"></asp:Label>
                    <asp:TextBox ID="txtaPagar" CssClass="txtPeq" runat="server" ForeColor="Red" Font-Bold="true"></asp:TextBox>

                </asp:Panel>

            </fieldset>
        </fieldset>

        <fieldset id="fsBotonera" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnMenuGrabar" CssClass="" runat="server">
                <asp:Button ID="btnValidar" CssClass="btnProceso" runat="server" Text="Validar" Visible="true"
                    OnClick="btnValidar_Click" />
                <asp:Button ID="btnGrabarPago" CssClass="btnProceso" runat="server" Text="Grabar"
                    OnClick="btnGrabarPago_Click" />
                <asp:Button ID="btnCancelarpago" CssClass="btnProceso" runat="server" Text="Regresar"
                    OnClick="btnCancelarpago_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <!-- ELIMINAR!-->
    <asp:Panel ID="pnBorrar" runat="server" CssClass="" Visible="true">
        <fieldset id="eliminar" class="fieldset-principal">
            <legend>Eliminar pago</legend>


            <asp:Label ID="lblBCodigo" CssClass="lblForm" runat="server" Text="Código" Visible="true"></asp:Label>
            <asp:TextBox ID="txtBCodigo" CssClass="txtForm" runat="server" Visible="true" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBRuc" CssClass="lblForm" runat="server" Text="# Identificación"></asp:Label>
            <asp:TextBox ID="txtBRuc" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBNombres" CssClass="lblForm" runat="server" Text="Nombres" Visible="true"></asp:Label>
            <asp:TextBox ID="txtBNombres" CssClass="txtForm" runat="server" Visible="true" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBDocumento" CssClass="lblForm" runat="server" Text="Documento"></asp:Label>
            <asp:TextBox ID="txtBDocumento" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBNumDocumento" CssClass="lblForm" runat="server" Text="# Documento"></asp:Label>
            <asp:TextBox ID="txtBNumDocumento" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBAutorizacion" CssClass="lblForm" runat="server" Text="Autorización"></asp:Label>
            <asp:TextBox ID="txtBAutorizacion" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBConcepto" CssClass="lblForm" runat="server" Text="Concepto"></asp:Label>
            <asp:TextBox ID="txtBConcepto" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBValorFactura" CssClass="lblForm" runat="server" Text="Valor del documento"></asp:Label>
            <asp:TextBox ID="txtBValorFactura" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBValorRetencion" CssClass="lblForm" runat="server" Text="Valor retención"></asp:Label>
            <asp:TextBox ID="txtBValorRetencion" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
            <asp:Label ID="lblBAPagar" CssClass="lblForm" runat="server" Text="A pagar"></asp:Label>
            <asp:TextBox ID="txtBAPagar" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>


            <asp:Panel ID="pnMenuBorrar" runat="server">
                <asp:Button ID="btnBorrarPago" CssClass="btnProceso" runat="server" Text="Borrar"
                    OnClick="btnBorrarPago_Click" />
                <asp:Button ID="btnRegresar3" CssClass="btnProceso" runat="server" Text="Regresar"
                    OnClick="btnRegresar3_Click" />
            </asp:Panel>



        </fieldset>
    </asp:Panel>

    <!-- EXPORTAR!-->
    <asp:Panel ID="pnExportar" CssClass="" runat="server" Wrap="true" Visible="true">
        <fieldset id="excel" class="fieldset-principal">
            <legend>EXPORTAR</legend>

            <asp:Button ID="btnExcelRe" runat="server" CssClass="btnProceso" Text="A Excel" Visible="true"
                OnClick="btnExcelRe_Click" />


        </fieldset>
    </asp:Panel>

    <!-- INGRESAR PROVEEDOR !-->
    <asp:Panel ID="pnIngresarProveedor" runat="server" Visible="false">
        <asp:Label ID="lblAviso" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnSucursal" runat="server">
            <fieldset id="fsSucursal">
                <legend>Datos de la Matriz</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblProveedor" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtProveedor" CssClass="txtForm" runat="server"></asp:TextBox>
                    <asp:ImageButton ID=ibConsultar runat="server" ImageUrl="~/images/iconos/219_2.png"
                        OnClick="ibConsultar_Click" />
                </asp:Panel>
                <asp:Label ID="lblrazonsocial" CssClass="lblForm" runat="server" Text="Razón Social"></asp:Label>
                <asp:TextBox ID="txtrazonsocial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblnombreComercial" CssClass="lblForm" runat="server" Text="Nombre comercial"></asp:Label>
                <asp:TextBox ID="txtnombreComercial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lbldirMatriz" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
                <asp:TextBox ID="txtdirMatriz" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblcontribuyenteEspecial" CssClass="lblForm" runat="server" Text="# Contribuyente especial"></asp:Label>
                <asp:TextBox ID="txtcontribuyenteEspecial" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblobligadoContabilidad" CssClass="lblForm" runat="server" Text="Obligado a llevar Contabilidad"></asp:Label>
                <asp:Panel ID="pnObligado" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlObligado" runat="server">
                        <asp:ListItem Value="SI">SI</asp:ListItem>
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblTel" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTel" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:Button ID=btnRegresar2 runat="server" Text="regresar" CssClass="btnForm"
                        OnClick="btnRegresar2_Click" />
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

</asp:Content>

