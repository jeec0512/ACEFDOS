<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="horariosAsignados.aspx.cs" Inherits="Escuela_horariosAsignados" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">

    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos/pestanas.css" rel="stylesheet" />


    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Horarios asignados</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Seleccione modalidad</asp:ListItem>
                        <asp:ListItem Value="1">15 días</asp:ListItem>
                        <asp:ListItem Value="2">7 días</asp:ListItem>
                        <asp:ListItem Value="3">Fines de semana</asp:ListItem>
                        <asp:ListItem Value="4">Curso corporativo</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>

                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>



                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Asignar" CssClass="btnForm" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <div class="contPest">
        <!--ESCOGER HORARIOS-->


        <!--EDUCACION BASICA MECANICA(PARA 15 DIAS)-->
        <!--<a href="#" class="cabPest c-activa  text" onclick="mostrarPestana(0)">Educación Básica</a>-->

        <asp:Button runat="server" ID="btnEducBas" CssClass="c-activa text" Text="Educación Básica" OnClick="btnEducBas_Click" />

        <asp:Panel ID="pnDetalle15" runat="server" CssClass="pestana p-activa">
            <div id="Fieldset1" class="contHoja h-activa">


                <asp:Panel ID="pnAulaDetalle15" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnHorarioDetalle15" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

            </div>
        </asp:Panel>


        <!--MECANICA(PARA FINES DE SEMANA Y 7 DIAS) PSICOLOGIA Y PRIMEROS AUXILIOS-->

        <!--MECANICA-->
        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(1)">Mecánica</a>-->
        <asp:Button runat="server" ID="btnMecanica" CssClass="c-activa text" Text="Mecánica" OnClick="btnMecanica_Click" />

        <asp:Panel ID="pnDetalleMecanica" runat="server" CssClass="pestana  p-activa">
            <div id="Fieldset2" class="contHoja h-activa">


                <asp:Panel ID="pnAulaDetalleMecanica" CssClass="pnPeq" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnHorarioDetalleMecanica" CssClass="pnPeq" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnFechasDetalleMecanica" CssClass="pnPeq" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium" Font-Strikeout="False" />
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

            </div>
        </asp:Panel>

        <!--PRIMEROS AUXILIOS-->
        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(2)">Primeros Auxilios</a>-->
        <asp:Button runat="server" ID="btnPrimAux" CssClass="c-activa text" Text="Primeros Auxilios" OnClick="btnPrimAux_Click" />
        <asp:Panel ID="pnDetallePrimeroAuxilios" runat="server" CssClass="pestana  p-activa">
            <div id="Fieldset3" class="contHoja h-activa">


                <asp:Panel ID="pnAulaDetallePrimeroAuxilios" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnHorarioDetallePrimeroAuxilios" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnFechasDetallePrimeroAuxilios" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

            </div>
        </asp:Panel>

        <!--PSICOLOGIA-->
        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(3)">psicología</a>-->
        <asp:Button runat="server" ID="btnPsico" CssClass="c-activa text" Text="Psicología" OnClick="btnPsico_Click" />
        <asp:Panel ID="pnDetallePsicologia" runat="server" CssClass="pestana p-activa">
            <div id="Fieldset4" class="contHoja h-activa">


                <asp:Panel ID="pnAulaDetallePsicologia" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnHorarioDetallePsicologia" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

                <asp:Panel ID="pnFechasDetallePsicologia" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

            </div>
        </asp:Panel>

        <!--PRACTICA-->
        <!--<a href="#" class="cabPest c-activa text" onclick="mostrarPestana(4)">Práctica</a>-->
        <asp:Button runat="server" ID="btnPrac" CssClass="c-activa text" Text="Práctica" OnClick="btnPrac_Click" />
        <asp:Panel ID="pnDetalleAuto" runat="server" CssClass="pestana p-activa">
            <div id="fsHorariosAuto" class="contHoja h-activa">


                <asp:Panel ID="pngrvAutoDetalle" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">
                    <asp:GridView ID="grvAutoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                        BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                        Width="90%"
                        AllowSorting="True" PageSize="5"
                        CssClass="Rojo" OnRowCommand="grvAutoDetalle_RowCommand" OnRowDataBound="grvAutoDetalle_RowDataBound">
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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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


                <asp:Panel ID="pnHorarioDetalleAuto" runat="server" Visible="true" Style="max-height: 200px; overflow-y: scroll;" CssClass="pnPeq">


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
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
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

            </div>
        </asp:Panel>

    </div>


    <script src="../js/funciones.js"></script>

</asp:Content>

