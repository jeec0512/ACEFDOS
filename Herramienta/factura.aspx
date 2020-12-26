<%@ Page Title="" Language="C#" MasterPageFile="~/Herramienta/mpHerramienta.master" AutoEventWireup="true" CodeFile="factura.aspx.cs" Inherits="Herramienta_factura"  EnableEventValidation="false" %>

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
            <legend>Listado de facturas</legend>
            <asp:Panel ID="pnCabeceraFactura" CssClass="pnAccionGrid" runat="server">
                <asp:Label ID="lblRuc" runat="server" Text="RUC" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaRuc" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>
                <asp:Label ID="lblSecuencial" runat="server" Text="Secuencial" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"></asp:Label>
                <asp:TextBox ID="txtBuscaSecuencial" runat="server" Font-Size="Larger" ForeColor="darkblue"></asp:TextBox>
                <asp:Button ID="btnListarRuc" runat="server" Text="Listar" CssClass="btnProceso" OnClick="btnListarRuc_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnListadoFactura" CssClass="" runat="server" Visible="true">
        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvListadoFac" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" HorizontalAlign="Center" Width="101%" AllowSorting="True"
            PageSize="5" DataKeyNames="FAC_ID" OnSelectedIndexChanged="grvListadoFac_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:TemplateField HeaderText="ID" FooterStyle-Wrap="False" Visible="false">
                    <ItemTemplate>
                        <%# Eval("FAC_ID") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Secuencial" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_SECUENCIAL") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_FECHAEMISION","{0:d}".ToString()) %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cliente" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_RAZONCOMPRADOR") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Factura" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_IMPORTETOTAL") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recaudado" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_RECAUDADO","{0:#,##0.##}".ToString()) %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_ESTADO") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripción" FooterStyle-Wrap="False">
                    <ItemTemplate>
                        <%# Eval("FAC_SRI") %>
                    </ItemTemplate>
                    <FooterStyle Wrap="False" />
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
            </fieldset>
        </asp:Panel>

    <asp:Panel ID="pnFactura" CssClass="" runat="server" Visible="false">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend></legend>
            <asp:HiddenField ID="hEstado" runat="server" />
            <asp:HiddenField ID="hRecaudado" runat="server" />
            <asp:Label ID="lblnumFactura" CssClass="lblForm" runat="server" Text="# Documento"></asp:Label>
            <asp:TextBox ID="txtNumFactura" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblFecha" CssClass="lblForm" runat="server" Text="Fecha" ></asp:Label>
            <asp:TextBox ID="txtFecha" CssClass="txtForm" runat="server" Enabled="false" ></asp:TextBox>
            <asp:Label ID="lblRucComprador" CssClass="lblForm" runat="server" Text="R.U.C."></asp:Label>
            <asp:TextBox ID="txtRucComprador" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>
            <asp:Label ID="lblNombres" CssClass="lblForm" runat="server" Text="Nombres"></asp:Label>
            <asp:TextBox ID="txtNombres" CssClass="txtForm" runat="server" Enabled="false"></asp:TextBox>

            <asp:GridView ID="grFacturaDetalle" runat="server" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" HorizontalAlign="Center" Width="95%" AllowSorting="True"
                PageSize="5" DataKeyNames="FAC_ID" ForeColor="#333333">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="false" />
                    <asp:TemplateField HeaderText="ID" FooterStyle-Wrap="False" Visible="false">
                        <ItemTemplate>
                            <%# Eval("FDE_ID") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Código" FooterStyle-Wrap="False">
                        <ItemTemplate>
                            <%# Eval("FDE_CODIGOAUX") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item" FooterStyle-Wrap="false">
                        <ItemTemplate>
                            <%# Eval("FDE_DESCRIPCION") %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cantidad" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("FDE_CANTIDAD","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="% IVA" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("FDE_TARIFA","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IVA" FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("FDE_VALOR","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prec.Unit." FooterStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%# Eval("FDE_PRECIOUNITARIO","{0:#,##0.##}".ToString()) %>
                        </ItemTemplate>
                        <FooterStyle Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:Panel ID="pnBotones" CssClass="pnListarGridRecacaudacion" runat="server"
                BackColor="white" ScrollBars="None" GroupingText="Botones">
                <asp:Panel ID="pnOpcion" CssClass="pnAccion" runat="server">
                    <asp:Button ID="btnBorrarFac" CssClass="btnProceso" runat="server" Text="Borrar factura" OnClick="btnBorrarFac_Click" />
                    <asp:Button ID="btnAnularFac" CssClass="btnProceso" runat="server" Text="Anular factura" OnClick="btnAnularFac_Click" />
					<asp:Button ID="btnNoUtilizar" CssClass="btnProceso" runat="server" Text="No utilizar factura" OnClick="btnNoUtilizar_Click" />
                    <asp:Button ID="btnCancelarFac" CssClass="btnProceso" runat="server" Text="Regresar" OnClick="btnCancelarFac_Click" />
                    <asp:Button ID="btnImprimir" CssClass="btnProceso" runat="server" Text="Imprimir factura" visible="false" OnClick="btnImprimir_Click" />
                    <asp:Label ID="Label1" CssClass="lblForm" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </asp:Panel>
            </fieldset>
        </asp:Panel>

</asp:Content>

