<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true" CodeFile="colaborador3.aspx.cs"
    Inherits="Catalogo_colaborador3" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/grid.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
    <asp:Panel ID="pnCabecera" runat="server">
        <asp:Panel ID="pnContieneSucursal" runat="server" Style="margin-bottom: 0.5rem">
            <asp:Label ID="lblSucursal" CssClass="" runat="server" Text="Sucursal" Style="font-size: 1rem;"></asp:Label>
            <asp:Panel ID="pnSucursal" runat="server" CssClass="">
                <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server" Style="font-size: 1rem; color: blue;" AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" CssClass="pnFormBotonera">
                <asp:Button ID="btnNuevoColaborador" runat="server" Text="Nuevo Colaborador" CssClass="btnForm" OnClick="btnNuevoColaborador_Click" />
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx" Style="border: solid; font-size: 1rem; font-weight: 700"></asp:HyperLink>
            </asp:Panel>

        </asp:Panel>
        <asp:Label ID="lblSubirFoto" CssClass="lblForm" runat="server" Text="Subir foto"></asp:Label>
        <asp:Panel ID="pnSubirFoto" runat="server" CssClass="">
            <div style="margin-bottom: 1rem; margin-top: 1rem;">
                <asp:FileUpload ID="ulCarga" runat="server" />
                <asp:Button ID="btnCargar" runat="server" Text="Cargar archivo" OnClick="btnCargar_Click" />
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnDetalleNomina">
        <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />
        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir listado de colaboradores" CssClass="btnForm"
            OnClick="btnImprimir_Click" />

        <asp:GridView ID="grvColaboradores" runat="server"
            DataKeyNames="cedula"
            AutoGenerateColumns="False"
            CssClass="grillaGral" BorderColor="Blue" BorderStyle="Double"
            OnSelectedIndexChanged="grvColaboradores_SelectedIndexChanged" ForeColor="Blue">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />

                <asp:BoundField DataField="Row" HeaderText="#" />
                <asp:BoundField DataField="cedula" HeaderText="Cédula" />
                <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="nombres" HeaderText="Nombres" />


                <asp:BoundField DataField="fechaIng" HeaderText="Fecha de ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="funcion" HeaderText="Cargo" HeaderStyle-CssClass="ancho" ItemStyle-CssClass="ancho" />
                <asp:BoundField DataField="RELACION" HeaderText="Relación laboral" />

                <asp:ImageField DataImageUrlField="foto" HeaderText="Imagen" AlternateText="Foto no encontrada" DataAlternateTextField="textoAlterno" NullImageUrl="~/admArchivos/nomina/sinImagen.jpg" NullDisplayText="Path de imagen es NULL" ControlStyle-CssClass="foto" ControlStyle-Font-Size="12px">
                    <ControlStyle CssClass="foto" />
                </asp:ImageField>

                <asp:CheckBoxField DataField="activo">
                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" CssClass="checkBox" ForeColor="Blue" Height="20px" Width="20px" />
                </asp:CheckBoxField>


            </Columns>

            <EditRowStyle ForeColor="Blue" />
            <EmptyDataRowStyle Font-Bold="True" ForeColor="Blue" />

        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="pnActualizacion" runat="server" Visible="false">


        <asp:Panel ID="pnColaborador" runat="server">
            <fieldset id="sucursal">


                <legend>Datos del colaborador(a)</legend>
                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>

                    <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server" Enabled="false" OnTextChanged="txtCedula_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <act1:MaskedEditExtender ID="meCedula" runat="server"
                        TargetControlID="txtCedula"
                        ClearMaskOnLostFocus="false"
                        MaskType="None"
                        Mask="9999999999"
                        MessageValidatorTip="true"
                        InputDirection="LeftToRight"
                        ErrorTooltipEnabled="True"></act1:MaskedEditExtender>



                </asp:Panel>
                <asp:Label ID="lblApellidos" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
                <asp:TextBox ID="txtApellidos" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:FilteredTextBoxExtender ID="ftApellidos"
                    runat="server" Enabled="True" TargetControlID="txtApellidos"
                    ValidChars="abcdefghijklmnopqrstuvwxyzñáéíóú ABCDEFGHIJKLMNOPQRSTUVWXYZÑÁÉÍÓÚ"></act1:FilteredTextBoxExtender>

                <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:FilteredTextBoxExtender ID="ftNombres"
                    runat="server" Enabled="True" TargetControlID="txtNombres"
                    ValidChars="abcdefghijklmnopqrstuvwxyzñáéíóú ABCDEFGHIJKLMNOPQRSTUVWXYZÑÁÉÍÓÚ"></act1:FilteredTextBoxExtender>


                <asp:Label ID="lblNombreCorto" CssClass="lblForm" runat="server" Text="Nombre corto"></asp:Label>
                <asp:TextBox ID="txtNombreCorto" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:FilteredTextBoxExtender ID="ftNombreCorto"
                    runat="server" Enabled="True" TargetControlID="txtNombreCorto"
                    ValidChars="abcdefghijklmnopqrstuvwxyzñáéíóú ABCDEFGHIJKLMNOPQRSTUVWXYZÑÁÉÍÓÚ"></act1:FilteredTextBoxExtender>

                <asp:Label ID="lblEmailDomicilio" CssClass="lblForm" runat="server" Text="E-mail Personal"></asp:Label>
                <asp:TextBox ID="txtEmailDomicilio" CssClass="txtForm" runat="server"></asp:TextBox>

                <asp:Label ID="lblEmailCorporativo" CssClass="lblForm" runat="server" Text="E-mail Corporativo"></asp:Label>
                <asp:TextBox ID="txtEmailCorporativo" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Label ID="lblCcosto" CssClass="lblForm" runat="server" Text="C. Costo"></asp:Label>
                <asp:Panel ID="pnCcosto" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCcosto" DataTextField="nom_cco" DataValueField="mae_cco" runat="server">
                    </asp:DropDownList>
                </asp:Panel>




                <asp:Label ID="lblCargo" CssClass="lblForm" runat="server" Text="Cargo"></asp:Label>
                <asp:Panel ID="pnCargo" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCargo" DataTextField="descripcion" DataValueField="cargo" runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblTelefono" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="mskSuperPhone" runat="server"
                    TargetControlID="txtTelefono"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="99-9999999"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>

                <asp:Label ID="lblMovil" CssClass="lblForm" runat="server" Text="Móvil"></asp:Label>
                <asp:TextBox ID="txtMovil" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtMovil"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="99-99999999"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>

                <asp:Label ID="lblFechaNacimiento" CssClass="lblForm" runat="server" Text="Fecha de nacimiento"></asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaNacimiento"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaNacimiento" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:Label ID="lblFechaIngreso" CssClass="lblForm" runat="server" Text="Fecha de ingreso"></asp:Label>
                <asp:TextBox ID="txtFechaIngreso" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender2" PopupButtonID="" runat="server" TargetControlID="txtFechaIngreso"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtFechaIngreso" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />


                <asp:Label ID="lblGenero" CssClass="lblForm" runat="server" Text="Sexo"></asp:Label>
                <asp:Panel ID="pnGenero" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlGenero" DataTextField="" DataValueField="" runat="server">
                        <asp:ListItem Value="-1">Seleccione género</asp:ListItem>
                        <asp:ListItem Value="1">Masculino</asp:ListItem>
                        <asp:ListItem Value="2">Femenino</asp:ListItem>
                        <asp:ListItem Value="3">De vez en cuando</asp:ListItem>
                        <asp:ListItem Value="4">Seguido</asp:ListItem>
                        <asp:ListItem Value="5">Cuando no estan los niños</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblEstadoCivil" CssClass="lblForm" runat="server" Text="Estado civil"></asp:Label>
                <asp:Panel ID="pnEstadoCivil" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlEstadoCivil" DataTextField="descripcion" DataValueField="id_estadoCivil" runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblTipoEmpleo" CssClass="lblForm" runat="server" Text="Tipo de relación laboral"></asp:Label>
                <asp:Panel ID="pnTipoEmpleo" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlTipoEmpleo" DataTextField="" DataValueField="" runat="server">
                        <asp:ListItem Value="-1">Seleccione relación laboral</asp:ListItem>
                        <asp:ListItem Value="1">Nómina</asp:ListItem>
                        <asp:ListItem Value="2">Servicios prestados</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>



                <asp:Label ID="lblFoto" CssClass="lblForm" runat="server" Text="Foto" Visible="false"></asp:Label>
                <asp:TextBox ID="txtFoto" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>

                <asp:Label ID="lblAlterno" CssClass="lblForm" runat="server" Text="Alterno" Visible="false"></asp:Label>
                <asp:TextBox ID="txtAlterno" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>

                <asp:Label ID="lblInstructorPractica" CssClass="lblForm" runat="server" Text="Instructor de práctica"></asp:Label>
                <asp:Panel ID="pninstructorPractica" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlinstructorPractica" DataTextField="" DataValueField="" runat="server">
                        <asp:ListItem Value="-1">Seleccione </asp:ListItem>
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="2">No</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Label ID="lblPro_cuentacontable" CssClass="lblForm" runat="server" Text="Cuenta contable"></asp:Label>
                <asp:Panel ID="pnCtaCble" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCtaCble" DataTextField="nom_cta" DataValueField="mae_cue" runat="server">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Colaborador activo"></asp:Label>
                <asp:Panel ID="pnActivo" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID="chkActivo" TextAlign="Left" runat="server" />
                </asp:Panel>




                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRegresar_Click" />

                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="lblUltimaModificacion" CssClass="lblFormAviso" runat="server" Text="Última modificación"></asp:Label>
    </asp:Panel>

</asp:Content>

