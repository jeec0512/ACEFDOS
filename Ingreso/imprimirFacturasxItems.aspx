<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirFacturasxItems.aspx.cs" Inherits="Ingreso_imprimirFacturasxItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>AUTOMOVIL CLUB DEL ECUADOR ANETA</title>
    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            <asp:Panel ID="Panel1" CssClass="" runat="server" Visible="true" Width="50%">
                <asp:Label ID="lblTitulo" runat="server" Text="AUTOMOVIL CLUB DEL ECUADOR ANETA"></asp:Label><br />
                <asp:Label ID="lblSubTitulo" runat="server" Text="FACTURACIÓN POR ITEMS"></asp:Label><br />
                <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label><br />
                <asp:Label ID="lblFechas" runat="server" Text="Fecha"></asp:Label><br />
            </asp:Panel>
            <asp:Panel ID="pnFacturasEmitidas" CssClass="" runat="server" Visible="true">

                <asp:GridView ID="grvResulItem" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100"
                    OnRowDataBound="grvResulItem_RowDataBound" ShowFooter="True">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="CURPRODUCTO" HeaderText="Producto" Visible="TRUE" />
                        <asp:BoundField DataField="PRODUCTO" HeaderText="Descripción" Visible="TRUE" />
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="valorUnit" HeaderText="Val/Unit" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="PRECIO" HeaderText="Venta Bruta" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="descuento" HeaderText="Descuento" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="TOTALASINIMP" HeaderText="Venta neta" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="IVA12" HeaderText="IVA 12" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="IVA0" HeaderText="IVA 0" DataFormatString="{0:N}" />
                        <asp:BoundField DataField="CXP" HeaderText="CXP" DataFormatString="{0:N}" />
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
            <fieldset id="Fieldset2" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="Panel3" CssClass="pnFormHijo" runat="server" Visible="true">
                    <asp:Label ID="Label1" runat="server" Text="ADMINISTRADOR SUCURSAL" ForeColor="#000099"></asp:Label>
                    <br />
                    <br />
                    <br />
                </asp:Panel>
                <asp:Panel ID="Panel4" CssClass="pnFormHijo" runat="server" Visible="true">
                    <asp:Label ID="Label4" runat="server" Text="SECRETARIA CONTADORA" ForeColor="#000099"></asp:Label>
                    <br />
                    <br />
                    <br />
                </asp:Panel>
            </fieldset>
            <asp:Panel ID="Panel2" CssClass="" runat="server" Visible="true">
                <asp:Label ID="lblHoy" runat="server" Text="" ForeColor="#000099"></asp:Label>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
