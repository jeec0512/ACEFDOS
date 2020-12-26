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
using acefdos;

public partial class Escuela_creacionHorarios2 : System.Web.UI.Page
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
            lblMensaje.Text = "";
            string accion = string.Empty;
            perfilUsuario();
            listarModalidad();
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
                Response.Redirect("~/inicio.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];



            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/inicio.aspx");
            }

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/inicio.aspx");
            lblMensaje.Text = e.Message;
        }
    }

    #endregion

    #region LISTAR HORARIOS FECHA HORAS TALLERES, MATERIAS
    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listMod = new ListItem("Seleccione la modalidad", "-1");

        ddlModalidad.Items.Insert(0, listMod);
    }

    protected void listarCurso()
    {
        var cCurso = ds.sp_abmCurso("", 0, 0, "", "", DateTime.Now, DateTime.Now, false, "", DateTime.Now);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();
        ListItem listCur = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCur);
    }

    protected void listarHorario()
    {
        string Accion = "ACTIVOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var cHorario = ds.sp_abmHorario(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal, modalidad, materia);

        grvHoras.DataSource = cHorario;
        grvHoras.DataBind();

    }

    protected void listarMateria()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string accion = string.Empty;
        if (modalidad == 5 || modalidad == 6)
        {
            accion = "RECUPERA";
        }
        else if (modalidad == 8)
        {
            accion = "PSICO";
        }
        else {
            accion = "CURSO";
        }

        var cMateria = ds.sp_abmMateria(accion, 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione Materia", "-1");

        ddlMateria.Items.Insert(0, listCon);

    }
    #endregion

    /*CHECKEDS*/
    #region CONTROL DE ACTIVACIONES DISPONIBLES
    /*AULAS*/
    protected void cbAulaHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in grvAulas.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cbAulaConfirmar")).Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void cbAulaConfirmar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (CheckBox)grvAulas.HeaderRow.FindControl("cbAulaHeader");
        if (headerCheckBox.Checked)
        {
            headerCheckBox.Checked = ((CheckBox)sender).Checked;
        }
        else
        {
            bool allCheckBoxesChecked = true;
            foreach (GridViewRow gridVieRow in grvAulas.Rows)
            {
                if (!((CheckBox)gridVieRow.FindControl("cbAulaConfirmar")).Checked)
                {
                    allCheckBoxesChecked = false;
                    break;
                }
            }
            headerCheckBox.Checked = allCheckBoxesChecked;
        }

    }

    /***************/

    /*AUTOS*/
    protected void cbAutoHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in grvAutos.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cbAutoConfirmar")).Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void cbAutoConfirmar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (CheckBox)grvAutos.HeaderRow.FindControl("cbAutoHeader");
        if (headerCheckBox.Checked)
        {
            headerCheckBox.Checked = ((CheckBox)sender).Checked;
        }
        else
        {
            bool allCheckBoxesChecked = true;
            foreach (GridViewRow gridVieRow in grvAutos.Rows)
            {
                if (!((CheckBox)gridVieRow.FindControl("cbAutoConfirmar")).Checked)
                {
                    allCheckBoxesChecked = false;
                    break;
                }
            }
            headerCheckBox.Checked = allCheckBoxesChecked;
        }

    }

    /***************/

    /*HORARIOS*/
    protected void cbHoraHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in grvHoras.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cbHoraConfirmar")).Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void cbHoraConfirmar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (CheckBox)grvHoras.HeaderRow.FindControl("cbHoraHeader");
        if (headerCheckBox.Checked)
        {
            headerCheckBox.Checked = ((CheckBox)sender).Checked;
        }
        else
        {
            bool allCheckBoxesChecked = true;
            foreach (GridViewRow gridVieRow in grvHoras.Rows)
            {
                if (!((CheckBox)gridVieRow.FindControl("cbHoraConfirmar")).Checked)
                {
                    allCheckBoxesChecked = false;
                    break;
                }
            }
            headerCheckBox.Checked = allCheckBoxesChecked;
        }

    }

    /*******************/
    /*MATERIAS*/
    protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria_SelectedIndexChanged();
    }


    protected void ddlMateria_SelectedIndexChanged()
    {
        lblMensaje.Text = "";
        string modalidad = ddlModalidad.SelectedValue;
        string materia = ddlMateria.SelectedValue;
        switch (materia)
        {
            case "-1": //SIN MATERIA
                pnAutos.Visible = false;
                pnAulas.Visible = false;
                pnHoras.Visible = false;
                pnDias.Visible = false;
                dvListado.Visible = false;
                break;
            case "3": //EDUCACION VIAL
                pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = false;
                break;
            case "4": // MECANICA
                pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                //if (modalidad == "2" || modalidad == "3" || modalidad == "4" || modalidad == "10")
                if (modalidad == "2" || modalidad == "4" || modalidad == "10" || modalidad == "11")
                {
                    pnDias.Visible = true;
                }
                else
                {
                    pnDias.Visible = false;
                }
                break;
            case "5": // PSICOLOGIA
                pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "6":// PRIMEROS AUXILIOS
                pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "7": // PRACTICA
                pnAutos.Visible = true;
                pnAulas.Visible = false;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "8": //LEYES DE TRANSITO
                 pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "10": //EDUCACION VIAL
                 pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "12": //PRIMEROS AUXILIOS
                 pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "13": //PSICOLOGIA
               pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = true;
                break;
            case "14": // PSICO
                pnAutos.Visible = true;
                pnAulas.Visible = false;
                pnHoras.Visible = true;
                pnDias.Visible = false;
                break;
            default:
                dvListado.Visible = true;
                break;
        }
          
            /*RECUPERACION DE PUNTOS*/
        /*
            case "8": //LEYES DE TRANSITO
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "10": //EDUCACION VIAL
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "12": //PRIMEROS AUXILIOS
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "13": //PSICOLOGIA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "14": //PSICOSENSOMÉTRICO
                pnAutoDetalle.Visible = true;
                pnAulaDetalle.Visible = false;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            default:
                break;

        }*/
        
            listarAula();
            listarHorario();
            listarAuto();
            listarTaller();
            verAsignaciones();
    }


    /***************/

    /*DIAS/TALLERES*/
    protected void cbDiaHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in grvDias.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cbDiaConfirmar")).Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void cbDiaConfirmar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (CheckBox)grvDias.HeaderRow.FindControl("cbDiaHeader");
        if (headerCheckBox.Checked)
        {
            headerCheckBox.Checked = ((CheckBox)sender).Checked;
        }
        else
        {
            bool allCheckBoxesChecked = true;
            foreach (GridViewRow gridVieRow in grvDias.Rows)
            {
                if (!((CheckBox)gridVieRow.FindControl("cbDiaConfirmar")).Checked)
                {
                    allCheckBoxesChecked = false;
                    break;
                }
            }
            headerCheckBox.Checked = allCheckBoxesChecked;
        }

    }

    /***************/

    /*LISTADO DE AULAS/AUTOS ACTIVADOS*/
    protected void cbActivarHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in grvActivaciones.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cbActivarConfirmar")).Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void cbActivarConfirmar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (CheckBox)grvActivaciones.HeaderRow.FindControl("cbActivarHeader");
        if (headerCheckBox.Checked)
        {
            headerCheckBox.Checked = ((CheckBox)sender).Checked;
        }
        else
        {
            bool allCheckBoxesChecked = true;
            foreach (GridViewRow gridVieRow in grvActivaciones.Rows)
            {
                if (!((CheckBox)gridVieRow.FindControl("cbActivarConfirmar")).Checked)
                {
                    allCheckBoxesChecked = false;
                    break;
                }
            }
            headerCheckBox.Checked = allCheckBoxesChecked;
        }

    }

    /***************/


    #endregion

    #region VERIFICAR SI ACTIVARON AULAS,HORAS,DIAS
    protected Tuple<string, bool> verificarActivaciones(string horarios, string confirmar, string nota, GridView grid)
    {
        string respuesta = string.Empty;
        bool pasa = true;

        List<string> lstParaPedido = new List<string>();

        foreach (GridViewRow gridViewRow in grid.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl(confirmar)).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl(nota)).Text;
                lstParaPedido.Add(regId);
            }
        }
        if (lstParaPedido.Count > 0)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Navy;
            respuesta = lstParaPedido.Count.ToString() + " " + horarios + "(s) confirmado(as)";
            pasa = true;
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            respuesta = "No existen " + " " + horarios + "(s) confirmado(as)";
            pasa = false;
        }
        var tuple = new Tuple<string, bool>(respuesta, pasa);
        return tuple;
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        List<string> lstEstudiantesParaPedido = new List<string>();

        foreach (GridViewRow gridViewRow in grvActivaciones.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cbConfirmar")).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl("lblIdNota")).Text;
                lstEstudiantesParaPedido.Add(regId);
            }
        }
        if (lstEstudiantesParaPedido.Count > 0)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Navy;

            /*foreach (string strRegId in lstEstudiantesParaPedido) 
            {
                estudianteDataAccessLayer.confirmarEstudiantes(Convert.ToInt32(strRegId));
                
            }*/

            estudianteDataAccessLayer.confirmarEstudiantes(lstEstudiantesParaPedido);

            lblMensaje.Text = lstEstudiantesParaPedido.Count.ToString() + "fila(s) confirmadas";
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "No existen fila(s) confirmadas";
        }
    }
    #endregion

    #region CAMBIOS AL SELECCIONAR DROPDOWNLIST
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModalidad_SelectedIndexChanged();
    }

    protected void ddlModalidad_SelectedIndexChanged()
    {
        lblMensaje.Text = "";
        string accion = "MODACTIVOS";
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso(accion, 0, modalidad, "", "", DateTime.Now, DateTime.Now, false, "", DateTime.Now);
        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCur = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCur);
        listarMateria();
    }
    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);

        switch (modalidad)
        {
            case -1: //SIN CURSO
                pnAutos.Visible = false;
                pnAulas.Visible = false;
                pnHoras.Visible = false;
                pnDias.Visible = false;
                break;

            case 5: //RECUPERACION DE PUNTOS 15 DIAS
                pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = false;
                break;
            case 6: //RECUPERACION DE PUNTOS FINES DE SEMANA
                pnAutos.Visible = false;
                pnAulas.Visible = true;
                pnHoras.Visible = true;
                pnDias.Visible = false;
                break;
        }

        /*LISTAR HORARIOS*/
        listarAula();
        listarHorario();
        listarMateria();
        ddlMateria_SelectedIndexChanged();
       // verAsignaciones();
    }
    #endregion

    #region LISTAR GRIDS
    protected void listarAuto()
    {
        string Accion = "XMODAL";
        
        
        /*OJO REVISAR  10/10/2019*/
        /**PENDIENTE PARA AUMENTAR EL TIPO**/

        string sucursal = ddlSucursal.SelectedValue;
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cAuto = new object();


        if (materia == 14)
        {
            Accion = "XMODALACT";
            cAuto = ds.sp_abmAuto_escuela3(Accion, sucursal,modalidad);
        }
        else {
            cAuto = ds.sp_abmAuto_escuela3(Accion, sucursal, modalidad);
        }

        grvAutos.DataSource = cAuto;
        grvAutos.DataBind();

    }
    protected void listarAula()
    {
        string Accion = "ACTIVOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cAula = ds.sp_abmAula(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal, 0);

        grvAulas.DataSource = cAula;
        grvAulas.DataBind();

    }

    protected void listarTaller()
    {
        string Accion = "ACTIVOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var ctaller = ds.sp_abmTaller(Accion, 0, 0, curso, materia, sucursal, DateTime.Today, "", DateTime.Today, false);

        grvDias.DataSource = ctaller;
        grvDias.DataBind();

    }

    protected void verAsignaciones()
    {
        dvListado.Visible = true;
        string accion = "AULAS";
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        var cAsignaciones = ds.sp_VerHorarios2(accion, sucursal, 0, curso, materia, 0, 0, 0, 0, "", DateTime.Now);

        grvActivaciones.DataSource = cAsignaciones;
        grvActivaciones.DataBind();


        int columnas = grvActivaciones.Columns.Count;

        if (materia==3) {
            grvActivaciones.Columns[0].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[0].HeaderStyle.CssClass = "Display";
            grvActivaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[3].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[3].HeaderStyle.CssClass = "Display";
            grvActivaciones.Columns[4].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[4].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";

        }

        if (materia == 4)
        {
            grvActivaciones.Columns[0].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[0].HeaderStyle.CssClass = "Display";
            grvActivaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";

        }

        if (materia == 7)
        {
            grvActivaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[3].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[3].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";

        }

        if (materia == 14)
        {
            grvActivaciones.Columns[1].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[1].HeaderStyle.CssClass = "Display";
            grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";

        }


        if (modalidad == 1) 
        {
            if (materia == 4)
            {
                grvActivaciones.Columns[0].ItemStyle.CssClass = "Display";
                grvActivaciones.Columns[0].HeaderStyle.CssClass = "Display";
                grvActivaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
                grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";

            }else if (materia == 5 || materia == 6)
            {
                grvActivaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
                grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";
            }
        }
        else if (materia == 4 || materia == 5 || materia == 6)
            {
                grvActivaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
                grvActivaciones.Columns[7].ItemStyle.CssClass = "Display";
                grvActivaciones.Columns[7].HeaderStyle.CssClass = "Display";
            }

        /*
        string accion = "AULAS";
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = 3;
        var cAsignaciones = ds.sp_VerHorarios2(accion, sucursal, 0, curso, materia, 0, 0, 0, 0, "", DateTime.Now);

        grvActivaciones.DataSource = cAsignaciones;
        grvActivaciones.DataBind();


        int columnas = grvActivaciones.Columns.Count;

        if (materia == 3)
        {
            grvActivaciones.Columns[1].ItemStyle.CssClass = "Display";
            grvActivaciones.Columns[1].HeaderStyle.CssClass = "Display";
            grvActivaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[7].ItemStyle.CssClass = "DisplayNone";
            grvActivaciones.Columns[7].HeaderStyle.CssClass = "DisplayNone";

        }
        */


    }

    #endregion


    #region BOTONES DE ACCIÓN
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;
        string usuario = (string)Session["SUsername"];
        DateTime fecha = DateTime.Now;


        string accion = "nada";
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        int kont = 0;
        //  var cAsignaciones = ds.sp_VerHorarios(accion, sucursal, modalidad, curso, materia,usuario,fecha);



        bool estado1 = false;
        bool estado2 = false;
        bool estado3 = false;
        int aul_id = 0;
        int veh_id = 0;
        int hor_id = 0;
        int tal_id = 0;

        /*ASIGNACION POR MATERIA*/
        /*EDUCACION VIAL*/
        if (materia == 3)
        {

            for (int i = 0; i < grvAulas.Rows.Count; i++)
            {
                //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[3].Text);
                estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                if (estado1)
                {
                    //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                    string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                    aul_id = Convert.ToInt32(regIdAula);

                    for (int j = 0; j < grvHoras.Rows.Count; j++)
                    {
                        //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                        estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                        if (estado2)
                        {
                           //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                            string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                            hor_id = Convert.ToInt32(regIdHora);
                            kont = 0;
                            var cuentaE =ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                            foreach (var registro in cuentaE)
                            {
                                 kont = Convert.ToInt32(registro.educvial);

                            }
                            if (kont <= 0)
                            {
                                ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                            }
                        }
                    }

                }
            }
        }


        if (modalidad == 1 || modalidad == 3)
        {
            if (materia == 4)
            {
                accion = "QUINCE";
                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);
                                kont = 0;
                               // var cuentaM = ds.sp_cuentaHorariosMecanica(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                var cuentaM = ds.sp_cuentaHorariosMecanica(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                            foreach (var registro in cuentaM)
                            {
                                 kont = Convert.ToInt32(registro.mecanica);

                            }
                            if (kont <= 0)
                            {
                               // ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                            }
                            }
                        }

                    }
                }
            }
        }
        else
        {
            if (materia == 4)
            {
                accion = "SIETE";
                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);

                                for (int K = 0; K < grvDias.Rows.Count; K++)
                                {
                                    //estado3 = Convert.ToBoolean(grvDias.Rows[K].Cells[0].Text);
                                    estado3 = ((CheckBox)grvDias.Rows[K].FindControl("cbDiaConfirmar")).Checked;

                                    if (estado3)
                                    {
                                        tal_id = Convert.ToInt32(grvDias.Rows[K].Cells[2].Text);
                                        kont = 0;
                                        var cuentaM = ds.sp_cuentaHorariosMecanica(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                            foreach (var registro in cuentaM)
                            {
                                 kont = Convert.ToInt32(registro.mecanica);

                            }
                            if (kont <= 0)
                            {


                                ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                            }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

       /*PSICOLOGIA PRIMEROS AUXILIOS*/

        /*PSICOLOGIA */
            estado1 = false;
            estado2 = false;
            estado3 = false;
            aul_id = 0;
            veh_id = 0;
            hor_id = 0;
            tal_id = 0;


            if (materia == 5 )
            {

                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;
                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;
                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);

                                for (int K = 0; K < grvDias.Rows.Count; K++)
                                {
                                    //estado3 = Convert.ToBoolean(grvDias.Rows[K].Cells[0].Text);
                                    estado3 = ((CheckBox)grvDias.Rows[K].FindControl("cbDiaConfirmar")).Checked;
                                    if (estado3)
                                    {
                                        tal_id = Convert.ToInt32(grvDias.Rows[K].Cells[2].Text);
                                         kont = 0;
                                         var cuentaPs = ds.sp_cuentaHorariosPsicologia(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                            foreach (var registro in cuentaPs)
                            {
                                 kont = Convert.ToInt32(registro.psico);

                            }
                            if (kont <= 0)
                            {
                                ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                            }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            /*PRIMEROS AUXILIOS*/
            estado1 = false;
            estado2 = false;
            estado3 = false;
            aul_id = 0;
            veh_id = 0;
            hor_id = 0;
            tal_id = 0;


            if ( materia == 6)
            {

                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;
                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);

                                for (int K = 0; K < grvDias.Rows.Count; K++)
                                {
                                    //estado3 = Convert.ToBoolean(grvDias.Rows[K].Cells[0].Text);
                                    estado3 = ((CheckBox)grvDias.Rows[K].FindControl("cbDiaConfirmar")).Checked;

                                    if (estado3)
                                    {
                                        tal_id = Convert.ToInt32(grvDias.Rows[K].Cells[2].Text);
                                        kont = 0;
                                        var cuentaPA = ds.sp_cuentaHorariosPrimerosAuxilios(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                        foreach (var registro in cuentaPA)
                                        {
                                            kont = Convert.ToInt32(registro.primaux);

                                        }
                                        if (kont <= 0)
                                        {
                                            ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            /*PRACTICA*/

            accion = "PRACTICA";
           estado1 = false;
            estado2 = false;
            estado3 = false;
            aul_id = 0;
            veh_id = 0;
            hor_id = 0;
            tal_id = 0;

            if (materia == 7)
            {

                for (int i = 0; i < grvAutos.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAutos.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAutos.Rows[i].FindControl("cbAutoConfirmar")).Checked;
                    if (estado1)
                    {
                        //veh_id = Convert.ToInt32(grvAutos.Rows[i].Cells[2].Text);
                        string regIdAuto = ((Label)grvAutos.Rows[i].FindControl("lblIdAuto")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);

                                kont = 0;
                                var cuentaPrac = ds.sp_cuentaHorariosPractica(accion, sucursal, modalidad, curso, materia, aul_id, Convert.ToInt32(regIdAuto), hor_id, tal_id, usuario, fecha);
                                        foreach (var registro in cuentaPrac)
                                        {
                                            kont = Convert.ToInt32(registro.practica);

                                        }
                                        if (kont <= 0)
                                        {
                                            ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, Convert.ToInt32(regIdAuto), hor_id, tal_id, usuario, fecha);
                                        }
                            }
                        }

                    }
                }
            }


        /*RECUPERACION DE PUNTOS*/
        /*LEYES DE TRANSITO*/
            if (materia == 8)
            {

                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);
                                kont = 0;
                                var cuentaE = ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                foreach (var registro in cuentaE)
                                {
                                    kont = Convert.ToInt32(registro.educvial);

                                }
                                if (kont <= 0)
                                {
                                    ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                }
                            }
                        }

                    }
                }
            }

            /*EDUCACION VIAL*/
            if (materia == 10)
            {

                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);
                                kont = 0;
                                var cuentaE = ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                foreach (var registro in cuentaE)
                                {
                                    kont = Convert.ToInt32(registro.educvial);

                                }
                                if (kont <= 0)
                                {
                                    ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                }
                            }
                        }

                    }
                }
            }

            /*PRIMEROS AUXILIOS*/
            if (materia == 12)
            {

                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;
                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);
                                kont = 0;
                                var cuentaE = ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                foreach (var registro in cuentaE)
                                {
                                    kont = Convert.ToInt32(registro.educvial);

                                }
                                if (kont <= 0)
                                {
                                    ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                }
                            }
                        }

                    }
                }
            }

            /*PSICOLOGIA*/
            if (materia == 13)
            {

                for (int i = 0; i < grvAulas.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAulas.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAulas.Rows[i].FindControl("cbAulaConfirmar")).Checked;

                    if (estado1)
                    {
                        //aul_id = Convert.ToInt32(grvAulas.Rows[i].Cells[2].Text);
                        string regIdAula = ((Label)grvAulas.Rows[i].FindControl("lblIdAula")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);
                                kont = 0;
                                var cuentaE = ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                foreach (var registro in cuentaE)
                                {
                                    kont = Convert.ToInt32(registro.educvial);

                                }
                                if (kont <= 0)
                                {
                                    ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, Convert.ToInt32(regIdAula), veh_id, hor_id, tal_id, usuario, fecha);
                                }
                            }
                        }

                    }
                }
            }

            /*PSICOSENSOMÉTRICO*/

            accion = "PSICOSENSO";
            estado1 = false;
            estado2 = false;
            estado3 = false;
            aul_id = 0;
            veh_id = 0;
            hor_id = 0;
            tal_id = 0;

            if (materia == 14)
            {

                for (int i = 0; i < grvAutos.Rows.Count; i++)
                {
                    //estado1 = Convert.ToBoolean(grvAutos.Rows[i].Cells[0].Text);
                    estado1 = ((CheckBox)grvAutos.Rows[i].FindControl("cbAutoConfirmar")).Checked;

                    if (estado1)
                    {
                        //veh_id = Convert.ToInt32(grvAutos.Rows[i].Cells[2].Text);
                        string regIdAuto = ((Label)grvAutos.Rows[i].FindControl("lblIdAuto")).Text;

                        for (int j = 0; j < grvHoras.Rows.Count; j++)
                        {
                            //estado2 = Convert.ToBoolean(grvHoras.Rows[j].Cells[0].Text);
                            estado2 = ((CheckBox)grvHoras.Rows[j].FindControl("cbHoraConfirmar")).Checked;

                            if (estado2)
                            {
                                //hor_id = Convert.ToInt32(grvHoras.Rows[j].Cells[2].Text);
                                string regIdHora = ((Label)grvHoras.Rows[j].FindControl("lblIdHora")).Text;
                                hor_id = Convert.ToInt32(regIdHora);
                                kont = 0;
                                var cuentaPrac = ds.sp_cuentaHorariosPractica(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                foreach (var registro in cuentaPrac)
                                {
                                    kont = Convert.ToInt32(registro.practica);

                                }
                                if (kont <= 0)
                                {
                                    ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, Convert.ToInt32(regIdAuto), hor_id, tal_id, usuario, fecha);
                                }
                            }
                        }

                    }
                }
            }
        verAsignaciones();
    }

    



    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = string.Empty;
        foreach (GridViewRow gridViewRow in grvActivaciones.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cbActivarConfirmar")).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl("lblIdActivar")).Text;
                int lid = Convert.ToInt32(regId);

                var deleteOrderDetails =
                from details in ds.TB_ASIGNA_MATERIA
                where details.ASM_ID == lid
                select details;

                foreach (var detail in deleteOrderDetails)
                {
                    ds.TB_ASIGNA_MATERIA.DeleteOnSubmit(detail);
                }

                try
                {
                    ds.SubmitChanges();
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp);
                    lblMensaje.Text = "Se encuentra alumnos registrados en este horario.." + Convert.ToString(exp).Substring(0, 200);
                    // Provide for exceptions.
                }

                verAsignaciones();

            }
        }

    }
    #endregion

    #region ACCIONES SOBRE ACTIVACIONES REALIZADAS
    protected void grvActivaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMensaje.Text = string.Empty;

        if (e.CommandName == "EliminaReg")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvActivaciones.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            
            //int lid = Convert.ToInt32(row.Cells[1].Text);

            string regId = ((Label)row.FindControl("lblIdActivar")).Text;
            int lid = Convert.ToInt32(regId);

            var deleteOrderDetails =
                from details in ds.TB_ASIGNA_MATERIA
                where details.ASM_ID == lid
                select details;

            foreach (var detail in deleteOrderDetails)
            {
                ds.TB_ASIGNA_MATERIA.DeleteOnSubmit(detail);
            }

            try
            {
                ds.SubmitChanges();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                lblMensaje.Text = "Se encuentra alumnos registrados en este horario.." + Convert.ToString(exp).Substring(0, 200);
                // Provide for exceptions.
            }

            verAsignaciones();
        }
    }
    #endregion

    #region CONFIRMARACTIVACIONES
    protected void confirmarAulas()
    {
        List<string> lstAulasparaActivar = new List<string>();

        foreach (GridViewRow gridViewRow in grvAulas.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cbAulaConfirmar")).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl("lblIdAula")).Text;
                lstAulasparaActivar.Add(regId);
                lblMensaje.Text = regId;
            }
        }
        if (lstAulasparaActivar.Count > 0)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Navy;

            /*foreach (string strRegId in lstEstudiantesParaPedido) 
            {
                estudianteDataAccessLayer.confirmarEstudiantes(Convert.ToInt32(strRegId));
                
            }*/

            // estudianteDataAccessLayer.confirmarEstudiantes(lstAulasparaActivar);

            lblMensaje.Text = lstAulasparaActivar.Count.ToString() + "Aula(s) confirmadas";
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "No existen fila(s) confirmadas";
        }
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        pnAutos.Visible = false;
        pnAulas.Visible = false;
        pnHoras.Visible = false;
        pnDias.Visible = false;
        dvListado.Visible = false;
        listarModalidad();
        ddlModalidad_SelectedIndexChanged();
    }
}