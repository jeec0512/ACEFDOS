using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tesoreria_mpTesoreria : System.Web.UI.MasterPage
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

        menu = "TESORERIA";
        submenu = "REGISTRARECAUDACION";
        lRecaudacion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "CIERREVENTAS";
        lCierreVentas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "CTRLVENTAS";
        lControlVentas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "REPTESORERIA";
        lReporteTesoreria.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "CORRIENTES";
        lCorrientes.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "INGRESOSEGRESOS";
        lIngresosEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);


    }
    #endregion  

}
