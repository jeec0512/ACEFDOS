<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="imprimirPedidoTitulos.aspx.cs" Inherits="Escuela_imprimirPedidoTitulos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>

        <fieldset id="fsAsignacion">
            <legend>Asignación de títulos</legend>
            <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
            <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

            <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged2">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
            <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                <asp:DropDownList ID="ddlModalidad" DataTextField="nom_suc" DataValueField="mod_id" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged1">
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

            <asp:Label ID="lblPedido" CssClass="lblForm" runat="server" Text="Pedidos" Visible="true"></asp:Label>
            <asp:Panel ID="pnPedido" runat="server" CssClass="pnFormDdl" Visible="true">
                <asp:DropDownList ID="ddlPedido" DataTextField="numpedido" DataValueField="numpedido" runat="server"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlPedido_SelectedIndexChanged">
                </asp:DropDownList>
            </asp:Panel>
            <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                <asp:Button ID="btnGuardar" runat="server" Text="Imprimir pedido de títulos (anexo 4)" CssClass="btnForm"
                    OnClick="btnGuardar_Click" />
                <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"
                    Font-Size="Larger" Font-Bold="true"></asp:HyperLink>
            </asp:Panel>
        </fieldset>
    </asp:Panel>





    <asp:Panel ID="Panel1" runat="server">
        <fieldset id="Fieldset1">
            <legend>Listado de alumnos para el pedido de títulos</legend>


            <asp:Panel ID="pnAutoDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">
                <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" OnRowCommand="grvCursoDetalle_RowCommand"
                    OnRowDataBound="grvCursoDetalle_RowDataBound" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="RNOTC_id" Visible="false"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO" Visible="true" HeaderText="No" />
                        <asp:BoundField DataField="ALUMNO" Visible="true" HeaderText="Apellidos y Nombres" />
                        <asp:BoundField DataField="RNOTC_CIRUC" Visible="true" HeaderText="Cédula" />
                        <asp:BoundField DataField="REG_FACTURANUMERO" Visible="true" HeaderText="No Factura" />

                        <asp:BoundField DataField="MATRICULA" Visible="true" HeaderText="Matrícula" />
                        <asp:BoundField DataField="PERMISO" Visible="true" HeaderText="Permiso" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" Visible="true" HeaderText="EduccionVíal Not1" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" Visible="true" HeaderText="Sup1" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" Visible="true" HeaderText="Sup2" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" Visible="true" HeaderText="Asis" />
                        <asp:BoundField DataField="MEC_NOTA" Visible="true" HeaderText="Mecánica Not1" />
                        <asp:BoundField DataField="MEC_SUP1" Visible="true" HeaderText="Sup1" />
                        <asp:BoundField DataField="MEC_SUP2" Visible="true" HeaderText="Sup2" />
                        <asp:BoundField DataField="RNOTC_MEC_ASIS" Visible="true" HeaderText="Asis" />
                        <asp:BoundField DataField="PAUX_NOTA" Visible="true" HeaderText="PrimAuxilios Not1" />
                        <asp:BoundField DataField="PAUX_SUP1" Visible="true" HeaderText="Sup1" />
                        <asp:BoundField DataField="PAUX_SUP2" Visible="true" HeaderText="Sup2" />
                        <asp:BoundField DataField="RNOTC_PAUX_ASIS" Visible="true" HeaderText="Asis" />
                        <asp:BoundField DataField="PSIC_NOTA" Visible="true" HeaderText="Psicología Not1" />
                        <asp:BoundField DataField="PSIC_SUP1" Visible="true" HeaderText="Sup1" />
                        <asp:BoundField DataField="PSIC_SUP2" Visible="true" HeaderText="Sup2" />
                        <asp:BoundField DataField="RNOTC_PSIC_ASIS" Visible="true" HeaderText="Asis" />
                        <asp:BoundField DataField="RNOTC_PRAC_NOTA" Visible="true" HeaderText="Práctica Not1" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP1" Visible="true" HeaderText="Sup1" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP2" Visible="true" HeaderText="Sup2" />
                        <asp:BoundField DataField="RNOTC_PRAC_ASIS" Visible="true" HeaderText="Asis" />
                        <asp:BoundField DataField="RNOTC_APROBADO" Visible="true" HeaderText="Aprobado" />
                        <asp:BoundField DataField="TITULO" Visible="true" HeaderText="No Título" />
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" Visible="true" HeaderText="Observ." />
                        <asp:BoundField DataField="REG_SUCURSAL" Visible="false" HeaderText="No" />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" Visible="false" HeaderText="No" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" Visible="false" HeaderText="No" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" Visible="false" HeaderText="No" />
                        <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" Visible="false" HeaderText="No" />
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

