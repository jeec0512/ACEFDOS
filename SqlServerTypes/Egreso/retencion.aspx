<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="retencion.aspx.cs"
    Inherits="Egreso_retencion" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">

    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>


    <!-- MENSAJES  !-->

    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="false">
        <asp:Label ID="lblMensaje" runat="server" Text="mensaje" Visible="true" font-size="15px" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="true"
            CssClass="btnProceso" OnClick="btnIngresaProv_Click1" />

    </asp:Panel>

    <!--REGISTRO DE RETENCIONES !-->

    <asp:Panel ID="pnSucursal" CssClass="" runat="server" Visible="true">
        <fieldset id="fsSucursal" class="fieldset-principal">
            <legend>Registro de retenciones</legend>

            <asp:Panel ID="pnSuc" CssClass="pnPeq" runat="server" Visible="true">

                <!--<asp:Label ID="lbCodSuc" CssClass="lblPeq" runat="server" Text="Sucursal" Visible="true"></asp:Label>!-->

                <asp:Panel ID="pnCodSuc" runat="server" CssClass="pnPeqDdl">
                    <asp:UpdatePanel ID="upSuc" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlSucursal" CssClass="peqDdl" DataTextField=nom_suc DataValueField=sucursal runat="server">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>


                <!--<asp:Label ID="lblTarifa" CssClass="lblPeq" runat="server" Text="Tarifa" Visible="true"></asp:Label>!-->

                <asp:Panel ID="pnTarifa" runat="server" CssClass="pnPeqDdl">
                    <asp:UpdatePanel ID="upTarifa" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTarifa" runat="server" CssClass="peqDdl" Visible="true" Enabled="true">
                                <asp:ListItem Value=-1>Seleccione tarifa</asp:ListItem>
                                <asp:ListItem Value=2>IVA 12%</asp:ListItem>
                                <asp:ListItem Value=6>No objeto de impuesto</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <!--<asp:Panel ID="pnAfecta" CssClass="pnPeq" runat="server" Visible="true" GroupingText="" ForeColor="#1d92e9">
                    <asp:Label ID="lblAfectaSucursal" CssClass="lblPeq" runat="server" Text="Afec/Suc."></asp:Label>!-->
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
            <!--</asp:Panel>!-->

            <asp:Panel ID="pnRetencion" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblFecha" CssClass="lblPeq" runat="server" Text="Fecha/Ret." Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtFecha" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" placeholder="Fecha/Ret."></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFecha" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" BehaviorID="mee1" TargetControlID="txtFecha" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <!--<asp:Label ID="lblNumRet" CssClass="lblPeq" runat="server" Text="#Retención" Visible="true"></asp:Label>!-->
                <asp:UpdatePanel ID="upNumRet" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnSecuencial" runat="server" CssClass=btnProceso Text="Generar #Retención"
                            Visible="true" OnClick="btnSecuencial_Click" />
                        <asp:TextBox ID="txtNumRet" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                            OnTextChanged="txtNumRet_TextChanged" placeholder="#Retención"></asp:TextBox>
                        <act1:MaskedEditExtender ID="maskNumRet" runat="server" TargetControlID="txtNumRet" Mask="999999999"
                            MaskType="None" />
                    </ContentTemplate>

                </asp:UpdatePanel>

            </asp:Panel>

            <asp:Panel ID="pnRuc" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblRuc" CssClass="lblPeq" runat="server" Text="R.U.C." Visible="true"></asp:Label>!-->

                <asp:UpdatePanel ID="upReceptor" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlReceptor" CssClass="peqDdl" runat="server">
                                <asp:ListItem Value="-1">Seleccione tipo de identificación</asp:ListItem>
                                <asp:ListItem Value="04">RUC</asp:ListItem>
                                <asp:ListItem Value="05">CEDULA</asp:ListItem>
                                <asp:ListItem Value="06">PASAPORTE</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                <asp:TextBox ID="txtRuc" CssClass="txtPeq" runat="server" Visible="true" Enabled="true"
                    AutoPostBack="True" OnTextChanged="txtRuc_TextChanged" placeholder="R.U.C."></asp:TextBox>
                <act1:MaskedEditExtender ID="maskRuc" runat="server" TargetControlID="txtRuc" Mask="9999999999999" MaskType="None" />

                <!--<asp:Label ID="lbRso" CssClass="lblPeq" runat="server" Text="Razón/Social" Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtrso" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false" placeholder="Razón/Social"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnContribuyente" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblEmail" CssClass="lblPeq" runat="server" Text="Email" Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtemail" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false" placeholder="E-mail"></asp:TextBox>

                <!--<asp:Label ID="lblContribuyente" CssClass="lblPeq" runat="server" Text="Contribuyente" Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtContribuyente" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="false" placeholder="Contribuyente"></asp:TextBox>
            </asp:Panel>



        </fieldset>
    </asp:Panel>

    <!--DATOS DEL DOCUMENTO !-->
    <asp:Panel ID="pnDocumento" CssClass="" runat="server" Visible="true">

        <fieldset id="fsDocumento" class="fieldset-principal">
            <legend>Documento</legend>
            <asp:Panel ID="pnTipoDoc" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblTipDoc" CssClass="lblPeq" runat="server" Text="Tipo/Documento" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnTipDoc" runat="server" CssClass="pnPeqDdl">
                    <asp:UpdatePanel ID="upTipDoc" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTipDoc" CssClass="peqDdl" runat="server">
                                <asp:ListItem Value="-1">Seleccione tipo de documento</asp:ListItem>
                                <asp:ListItem Value="01">Factura</asp:ListItem>
                                <asp:ListItem Value="03">Liquidación de compra</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <!--<asp:Label ID="lblFechDoc" CssClass="lblPeq" runat="server" Text="Fecha/Doc" Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtFechDoc" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" placeholder="Fecha/Doc"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechDoc"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" BehaviorID="mee4" TargetControlID="txtFechDoc"
                    Mask="99/99/9999" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>

            <asp:Panel ID="pnSerie" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblSerie" CssClass="lblPeq" runat="server" Text="Estab/PtoVta" Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtSerie" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" placeholder="Estab/PtoVta"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtSerie" AutoCompleteValue="0"
                    AutoComplete="true" Mask="999-999" MaskType="None" />

                <!--<asp:Label ID="lblNumDoc" CssClass="lblPeq" runat="server" Text="#Documento" Visible="true"></asp:Label>!-->
                <asp:UpdatePanel ID="uptxtNumDoc" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNumDoc" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                            OnTextChanged="txtNumDoc_TextChanged" placeholder="#Documento"></asp:TextBox>
                        <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtNumDoc" AutoCompleteValue="0"
                            AutoComplete="true" Mask="999999999" MaskType="None" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:Panel ID="pnAutorizacion" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblAutorizacion" CssClass="lblPeq" runat="server" Text="Autorización" Visible="true"></asp:Label>!-->
                <asp:UpdatePanel ID="upAutorizacion" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtAutorizacion" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                            OnTextChanged="txtAutorizacion_TextChanged" placeholder="Autorización"></asp:TextBox>
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--<asp:Label ID="lblFechCaduc" CssClass="lblPeq" runat="server" Text="Fecha/Caduc." Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtFechCaduc" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" placeholder="Fecha/Caduc."></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender2" PopupButtonID="" runat="server" TargetControlID="txtFechCaduc"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>

                

                <act1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" BehaviorID="mee8" TargetControlID="txtFechCaduc"
                    Mask="99/99/9999" MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="LeftToRight"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="pnBien" CssClass="pnPeq" runat="server" Visible="true">
                <!--<asp:Label ID="lblBien" CssClass="lblPeq" runat="server" Text="Desc.Bien/Serv" Visible="true"></asp:Label>!-->
                <asp:TextBox ID="txtBien" CssClass="txtPeq" runat="server" Visible="TRUE" Enabled="true" placeholder="Desc.Bien/Serv"></asp:TextBox>
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <!--VALORES DEL DOCUMENTO!-->
    <asp:Panel ID="pnValores" CssClass="" runat="server" Visible="true">

        <fieldset id="fsValores" class="fieldset-principal">
            <legend>Valores del documento</legend>
            <asp:Panel ID="pnBienes" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Bienes" ForeColor="#1d92e9">
                <asp:Label ID="lblBsubtotal" CssClass="lblPeq" runat="server" Text="Subtotal" Visible="true"></asp:Label>
                <asp:TextBox ID="txtBsubtotal" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" ValidationGroup="MKE"
                    AutoPostBack="True" OnTextChanged="txtBsubtotal_TextChanged" placeholder="Subtotal"></asp:TextBox>
                <asp:Label ID="lblBtarifa0" CssClass="lblPeq" runat="server" Text="Tarifa0" Visible="true"></asp:Label>
                <asp:TextBox ID="txtBtarifa0" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                    OnTextChanged="txtBtarifa0_TextChanged"></asp:TextBox>
                <asp:Label ID="lblBotros" CssClass="lblPeq" runat="server" Text="otros" Visible="true"></asp:Label>
                <asp:TextBox ID="txtBotros" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                    OnTextChanged="txtBotros_TextChanged"></asp:TextBox>
                <asp:Label ID="lblBiva" CssClass="lblPeq" runat="server" Text="I.V.A." Visible="true"></asp:Label>
                <asp:TextBox ID="txtBiva" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="pnServicios" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Servicios" ForeColor="#1d92e9">
                <asp:Label ID="lblSsubtotal" CssClass="lblPeq" runat="server" Text="Subtotal" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSsubtotal" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                    OnTextChanged="txtSsubtotal_TextChanged"></asp:TextBox>
                <asp:Label ID="lblStarifa0" CssClass="lblPeq" runat="server" Text="Tarifa0" Visible="true"></asp:Label>
                <asp:TextBox ID="txtStarifa0" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                    OnTextChanged="txtStarifa0_TextChanged"></asp:TextBox>
                <asp:Label ID="lblSotros" CssClass="lblPeq" runat="server" Text="otros" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSotros" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="true" AutoPostBack="True"
                    OnTextChanged="txtSotros_TextChanged"></asp:TextBox>
                <asp:Label ID="lblSiva" CssClass="lblPeq" runat="server" Text="I.V.A." Visible="true"></asp:Label>
                <asp:TextBox ID="txtSiva" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>


            </asp:Panel>
            <asp:Panel ID="pnSubtotales" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Subtotales"
                ForeColor="#1d92e9">
                <asp:Label ID="lblSubtotalBienes" CssClass="lblPeq" runat="server" Text="Bienes" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSubtotalBienes" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblSubtotalServicios" CssClass="lblPeq" runat="server" Text="Servicios" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSubtotalServicios" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblSubtotalGeneral" CssClass="lblPeq" runat="server" Text="General" Visible="true"></asp:Label>
                <asp:TextBox ID="txtSubtotalGeneral" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblSubtotalIva" CssClass="lblPeq" runat="server" Text="Total/I.V.A." Visible="true"></asp:Label>
                <asp:TextBox ID="txtSubtotalIva" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblPorcIce" CssClass="lblPeq" runat="server" Text="% ICE" Visible="true"></asp:Label>
                <asp:TextBox ID="txtPorcIce" CssClass="txtSmall" runat="server" Visible="true" Enabled="true"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnTotales" CssClass="pnPeq" runat="server" Visible="true" GroupingText="Totales" ForeColor="#1d92e9">
                <asp:Label ID="lblTotalFuente" CssClass="lblPeq" runat="server" Text="Ret.Fuente" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTotalFuente" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalIva" CssClass="lblPeq" runat="server" Text="Ret.IVA" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTotalIva" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalRetencion" CssClass="lblPeq" runat="server" Text="Retenido" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTotalRetencion" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblTotalDocumento" CssClass="lblPeq" runat="server" Text="Documento" Visible="true"></asp:Label>
                <asp:TextBox ID="txtTotalDocumento" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                <asp:Label ID="lblApagar" CssClass="lblPeq" runat="server" Text="APagar" Visible="true"></asp:Label>
                <asp:TextBox ID="txtApagar" CssClass="txtSmall" runat="server" Visible="TRUE" Enabled="false" ForeColor="Red"
                    Font-Bold="true"></asp:TextBox>
            </asp:Panel>


        </fieldset>
    </asp:Panel>

    <!--PORCENTAJES Y VALORES A RETENER !-->
    <asp:Panel ID="pnRetener" CssClass="" runat="server" Visible="true">

        <fieldset id="fsRetener" class="fieldset-principal">
            <legend>Porcentajes y valores a retener</legend>
            <asp:Panel ID="pnBienIva" CssClass="pnPeq" runat="server" Visible="false" GroupingText="Bienes IVA" ForeColor="#1d92e9">
                <!--<asp:Label ID="lblBiGastos" CssClass="lblPeq" runat="server" Text="Tipo" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnBiGastos" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlBiGastos" CssClass="peqDdl" DataTextField=nombre DataValueField=mae_gas
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBiGastos_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <!--<asp:Label ID="lblBiCodCble" CssClass="lblPeq" runat="server" Text="CodCble" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnBiCodCble" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlBiCodCble" CssClass="peqDdl" DataTextField=nom_ic DataValueField=var_gen
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBiCodCble_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>





                <asp:Panel ID="pnBIVA" CssClass="pnBit" runat="server" Visible="true" ForeColor="#1d92e9">

                    <asp:Label ID="lblTitulo" CssClass="lblBit" runat="server" Text="Tip." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtTitulo1" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="FTE."
                        BorderStyle=None></asp:TextBox>
                    <asp:TextBox ID="txtTitulo2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="I.V.A."
                        BorderStyle=None></asp:TextBox>


                    <asp:Label ID="lblBibase" CssClass="lblBit" runat="server" Text="B.Imp." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBibase" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtBibase2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lbBiporc" CssClass="lblBit" runat="server" Text="%Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBiporc" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtBiporc2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblBiValor" CssClass="lblBit" runat="server" Text="V.Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBiValor" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtBiValor2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblBiCodigo" CssClass="lblBit" runat="server" Text="Cód." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtBiCodigo" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtBiCodigo2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnBienCero" CssClass="pnPeq" runat="server" Visible="false" GroupingText="Bienes 0" ForeColor="#1d92e9">
                <!--<asp:Label ID="lblB0Gastos" CssClass="lblPeq" runat="server" Text="Tipo" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnB0Gastos" runat="server" CssClass="pnPeqDdl">

                    <asp:DropDownList ID="ddlB0Gastos" CssClass="peqDdl" DataTextField=nombre DataValueField=mae_gas
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlB0Gastos_SelectedIndexChanged">
                    </asp:DropDownList>


                </asp:Panel>
                <!--<asp:Label ID="lblB0CodCble" CssClass="lblPeq" runat="server" Text="CodCble" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnB0CodCble" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlB0CodCble" CssClass="peqDdl" DataTextField=nom_ic DataValueField=var_gen
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlB0CodCble_SelectedIndexChanged">
                    </asp:DropDownList>

                </asp:Panel>

                <asp:Panel ID="Panel4" CssClass="pnBit" runat="server" Visible="true" ForeColor="#1d92e9">
                    <asp:Label ID="Label1" CssClass="lblBit" runat="server" Text="" Visible="true"></asp:Label>
                    <asp:TextBox ID="TextBox1" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="FTE."
                        BorderStyle=None></asp:TextBox>
                    <asp:TextBox ID="TextBox2" CssClass="txtBit" runat="server" Visible="true" Enabled="false" Text="" BorderStyle=None></asp:TextBox>

                    <asp:Label ID="lblB0base" CssClass="lblBit" runat="server" Text="B.Imp." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtB0base" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtB0base2" CssClass="txtBit" runat="server" Visible="true" Enabled="false" BorderStyle=None></asp:TextBox>
                    <asp:Label ID="lblB0porc" CssClass="lblBit" runat="server" Text="%Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtB0porc" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtB0porc2" CssClass="txtBit" runat="server" Visible="true" Enabled="false" BorderStyle=None></asp:TextBox>
                    <asp:Label ID="lblB0Valor" CssClass="lblBit" runat="server" Text="V.Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtB0Valor" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtB0Valor2" CssClass="txtBit" runat="server" Visible="true" Enabled="false" BorderStyle=None></asp:TextBox>
                    <asp:Label ID="lblB0Codigo" CssClass="lblBit" runat="server" Text="Cód." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtB0Codigo" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtB0Codigo2" CssClass="txtBit" runat="server" Visible="true" Enabled="false" BorderStyle=None></asp:TextBox>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnServicioIva" CssClass="pnPeq" runat="server" Visible="false" GroupingText="Servicios IVA"
                ForeColor="#1d92e9">
                <!--<asp:Label ID="lblSiGastos" CssClass="lblPeq" runat="server" Text="Tipo" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnSiGastos" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlSiGastos" CssClass="peqDdl" DataTextField=nombre DataValueField=mae_gas
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSiGastos_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <!--<asp:Label ID="lblSiCodCble" CssClass="lblPeq" runat="server" Text="CodCble" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnSiCodCble" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlSiCodCble" CssClass="peqDdl" DataTextField=nom_ic DataValueField=var_gen
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSiCodCble_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="Panel5" CssClass="pnBit" runat="server" Visible="true" ForeColor="#1d92e9">
                    <asp:Label ID="Label2" CssClass="lblBit" runat="server" Text="" Visible="true"></asp:Label>
                    <asp:TextBox ID="TextBox3" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="FTE."
                        BorderStyle=None></asp:TextBox>
                    <asp:TextBox ID="TextBox4" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="I.V.A."
                        BorderStyle=None></asp:TextBox>

                    <asp:Label ID="lblSibase" CssClass="lblBit" runat="server" Text="B.Imp." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSibase" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtSibase2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblSiporc" CssClass="lblBit" runat="server" Text="%Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSiporc" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtSiporc2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblSiValor" CssClass="lblBit" runat="server" Text="V.Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSiValor" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtSiValor2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblSiCodigo" CssClass="lblBit" runat="server" Text="Cód." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtSiCodigo" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtSiCodigo2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnServicioCero" CssClass="pnPeq" runat="server" Visible="false" GroupingText="Servicios 0"
                ForeColor="#1d92e9">
                <!--<asp:Label ID="lblS0Gastos" CssClass="lblPeq" runat="server" Text="Tipo" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnS0Gastos" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlS0Gastos" CssClass="peqDdl" DataTextField=nombre DataValueField=mae_gas
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlS0Gastos_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <!--<asp:Label ID="lblS0CodCble" CssClass="lblPeq" runat="server" Text="CodCble" Visible="true"></asp:Label>!-->
                <asp:Panel ID="pnS0CodCble" runat="server" CssClass="pnPeqDdl">
                    <asp:DropDownList ID="ddlS0CodCble" CssClass="peqDdl" DataTextField=nom_ic DataValueField=var_gen
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlS0CodCble_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="Panel6" CssClass="pnBit" runat="server" Visible="true" ForeColor="#1d92e9">
                    <asp:Label ID="Label3" CssClass="lblBit" runat="server" Text="" Visible="true"></asp:Label>
                    <asp:TextBox ID="TextBox5" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="FTE."
                        BorderStyle=None></asp:TextBox>
                    <asp:TextBox ID="TextBox6" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" Text="" BorderStyle=None></asp:TextBox>

                    <asp:Label ID="lblS0base" CssClass="lblBit" runat="server" Text="B.Imp." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtS0base" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtS0base2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" BorderStyle=None></asp:TextBox>
                    <asp:Label ID="lblS0porc" CssClass="lblBit" runat="server" Text="%Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtS0porc" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtS0porc2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" BorderStyle=None></asp:TextBox>
                    <asp:Label ID="lblS0Valor" CssClass="lblBit" runat="server" Text="V.Ret." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtS0Valor" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtS0Valor2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" BorderStyle=None></asp:TextBox>
                    <asp:Label ID="lblS0Codigo" CssClass="lblBit" runat="server" Text="Cód." Visible="true"></asp:Label>
                    <asp:TextBox ID="txtS0Codigo" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false"></asp:TextBox>
                    <asp:TextBox ID="txtS0Codigo2" CssClass="txtBit" runat="server" Visible="TRUE" Enabled="false" BorderStyle=None></asp:TextBox>
                </asp:Panel>
            </asp:Panel>

        </fieldset>
    </asp:Panel>

    <!--BOTONERA !-->
    <asp:Panel ID="pnBotonera" CssClass="" runat="server" Visible="true">

        <fieldset id="fsBotonera" class="fieldset-principal">
            <legend></legend>
            <asp:Button ID="btnValidar" runat="server" CssClass=btnProceso Text="Validar" Visible="true"
                OnClick="btnValidar_Click" />
            <asp:Button ID="btnGuardar" runat="server" CssClass=btnProceso Text="Guardar" Visible="false" Enabled="true"
                OnClick="btnGuardar_Click" />
            <asp:Button ID="btnEnviar" runat="server" CssClass=btnProceso Text="Enviar al SRI" Visible="false" Enabled="true"
                OnClick="btnEnviar_Click" />
            <asp:Button ID="btnRegresar" runat="server" CssClass=btnProceso Text="Regresar" Visible="true" OnClick="btnRegresar_Click" />

        </fieldset>
    </asp:Panel>

    <!-- RETENCIÓN DEL SRI !-->
    <asp:Panel ID="pnRetencionSRI" runat="server" BackColor="#CCCCCC" ScrollBars="Vertical"
        CssClass="pnRegistros">

        <asp:GridView ID="grvRetencionSRI" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
            Width="90%"
            AllowPaging="True"
            AllowSorting="True" PageSize="20">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:CommandField ShowSelectButton="False" />
                <asp:TemplateField HeaderText="Mensaje SRI">
                    <ItemTemplate>
                        <%# Eval("cre_sri") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CÓDIGO">
                    <ItemTemplate>
                        <%# Eval("codigo") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cod.Retención">
                    <ItemTemplate>
                        <%# Eval("codigoRetencion") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Base imponible">
                    <ItemTemplate>
                        <%# Eval("baseImponible") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Porcentaje">
                    <ItemTemplate>
                        <%# Eval("porcentajeRetener") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor retenido">
                    <ItemTemplate>
                        <%# Eval("valorRetenido") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Doc.Sustento">
                    <ItemTemplate>
                        <%# Eval("codDocSustento") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="# Documento">
                    <ItemTemplate>
                        <%# Eval("numDocSustento") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha">
                    <ItemTemplate>
                        <%# Eval("fechaEmisionDocSustento") %>
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
    </asp:Panel>

    <!-- INGRESAR PROVEEDOR !-->
    <asp:Panel ID="pnIngresarProveedor" runat="server" Visible="false">
        <asp:Label ID="lblAviso" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="Panel1" runat="server">
            <fieldset id="Fieldset1">
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
                <asp:Label ID="lblMailProv" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtMailProv" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Panel ID="Panel3" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardaProv runat="server" Text="Grabar" CssClass="btnForm"
                        OnClick="btnGuardaProv_Click" />
                    <asp:Button ID=btnRegresar2 runat="server" Text="regresar" CssClass="btnForm"
                        OnClick="btnRegresar2_Click" />
                    <asp:Label ID="lblpId_InfoTributaria" runat="server" Text="" CssClass="lblMensaje"
                        Visible="false"></asp:Label>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

</asp:Content>

