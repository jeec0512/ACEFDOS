<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="reporteSicoPracticos.aspx.cs"
    Inherits="Escuela_reporteSicoPracticos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
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
            <legend>Socios facturados</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq" Visible="true"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq" Visible="true"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnSocTotal" runat="server" CssClass="btnProceso" Text="PsicoPrácticos de ANETA" Visible="true"
                    OnClick="btnSocTotal_Click" />
                <asp:Button ID="btnSocxSuc" runat="server" CssClass="btnProceso" Text="PsicoPrácticos por sucursal" Visible="TRUE"
                    OnClick="btnSocxSuc_Click" />
            </asp:Panel>
        </fieldset>

    </asp:Panel>
    <asp:Panel ID="pnTotalSocios" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend></legend>


            <asp:Button ID="btnExcelSS" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true"
                OnClick="btnExcelSS_Click" />

            <asp:GridView ID="grvTotalSocios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                OnRowDataBound="grvTotalSocios_RowDataBound" ShowFooter="True">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="sucursal" HeaderText="Código" />
                    <asp:BoundField DataField="nom_suc" HeaderText="Sucursal" FooterStyle-HorizontalAlign="Right" FooterStyle-ForeColor=Red
                        FooterStyle-Font-Bold="true" />
                    <asp:BoundField DataField="aprobados" HeaderText="Aprobados" FooterStyle-HorizontalAlign="Center" FooterStyle-ForeColor=Red
                        FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign=Center />
                    <asp:BoundField DataField="reprobados" HeaderText="Reprobados" FooterStyle-HorizontalAlign="Center" FooterStyle-ForeColor=Red
                        FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign=Center />
                    <asp:BoundField DataField="enespera" HeaderText="En espera" FooterStyle-HorizontalAlign="Center" FooterStyle-ForeColor=Red
                        FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign=Center />
                    <asp:BoundField DataField="" HeaderText="Total por sucursal" FooterStyle-HorizontalAlign="Left" FooterStyle-ForeColor=Red
                        FooterStyle-BorderStyle=Double FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign=Center />

                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Blue" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </fieldset>
    </asp:Panel>



    <asp:Panel ID="pnSocios" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend></legend>

            <asp:Button ID="btnExcelSA" runat="server" CssClass="btnLargoForm " Text="A Excel detalle" Visible="true"
                OnClick="btnExcelSA_Click" />

            <asp:GridView ID="grvSocios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="sucursal" HeaderText="Código" />
                    <asp:BoundField DataField="nom_suc" HeaderText="Sucursal" />
                    <asp:BoundField DataField="cedula" HeaderText="Cédula" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                    <asp:BoundField DataField="factura" HeaderText="Factura" />
                    <asp:BoundField DataField="notaPractico" HeaderText="Nota" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                    <asp:BoundField DataField="instructorEvaluador" HeaderText="Instructor evaluador" />
                    <asp:BoundField DataField="elaborado" HeaderText="Elaborado por" />
                    <asp:BoundField DataField="observacion" HeaderText="Observación" />


                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="False" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Blue" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>

        </fieldset>
    </asp:Panel>
</asp:Content>

