<%@ Page Title="" Language="C#" MasterPageFile="~/Socio/mpSocio.master" AutoEventWireup="true" CodeFile="listadosContratos.aspx.cs"
    Inherits="Socio_listadosContratos" EnableEventValidation="false" %>

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
            <legend>Listados </legend>
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
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnMembresias" runat="server" CssClass="btnProceso" Text="Membresias ingresadas"
                    OnClick="btnMembresias_Click" />
                <asp:Button ID="btnTarjetas" runat="server" CssClass="btnProceso" Text="Tarjetas no emitidas"
                    Visible="true" OnClick="btnTarjetas_Click" />
                <asp:Button ID="btnGuias" runat="server" CssClass="btnProceso" Text="Guías no impresas"
                    Visible="true" OnClick="btnGuias_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnMembresias" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Membresías </legend>

            <asp:Button ID="btnExcelMem" runat="server" CssClass="btnLargoForm " Text="A Excel"
                Visible="true" OnClick="btnExcelMem_Click" />

            <asp:GridView ID="grvMembresias" runat="server" AutoGenerateColumns="False"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True"
                PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código" Visible="false">
                        <ItemTemplate>
                            <%# Eval("id_nova") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Identidad">
                        <ItemTemplate>
                            <%# Eval("ciruc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombres">
                        <ItemTemplate>
                            <%# Eval("nombres") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Apellidos">
                        <ItemTemplate>
                            <%# Eval("apellidos") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No contrato">
                        <ItemTemplate>
                            <%# Eval("ncontrato_membr") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <%# Eval("tipo_membr") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendedor">
                        <ItemTemplate>
                            <%# Eval("vendedor_membr") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inicio">
                        <ItemTemplate>
                            <%# Eval("fecha_afiliacion_membr", "{0:d}".ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vence">
                        <ItemTemplate>
                            <%# Eval("fecha_vencimie_membr", "{0:d}".ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No factura">
                        <ItemTemplate>
                            <%# Eval("factura")%>
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

    <asp:Panel ID="pnTarjeta" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Tarjetas </legend>

            <asp:Button ID="btnExcelTar" runat="server" CssClass="btnLargoForm " Text="A Excel"
                Visible="true" OnClick="btnExcelTar_Click" />

            <asp:GridView ID="grvTarjeta" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True"
                PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código" Visible="false">
                        <ItemTemplate>
                            <%# Eval("id_tarjetaSocio") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Identidad" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("ciruc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No Contrato" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("ncontrato_membr") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <%# Eval("tipo_membr") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombres">
                        <ItemTemplate>
                            <%# Eval("nombres") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Inicio">
                        <ItemTemplate>
                            <%# Eval("fecha_afiliacion_membr", "{0:d}".ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vence">
                        <ItemTemplate>
                            <%# Eval("fecha_vencimie_membr", "{0:d}".ToString())%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# años">
                        <ItemTemplate>
                            <%# Eval("numero")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <%# Eval("envio_corresponden")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("cod_suc")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vendedor">
                        <ItemTemplate>
                            <%# Eval("vendedor_membr")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio">
                        <ItemTemplate>
                            <%# Eval("valor_con_iva")%>
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

    <asp:Panel ID="pnGuia" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset3" class="fieldset-principal">
            <legend>Tarjetas </legend>

            <asp:Button ID="btnExceGui" runat="server" CssClass="btnLargoForm " Text="A Excel"
                Visible="true" OnClick="btnExceGui_Click" />

            <asp:GridView ID="grvGuia" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True"
                PageSize="100">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código" Visible="false">
                        <ItemTemplate>
                            <%# Eval("id_guiaRemision") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Identidad" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("ciruc") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombres" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("nombres") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ciudad" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("ciudad") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sector" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("sector") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dirección" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("direccion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teléfonos" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("telefono") %>
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

