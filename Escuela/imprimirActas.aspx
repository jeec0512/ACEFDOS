<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="imprimirActas.aspx.cs" Inherits="Escuela_imprimirActas" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
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

                    <asp:Panel ID="pnPedido" runat="server" CssClass="mainSelect" Visible="true">
                        <asp:DropDownList ID="ddlPedido" DataTextField="numpedido" DataValueField="numpedido" runat="server"
                            AutoPostBack="True" CssClass="mainSelect__item" OnSelectedIndexChanged="ddlPedido_SelectedIndexChanged">
                        </asp:DropDownList>
                    </asp:Panel>
                </div>
            </div>

            <div class="areaHorarios main-principalActivarCurso__horariosDisponibles subitem">
                <asp:Panel ID="pnActualizacion" runat="server" Visible="true" Style="margin-top: 1rem;">
                    <asp:Label ID="Label2" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
                    <asp:Panel ID="pnAuto" runat="server">
                        <fieldset id="fsAuto">
                            <legend>Datos generales</legend>
                            <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                             <asp:Panel ID="pnProvincia" runat="server" CssClass="mainSelect" Visible="true">
                                <asp:DropDownList ID="ddlProvincia" DataTextField="provincia" DataValueField="provincia" runat="server"
                                    AutoPostBack="True" CssClass="mainSelect__item" >
                                </asp:DropDownList>
                            </asp:Panel>
                             <asp:Panel ID="pnCiudad" runat="server" CssClass="mainSelect" Visible="true">
                                <asp:DropDownList ID="ddlCiudad" DataTextField="ciudad" DataValueField="ciudad" runat="server"
                                    AutoPostBack="True" CssClass="mainSelect__item" >
                                </asp:DropDownList>
                            </asp:Panel>
                            <asp:Label ID="lblFecha" CssClass="lblForm" runat="server" Text="Fecha"></asp:Label>
                            <asp:TextBox ID="txtFecha" CssClass="txtForm" runat="server"></asp:TextBox>
                             <act1:CalendarExtender ID="calFechaPsico" PopupButtonID="" runat="server" TargetControlID="txtFecha"
                        Format="dd/MM/yyyy"></act1:CalendarExtender>
                    <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" BehaviorID="mee1" TargetControlID="txtFecha"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                        AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="True" />

                            <asp:Label ID="lblDirector" CssClass="lblForm" runat="server" Text="Director de escuela"></asp:Label>
                            <asp:TextBox ID="txtDirector" CssClass="txtForm" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSupTeoria" CssClass="lblForm" runat="server" Text="Supervisor cursos de teoría"></asp:Label>
                            <asp:TextBox ID="txtSupTeoria" CssClass="txtForm" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSupPractica" CssClass="lblForm" runat="server" Text="Supervisor de clase de práctica"></asp:Label>
                            <asp:TextBox ID="txtSupPractica" CssClass="txtForm" runat="server"></asp:TextBox>
                            <asp:Label ID="lblSecAcad" CssClass="lblForm" runat="server" Text="Secretaria académica"></asp:Label>
                            <asp:TextBox ID="txtSecAcad" CssClass="txtForm" runat="server" Enabled="true"></asp:TextBox>
                            
                            <!--<asp:Label ID="lblSuc_Id" CssClass="lblForm" runat="server" Text="SucId"></asp:Label>
                <asp:TextBox ID="txtSuc_Id" CssClass="txtForm" runat="server"></asp:TextBox>-->
                            <!--<asp:Label ID="lblTve_id" CssClass="lblForm" runat="server" Text="Tve_id"></asp:Label>
                <asp:TextBox ID="txtTve_id" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPer_id" CssClass="lblForm" runat="server" Text="Per_id"></asp:Label>
                <asp:TextBox ID="txtPer_id" CssClass="txtForm" runat="server"></asp:TextBox>
                            <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                            <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                                <asp:CheckBox ID="chkEstado" TextAlign="Left" runat="server" />
                            </asp:Panel>
                            <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                                <asp:Button ID="Button1" runat="server" Text="Grabar" CssClass="btnForm" />
                                <asp:Button ID="Button2" runat="server" Text="Cancelar" CssClass="btnForm" />
                                <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                            </asp:Panel>-->

                        </fieldset>
                    </asp:Panel>
                </asp:Panel>

            </div>


            <div class="areaBotones main-principalActivarCurso__botonera subitem">
                <div class="contieneBotones">
                    <h3 class="titulo3">Acciones</h3>
                    <div class="cajaBotones">
                        <div class="buttonHolder">
                            <asp:Button ID="btnGuardar" runat="server" CssClass="button button-title" Text="Imprimir actas" BorderStyle="None"  />
                            <asp:Button ID="btnCancelar" runat="server" CssClass="button button-title" Text="Cancelar" BorderStyle="None" Visible="false" />
                            <asp:Button ID="btnImprimir" runat="server" CssClass="button button-title" Text="Imprimir" BorderStyle="None" Visible="false" />
                            <asp:Button ID="btnRegresar" runat="server" CssClass="button button-title" Text="Regresar" BorderStyle="None" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="areaAsignados main-principalActivarCurso__horariosAsignados subitem" id="dvListado" runat="server">
                <h3 class="titulo3">Listado de estudiantes</h3>
                <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="50" ShowFooter="false" OnRowCommand="grvCursoDetalle_RowCommand" CssClass="grilla">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="RNOTC_CONFIRMACION" HeaderText="CONFIRMA" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone" />
                        <asp:BoundField DataField="RNOTC_id" HeaderText="Código" Visible="true"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>

                        <asp:ButtonField HeaderText="Confirmado" Text="..." ButtonType="Image"
                            ImageUrl="~/images/iconos/086.ico" CommandName="Confirmar" ItemStyle-Width="10px" Visible="false">
                            <ItemStyle Width="60px" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="REG_SUCURSAL" HeaderText="REG_SUCURSAL" Visible="false" />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="CUR_NOMENCLATURA" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" HeaderText="CUR_FECHA_INICIO" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" HeaderText="CUR_FECHA_FIN" Visible="false" />
                        <asp:BoundField DataField="NO" HeaderText="NO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_CIRUC" Visible="true" HeaderText="Cédula" />
                        <asp:BoundField DataField="ALUMNO" HeaderText="ALUMNO" Visible="true" />

                        <asp:BoundField DataField="REG_FACTURANUMERO" HeaderText="#Factura" Visible="true" />
                        <asp:BoundField DataField="MATRICULA" HeaderText="MATRICULA" Visible="false" />
                        <asp:BoundField DataField="PERMISO" HeaderText="PERMISO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" HeaderText="NEDU" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" HeaderText="NEDUS1" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" HeaderText="NEDUS2" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" HeaderText="AEDU" Visible="true" />
                        <asp:BoundField DataField="RNOTC_MEC_ASIS" HeaderText="AMEC" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PAUX_ASIS" HeaderText="APAUX" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PSIC_ASIS" HeaderText="APSIC" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_NOTA" HeaderText="NPRAC" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP1" HeaderText="PRACS1" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP2" HeaderText="PRACS2" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_ASIS" HeaderText="APRAC" Visible="true" />
                        <asp:BoundField DataField="RNOTC_APROBADO" HeaderText="ESTADO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="OBSERVACIONES" Visible="false" />
                        <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" HeaderText="PEDIDO TITULOS" Visible="false" ReadOnly="false" />
                        <asp:BoundField DataField="TITULO" HeaderText="TITULO" Visible="false" />

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



            </div>

        </div>

        <footer class="footer-activarCurso item">
            CONTACTO
        </footer>
    </div>
</asp:Content>

