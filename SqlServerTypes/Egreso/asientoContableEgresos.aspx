<%@ Page Title="" Language="C#" MasterPageFile="~/Egreso/mpEgreso.master" AutoEventWireup="true" CodeFile="asientoContableEgresos.aspx.cs"
    Inherits="Egreso_asientoContableEgresos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">

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
            <legend>Asiento contable egresos</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:Panel ID="pnCaja" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCaja" runat="server">
                        <asp:ListItem Value="S">S</asp:ListItem>
                        <asp:ListItem Value="K">K</asp:ListItem>
                        <asp:ListItem Value="P">P</asp:ListItem>
                        <asp:ListItem Value="X">X</asp:ListItem>
                        <asp:ListItem Value="Y">Y</asp:ListItem>
                        <asp:ListItem Value="Z">Z</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

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



                <asp:Button ID="btnAsiento" runat="server" CssClass="btnProceso" Text="Asiento contable"
                    OnClick="btnAsiento_Click" />
                <!-- <asp:Button ID="btnAutoconsumos" runat="server" CssClass="btnProceso" Text="Autoconsumos" />
                <asp:Button ID="btnRetenciones" runat="server" CssClass="btnProceso" Text="Retenciones"/>!-->




            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <asp:Panel ID="Panel1" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend></legend>

            <asp:Button ID="btnExcelFe" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />

            <asp:GridView ID="grvContabilizacion" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("cod_cta") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("nom_cta") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DEBE" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("deb_mov","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HABER">
                        <ItemTemplate>
                            <%# Eval("cre_mov","{0:#,##0.00}".ToString()) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Referencia">
                        <ItemTemplate>
                            <%# Eval("doc_ref") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción">
                        <ItemTemplate>
                            <%# Eval("des_mov") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>

            <asp:Panel ID="pnCuadre" runat="server" CssClass="pnListarGridConsolidado" BackColor="#CCCCCC" ScrollBars="Vertical"
                Visible="true">
                <asp:GridView ID="grvCuadre" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                    HorizontalAlign="Center" Width="90%" AllowPaging="True" AllowSorting="True" PageSize="100">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="DEBE" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("deb_mov","{0:#,##0.00}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HABER">
                            <ItemTemplate>
                                <%# Eval("cre_mov","{0:#,##0.00}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>



                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </asp:Panel>
            <asp:Panel ID="pnExcel" CssClass="" runat="server" Wrap="False">
                
            </asp:Panel>
        </fieldset>
    </asp:Panel>
</asp:Content>

