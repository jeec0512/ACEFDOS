<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImprimirListadosPractica.aspx.cs" Inherits="Escuela_ImprimirListadosPractica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de asistencia y notas prácticas</title>
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header class="header-main">
                <div class="header-img">
                    <figure>
                        <img class="imagenAneta" src="../images/iconos/icoaneta.png" />
                    </figure>
                </div>

                <div class="header-det">
                    <div class="header-det">
                        <asp:Label ID="lblTitulo" runat="server" Text="REPORTE DE ASISTENCIAS Y NOTAS PRÁCTICAS"></asp:Label>
                    </div>
                </div>
                

            </header>
            <nav>
                <!-- ESCUELA-->
                <div class="escuela">
                    <asp:Label ID="lblEscuela" runat="server" Text="Nombre de la Escuela"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEscuela"></asp:TextBox>
                </div>
                <!-- CURSO-->
                <div  style="margin-bottom:2rem;">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCurso"></asp:TextBox>

                    <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio Curso:"></asp:Label>
                    <asp:TextBox runat="server" ID="txtFechaInicio" Format="dd/MM/yyyy"></asp:TextBox>

                    <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Fin Curso:" Format="dd/MM/yyyy"></asp:Label>
                    <asp:TextBox runat="server" ID="txtFechaFin"></asp:TextBox>
                </div>
                <!-- HORARIO
                <div class="horario">
                    <asp:Label ID="lblHorario" runat="server" Text="Horario:"></asp:Label>
                    <asp:TextBox runat="server" ID="txtHorario"></asp:TextBox>
                </div>
                 -->

            </nav>
            <div>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:Panel ID="pnListado" CssClass="" runat="server" Visible="true" Style="display: grid; grid-template-columns: auto auto auto auto auto;justify-content:center">
                        <asp:GridView ID="grvListado" runat="server"></asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </div>
            <footer Style="margin-top:3rem;display: grid; grid-template-columns: auto auto auto auto auto;justify-content:center">
                <asp:Label runat="server" Text="FIRMA SUPERVISOR" Style="margin: auto; border-top:solid"></asp:Label>
            </footer>

        </div>
    </form>
</body>
</html>
