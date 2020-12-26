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
public partial class Escuela_listadoEstudiantes : System.Web.UI.Page
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
            listarEscuelas();
            listarModalidad();
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
    

    

    
    
    

    

    /*LISTAR SUCURSAL*/
    protected void listarEscuelas()
    {
        string sucursal = ddlSucursal.SelectedValue;
        var cSucursal2 = dc.sp_listarSucursal("", "", 4, 0, sucursal);

        ddlEscuela.DataSource = cSucursal2;
        ddlEscuela.DataBind();
    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        string accion = string.Empty;
        int materia = 0;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        listarCurso();
        activarObjetos(modalidad, materia);

 

        ddlModEducVial_SelectedIndexChanged();
        ddlModMecanica_SelectedIndexChanged();

        ddlModPsicologia_SelectedIndexChanged();
        ddlModPrimerosAuxilios_SelectedIndexChanged();
        ddlModPractica_SelectedIndexChanged();
    }

    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
   

        horarioEducacionVial();
        horarioMecanica();
        horarioPrimerosAuxilios();
        horarioPsicologia();
        horarioPractica();

    }

    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listCon = new ListItem("Seleccione Modalidad", "-1");

        ddlModalidad.Items.Insert(0, listCon);

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



    protected void activarObjetos(int modalidad, int materia)
    {
        /*
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
        /*

        /*pnAulaDetalle15.Enabled = false;
        pnAulaDetalleMecanica.Enabled = false;
        pnAulaDetallePrimeroAuxilios.Enabled = false;
        pnAulaDetallePsicologia.Enabled = false;
        pnAutoDetalle.Enabled = false;*/

        /*

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
         */
    }



    

    #region LISTADOS DE CURSOS POR MATERIA

    protected void ddlModEducVial_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModEducVial_SelectedIndexChanged();

    }

    protected void ddlModEducVial_SelectedIndexChanged()
    {
        /*string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);*/
        
        horarioEducacionVial();


    }

    protected void ddlModMecanica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModMecanica_SelectedIndexChanged();
    }
    protected void ddlModMecanica_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        
        horarioMecanica();
    }
    protected void ddlModPrimerosAuxilios_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModPrimerosAuxilios_SelectedIndexChanged();
    }

    protected void ddlModPrimerosAuxilios_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        
        horarioPrimerosAuxilios();
    }

    protected void ddlModPsicologia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModPsicologia_SelectedIndexChanged();
    }

    protected void ddlModPsicologia_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
       
        horarioPsicologia();
    }
    protected void ddlModPractica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModPractica_SelectedIndexChanged();
        horarioPractica();
    }

    protected void ddlModPractica_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
       
    }


    

    


    

    

    
    #endregion

    #region HORARIOS ASIGNADOS POR CURSO Y MATERIA
    protected void horarioEducacionVial()
    {
        string accion = "REGISTRADO";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*EDUCACION BASICA*/
        materia = 3;

        var cAulas15 = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        ddlEducacionVialAulas.DataSource = cAulas15;
        ddlEducacionVialAulas.DataBind();

        ListItem listCon = new ListItem("Seleccione aula", "-1");
        ddlEducacionVialAulas.Items.Insert(0, listCon);



        var cHoras15 = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);
        ddlEducacionVialHoras.DataSource = cHoras15;
        ddlEducacionVialHoras.DataBind();
        ListItem listCon2 = new ListItem("Seleccione hora", "-1");
        ddlEducacionVialHoras.Items.Insert(0, listCon2);
 
    }

    protected void horarioMecanica()
    {
        string accion = "REGISTRADO";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*MECANICA*/
        materia = 4;
        var cAulasMec = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        ddlMecanicaAula.DataSource = cAulasMec;
        ddlMecanicaAula.DataBind();
        ListItem listCon = new ListItem("Seleccione aula", "-1");
        ddlMecanicaAula.Items.Insert(0, listCon);

        var cHorasMec = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);
        ddlMecanicaHora.DataSource = cHorasMec;
        ddlMecanicaHora.DataBind();
        ListItem listCon2 = new ListItem("Seleccione hora", "-1");
        ddlMecanicaHora.Items.Insert(0, listCon2);

        var cFechasMec = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);
        ddlMecanicaFecha.DataSource = cFechasMec;
        ddlMecanicaFecha.DataBind();
        ListItem listCon3 = new ListItem("Seleccione fecha", "-1");
        ddlMecanicaFecha.Items.Insert(0, listCon2);

   
    }

    protected void horarioPsicologia()
    {
        string accion = "REGISTRADO";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //activarObjetos(modalidad,materia);
        /*PSICOLOGIA*/
        materia = 5;

        var cAulasPs = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);

        ddlPsicologiaAulas.DataSource = cAulasPs;
        ddlPsicologiaAulas.DataBind();
        ListItem listCon = new ListItem("Seleccione aula", "-1");
        ddlPsicologiaAulas.Items.Insert(0, listCon);

        var cHorasPs = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);


        ddlPsicologiaHoras.DataSource = cHorasPs;
        ddlPsicologiaHoras.DataBind();
        ListItem listCon2 = new ListItem("Seleccione hora", "-1");
        ddlPsicologiaHoras.Items.Insert(0, listCon2);

        var cFechasPs = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);


        ddlPsicologiaFecha.DataSource = cFechasPs;
        ddlPsicologiaFecha.DataBind();
        ListItem listCon3 = new ListItem("Seleccione fecha", "-1");
        ddlPsicologiaFecha.Items.Insert(0, listCon2);

    }
    protected void horarioPrimerosAuxilios()
    {
        string accion = "REGISTRADO";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //activarObjetos(modalidad,materia);
        /*PRIMEROS AUXILIOS*/
        materia = 6;

        var cAulasPA = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
 
        ddlPrimerosAuxiliosAulas.DataSource = cAulasPA;
        ddlPrimerosAuxiliosAulas.DataBind();
        ListItem listCon = new ListItem("Seleccione aula", "-1");
        ddlPrimerosAuxiliosAulas.Items.Insert(0, listCon);

        var cHorasPA = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

  
        ddlPrimerosAuxiliosHoras.DataSource = cHorasPA;
        ddlPrimerosAuxiliosHoras.DataBind();
        ListItem listCon2 = new ListItem("Seleccione hora", "-1");
        ddlPrimerosAuxiliosHoras.Items.Insert(0, listCon2);

        var cFechasPA = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

        ddlPrimerosAuxiliosFecha.DataSource = cFechasPA;
        ddlPrimerosAuxiliosFecha.DataBind();
        ListItem listCon3 = new ListItem("Seleccione fecha", "-1");
        ddlPrimerosAuxiliosFecha.Items.Insert(0, listCon2);
   
    }

    

    protected void horarioPractica()
    {
        string accion = "REGISTRADO";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //activarObjetos(modalidad,materia);

        /*PRACTICA*/
        materia = 7;


        var cAuto = ds.sp_HorariosAsignadosVehiculos(accion, sucursal, curso, materia);


        ddlPracticaAutos.DataSource = cAuto;
        ddlPracticaAutos.DataBind();
        ListItem listCon = new ListItem("Seleccione aula", "-1");
        ddlPracticaAutos.Items.Insert(0, listCon);

        // accion = "DISPAUTOS";

        accion = "REGISTRADO";
        var cHorasAuto = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);


        ddlPracticaHoras.DataSource = cHorasAuto;
        ddlPracticaHoras.DataBind();
        ListItem listCon2 = new ListItem("Seleccione hora", "-1");
        ddlPracticaHoras.Items.Insert(0, listCon);


    }
    #endregion

    #region BOTONES PESTAÑAS
    
    protected void btnHorario_Click(object sender, EventArgs e)
    {
        bool visible = pnMaterias.Visible;
        if (visible)
        {
            pnMaterias.Visible = false;
            btnHorario.Text = "Ver Horarios";
        }
        else
        {
            pnMaterias.Visible = true;
            btnHorario.Text = "Ocultar Horarios";
        }
    }
    
    #endregion


    #region AULAS HORARIOS TALLERES ESCOGIDOS PARA ASIGNASR

    #endregion

    #region DESENLACE POR MATERIA Y CURSO

    protected void ddlCurEducVial_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurEducVial_SelectedIndexChanged();
        horarioEducacionVial();
    }

    protected void ddlCurEducVial_SelectedIndexChanged()
    {

    }


    protected void ddlCurMecanica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurMecanica_SelectedIndexChanged();
        horarioMecanica();
    }

    protected void ddlCurMecanica_SelectedIndexChanged()
    {

    }
    protected void ddlCurPrimerosAuxilios_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurPrimerosAuxilios_SelectedIndexChanged();
        horarioPrimerosAuxilios();
    }
    protected void ddlCurPrimerosAuxilios_SelectedIndexChanged()
    {

    }
    protected void ddlCurPsicologia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurPsicologia_SelectedIndexChanged();
        horarioPsicologia();
    }
    protected void ddlCurPsicologia_SelectedIndexChanged()
    {

    }

    protected void ddlCurPractica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurPractica_SelectedIndexChanged();
        horarioPractica();
    }

    protected void ddlCurPractica_SelectedIndexChanged()
    {

    }
    #endregion

    #region IMPRIMIR
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        string accion = "TODOS";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int aula = Convert.ToInt32(ddlEducacionVialAulas.SelectedValue);
        int materia = 3;
        int hora = Convert.ToInt32(ddlEducacionVialHoras.SelectedValue);


        grvListado.DataSource = ds.sp_ListadoEstudiantesConNotas(accion, escuela, curso, materia, hora, aula);
        grvListado.DataBind();


    }

    protected void btnImprimirEV_Click(object sender, EventArgs e)
    {
        string accion = "ESPECIFICO";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int aula = Convert.ToInt32(ddlEducacionVialAulas.SelectedValue);
        int materia = 3;
        int hora = Convert.ToInt32(ddlEducacionVialHoras.SelectedValue);


        grvListado.DataSource = ds.sp_ListadoEstudiantesConNotasEducVial(accion, escuela, curso, materia, hora, aula);
        grvListado.DataBind();


    }

    protected void btnImprimirMec_Click(object sender, EventArgs e)
    {
        string accion = "ESPECIFICO";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        
        int aula = Convert.ToInt32(ddlMecanicaAula.SelectedValue);
        int materia = 4;
        int hora = Convert.ToInt32(ddlMecanicaHora.SelectedValue);
        int taller = Convert.ToInt32(ddlMecanicaFecha.SelectedValue);

        grvListado.DataSource = ds.sp_ListadoEstudiantesConNotasMecanica(accion, escuela, curso, materia, hora, aula,taller);
        grvListado.DataBind();


    }

    protected void btnImprimirPs_Click(object sender, EventArgs e)
    {
        string accion = "ESPECIFICO";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        int aula = Convert.ToInt32(ddlPsicologiaAulas.SelectedValue);
        int materia = 5;
        int hora = Convert.ToInt32(ddlPsicologiaHoras.SelectedValue);
        int taller = Convert.ToInt32(ddlPsicologiaFecha.SelectedValue);



        grvListado.DataSource = ds.sp_ListadoEstudiantesConNotasPsicologia(accion, escuela, curso, materia, hora, aula, taller);
        grvListado.DataBind();

    }

    protected void btnImprimirPA_Click(object sender, EventArgs e)
    {
        string accion = "ESPECIFICO";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        int aula = Convert.ToInt32(ddlPrimerosAuxiliosAulas.SelectedValue);
        int materia = 6;
        int hora = Convert.ToInt32(ddlPrimerosAuxiliosHoras.SelectedValue);
        int taller = Convert.ToInt32(ddlPrimerosAuxiliosFecha.SelectedValue);

        grvListado.DataSource = ds.sp_ListadoEstudiantesConNotasPrimerosAuxilios(accion, escuela, curso, materia, hora, aula, taller);
        grvListado.DataBind();


    }

    

    protected void btnImprimirPrac_Click(object sender, EventArgs e)
    {
        string accion = "TODOS";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        int vehiculo = Convert.ToInt32(ddlPracticaAutos.SelectedValue);
        int materia = 7;
        int hora = Convert.ToInt32(ddlPracticaHoras.SelectedValue);


        grvListado.DataSource = ds.sp_ListadoEstudiantesConNotasPractica(accion, escuela, curso, materia, hora, vehiculo);
        grvListado.DataBind();


    }
    #endregion
    #region EXCEL
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void uno()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grvListado.AllowPaging = false;
                /// this.BindGrid();

                grvListado.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvListado.HeaderRow.Cells)
                {
                    cell.BackColor = grvListado.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvListado.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvListado.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvListado.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvListado.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }
    }

    protected void btnExcelPe_Click(object sender, EventArgs e)
    {
        uno();
    }
    #endregion
    #region VISTA PREVIA
   
    #endregion

    protected void btnVistaPrevia_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string accion = "TODOS";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        int vehiculo = Convert.ToInt32(ddlPracticaAutos.SelectedValue);
        int materia = 7;
        int hora = Convert.ToInt32(ddlPracticaHoras.SelectedValue);

        Session["pEscuela"] = escuela;
        Session["pModalidad"] = modalidad;
        Session["pCurso"] = curso;
        Session["pVehiculo"] = vehiculo;
        Session["pMateria"] = materia;
        Session["pHora"] = hora;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('ImprimirListadosPractica.aspx','','width=800px,height=700px') </script>");




        lblMensaje.Text = "";
    }
}