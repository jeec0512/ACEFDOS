using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cartera_mpCartera : System.Web.UI.MasterPage
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

        menu = "CARTERA";
        submenu = "ESTADOCLIENTE";
        lEstadoCliente.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
        submenu = "CARTERAINGRESOS";
        lCarteraIngresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "ESTADOPROVEEDOR";
        lEstadoProveedor.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "RETENCIONPROVEEDOR";
        lRetencionProveedor.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

    }
    #endregion  
}
