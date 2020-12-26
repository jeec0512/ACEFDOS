using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Socio_mpSocio : System.Web.UI.MasterPage
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

        menu = "SOCIOS";
        submenu = "INGCONTRATO";
        lIngContrato.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "MODCONTRATO";
        lModContrato.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "AUTOSOC";
        lAutosSoc.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "FACTURADOSOC";
        lFacturadosSoc.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "TABLAMEMB";
        lTablamembresias.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "ACTIVOSOC";
        lActivosSoc.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "ESTADOSOC";
        lEstadoSoc.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "LISTACONTRATOS";
        lListaContratos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "ENVIOCONTRATOS";
        lEnvioContratos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "SOCIOSINACTIVOS";
        lSociosInactivos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        


    }
    #endregion  
}
