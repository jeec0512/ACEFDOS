<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="certificadoSicopractico.aspx.cs"
    Inherits="Escuela_certificadoSicopractico" EnableEventValidation="false" %>

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
            <legend>Ingreso de certificado sicopráctico</legend>
            <asp:Panel ID="pnCabecera" CssClass="pnBuscarGrid" runat="server">
                <asp:Label ID="lblBuscarx" runat="server" Text="Buscar por:" Font-Bold="True"
                    Font-Size="Larger" ForeColor="darkblue" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Visible="false" Font-Size="Larger"
                    ForeColor="darkblue" BackColor="#9aaff1">
                    <asp:ListItem
                        Value="0">RUC/C.C.</asp:ListItem>
                    <asp:ListItem
                        Value="1">Apellidos y/o Nombres</asp:ListItem>
                </asp:DropDownList>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnPeqDdl" Visible="true">
                        <asp:UpdatePanel ID="upSucursal" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal que certifica" Visible="true" CssClass="lblPeq"></asp:Label>
                                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                    DataValueField="sucursal">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                <asp:Label ID="lblSocio" runat="server" Text="Ingrese CC/RUC:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtSocio" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1"></asp:TextBox>
                <asp:Label ID="lblContrato" runat="server" Text="#Contrato" Font-Bold="True"
                    Font-Size="Larger" ForeColor="darkblue" Visible="false"></asp:Label>
                <asp:TextBox runat="server" ID="txtContrato" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1" Visible="false"></asp:TextBox>
                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.ico"
                    Width="27px" ToolTip="Buscar" BorderColor="#9aaff1" OnClick="imgBuscar_Click" />
                <asp:Label ID="lblMsg" runat="server" Text="MSG" Font-Bold="True" Font-Size="Small" ForeColor="red"
                    Visible="false"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnGuardar" CssClass="pnPeq" runat="server" Wrap="False">
                <asp:Button
                    ID="btnGuardar" runat="server" CssClass=btnProceso Text="Grabar" Visible="true"
                    OnClick="btnGuardar_Click" />
                <asp:Button
                    ID="btnCancelar" runat="server" CssClass=btnProceso Text="Regresar" Visible="true" />
                <asp:Button ID="btnImprimir" runat="server" CssClass="btnProceso" Text="Imprimir certificación" OnClick="btnImprimir_Click" />
            </asp:Panel>

        </fieldset>

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Datos generales del certificado sicopráctico</legend>

            <fieldset id="Fieldset7" class="fieldset-principal">
                <legend></legend>

                <asp:Panel ID=Panel8 runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                    <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha" CssClass="lblPeq"
                        Text="Fec/certif."></asp:Label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="txtPeq" placeholder="Fecha del certificado"></asp:TextBox>


                    <asp:Panel ID="pnSucursal2" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upSucursal2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSucursal2" runat="server" Text="Suc/factura" Visible="true" CssClass="lblPeq"></asp:Label>
                                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                    DataValueField="sucursal">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtNumFactura" CssClass="lblPeq" Text="#/factura:"></asp:Label>
                    <asp:TextBox ID="txtNumFactura" runat="server" CssClass="txtPeq" placeholder="# factura"></asp:TextBox>
                    <asp:Panel ID="pnTipoDoc" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upTipoDoc" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTipoDoc" runat="server" AssociatedControlID="ddlTipoDoc" CssClass="lblPeq"
                                    Text="Tipo/doc.:"></asp:Label>
                                <asp:DropDownList ID="ddlTipoDoc" runat="server" CssClass="pnSocioDdl">
                                    <asp:ListItem Value="0">Seleccione tipo de certificado de identidad</asp:ListItem>
                                    <asp:ListItem Value="1">Cédula de ciudadanía</asp:ListItem>
                                    <asp:ListItem Value="2">Cédula de identidad</asp:ListItem>
                                    <asp:ListItem Value="3">Cédula extranjero visado por 2 años</asp:ListItem>
                                    <asp:ListItem Value="4">Refugiado</asp:ListItem>
                                    <asp:ListItem Value="5">Pasaporte</asp:ListItem>
                                    <asp:ListItem Value="6">RUC</asp:ListItem>
                                    <asp:ListItem Value="7">Cédula extranjero visado por 18 meses</asp:ListItem>
                                    <asp:ListItem Value="8">Extranjero visado por 6 meses</asp:ListItem>
                                    <asp:ListItem Value="8">Extranjero visado por 1 año</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Label ID="lblRuc" runat="server" AssociatedControlID="txtRuc" CssClass="lblPeq" Text="RUC/CC" ></asp:Label>
                    <asp:TextBox ID="txtRuc" runat="server" CssClass="txtPeq" AutoPostBack="True" Enabled="false" placeholder="# de identidad"></asp:TextBox>
                    <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="lblPeq" Text="Nombres:"></asp:Label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="txtPeq" placeholder="Nombres del cliente" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="lblPeq" Text="Apellidos"></asp:Label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="txtPeq" placeholder="Apellidos del cliente" Enabled="false"></asp:TextBox>

                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="lblPeq" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtPeq" placeholder="Correo electrónico"></asp:TextBox>
                </asp:Panel>

                <asp:Panel ID=Panel3 runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                    <asp:Label ID="lblNumero" runat="server" AssociatedControlID="txtNumero" CssClass="lblPeq" Text="#/Documento:"></asp:Label>
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="txtPeq" placeholder="# documento consecutivo"></asp:TextBox>




                    <asp:Label ID="lblFechaPsico" runat="server" AssociatedControlID="txtFechaPsico" CssClass="lblPeq"
                        Text="FecSico:"></asp:Label>
                    <asp:TextBox ID="txtFechaPsico" runat="server" CssClass="txtPeq" placeholder="Fecha del sicotécnico"></asp:TextBox>
                    <act1:CalendarExtender ID="calFechaPsico" PopupButtonID="" runat="server" TargetControlID="txtFechaPsico"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" BehaviorID="mee1" TargetControlID="txtFechaPsico"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                        AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />


                    <asp:Panel ID="pnEstadoPsico" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upEstadoPsico" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblEstadoPsico" runat="server" AssociatedControlID="ddlEstadoPsico" CssClass="lblPeq"
                                    Text="Estado Psico:"></asp:Label>
                                <asp:DropDownList ID="ddlEstadoPsico" runat="server" CssClass="pnSocioDdl">
                                    <asp:ListItem Value="0">Seleccione estado psico</asp:ListItem>
                                    <asp:ListItem Value="1">Aprobado</asp:ListItem>
                                    <asp:ListItem Value="2">Reprobado</asp:ListItem>
                                    <asp:ListItem Value="3">Espera</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>


                    <asp:Label ID="lblFechaPract" runat="server" AssociatedControlID="txtFechaPract" CssClass="lblPeq" Text="FecPrác:"></asp:Label>
                    <asp:TextBox ID="txtFechaPract" runat="server" CssClass="txtPeq" placeholder="Fecha práctico"></asp:TextBox>
                    <act1:CalendarExtender ID="calFechaPract" PopupButtonID="" runat="server" TargetControlID="txtFechaPract"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <act1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" BehaviorID="mee1" TargetControlID="txtFechaPract"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                        AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />







                    <asp:Panel ID="pnEstadoPract" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upEstadoPract" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblPractico" runat="server" AssociatedControlID="ddlEstadoPract" CssClass="lblPeq"
                                    Text="EstadoPráctico:"></asp:Label>
                                <asp:DropDownList ID="ddlEstadoPract" runat="server" CssClass="pnSocioDdl">
                                    <asp:ListItem Value="0">Seleccione estado práctico</asp:ListItem>
                                    <asp:ListItem Value="1">Aprobado</asp:ListItem>
                                    <asp:ListItem Value="2">Reprobado</asp:ListItem>
                                    <asp:ListItem Value="3">Espera</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                    <asp:Label ID="lblNotaPractico" runat="server" AssociatedControlID="txtNotaPractico" CssClass="lblPeq" Text="Not/prác:" ></asp:Label>
                    <asp:TextBox ID="txtNotaPractico" runat="server" CssClass="txtPeq" placeholder="Nota del exámen práctico (ejemplo: 16,80)"></asp:TextBox>
                    <asp:Panel ID="pnResultado" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upResultado" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblResultado" runat="server" AssociatedControlID="ddlResultado" CssClass="lblPeq"
                                    Text="Resultado:"></asp:Label>
                                <asp:DropDownList ID="ddlResultado" runat="server" CssClass="pnSocioDdl">
                                    <asp:ListItem Value="0">Seleccione estado del resultado</asp:ListItem>
                                    <asp:ListItem Value="1">Aprobado</asp:ListItem>
                                    <asp:ListItem Value="2">Reprobado</asp:ListItem>
                                    <asp:ListItem Value="3">Espera</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID=Panel2 runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">

                    <asp:Label ID="lblInstructor" runat="server" AssociatedControlID="txtInstructor" CssClass="lblPeq" Text="Instr.Evaluador:"></asp:Label>
                    <asp:TextBox ID="txtInstructor" runat="server" CssClass="txtPeq" placeholder="Nombres del instructor que tomo el ex. pract."></asp:TextBox>

                    <asp:Label ID="lblElaborado" runat="server" AssociatedControlID="txtElaborado" CssClass="lblPeq" Text="Elaborado por:"></asp:Label>
                    <asp:TextBox ID="txtElaborado" runat="server" CssClass="txtPeq" placeholder="Nombres de la persona que elaboró el certificado"></asp:TextBox>

                    <asp:Label ID="lblIniciales" runat="server" AssociatedControlID="txtIniciales" CssClass="lblPeq" Text="Iniciales:"></asp:Label>
                    <asp:TextBox ID="txtIniciales" runat="server" CssClass="txtPeq" placeholder="Iniciales de la persona q elaboró"></asp:TextBox>

                    
                    <asp:Label ID="lblDirector" runat="server" AssociatedControlID="txtDirector" CssClass="lblPeq" Text="Director/Escuela:"></asp:Label>
                    <asp:TextBox ID="txtDirector" runat="server" CssClass="txtPeq" placeholder="Nombres del Director de la Escuela"></asp:TextBox>

                    <asp:Label ID="lblRespPractico" runat="server" AssociatedControlID="txtRespPractico" CssClass="lblPeq"
                        Text="Resp/Práctico:"></asp:Label>
                    <asp:TextBox ID="txtRespPractico" runat="server" CssClass="txtPeq" placeholder="Nombres del resposable de práctica"></asp:TextBox>

                    <asp:Label ID="lblRespPsico" runat="server" AssociatedControlID="txtRespPsico" CssClass="lblPeq" Text="Resp/Sicotécnico:"></asp:Label>
                    <asp:TextBox ID="txtRespPsico" runat="server" CssClass="txtPeq" placeholder="Nombres del responsable del psicotécnico"></asp:TextBox>

                    <asp:Label ID="lblObservacion" runat="server" AssociatedControlID="txtObservacion" CssClass="lblPeq"
                        Text="Observación:"></asp:Label>
                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="txtPeq" placeholder="Novedades u observaciones"></asp:TextBox>

                </asp:Panel>

            </fieldset>

          
        </fieldset>

    </asp:Panel>
</asp:Content>

