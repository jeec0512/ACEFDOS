<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirTitulosxPedido.aspx.cs" Inherits="Escuela_imprimirTitulosxPedido" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header>
                <div class="padre" style="width: 100%;">
                    <asp:Label ID="lblPermiso" runat="server" CssClass="lblCentrado" Text="permiso" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                </div>

                <div class="padre" style="width: 100%; height: 80px">
                    <asp:Label ID="Label1" runat="server" CssClass="lblCentrado" Text="" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                </div>

                <div class="padre">
                    <asp:Label ID="lblNombres" runat="server" CssClass="lblCentrado" Text="Nombres" Font-Size="Larger" Font-Bold="true"></asp:Label>
                </div>
                <div class="padre" style="width: 100%; height: 180px">
                    <asp:Label ID="Label2" runat="server" CssClass="lblCentrado" Text="" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                </div>
                <div style="width: 90%; height: 80px">
                    <div class="padre" >
                        <asp:Label ID="Label3" runat="server" CssClass="lblCentrado" Text="" Font-Size="XX-Large" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="padre" style="align-content:flex-end">
                        <asp:Label ID="lblFecha" runat="server" CssClass="lblCentrado" Text="Fecha" Font-Size="Larger" Font-Bold="true"></asp:Label>
                    </div>
                </div>
                <div class="padre">
                    <asp:Label ID="lblCurso" runat="server" CssClass="lblCentrado" Text="Curso" Font-Size="Larger" Font-Bold="true"></asp:Label>
                </div>
                <div class="padre">
                    <asp:Label ID="lblActa" runat="server" CssClass="lblCentrado" Text="Acta" Font-Size="Larger" Font-Bold="true"></asp:Label>
                </div>
                <div class="padre">
                    <asp:Label ID="lblCalificacion" runat="server" CssClass="lblCentrado" Text="Calificacion" Font-Size="Larger" Font-Bold="true"></asp:Label>
                </div>
                <div class="padre">
                    <asp:Label ID="lblFecha2" runat="server" CssClass="lblCentrado" Text="Fecha" Font-Size="Larger" Font-Bold="true"></asp:Label>
                </div>

            </header>


        </div>
    </form>
</body>
</html>
