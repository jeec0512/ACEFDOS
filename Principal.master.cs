using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Principal : System.Web.UI.MasterPage
{
    public Label lblUsuarioEnMaster
    {
        get
        {
            return this.lblusuario;
        }
    }
    #region INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            perfilUsuario();
            //lblusuario.Text = (string)Session["SUsername"] + (string)Session["Sapellidos"] + (string)Session["Snombres"];
           // lblusuario.Text = (string)Session["SnombreCorto"]; // (string)Session["Sapellidos"] + (string)Session["Snombres"];

            lblusuario.Text = (string)Session["Sapellidos"] + (string)Session["Snombres"] ;
            spUsuario.InnerText = (string)Session["Ssucursal"];
            calificarOpciones();

        }    
    }

    #endregion

    protected void perfilUsuario()
    {
        try
        {
            string grupo = (string)Session["SGrupo"];
            string sucursal = (string)Session["SSucursal"];
            if (grupo == ""
               || grupo == null
               || sucursal == ""
               || sucursal == null)
            {
                Response.Redirect("~/ingresar.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];

            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/ingresar.aspx");
            }


            
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/ingresar.aspx");
            lblusuario.Text = e.Message;
            
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

        menu = "INICIO";
        lInicio.Visible = LoginService.calificarOpcion(accion, grupo.Trim(), menu, submenu, boton);

        menu = "CATALOGO";
        lCatalogo.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "INGRESOS";
        lIngresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "EGRESOS";
        lEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "TESORERIA ";
        lTesoreria.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "TRIBUTACION";
        lTributacion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "INFORMES";
        lInformes.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CARTERA";
        lCartera.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "SOCIOS";
        lSocios.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);


        menu = "VALIJA";
        lValija.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        
        menu = "HERRAMIENTAS";
        lHerramientas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "ESCUELA";
        lEscuela.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "SECRETARIAACADEMICA";
        lSecAcademica.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "PSICOTECNICO";
        lPsicotecnico.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "FLOTAVEHICULAR";
        lFlotaVehicular.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CERTIFICADOISO";
        lCertificadoIso.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "HERRAMIENTAS";
        lHerramientas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
    }
    #endregion
}
