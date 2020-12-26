using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Psicotecnico_cambioFechaPsico : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion


    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int userId = Convert.ToInt32(Session["SUsuarioID"]);
            string accion = string.Empty;
            string facturacion = " http://www.aneta.org.ec:9095/escuelaweb/petrinovic/modifica.an?prmusrid=" + userId;

            perfilUsuario();
            activarObjetos();
            ifFacturacion.Attributes["src"] = facturacion;

        }
    }
    #endregion

    #region PROCESOS INTERNOS

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


            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }

    protected void activarObjetos()
    {
        pnTitulos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    #endregion



    protected void btnNotas_Click(object sender, EventArgs e)
    {
        string usuario, Username, sUrl, sScript;

        usuario = Convert.ToString(Session["SUsuarioID"]);
        int userId = Convert.ToInt32(Session["SUsuarioID"]);

        if (usuario == "" || usuario == null)
            Response.Redirect("~/ingresar.aspx");
        Username = (string)Session["SUsername"];

        sUrl = "  http://www.aneta.org.ec:9095/escuelaweb/petrinovic/modifica.an?prmusrid={*usuarios}";

        string facturacion = "http://www.aneta.org.ec:9095/escuelaweb/petrinovic/modifica.an?prmusrid=" + userId;
        Response.Redirect(facturacion, false);
    }
}