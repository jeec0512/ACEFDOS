<%@ Page Title="" Language="C#" MasterPageFile="~/Tributacion/mpTributacion.master" AutoEventWireup="true" CodeFile="documentoNova.aspx.cs" Inherits="Tributacion_documentoNova" EnableEventValidation="false" %>

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
            <legend>Cierre de egresos</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="peqDdl" DataTextField="descrip" DataValueField="tipodoc">
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



                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar"
                    OnClick="btnConsultar_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>


    <asp:Panel ID="pnDocumentos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDocumentos" class="fieldset-principal">
            <legend>Cajas</legend>

            <asp:Panel ID="pnExcel" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnExcelFe" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />
            </asp:Panel>

            <asp:GridView ID="grvDocumentoCabecera" runat="server" AutoGenerateColumns="False" BackColor="blue"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="20%" AllowSorting="True" PageSize="25" DataKeyNames="numero" AutoGenerateSelectButton="True"
                Height="157px">

                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>

                    <asp:TemplateField HeaderText="Documento" >
                        <ItemTemplate>
                            <%# Eval("numero") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha" >
                        <ItemTemplate>
                            <%# Eval("fecha","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detalle" >
                        <ItemTemplate>
                            <%# Eval("detalle") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="#Factura"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("fac_prov") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="#Autorización"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("aut_sri") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="#Serie"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("no_ser") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="fec_caduc"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("fec_caduc","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="num_serie"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("num_serie") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="fec_ret"  ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("fec_ret","{0:d}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
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

