<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="mantenimientoPersonas.aspx.cs" Inherits="Catalogo_mantenimientoPersonas" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true" Style="color: red; font-size: 0.8rem;">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
        <asp:Label ID="lblTipoConsulta" runat="server" Text="" Visible="false"></asp:Label>
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Buscar persona</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeqSoc" runat="server" Visible="true">
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Panel ID="pnInstruccion" runat="server" CssClass="pnFormDdl" Font-Size="Larger" ForeColor="White"
                    BackColor="#9aaff1">
                    <asp:DropDownList ID="ddlTipoMantenimiento" runat="server" OnSelectedIndexChanged="ddlTipoMantenimiento_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="-1">Seleccione mantenimiento</asp:ListItem>
                        <asp:ListItem Value="1">Cliente</asp:ListItem>
                        <asp:ListItem Value="2">Alumno</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="Label3" runat="server" Text="Buscar por:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="True"></asp:Label>
                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Visible="true" Font-Size="Larger" ForeColor="White"
                    BackColor="#9aaff1">
                    <asp:ListItem Value="0">RUC/C.C.</asp:ListItem>
                    <asp:ListItem Value="1">Apellidos-Nombres</asp:ListItem>
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtBuscar" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase"
                    BorderColor="#9aaff1"></asp:TextBox>

                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.ico" Width="27px" ToolTip="Buscar"
                    BorderColor="#9aaff1" OnClick="imgBuscar_Click" />



            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnBuscados" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnClientes" CssClass="pnPeqSoc" runat="server"
                ScrollBars="Vertical" Wrap="False" GroupingText="Cliente" Visible="false">
                <asp:Button ID="btnNuevoCliente" runat="server" Text="Nuevo cliente" Visible="true" OnClick="btnNuevoCliente_Click" />
                <asp:GridView ID="grvClientes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                    HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                    AutoGenerateSelectButton="True"
                    DataKeyNames="cli_ruc" CssClass="peqDdl" OnSelectedIndexChanged="grvClientes_SelectedIndexChanged1">

                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="RUC/C.C.">
                            <ItemTemplate>
                                <%# Eval("cli_ruc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("cli_telefono") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Celular">
                            <ItemTemplate>
                                <%# Eval("cli_celular") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-mail">
                            <ItemTemplate>
                                <%# Eval("cli_email") %>
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

            <asp:Panel ID="pnAlumnos" CssClass="pnPeqSoc" runat="server"
                ScrollBars="Vertical" Wrap="False" GroupingText="Alumno" Visible="false">

                <asp:GridView ID="grvAlumno" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                    HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                    AutoGenerateSelectButton="True" DataKeyNames="ALU_IDENTIFICACION" CssClass="peqDdl" OnSelectedIndexChanged="grvAlumno_SelectedIndexChanged">

                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="RUC/C.C.">
                            <ItemTemplate>
                                <%# Eval("alu_identificacion") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("alu_telefono") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Celular">
                            <ItemTemplate>
                                <%# Eval("alu_celular") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-mail">
                            <ItemTemplate>
                                <%# Eval("alu_email") %>
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
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDatosAlumno" runat="server" Style="display: flex; width: 60vw" Visible="false">

        <asp:Panel runat="server" ID="datos1" CssClass="pnFormDdl">
            <asp:TextBox ID="txtAlu_id" CssClass="txtForm" runat="server" AutoPostBack="True" Visible="true"></asp:TextBox>
            <asp:Panel ID="pnCedula" runat="server" Style="display: flex;">
                <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server" AutoPostBack="True" Enabled="false"></asp:TextBox>
                <asp:ImageButton ID="ibConsultar" runat="server" ImageUrl="~/images/iconos/219_2.png" Visible="false" />
            </asp:Panel>


            <asp:Label ID="lblApellidos" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
            <asp:TextBox ID="txtApellidos" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
            <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="lblDireccion" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
            <asp:TextBox ID="txtDireccion" CssClass="txtForm" runat="server"></asp:TextBox>
            
            <asp:Panel runat="server" ID="Panel1" >
                <asp:Panel ID="pnCelular" runat="server" CssClass="pnFormDdl">
                    <asp:Label ID="lblCelular" CssClass="lblForm" runat="server" Text="Celular"></asp:Label>
                    <asp:TextBox ID="txtCelular" CssClass="txtForm" runat="server" MaxLength="10"></asp:TextBox>
                    <act1:MaskedEditExtender ID="mskSuperPhone" runat="server"
                        TargetControlID="txtCelular"
                        ClearMaskOnLostFocus="false"
                        MaskType="None"
                        Mask="9999999999"
                        MessageValidatorTip="true"
                        InputDirection="LeftToRight"
                        ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
                </asp:Panel>
                <asp:Panel ID="pnTelefono" runat="server" CssClass="pnFormDdl" MaxLength="10">
                    <asp:Label ID="lblTelefono" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                    <asp:TextBox ID="txtTelefono" CssClass="txtForm" runat="server"></asp:TextBox>
                    <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtTelefono"
                        ClearMaskOnLostFocus="false"
                        MaskType="None"
                        Mask="9999999999"
                        MessageValidatorTip="true"
                        InputDirection="LeftToRight"
                        ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
                </asp:Panel>
                <asp:Panel ID="pnFechaNacimiento" runat="server" CssClass="pnFormDdl">
                    <asp:Label ID="lblFechaNacimiento" CssClass="lblForm" runat="server" Text="Fec/Nac"></asp:Label>
                    <asp:TextBox ID="txtFechaNacimiento" CssClass="txtForm" runat="server"></asp:TextBox>
                    <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaNacimiento"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaNacimiento"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                        AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />

                </asp:Panel>
                <asp:Panel ID="pnEmail" runat="server" CssClass="pnFormDdl">
                    <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>

                </asp:Panel>
                <asp:Panel ID="pnFactura" runat="server" CssClass="pnFormDdl" Visible="false">
                    <asp:Label ID="lblFactura" CssClass="lblForm" runat="server" Text="Factura"></asp:Label>
                    <asp:TextBox ID="txtFactura" CssClass="txtForm" runat="server"></asp:TextBox>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnDatos2">
            <asp:Panel ID="TipoSangre" runat="server" CssClass="pnFormDdl">
                <!--  <asp:Label ID="lblTipoSangre" CssClass="lblForm" runat="server" Text="Tipo de sangre"></asp:Label>-->
                <asp:DropDownList ID="ddlTipoSangre" runat="server" DataTextField="TS_TIPOSANGRE" DataValueField="TS_TIPOSANGRE">
                </asp:DropDownList>
            </asp:Panel>


            <asp:Panel ID="pnNacionalidad" runat="server" CssClass="pnFormDdl">
                <!--<asp:Label ID="lblNacionalidad" CssClass="lblForm" runat="server" Text="Nacionalidad"></asp:Label>-->
                <asp:DropDownList ID="ddlNacionalidad" runat="server" DataTextField="NAC_NACIONALIDAD" DataValueField="NAC_NACIONALIDAD">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="pnEstadoCivil" runat="server" CssClass="pnFormDdl">
                <!--<asp:Label ID="lblEstadoCivil" CssClass="lblForm" runat="server" Text="Estado Civil"></asp:Label>-->
                <asp:DropDownList ID="ddlEstadoCivil" runat="server" DataTextField="ECV_DESCRIPCION" DataValueField="ECV_DESCRIPCION">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Panel ID="pnGenero" runat="server" CssClass="pnFormDdl">
                <!--<asp:Label ID="lblGenero" CssClass="lblForm" runat="server" Text="Género"></asp:Label>-->
                <asp:DropDownList ID="ddlGenero" runat="server" DataTextField="GEN_GENERO" DataValueField="GEN_GENERO">
                </asp:DropDownList>
            </asp:Panel>




            <asp:Panel ID="pnInstruccionEscolar" runat="server" CssClass="pnFormDdl">
                <!--<asp:Label ID="lblInstruccionEsc" CssClass="lblForm" runat="server" Text="Instrucción escolar"></asp:Label>-->
                <asp:DropDownList ID="ddlInstruccionEscolar" runat="server" DataTextField="INS_NIVELINSTRUCCION" DataValueField="INS_ID">
                </asp:DropDownList>
            </asp:Panel>


            <asp:Panel ID="pnTipoLicencia" runat="server" CssClass="pnFormDdl">
                <!--<asp:Label ID="lblLicencia" CssClass="lblForm" runat="server" Text="Licencia de conducir"></asp:Label>-->
                <asp:DropDownList ID="ddlTipoLicencia" runat="server" DataTextField="TL_TIPOLICENCIA" DataValueField="TL_ID">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm"
                    OnClick="btnGuardar_Click" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRegresar_Click" />

            </asp:Panel>
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="pnDatosCliente" runat="server" Style="display: flex; width: 60vw" Visible="false">

        <asp:Panel runat="server" ID="Panel2" CssClass="pnFormDdl">
            <asp:TextBox ID="txtCli_id" CssClass="txtForm" runat="server" AutoPostBack="True" Visible="false"></asp:TextBox>
            <asp:Panel ID="pnCedulaC" runat="server" Style="display: flex;">
                <asp:Label ID="lblCedulaC" CssClass="lblForm" runat="server" Text="Doc/Ident. "></asp:Label>
                <asp:TextBox ID="txtCedulaC" CssClass="txtForm" runat="server" AutoPostBack="True"  Enabled="true"></asp:TextBox>
                <asp:ImageButton ID="ibtCedulaC" runat="server" ImageUrl="~/images/iconos/219_2.png" Visible="false" />
                <asp:Panel ID="pnTipoIdentificacion" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlTipoIdentificacion" DataTextField="TID_DESCRIPCION" DataValueField="TID_ID" runat="server">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="pnObligado" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlObligado" DataTextField="" DataValueField="" runat="server">
                        <asp:ListItem Value="-1">Seleccione obligado a llevar contabilidad</asp:ListItem>
                        <asp:ListItem Value="NO">NO</asp:ListItem>
                        <asp:ListItem Value="SI">SI</asp:ListItem>
                    </asp:DropDownList>


                </asp:Panel>
            </asp:Panel>

            <asp:Label ID="lblApellidoP" CssClass="lblForm" runat="server" Text="Primer apellido"></asp:Label>
            <asp:TextBox ID="txtApellidoP" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="lblApellidoM" CssClass="lblForm" runat="server" Text="Segundo apellido"></asp:Label>
            <asp:TextBox ID="txtApellidoM" CssClass="txtForm" runat="server"></asp:TextBox>


            <asp:Label ID="lblNombre" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
            <asp:TextBox ID="txtNombreC" CssClass="txtForm" runat="server"></asp:TextBox>

            <asp:Label ID="lblPrincipal" CssClass="lblForm" runat="server" Text="Calle principal"></asp:Label>
            <asp:TextBox ID="txtPrincipal" CssClass="txtForm" runat="server"></asp:TextBox>

            <asp:Label ID="lblNumero" CssClass="lblForm" runat="server" Text="Numeral"></asp:Label>
            <asp:TextBox ID="txtNumeral" CssClass="txtForm" runat="server"></asp:TextBox>

            <asp:Label ID="lblSecundaria" CssClass="lblForm" runat="server" Text="Calle secundaria"></asp:Label>
            <asp:TextBox ID="txtSecundaria" CssClass="txtForm" runat="server"></asp:TextBox>

            <asp:Label ID="lblSector" CssClass="lblForm" runat="server" Text="Sector"></asp:Label>
            <asp:TextBox ID="txtSector" CssClass="txtForm" runat="server"></asp:TextBox>

            <asp:Panel ID="pnTelefonoC" runat="server" CssClass="pnFormDdl">
                <asp:Label ID="lblTelefonoC" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTelefonoC" CssClass="txtForm" runat="server" MaxLength="10"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtTelefonoC"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="9999999999"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
            </asp:Panel>
            <asp:Panel ID="pnCelularC" runat="server" CssClass="pnFormDdl" MaxLength="10">
                <asp:Label ID="lblCelularC" CssClass="lblForm" runat="server" Text="Celular"></asp:Label>
                <asp:TextBox ID="txtCelularC" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                    TargetControlID="txtCelularC"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="9999999999"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
            </asp:Panel>
            <asp:Panel ID="pnEmailc" runat="server" CssClass="pnFormDdl">
                <asp:Label ID="lbl" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtEmailc" CssClass="txtForm" runat="server"></asp:TextBox>

            </asp:Panel>
            <asp:Panel ID="pnBcliente" runat="server" CssClass="pnFormBotonera" Style="margin-top: 50px;">
                <asp:Button ID="btnBcliente" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnBcliente_Click" />
                <asp:Button ID="btnRegresarC" runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRegresar_Click" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>




</asp:Content>

