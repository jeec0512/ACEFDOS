<%@ Page Title="" Language="C#" MasterPageFile="~/awmMembresias/mpAwmMembresia.master" AutoEventWireup="true"
     CodeFile="sociosActivos.aspx.cs" Inherits="awmMembresias_sociosActivos" EnableEventValidation="false" %>

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
            <legend>Socios facturados</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">

                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq" Visible="false"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy">
                </act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq" Visible="false"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
            <asp:Panel ID="Panel3" CssClass="pnAccionGrid" runat="server" Wrap="False">
                <asp:Button ID="btnSocTotal" runat="server" CssClass="btnProceso" Text="Socios activos de ANETA" Visible="true"
                    OnClick="btnSocTotal_Click" />
                <asp:Button ID="btnSocxSuc" runat="server" CssClass="btnProceso" Text="Socios por sucursal" visible="TRUE" OnClick="btnSocxSuc_Click" />
            </asp:Panel>
        </fieldset>

    </asp:Panel>
    <asp:Panel ID="pnTotalSocios" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Socios facturados</legend>
        </fieldset>

        <asp:Button ID="btnExcelSS" runat="server" CssClass="btnLargoForm " Text="A Excel" Visible="true"
            OnClick="btnExcelSS_Click" />

        <asp:GridView ID="grvTotalSocios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
            BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
            Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="Código">
                    <ItemTemplate>
                        <%# Eval("COD_SUC") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sucursal" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("nom_suc") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#Socios" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("NUMSOCIOS") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                
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
    </asp:Panel>
    <asp:Panel ID="pnSocios" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Socios facturados</legend>

            <asp:Button ID="btnExcelSA" runat="server" CssClass="btnLargoForm " Text="A Excel detalle" Visible="true"
                OnClick="btnExcelSA_Click" />

            <asp:GridView ID="grvSocios" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:TemplateField HeaderText="Código">
                    <ItemTemplate>
                        <%# Eval("cod_suc") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sucursal" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("nom_suc") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RUC/CC" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("CIRUC") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Apellidos" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("APELLIDOS") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombres" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("NOMBRES") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="E-mail" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("EMAIL_CONTAC") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Teléfono" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("TELEFONO_CONTAC") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Celular" ItemStyle-Wrap="true">
                    <ItemTemplate>
                        <%# Eval("CELULAR_CONTAC") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#Factura" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FACTURA") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#Contrato" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("NCONTRATO_MEMBR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("TIPO") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Membresía" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("TIPO_MEMBR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FechaAfil." ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FECHA_AFILIACION_MEMBR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FechaCaduc." ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FECHA_VENCIMIE_MEMBR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ejecutivo" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("VENDEDOR_MEMBR") %>
                    </ItemTemplate>
                </asp:TemplateField>
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

