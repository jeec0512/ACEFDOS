<%@ Page Language="C#" AutoEventWireup="true" CodeFile="impresionPedidoTitulos.aspx.cs" Inherits="Escuela_impresionPeditoTitulos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../App_Themes/Estilos/anexo4.css" rel="stylesheet"  />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header class="header-main">
                <div class="header-img">
                    <figure>
                        <img class="imagenAnt" src="../images/iconos/comNac.jpg" />
                    </figure>
                </div>
                <div class="header-det">
                    <div class="header-det">
                        <asp:Label ID="lblTitulo" runat="server" Text="COMISIÓN NACIONAL DE TRANSPORTE TERRESTRE, TRANSITO Y SEGURIDAD VIAL"></asp:Label>
                    </div>

                    <div class="header-det">
                        <asp:Label ID="lblSubtitulo" runat="server" Text="UNIDAD DE ESCUELAS DE CAPACITACIÓN"></asp:Label>
                    </div>
                    <div class="header-det">
                        <asp:Label ID="Label1" runat="server" Text="ESCUELAS DE CAPACITACIÓN DE CONDUCTORES NO PROFESIONALES"></asp:Label>
                    </div>

                    <div class="header-det">
                        <asp:Label ID="Label2" runat="server" Text="VERIFICACIÓN DEL CUMPLIMIENTO DE REQUISITOS DE LOS ALUMNOS PARA EL OTORGAMIENTO DEL TITULO DE CONDUCTOR NO PROFESIONAL - ANEXO 4"></asp:Label>
                    </div>
                </div>
                <div class="header-img">
                    <figure>
                        <img class="imagenAneta" src="../images/iconos/anetatitulo2.jpg" />
                    </figure>
                </div>

            </header>
            <nav>
                <!-- ESCUELA-->
                <div class="escuela">
                    <asp:Label ID="lblEscuela" runat="server" Text="Nombre de la Escuela"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEscuela"></asp:TextBox>
                </div>
                <!-- CURSO-->
                <div class="curso">
                    <asp:Label ID="lblCurso" runat="server" Text="Curso"></asp:Label>
                    <asp:TextBox runat="server" ID="txtCurso"></asp:TextBox>

                    <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha Inicio Curso:"></asp:Label>
                    <asp:TextBox runat="server" ID="txtFechaInicio"></asp:TextBox>

                    <asp:Label ID="lblFechaFin" runat="server" Text="Fecha Fin Curso:"></asp:Label>
                    <asp:TextBox runat="server" ID="txtFechaFin"></asp:TextBox>
                </div>
                <!-- HORARIO -->
                <div class="horario">
                    <asp:Label ID="lblHorario" runat="server" Text="Horario:"></asp:Label>
                    <asp:TextBox runat="server" ID="txtHorario"></asp:TextBox>
                </div>


            </nav>
            <div>
                <asp:Panel ID="Panel1" runat="server">
                    <!--<div class="titulo-main">
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo">
                            CONTROL DE CALIFICACIONES Y ASISTENCIA
                            <div class="calificacion">
                                <div class="item-materia">
                                    Educación Vial
                                </div>
                                <div class="item-materia">
                                    Mecánica Básica
                                </div>
                                <div class="item-materia">
                                    Primeros Auxilios
                                </div>
                                <div class="item-materia">
                                    Actitud Psicológca
                                </div>
                                <div class="item-materia">
                                    Práctica
                                </div>
                            </div>
                        </div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                        <div class="item-titulo"></div>
                    </div> 
                        -->
                    <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" 
                        AllowSorting="True" PageSize="100" CssClass="grilla ">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:BoundField DataField="RNOTC_id" Visible="false"
                                ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                                <HeaderStyle CssClass="DisplayNone" />
                                <ItemStyle CssClass="DisplayNone" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NO" Visible="true" headertext= "No"/>
                            <asp:BoundField DataField="ALUMNO" Visible="true" headertext= "Apellidos y Nombres"/>
                            <asp:BoundField DataField="RNOTC_CIRUC" Visible="true" headertext= "Cédula"/>
                            <asp:BoundField DataField="REG_FACTURANUMERO" Visible="true" headertext= "No Factura"/>

                            <asp:BoundField DataField="MATRICULA" Visible="true" headertext= "Matrícula"/>
                            <asp:BoundField DataField="PERMISO" Visible="true" headertext= "Permiso" />
                            <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" Visible="true" headertext= "EduccionVíal Not1"/>
                            <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" Visible="true" headertext= "Sup1"/>
                            <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" Visible="true" headertext= "Sup2"/>
                            <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" Visible="true" headertext= "Asis"/>
                             <asp:BoundField DataField="MEC_NOTA" Visible="true" headertext= "Mecánica Not1"/>
                            <asp:BoundField DataField="MEC_SUP1" Visible="true" headertext= "Sup1"/>
                            <asp:BoundField DataField="MEC_SUP2" Visible="true" headertext= "Sup2"/>
                            <asp:BoundField DataField="RNOTC_MEC_ASIS" Visible="true" headertext= "Asis"/>
                            <asp:BoundField DataField="PAUX_NOTA" Visible="true" headertext= "PrimAuxilios Not1"/>
                            <asp:BoundField DataField="PAUX_SUP1" Visible="true" headertext= "Sup1"/>
                            <asp:BoundField DataField="PAUX_SUP2" Visible="true" headertext= "Sup2"/>
                            <asp:BoundField DataField="RNOTC_PAUX_ASIS" Visible="true" headertext= "Asis"/>
                             <asp:BoundField DataField="PSIC_NOTA" Visible="true" headertext= "Psicología Not1"/>
                            <asp:BoundField DataField="PSIC_SUP1" Visible="true" headertext= "Sup1"/>
                            <asp:BoundField DataField="PSIC_SUP2" Visible="true" headertext= "Sup2"/>
                            <asp:BoundField DataField="RNOTC_PSIC_ASIS" Visible="true" headertext= "Asis"/>
                            <asp:BoundField DataField="RNOTC_PRAC_NOTA" Visible="true" headertext= "Práctica Not1"/>
                            <asp:BoundField DataField="RNOTC_PRAC_SUP1" Visible="true" headertext= "Sup1"/>
                            <asp:BoundField DataField="RNOTC_PRAC_SUP2" Visible="true" headertext= "Sup2"/>
                            <asp:BoundField DataField="RNOTC_PRAC_ASIS" Visible="true" headertext= "Asis"/>
                            <asp:BoundField DataField="RNOTC_APROBADO" Visible="true" headertext= "Aprobado"/>
                            <asp:BoundField DataField="TITULO" Visible="true" headertext= "No Título"/>
                            <asp:BoundField DataField="RNOTC_OBSERVACIONES" Visible="true" headertext= "Observ."/>
                            <asp:BoundField DataField="REG_SUCURSAL" Visible="false" headertext= "No"/>
                            <asp:BoundField DataField="CUR_NOMENCLATURA" Visible="false" headertext= "No"/>
                            <asp:BoundField DataField="CUR_FECHA_INICIO" Visible="false" headertext= "No"/>
                            <asp:BoundField DataField="CUR_FECHA_FIN" Visible="false" headertext= "No"/>
                            <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" Visible="false" headertext= "No"/>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <footer>
            </footer>

        </div>
    </form>
</body>
</html>
