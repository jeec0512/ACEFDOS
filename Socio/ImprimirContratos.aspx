<%@ Page Title="" Language="C#" MasterPageFile="~/Socio/mpSocio.master" AutoEventWireup="true" CodeFile="ImprimirContratos.aspx.cs"
    Inherits="Socio_ImprimirContratos" EnableEventValidation="false" %>

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
            <legend>Registro de envíos </legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="false">

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
            <asp:Panel ID="Panel3" CssClass="pnPeqSoc" runat="server" Wrap="False">
                <asp:Button ID="btnTarjetas" runat="server" CssClass="btnProceso"
                    Text="Registrar tarjetas enviadas" OnClick="btnTarjetas_Click" />
                <asp:Button ID="btnEnvios" runat="server" CssClass="btnProceso"
                    Text="Registra envíos" Visible="true" OnClick="btnEnvios_Click" />
            </asp:Panel>

            <asp:Panel ID="Panel1" CssClass="pnPeqSoc" runat="server" Wrap="False">
                <asp:Button ID="btnTarEnv" runat="server" CssClass="btnProceso"
                    Text="Ver tarjetas enviadas" Visible="true" OnClick="btnTarEnv_Click" />
                <asp:Button ID="btnEnvio" runat="server" CssClass="btnProceso"
                    Text="Ver envíos realizados" Visible="true" OnClick="btnEnvio_Click" />
            </asp:Panel>

        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnTarjeta" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Registro de envíos </legend>
            <asp:GridView ID="grvTarjeta" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True"
                PageSize="100" AutoGenerateSelectButton="True"
                OnSelectedIndexChanged="grvTarjeta_SelectedIndexChanged" DataKeyNames="id_tarjetaSocio">
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
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnGuia" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Registro de envíos </legend>
            <asp:GridView ID="grvGuia" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True"
                PageSize="100" AutoGenerateSelectButton="True"
                OnSelectedIndexChanged="grvGuia_SelectedIndexChanged" DataKeyNames="id_guiaRemision">
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
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnTarEnvio" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset3" class="fieldset-principal">
            <legend>Registro de envíos </legend>

            <asp:Button ID="btnExcelTar" runat="server" CssClass="btnLargoForm "
                Text="A Excel" Visible="true" OnClick="btnExcelTar_Click" />

            <asp:GridView ID="grvTarEnvio" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True"
                PageSize="100" AutoGenerateSelectButton="True">
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
                    <asp:TemplateField HeaderText="fecha impresion">
                        <ItemTemplate>
                            <%# Eval("fechaImpresion", "{0:d}".ToString())%>
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
        </fieldset>

    </asp:Panel>


    <asp:Panel ID="pnEnvio" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset4" class="fieldset-principal">
            <legend>Registro de envíos </legend>


            <asp:Button ID="btnExceGui" runat="server" CssClass="btnLargoForm "
                Text="A Excel" Visible="true" OnClick="btnExceGui_Click" />

            <asp:GridView ID="grvEnvio" runat="server" AutoGenerateColumns="False" BackColor="White"
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
                    <asp:TemplateField HeaderText="Fecha de envio" Visible="TRUE">
                        <ItemTemplate>
                            <%# Eval("fechaEnvio")%>
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
        </fieldset>
    </asp:Panel>

</asp:Content>

