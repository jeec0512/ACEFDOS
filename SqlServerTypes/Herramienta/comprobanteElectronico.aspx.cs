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

public partial class Herramienta_comprobanteElectronico : System.Web.UI.Page
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

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

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
            /*
            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();*/
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string usuario, Username, sUrl, sScript;
        usuario = Convert.ToString(Session["SUsuarioID"]);
        if (usuario == "" || usuario == null)
            Response.Redirect("~/ingresar.aspx");

        //Username = Convert.ToString(Session["SUsuarioname"]).Trim();
        
        Username = (string)Session["SUsername"];

        sUrl = "http://aneta.org.ec:9094/AnetaFacturacionEsc/site/home.jsf?prmusrid=" + Username;
        sScript = "<script language =javascript> ";

        sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=800,height=500,left=100,top=100');";
        sScript += "</script> ";

        Response.Write(sScript);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //Response.Redirect("190.63.17.119:8087/Reteu");
        string usuario, Username, sUrl, sScript;
        usuario = Convert.ToString(Session["SUsuarioID"]);
        if (usuario == "" || usuario == null)
            Response.Redirect("~/ingresar.aspx");

        Username = (string)Session["SUsername"];

        sUrl = "http://aneta.org.ec:8087/Reteu";
        sScript = "<script language =javascript> ";

        sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=800,height=500,left=100,top=100');";
        sScript += "</script> ";

        Response.Write(sScript);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //Response.Redirect("190.63.17.119:8087/Reteu");
        string usuario, Username, sUrl, sScript;
        usuario = Convert.ToString(Session["SUsuarioID"]);
        if (usuario == "" || usuario == null)
            Response.Redirect("~/ingresar.aspx");

        Username = (string)Session["SUsername"];

        //sUrl = "http://aneta.org.ec:9094/AnetaComprobantesRet";
        sUrl = "http://aneta.org.ec:9094/AnetaComprobantesRet/site/home.jsf";

        sScript = "<script language =javascript> ";

        sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=800,height=500,left=100,top=100');";
        sScript += "</script> ";

        Response.Write(sScript);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //Response.Redirect("190.63.17.119:8087/Reteu");
        string usuario, Username, sUrl, sScript;
        usuario = Convert.ToString(Session["SUsuarioID"]);
        if (usuario == "" || usuario == null)
            Response.Redirect("~/ingresar.aspx");

        Username = (string)Session["SUsername"];

        //sUrl = "http://aneta.org.ec:8087/Ncreu";

        sUrl = "http://aneta.org.ec:9094/AnetaComprobantesNc/site/home.jsf";

        sScript = "<script language =javascript> ";

        sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=800,height=500,left=100,top=100');";
        sScript += "</script> ";

        Response.Write(sScript);
    }
}