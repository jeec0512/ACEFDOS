using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ingreso_mpIngreso : System.Web.UI.MasterPage
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

                menu = "INGRESOS";
                submenu = "FACTURA";
                lFactura.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "LISTARFACTURA";
                lListarFacuras.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "FACTXGRUPOS";
                lFacturadosxGrupos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "RESUMENFAC";
                 lResumenFacturacion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                submenu = "FACANULADAS";
                lFacturasAnuladas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
                


    }
    #endregion  

        
       
            
            
}
