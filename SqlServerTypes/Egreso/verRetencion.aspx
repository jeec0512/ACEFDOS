<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="verRetencion.aspx.cs" Inherits="Egreso_verRetencion" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Retención</legend>
            <asp:Panel ID="pnCabeceraFactura" CssClass="pnAccionGrid" runat="server">
                <asp:Label ID="lblRuc" runat="server" Text="RUC" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaRuc" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblSecuencial" runat="server" Text="Secuencial" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaSecuencial" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Button ID="btnTraerRetencion" runat="server" Text="Traer retención"
                    OnClick="btnTraerRetencion_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="pnDetalleRetencion" CssClass="" runat="server" Visible="true">

        <fieldset id="fsDetalle" class="fieldset-principal">
            <legend>Detalle de la retención</legend>
            <asp:GridView ID="grvListadoRuc" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%" AllowPaging="True" AllowSorting="True"
                PageSize="20" OnRowCommand="grvListadoRuc_RowCommand" 
                OnRowDataBound="grvListadoRuc_RowDataBound">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Verificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
   
                    <asp:BoundField HeaderText="Mensaje SRI" DataField="cre_sri" Visible="true" />
                    <asp:BoundField HeaderText="CÓDIGO" DataField="codigo" Visible="true" />
                    <asp:BoundField HeaderText="Cod.Retención" DataField="codigoRetencion" Visible="true" />
                    <asp:BoundField HeaderText="Base imponible" DataField="baseImponible" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField HeaderText="Porcentaje" DataField="porcentajeRetener" Visible="true" />
                    <asp:BoundField HeaderText="Valor retenido" DataField="valorRetenido" Visible="true" DataFormatString="{0:N}" />
                    <asp:BoundField HeaderText="Doc.Sustento" DataField="codDocSustento" Visible="true" />
                    <asp:BoundField HeaderText="# Documento" DataField="numDocSustento" Visible="true" />
                    <asp:BoundField HeaderText="Fecha" DataField="fechaEmisionDocSustento" Visible="true" DataFormatString="{0:d}"/>
                     <asp:BoundField HeaderText="#Retención" DataField="retencion" Visible="true" />
                    
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
            <asp:Panel ID="pnOpcion" CssClass="pnAccion" runat="server">
                <asp:Button ID="btnEnviarRet" CssClass="btnForm" runat="server" Text="Enviar" OnClick="btnEnviarRet_Click" />
                <asp:Button ID="btnBorrarRet" CssClass="btnForm" runat="server" Text="Borrar" OnClick="btnBorrarRet_Click" />
                <asp:Button ID="btnAnularRet" CssClass="btnForm" runat="server" Text="Anular" OnClick="btnAnularRet_Click"
                    Visible="False" />
                <asp:Button ID="btnCancelarRet" CssClass="btnForm" runat="server" Text="Regresar" OnClick="btnCancelarRet_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>
</asp:Content>

