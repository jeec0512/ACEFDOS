<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirActaEntregaTitulos.aspx.cs" Inherits="secretariaAcademica_imprimirActaEntregaTitulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ACTA ENTREGA RECEPCION DE TITULOS</title>
    <link href="../App_Themes/Estilos/actaEntregaTitulos.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <header class="title">
                <figure class="title-img">
                    <img src="../images/iconos/icoaneta2.png" alt="ANETA">
                </figure>
                <h1 class="title-titulo">ACTA ENTREGA RECEPCION DE TÍTULOS DE CONDUCTOR NO PROFESIONAL</h1>
                <span class="title-acta">
                    <asp:Label runat="server" ID="lblNumActa"></asp:Label>
                </span>
            </header>
            <section class="entrega">
                <article class="article">
                    <p class="paragraf">
                        En la ciudad de Quito a los 
				<asp:Label runat="server" ID="lblDia"></asp:Label>
                        días del mes de 
				<asp:Label runat="server" ID="lblMes"></asp:Label>
                        del año 
				<asp:Label runat="server" ID="lblAno"></asp:Label>, el 
				<span>Ing. Fabio Esteban Tamayo Proaño,</span> Director General de Escuelas de Conducción 
				<span>ANETA,</span> y el Señor (a) 
				<span>Srta. Lourdes Catalina Maldonado Novoa</span> hacen entrega al señor (a) 
				<asp:Label runat="server" ID="lblNombresAdministrador"></asp:Label>
                        para la sucursal de 
				<span></span>
                        <asp:Label runat="server" ID="lblSucursal"></asp:Label>
                        <span>, de </span>
                        <asp:Label runat="server" ID="lblNumeroTitulos"></asp:Label>

                        <span>TITULOS DE CONDUCTOR NO PROFESIONAL</span> del 
                <asp:Label runat="server" ID="lblDel"></asp:Label>
                        <span>al</span>
                        <asp:Label runat="server" ID="lblAl"></asp:Label>
                        quien los recibe a su entera y absoluta conformidad.
                    </p>

                    <p class="paragraf">
                        Con la firma de esta acta, el Señor (a) 
                        <asp:Label runat="server" ID="lblAdministrador"></asp:Label>
				<span>,</span>
                        asume la responsabilidad del custodio, buen uso y emisión de los mencionados títulos de conductor no profesional,
                        cumpliendo con todos los requisitos y procedimientos establecidos por ANETA.
                    </p>

                    <p class="paragraf">Declaro bajo juramento y acepto libre y voluntariamente someterme a las acciones legales a que hubiere lugar por el uso indebido o la emisión incorrecta de los referidos documentos.</p>
                </article>
            </section>
            <section class="firmas">
                <div class="firmas-entrega">
                    <h2 class="firmas-subtitle">ENTREGAMOS CONFORME</h2>
                    
                    <figure class="firmas-images">
                       <!-- <img src="../images/firmas/firma1.png" alt="ANETA" class="images-item"/>
                        <img src="../images/firmas/firma2.png" alt="ANETA" class="images-item"/> -->
                    </figure>
                    <div class="firmas-signs">
                        <div class="firmas-second">
                            <p class="firmas-nombre linear">ING. FABIO ESTEBAN TAMAYO PROAÑO</p>
                            <p class="firmas-nombre">Director General</p>
                            <p class="firmas-nombre">de Escuelas de Conduciión ANETA</p>
                        </div>

                        <div class="firmas-second">
                            <p class="firmas-nombre linear">SRTA. LOURDES CATALINA MALDONADO NOVOA</p>
                            <p class="firmas-nombre">Jefe del Departamento Académico</p>
                            <p class="firmas-nombre">Escuelas de Conducción ANETA</p>
                        </div>
                    </div>
                </div>

                <div class="firmas-recibe">
                    <h2 class="firmas-subtitle">RECIBÍ CONFORME</h2>

                    <figure class="firmas-images--uno">
                       <!--  <img src="../images/firmas/firma3.png" alt="ANETA" class="images-item"/> -->
                    </figure>
                    <div class="firmas-second">
                        <p class="firmas-nombre linear"><asp:Label runat="server" ID="lblFirmaAdministrador"></asp:Label></p>
                        <p class="firmas-nombre">Director (a) de Escuela</p>
                    </div>

                </div>
                <div class="firmas-entrega">
                    <p class="declara">Declaramos bajo el juramento que las firmas y rúbricas anteriormente estampadas
                         en el presente documento son las nuestras propias y auténticas que usamos en todos nuestros actos.</p>
                    <figure class="firmas-images">
                      <!--   <img src="../images/firmas/firma1.png" alt="ANETA" class="images-item"/>
                        <img src="../images/firmas/firma2.png" alt="ANETA" class="images-item"/> -->
                    </figure>
                    <div class="firmas-signs">
                        <div class="firmas-second">
                            <p class="firmas-nombre linear">ING. FABIO ESTEBAN TAMAYO PROAÑO</p>
                            <p class="firmas-nombre">Director General</p>
                            <p class="firmas-nombre">de Escuelas de Conduciión ANETA</p>
                        </div>

                        <div class="firmas-second">
                            <p class="firmas-nombre linear">SRTA. LOURDES CATALINA MALDONADO NOVOA</p>
                            <p class="firmas-nombre">Jefe del Departamento Académico</p>
                            <p class="firmas-nombre">Escuelas de Conducción ANETA</p>
                        </div>
                    </div>

                </div>
            </section>
            <footer class="footer">
                <p class="nota">
                    NOTA: ENVIAR EL ACTA FIRMADA AL CORREO <span>kmaldonado@aneta.org.ec</span>
                </p>
            </footer>
    </div>
    </form>
</body>
</html>
