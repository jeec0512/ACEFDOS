﻿using AjaxControlToolkit;
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


public partial class Escuela_pensumAcademico : System.Web.UI.Page
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

            //DateTime lfecha = DateTime.Today;
            //txtFechaIni.Text = Convert.ToString(lfecha);
            //txtFechaFin.Text = Convert.ToString(lfecha);

            //var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            //ddlSucursal2.DataSource = cSucursal;
            //ddlSucursal2.DataBind();
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void activarObjetos()
    {

    }

    #endregion

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string lbuscar, lopcion, laccion, lcedula;
        string cadena = string.Empty;
        char caracter = '0';

        laccion = "";

        lopcion = ddlTipoBusqueda.SelectedValue.Trim();

        lbuscar = txtBuscar.Text.Trim();

        lcedula = lbuscar.Trim();


        if (lopcion == "0")
        {
            ///lista clientes q cumplen condicipon de cédula
            ///
            laccion = "XCLIENTE";
            grvEstudiante.DataSource = ds.sp_abmRegistroNota_Con(laccion,0,0,lcedula,lcedula,"","",0,0,0,0,false,0,0,0,0,false,0,false,0,false,0,false,false,"",false,false,"","" ,"","","","","",0,"",0,"",false,false);
            grvEstudiante.DataBind();


            grvHistorico.DataSource = ds.sp_abmDatosAcademicos(laccion, cadena, cadena, lcedula, cadena, cadena, cadena, cadena, cadena, cadena, cadena, cadena, cadena, cadena, 0, caracter, cadena, cadena, cadena, cadena, caracter, caracter, cadena, cadena, DateTime.Now, DateTime.Now, DateTime.Now, cadena, cadena, cadena, cadena, cadena, cadena, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, cadena, cadena, cadena, cadena, cadena, cadena, false, false, false, false, false, cadena, caracter, caracter, caracter, caracter, false, false, false, false, cadena, caracter, cadena, cadena, caracter, DateTime.Now, caracter, caracter, caracter, caracter, caracter, 0, DateTime.Now, cadena, lcedula, cadena, 0, false);
            grvHistorico.DataBind();


           
        }

        if (lopcion == "1")
        {
            laccion = "XNOMBRE";

            ///lista clientes q cumplen condicipon de nombre
            ///

            grvEstudiante.DataSource = ds.sp_abmRegistroNota_Con(laccion, 0, 0, lcedula, lcedula, "", "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false,false);
            grvEstudiante.DataBind();

            grvHistorico.DataSource = ds.sp_abmDatosAcademicos(laccion, cadena, cadena, lcedula, cadena, cadena, cadena, cadena, cadena, cadena, cadena, cadena, cadena, cadena, 0, caracter, cadena, cadena, cadena, cadena, caracter, caracter, cadena, cadena, DateTime.Now, DateTime.Now, DateTime.Now, cadena, cadena, cadena, cadena, cadena, cadena, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, false, cadena, cadena, cadena, cadena, cadena, cadena, false, false, false, false, false, cadena, caracter, caracter, caracter, caracter, false, false, false, false, cadena, caracter, cadena, cadena, caracter, DateTime.Now, caracter, caracter, caracter, caracter, caracter, 0, DateTime.Now, cadena, lcedula, cadena, 0, false);
            grvHistorico.DataBind();
        }
    }
}