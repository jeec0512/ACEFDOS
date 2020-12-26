<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="multiplesFilas.aspx.cs" Inherits="Escuela_multiplesFilas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">

    <asp:Button ID="btnConfirmar" runat="server" Text="Realizar el pedido" OnClick="btnConfirmar_Click" ></asp:Button>
    <asp:GridView ID="grvEstudiantes" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="cbConfirmarHeader" runat="server" AutoPostBack="True" OnCheckedChanged="cbConfirmarHeader_CheckedChanged" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="cbConfirmar" runat="server" OnCheckedChanged="cbConfirmar_CheckedChanged" />
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
</asp:GridView>
   <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ></asp:Label>
</asp:Content>

