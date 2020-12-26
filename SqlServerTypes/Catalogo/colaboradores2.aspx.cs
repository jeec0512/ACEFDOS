using AjaxControlToolkit;
using enviarEmail;
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
using acefdos;
public partial class Catalogo_colaboradores2 : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["temporalraceConnectionString"].ConnectionString;


    Data_TemporalRaceDataContext dt = new Data_TemporalRaceDataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["COMISIONESConnectionString"].ConnectionString;

    DataComisionesDataContext ds = new DataComisionesDataContext();

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString;
    
    Data_EscuelaDataContext de = new Data_EscuelaDataContext();
   


    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            listarColaboradores();

        }
    }

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


            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void listarColaboradores()
    {
        //var cColaborador = dc.sp_abmColaborador("CONSULTAR", "", "", "", "", "", false, "", 0, "", "", DateTime.Now, DateTime.Now, 0, 0, 0, "");

        grvColaboradores.DataSource = colaboradorCapaDatos.getAllColaboradores();
        grvColaboradores.DataBind();
    }

    #endregion
}