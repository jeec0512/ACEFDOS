using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Egreso_mpEgreso : System.Web.UI.MasterPage
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

        menu = "EGRESOS";
       
        submenu = "RETENCION";
        lRetencion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "VERRET";
        lVerRet.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "REPORTERETENCION";
        lReportesRetenciones.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);


        submenu = "REGEGRESOS";
        lRegEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);


        submenu = "AUTOCONSUMO";
        lAautoconsumo.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "CIERREEGRESO";
        lCierreEgreso.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "CONTROLCAJA";
        lControlCaja.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "CONTROLAUTO";
        lControlAuto.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
        submenu = "ASIENTOEGRESO";
        lAsientoEgreso.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "ASIENTOAUTOCONSUMO";
        lAsientoAutoconsumo.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "REPORTEEGRESOS";
        lReporteEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "RESUMENEGRESO";
        lResumenEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
    }
    #endregion  
}
