<%@ Page Title="" Language="C#" MasterPageFile="~/Cartera/mpCartera.master" AutoEventWireup="true" CodeFile="retencionesProveedor.aspx.cs" Inherits="Cartera_retencionesProveedor"  EnableEventValidation="false" %>

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
            <legend>Estado del proveedor</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeqSoc" runat="server" Visible="true">
                 <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="Buscar por:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="True"></asp:Label>
                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Visible="true" Font-Size="Larger" ForeColor="White"
                    BackColor="#9aaff1">
                    <asp:ListItem Value="0">RUC/C.C.</asp:ListItem>
                    <asp:ListItem Value="1">Razón social</asp:ListItem>
                </asp:DropDownList>

                <asp:TextBox runat="server" ID="txtBuscar" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase"
                    BorderColor="#9aaff1"></asp:TextBox>
                
                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.ico" Width="27px" ToolTip="Buscar"
                    BorderColor="#9aaff1" OnClick="imgBuscar_Click" />
                   
            </asp:Panel>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnGeneral" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend></legend>
            <asp:Panel ID="pnProveedores" CssClass="" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Cliente">

                <asp:GridView ID="grvProveedores" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                    HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                    AutoGenerateSelectButton="True" OnSelectedIndexChanged="grvProveedores_SelectedIndexChanged"
                    DataKeyNames="ruc" CssClass="peqDdl">

                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="RUC/C.C.">
                            <ItemTemplate>
                                <%# Eval("ruc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Razón social" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("razonsocial") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirección" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("dirMatriz") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-Mail">
                            <ItemTemplate>
                                <%# Eval("e_mail") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono">
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
            </asp:Panel>

            <asp:Panel ID="pnFacturasCanceladas" CssClass="" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Facturas">

                 <asp:Button ID="btnExcelSS" runat="server" CssClass="btnLargoForm " Text="A Excel total" Visible="true"
            OnClick="btnExcelSS_Click" />

                <asp:GridView ID="grvFacturasEmitidas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                    HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                    AutoGenerateSelectButton="false" 
                    DataKeyNames="identificacionsujetoretenido">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                         <asp:TemplateField HeaderText="Fecha" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("fechadocumento","{0:d}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Concepto" Visible="true">
                            <ItemTemplate>
                                <%# Eval("campoadicional") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="#Documento" Visible="true">
                            <ItemTemplate>
                                <%# Eval("numDocsustento") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="ValorDoc" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("totalfactura","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Retencion" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("totretenido","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Pagado">
                            <ItemTemplate>
                                <%# Eval("apagar","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#Retención">
                            <ItemTemplate>
                                <%# Eval("retencion") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sucursal">
                            <ItemTemplate>
                                <%# Eval("nom_suc") %>
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
            </asp:Panel>

            

            
        </fieldset>
    </asp:Panel>
</asp:Content>

