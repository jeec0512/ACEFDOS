<%@ Page Language="C#" AutoEventWireup="true" CodeFile="envioValija.aspx.cs" Inherits="Valija_envioValija" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ANETA</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel
                ID="pnCierreCaja"
                CssClass="pnListarGridRecacaudacion"
                runat="server"
                BackColor="white"
                ScrollBars="Vertical"
                GroupingText="">
                <asp:Label
                    ID="lblTitulo"
                    runat="server"
                    Text="Automovil Club Del Ecuador ANETA"></asp:Label><br />
                <br />
                <asp:Label
                    ID="lblSubtitulo"
                    runat="server"
                    Text="Reporte de valija enviada"></asp:Label><br />
                <br />
                <asp:Label
                    ID="lblSucursal"
                    runat="server"
                    Text="Sucursal"></asp:Label><br />
                <br />
                <asp:Label
                    ID="lblFechas"
                    runat="server"
                    Text="Fecha" Visible="false"></asp:Label><br />
        <asp:Label
                    ID="lblDocumento"
                    runat="server"
                    Text="Documento"></asp:Label><br />
                <br />


                <asp:GridView ID="grvDetallePagos" runat="server"
                AutoGenerateColumns="False"
                CellPadding="5"
                GridLines="Vertical"
                HorizontalAlign="Center"
                Width="100%"
                AllowPaging="True"
                AllowSorting="True"
                PageSize="10"
                DataKeyNames="id_DetValija" >

                <AlternatingRowStyle
                    BackColor="#DCDCDC" />

                <Columns>
                    <asp:CommandField
                        ShowSelectButton="false" />
                    <asp:TemplateField HeaderText="Código" Visible="false">
                        <ItemTemplate>
                            <%# Eval("id_DetValija") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sucursal">
                        <ItemTemplate>
                            <%# Eval("sucDestino") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Departamento">
                        <ItemTemplate>
                            <%# Eval("departamento") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# Guía">
                        <ItemTemplate>
                            <%# Eval("numeroGuia") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripción del paquete">
                        <ItemTemplate>
                            <%# Eval("descripcionEnvio") %>
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
                <br />
                <asp:Label
                    ID="lblHoy"
                    runat="server"
                    Text=""></asp:Label>
                <br />
                <br />
                <asp:Label
                    ID="lblFirmas"
                    runat="server"
                    Text="Firmas:"></asp:Label><br />
                <br />
                <br />
                <br />

                <table runat="server" id="tblReporte" style="width: 100%">
                    <tr id="Tr1" runat="server">
                        <th id="Th1" runat="server">ADMINISTRADOR</th>
                        <th id="Th2" runat="server">RESPONSABLE DE ENVÍO</th>
                       
                    </tr>
                </table>



            </asp:Panel>
    </div>
    </form>
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>
    <script src="js/jquery.backstretch.min.js"></script>
    <script src="js/scripts.js"></script>
    <script src="~/js/cuerpo.js"></script>
</body>
</html>
