using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Contabilidad_mpContabilidad : System.Web.UI.MasterPage
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

        menu = "CONTABILIDAD";
        submenu = "CNTINGRESOS";
        lCntIngresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "CNTEGRESOS";
        lCntEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "LIBRODIARIO";
        lLibroDiario.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "LIBROMAYOR";
        lLibroMayor.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "MAYORIZAR";
        lMayorizar.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "AUTORIZARET";
        lAutorizarRetencion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
        menu = "CONTABILIDAD";
        submenu = "AWACOUNTING";
        lAwaContabilidad.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "APETURAPERIODO";
        lAperturaPeriodo.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CONTABILIDAD";
        submenu = "CONTABILIZACION";
        lContabilizacion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        

    }

    #endregion 
}
