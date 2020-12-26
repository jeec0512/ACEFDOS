<%@ Page Title="" Language="C#" MasterPageFile="~/awmMembresias/mpAwmMembresia.master" AutoEventWireup="true" 
    CodeFile="sociosInactivos.aspx.cs" Inherits="awmMembresias_sociosInactivos"  EnableEventValidation="false" %>

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
            <legend>Facturas por grupos</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
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
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server">
                <asp:Button ID="btnTodos" runat="server" CssClass="btnProceso" Text="Socios inactivos"
                    OnClick="btnTodos_Click" />
               
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    

    <asp:Panel ID="pnConsolidado" runat="server" CssClass="" Visible="true">
        <fieldset id="fsConsolidado" class="fieldset-principal">
            <legend>Socios inactivos</legend>

            <asp:Button ID="btnExcelC" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelC_Click" />

            <asp:GridView ID="grvConsolidado" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1"
                GridLines="Vertical" HorizontalAlign="Center" Width="90%"
                AllowPaging="True" AllowSorting="True" PageSize="50" 
                OnPageIndexChanged="grvConsolidado_PageIndexChanged" 
                OnPageIndexChanging="grvConsolidado_PageIndexChanging" 
                OnSelectedIndexChanging="grvConsolidado_SelectedIndexChanging" >
                <AlternatingRowStyle BackColor="#DCDCDC" />


               
                <Columns>
                    <asp:BoundField DataField="cli_ruc" HeaderText="Cédula" Visible="true" />
                   <asp:BoundField DataField="cli_nombres" HeaderText="Nombres" Visible="true" />
                    <asp:BoundField DataField="cli_email" HeaderText="E-MAIL" Visible="true" />
                    <asp:BoundField DataField="cli_direccion" HeaderText="DIRECCIÓN" Visible="true" />
                    <asp:BoundField DataField="cli_sector" HeaderText="SECTOR" Visible="true" />
                    <asp:BoundField DataField="cli_telefono" HeaderText="TELÉFONO" Visible="true" />
                    <asp:BoundField DataField="cli_celular" HeaderText="CELULAR" Visible="true" />
                    <asp:BoundField DataField="cli_tiposangre" HeaderText="TIPO DE SANGRE" Visible="true" />
                    <asp:BoundField DataField="cli_nacionalidad" HeaderText="NACIONALIDAD" Visible="true" />
                    <asp:BoundField DataField="cli_estadocivil" HeaderText="ESTADO CIVIL" Visible="true" />
                    <asp:BoundField DataField="cli_fechanacimiento" HeaderText="FECHA DE NACIMIENTO" Visible="true"  DataFormatString="{0:d}" />
                    <asp:BoundField DataField="cli_genero" HeaderText="GÉNERO" Visible="true" />
                     <asp:BoundField DataField="cli_obligado" HeaderText="LLEVA CONTABILIDAD" Visible="true" />
                    
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
        </fieldset>
    </asp:Panel>

</asp:Content>

