<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="matricularAlumno2.aspx.cs" Inherits="Escuela_matricularAlumno2" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->

    <asp:Panel ID="pnHojas" CssClass="" runat="server" Visible="true">
        <asp:Button ID="btnCliente" runat="server" Text="Ocultar Cliente" OnClick="btnCliente_Click" />
        <asp:Button ID="btnMatricula" runat="server" Text="Ocultar Matrícula" OnClick="btnMatricula_Click" />
        <asp:Button ID="btnHorario" runat="server" Text="Ocultar Horario" OnClick="btnHorario_Click" />
        <asp:Button ID="btn_abmAlumno" runat="server" Text="Ocultar Alumno" OnClick="btn_abmAlumno_Click" />
        <asp:Label ID="lblTipoConsulta" runat="server" Text="" Visible="false"></asp:Label>

    </asp:Panel>

    <asp:Panel ID="pnMensaje" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true" ForeColor="Red" Font-Size="Medium"></asp:Label>
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnFacturaGral" CssClass="" runat="server" Visible="true">

        <fieldset id="fdFactura" class="fieldset-principal">
            <legend>Cliente Datos de la factura</legend>
            <asp:Panel ID="pnCabecera" CssClass="pnBuscarGrid" runat="server">

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnPeqDdl" Visible="true">
                    <asp:UpdatePanel ID="upSucursal" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal de emisión de la factura:" Visible="true" CssClass="lblPeq"></asp:Label>
                            <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="pnSocioDdl" DataTextField="nom_suc"
                                DataValueField="sucursal" AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <asp:Panel ID="pnCliente" runat="server" CssClass="pnPeqDdl">
                    <asp:Label ID="lblCliente" runat="server" Text="CC/RUC del cliente:" CssClass="lblPeq" Visible="true"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCliente" CssClass="lblPeq"></asp:TextBox>
                    <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.png"  ToolTip="Buscar" BorderColor="#9aaff1" OnClick="imgBuscar_Click" />
                    <asp:Label ID="lblNombresCliente" runat="server" Text="" CssClass="lblPeq" Visible="true"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnFactura" runat="server" CssClass="pnPeqDdl">
                    <asp:UpdatePanel ID="upFactura" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblFactura" runat="server" Text="#factura" Visible="true" CssClass="lblPeq"></asp:Label>
                            <asp:DropDownList ID="ddlFactura" runat="server" CssClass="pnSocioDdl" DataTextField="factura"
                                DataValueField="factura" Style="color: blue; font-size: 0.8rem">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </asp:Panel>


            </asp:Panel>

            <asp:Panel ID="pnMensaje2" CssClass="pnPeqDdl" runat="server" Wrap="False">
                <asp:Label ID="lblMensaje2" runat="server" Text="Juan Perez se ha matriculado en el curso A001-19 que comprende de lunes a viernes en días hábiles" Visible="true"></asp:Label>
            </asp:Panel>



        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnMatricula" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Matrícula</legend>



            <asp:Panel ID="pnAlumno" runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                <fieldset id="Fieldset7">
                    <legend>Alumno</legend>
                    <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha" CssClass="lblPeq" Text="Fec/matr." style="margin-right:0.1rem;padding-right:0;padding-left:0"></asp:Label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="txtPeq" placeholder="Fecha de la matrícula" style="margin-right:0;padding-right:0;padding-left:0"></asp:TextBox>

                    <div style="display: inline-block">
                        <asp:Label ID="lblEstudiante" runat="server" AssociatedControlID="txtEstudiante" CssClass="lblPeq" Text="CC-estudiante" style="margin-right:0.1rem;padding-right:0;padding-left:0"></asp:Label>
                        <asp:TextBox ID="txtEstudiante" runat="server" CssClass="txtPeq" AutoPostBack="True" Enabled="true"
                            placeholder="# de identidad" style="margin-right:0;padding-right:0;padding-left:0"></asp:TextBox>
                        <asp:ImageButton ID="btnBuscaAlumno" runat="server" ImageUrl="~/images/iconos/219.png" ToolTip="Buscar" BorderColor="#9aaff1" OnClick="btnBuscaAlumno_Click" />

                    </div>

                    <asp:Label ID="lblNombres" runat="server" AssociatedControlID="txtNombres" CssClass="lblPeq" Text="Nombres:" style="margin-right:0.1rem;padding-right:0;padding-left:0"></asp:Label>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="txtPeq" placeholder="Nombres del cliente" Enabled="false" style="margin-right:0;padding-right:0;padding-left:0"></asp:TextBox>
                    <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="lblPeq" Text="Apellidos" style="margin-right:0.1rem;padding-right:0;padding-left:0"></asp:Label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="txtPeq" placeholder="Apellidos del cliente" Enabled="false" style="margin-right:0;padding-right:0;padding-left:0"></asp:TextBox>

                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="lblPeq" Text="E-mail" style="margin-right:0.1rem;padding-right:0;padding-left:0"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtPeq" placeholder="Correo electrónico" style="margin-right:0;padding-right:0;padding-left:0"></asp:TextBox>
                </fieldset>
            </asp:Panel>


            <asp:Panel ID="pnEscuela" runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                <asp:Panel ID="pnAsignacion" runat="server">
                    <fieldset id="fsAsignacion">
                        <legend>Escuela</legend>

                        <asp:Label ID="lblEscuela" CssClass="lblPeq" runat="server" Text="Escuela" Visible="true"></asp:Label>

                        <asp:Panel ID="pnEscuela2" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlEscuela" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 80%">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Label ID="lblModalidad" CssClass="lblPeq" runat="server" Text="Modalidad"></asp:Label>
                        <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModalidad" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" Style="font-size: 0.7rem; width: 60%" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                                <asp:ListItem Value="1">15 días</asp:ListItem>
                                <asp:ListItem Value="2">7 días</asp:ListItem>
                                <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                                <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                                <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                                <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
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
            </asp:Panel>

            <asp:Panel ID="pnLinkAlumno" runat="server" CssClass="pnFormSocio" GroupingText="" ForeColor="#0033cc">
                <asp:Panel ID="Panel3" runat="server">
                    <fieldset id="Fieldset2">
                        <legend>Creación de alumno</legend>
                        <asp:ImageButton ID="btnAlumno" runat="server" ImageUrl="~/images/iconos/161.ico" Width="27px" ToolTip="Buscar" BorderColor="#9aaff1" OnClick="btnAlumno_Click" />

                    </fieldset>
                </asp:Panel>
            </asp:Panel>


        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnMaterias" CssClass="" runat="server" Visible="true">
        <fieldset id="fsMaterias" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnAsignacionGral" CssClass="" runat="server" Visible="true" Style="display: grid; grid-template-columns: auto auto auto auto auto;">



                <asp:Panel ID="pnEducVial1" runat="server" GroupingText="" ForeColor="#0033cc" >
                    <fieldset id="fsEducVial">
                        <legend>Educación Vial</legend>
                        <!--ESCOGER HORARIOS-->


                        <!--EDUCACION VIAL MECANICA(PARA 15 DIAS)-->
                        <!--<a href="#" class="cabPest c-activa  text" onclick="mostrarPestana(0)">Educación Básica</a>-->

                        <!--<asp:Button runat="server" ID="btnEducBas" CssClass="c-activa text" Text="Educación Básica" />-->

                        <asp:Panel ID="pnModEducVial" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModEducVial" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlModEducVial_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                                <asp:ListItem Value="1">15 días</asp:ListItem>
                                <asp:ListItem Value="2">7 días</asp:ListItem>
                                <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                                <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                                <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                                <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>

                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnCurEducVial" runat="server" CssClass="pnFormDdl">

                            <asp:DropDownList ID="ddlCurEducVial" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCurEducVial_SelectedIndexChanged">
                            </asp:DropDownList>

                        </asp:Panel>

                        <asp:Panel ID="pnAulaDetalle15" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem">


                            <asp:GridView ID="grvAulaDetalle15" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAulaDetalle15_RowCommand" OnRowDataBound="grvAulaDetalle15_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="aul_activo" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="aula15" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción aula" Visible="true" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnHorarioDetalle15" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvHorarioDetalle15" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvHorarioDetalle15_RowCommand" OnRowDataBound="grvHorarioDetalle15_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="hor_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="horario15" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />
                                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />



                                </Columns>

                            </asp:GridView>

                        </asp:Panel>


                    </fieldset>
                </asp:Panel>

                <asp:Panel ID="pnMecanica1" runat="server" GroupingText="" ForeColor="#0033cc" >
                    <fieldset id="fsMecanica">
                        <legend>Mecánica</legend>
                        <!--MECANICA(PARA FINES DE SEMANA Y 7 DIAS) PSICOLOGIA Y PRIMEROS AUXILIOS-->

                        <!--MECANICA-->
                        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(1)">Mecánica</a>-->
                        <!-- <asp:Button runat="server" ID="btnMecanica" CssClass="c-activa text" Text="Mecánica" />-->


                        <asp:Panel ID="pnModMecanica" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModMecanica" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlModMecanica_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                                <asp:ListItem Value="1">15 días</asp:ListItem>
                                <asp:ListItem Value="2">7 días</asp:ListItem>
                                <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                                <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                                <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                                <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>


                        <asp:Panel ID="pnCurMecanica" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlCurMecanica" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCurMecanica_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pnAulaDetalleMecanica" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">
                            <asp:GridView ID="grvAulaDetalleMecanica" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAulaDetalleMecanica_RowCommand" OnRowDataBound="grvAulaDetalleMecanica_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="aul_activo" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="mecAula" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción aula" Visible="true" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnHorarioDetalleMecanica" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvHorarioDetalleMecanica" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvHorarioDetalleMecanica_RowCommand" OnRowDataBound="grvHorarioDetalleMecanica_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="hor_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="mecHora" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />
                                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />



                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnFechasDetalleMecanica" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvFechasDetalleMecanica" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="10" ShowFooter="false" OnRowCommand="grvFechasDetalleMecanica_RowCommand" OnRowDataBound="grvFechasDetalleMecanica_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="TAL_ESTADO" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png" CommandName="mecFecha" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="tal_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="TAL_FECHA" HeaderText="Fecha" Visible="true" DataFormatString="{0:D}" />


                                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                    </fieldset>

                </asp:Panel>

                <asp:Panel ID="pnPrimerosAuxilios1" runat="server" GroupingText="" ForeColor="#0033cc" >
                    <fieldset id="fsPrimerosAuxilios">
                        <legend>Primeros Auxilios</legend>
                        <!--PRIMEROS AUXILIOS-->
                        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(2)">Primeros Auxilios</a>-->
                        <!--<asp:Button runat="server" ID="btnPrimAux" CssClass="c-activa text" Text="Primeros Auxilios" />-->

                        <asp:Panel ID="pnModPrimerosAuxilios" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModPrimerosAuxilios" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlModPrimerosAuxilios_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                                <asp:ListItem Value="1">15 días</asp:ListItem>
                                <asp:ListItem Value="2">7 días</asp:ListItem>
                                <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                                <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                                <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                                <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pnCurPrimerosAuxilios" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlCurPrimerosAuxilios" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCurPrimerosAuxilios_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pnAulaDetallePrimeroAuxilios" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvAulaDetallePrimeroAuxilios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAulaDetallePrimeroAuxilios_RowCommand" OnRowDataBound="grvAulaDetallePrimeroAuxilios_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="aul_activo" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png" CommandName="paAula" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción aula" Visible="true" />



                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnHorarioDetallePrimeroAuxilios" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvHorarioDetallePrimeroAuxilios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvHorarioDetallePrimeroAuxilios_RowCommand" OnRowDataBound="grvHorarioDetallePrimeroAuxilios_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="hor_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="paHora" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />
                                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />



                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnFechasDetallePrimeroAuxilios" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvFechasDetallePrimeroAuxilios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="10" ShowFooter="false" OnRowCommand="grvFechasDetallePrimeroAuxilios_RowCommand" OnRowDataBound="grvFechasDetallePrimeroAuxilios_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="TAL_ESTADO" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png" CommandName="paFecha" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="tal_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="TAL_FECHA" HeaderText="Fecha" Visible="true" DataFormatString="{0:D}" />


                                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>


                    </fieldset>
                </asp:Panel>


                <asp:Panel ID="pnPsicologia1" runat="server" GroupingText="" ForeColor="#0033cc">
                    <fieldset id="fsPsicologia">
                        <legend>Psicología</legend>
                        <!--PSICOLOGIA-->
                        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(3)">psicología</a>-->
                        <!--<asp:Button runat="server" ID="btnPsico" CssClass="c-activa text" Text="Psicología" />-->

                        <asp:Panel ID="pnModPsicologia" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModPsicologia" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlModPsicologia_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                                <asp:ListItem Value="1">15 días</asp:ListItem>
                                <asp:ListItem Value="2">7 días</asp:ListItem>
                                <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                                <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                                <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                                <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnCurPsicologia" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlCurPsicologia" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCurPsicologia_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnAulaDetallePsicologia" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvAulaDetallePsicologia" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAulaDetallePsicologia_RowCommand" OnRowDataBound="grvAulaDetallePsicologia_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="aul_activo" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="psiAula" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción aula" Visible="true" />



                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnHorarioDetallePsicologia" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvHorarioDetallePsicologia" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvHorarioDetallePsicologia_RowCommand" OnRowDataBound="grvHorarioDetallePsicologia_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="hor_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="psiHora" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />
                                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />



                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                        <asp:Panel ID="pnFechasDetallePsicologia" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvFechasDetallePsicologia" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="10" ShowFooter="false" OnRowCommand="grvFechasDetallePsicologia_RowCommand" OnRowDataBound="grvFechasDetallePsicologia_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="TAL_ESTADO" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png" CommandName="psiFecha" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="tal_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="TAL_FECHA" HeaderText="Fecha" Visible="true" DataFormatString="{0:D}" />


                                    <asp:BoundField DataField="sucursal" HeaderText="Sucursal" Visible="false" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>


                    </fieldset>
                </asp:Panel>

                <asp:Panel ID="pnPractica1" runat="server" GroupingText="" ForeColor="#0033cc" >
                    <fieldset id="fsPractica">
                        <legend>Práctica</legend>
                        <!--PRACTICA-->
                        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(4)">Práctica</a>-->
                        <!--<asp:Button runat="server" ID="btnPrac" CssClass="c-activa text" Text="Práctica" />-->

                        <asp:Panel ID="pnModPractica" runat="server" CssClass="pnFormDdl">
                            <asp:DropDownList ID="ddlModPractica" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlModPractica_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                                <asp:ListItem Value="1">15 días</asp:ListItem>
                                <asp:ListItem Value="2">7 días</asp:ListItem>
                                <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                                <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                                <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                                <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnCurPractica" runat="server" CssClass="pnFormDdl" Visible="true">
                            <asp:DropDownList ID="ddlCurPractica" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlCurPractica_SelectedIndexChanged">
                            </asp:DropDownList>
                        </asp:Panel>

                        <asp:Panel ID="pngrvAutoDetalle" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">
                            <asp:GridView ID="grvAutoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="5" OnRowCommand="grvAutoDetalle_RowCommand" OnRowDataBound="grvAutoDetalle_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="veh_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image"
                                        ImageUrl="~/images/iconos/grabar2.png" CommandName="pracAuto" ItemStyle-Width="60" Visible="true">
                                        <ItemStyle Width="60px" />
                                    </asp:ButtonField>

                                    <asp:BoundField DataField="veh_id" HeaderText="Código" Visible="true"></asp:BoundField>
                                    <asp:BoundField DataField="vehiculo" HeaderText="# vehículo" Visible="true" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>


                        <asp:Panel ID="pnHorarioDetalleAuto" runat="server" Style="max-height: 200px; overflow-y: scroll; margin-bottom: 1.5rem;">


                            <asp:GridView ID="grvHorarioDetalleAuto" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                                Width="90%"
                                AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvHorarioDetalleAuto_RowCommand" OnRowDataBound="grvHorarioDetalleAuto_RowDataBound">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                    <asp:BoundField DataField="hor_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                    <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar2.png"
                                        CommandName="pracHora" ItemStyle-Width="60" Visible="true" />
                                    <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" />
                                    <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />
                                    <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />
                                </Columns>

                            </asp:GridView>

                        </asp:Panel>


                    </fieldset>
                </asp:Panel>



            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnAcciones" CssClass="" runat="server" Visible="true">
        <fieldset id="fsAcciones" class="fieldset-principal">
            <legend></legend>

            <asp:Panel ID="pnGuardar" CssClass="pnPeq" runat="server" Wrap="False">
                <asp:Button ID="btnGuardar" runat="server" CssClass="btnProceso" Text="Grabar" Visible="true" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnImprimir" runat="server" CssClass="btnProceso" Text="Imprimir comprobante" />
                <asp:Button ID="btnCancelar" runat="server" CssClass="btnProceso" Text="Regresar" Visible="true" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <!--CREACION DE ALUMNO -->

    <asp:Panel ID="pnCreaAlumno" runat="server" Visible="true">
        <fieldset id="fsAlumno">
            <legend>Datos del alumno(a)</legend>
            <div style="display: none;">
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
            </div>

            <asp:Panel ID="Panel5" runat="server" CssClass="pnFormTitulo">
                <asp:Label ID="lblCedula" CssClass="lblForm" runat="server" Text="Documento de identificación "></asp:Label>
                <asp:TextBox ID="txtCedula" CssClass="txtForm" runat="server" AutoPostBack="True"></asp:TextBox>
                <asp:ImageButton ID="ibConsultar" runat="server" ImageUrl="~/images/iconos/219_2.png" OnClick="ibConsultar_Click" />
            </asp:Panel>

            <asp:Label ID="lblApellidoAlumno" CssClass="lblForm" runat="server" Text="Apellidos"></asp:Label>
            <asp:TextBox ID="txtApellidoAlumno" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="lblNombreAlumno" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
            <asp:TextBox ID="txtNombreAlumno" CssClass="txtForm" runat="server"></asp:TextBox>
            <asp:Label ID="lblDireccion" CssClass="lblForm" runat="server" Text="Dirección"></asp:Label>
            <asp:TextBox ID="txtDireccion" CssClass="txtForm" runat="server"></asp:TextBox>


            <asp:Label ID="lblCelular" CssClass="lblForm" runat="server" Text="Celular"></asp:Label>
            <asp:TextBox ID="txtCelular" CssClass="txtForm" runat="server"></asp:TextBox>
            <act1:MaskedEditExtender ID="mskSuperPhone" runat="server"
                TargetControlID="txtCelular"
                ClearMaskOnLostFocus="false"
                MaskType="None"
                Mask="99-99999999"
                MessageValidatorTip="true"
                InputDirection="LeftToRight"
                ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
            <asp:Label ID="lblTelefono" CssClass="lblForm" runat="server" Text="Teléfono"></asp:Label>
            <asp:TextBox ID="txtTelefono" CssClass="txtForm" runat="server"></asp:TextBox>
            <act1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                TargetControlID="txtTelefono"
                ClearMaskOnLostFocus="false"
                MaskType="None"
                Mask="99-9999999"
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


            <asp:Label ID="lblEmailAlumno" CssClass="lblForm" runat="server" Text="E-mail"></asp:Label>
            <asp:TextBox ID="txtEmailAlumno" CssClass="txtForm" runat="server"></asp:TextBox>



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
            <asp:Label ID="Label7" CssClass="lblForm" runat="server" Text="Factura"></asp:Label>
            <asp:TextBox ID="txtFactura" CssClass="txtForm" runat="server"></asp:TextBox>


            <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                <asp:Button ID="btnGuardarAlumno" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardarAlumno_Click" />
                <!--<asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>-->
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRegresar_Click" />
            </asp:Panel>


        </fieldset>
    </asp:Panel>
</asp:Content>

