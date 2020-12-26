<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true"
    CodeFile="alumno.aspx.cs" Inherits="Catalogo_alumno" EnableEventValidation="false" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAlumno" runat="server">
            <fieldset id="fsAlumno">
                <legend>Datos del alumno(a)</legend>
                <div style="display: none;">
                    <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>
                    <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                    </asp:DropDownList>
                </div>

                <asp:Panel ID="Panel2" runat="server" CssClass="pnFormTitulo">
                    <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                    <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server" AutoPostBack="True"></asp:TextBox>
                    <asp:ImageButton ID="ibConsultar" runat="server" ImageUrl="~/images/iconos/219_2.png" OnClick="ibConsultar_Click" />
                </asp:Panel>

                <asp:Label ID="lblApellidos" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
                <asp:TextBox ID="txtApellidos" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblDireccion" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
                <asp:TextBox ID="txtDireccion" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Label ID="lblCelular" CssClass="lblForm" runat="server" Text="Celular"></asp:Label>
                <asp:TextBox ID="txtCelular" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="mskSuperPhone" runat="server"
                    TargetControlID="txtCelular"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="(999)999-999999"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
                <asp:Label ID="lblTelefono" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="txtTelefono"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="(999)999-999999"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
                <asp:Label ID="lblTipoSangre" CssClass="lblForm" runat="server" Text="Tipo de sangre"></asp:Label>
                <asp:TextBox ID="txtTipoSangre" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNacionalidad" CssClass="lblForm" runat="server" Text="Nacionalidad"></asp:Label>
                <asp:TextBox ID="txtNacionalidad" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Panel ID="pnEstadoCivil" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlEstadoCivil" runat="server">
                        <asp:ListItem Value="-1">Seleccione estado civil</asp:ListItem>
                        <asp:ListItem Value="1">Soltero</asp:ListItem>
                        <asp:ListItem Value="2">Casado</asp:ListItem>
                        <asp:ListItem Value="3">Divorciado</asp:ListItem>
                        <asp:ListItem Value="4">Unión libre</asp:ListItem>
                        <asp:ListItem Value="5">Separado</asp:ListItem>
                        <asp:ListItem Value="6">Viudo</asp:ListItem>
                        <asp:ListItem Value="7">Clérigo</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="pnGenero" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlGenero" runat="server">
                        <asp:ListItem Value="-1">Seleccione género</asp:ListItem>
                        <asp:ListItem Value="1">Masculino</asp:ListItem>
                        <asp:ListItem Value="2">Femenino</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>



                <asp:Label ID="lblFechaNacimiento" CssClass="lblForm" runat="server" Text="Fecha de nacimiento"></asp:Label>
                <asp:TextBox ID="txtFechaNacimiento" CssClass="txtForm" runat="server"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaNacimiento"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaNacimiento"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />


                <asp:Label ID="lblEmail" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
                <asp:TextBox ID="txtEmail" CssClass="txtForm" runat="server"></asp:TextBox>



                <asp:Panel ID="pnInstruccion" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlInstruccion" runat="server">
                        <asp:ListItem Value="-1">Instrución escolar</asp:ListItem>
                        <asp:ListItem Value="1">Ninguna</asp:ListItem>
                        <asp:ListItem Value="2">Inicial</asp:ListItem>
                        <asp:ListItem Value="3">Bachillerato</asp:ListItem>
                        <asp:ListItem Value="4">Superior</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Panel ID="pnLicencia" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlLicencia" runat="server">
                        <asp:ListItem Value="-1">Licencia de conducir</asp:ListItem>
                        <asp:ListItem Value="1">Ninguna</asp:ListItem>
                        <asp:ListItem Value="2">A</asp:ListItem>
                        <asp:ListItem Value="3">B</asp:ListItem>
                        <asp:ListItem Value="4">C</asp:ListItem>
                        <asp:ListItem Value="5">D</asp:ListItem>
                        <asp:ListItem Value="6">E</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblFactura" CssClass="lblForm" runat="server" Text="Factura"></asp:Label>
                <asp:TextBox ID="txtFactura" CssClass="txtForm" runat="server"></asp:TextBox>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <!--<asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>-->
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRegresar_Click"/>
                </asp:Panel>


            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

