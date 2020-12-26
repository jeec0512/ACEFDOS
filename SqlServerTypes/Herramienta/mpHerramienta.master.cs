using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Herramienta_mpHerramienta : System.Web.UI.MasterPage
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

                menu = "HERRAMIENTAS";
                submenu = "FACTURA";
                lFactura.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "RETENCION";
                lRetencion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "ACTIVARING";
                lActivarIngresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "ACTIVAREGR";
                lActivarEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "CTRLING";
                lControlCierreIngresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "CTRLEGR";
                lControlCierreEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "SRI";
                lSRI.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                


    }
    #endregion  
}
