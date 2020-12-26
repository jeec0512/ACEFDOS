<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="controlCajas2.aspx.cs"
    Inherits="Egreso_controlCajas2" EnableEventValidation="false" %>

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
                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar"
                    OnClick="btnConsultar_Click" />
                

            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDetalleCaja" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleCaja" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvEgresosDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvEgresosDetalle_RowCommand" 
                OnRowDataBound="grvEgresosDetalle_RowDataBound" ShowFooter="True">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:ButtonField HeaderText="Ver retención" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/ohist.ico"
                        CommandName="verRet" ItemStyle-Width="60" />

                    <asp:BoundField HeaderText="fecha" DataField="fecha"  Visible="true" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="numero" HeaderText="numero" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="id_cabegresos" HeaderText="id_cabegresos" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="id_detegresos" HeaderText="id_detegresos" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="ruc" HeaderText="RUC" Visible="true" />
                    <asp:BoundField DataField="fechaemisiondoc" HeaderText="fechaemisiondoc" Visible="true" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="concepto" HeaderText="concepto" Visible="true" />
                    <asp:BoundField DataField="numerodocumento" HeaderText="numerodocumento" Visible="true" />
                    <asp:BoundField DataField="doc_autorizacion" HeaderText="doc_autorizacion" Visible="true" />
                    <asp:BoundField DataField="subtotal" HeaderText="subtotal" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="tarifacero" HeaderText="tarifacero" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="otros" HeaderText="otros" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="totaliva" HeaderText="totaliva" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="totaldoc" HeaderText="totaldoc" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="fechacaducdoc" HeaderText="fechacaducdoc" Visible="true" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="numretencion" HeaderText="numretencion" Visible="true" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="fechadocumento" HeaderText="fechadocumento" Visible="true" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="numAutorizacionretencion" HeaderText="doc_autorizacion" Visible="true" />
                    <asp:BoundField DataField="var_gen" HeaderText="var_gen" Visible="true" />
                    <asp:BoundField DataField="mae_gas" HeaderText="mae_gas" Visible="true" />
                    <asp:BoundField DataField="svar_gen" HeaderText="svar_gen" Visible="true" />
                    <asp:BoundField DataField="smae_gas" HeaderText="smae_gas" Visible="true" />
                    <asp:BoundField DataField="fuente" HeaderText="fuente" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="iva" HeaderText="iva" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="totalretenido" HeaderText="totalretenido" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="apagar" HeaderText="apagar" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="estado" HeaderText=".estado" Visible="true" />

                    <asp:CheckBoxField DataField="justificado" HeaderText="Justificado" />
                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                        CommandName="Jus" ItemStyle-Width="60" />

                </Columns>
                <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium" 
                            Font-Strikeout="False" />
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

    <!-- INGRESO DE REGISTRO DE EGRESOS  !-->
    <asp:Panel ID="pnPagos" runat="server" CssClass="" Visible="false">
        <fieldset id="registrar" class="fieldset-principal">
            <legend>Registrar egreso</legend>


            <!-- se utiliza para procesos internos no se visializa  !-->
            <asp:Panel ID="pnDatosGenerales" CssClass="pnPeq" runat="server" Visible="false" GroupingText="" ForeColor="#1d92e9">
                <asp:Label ID="lblSucursal" CssClass="lblPeq" runat="server" Text="Sucursal" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSucursal" CssClass="txtPeq" runat="server" Visible="true"></asp:TextBox>
                <asp:Label ID="lblIFecha" CssClass="lblPeq" runat="server" Text="Fecha" Visible="true" ></asp:Label>
                <asp:TextBox ID="txtIFecha" CssClass="txtPeq" runat="server" Visible="true"></asp:TextBox>


            </asp:Panel>

            <fieldset id="fsCaja" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="pnBienes" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <!--<asp:Label ID="lblnumero" CssClass="lblPeq" runat="server" Text="Cód/caja" Visible="true"></asp:Label>!-->
                    <asp:TextBox ID="txtNumero" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtIdCabecera" CssClass="txtPeq" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtIdDetalle" CssClass="txtPeq" runat="server" Visible="false"></asp:TextBox>

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

                    <asp:TextBox ID="txtFechaEmisionDoc" CssClass="txtPeq" runat="server" AutoPostBack="True" Enabled="true"
                        placeHolder="FechaEmisionDoc"></asp:TextBox>
                    <act1:CalendarExtender ID="CalendarExtender2" PopupButtonID="" runat="server" TargetControlID="txtFechaEmisionDoc"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <asp:TextBox ID="txtFechaCaducDoc" CssClass="txtPeq" runat="server" AutoPostBack="True" Enabled="true"
                        placeHolder="FechaCaducDoc"></asp:TextBox>
                    <act1:CalendarExtender ID="CalendarExtender3" PopupButtonID="" runat="server" TargetControlID="txtFechaCaducDoc"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>



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
</asp:Content>

