<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="creacionHorarios.aspx.cs"
    Inherits="Escuela_asignacionHorarios" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Creación de cupos</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblMateria" CssClass="lblForm" runat="server" Text="Materia" Visible="true"></asp:Label>
                <asp:Panel ID="pnMateria" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlMateria" DataTextField="mat_descripcion" DataValueField="mat_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Asignar" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>


    <asp:Panel ID="pnHorarios" runat="server">
        <fieldset id="fsHorarios">
            <legend>Listado de aulas , horas, autos y días para talleres</legend>


            <asp:Panel ID="pnAutoDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double" Style="max-height: 200px; overflow-y: scroll;">
                <asp:GridView ID="grvAutoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" OnRowCommand="grvAutoDetalle_RowCommand"
                    OnRowDataBound="grvAutoDetalle_RowDataBound" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="veh_activo" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image"
                            ImageUrl="~/images/iconos/grabar.ico" CommandName="Jusveh" ItemStyle-Width="60" Visible="true">
                            <ItemStyle Width="60px" />
                        </asp:ButtonField>

                        <asp:BoundField DataField="veh_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                            HeaderStyle-CssClass="DisplayNone"></asp:BoundField>
                        <asp:BoundField DataField="veh_numero" HeaderText="# vehículo" Visible="false" />
                        <asp:BoundField DataField="numplaca" HeaderText="vehículo" Visible="true" />
                        <asp:BoundField DataField="veh_placa" HeaderText="Placa" Visible="false" />
                        <asp:BoundField DataField="veh_marca" HeaderText="Marca" Visible="false" />
                        <asp:BoundField DataField="veh_modelo" HeaderText="Modelo" Visible="false" />
                        <asp:BoundField DataField="veh_anio" HeaderText="Año" Visible="false" />
                        <asp:BoundField DataField="veh_motor" HeaderText="Motor" Visible="false" />
                        <asp:BoundField DataField="veh_chasis" HeaderText="Chasis" Visible="false" />

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

            <asp:Panel ID="pnAulaDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double" Style="max-height: 200px; overflow-y: scroll;">


                <asp:GridView ID="grvAulaDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAulaDetalle_RowCommand"
                    OnRowDataBound="grvAulaDetalle_RowDataBound">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="aul_activo" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                            CommandName="Jusaula" ItemStyle-Width="60" Visible="true" />
                        <asp:BoundField DataField="aul_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="aul_escuela" HeaderText="Escuela" Visible="true" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="aul_descripcion" HeaderText="Descripción aula" Visible="true" />
                        <asp:BoundField DataField="aul_capacidad" HeaderText="Capacidad" Visible="true" />
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

            <asp:Panel ID="pnHorarioDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double" Style="max-height: 200px; overflow-y: scroll;">


                <asp:GridView ID="grvHorarioDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="50" ShowFooter="false"
                    OnRowCommand="grvHorarioDetalle_RowCommand" OnRowDataBound="grvHorarioDetalle_RowDataBound">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="hor_estado" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico"
                            CommandName="Jushorario" ItemStyle-Width="60" Visible="true" />
                        <asp:BoundField DataField="hor_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                            HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="hor_inicio" HeaderText="Hora de inicio" Visible="true" />
                        <asp:BoundField DataField="hor_fin" HeaderText="Hora de finalización" Visible="true" />
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

            <asp:Panel ID="pnFechasDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double" Style="max-height: 200px; overflow-y: scroll;">


                <asp:GridView ID="grvTallerDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="50" ShowFooter="false"
                    OnRowCommand="grvTallerDetalle_RowCommand" OnRowDataBound="grvTallerDetalle_RowDataBound">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="TAL_ESTADO" HeaderText="Estado" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:ButtonField HeaderText="Marcar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/grabar.ico" CommandName="Jusfecha" ItemStyle-Width="60" Visible="true" />
                        <asp:BoundField DataField="tal_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
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
        </fieldset>
    </asp:Panel>



    <!--listado de asignaciones -->
    <asp:Panel ID="pnAsignaciones" runat="server">
        <fieldset id="fsAsignaciones">
            <legend>Cupos asignados</legend>
            <asp:Panel ID="pnCreacion" runat="server">
                <asp:GridView ID="grvAsignaciones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvAsignaciones_RowCommand" OnRowDataBound="grvAsignaciones_RowDataBound">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>

                        <asp:BoundField DataField="asm_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="aul_descripcion" HeaderText="Aula" Visible="true" />
                        <asp:BoundField DataField="vehiculo" HeaderText="Vehículo" Visible="true" />
                        <asp:BoundField DataField="hor_inicio" HeaderText="Hora inicio" Visible="true" />
                        <asp:BoundField DataField="hor_fin" HeaderText="Hora fin" Visible="true" />
                        <asp:BoundField DataField="asm_disponible" HeaderText="Disponibilidad" Visible="true" />
                        <asp:BoundField DataField="asm_registrado" HeaderText="# de registrados" Visible="true" />
                        <asp:BoundField DataField="tal_fecha" HeaderText="Fecha de taller" Visible="true" DataFormatString="{0:D}" />
                        <asp:ButtonField HeaderText="Eliminar" Text="..." ButtonType="Image"
                            ImageUrl="~/images/iconos/garbage.png" CommandName="EliminaReg" ItemStyle-Width="10px" Visible="true">
                            <ItemStyle Width="60px" />
                        </asp:ButtonField>

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
        </fieldset>
    </asp:Panel>


    <script src="../js/funciones.js"></script>

</asp:Content>

