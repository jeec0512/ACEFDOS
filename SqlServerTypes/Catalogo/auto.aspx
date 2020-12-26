<%@ Page Title="" Language="C#" MasterPageFile="~/Catalogo/mpCatalogo.master" AutoEventWireup="true"
    CodeFile="auto.aspx.cs" Inherits="Catalogo_auto" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAuto" runat="server">
            <fieldset id="fsAuto">
                <legend>Autos</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>
                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField=nom_suc DataValueField=sucursal runat="server" 
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblMarca" CssClass="lblForm" runat="server" Text="Marca"></asp:Label>
                <asp:TextBox ID="txtMarca" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblModelo" CssClass="lblForm" runat="server" Text="Modelo"></asp:Label>
                <asp:TextBox ID="txtModelo" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblAno" CssClass="lblForm" runat="server" Text="Año"></asp:Label>
                <asp:TextBox ID="txtAno" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblNumero" CssClass="lblForm" runat="server" Text="Número"></asp:Label>
                <asp:TextBox ID="txtNumero" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblChasis" CssClass="lblForm" runat="server" Text="Chasis"></asp:Label>
                <asp:TextBox ID="txtChasis" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblMotor" CssClass="lblForm" runat="server" Text="Motor"></asp:Label>
                <asp:TextBox ID="txtMotor" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblPlaca" CssClass="lblForm" runat="server" Text="Placa"></asp:Label>
                <asp:TextBox ID="txtPlaca" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblSuc_Id" CssClass="lblForm" runat="server" Text="SucId"></asp:Label>
                <asp:TextBox ID="txtSuc_Id" CssClass="txtForm" runat="server"></asp:TextBox>
                <!--<asp:Label ID="lblTve_id" CssClass="lblForm" runat="server" Text="Tve_id"></asp:Label>
                <asp:TextBox ID="txtTve_id" CssClass="txtForm" runat="server"></asp:TextBox>-->
                <asp:Label ID="lblPer_id" CssClass="lblForm" runat="server" Text="Per_id"></asp:Label>
                <asp:TextBox ID="txtPer_id" CssClass="txtForm" runat="server"></asp:TextBox>
                <asp:Label ID="lblEstado" CssClass="lblForm" runat="server" Text="Estado"></asp:Label>
                <asp:Panel ID="pnEstado" runat="server" CssClass="pnFormChk">
                    <asp:CheckBox ID=chkEstado TextAlign=Left runat="server" />
                </asp:Panel>
              <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID=btnGuardar runat="server" Text="Grabar" CssClass="btnForm" OnClick="btnGuardar_Click" />
                    <asp:HyperLink ID=blRegresar runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"></asp:HyperLink>
                </asp:Panel>

            </fieldset>
        </asp:Panel>
    </asp:Panel>

    <asp:Panel ID="pnDetalleAuto" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDetalleAuto" class="fieldset-principal">
            <legend></legend>
            <asp:GridView ID="grvAutoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                Width="90%"
                AllowSorting="True" PageSize="50" OnRowCommand="grvAutoDetalle_RowCommand"
                OnRowDataBound="grvAutoDetalle_RowDataBound" ShowFooter="false">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:ButtonField HeaderText="Modificar" Text="..." ButtonType="Image" ImageUrl="~/images/iconos/mod.ico"
                        CommandName="modReg" ItemStyle-Width="60" />
                    <asp:BoundField DataField="Veh_id" HeaderText="Código" Visible="true" ItemStyle-CssClass="DisplayNone"
                        HeaderStyle-CssClass="DisplayNone" />
                    <asp:BoundField DataField="veh_numero" HeaderText="# vehículo" Visible="true" />
                    <asp:BoundField DataField="veh_placa" HeaderText="Placa" Visible="true" />
                    <asp:BoundField DataField="veh_marca" HeaderText="Marca" Visible="true" />
                    <asp:BoundField DataField="veh_modelo" HeaderText="Modelo" Visible="true" />
                    <asp:BoundField DataField="veh_anio" HeaderText="Año" Visible="true" />
                    <asp:BoundField DataField="veh_motor" HeaderText="Motor" Visible="true" />
                    <asp:BoundField DataField="veh_chasis" HeaderText="Chasis" Visible="true" />
                    


                </Columns>
                <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                    Font-Strikeout="False" />
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

