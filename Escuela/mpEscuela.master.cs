using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Escuela_mpEscuela : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            calificarOpciones();
        }
    }

    #region CALIFICAR OPCIONES
    protected void calificarOpciones()
    {
        /*CALIFICAR OPCIONES*/
        string accion, grupo, menu, submenu, boton;

        submenu = string.Empty;
        boton = string.Empty;



        accion = string.Empty;
        grupo = (string)Session["Sgrupo"];

        menu = "ESCUELA";
            submenu = "EVAANT2019";
            lEvaAnt2019.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "CURSOSFACTURADOS";
            lCursosFacturados.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "EVAANT2019";
            lCreacionDeCupos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "CREACIONDECUPOS";
            lCreacionDeCupos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "INSCRIPCIONESTUDIANTE";
            lInscripcionEstudiante.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                
            submenu = "PENSUMACADEMICO";
            lPensumAcademico.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "CTRLESTUDIANTIL";
            lCtrlEstudiantil.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "POSTERGACION";
            lPostergacion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "GENERACIONPEDIDOTITULOS";
            lGeneracionPedidotitulos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);



            submenu = "IMPRESIONTITULOS";
            lImpresionTitulos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "NOTIFICACIONESTUDIANTE";
            lNotificacionestudiante.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            //submenu = "REASIGNACIONTITULOS";
            //lReasignacionTitulos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "IMPRESON2";
            lImpresion2.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "RECUPERACIONPUNTOS";
            lrecuperacionPuntos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "CERTIFICADOSICOPRACTICO";
            lCertificadoSicopractico.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "REPORTECERTIFICADOSICOPRACTICO";
            lReporteCertificadoSicopractico.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "VERIFICARCERTIFICADOSICOPRACTICO";
            lVerificarCertificadoSicopractico.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "DISPONIBILIDADHORARIOS";
            lDisponibilidadHorarios.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "REPORTENOTAS";
            lReporteNotas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            //submenu = "EVAANT2019  ";
            //lInformacionAlumno.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
			
			submenu = "ANEXO3";
            lAnexo3.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
			
			submenu = "LISTADOESTUDIANTES";
            lListasEstudiante.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
			
			submenu = "IMPRIMIRACTAS";
			lImprimirActas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "ANULARPEDIDOPERMISOS";
            lAnularPermisos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "ANULARPEDIDOTITULOS";
            lAnularTitulos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
    }
	
    #endregion 

}
