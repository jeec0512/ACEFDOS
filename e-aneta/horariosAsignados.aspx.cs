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

public partial class Escuela_horariosAsignados : System.Web.UI.Page
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
            listarCurso();
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

    /*LISTAS*/
    #region listas
    protected void listarModalidad()
    {
        ddlModalidad.SelectedValue = "-1";

    }

    protected void listarCurso()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }


    #endregion

    /*CAMBIOS*/
    #region CAMBIOS
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        string accion = string.Empty;
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        listarModalidad();
        listarCurso();
        activarObjetos(modalidad, materia);
    }
    #endregion


    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        string accion = string.Empty;
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        listarCurso();
        activarObjetos(modalidad, materia);
    }
    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        string accion = string.Empty;
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        activarObjetos(modalidad,materia);


       
    }

    protected void activarObjetos(int modalidad,int materia)
    {
        if (modalidad == 1)
        {
          //  pnDetalleMecanica.Visible = false;
          // btnMecanica.Visible = false;
        }

        pnDetalle15.Visible = false;
        pnDetalleMecanica.Visible = false;
        pnDetallePrimeroAuxilios.Visible = false;
        pnDetallePsicologia.Visible = false;
        pnDetalleAuto.Visible = false;


        /*pnAulaDetalle15.Enabled = false;
        pnAulaDetalleMecanica.Enabled = false;
        pnAulaDetallePrimeroAuxilios.Enabled = false;
        pnAulaDetallePsicologia.Enabled = false;
        pnAutoDetalle.Enabled = false;*/



        if (materia == 3)
        {
            pnDetalle15.Visible = true;
        }

        if (materia == 4)
        {
            pnDetalleMecanica.Visible = true;
        }

        if (materia == 5)
        {
            pnDetallePrimeroAuxilios.Visible = true;
        }

        if (materia == 6)
        {
            pnDetallePsicologia.Visible = true;
        }

        if (materia == 7)
        {
            pnDetalleAuto.Visible = true;
        }
    }


    #region SELECCION DE  AULAS HORARIOS Y AUTOS

    /*EDUCACION BASICA*/
    protected void grvAulaDetalle15_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetalle15_RowDataBound(0);
    }

    protected void grvAulaDetalle15_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvAulaDetalle15.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetalle15.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetalle15.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetalle15.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetalle15.Rows[i].BackColor = Color.White;
                grvAulaDetalle15.Rows[i].ForeColor = Color.Black;

            }
        }
    }

    protected void grvAulaDetalle15_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "aula15")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalle15.Rows[indice];
           row.Cells[0].Text = Convert.ToString(true);


            grvAulaDetalle15_RowDataBound(indice);
        }
    }
    
    protected void grvHorarioDetalle15_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalle15_RowDataBound(0);
    }
    protected void grvHorarioDetalle15_RowDataBound(int indice) 
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetalle15.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalle15.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalle15.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalle15.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalle15.Rows[i].BackColor = Color.White;
                grvHorarioDetalle15.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvHorarioDetalle15_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "horario15")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalle15.Rows[indice];
           row.Cells[0].Text = Convert.ToString(true);


           grvHorarioDetalle15_RowDataBound(indice);
        }
    }
 
    /*MECANICA*/
    protected void grvAulaDetalleMecanica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetalleMecanica_RowDataBound(0);
    }

    protected void grvAulaDetalleMecanica_RowDataBound(int indice) 
    {
        bool estado;
        for (int i = 0; i < grvAulaDetalleMecanica.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetalleMecanica.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetalleMecanica.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetalleMecanica.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetalleMecanica.Rows[i].BackColor = Color.White;
                grvAulaDetalleMecanica.Rows[i].ForeColor = Color.Black;

            }
        }
    }

    protected void grvAulaDetalleMecanica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mecAula")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalleMecanica.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAulaDetalleMecanica_RowDataBound(indice);
        }
    }
    protected void grvHorarioDetalleMecanica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalleMecanica_RowDataBound(0);
    }

    protected void grvHorarioDetalleMecanica_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetalleMecanica.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalleMecanica.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalleMecanica.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalleMecanica.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalleMecanica.Rows[i].BackColor = Color.White;
                grvHorarioDetalleMecanica.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvHorarioDetalleMecanica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mecHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalleMecanica.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetalleMecanica_RowDataBound(indice);
        }
    }
    protected void grvFechasDetalleMecanica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvFechasDetalleMecanica_RowDataBound(0);
    }

    protected void grvFechasDetalleMecanica_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvFechasDetalleMecanica.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvFechasDetalleMecanica.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvFechasDetalleMecanica.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvFechasDetalleMecanica.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvFechasDetalleMecanica.Rows[i].BackColor = Color.White;
                grvFechasDetalleMecanica.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvFechasDetalleMecanica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mecFecha")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalleMecanica.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetalleMecanica_RowDataBound(indice);
        }
    }



    /*PRIMEROS AUXILIOS*/
    protected void grvAulaDetallePrimeroAuxilios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetallePrimeroAuxilios_RowDataBound(0);
    }
    protected void grvAulaDetallePrimeroAuxilios_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvAulaDetallePrimeroAuxilios.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetallePrimeroAuxilios.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetallePrimeroAuxilios.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetallePrimeroAuxilios.Rows[i].BackColor = Color.White;
                grvAulaDetallePrimeroAuxilios.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvAulaDetallePrimeroAuxilios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "paAula")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetallePrimeroAuxilios.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAulaDetallePrimeroAuxilios_RowDataBound(indice);
        }
    }
    protected void grvHorarioDetallePrimeroAuxilios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetallePrimeroAuxilios_RowDataBound(0);
    }
    protected void grvHorarioDetallePrimeroAuxilios_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetallePrimeroAuxilios.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetallePrimeroAuxilios.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetallePrimeroAuxilios.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetallePrimeroAuxilios.Rows[i].BackColor = Color.White;
                grvHorarioDetallePrimeroAuxilios.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvHorarioDetallePrimeroAuxilios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "paHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetallePrimeroAuxilios.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetallePrimeroAuxilios_RowDataBound(indice);
        }
    }
    protected void grvFechasDetallePrimeroAuxilios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvFechasDetallePrimeroAuxilios_RowDataBound(0);
    }

    protected void grvFechasDetallePrimeroAuxilios_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvFechasDetallePrimeroAuxilios.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvFechasDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvFechasDetallePrimeroAuxilios.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvFechasDetallePrimeroAuxilios.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvFechasDetallePrimeroAuxilios.Rows[i].BackColor = Color.White;
                grvFechasDetallePrimeroAuxilios.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvFechasDetallePrimeroAuxilios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "paFecha")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvFechasDetallePrimeroAuxilios.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvFechasDetallePrimeroAuxilios_RowDataBound(indice);
        }
    }
    /*PSICOLOGIA*/

    protected void grvAulaDetallePsicologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetallePsicologia_RowDataBound(0);
    }
    protected void grvAulaDetallePsicologia_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvAulaDetallePsicologia.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetallePsicologia.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetallePsicologia.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetallePsicologia.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetallePsicologia.Rows[i].BackColor = Color.White;
                grvAulaDetallePsicologia.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvAulaDetallePsicologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "psiAula")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetallePsicologia.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAulaDetallePsicologia_RowDataBound(indice);
        }
    }

    protected void grvHorarioDetallePsicologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetallePsicologia_RowDataBound(0);
    }
    protected void grvHorarioDetallePsicologia_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetallePsicologia.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetallePsicologia.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetallePsicologia.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetallePsicologia.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetallePsicologia.Rows[i].BackColor = Color.White;
                grvHorarioDetallePsicologia.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvHorarioDetallePsicologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "psiHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetallePsicologia.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetallePsicologia_RowDataBound(indice);
        }
    }
 

    protected void grvFechasDetallePsicologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvFechasDetallePsicologia_RowDataBound(0);
    }
    protected void grvFechasDetallePsicologia_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvFechasDetallePsicologia.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvFechasDetallePsicologia.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvFechasDetallePsicologia.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvFechasDetallePsicologia.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvFechasDetallePsicologia.Rows[i].BackColor = Color.White;
                grvFechasDetallePsicologia.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvFechasDetallePsicologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "psiFecha")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvFechasDetallePsicologia.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvFechasDetallePsicologia_RowDataBound(indice);
        }
    }
    /*PRACTICA*/
    
    protected void grvAutoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAutoDetalle_RowDataBound(0);
    }
    protected void grvAutoDetalle_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvAutoDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAutoDetalle.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAutoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAutoDetalle.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAutoDetalle.Rows[i].BackColor = Color.White;
                grvAutoDetalle.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvAutoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pracAuto")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutoDetalle.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAutoDetalle_RowDataBound(indice);
        }
    }

    protected void grvHorarioDetalleAuto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalleAuto_RowDataBound(0);
    }
    protected void grvHorarioDetalleAuto_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetalleAuto.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalleAuto.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalleAuto.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalleAuto.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalleAuto.Rows[i].BackColor = Color.White;
                grvHorarioDetalleAuto.Rows[i].ForeColor = Color.Black;

            }
        }
    }

    protected void grvHorarioDetalleAuto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pracHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalleAuto.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetalleAuto_RowDataBound(indice);
        }
    }

    #endregion
    #region BOTONESPESTAÑAS
    protected void btnEducBas_Click(object sender, EventArgs e)
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*EDUCACION BASICA*/
        materia = 3;

        var cAulas15 = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);

        grvAulaDetalle15.DataSource = cAulas15;
        grvAulaDetalle15.DataBind();


        var cHoras15 = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetalle15.DataSource = cHoras15;
        grvHorarioDetalle15.DataBind();

        activarObjetos(modalidad,materia);
    }

    protected void btnMecanica_Click(object sender, EventArgs e)
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*MECANICA*/
        materia = 4;
        var cAulasMec = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);

        grvAulaDetalleMecanica.DataSource = cAulasMec;
        grvAulaDetalleMecanica.DataBind();


        var cHorasMec = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetalleMecanica.DataSource = cHorasMec;
        grvHorarioDetalleMecanica.DataBind();

        var cFechasMec = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

        grvFechasDetalleMecanica.DataSource = cFechasMec;
        grvFechasDetalleMecanica.DataBind();

        activarObjetos(modalidad, materia);
    }

    protected void btnPrimAux_Click(object sender, EventArgs e)
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);
        /*PRIMEROS AUXILIOS*/
        materia = 5;

        var cAulasPA = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        grvAulaDetallePrimeroAuxilios.DataSource = cAulasPA;
        grvAulaDetallePrimeroAuxilios.DataBind();


        var cHorasPA = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetallePrimeroAuxilios.DataSource = cHorasPA;
        grvHorarioDetallePrimeroAuxilios.DataBind();

        var cFechasPA = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

        grvFechasDetallePrimeroAuxilios.DataSource = cFechasPA;
        grvFechasDetallePrimeroAuxilios.DataBind();

        activarObjetos(modalidad, materia);
    }

    protected void btnPsico_Click(object sender, EventArgs e)
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);
        /*PSICOLOGIA*/
        materia = 6;

        var cAulasPs = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        grvAulaDetallePsicologia.DataSource = cAulasPs;
        grvAulaDetallePsicologia.DataBind();


        var cHorasPs = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetallePsicologia.DataSource = cHorasPs;
        grvHorarioDetallePsicologia.DataBind();

        var cFechasPs = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

        grvFechasDetallePsicologia.DataSource = cFechasPs;
        grvFechasDetallePsicologia.DataBind();
        
        activarObjetos(modalidad, materia);
    }

    protected void btnPrac_Click(object sender, EventArgs e)
    {
        string accion = string.Empty;
        int materia = 0;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);

        /*PRACTICA*/
        materia = 7;
        accion = "DISPONIBLE";

        var cAuto = ds.sp_HorariosAsignadosVehiculos(accion, sucursal, curso, materia);

        grvAutoDetalle.DataSource = cAuto;
        grvAutoDetalle.DataBind();


        var cHorasAuto = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetalleAuto.DataSource = cHorasAuto;
        grvHorarioDetalleAuto.DataBind();

        activarObjetos(modalidad, materia);

    }
    #endregion









   
}