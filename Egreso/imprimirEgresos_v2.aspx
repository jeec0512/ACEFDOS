<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirEgresos_v2.aspx.cs" Inherits="Egreso_imprimirEgresos_v2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>AUTOMOVIL CLUB DEL ECUADOR ANETA</title>
    <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset id="Fieldset1" class="fieldset-principal">
                <legend></legend>
                <asp:Panel ID="Panel1" CssClass="" runat="server" Visible="true" Width="50%">
                    <asp:Label ID="lblTitulo" runat="server" Text="AUTOMOVIL CLUB DEL ECUADOR ANETA"></asp:Label><br />
                    <asp:Label ID="lblSubTitulo" runat="server" Text="REPORTE DE GASTOS"></asp:Label><br />
                    <asp:Label ID="lblSucursal" runat="server" Text="Sucursal"></asp:Label><br />
                    <asp:Label ID="lblFechas" runat="server" Text="Fecha"></asp:Label><br />
                </asp:Panel>
            </fieldset>
            <asp:Panel ID="pnDetalleCaja" CssClass="" runat="server" Visible="true">

                <fieldset id="fdDetalleCaja" class="fieldset-principal">
                    <legend></legend>
                    <asp:GridView ID="grvEgresosDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" HorizontalAlign="Center"
                        Width="90%"
                        AllowSorting="True" PageSize="50" Font-Size="Small"
                        OnRowDataBound="grvEgresosDetalle_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:BoundField HeaderText="fecha" DataField="Fecha"  Visible="true" DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="numero" HeaderText="Caja" Visible="true"  />
                    <asp:BoundField DataField="numerodocumento" HeaderText="#Documento" Visible="true"  />
                    <asp:BoundField DataField="doc_autorizacion" HeaderText="Autorización" Visible="true"  />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha emisión" Visible="true"  />
                    <asp:BoundField DataField="fechacaducdoc" HeaderText="Fecha caducidad" Visible="true"  />
                    <asp:BoundField DataField="dgas_item" HeaderText="Producto" Visible="true"  />
                    <asp:BoundField DataField="dgas_valor" HeaderText="Valor" Visible="true"  />
                    <asp:BoundField DataField="cod_cta" HeaderText="CuentaContable" Visible="true"  />
                    <asp:BoundField DataField="numretencion" HeaderText="#retencion" Visible="true"  />

                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                            Font-Strikeout="False" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </fieldset>
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
