<%@ Page Title="" Language="C#" MasterPageFile="~/secretariaAcademica/mpSecretariaAcademica.master" AutoEventWireup="true" 
    CodeFile="reasignacionTitulos.aspx.cs" Inherits="secretariaAcademica_reasignacionTitulos"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
     <link href="../App_Themes/Estilos/estiloFormulario.css" rel="stylesheet" />
    <link href="../App_Themes/Estilos/actaEntregaTitulos.css" rel="stylesheet" />
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <asp:Panel ID="pnActualizacion" runat="server">
        <asp:Label ID="lblMensaje" CssClass="lblFormAviso" runat="server" Text=""></asp:Label>
        <asp:Panel ID="pnAsignacion" runat="server">
            <fieldset id="fsAsignacion">
                <legend>Asignación de títulos</legend>
                <asp:TextBox ID="txtVeh_id" CssClass="txtForm" runat="server" Visible="false"></asp:TextBox>
                <asp:Label ID="lblSucursal" CssClass="lblForm" runat="server" Text="Sucursal" Visible="true"></asp:Label>

                <asp:Panel ID="pnSucursal" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlSucursal" DataTextField="nom_suc" DataValueField="sucursal" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged2">
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Label ID="lblModalidad" CssClass="lblForm" runat="server" Text="Modalidad"></asp:Label>
                <asp:Panel ID="pnModalidad" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlModalidad" DataTextField="mod_descripcion" DataValueField="mod_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlModalidad_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblCurso" CssClass="lblForm" runat="server" Text="Curso" Visible="true"></asp:Label>
                <asp:Panel ID="pnCurso" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlCurso" DataTextField="cur_nomeNclatura" DataValueField="cur_id" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Label ID="lblPedido" CssClass="lblForm" runat="server" Text="Pedidos" Visible="true"></asp:Label>
                <asp:Panel ID="pnPedido" runat="server" CssClass="pnFormDdl" Visible="true">
                    <asp:DropDownList ID="ddlPedido" DataTextField="numpedido" DataValueField="numpedido" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlPedido_SelectedIndexChanged">
                    </asp:DropDownList>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
            <fieldset id="Fieldset2">

                
                <asp:Panel ID="Panel5" runat="server" CssClass="pnFormDdl">
                    <asp:Label ID="lblObservacion" CssClass="" runat="server" Text="Observación" Visible="true"></asp:Label>
                    <asp:TextBox ID="txtObservacion" CssClass="" runat="server" Visible="true" Enabled="false"></asp:TextBox>
                </asp:Panel>
               


                <asp:Panel ID="pnBotonera" runat="server" CssClass="pnFormBotonera">
                    <asp:Button ID="btnGuardar" runat="server" Text="Rasignar número de títulos" CssClass="btnForm"
                        OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnActa" runat="server" Text="Imprimir acta" CssClass="btnForm" OnClick="btnActa_Click" visible="false"/>
                    <asp:Button ID="btnTitulo" runat="server" Text="Imprimir listado de títulos" CssClass="btnForm" OnClick="btnTitulo_Click" visible="false"/>
                    <asp:HyperLink ID="blRegresar" runat="server" Text="Regresar" NavigateUrl="~/catalogo/inicioCatalogo.aspx"
                        Font-Size="Larger" Font-Bold="true"></asp:HyperLink>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </asp:Panel>




    <asp:Panel ID="Panel1" runat="server">
        <fieldset id="Fieldset1">
            <legend>Listado de alumnos aptos para el pedido de títulos</legend>


            <asp:Panel ID="pnAutoDetalle" CssClass="pnPeq" runat="server" Visible="true" BorderStyle="Double">
                <asp:GridView ID="grvCursoDetalle" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical" HorizontalAlign="Center"
                    Width="90%"
                    AllowSorting="True" PageSize="10" OnRowCommand="grvCursoDetalle_RowCommand"
                    OnRowDataBound="grvCursoDetalle_RowDataBound" CssClass="Rojo">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="RNOTC_id" HeaderText="Código" Visible="true"
                            ItemStyle-CssClass="DisplayNone" HeaderStyle-CssClass="DisplayNone">
                            <HeaderStyle CssClass="DisplayNone" />
                            <ItemStyle CssClass="DisplayNone" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REG_SUCURSAL" HeaderText="REG_SUCURSAL" Visible="true" />
                        <asp:BoundField DataField="CUR_NOMENCLATURA" HeaderText="CUR_NOMENCLATURA" Visible="true" />
                        <asp:BoundField DataField="CUR_FECHA_INICIO" HeaderText="CUR_FECHA_INICIO" Visible="true" />
                        <asp:BoundField DataField="CUR_FECHA_FIN" HeaderText="CUR_FECHA_FIN" Visible="true" />
                        <asp:BoundField DataField="NO" HeaderText="NO" Visible="true" />
                        <asp:BoundField DataField="ALUMNO" HeaderText="ALUMNO" Visible="true" />
                        <asp:BoundField DataField="REG_FACTURANUMERO" HeaderText="REG_FACTURANUMERO" Visible="true" />
                        <asp:BoundField DataField="MATRICULA" HeaderText="MATRICULA" Visible="true" />
                        <asp:BoundField DataField="PERMISO" HeaderText="PERMISO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_NOTA" HeaderText="RNOTC_EDUC_VIAL_NOTA" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP1" HeaderText="RNOTC_EDUC_VIAL_SUP1" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_SUP2" HeaderText="RNOTC_EDUC_VIAL_SUP2" Visible="true" />
                        <asp:BoundField DataField="RNOTC_EDUC_VIAL_ASIS" HeaderText="RNOTC_EDUC_VIAL_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_MEC_ASIS" HeaderText="RNOTC_MEC_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PAUX_ASIS" HeaderText="RNOTC_PAUX_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PSIC_ASIS" HeaderText="RNOTC_PSIC_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_NOTA" HeaderText="RNOTC_PRAC_NOTA" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP1" HeaderText="RNOTC_PRAC_SUP1" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_SUP2" HeaderText="RNOTC_PRAC_SUP2" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PRAC_ASIS" HeaderText="RNOTC_PRAC_ASIS" Visible="true" />
                        <asp:BoundField DataField="RNOTC_APROBADO" HeaderText="RNOTC_APROBADO" Visible="true" />
                        <asp:BoundField DataField="TITULO" HeaderText="TITULO" Visible="true" />
                        <asp:BoundField DataField="RNOTC_OBSERVACIONES" HeaderText="RNOTC_OBSERVACIONES" Visible="true" />
                        <asp:BoundField DataField="RNOTC_PEDIDO_TITULOS" HeaderText="RNOTC_PEDIDO_TITULOS" Visible="true" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
                        Font-Strikeout="False" />
                    <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>

            </asp:Panel>

        </fieldset>

        <asp:panel id="Reporte" visible="false" runat="server">
            <input type="button" onclick="DescargarPDF('Reporte', 'ReporteASP')" value="Decargar Reporte" />
            <div class="main">
                <header class="title" style="margin-top: 4rem; margin-bottom: 2rem; height: 8rem; display: grid; grid-template-columns: 1fr 6fr 1fr; justify-content: space-evenly;">
                    <figure class="title-img">
                        <img src="../images/iconos/icoaneta2.png" alt="ANETA">
                    </figure>
                    <h1 class="title-titulo">ACTA ENTREGA RECEPCION DE TÍTULOS DE CONDUCTOR NO PROFESIONAL</h1>
                    <span class="title-acta">No. 23484</span>
                </header>
                <section class="entrega">
                    <article class="article">
                        <p class="paragraf">
                            En la ciudad de Quito a los 
				<span>30</span> días del mes de 
				<span>ENERO</span> del año 
				<span>2019</span>
                            , el 
				<span>Ing. Fabio Esteban Tamayo Proaño,</span> Director General de Escuelas de Conducción 
				<span>ANETA,</span> y el Señor (a) 
				<span>Srta. Lourdes Catalina Maldonado Novoa</span> hacen entrega al señor (a) 
				<span>WILSON RIOS</span> para la sucursal de 
				<span>ANETA,</span>
                            <span>CAYAMBE,</span> de 
				<span>7 TITULOS DE CONDUCTOR NO PROFESIONAL</span> del 
				<span>No.6157 al No.6163,</span>
                            quien los recibe a su entera y absoluta conformidad.
                        </p>

                        <p class="paragraf">
                            Con la firmade esta acta, el Señor (a) 
				<span>WILSON RIOS,</span>
                            asume la responsabilidad del custodio, buen uso y emisión de los mencionado títulos de conductor no profesional, cumpliendo con todos los requisitos y procedimientos establecidos por ANETA.
                        </p>

                        <p class="paragraf">Declaro bajo juramento y acepto libre y voluntariamente someterme a las acciones legales a que hubiere lugar por el uso indebido o la emisión incorrecta de los referidos documentos.</p>
                    </article>
                </section>
                <section class="firmas">
                    <div class="firmas-entrega">
                        <h2 class="firmas-subtitle">ENTREGAMOS CONFORME</h2>

                        <figure class="firmas-images">
                            <img src="../images/firmas/firma1.png" alt="ANETA" class="images-item">
                            <img src="../images/firmas/firma2.png" alt="ANETA" class="images-item">
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
                            <img src="../images/firmas/firma3.png" alt="ANETA" class="images-item">
                        </figure>
                        <div class="firmas-second">
                            <p class="firmas-nombre linear">WILSON RIOS</p>
                            <p class="firmas-nombre">Director (a) de Escuela</p>
                        </div>

                    </div>
                    <div class="firmas-entrega">
                        <p class="declara">Declaramos bajo el juramento que las firmas y rúbricas anteriormente estampadas en el presente documento son las nuestras propias y auténticas que usamos en todos nuestros actos.</p>
                        <figure class="firmas-images">
                            <img src="../images/firmas/firma1.png" alt="ANETA" class="images-item">
                            <img src="../images/firmas/firma2.png" alt="ANETA" class="images-item">
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



        </asp:panel>

    </asp:Panel>



    <script src="../js/jQuery%20v3.4.0.js"></script>
    <script src="../js/jspdf.debug.js"></script>
    <script src="../js/funciones.js"></script>
    <script>
        function DescargarPDF(ContenidoID, nombre) {
            var pdf = new jsPDF('p', 'pt', 'letter');
            html = $('#' + ContenidoID).html();
            specialElementHandlers = {};
            margins = { top: 10, bottom: 20, left: 20, width: 522 };
            margin_top = 4;
            margin_bottom = 2;
            height = 8;



            pdf.fromHTML(html, margins.left, margins.top, {
                'width': margins.width

            }, function (dispose) { pdf.save(nombre + '.pdf'); }, margins);
        }

    </script>
</asp:Content>

