<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirRetencion.aspx.cs" Inherits="Egreso_imprimirRetencion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Retención ANETA</title>
    <link href="../App_Themes/Estilos/estiloImprimir.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header>
                <section id="logo">
                    <img src="../images/iconos/anetatitulo.jpg" />
                </section>
                <section id="identificacion">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="lbl" Text="AUTOMOVIL CLUB DEL ECUADOR ANETA"></asp:Label>
                    <asp:Label ID="lblSucursal" runat="server" CssClass="lbl" Text="Sucursal"></asp:Label><br />
                    <asp:Label ID="lblBlanco" runat="server" CssClass="lbl" Text=""></asp:Label>
                    <asp:Label ID="lblDirMatriz" runat="server" CssClass="lbl" Text="Dir.Matriz: "></asp:Label>
                    <asp:Label ID="lblDirSucursal" runat="server" CssClass="lbl" Text="Dir.Sucursal: "></asp:Label>
                    <asp:Label ID="lblContibuyente" runat="server" CssClass="lbl" Text="Contribuyente Especial N°: "></asp:Label>
                    <asp:Label ID="lblObligado" runat="server" CssClass="lbl" Text="Obligado a llevar contabilidad: "></asp:Label>
                </section>
                <section id="cabecera">
                    <asp:Label ID="lblRuc" runat="server" CssClass="lbl" Text="RUC: "></asp:Label>
                    <asp:Label ID="lblretencion" runat="server" CssClass="lbl" Text="RETENCIÓN N° "></asp:Label>
                    <asp:Label ID="lblAutorizacion" runat="server" CssClass="lbl" Text="N° DE AUTORIZACION:"></asp:Label>
                    <asp:Label ID="lblNumAut" runat="server" CssClass="lbl" Text=""></asp:Label>
                    <asp:Label ID="lblFechaAut" runat="server" CssClass="lbl" Text="Fecha y hora de autorizacion: "></asp:Label>
                    <asp:Label ID="lblAmbiente" runat="server" CssClass="lbl" Text="AMBIENTE: "></asp:Label>
                    <asp:Label ID="lblEmision" runat="server" CssClass="lbl" Text="EMISION: "></asp:Label>
                    <asp:Label ID="lblClave" runat="server" CssClass="lbl" Text="CLAVE DE ACCESO: "></asp:Label>
                    <asp:Label ID="lblNumClave" runat="server" CssClass="lbl" Text=": "></asp:Label>


                </section>

            </header>
            <nav>
                <asp:Label ID="lblRso" runat="server" CssClass="lbl" Text="Razón Social/Nombres y Apellidos:"></asp:Label>
                <asp:Label ID="lblRucCliente" runat="server" CssClass="lbl" Text="RUC/CI:"></asp:Label>
                <asp:Label ID="lblFechaEmision" runat="server" CssClass="lbl" Text="FechaEmisión:"></asp:Label>
            </nav>
            <main>
                <section id ="detalle">
                    <asp:Panel ID="pnDetalleCaja" CssClass="" runat="server" Visible="true" >
          
                    <asp:GridView ID="grvDetalleretencion" runat="server" AutoGenerateColumns="False" BackColor="White" 
                        BorderStyle="None"  CellPadding="0" HorizontalAlign="Center"
                        Width="95%"
                        AllowSorting="True" PageSize="50" Font-Size="Small" 
                        ShowFooter="True" OnRowDataBound="grvDetalleretencion_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="comprobante" HeaderText="Comprobante" Visible="true"  />
                            <asp:BoundField DataField="cde_numdocsustento" HeaderText="Número" Visible="true"  />
                            <asp:BoundField DataField="cde_fechaemisdocsust" HeaderText="FechaEmisión" Visible="true" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="ejercicioFiscal" HeaderText="EjercicioFiscal" Visible="true" />
                            <asp:BoundField DataField="cde_baseimponible" HeaderText="N°Documento" Visible="true" />
                            <asp:BoundField DataField="impuesto" HeaderText="BI/Retención" Visible="true" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="impuesto" HeaderText="IMPUESTO" Visible="true" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="CDE_PORCENTARETENER" HeaderText="Porcentajeretención" Visible="true" DataFormatString="{0:N}" />
                            <asp:BoundField DataField="CDE_VALORRETENIDO" HeaderText="ValorRetenido" Visible="true" DataFormatString="{0:N}" />
                            
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

            </asp:Panel>
                </section>
            </main>
            <footer>
                <section id="pie">
                    <asp:Label ID="lblAdicional" runat="server" CssClass="lbl" Text="Información adicional"></asp:Label>
                    <asp:Label ID="lblDireccion" runat="server" CssClass="lbl" Text="Dirección:"></asp:Label>
                    <asp:Label ID="lblTelefono" runat="server" CssClass="lbl" Text="Teléfono:"></asp:Label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="Email:"></asp:Label>
                    <asp:Label ID="lblHoy" runat="server" CssClass="lbl" Text="" ForeColor="#000099"></asp:Label>
                </section>
            </footer>

        </div>
    </form>
</body>
</html>
