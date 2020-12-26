<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="multiplesFilas.aspx.cs" Inherits="Escuela_multiplesFilas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <script src="../js/jquery-1.11.1.min.js" type="text/javascript"></script>

    

    <asp:Button ID="btnConfirmar" runat="server" Text="Realizar el pedido" OnClick="btnConfirmar_Click" ></asp:Button>
    <asp:GridView ID="grvEstudiantes" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" >
    <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="cbConfirmarHeader" runat="server"  onclick="toggleSelectionUsingHeaderCheckBox(this);"/>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbConfirmar" onlick="toggleSelectionOfHeaderCheckBox();" runat="server"   />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="idNota">
                <ItemTemplate>
                    <asp:Label ID="lblIdNota" runat="server" Text='<%# Bind("RNOTC_ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RNOTC_ID" HeaderText="idReg" />
            <asp:BoundField DataField="RNOTC_CIRUC" HeaderText="Identificación" />
            <asp:BoundField DataField="ALUMNO" HeaderText="NOMBRES" />
            <asp:BoundField DataField="REG_FACTURANUMERO" HeaderText="#FACTURA" />
        </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
   <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ></asp:Label>

    <script language="javascript" type="text/javascript">
       

        function toggleSelectionUsingHeaderCheckBox(source) {
            $("#grvEstudiantes input[name$='cbConfirmar']").each(function () {
                $(this).attr('checked', source.checked);
            });
        }

        function toggleSelectionOfHeaderCheckBox() {
            if ($("#grvEstudiantes input[name$='cbConfirmar']").length == $("#grvEstudiantes input[name$='cbConfirmar']:checked").length) {
                $("#grvEstudiantes input[name$='cbConfirmarHeader']").first().attr('checked', true);
            }
            else {
                $("#grvEstudiantes input[name$='cbConfirmarHeader']").first().attr('checked', false);
            }
        }
        console.log("Hello world!");
    </script>
</asp:Content>

