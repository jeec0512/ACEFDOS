<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="matricularAlumno.aspx.cs"
    Inherits="Escuela_matricularAlumno" EnableEventValidation="false" %>

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
            <legend>matriculación</legend>
            <asp:Panel ID="pnCabecera" CssClass="pnBuscarGrid" runat="server">

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnPeqDdl" Visible="true">
                    <asp:UpdatePanel ID="upSucursal" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblSucursal" runat="server" Text="Escuela de matrícula" Visible="true" CssClass="lblPeq"></asp:Label>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                DataValueField="sucursal" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

                <asp:Label ID="lblEstudiante" runat="server" Text="Ingrese CC/RUC del estudiante:" Font-Bold="True" Font-Size="Larger"
                    ForeColor="darkblue"
                    Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtEstudiante" Font-Size="Larger" ForeColor="darkblue"
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
                    ID="btnGuardar" runat="server" CssClass="btnProceso" Text="Grabar" Visible="true"
                    OnClick="btnGuardar_Click" />
                <asp:Button
                    ID="btnCancelar" runat="server" CssClass="btnProceso" Text="Regresar" Visible="true" />
                <asp:Button ID="btnImprimir" runat="server" CssClass="btnProceso" Text="Imprimir matrícula" OnClick="btnImprimir_Click" />
            </asp:Panel>

        </fieldset>

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Datos generales</legend>

            <fieldset id="Fieldset7" class="fieldset-principal">
                <legend></legend>

                <asp:Panel ID="Panel8" runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                    <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha" CssClass="lblPeq"
                        Text="Fec/matr."></asp:Label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="txtPeq" placeholder="Fecha de la matrícula"></asp:TextBox>


                    <asp:Panel ID="pnSucursal2" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upSucursal2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblSucursal2" runat="server" Text="Suc/factura" Visible="true" CssClass="lblPeq"></asp:Label>
                                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                    DataValueField="sucursal" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSucursal2_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <asp:Label ID="lblRuc" runat="server" AssociatedControlID="txtRuc" CssClass="lblPeq" Text="RUC/CC"></asp:Label>
                    <asp:TextBox ID="txtRuc" runat="server" CssClass="txtPeq" AutoPostBack="True" Enabled="true"
                        placeholder="# de identidad" OnTextChanged="txtRuc_TextChanged"></asp:TextBox>


                    <asp:Panel ID="pnFactura" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upFactura" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblFactura" runat="server" Text="#factura" Visible="true" CssClass="lblPeq"></asp:Label>
                                <asp:DropDownList ID="ddlFactura" runat="server" CssClass="pnSocioDdl" DataTextField="factura"
                                    DataValueField="fac_id">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>


                    <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="lblPeq" Text="Nombres:"></asp:Label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="txtPeq" placeholder="Nombres del cliente" Enabled="false"></asp:TextBox>
                    <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="lblPeq" Text="Apellidos"></asp:Label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="txtPeq" placeholder="Apellidos del cliente" Enabled="false"></asp:TextBox>

                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="lblPeq" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtPeq" placeholder="Correo electrónico"></asp:TextBox>
                    <asp:Panel ID="pnCurso" runat="server" CssClass="pnPeqDdl">
                        <asp:UpdatePanel ID="upCurso" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblCurso" runat="server" AssociatedControlID="ddlCurso" CssClass="lblPeq" Text="Curso:"></asp:Label>
                                <asp:DropDownList ID="ddlCurso" runat="server" CssClass="pnSocioDdl" DataTextField="cur_nomenclatura"
                                    DataValueField="cur_id" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>




                </asp:Panel>

                <asp:Panel ID="Panel3" runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                    <asp:Panel ID="pnHorarios" runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                        <asp:Panel ID="pnHEducacionVial" runat="server">
                            <asp:UpdatePanel ID="upHEducacionVial" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblHEducacionVial" runat="server" AssociatedControlID="ddlHEducacionVial" Text="Horario Educación Vial:"></asp:Label>
                                    <asp:DropDownList ID="ddlHEducacionVial" runat="server" DataTextField="asm_descripcion" DataValueField="asm_id">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>


                        <asp:Panel ID="pnHMecanica" runat="server" CssClass="pnPeqDdl">
                            <asp:UpdatePanel ID="upHMecanica" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblHMecanica" runat="server" AssociatedControlID="ddlHMecanica" Text="Horario Mecánica"></asp:Label>
                                    <asp:DropDownList ID="ddlHMecanica" runat="server" DataTextField="asm_descripcion" DataValueField="asm_id">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>


                        <asp:Panel ID="pnHPrimerosAuxilios" runat="server" CssClass="pnPeqDdl">
                            <asp:UpdatePanel ID="upHPrimerosAuxilios" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblHPrimerosAuxilios" runat="server" AssociatedControlID="ddlHPrimerosAuxilios" Text="Primeros Auxilios:"></asp:Label>
                                    <asp:DropDownList ID="ddlHPrimerosAuxilios" runat="server" DataTextField="asm_descripcion" DataValueField="asm_id">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>


                        <asp:Panel ID="pnHPsicologia" runat="server" CssClass="pnPeqDdl">
                            <asp:UpdatePanel ID="upHPsicologia" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblHPsicologia" runat="server" AssociatedControlID="ddlHPsicologia" Text="Psicología:"></asp:Label>
                                    <asp:DropDownList ID="ddlHPsicologia" runat="server" DataTextField="asm_descripcion" DataValueField="asm_id">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>

                        <asp:Panel ID="pnPractica" runat="server" CssClass="pnPeqDdl">
                            <asp:UpdatePanel ID="upPractica" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblHPractica" runat="server" AssociatedControlID="ddlHPractica" Text="Práctica:"></asp:Label>
                                    <asp:DropDownList ID="ddlHPractica" runat="server" DataTextField="asm_descripcion" DataValueField="asm_id">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>



                    </asp:Panel>

                </asp:Panel>
            </fieldset>
        </fieldset>
    </asp:Panel>







</asp:Content>

