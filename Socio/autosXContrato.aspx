<%@ Page Title="" Language="C#" MasterPageFile="~/Socio/mpSocio.master" AutoEventWireup="true" CodeFile="autosXContrato.aspx.cs" Inherits="Socio_autosXContrato" EnableEventValidation="false" %>

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
            <legend>Ingreso de contratos del socio</legend>
            <asp:Panel ID="pnCabecera" CssClass="pnBuscarGrid" runat="server">
                <asp:Label ID="lblBuscarx" runat="server" Text="Buscar por:" Font-Bold="True"
                    Font-Size="Larger" ForeColor="darkblue" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlTipoBusqueda" runat="server" Visible="false" Font-Size="Larger"
                    ForeColor="darkblue" BackColor="#9aaff1">
                    <asp:ListItem
                        Value="0">RUC/C.C.</asp:ListItem>
                    <asp:ListItem
                        Value="1">Apellidos y/o Nombres</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblSocio" runat="server" Text="RUC:" Font-Bold="True" Font-Size="Larger" ForeColor="darkblue"
                    Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtSocio" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1"></asp:TextBox>
                <asp:Label ID="lblContrato" runat="server" Text="#Contrato" Font-Bold="True"
                    Font-Size="Larger" ForeColor="darkblue" Visible="true"></asp:Label>
                <asp:TextBox runat="server" ID="txtContrato" Font-Size="Larger" ForeColor="darkblue"
                    Style="text-transform: uppercase" BorderColor="#9aaff1" Visible="true"></asp:TextBox>
                <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/iconos/219.ico"
                    Width="27px" ToolTip="Buscar" BorderColor="#9aaff1" OnClick="imgBuscar_Click" />
                <asp:Label ID="lblMsg" runat="server" Text="MSG" Font-Bold="True" Font-Size="Small" ForeColor="red"
                    Visible="false"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnGuardar" CssClass="pnPeq" runat="server" Wrap="False">
                <asp:Button
                    ID="btnGuardar" runat="server" CssClass=btnProceso Text="Modificar" Visible="true" OnClick="btnGuardar_Click" />
                <asp:Button
                    ID="btnCancelar" runat="server" CssClass=btnProceso Text="Regresar" Visible="true" OnClick="btnCancelar_Click" />
            </asp:Panel>
        </fieldset>
        </asp:Panel>


     <asp:Panel ID="pnGeneral" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Registro de autos</legend>
            <asp:Label CssClass="lblPeq" ID="lblMarca" runat="server" Text="Marca" Visible="true"></asp:Label>
            <asp:TextBox runat="server" ID="txtMarca" CssClass="txtPeq" Visible="true"></asp:TextBox>
            <asp:Label ID="lblModelo" runat="server" Text="Modelo" CssClass="lblPeq" Visible="true"></asp:Label>
            <asp:TextBox runat="server" ID="txtModelo" CssClass="txtPeq" Visible="true"></asp:TextBox>
            <asp:Label ID="lblano" runat="server" Text="Año" CssClass="lblPeq" Visible="true"></asp:Label>
            <asp:TextBox runat="server" ID="txtAno" CssClass="txtPeq" Visible="true"></asp:TextBox>
            <asp:Label ID="lblPlaca" runat="server" Text="Placa" CssClass="lblPeq" Visible="true"></asp:Label>
            <asp:TextBox runat="server" ID="txtPlaca" CssClass="txtPeq" Visible="true"></asp:TextBox>
            <asp:Label ID="lblColor" runat="server" Text="Color" CssClass="lblPeq" Visible="true"></asp:Label>
            <asp:TextBox runat="server" ID="txtColor" CssClass="txtPeq" Visible="true"></asp:TextBox>
            <asp:Label ID="lblChasis" runat="server" Text="Chasis" CssClass="lblPeq" Visible="true"></asp:Label>
            <asp:TextBox runat="server" ID="txtChasis" CssClass="txtPeq" Visible="true"></asp:TextBox>


            <asp:TextBox runat="server" ID="txtCedula" CssClass="txtForm" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtFecIni" CssClass="txtForm" Visible="false"></asp:TextBox>
            <asp:TextBox runat="server" ID="txtFecFin" CssClass="txtForm" Visible="false"></asp:TextBox>
            </fieldset>
         </asp:Panel>

    <asp:Panel ID="pnListadoAutos" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset2" class="fieldset-principal">
            <legend>Registro de autos</legend>
            </fieldset>
        <asp:GridView ID="grvListadoAutos" runat="server" AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None"
            BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center" Width="90%" AllowPaging="True"
            AllowSorting="True" PageSize="20">
            <AlternatingRowStyle
                BackColor="#DCDCDC" />
            <Columns>
                <asp:CommandField/>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <%# Eval("codveh") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha Inicial">
                    <ItemTemplate>
                        <%# Eval("fecini") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha caducidad">
                    <ItemTemplate>
                        <%# Eval("vigmem") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Marca">
                    <ItemTemplate>
                        <%# Eval("marca") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Modelo">
                    <ItemTemplate>
                        <%# Eval("modelo") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Año">
                    <ItemTemplate>
                        <%# Eval("ano") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Placa">
                    <ItemTemplate>
                        <%# Eval("placa") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Color">
                    <ItemTemplate>
                        <%# Eval("color") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Chasis">
                    <ItemTemplate>
                        <%# Eval("chasis") %>
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


</asp:Content>

