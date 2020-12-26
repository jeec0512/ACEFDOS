<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirColaborador.aspx.cs" Inherits="Catalogo_imprimirColaborador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ANETA-COLABORADORES</title>
    <link href="../App_Themes/Estilos/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnCabecera" runat="server">
                <asp:Panel ID="pnContieneSucursal" runat="server" Style="margin-bottom: 0.5rem">
                    <asp:Label ID="lblSucursal" CssClass="" runat="server" Text="Sucursal" Style="font-size: 1rem;"></asp:Label>
                    <asp:TextBox id="txtSucursal" runat="server"></asp:TextBox>
                </asp:Panel>
                
            </asp:Panel>
            <asp:Panel runat="server" ID="pnDetalleNomina">
          
                <asp:GridView ID="grvColaboradores" runat="server"
                    DataKeyNames="cedula"
                    AutoGenerateColumns="False"
                    CssClass="grillaGral" BorderColor="Blue" BorderStyle="Double"
                    ForeColor="Blue">
                    <Columns>
                        <asp:CommandField ShowSelectButton="false" />

                        <asp:BoundField DataField="Row" HeaderText="#" />
                        <asp:BoundField DataField="cedula" HeaderText="Cédula" />
                        <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" />


                        <asp:BoundField DataField="fechaIng" HeaderText="Fecha de ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="funcion" HeaderText="Cargo" HeaderStyle-CssClass="ancho" ItemStyle-CssClass="ancho" />
                        <asp:BoundField DataField="RELACION" HeaderText="Relación laboral" />

                        <asp:ImageField DataImageUrlField="foto" HeaderText="Imagen" AlternateText="Foto no encontrada" DataAlternateTextField="textoAlterno" NullImageUrl="~/admArchivos/nomina/sinImagen.jpg" NullDisplayText="Path de imagen es NULL" ControlStyle-CssClass="foto" ControlStyle-Font-Size="12px">
                            <ControlStyle CssClass="foto" />
                        </asp:ImageField>




                    </Columns>

                    <EditRowStyle ForeColor="Blue" />
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Blue" />

                </asp:GridView>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
