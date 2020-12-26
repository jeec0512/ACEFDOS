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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Escuela_recuperacionPuntos : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["AdmBitaAutoConnectionString"].ConnectionString;
    Data_AdmBitaAutoDataContext da = new Data_AdmBitaAutoDataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();
            activarObjetos();
            listarCurso();
            listarMateria();
        }
    }
    #endregion

    #region PERFIL USUARIO

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

    #endregion

    #region METODOS GENERALES
    protected void activarObjetos()
    {
        lblMensaje.Text = string.Empty;
    }

    protected void listarCurso()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("MODALIDAD", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }

    protected void listarMateria()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string accion = string.Empty;
        if (modalidad == 5 || modalidad == 6)
        {
            accion = "RECUPERA";
        }
        else
        {
            accion = "CURSO";
        }

        var cMateria = ds.sp_abmMateria(accion, 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione Materia", "-1");

        ddlMateria.Items.Insert(0, listCon);

    }

    protected void listarModalidad()
    {
        ddlModalidad.SelectedValue = "-1";

    }
    #endregion

    #region CAMBIOS DDL
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarModalidad();
        listarCurso();
        listarMateria();
        
        ddlMateria_SelectedIndexChanged();
        activarObjetos();
    }
    protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria_SelectedIndexChanged();
    }


    protected void ddlMateria_SelectedIndexChanged()
    {
        string modalidad = ddlModalidad.SelectedValue;
        string materia = ddlMateria.SelectedValue;
       
       /* 

        listarAula();
        listarHorario();
        listarAuto();
        listarTaller();
        verAsignaciones();*/
    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarCurso();
        listarMateria();
   
        ddlMateria_SelectedIndexChanged();
        activarObjetos();
    }
    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarMateria();

        ddlMateria_SelectedIndexChanged();
        activarObjetos();
    }
    #endregion  
    protected void btnImpRecupInic_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string acta = string.Empty;
        string alterno = string.Empty;

        string accion = "ACTUALIZAR";
        string sucursal = ddlSucursal.Text;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        
        /*******************************************************/
        string caminoLogo = "imprimirRehabilitacionVial.aspx";
        StringBuilder emailHtml = new StringBuilder(File.ReadAllText(Server.MapPath(caminoLogo)));



        emailHtml.Replace("NUMCONVENIO", "5");


        /******************************************************/

        Session["pSucursal"] = sucursal;
        Session["pCur_id"] = cur_id;
        

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open(emailHtml,'','width=800,height=500') </script>");




        lblMensaje.Text = "";
    }
    protected void btnImpRecupFinal_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string acta = string.Empty;
        string alterno = string.Empty;

        string accion = "ACTUALIZAR";
        string sucursal = ddlSucursal.Text;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);



        Session["pSucursal"] = sucursal;
        Session["pCur_id"] = cur_id;


        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirRehabilitacionVialFinal.aspx','','width=800,height=500') </script>");




        lblMensaje.Text = "";
    }
}