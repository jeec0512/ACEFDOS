<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="verificarCertificado.aspx.cs"
    Inherits="Escuela_verificarCertificado" EnableEventValidation="false" %>

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
            <legend>verificar certificado sicopráctico</legend>
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
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnPeqDdl" Visible="false">
                    <asp:UpdatePanel ID="upSucursal" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal que certifica" Visible="false" CssClass="lblPeq"></asp:Label>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                DataValueField="sucursal">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:Label ID="lblCodigo" runat="server" Text="Ingrese código:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtCodigo" Font-Size="Larger" ForeColor="darkblue" Style="text-transform: uppercase" BorderColor="#9aaff1"></asp:TextBox>
                <asp:Label ID="lblContrato" runat="server" Text="#Contrato" Font-Bold="True"
                    Font-Size="Larger" ForeColor="darkblue" Visible="false"></asp:Label>
                <asp:TextBox runat="server" ID="txtContrato" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1" Visible="false"></asp:TextBox>
                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.ico" Width="27px" ToolTip="Buscar"
                    BorderColor="#9aaff1" OnClick="imgBuscar_Click" />
                <asp:Label ID="lblMsg" runat="server" Text="MSG" Font-Bold="True" Font-Size="Small" ForeColor="red"
                    Visible="false"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="pnGuardar" CssClass="pnPeq" runat="server" Wrap="False">
                <asp:Button ID="btnCancelar" runat="server" CssClass=btnProceso Text="Regresar" Visible="true" />

            </asp:Panel>

        </fieldset>
        <asp:Panel ID="pnCertificado" CssClass="" runat="server" Visible="false">
            <fieldset id="Fieldset1" class="fieldset-principal">
                <legend>Certificado sicopráctico</legend>


                <article>
                    <iframe width="98%" height="600" src="http://www.aneta.org.ec:5090/acefdos/Escuela/imprimirSicopractico.aspx/"
                        frameborder="0"
                        allow="autoplay; encrypted-media" allowfullscreen id="frCertificado"></iframe>

                </article>


            </fieldset>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

