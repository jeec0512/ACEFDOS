<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imprimirActaGrado.aspx.cs" Inherits="Escuela_imprimirActaGrado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Acta de grado</title>
    <link href="../App_Themes/Estilos/normalize.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos/actaGrado.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Header -->
            <header class="main-header">
                <div class="l-container main-header__block">
                    <img src="../images/iconos/icoaneta2.png" alt="logo ANETA" class="main-logo" />

                    <div class="center-content">
                        <h2>AUTOMOVIL CLUB DEL ECUADOR</h2>
                        <h3>ESCUELA DE CONDUCCIÓN</h3>
                        <h4>ACTA DE GRADO</h4>
                    </div>
                </div>
            </header>

            <main>
              <!-- Descripción -->
              <section class="l-section l-container">
                <div class="l-100 center-block left-content"> 
                    <p class="l-100 center-block left-content">En la ciudad de <span id="spanCiudad" runat="server">CIUDAD</span>,
                         Provincia de <span id="spanProvincia" runat="server">PROVINCIA</span> a los <span id="spanDias" runat="server">DIAS</span> días del mes de
                         <span id="spanMes" runat="server">MES</span> del año <span id="spanAno" runat="server">ANO</span>,
                         en la Escuela de Conducción de ANETA, ante los miembros de la institución integrada por
                         <span id="spanDirectorEscuela1" runat="server">DIRECTORESCUELA</span> Director(a) de
                         <span id="spanEscuela" runat="server">ESCUELA</span>,
                         <span id="spanSupervisor" runat="server">SUPERVISOR</span> Supervisor Instructores,
                         <span id="spanSecretariaAcademica1" runat="server">SECRETARIAACDEMICA</span>, Secretario(a) Académico(a). </p>
                </div>
                <div class="l-100 center-block left-content"> 
                    <p class="l-100 center-block left-content" >Compareció el (la) Señor(a)(ita) <span id="spanEstudiante" runat="server">ESTUDIANTE</span> a rendir sus pruebas teóricas y prácticas, obteniendo los siguientes resultados: </p>
                </div>
                <div class="l-100 center-block center-content"> 
                    <p class="l-100 center-block left-content">1.Teórico de Educación Vial <span  id="spanNotaTeoria" runat="server">NOTATEORIA</span></p>
                    <p class="l-100 center-block left-content">EQUIVALENTE A <span id="spanEquivalenteteoria" runat="server">EQUIVALENTETEORIA</span></p>
                    <p class="l-100 center-block left-content">2.Práctica de conducción <span id="spanNotaPractica" runat="server">NOTAPRACTICA</span></p>
                    <p class="l-100 center-block left-content">EQUIVALENTE A <span id="spanEquivalentePractica" runat="server">EQUIVALENTEPRACTICA</span></p>
                </div>
                <div class="l-100  center-block left-content"> 
                    <p class="l-100 center-block left-content">En virtud de la aprobación, la Escuela de Conducción le confirió el Certificado de: </p>
                </div>
                <div class="l-100 center-block left-content"> 
                    <p class="l-100 center-block left-content">CONDUCTOR NO PROFESIONAL </p>
                </div>
                <div class="l-100 center-block left-content"> 
                    <p class="l-100 center-block left-content">N°. <span id="spanTitulo" runat="server">TITULO</span> </p>
                </div>
                <div class="l-100 center-block left-content"> 
                    <p class="l-100 center-block left-content">Para constancia de lo actuado, la autoridad y los examinadores en unidad de acto presente Acta de acuerdo a lo dispuesto en el Artículo 39 del Reglamento de escuelas de Capacitación de Conductores No profesionales, juntamente con la secretaria titular, que dá fé y certifica.</p>
                </div>
                <div class="l-100 center-block left-content"> 
                    <p class="l-100 center-block left-content">Curso N° <span id="spanCurso" runat="server">CURSO</span></p>
                </div>




              </section>

              <!-- Eventos -->
               <section class="l-container ">

                    <div class="l-100">
                      <div> 
                    
                            
                           
                                        <div class="l-100 ">

                            <div class="card cards-grid">

                                <div class="card__content">
                                    <h4 class="card__title">DIRECTOR DE ESCUELA DE CONDUCCIÓN
                                    </h4>
                                    <div class="card__subtitle">
                                        <span  id="spanDirectorEscuela2" runat="server">DIRECTORESCUELA</span>
                                    </div>

                                </div>

                                <div class="card__content">
                                    <h4 class="card__title">SUPERVISOR DE CURSOS DE TEORÍA DE ESCUELA
                                    </h4>
                                    <div class="card__subtitle">
                                        <span id="spanSupervisorTeoria" runat="server">SUPERVISORTEORIA</span>
                                    </div>
                                </div>
                            </div>

                            <div class="card cards-grid" >
                                <div class="card__content">
                                    <h4 class="card__title">SUPERVISOR DE CLASES PRÁCTICAS DE CONDUCCIÓN
                                    </h4>
                                    <div class="card__subtitle">
                                        <span id="spanSupervisorPractica" runat="server">SUPERVISORPRACTICA</span>
                                    </div>

                                </div>

                                <div class="card__content">
                                    <h4 class="card__title">SECRETARÍA ACADÉMICA
                                    </h4>
                                    <div class="card__subtitle">
                                        <span id="spanSecretariaAcademica2" runat="server">SECRETARIAACADEMICA</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                        </div>
                </section>

            </main>
            <!-- Footer -->
            <footer class="main-footer">
            </footer>
            <nav class="s-100 left-content fin">
                                <div class="posData">
                                    <img runat="server" id="imgCtrl" height="150" width="150" />
                                </div>
                            </nav>

        </div>
    </form>
</body>
</html>
