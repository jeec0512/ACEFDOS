<%@ Page Title="" Language="C#" MasterPageFile="~/Cartera/mpCartera.master" AutoEventWireup="true" CodeFile="estadoCliente.aspx.cs" Inherits="Cartera_estadoCliente" EnableEventValidation="false" %>

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
            <legend>Estado del cliente</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeqSoc" runat="server" Visible="true">
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="Buscar por:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="True"></asp:Label>
                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Visible="true" Font-Size="Larger" ForeColor="White"
                    BackColor="#9aaff1">
                    <asp:ListItem Value="0">RUC/C.C.</asp:ListItem>
                    <asp:ListItem Value="1">Apellidos-Nombres</asp:ListItem>
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
            <asp:Panel ID="pnClientes" CssClass="pnPeqSoc" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Cliente">

                <asp:GridView ID="grvClientes" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                    HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                    AutoGenerateSelectButton="True" OnSelectedIndexChanged="grvClientes_SelectedIndexChanged"
                    DataKeyNames="cli_ruc" CssClass="peqDdl">

                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="RUC/C.C.">
                            <ItemTemplate>
                                <%# Eval("cli_ruc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("cli_telefono") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Celular">
                            <ItemTemplate>
                                <%# Eval("cli_celular") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-mail">
                            <ItemTemplate>
                                <%# Eval("cli_email") %>
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

            <asp:Panel ID="pnFacturasEmitidas" CssClass="pnPeqSoc" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Facturas">
                <asp:GridView ID="grvFacturasEmitidas" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical"
                    HorizontalAlign="Center" Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50"
                    AutoGenerateSelectButton="True" OnSelectedIndexChanged="grvTotalSocios_SelectedIndexChanged"
                    DataKeyNames="unico">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Código" Visible="false">
                            <ItemTemplate>
                                <%# Eval("unico") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sucursal" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("fac_sucursal") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("fac_fechaemision","{0:d}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="# factura">
                            <ItemTemplate>
                                <%# Eval("fac_secuencial") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total factura">
                            <ItemTemplate>
                                <%# Eval("fac_importetotal","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total pagado">
                            <ItemTemplate>
                                <%# Eval("fac_recaudado","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Concepto">
                            <ItemTemplate>
                                <%# Eval("fac_tipoConcepto") %>
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

            <asp:Panel ID="pnProductos" CssClass="pnPeqSoc" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Productos">
                <asp:GridView ID="grvProductos" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <%# Eval("fde_codigoaux") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("fde_descripcion") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cant." ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("fde_cantidad","{0:#,##0}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prec/Unit">
                            <ItemTemplate>
                                <%# Eval("fde_preciounitario","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dscto.">
                            <ItemTemplate>
                                <%# Eval("fde_descuento","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <%# Eval("fde_baseimponible","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IVA">
                            <ItemTemplate>
                                <%# Eval("fde_valor","{0:#,##0.##}".ToString()) %>
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

            <asp:Panel ID="pnTesoreria" CssClass="pnPeqSoc" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Tesorería">
                <asp:GridView ID="grvTesoreria" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="95%" AllowPaging="false" AllowSorting="True" PageSize="50">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <%# Eval("numero") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("valor","{0:#,##0}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ret/Fuente" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("retencionFuente","{0:#,##0}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ret/IVA">
                            <ItemTemplate>
                                <%# Eval("retencionIva","{0:#,##0.##}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo/pago">
                            <ItemTemplate>
                                <%# Eval("descripcionTipoPago") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <%# Eval("descripcionTipoDetalle") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Documento">
                            <ItemTemplate>
                                <%# Eval("numeroDocumento") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("estado") %>
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

            <asp:Panel ID="pnHistorialMembrecias" CssClass="pnPeqSoc" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Historial de membrecías">
                <asp:GridView ID="grvHistorialMembrecias"
                    runat="server"
                    AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999"
                    BorderStyle="None"
                    BorderWidth="1px"
                    CellPadding="5"
                    GridLines="Vertical" HorizontalAlign="Center"
                    Width="95%"
                    AllowPaging="false"
                    AllowSorting="True"
                    PageSize="50" AutoGenerateSelectButton="True"
                    DataKeyNames="ncontrato_membr"
                    OnSelectedIndexChanged="grvHistorialMembrecias_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Código">
                            <ItemTemplate>
                                <%# Eval("cod_suc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#Factura" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("factura") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#Contrato" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <%# Eval("ncontrato_membr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                                <%# Eval("tipo_membr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inicio">
                            <ItemTemplate>
                                <%# Eval("fecha_afiliacion_membr","{0:d}".ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Finaliza">
                            <ItemTemplate>
                                <%# Eval("fecha_vencimie_membr","{0:d}".ToString())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendedor">
                            <ItemTemplate>
                                <%# Eval("vendedor_membr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono">
                            <ItemTemplate>
                                <%# Eval("telefono_contac") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Celular">
                            <ItemTemplate>
                                <%# Eval("celular_contac") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="E-mail">
                            <ItemTemplate>
                                <%# Eval("email_contac") %>
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

            <asp:Panel ID="pnAuxilio" CssClass="pnPeqSoc" runat="server" 
                ScrollBars="Vertical" Wrap="False" GroupingText="Auxilios mecánicos prestados">
                <asp:GridView ID="grvAuxilio"
                    runat="server"
                    AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999"
                    BorderStyle="None"
                    BorderWidth="1px"
                    CellPadding="5"
                    GridLines="Vertical" HorizontalAlign="Center"
                    Width="95%"
                    AllowPaging="false"
                    AllowSorting="True"
                    PageSize="50">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Fecha">
                            <ItemTemplate>
                                <%# Eval("fecha","{0:d}".ToString()) %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Avería">
                            <ItemTemplate>
                                <%# Eval("averia") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <%# Eval("marca") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Observación">
                            <ItemTemplate>
                                <%# Eval("observaciones") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# Eval("estado") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Razón">
                            <ItemTemplate>
                                <%# Eval("razon_anula") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono ">
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
        </fieldset>
    </asp:Panel>


</asp:Content>

