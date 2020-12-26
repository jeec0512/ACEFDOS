<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="pedidoTitulos.aspx.cs"
    Inherits="Escuela_pedidoTitulos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Pedido de títulos</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged2">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField=nom_suc DataValueField=mod_id runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged1">
                        <asp:ListItem Value=-1>Seleccione modalidad</asp:ListItem>
                        <asp:ListItem Value=1>15 días</asp:ListItem>
                        <asp:ListItem Value=2>7 días</asp:ListItem>
                        <asp:ListItem Value=3>Fines de semana</asp:ListItem>
                        <asp:ListItem Value=4>Curso corporativo</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField=cur_nomeNclatura DataValueField=cur_id runat="server"
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
                    <asp:Button ID=btnGuardar runat="server" Text="Crear pedido" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir pedido de títulos (anexo 4)" CssClass="btnForm"
                    OnClick="btnImprimir_Click" />
                   
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/Escuela/inicioEscuela.aspx" ForeColor="#0000ff" Font-Size="Larger" Font-Bold="true"></asp:HyperLink>    
                </asp:Panel>
                

            </fieldset>
        </asp:Panel>
    </asp:Panel>


    <asp:Panel ID="pnPedidos" runat="server">
        <fieldset id="fsPedidos">
            <legend>Listado de alumnos con pedido de títulos</legend>

            <asp:Button ID="btnExcelPe" runat="server" CssClass="btnLargoForm " Text="A Excel Alumnos con pedido" Visible="true" OnClick="btnExcelPe_Click"  />

            <asp:Panel ID="pnAutoDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">
                <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" OnRowCommand="grvCursoDetalle_RowCommand"
                    OnRowDataBound="grvCursoDetalle_RowDataBound" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="RNOTC_CONFIRMACION" HeaderText="CONFIRMA" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone"/>
                        <asp:BoundField DataField="RNOTC_id" HeaderText="Código" Visible="true"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        
                         <asp:ButtonField HeaderText="Confirmado" Text="..." ButtonType="Image" 
                            ImageUrl="~/images/iconos/086.ico" CommandName="Confirmar" ItemStyle-Width="10px" Visible="false"  >
                            <ItemStyle Width="60px" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="REG_SUCURSAL" HeaderText="REG_SUCURSAL" Visible="false" />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="CUR_NOMENCLATURA" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" HeaderText="CUR_FECHA_INICIO" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" HeaderText="CUR_FECHA_FIN" Visible="false" />
                        <asp:BoundField DataField="NO" HeaderText="NO" Visible="true" />
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
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="OBSERVACIONES" Visible="true" />
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

            </asp:Panel>





        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnPedidoTitulos" CssClass="" runat="server" Visible="true">
        <fieldset id="fsPedidoTitulos" class="fieldset-principal">
            <legend>Pedido de títulos</legend>
           
             <asp:Button ID="btnAlumnos" runat="server" CssClass="btnLargoForm " Text="A Excel Alumnos aprobados" Visible="true" OnClick="btnAlumnos_Click"  />
                <asp:GridView ID="grvPedidoTitulos" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="RNOTC_id"
                    ShowHeaderWhenEmpty="True"
                    OnRowCommand="grvPedidoTitulos_RowCommand"
                    OnRowEditing="grvPedidoTitulos_RowEditing"
                    OnRowCancelingEdit="grvPedidoTitulos_RowCancelingEdit"
                    OnRowUpdating="grvPedidoTitulos_RowUpdating"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnDataBound="grvPedidoTitulos_DataBound"
                     OnSelectedIndexChanged="grvPedidoTitulos_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />

                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                         <asp:BoundField DataField="RNOTC_id" HeaderText="Código" Visible="true"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REG_SUCURSAL" HeaderText="REG_SUCURSAL" Visible="false"  ReadOnly="True"  />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="CUR_NOMENCLATURA" Visible="false"  ReadOnly="True" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" HeaderText="CUR_FECHA_INICIO" Visible="false"  ReadOnly="True" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" HeaderText="CUR_FECHA_FIN" Visible="false"  ReadOnly="True" />
                        <asp:BoundField DataField="NO" HeaderText="NO" Visible="false"  ReadOnly="True" />
                        <asp:BoundField DataField="ALUMNO" HeaderText="ALUMNO" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="REG_FACTURANUMERO" HeaderText="#Factura" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="MATRICULA" HeaderText="MATRICULA" Visible="false"  ReadOnly="True" />
                        <asp:BoundField DataField="PERMISO" HeaderText="PERMISO" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" HeaderText="NEDU" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" HeaderText="NEDUS1" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" HeaderText="NEDUS2" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" HeaderText="AEDU" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_MEC_ASIS" HeaderText="AMEC" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PAUX_ASIS" HeaderText="APAUX" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PSIC_ASIS" HeaderText="APSIC" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PRAC_NOTA" HeaderText="NPRAC" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP1" HeaderText="PRACS1" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP2" HeaderText="PRACS2" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PRAC_ASIS" HeaderText="APRAC" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_APROBADO" HeaderText="ESTADO" Visible="true"  ReadOnly="True" />
                        <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" HeaderText="PEDIDO TITULOS" Visible="false"  ReadOnly="True"  />
                        <asp:BoundField DataField="TITULO" HeaderText="TITULO" Visible="false" />
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="OBSERVACIONES" Visible="true" />
                        <asp:BoundField DataField="RNOTC_CONFIRMACION" HeaderText=" NEGADOS" Visible="true" />
                       
                        
                    </Columns>
                </asp:GridView>


        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnActiva" runat="server" Visible="false">


        <asp:Panel ID="pnpnActiva" runat="server">
            <fieldset id="sucursal">
                <legend>Activar o desactivar al estudiante para el pedido</legend>
                <asp:Label ID="Label1" CssClass="lblForm" runat="server" Text="id"></asp:Label>
                <asp:TextBox ID="txtId" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                
                <asp:Label ID="Label2" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtEstudiante" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
                
                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnActivo" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID="chkActivo" TextAlign="Left" runat="server" />
                </asp:Panel>

                
                <asp:Label ID="lblObservacion" CssClass="lblForm" runat="server" Text="Observacion"></asp:Label>
                <asp:TextBox ID="txtObservacion" CssClass="txtForm" runat="server"></asp:TextBox>




                <asp:Panel ID="Panel1" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardaObservacion" runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardaObservacion_Click"
                        />
                    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btnForm" OnClick="btnRegresar_Click"  />

                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <script src="../js/funciones.js"></script>
</asp:Content>

