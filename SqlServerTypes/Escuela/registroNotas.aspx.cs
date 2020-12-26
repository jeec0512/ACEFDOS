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

public partial class Escuela_registroNotas : System.Web.UI.Page
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

    protected void btnNotas_Click(object sender, EventArgs e)
    {
        ifIngNotas.Attributes["visible"] = "false";
        int userId = Convert.ToInt32(Session["SUsuarioID"]);
        string accion = string.Empty;
        string controlEstudiantil = "http://www.aneta.org.ec:8080/ASIGNOT/WFNotasEstudiante.aspx?prm=" + userId;


        perfilUsuario();
        activarObjetos();

        ifIngNotas.Attributes["src"] = controlEstudiantil;
        
        
        
        
        /*string usuario, Username, sUrl, sScript;

        usuario = Convert.ToString(Session["SUsuarioID"]);
        int userId = Convert.ToInt32(Session["SUsuarioID"]);

        if (usuario == "" || usuario == null)
            Response.Redirect("~/ingresar.aspx");

        //Username = Convert.ToString(Session["SUsuarioname"]).Trim();

        Username = (string)Session["SUsername"];

        //sUrl = "http://190.63.17.119:9094/AnetaFacturacionEsc/site/home.jsf?prmusrid=" + Username;

        sUrl = " http://www.aneta.org.ec:8080/ASIGNOT/WFNotasEstudiante.aspx?prm=" + userId;

        string notas = " http://www.aneta.org.ec:8080/ASIGNOT/WFNotasEstudiante.aspx?prm=" + userId;*/
      /*  Response.Redirect(notas, false);*/

/*
        sScript = "<script language =javascript> ";

        sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=1300,height=800,left=100,top=100');";
        sScript += "</script> ";

        Response.Write(sScript);*/
        
    }
    protected void btnGeneral_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["SUsuarioID"]);
        string accion = string.Empty;
        string ingGral = "http://www.aneta.org.ec:8080/ASIGNOT/WFNotasEstudiante.aspx?prm=" + userId;


        perfilUsuario();
        activarObjetos();

        ifGeneral.Attributes["src"] = ingGral;

        /*if (Page.IsValid)
        {
            lblMensaje.Visible = true;
            System.Diagnostics.Process oProcess = null;
            try
            {
                string strPathName = @"E:\APP TO WF\WindowsFormsApplication5\WindowsFormsApplication5\bin\Debug/WindowsFormsApplication5.exe";

                if (System.IO.File.Exists(strPathName) == false)
                {
                    lblMensaje.Text = "Error: File Not Found!";
                }
                else
                {
                    oProcess = new System.Diagnostics.Process();
                    oProcess.StartInfo.Arguments = "467";
                    oProcess.StartInfo.FileName = strPathName;
                    oProcess.Start();
                    oProcess.WaitForExit();
                    System.Threading.Thread.Sleep(200);
                    lblMensaje.Text = "Application Executed Successfully...";
                }
            }
            catch (System.Exception ex)
            {
                lblMensaje.Text =
                string.Format("Error: {0}", ex.Message);
            }
            finally
            {
                if (oProcess != null)
                {
                    oProcess.Close();
                    oProcess.Dispose();
                    oProcess = null;
                }
            }
        }*/
    }
    protected void betEspecifico_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(Session["SUsuarioID"]);
        string accion = string.Empty;
        string ingEsp = "http://www.aneta.org.ec:8080/ASIGNOT/WFNotasEstudiante.aspx?prm=" + userId;


        perfilUsuario();
        activarObjetos();

        ifEspecifico.Attributes["src"] = ingEsp;
        /*if (Page.IsValid)
        {
            lblMensaje.Visible = true;
            System.Diagnostics.Process oProcess = null;
            try
            {
                string strPathName = @"E:\APP TO WF\WindowsFormsApplication5\WindowsFormsApplication5\bin\Debug/WindowsFormsApplication5.exe";

                if (System.IO.File.Exists(strPathName) == false)
                {
                    lblMensaje.Text = "Error: File Not Found!";
                }
                else
                {
                    oProcess = new System.Diagnostics.Process();
                    oProcess.StartInfo.Arguments = "467";
                    oProcess.StartInfo.FileName = strPathName;
                    oProcess.Start();
                    oProcess.WaitForExit();
                    System.Threading.Thread.Sleep(200);
                    lblMensaje.Text = "Application Executed Successfully...";
                }
            }
            catch (System.Exception ex)
            {
                lblMensaje.Text =
                string.Format("Error: {0}", ex.Message);
            }
            finally
            {
                if (oProcess != null)
                {
                    oProcess.Close();
                    oProcess.Dispose();
                    oProcess = null;
                }
            }
        }*/
    }
}