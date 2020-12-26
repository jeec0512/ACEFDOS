<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="disponibilidadAutos.aspx.cs" Inherits="Escuela_disponibilidadAutos" EnableEventValidation="false" %>

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
            <legend>Control de cajas</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                 <asp:Label ID="Label1" runat="server" Text="Fecha Inicio" CssClass="lblPeq"></asp:Label>
                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
                 <asp:Label ID="Label2" runat="server" Text="Fecha fin" CssClass="lblPeq"></asp:Label>
                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
               
                <asp:Label ID="lblHoraInicio" CssClass="lblPeq" runat="server" Text="Entre las "></asp:Label>
                <asp:TextBox ID="txtHoraInicio" CssClass="txtPeq" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="mskSuperPhone" runat="server"
                    TargetControlID="txtHoraInicio"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="99:99"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>

                <asp:Label ID="lblHoraFin" CssClass="lblPeq" runat="server" Text="y"></asp:Label>
                <asp:TextBox ID="txtHoraFin" CssClass="txtPeq" runat="server"></asp:TextBox>
                <act1:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                    TargetControlID="txtHoraFin"
                    ClearMaskOnLostFocus="false"
                    MaskType="None"
                    Mask="99:99"
                    MessageValidatorTip="true"
                    InputDirection="LeftToRight"
                    ErrorTooltipEnabled="True"></act1:MaskedEditExtender>
                 <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar" OnClick="btnConsultar_Click"/>
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnDisponibles" CssClass="" runat="server" Visible="true">

        <fieldset id="fdpnDisponibles" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvpnDisponibles" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="100" >
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" Visible="false" />
                    <asp:BoundField DataField="SUCURSAL" HeaderText="SUCURSAL" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="ESC_DESCRIPCION" HeaderText="DESCRIPCION" Visible="true" />
                   


                    <asp:BoundField DataField="ESC_DIRECCION" HeaderText="DIRECCION" Visible="true" />
                    <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="NOMENCLATURA" Visible="true" />
                    
                    <asp:BoundField DataField="NUMAUTO" HeaderText="AUTO" Visible="true" />
                    <asp:BoundField DataField="HOR_INICIO" HeaderText="HORA INICIO" Visible="true" />
                    <asp:BoundField DataField="HOR_FIN" HeaderText="HORA FIN" Visible="true" />

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

