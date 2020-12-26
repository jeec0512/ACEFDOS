﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirSicoPractico.aspx.cs" Inherits="Escuela_imprimirSicoPractico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>PsicoPráctico ANETA</title>
    <link href="../App_Themes/Estilos/estiloImprimir.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header>
                <div class="padre">
                    <img src="../images/iconos/icoaneta2.png" />
                </div>

                <div class="padre">
                    <asp:Label ID="lblTitulo" runat="server" CssClass="lblCentrado" Text="CERTIFICADO" Font-Size="XX-Large"
                        Font-Bold="true"></asp:Label>
                </div>

                <div class="padre">
                    <asp:Label ID="lblSubtitulo" runat="server" CssClass="lblCentrado" Text="No. de convenio de autorización 002-2019"
                        Font-Size="Larger" Font-Bold="true"></asp:Label>
                </div>

            </header>
            <nav>
            </nav>
            <main>
                <div class="">
                        <asp:Label ID="lblCuerpo" runat="server" Text="La escuela de Conducción " Font-Size="Large"></asp:Label>
                  
                </div>
                <div class="">
                     <p> <p>
                    <asp:Label ID="lblFecha" runat="server"  Text="Fecha" Font-Size="Medium" ></asp:Label>
                </div>
               
            </main>
            <footer>
                <section id="mitad1">

                    <div class="padreFirmas">
                        <asp:Label ID="lblDirector" runat="server" CssClass="lblCentradoFirmas" Text="Director Escuela" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblDirEsc" runat="server" CssClass="lblCentradoFirmas" Text="Director de Escuela" Font-Size="Medium"></asp:Label>

                    </div>
                    <div class="padreFirmas ">
                    </div>
                </section>
                <section id="mitad2">
                    <div class="padreFirmas">
                        <asp:Label ID="lblPractico" runat="server" CssClass="lblCentradoFirmas" Text="Responsable del examen práctico"
                            Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblResPrac" runat="server" CssClass="lblCentradoFirmas" Text="Responsable del Examen Práctico"
                            Font-Size="Medium"></asp:Label>
                    </div>
                    <div class="padre">
                    </div>
                </section>

                <div class="padre">
                    <asp:Label ID="lblPsico" runat="server" CssClass="lblCentradoFirma" Text="Responsable del examen Psico"
                        Font-Size="Medium"></asp:Label>
                    <asp:Label ID="lblRespSico" runat="server" CssClass="lblCentradoFirma" Text="Responsable del Examen Psicotécnico"
                        Font-Size="Medium"></asp:Label>
                </div>


            </footer>
            <nav class="fin">

                <div class="posData">
                    <img runat="server" id="imgCtrl" height="150" width="150" />

                    <asp:Label ID="lblOficio" runat="server" Text="283 QTE53481 KMN" Font-Size="Smaller"></asp:Label>
                    <asp:Label ID="lblFactura" runat="server" Text="283 QTE53481 KMN" Font-Size="Smaller"></asp:Label>
                    <asp:Label ID="lblIniciales" runat="server" Text="283 QTE53481 KMN" Font-Size="Smaller"></asp:Label>
                </div>


            </nav>
        </div>
    </form>
</body>
</html>
