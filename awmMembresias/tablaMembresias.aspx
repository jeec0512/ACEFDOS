<%@ Page Title="" Language="C#" MasterPageFile="~/awmMembresias/mpAwmMembresia.master" AutoEventWireup="true" CodeFile="tablaMembresias.aspx.cs" Inherits="awmMembresias_tablaMembresias" 
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
        <asp:Label ID="lblTipoConsulta" runat="server" Text="" Visible="false"></asp:Label>
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq" Visible="false"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal"
                    Visible="false">
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnSocTotal" runat="server" CssClass="btnProceso" Text="Membresías por sucursal" Visible="true"
                    OnClick="btnSocTotal_Click" />
                <asp:Button ID="btnSocxSuc" runat="server" CssClass="btnProceso" Text="Membresías por vendedor" OnClick="btnSocxSuc_Click" />
            </asp:Panel>
        </fieldset>

    </asp:Panel>

    <asp:Panel ID="pnTotalSocios" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Tabla de membresías</legend>
        </fieldset>

        <asp:Button ID="btnExcelSS" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true"
            OnClick="btnExcelSS_Click" />

        <asp:GridView ID="grvTotalSocios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
            Width="95%" AllowPaging="false" AllowSorting="True" PageSize="60" 
            OnRowCommand="grvTotalSocios_RowCommand" OnRowDataBound="grvTotalSocios_RowDataBound" ShowFooter="True">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                
                <asp:BoundField DataField="COD_SUC" HeaderText="COD_SUC" Visible="TRUE" />
                 <asp:ButtonField HeaderText="Ver detalle" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/ohist.ico"
                        CommandName="verReg" ItemStyle-Width="60" >
                 <ItemStyle Width="60px" />
                 </asp:ButtonField>
                <asp:BoundField DataField="nom_suc" HeaderText="nom_suc" Visible="true" />
                <asp:BoundField DataField="tres3mesesN" HeaderText="tres3mesesN" Visible="true" />
                <asp:BoundField DataField="tres3mesesR" HeaderText="tres3mesesR" Visible="true" />
                <asp:BoundField DataField="estandarN" HeaderText="estandarN" Visible="true" />
                <asp:BoundField DataField="estandarR" HeaderText="estandarR" Visible="true" />
                <asp:BoundField DataField="motoN" HeaderText="motoN" Visible="true" />
                <asp:BoundField DataField="motoR" HeaderText="motoR" Visible="true" />
                <asp:BoundField DataField="premiumN" HeaderText="premiumN" Visible="true" />
                <asp:BoundField DataField="premiumR" HeaderText="premiumR" Visible="true" />
                <asp:BoundField DataField="premium2N" HeaderText="premium2N" Visible="true" />
                <asp:BoundField DataField="premium2R" HeaderText="premium2R" Visible="true" />
                <asp:BoundField DataField="premium3N" HeaderText="premium3N" Visible="true" />
                <asp:BoundField DataField="premium3R" HeaderText="premium3R" Visible="true" />
                <asp:BoundField DataField="proautopremiumN" HeaderText="proautopremiumN" Visible="true" />
                <asp:BoundField DataField="proautopremiumR" HeaderText="proautopremiumR" Visible="true" />
                <asp:BoundField DataField="taxiN" HeaderText="taxiN" Visible="true" />
                <asp:BoundField DataField="taxiR" HeaderText="taxiR" Visible="true" />
                <asp:BoundField DataField="otros" HeaderText="otros" Visible="true" />
                <asp:BoundField DataField="totalMembresias" HeaderText="totalMembresias" Visible="true" />
                

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

    <asp:Panel ID="pnSocios" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Detalle de membresías</legend>

            <asp:Button ID="btnExcelSA" runat="server" CssClass="btnLargoForm " Text="A Excel detalle" Visible="true"
                OnClick="btnExcelSA_Click" />
            
            <asp:Button ID="btnCancelarDetalle" CssClass="btnProceso" runat="server" Text="Regresar " 
                OnClick="btnCancelarDetalle_Click"/>

            <asp:GridView ID="grvSocios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="COD_SUC" HeaderText="COD_SUC" Visible="true" />
                    <asp:BoundField DataField="FACTURA" HeaderText="FACTURA" Visible="true" />
                    <asp:BoundField DataField="NCONTRATO_MEMBR" HeaderText="NCONTRATO_MEMBR" Visible="true" />
                     <asp:BoundField DataField="envio_corresponden" HeaderText="nuevo/renov" Visible="true" />
                    <asp:BoundField DataField="VENDEDOR_MEMBR" HeaderText="VENDEDOR_MEMBR" Visible="true" />
                    <asp:BoundField DataField="TIPO_MEMBR" HeaderText="TIPO_MEMBR" Visible="true" />
                    <asp:BoundField DataField="FECHA_AFILIACION_MEMBR" HeaderText="FECHA_AFILIACION_MEMBR" Visible="true" />
                    <asp:BoundField DataField="FECHA_VENCIMIE_MEMBR" HeaderText="FECHA_VENCIMIE_MEMBR" Visible="true" />
                    <asp:BoundField DataField="CIRUC" HeaderText="IDENTIFICACION" Visible="true" />
                    <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" Visible="true" />
                    <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" Visible="true" />
                   
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

        </fieldset>
    </asp:Panel>


    <asp:Panel ID="pnVendedorTotal" CssClass="" runat="server" Visible="true">

        <fieldset id="fsVendedorTotal" class="fieldset-principal">
            <legend>Tabla de membresías</legend>
        </fieldset>

        <asp:Button ID="btnExcelvendedorTotal" runat="server" CssClass="btnLargoForm " Text="A Excel" 
            Visible="true" OnClick="btnExcelvendedorTotal_Click"/>

        <asp:GridView ID="grvVendedorTotal" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
            Width="95%" AllowPaging="false" AllowSorting="True" PageSize="60" 
             ShowFooter="True" OnRowCommand="grvVendedorTotal_RowCommand" 
            OnRowDataBound="grvVendedorTotal_RowDataBound">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                
                <asp:BoundField DataField="vendedor_membr" HeaderText="Vendedor" Visible="false" />
                 <asp:ButtonField HeaderText="Ver detalle" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/ohist.ico"
                        CommandName="verReg" ItemStyle-Width="60" >
                 <ItemStyle Width="60px" />
                 </asp:ButtonField>
                <asp:BoundField DataField="vendedor_membr" HeaderText="Vendedor" Visible="true" />
                <asp:BoundField DataField="tres3mesesN" HeaderText="tres3mesesN" Visible="true" />
                <asp:BoundField DataField="tres3mesesR" HeaderText="tres3mesesR" Visible="true" />
                <asp:BoundField DataField="estandarN" HeaderText="estandarN" Visible="true" />
                <asp:BoundField DataField="estandarR" HeaderText="estandarR" Visible="true" />
                <asp:BoundField DataField="motoN" HeaderText="motoN" Visible="true" />
                <asp:BoundField DataField="motoR" HeaderText="motoR" Visible="true" />
                <asp:BoundField DataField="premiumN" HeaderText="premiumN" Visible="true" />
                <asp:BoundField DataField="premiumR" HeaderText="premiumR" Visible="true" />
                <asp:BoundField DataField="premium2N" HeaderText="premium2N" Visible="true" />
                <asp:BoundField DataField="premium2R" HeaderText="premium2R" Visible="true" />
                <asp:BoundField DataField="premium3N" HeaderText="premium3N" Visible="true" />
                <asp:BoundField DataField="premium3R" HeaderText="premium3R" Visible="true" />
                <asp:BoundField DataField="proautopremiumN" HeaderText="proautopremiumN" Visible="true" />
                <asp:BoundField DataField="proautopremiumR" HeaderText="proautopremiumR" Visible="true" />
                <asp:BoundField DataField="taxiN" HeaderText="taxiN" Visible="true" />
                <asp:BoundField DataField="taxiR" HeaderText="taxiR" Visible="true" />
                <asp:BoundField DataField="otros" HeaderText="otros" Visible="true" />
                <asp:BoundField DataField="totalMembresias" HeaderText="totalMembresias" Visible="true" />
                

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

    <asp:Panel ID="pnVendedor" CssClass="" runat="server" Visible="true">

        <fieldset id="fsVendedor" class="fieldset-principal">
            <legend>Detalle de vendedores</legend>

            <asp:Button ID="btnExcelVendedor" runat="server" CssClass="btnLargoForm " Text="A Excel detalle" 
                Visible="true" OnClick="btnExcelVendedor_Click"/>
            
            <asp:Button ID="btnregresarVendedor" CssClass="btnProceso" runat="server" Text="Regresar " 
                OnClick="btnregresarVendedor_Click"/>

            <asp:GridView ID="grvVendedor" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="COD_SUC" HeaderText="COD_SUC" Visible="true" />
                    <asp:BoundField DataField="FACTURA" HeaderText="FACTURA" Visible="true" />
                    <asp:BoundField DataField="NCONTRATO_MEMBR" HeaderText="NCONTRATO_MEMBR" Visible="true" />
                     <asp:BoundField DataField="envio_corresponden" HeaderText="nuevo/renov" Visible="true" />
                    
                    <asp:BoundField DataField="TIPO_MEMBR" HeaderText="TIPO_MEMBR" Visible="true" />
                    <asp:BoundField DataField="FECHA_AFILIACION_MEMBR" HeaderText="FECHA_AFILIACION_MEMBR" Visible="true" />
                    <asp:BoundField DataField="FECHA_VENCIMIE_MEMBR" HeaderText="FECHA_VENCIMIE_MEMBR" Visible="true" />
                    <asp:BoundField DataField="CIRUC" HeaderText="IDENTIFICACION" Visible="true" />
                    <asp:BoundField DataField="APELLIDOS" HeaderText="APELLIDOS" Visible="true" />
                    <asp:BoundField DataField="NOMBRES" HeaderText="NOMBRES" Visible="true" />
                    <asp:BoundField DataField="VENDEDOR_MEMBR" HeaderText="VENDEDOR_MEMBR" Visible="true" />
                   
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

        </fieldset>
    </asp:Panel>
</asp:Content>

