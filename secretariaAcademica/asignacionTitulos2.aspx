<%@ Page Title="" Language="C#" MasterPageFile="~/secretariaAcademica/mpSecretariaAcademica.master" AutoEventWireup="true" CodeFile="asignacionTitulos2.aspx.cs" Inherits="secretariaAcademica_asignacionTitulos2"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <!--<link href="https://file.myfontastic.com/82NMqBMRcbALbXYak8fmp/icons.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,700,900" rel="stylesheet">-->
    <link href="../App_Themes/Estilos/grilla.css" rel="stylesheet" />
    <link rel="stylesheet" href="../App_Themes/estilos/fonts.css">

    <link rel="stylesheet" href="../App_Themes/estilos/icons.css">


    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos/actaEntregaTitulos.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="act1">
    </asp:ScriptManager>
    <asp:Label ID="lblMensaje" runat="server" Text="" Style="font-size: 1rem; color: red;"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" Visible="true">
        <asp:DropDownList ID="ddlMensaje" DataTextField="" DataValueField="" runat="server"
            AutoPostBack="True">
        </asp:DropDownList>
    </asp:Panel>
    <asp:Panel ID="pnActualizacion" runat="server" Style="display: flex; justify-content: space-between;">
        <asp:Panel ID="pnAsignacion" runat="server" Style="margin: 1rem;">

            <asp:TextBox ID="txtVeh_id" runat="server" Visible="false"></asp:TextBox>

            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal" Visible="true"></asp:Label>
            <asp:Panel ID="pnSucursal" runat="server" Visible="true">
                <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Label ID="lblModalidad" runat="server" Text="Modalidad"></asp:Label>
            <asp:Panel ID="pnModalidad" runat="server">
                <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Label ID="lblCurso" runat="server" Text="Curso" Visible="true"></asp:Label>
            <asp:Panel ID="pnCurso" runat="server" Visible="true">
                <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Label ID="lblPedido" runat="server" Text="Pedidos" Visible="true"></asp:Label>
            <asp:Panel ID="pnPedido" runat="server" Visible="true">
                <asp:DropDownList ID="ddlPedido" DataTextField="numpedido" DataValueField="numpedido" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlPedido_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnPedidos" runat="server" Style="margin: 1rem;">

            <asp:Label ID="lblTitulos" runat="server" Text="Titulos" Visible="true"></asp:Label>
            <asp:Panel ID="pnTitulos" runat="server" Visible="true">
                <asp:DropDownList ID="ddlTitulos" DataTextField="descripcion" DataValueField="tit_id" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlTitulos_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Label ID="lblCtrlTit" runat="server" Text="Series asignadas" Visible="true"></asp:Label>
            <asp:Panel ID="pnCtrlTit" runat="server" Visible="true">
                <asp:DropDownList ID="ddlCtrlTit" DataTextField="descripcion" DataValueField="ctrl_id" runat="server"
                    AutoPostBack="True">
                </asp:DropDownList>
            </asp:Panel>

            <asp:Panel ID="Panel3" runat="server">
                <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha"
                    Text="Fecha de asignación:" Font-Size="Small"></asp:Label>

                <asp:TextBox ID="txtFecha" runat="server" placeholder="Fecha de asignación"></asp:TextBox>
                <act1:CalendarExtender ID="calFecha" PopupButtonID="" runat="server" TargetControlID="txtFecha"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" BehaviorID="mee1" TargetControlID="txtFecha"
                    Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server" Visible="true">
                <asp:Label ID="lblActa" runat="server" Text="# Acta" Visible="true"></asp:Label>
                <asp:TextBox ID="txtActa" runat="server" Visible="true" Enabled="true"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="Panel5" runat="server">
                <asp:Label ID="lblDesde" runat="server" Text="Desde" Visible="true"></asp:Label>
                <asp:TextBox ID="txtDesde" runat="server" Visible="true" Enabled="false"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="Panel6" runat="server">
                <asp:Label ID="lblHasta" runat="server" Text="Hasta" Visible="true"></asp:Label>
                <asp:TextBox ID="txtHasta" runat="server" Visible="true" Enabled="false"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnAlterno" runat="server">
                <asp:Label ID="lblAlterno" runat="server" Text="Alterno" Visible="true"></asp:Label>
                <asp:TextBox ID="txtAlterno" runat="server" Visible="true" Enabled="false"></asp:TextBox>
            </asp:Panel>
        </asp:Panel>

        <asp:Panel ID="pnBotonera" runat="server" Style="display: flex; flex-direction: column;">
            <asp:Button ID="btnGuardar" runat="server" Text="Asignar número de títulos" CssClass="btnForm"
                OnClick="btnGuardar_Click" />
            <asp:Button ID="btnActa" runat="server" Text="Imprimir acta" Visible="true" CssClass="btnForm" OnClick="btnActa_Click" />
            <asp:Button ID="btnTitulo" runat="server" Text="Imprimir listado de títulos" CssClass="btnForm" OnClick="btnTitulo_Click" />
            <asp:Button ID="btnReasignar" runat="server" Text="Reasignar número de títulos a todo el pedido" CssClass="btnForm" OnClick="btnReasignar_Click" />
            <asp:HyperLink ID="hlRegresar" runat="server" Text="Regresar" NavigateUrl="~/secretariaAcademica/inicioSecretariaAcademica.aspx" CssClass="btnForm" Style="padding: 3px; border: solid 1px black; color: blue;"></asp:HyperLink>
        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="pnPedidoTitulos" runat="server">
        <fieldset id="Fieldset1">
            <legend>Listado de alumnos</legend>


            <asp:Panel ID="pnAutoDetalle" CssClass="" runat="server" Visible="true" BorderStyle="Double">

                <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" CssClass="grilla" OnRowCommand="grvCursoDetalle_RowCommand"
                    OnRowDataBound="grvCursoDetalle_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="RNOTC_id" HeaderText="Código" Visible="true"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REG_SUCURSAL" HeaderText="REG_SUCURSAL" Visible="false" />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="CUR_NOMENCLATURA" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" HeaderText="CUR_FECHA_INICIO" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" HeaderText="CUR_FECHA_FIN" Visible="false" />
                        <asp:BoundField DataField="NO" HeaderText="NO" Visible="true" />
                        <asp:BoundField DataField="ALUMNO" HeaderText="Nombres_del_Alumno" Visible="true" />
                        <asp:BoundField DataField="REG_FACTURANUMERO" HeaderText="FACTURANUMERO" Visible="true" />
                        <asp:BoundField DataField="MATRICULA" HeaderText="MATRICULA" Visible="false" />
                        <asp:BoundField DataField="PERMISO" HeaderText="PERMISO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" HeaderText="EDUCVIAL" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" HeaderText="EV_S1" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" HeaderText="EV_S2" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" HeaderText="EV_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_MEC_ASIS" HeaderText="MC_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PAUX_ASIS" HeaderText="PA_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PSIC_ASIS" HeaderText="PS_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_NOTA" HeaderText="PRACT" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP1" HeaderText="PR_S1" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP2" HeaderText="PR_S2" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_ASIS" HeaderText="PR_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_APROBADO" HeaderText="APROBADO" Visible="true" />
                        <asp:BoundField DataField="TITULO" HeaderText="TITULO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="OBSERVACIONES" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" HeaderText="PED_TIT" Visible="true" />
                    </Columns>

                </asp:GridView>

            </asp:Panel>

        </fieldset>



    </asp:Panel>



    <asp:Panel ID="pnValidarTit" runat="server" Visible="false">
        <fieldset id="Fieldset2">
            <legend>Listado de alumnos por validar</legend>


            <asp:Panel ID="Panel2" CssClass="" runat="server" Visible="true" BorderStyle="Double">

                <asp:GridView ID="grvVerificar" runat="server" AutoGenerateColumns="False" CssClass="grilla" OnRowDataBound="grvVerificar_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NO" HeaderText="N°" Visible="true" />
                        <asp:BoundField DataField="reg_facturanumero" HeaderText="# Factura" Visible="true" />
                        <asp:BoundField DataField="reg_numpermiso" HeaderText="# Permiso " Visible="true" />
                        <asp:BoundField DataField="petri_id" HeaderText="IdPetri " Visible="true" />
                        <asp:BoundField DataField="reg_petriresultado" HeaderText="PetriResultado " Visible="true" />
                        <asp:BoundField DataField="rnotc_licencia" HeaderText="Licencia " Visible="true" />
                        <asp:BoundField DataField="rnotc_educ_vial_asis" HeaderText="A_EducVial " Visible="true" />
                        <asp:BoundField DataField="rnotc_educ_vial_nota" HeaderText="N_EducVial " Visible="true" />
                        <asp:BoundField DataField="rnotc_educ_vial_sup1" HeaderText="S1_EducVial " Visible="true" />
                        <asp:BoundField DataField="rnotc_educ_vial_sup2" HeaderText="S2_EducVial" Visible="true" />
                        <asp:BoundField DataField="rnotc_prac_asis" HeaderText="A_Prac" Visible="true" />
                        <asp:BoundField DataField="rnotc_prac_nota" HeaderText="N_Prac" Visible="true" />
                        <asp:BoundField DataField="rnotc_prac_sup1" HeaderText="S1_Prac" Visible="true" />
                        <asp:BoundField DataField="rnotc_prac_sup2" HeaderText="S2_Prac" Visible="true" />
                        <asp:BoundField DataField="rnotc_psic_asis" HeaderText="A_Psic" Visible="true" />
                        <asp:BoundField DataField="rnotc_paux_asis" HeaderText="A_Paux" Visible="true" />
                        <asp:BoundField DataField="rnotc_mec_asis" HeaderText="A_Mec" Visible="true" />
                        <asp:BoundField DataField="rnotc_pedido_titulos" HeaderText="Pedido" Visible="true" />
                        <asp:BoundField DataField="Alumno" HeaderText="Alumno" Visible="true" />

                    </Columns>

                </asp:GridView>

            </asp:Panel>

        </fieldset>
    </asp:Panel>





    <script src="../js/jQuery%20v3.4.0.js"></script>
    <script src="../js/jspdf.debug.js"></script>
    <script src="../js/funciones.js"></script>
    <script>
        function DescargarPDF(ContenidoID, nombre) {
            var pdf = new jsPDF('p', 'pt', 'letter');
            html = $('#' + ContenidoID).html();
            specialElementHandlers = {};
            margins = { top: 10, bottom: 20, left: 20, width: 522 };
            margin_top = 4;
            margin_bottom = 2;
            height = 8;



            pdf.fromHTML(html, margins.left, margins.top, {
                'width': margins.width

            }, function (dispose) { pdf.save(nombre + '.pdf'); }, margins);
        }

    </script>
</asp:Content>

