<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="generarCodigoQR.aspx.cs"
    Inherits="Escuela_generarCodigoQR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <div>
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnGenerar" runat="server" Text="Generar" OnClick="btnGenerar_Click" />
        <hr />
        <div>
            <img runat="server" id="imgCtrl" />
        </div>
    </div>
</asp:Content>

