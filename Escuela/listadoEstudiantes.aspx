<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="listadoEstudiantes.aspx.cs" Inherits="Escuela_listadoEstudiantes"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->

    <asp:Panel ID="pnHojas" CssClass="" runat="server" Visible="false">
        <asp:Button ID="btnHorario" runat="server" Text="Ocultar Horario" OnClick="btnHorario_Click" />
        <asp:Label ID="lblTipoConsulta" runat="server" Text="" Visible="false"></asp:Label>

    </asp:Panel>

    <asp:Panel ID="pnMensaje" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true" ForeColor="Red" Font-Size="Medium"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="false">
        <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
            AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
        </asp:DropDownList>
    </asp:Panel>
    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnMatricula" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Listados</legend>
            <asp:Panel ID="pnEscuelaMain" runat="server" CssClass="" GroupingText="" ForeColor="#0033cc" Style="display: flex;">

                <asp:Panel ID="pnAsignacion" runat="server">
                    <fieldset id="fsAsignacion">
                        <legend>Seleccionar datos generales</legend>

                        <asp:Label ID="lblEducacionVial" CssClass="lblPeq" runat="server" Text="Escuela" Visible="true"></asp:Label>

                        <asp:Panel ID="pnEscuela" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlEscuela" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Label ID="lblModalidad" CssClass="lblPeq" runat="server" Text="Modalidad"></asp:Label>
                        <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Label ID="lblCurso" CssClass="lblPeq" runat="server" Text="Curso asignado" Visible="true"></asp:Label>

                        <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 60%" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>
                <!--
                <asp:Panel ID="pnEducacionVialMain" runat="server">
                    <fieldset id="fsEducacionVialMain" style="display: flex; justify-content: space-between; flex-direction: column;">
                        <legend>Educación vial</legend>

                        <asp:Panel ID="pnEducacionVialAulas" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblEducacionVialAulas" CssClass="lblPeq" runat="server" Text="Aulas"></asp:Label>
                            <asp:DropDownList ID="ddlEducacionVialAulas" DataTextField="AUL_DESCRIPCION" DataValueField="AUL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pnEducacionVialHoras" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblEducacionVialHoras" CssClass="lblPeq" runat="server" Text="Horas"></asp:Label>
                            <asp:DropDownList ID="ddlEducacionVialHoras" DataTextField="HOR_INICIO" DataValueField="HOR_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pnImpEducVial" CssClass="" runat="server" Wrap="False" Style="margin-top: 0.5rem;">
                            <asp:Button ID="btnImpEducVial" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnImprimirEV_Click" />
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>

                <asp:Panel ID="pnMecanicaMain" runat="server">
                    <fieldset id="fsMecanicaMain" style="display: flex; justify-content: space-between; flex-direction: column;">
                        <legend>Mecánica</legend>
                        <asp:Panel ID="pnMecanicaAula" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblMecanicaAula" CssClass="lblPeq" runat="server" Text="Aulas"></asp:Label>
                            <asp:DropDownList ID="ddlMecanicaAula" DataTextField="AUL_DESCRIPCION" DataValueField="AUL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnMecanicaHora" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblMecanicaHora" CssClass="lblPeq" runat="server" Text="Horas"></asp:Label>
                            <asp:DropDownList ID="ddlMecanicaHora" DataTextField="HOR_INICIO" DataValueField="HOR_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnMecanicaFecha" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblMecanicaFecha" CssClass="lblPeq" runat="server" Text="Fecha"></asp:Label>
                            <asp:DropDownList ID="ddlMecanicaFecha" DataTextField="TAL_FECHA" DataValueField="TAL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" CssClass="" runat="server" Wrap="False" Style="margin-top: 0.5rem;">
                            <asp:Button ID="btnImprimirMec" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnImprimirMec_Click" />
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>

                <asp:Panel ID="pnPrimerosAuxilios" runat="server">
                    <fieldset id="fsPrimerosAuxiliosMain" style="display: flex; justify-content: space-between; flex-direction: column;">
                        <legend>Primeros Auxilios</legend>
                        <asp:Panel ID="pnPrimerosAuxiliosAulas" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPrimerosAuxiliosAulas" CssClass="lblPeq" runat="server" Text="Aulas"></asp:Label>
                            <asp:DropDownList ID="ddlPrimerosAuxiliosAulas" DataTextField="AUL_DESCRIPCION" DataValueField="AUL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnPrimerosAuxiliosHoras" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPrimerosAuxiliosHoras" CssClass="lblPeq" runat="server" Text="Horas"></asp:Label>
                            <asp:DropDownList ID="ddlPrimerosAuxiliosHoras" DataTextField="HOR_INICIO" DataValueField="HOR_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnPrimerosAuxiliosFecha" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPrimerosAuxiliosFecha" CssClass="lblPeq" runat="server" Text="Fecha"></asp:Label>
                            <asp:DropDownList ID="ddlPrimerosAuxiliosFecha" DataTextField="TAL_FECHA" DataValueField="TAL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnImprimirPA" CssClass="" runat="server" Wrap="False" Style="margin-top: 0.5rem;">
                            <asp:Button ID="btnImprimirPA" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnImprimirPA_Click" />
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>

                <asp:Panel ID="pnPsicologiaMain" runat="server">
                    <fieldset id="Fieldset3" style="display: flex; justify-content: space-between; flex-direction: column;">
                        <legend>Psicología</legend>
                        <asp:Panel ID="pnPsicologiaAulas" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPsicologiaAulas" CssClass="lblPeq" runat="server" Text="Aulas"></asp:Label>
                            <asp:DropDownList ID="ddlPsicologiaAulas" DataTextField="AUL_DESCRIPCION" DataValueField="AUL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnPsicologiaHoras" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPsicologiaHoras" CssClass="lblPeq" runat="server" Text="Horas"></asp:Label>
                            <asp:DropDownList ID="ddlPsicologiaHoras" DataTextField="HOR_INICIO" DataValueField="HOR_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnPsicologiaFecha" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPsicologiaFecha" CssClass="lblPeq" runat="server" Text="Fecha"></asp:Label>
                            <asp:DropDownList ID="ddlPsicologiaFecha" DataTextField="TAL_FECHA" DataValueField="TAL_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="Panel3" CssClass="" runat="server" Wrap="False" Style="margin-top: 0.5rem;">
                            <asp:Button ID="btnImprimirPs" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnImprimirPs_Click" />
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>

                -->

                <asp:Panel ID="pnPracticaMain" runat="server">
                    <fieldset id="fsPracticaMain" style="display: flex; justify-content: space-between; flex-direction: column;">
                        <legend>Práctica</legend>
                        
                        <!--<asp:Panel ID="pnPracticaAutos" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPracticaAutos" CssClass="lblPeq" runat="server" Text="Autos"></asp:Label>
                            <asp:DropDownList ID="ddlPracticaAutos" DataTextField="VEHICULO" DataValueField="VEH_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnPracticaHoras" runat="server" CssClass="" Visible="true">
                            <asp:Label ID="lblPracticaHoras" CssClass="lblPeq" runat="server" Text="Horas"></asp:Label>
                            <asp:DropDownList ID="ddlPracticaHoras" DataTextField="HOR_INICIO" DataValueField="HOR_ID" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>-->
                        <asp:Panel ID="Panel4" CssClass="" runat="server" Wrap="False" Style="margin-top: 0.5rem;">
                            <asp:Button ID="btnImprimirPrac" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnImprimirPrac_Click" />
                        </asp:Panel>
                    </fieldset>
                </asp:Panel>

            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnMaterias" CssClass="" runat="server" Visible="true">
        <fieldset id="fsMaterias" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="Panel2" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">
                <asp:Button ID="btnVistaPrevia" runat="server" Text="Vista previa" CssClass="btnForm"
                    OnClick="btnVistaPrevia_Click" />
                <asp:Button ID="btnExcelPe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelPe_Click" />
            </asp:Panel>
            <asp:Panel ID="pnListado" CssClass="" runat="server" Visible="true" Style="display: grid; grid-template-columns: auto auto auto auto auto;">
                <asp:GridView ID="grvListado" runat="server"></asp:GridView>
            </asp:Panel>
        </fieldset>
    </asp:Panel>





</asp:Content>

