<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="creacionHorarios2.aspx.cs" Inherits="Escuela_creacionHorarios2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server"><link href="../App_Themes/estilos/menu.css" rel="stylesheet" />
    <link href="https://file.myfontastic.com/82NMqBMRcbALbXYak8fmp/icons.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,700,900" rel="stylesheet">
    <link rel="stylesheet" href="../App_Themes/estilos/fonts.css">

    <link rel="stylesheet" href="../App_Themes/estilos/icons.css">
    <link href="../App_Themes/estilos/grilla.css" rel="stylesheet" />

    <link rel="stylesheet" href="../App_Themes/estilos/activarCurso.css">

    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <div class="main-activarCurso">

        <div class="header-activarCurso item">
            <asp:Label runat="server" ID="lblMensaje" class="error-msg" Visible="true"><span class="icon-cancelcirculo"></span></asp:Label>
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>

        <div class="areaActivarCurso main-principalActivarCurso item">
            <div class="areaSelects main-principalActivarCurso__select subitem">

                <h3 class="titulo3">Seleccione</h3>

                <div class="containerSelect">
                    <asp:Panel ID="pnSucursal" runat="server" CssClass="mainSelect" Visible="true">
                        <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                            AutoPostBack="True" CssClass="mainSelect__item" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>

                    <asp:Panel ID="pnModalidad" runat="server" CssClass="mainSelect" Visible="true">
                        <asp:DropDownList ID="ddlModalidad" DataTextField="MOD_DESCRIPCION" DataValueField="MOD_ID" runat="server"
                            AutoPostBack="True" CssClass="mainSelect__item" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>
                    <asp:Panel ID="pnCurso" runat="server" CssClass="mainSelect" Visible="true">
                        <asp:DropDownList ID="ddlCurso" DataTextField="CUR_NOMENCLATURA" DataValueField="CUR_ID" runat="server"
                            AutoPostBack="True" CssClass="mainSelect__item" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>

                   <asp:Panel ID="pnMateria" runat="server" CssClass="mainSelect" Visible="true">
                    <asp:DropDownList ID="ddlMateria" DataTextField="mat_descripcion" DataValueField="mat_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlMateria_SelectedIndexChanged"  CssClass="mainSelect__item" >
                    </asp:DropDownList>
                </asp:Panel>
                </div>
            </div>

            <div class="areaHorarios main-principalActivarCurso__horariosDisponibles subitem">

                <h3 class="titulo3">Horarios</h3>
                <div class="contieneHorasDisponibles">
                    <asp:Panel ID="pnAutos" runat="server" Visible="false" class="horariosDisponibles__item areaAutos" Style="overflow-y: auto;">
                        <h4>AUTOS</h4>
                        <asp:GridView ID="grvAutos" runat="server" AutoGenerateColumns="False" CssClass="grilla">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbAutoHeader" runat="server" AutoPostBack="True" OnCheckedChanged="cbAutoHeader_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbAutoConfirmar" runat="server" OnCheckedChanged="cbAutoConfirmar_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdAuto" runat="server" Text='<%# Bind("VEH_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="VEH_ID" HeaderText="id" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                <asp:BoundField DataField="NUMPLACA" HeaderText="Placa y #" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="pnAulas" runat="server" Visible="false" class="horariosDisponibles__item areaAulas" Style="overflow-y: auto;">
                        <h4>AULAS</h4>
                        <asp:GridView ID="grvAulas" runat="server" AutoGenerateColumns="False" CssClass="grilla">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbAulaHeader" runat="server" AutoPostBack="True" OnCheckedChanged="cbAulaHeader_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbAulaConfirmar" runat="server" OnCheckedChanged="cbAulaConfirmar_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdAula" runat="server" Text='<%# Bind("AUL_ID") %>' CssClass="noDesplegar"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AUL_ID" HeaderText="Id" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" Visible="false" />
                                <asp:BoundField DataField="AUL_DESCRIPCION" HeaderText="Aulas" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="pnHoras" runat="server" Visible="false" class="horariosDisponibles__item areaHoras" Style="overflow-y: auto;">
                        <h4>HORAS</h4>
                        <asp:GridView ID="grvHoras" runat="server" AutoGenerateColumns="False" CssClass="grilla">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbHoraHeader" runat="server" AutoPostBack="True" OnCheckedChanged="cbHoraHeader_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbHoraConfirmar" runat="server" OnCheckedChanged="cbHoraConfirmar_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdHora" runat="server" Text='<%# Bind("HOR_ID") %>' CssClass="noDesplegar"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="HOR_ID" HeaderText="id" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" Visible="false" />
                                <asp:BoundField DataField="HOR_INICIO" HeaderText="Hora de inicio" />
                                <asp:BoundField DataField="HOR_FIN" HeaderText="Hora de finalización" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="pnDias" runat="server" Visible="false" class="horariosDisponibles__item areaDias" Style="overflow-y:auto;">
                        <h4>DÍAS</h4>
                        <asp:GridView ID="grvDias" runat="server" AutoGenerateColumns="False" CssClass="grilla">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbDiaHeader" runat="server" AutoPostBack="True" OnCheckedChanged="cbDiaHeader_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbDiaConfirmar" runat="server" OnCheckedChanged="cbDiaConfirmar_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdDia" runat="server" Text='<%# Bind("TAL_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TAL_ID" HeaderText="id" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                                <asp:BoundField DataField="TAL_FECHA" HeaderText="Fecha" DataFormatString="{0:D}" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                </div>
            </div>


            <div class="areaBotones main-principalActivarCurso__botonera subitem">
                <div class="contieneBotones">
                    <h3 class="titulo3">Acciones</h3>
                    <div class="cajaBotones">
                        <div class="buttonHolder">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="button button-title" Text="Activar" BorderStyle="None" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" CssClass="button button-title" Text="Cancelar" BorderStyle="None" Visible="false"/>
                            <asp:Button ID="btnImprimir" runat="server" CssClass="button button-title" Text="Imprimir" BorderStyle="None" Visible="false" />
                            <asp:Button ID="btnRegresar" runat="server" CssClass="button button-title" Text="Regresar" BorderStyle="None" Visible="false"/>
                        </div>
                    </div>
                </div>
            </div>

            <div class="areaAsignados main-principalActivarCurso__horariosAsignados subitem" id="dvListado" runat="server">
                <h3 class="titulo3">Listado de horarios activos</h3>
                <asp:GridView ID="grvActivaciones" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvActivaciones_RowCommand" CssClass="grilla">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbActivarHeader" runat="server" AutoPostBack="True" OnCheckedChanged="cbActivarHeader_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbActivarConfirmar" runat="server" OnCheckedChanged="cbActivarConfirmar_CheckedChanged" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblIdActivar" runat="server" Text='<%# Bind("asm_id") %>' CssClass="noDesplegar"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:BoundField DataField="asm_id" HeaderText="ID" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" Visible="false" />
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

                <div class="areaBotones main-principalActivarCurso__botonera subitem">
                <div class="contieneBotones">
                    <h3 class="titulo3"></h3>
                    <div class="cajaBotones">
                        <div class="buttonHolder">


                            <asp:Button ID="btnEliminar" runat="server" CssClass="button button-title" Text="Eliminar" BorderStyle="None" OnClick="btnEliminar_Click" />

                            
                        </div>
                    </div>
                </div>
            </div>

            </div>

        </div>

        <footer class="footer-activarCurso item">
            CONTACTO
        </footer>
    </div>
</asp:Content>

