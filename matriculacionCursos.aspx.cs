using AjaxControlToolkit;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class matriculacionCursos : System.Web.UI.Page
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

            grvAutos.DataSource = ds.sp_abmCursoMatricula("TODOS", 0, 0, DateTime.Now, DateTime.Now, 0, DateTime.Now, 0);
            grvAutos.DataBind();
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

    #region LISTAR
    

 

    
    
    #endregion

    #region SELECTED
    

    
    


    #endregion

    #region GRILLAS
    protected void grvAutos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutos.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[1].Text);

            abilitarObjetos();
            ibConsultar_Click(lidCurso);
            ddlCurso.Enabled = false;

        }
    }


    protected void grvAutos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarObjetos();
        //listarAutos();
    }

    protected void ddlEscuela2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarObjetos();
        //listarAutos();
    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ibConsultar_Click(int id)
    {
        string Accion = "CONSULTAR";

        string sucursal = ddlSucursal.SelectedValue;
        txtId.Text = Convert.ToString(id);
         

        var cMatric = ds.sp_abmCursoMatricula(Accion,id,0,DateTime.Now,DateTime.Now,0,DateTime.Now,0);

        foreach (var registro in cMatric)
        {
            lblMensaje.Text = string.Empty;
            ddlCurso.SelectedValue = Convert.ToString(registro.CUR_ID);
            txtFechaIni.Text = Convert.ToString(registro.FECHA_INICIO_MATRICULA);
            txtFechaFin.Text = Convert.ToString(registro.FECHA_FIN_MATRICULA);
        }


        txtFechaIni.Focus();
    }
    #endregion

    #region MATENIMIENTO

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensaje.Visible = true;
            int id = 0;
            string hacer = lblNuevo.Text.Trim();
            string Accion = string.Empty;
            if (hacer == "N")
            {
                Accion = "AGREGAR";
                id = 0;
            }
            else
            {
                Accion = "MODIFICAR";
                id = Convert.ToInt32(txtId.Text);
            }
            int usuario = Convert.ToInt32(Session["SUsuarioID"]);

            DateTime fechaIni = Convert.ToDateTime(txtFechaIni.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

            int usuid = Convert.ToInt32(Session["SUsuarioID"]);
            string userName = (string)Session["SUsername"];
            //string fechaModificacion = Convert.ToString(DateTime.Now);



            bool pasa = validarDatos(fechaIni, fechaFin);

            if (!pasa)
            {
                throw new InvalidCastException("Ingrese toda la información solicitada");
                
            }
            
        }

        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
    }

    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtId.Text = "0";
    }

    protected bool validarDatos(DateTime fechaIni, DateTime fechaFin)
    {
        bool pasa = true;



        

        return pasa;

    }



    protected void btnNuevoVehiculo_Click(object sender, EventArgs e)
    {
        lblNuevo.Text = "N";
        abilitarObjetos();
        ddlCurso.Enabled = true;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();
    }

    protected void btnCancelar_Click()
    {
        lblNuevo.Text = "";
        pnEscuela.Enabled = true;
        pnListaAutos.Visible = true;
        pnActualizacion.Visible = false;
        blanquearObjetos();
    }

    protected void abilitarObjetos()
    {
        pnEscuela.Enabled = false;
        pnListaAutos.Visible = false;
        pnActualizacion.Visible = true;
    }

    #endregion
}