<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="envioEmailTitulos.aspx.cs" Inherits="Escuela_envioEmailTitulos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Impresión de títulos</legend>
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
                        <asp:ListItem Value="5">Recuperación de puntos regular</asp:ListItem>
                        <asp:ListItem Value="6">Recuperación de puntos fin de semana</asp:ListItem>
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
                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera" Visible="false">
                    <asp:Button ID="btnGuardar" runat="server" Text="Imprimir títulos" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />

                    <asp:Button ID="btnActa" runat="server" Text="Imprimir ACTA" CssClass="btnForm"
                        OnClick="btnActa_Click" />


                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"
                        Font-Size="Larger" Font-Bold="true"></asp:HyperLink>
                </asp:Panel>
            </fieldset>
        </asp:Panel>

    </asp:Panel>




    <asp:Panel ID="Panel1" runat="server" Style="width: 100%; height: auto">
        <fieldset id="Fieldset1">
            <legend>Notificación al estudiante</legend>


            
            <asp:Panel ID="pnAutoDetalle"  style="width:100% ;height:auto" runat="server" Visible="true" BorderStyle="Double">
                <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="100%"
                    AllowSorting="True" PageSize="50" OnRowCommand="grvCursoDetalle_RowCommand"
                    OnRowDataBound="grvCursoDetalle_RowDataBound" CssClass="Rojo" EnablePersistedSelection="False" OnRowEditing="grvCursoDetalle_RowEditing">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="RNOTC_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        <asp:BoundField  HeaderText="EJEMPLO"  Visible="TRUE"  ReadOnly="false"   />
                        <asp:BoundField DataField="REG_SUCURSAL" HeaderText="REG_SUCURSAL" Visible="false"  />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="CUR_NOMENCLATURA" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" HeaderText="CUR_FECHA_INICIO" Visible="false" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" HeaderText="CUR_FECHA_FIN" Visible="false" />
                        <asp:BoundField DataField="No" HeaderText="NO" Visible="true" />
                        <asp:BoundField DataField="ALUMNO" HeaderText="ALUMNO" Visible="true" />
                        <asp:BoundField DataField="REG_FACTURANUMERO" HeaderText="#FACTURA" Visible="true" />
                        <asp:BoundField DataField="MATRICULA" HeaderText="MATRICULA" Visible="false" />
                        <asp:BoundField DataField="PERMISO" HeaderText="PERMISO" Visible="true" />
                        <asp:BoundField DataField="alu_email" HeaderText="E-Mail" Visible="true" />
                       
                        <asp:BoundField DataField="TITULO" HeaderText="#TITULO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="OBSERVACIONES" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" HeaderText="RNOTC_PEDIDO_TITULOS" Visible="false" />
                        
                        <asp:ButtonField HeaderText="Enviado" Text="..." ButtonType="Image" 
                            ImageUrl="~/images/iconos/086.ico" CommandName="Confirmar" ItemStyle-Width="10px" Visible="false"  >
                            <ItemStyle Width="60px" />
                        </asp:ButtonField>

                        <asp:ButtonField HeaderText="Enviar" Text="..." ButtonType="Image" 
                            ImageUrl="~/images/iconos/email.png" CommandName="EnviaMail" ItemStyle-Width="10px" Visible="true" >
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

