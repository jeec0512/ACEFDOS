<%@ Page Title="" Language="C#" MasterPageFile="~/Escuela/mpEscuela.master" AutoEventWireup="true" CodeFile="mensajeWhatsApp.aspx.cs" Inherits="Escuela_mensajeWhatsApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:Label ID=Label1 runat="server" Text="A:"></asp:Label>
    <asp:TextBox ID=txtA runat="server"></asp:TextBox>
    <asp:Label ID=Label2 runat="server" Text="Mensaje:"></asp:Label>
    <asp:TextBox ID=txtMemsaje runat="server"></asp:TextBox>
    <asp:Label ID=Label3 runat="server" Text="Estatus"></asp:Label>
    <asp:TextBox ID=txtEstatus runat="server"></asp:TextBox>
    <asp:Label ID=Label4 runat="server" Text="Nombre"></asp:Label>
    <asp:TextBox ID=txtNombre runat="server"></asp:TextBox>
    <asp:Label ID=Label5 runat="server" Text="Teléfono"></asp:Label>
    <asp:TextBox ID=txtTelefono runat="server"></asp:TextBox>
    <asp:Label ID=Label6 runat="server" Text="Contraseña"></asp:Label>
    <asp:TextBox ID=txtContrasena runat="server"></asp:TextBox>
    <asp:Button ID=btnEnviar runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
</asp:Content>

