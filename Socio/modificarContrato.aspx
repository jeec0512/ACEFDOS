<%@ Page Title="" Language="C#" MasterPageFile="~/Socio/mpSocio.master" AutoEventWireup="true" CodeFile="modificarContrato.aspx.cs" Inherits="Socio_modificarContrato" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
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
            <legend>Ingreso de contratos del socio</legend>
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
                <asp:Label ID="lblSocio" runat="server" Text="RUC:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtSocio" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1"></asp:TextBox>
                <asp:Label ID="lblContrato" runat="server" Text="#Contrato" Font-Bold="True"
                    Font-Size="Larger" ForeColor="darkblue" Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtContrato" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1" Visible="true"></asp:TextBox>
                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.ico"
                    Width="27px" ToolTip="Buscar" BorderColor="#9aaff1" OnClick="imgBuscar_Click" />
                <asp:Label ID="lblMsg" runat="server" Text="MSG" Font-Bold="True" Font-Size="Small" ForeColor="red"
                    Visible="false"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnGuardar" CssClass="pnPeq" runat="server" Wrap="False">
                <asp:Button
                    ID="btnGuardar" runat="server" CssClass=btnProceso Text="Modificar" Visible="true" OnClick="btnGuardar_Click" />
                <asp:Button
                    ID="btnCancelar" runat="server" CssClass=btnProceso Text="Regresar" Visible="true" OnClick="btnCancelar_Click" />
            </asp:Panel>
        </fieldset>

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Modificar datos personales</legend>

            <fieldset id="Fieldset7" class="fieldset-principal">
                <legend></legend>

                <asp:Panel ID=Panel8 runat="server" CssClass="pnFormSocio" GroupingText="Datos personales" ForeColor="#0033cc">
                    <asp:TextBox ID="txtIdNova" runat="server" CssClass="txtSForm" AutoPostBack="True" Enabled="false" visible = "false"></asp:TextBox>
                    <asp:Label ID="lblRuc" runat="server" AssociatedControlID="txtRuc" CssClass="lblPeq" Text="RUC/CC"></asp:Label>
                    <asp:TextBox ID="txtRuc" runat="server" CssClass="txtPeq" AutoPostBack="True" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="lblPeq" Text="Nombres:"></asp:Label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="lblPeq" Text="Apellidos"></asp:Label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblLugarNac" runat="server" AssociatedControlID="txtLugarNac" CssClass="lblPeq" Text="Lugar de nacimiento:"></asp:Label>
                    <asp:TextBox ID="txtLugarNac" runat="server" CssClass="txtPeq"></asp:TextBox>




                    <asp:Panel ID="pnTipDoc" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upTipDoc" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblEstadoCivil" runat="server" AssociatedControlID="ddlEstadoCivil" CssClass="lblPeq"
                                    Text="Est/Civil:"></asp:Label>
                                <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="pnSocioDdl">
                                    <asp:ListItem Value="1">SOLTERO</asp:ListItem>
                                    <asp:ListItem Value="2">CASADO</asp:ListItem>
                                    <asp:ListItem Value="3">DIVORCIADO</asp:ListItem>
                                    <asp:ListItem Value="4">VIUDO</asp:ListItem>
                                    <asp:ListItem Value="5">UNION LIBRE</asp:ListItem>
                                    <asp:ListItem Value="6">CLERIGO</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>


                    <asp:Label ID="lblProfesion" runat="server" AssociatedControlID="txtProfesion" CssClass="lblPeq" Text="Profesión:"></asp:Label>
                    <asp:TextBox ID="txtProfesion" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblNombresCon" runat="server" AssociatedControlID="txtNombresCon" CssClass="lblPeq" Text="Nombres/Cóny"></asp:Label>
                    <asp:TextBox ID="txtNombresCon" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblApellidosCon" runat="server" AssociatedControlID="txtApellidosCon" CssClass="lblPeq" Text="Apellidos/Cóny."></asp:Label>
                    <asp:TextBox ID="txtApellidosCon" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblFechaNac" runat="server" AssociatedControlID="txtFechaNac" CssClass="lblPeq" Text="Fecha/Nac."></asp:Label>
                    <asp:TextBox ID="txtFechaNac" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Panel ID="Panel4" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:Label ID="lblGenero" runat="server" AssociatedControlID="ddlGenero" CssClass="lblPeq" Text="Género"></asp:Label>
                                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="pnSocioDdl">
                                    <asp:ListItem Value="1">MASCULINO</asp:ListItem>
                                    <asp:ListItem Value="2">FEMENINO</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                </asp:Panel>

                <asp:Panel ID=Panel9 runat="server" CssClass="pnFormSocio" GroupingText="Dirección domicilio" ForeColor="#0033cc">
                    <asp:Label ID="lblDireccionDom" runat="server" AssociatedControlID="txtDireccionDom" CssClass="lblPeq"
                        Text="Dirección:"></asp:Label>
                    <asp:TextBox ID="txtDireccionDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblSectorDom" runat="server" AssociatedControlID="txtSectorDom" CssClass="lblPeq" Text="Sector:"></asp:Label>
                    <asp:TextBox ID="txtSectorDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblEdificioDom" runat="server" AssociatedControlID="txtEdificioDom" CssClass="lblPeq"
                        Text="Edificio:"></asp:Label>
                    <asp:TextBox ID="txtEdificioDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblPisoDom" runat="server" AssociatedControlID="txtPisoDom" CssClass="lblPeq" Text="Piso:"></asp:Label>
                    <asp:TextBox ID="txtPisoDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblDepartamentoDom" runat="server" AssociatedControlID="txtDepartamentoDom" CssClass="lblPeq"
                        Text="Departamento:"></asp:Label>
                    <asp:TextBox ID="txtDepartamentoDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblReferenciaDom" runat="server" AssociatedControlID="txtReferenciaDom" CssClass="lblPeq"
                        Text="Referencia:"></asp:Label>
                    <asp:TextBox ID="txtReferenciaDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblTelefonoDom" runat="server" AssociatedControlID="txtTelefonoDom" CssClass="lblPeq"
                        Text="Teléfono: fijo:"></asp:Label>
                    <asp:TextBox ID="txtTelefonoDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblCelularDom" runat="server" AssociatedControlID="txtCelularDom" CssClass="lblPeq" Text="Celular:"></asp:Label>
                    <asp:TextBox ID="txtCelularDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblEmailDom" runat="server" AssociatedControlID="txtEmailDom" CssClass="lblPeq" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmailDom" runat="server" CssClass="txtPeq" TextMode="SingleLine"></asp:TextBox>
                    <asp:Label ID="lblCiudadDom" runat="server" AssociatedControlID="txtCiudadDom" CssClass="lblPeq" Text="Ciudad"></asp:Label>
                    <asp:TextBox ID="txtCiudadDom" runat="server" CssClass="txtPeq"></asp:TextBox>
                </asp:Panel>

                <asp:Panel ID=Panel10 runat="server" CssClass="pnFormSocio" GroupingText="Dirección trabajo" ForeColor="#0033cc">
                    <asp:Label ID="lblEmpresa" runat="server" AssociatedControlID="txtEmpresa" CssClass="lblPeq" Text="Empresa:"></asp:Label>
                    <asp:TextBox ID="txtEmpresa" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblCiudad" runat="server" AssociatedControlID="txtCiudad" CssClass="lblPeq" Text="Ciudad:"></asp:Label>
                    <asp:TextBox ID="txtCiudad" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblDireccion" runat="server" AssociatedControlID="txtDireccion" CssClass="lblPeq" Text="Dirección:"></asp:Label>
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblSector" runat="server" AssociatedControlID="txtSector" CssClass="lblPeq" Text="Sector:"></asp:Label>
                    <asp:TextBox ID="txtSector" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblReferencia" runat="server" AssociatedControlID="txtReferencia" CssClass="lblPeq" Text="Referencia:"></asp:Label>
                    <asp:TextBox ID="txtReferencia" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblTelefono" runat="server" AssociatedControlID="txtTelefono" CssClass="lblPeq" Text="Teléfono fijo:"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="lblPeq" Text="E-mail:"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtPeq"></asp:TextBox>
                </asp:Panel>

            </fieldset>

            <fieldset id="Fieldset4" class="fieldset-principal">
                <legend></legend>

                <asp:Panel ID="pnMembrecia" runat="server" CssClass="pnFormSocio" GroupingText="Datos membrecía" ForeColor="#0033cc" Enabled="false">


                    <asp:Label ID="lblrucfac" runat="server" AssociatedControlID="txtRucFactura" CssClass="lblPeq" Text="Ruc/Fac:"></asp:Label>
                    <asp:TextBox ID="txtRucFactura" runat="server" CssClass="txtPeq" placeholder="Ruc-Fac"></asp:TextBox>

                    <asp:Panel ID="Panel6" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Visible="true" CssClass="lblPeq"></asp:Label>
                                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                    DataValueField="sucursal">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Label ID="lblNumFactura" runat="server" AssociatedControlID="txtNumFactura" CssClass="lblPeq" Text="#/factura:"></asp:Label>
                    <asp:TextBox ID="txtNumFactura" runat="server" CssClass="txtPeq" placeholder="# factura"></asp:TextBox>



                    <asp:Panel ID="Panel2" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblVendedor" runat="server" Text="Vendedor:" Visible="true" CssClass="lblPeq"></asp:Label>
                                <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="pnSocioDdl" DataValueField="RUC" DataTextField="VENDEDOR">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>




                    <asp:Panel ID="Panel3" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTipoMembrecia" runat="server" AssociatedControlID="ddlTipoMembrecia" CssClass="lblPeq"
                                    Text="Tipo/Memb.:"></asp:Label>
                                <asp:DropDownList ID="ddlTipoMembrecia" runat="server" CssClass="pnSocioDdl" OnSelectedIndexChanged="ddlTipoMembrecia_SelectedIndexChanged"
                                    DataTextField="descripcion"
                                    DataValueField="prefijo">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>




                    <asp:Label ID="lblFechaInicio" runat="server" AssociatedControlID="txtFechaInicio" CssClass="lblPeq"
                        Text="Fecha/Afil."></asp:Label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaInicio"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <act1:MaskedEditExtender ID="maskFecha" runat="server" BehaviorID="mee1" TargetControlID="txtFechaInicio"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                        AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />



                    <asp:Label ID="lblVencimiento" runat="server" AssociatedControlID="txtVencimiento" CssClass="lblPeq"
                        Text="Vencimiento:"></asp:Label>
                    <asp:TextBox ID="txtVencimiento" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtVencimiento"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" BehaviorID="mee1" TargetControlID="txtVencimiento"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                        AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />




                    <asp:Label ID="lblRenovacion" runat="server" AssociatedControlID="txtRenovacion" CssClass="lblPeq" Text="Ren/Nuevo"></asp:Label>
                    <asp:TextBox ID="txtRenovacion" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblenvio" runat="server" AssociatedControlID="ddlEnvio" CssClass="lblPeq" Text="EnviarA:"></asp:Label>

                    <asp:Panel ID="Panel5" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEnvio" runat="server" CssClass="pnSocioDdl" OnSelectedIndexChanged="ddlEnvio_SelectedIndexChanged">
                                    <asp:ListItem Value="0">ENVIAR A:</asp:ListItem>
                                    <asp:ListItem Value="1">DOMICILIO</asp:ListItem>
                                    <asp:ListItem Value="2">OFICINA</asp:ListItem>
                                    <asp:ListItem Value="3">SUCURSAL</asp:ListItem>
                                    <asp:ListItem Value="4">OTRO</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                    <asp:Label ID="lblNumContrato" runat="server" AssociatedControlID="txtNumContrato" CssClass="lblPeq"
                        Text="#/Contrato:"></asp:Label>
                    <asp:TextBox ID="txtNumContrato" runat="server" CssClass="txtPeq" AutoPostBack="True" placeholder="#/Contrato"></asp:TextBox>


                    <asp:TextBox ID="txtdirsuc" runat="server" CssClass="txtSForm" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtciusuc" runat="server" CssClass="txtSForm" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtte1suc" runat="server" CssClass="txtSForm" Visible="false"> </asp:TextBox>
                    <asp:TextBox ID="txtSucEmail" runat="server" CssClass="txtSForm" Visible="false"></asp:TextBox>
                </asp:Panel>



                <asp:Panel ID="pnTarjeta" runat="server" CssClass="pnFormSocio" GroupingText="Tarjeta del socio" ForeColor="#0033cc" Enabled="false">
                    <asp:Label ID="lblRucTar" runat="server" AssociatedControlID="txtRucTar" CssClass="lblPeq" Text="RUC/CC"></asp:Label>
                    <asp:TextBox ID="txtRucTar" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblNombresTar" runat="server" AssociatedControlID="txtNombresTar" CssClass="lblPeq" Text="Nombres:"></asp:Label>
                    <asp:TextBox ID="txtNombresTar" runat="server" CssClass="txtPeq" Enabled="true"></asp:TextBox>
                    <asp:Label ID="lblDesdeTar" runat="server" AssociatedControlID="txtDesdeTar" CssClass="lblPeq" Text="Desde"></asp:Label>
                    <asp:TextBox ID="txtDesdeTar" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblVenceTar" runat="server" AssociatedControlID="txtVenceTar" CssClass="lblPeq" Text="Vence:"></asp:Label>
                    <asp:TextBox ID="txtVenceTar" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblTipoMembTar" runat="server" AssociatedControlID="txtTipoMembTar" CssClass="lblPeq"
                        Text="Tipo/Memb.:"></asp:Label>
                    <asp:TextBox ID="txtTipoMembTar" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblNumContratoTar" runat="server" AssociatedControlID="txtNumContratoTar" CssClass="lblPeq"
                        Text="#/Contrato:"></asp:Label>
                    <asp:TextBox ID="txtNumContratoTar" runat="server" CssClass="txtPeq"></asp:TextBox>
                </asp:Panel>

                <asp:Panel ID="pnGuia" runat="server" CssClass="pnFormSocio" GroupingText="Guía de remisión al socio"
                    ForeColor="#0033cc"  Enabled="false">
                    <asp:Label ID="lblRucGuia" runat="server" AssociatedControlID="txtRucGuia" CssClass="lblPeq" Text="RUC/CC"></asp:Label>
                    <asp:TextBox ID="txtRucGuia" runat="server" CssClass="txtPeq" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblNombresGuia" runat="server" AssociatedControlID="txtNombresGuia" CssClass="lblPeq"
                        Text="Nombres:"></asp:Label>
                    <asp:TextBox ID="txtNombresGuia" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblDireccionGuia" runat="server" AssociatedControlID="txtDireccionGuia" CssClass="lblPeq"
                        Text="Dirección:"></asp:Label>
                    <asp:TextBox ID="txtDireccionGuia" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblCiudadGuia" runat="server" AssociatedControlID="txtCiudadGuia" CssClass="lblPeq" Text="Ciudad:"></asp:Label>
                    <asp:TextBox ID="txtCiudadGuia" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lblSectorGuia" runat="server" AssociatedControlID="txtSectorGuia" CssClass="lblPeq" Text="Sector:"></asp:Label>
                    <asp:TextBox ID="txtSectorGuia" runat="server" CssClass="txtPeq"></asp:TextBox>
                    <asp:Label ID="lbltelefonoGuia" runat="server" AssociatedControlID="txttelefonoGuia" CssClass="lblPeq"
                        Text="Teléfono:"></asp:Label>
                    <asp:TextBox ID="txttelefonoGuia" runat="server" CssClass="txtPeq"></asp:TextBox>
                </asp:Panel>

            </fieldset>

        </fieldset>
    </asp:Panel>
</asp:Content>

