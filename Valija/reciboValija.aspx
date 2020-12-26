<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reciboValija.aspx.cs" Inherits="Valija_reciboValija" %>

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
                    Text="RECEPCION DE VALIJA"></asp:Label><br />
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
                AllowPaging="FALSE"
                AllowSorting="True"
                PageSize="50"
                DataKeyNames="NUMERO" >

                <AlternatingRowStyle
                    BackColor="#DCDCDC" />

                <Columns>
                    <asp:CommandField
                        ShowSelectButton="false" />

                    <asp:TemplateField HeaderText="ENTREGADO A">
                        <ItemTemplate>
                            <%# Eval("ENTREGADO") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SUCURSAL ORIGEN">
                        <ItemTemplate>
                            <%# Eval("NOM_SUC") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPCION">
                        <ItemTemplate>
                            <%# Eval("descripcionRecibe") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="# GUIA">
                        <ItemTemplate>
                            <%# Eval("NUMEROGUIA") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRMA">
                        <ItemTemplate>
                            <%# Eval("FIRMA") %>
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
