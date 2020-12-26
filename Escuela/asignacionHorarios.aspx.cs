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

public partial class Escuela_asignacionHorarios : System.Web.UI.Page
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
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }

    protected void listarMateria()
    {
        var cMateria = ds.sp_abmMateria("TODOS", 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione Materia", "-1");

        ddlMateria.Items.Insert(0, listCon);

    }

    protected void listarAuto()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cAuto = ds.sp_abmAuto(Accion,0,"","",0,"","","","",0,0,0,sucursal,false);

        grvAutoDetalle.DataSource = cAuto;
        grvAutoDetalle.DataBind();

    }

    protected void listarAula()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
       
        var cAula = ds.sp_abmAula(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal,0);

        grvAulaDetalle.DataSource = cAula;
        grvAulaDetalle.DataBind();

    }

    protected void listarHorario()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var cHorario = ds.sp_abmHorario(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal, modalidad, materia);

        grvHorarioDetalle.DataSource = cHorario;
        grvHorarioDetalle.DataBind();

    }


    protected void listarTaller()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var ctaller = ds.sp_abmTaller(Accion, 0, 0, curso, materia, sucursal, DateTime.Today, "", DateTime.Today, false);

        grvTallerDetalle.DataSource = ctaller;
        grvTallerDetalle.DataBind();

    }

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

        var cAsignaciones = ds.sp_VerHorarios(accion, sucursal, modalidad, curso, materia,usuario,fecha);

        grvAsignaciones.DataSource = cAsignaciones;
        grvAsignaciones.DataBind();
       
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
   
    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, int modalidad, string horaInicio, string horaFin, int activo, string usuario, string fechaModificacion, int materia)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || horaInicio.Length <= 2
            || horaFin.Length <= 2
            || usuario.Length <= 2
)
        {
            pasa = false;
        };

        if (modalidad == -1
            || materia == -1)
        {
            pasa = false;
        }


        if (fechaModificacion.Length <= 2)
        {
            pasa = false;
        }
        return pasa;

    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ibConsultar_Click(int id)
    {
        string Accion = "CONSULTAR";

        string sucursal = ddlSucursal.SelectedValue;
        int horario = Convert.ToInt32(ddlMateria.SelectedValue);


        var cAula = ds.sp_abmHorario(Accion, id, 0, "", "", false, "", DateTime.Today, sucursal, 0, horario);

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            ddlSucursal.SelectedValue = registro.SUCURSAL;


        }


        //txtHoraInicio.Focus();
    }


    
    #endregion

    #region GRILLAS
    protected void grvHorarioDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[1].Text);


            ibConsultar_Click(lidCurso);

        }
    }

    protected void grvHorarioDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarHorario();
        listarMateria();
        listarCurso();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        listarMateria();
    }
    protected void ddlSucursal_SelectedIndexChanged1(object sender, EventArgs e)
    {
        activarObjetos();
        listarMateria();
    }

    protected void ddlModalidad_SelectedIndexChanged1(object sender, EventArgs e)
    {
        listarCurso();
    }
    protected void ddlSucursal_SelectedIndexChanged2(object sender, EventArgs e)
    {
        listarCurso();
        listarMateria();
        listarHorario();
    }
    protected void ddlMateria_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string materia = ddlMateria.SelectedValue;
        switch (materia)
        {
            case "3": //EDUCACION VIAL
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "4": // MECANICA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = true;
                break;
            case "5": // PSICOLOGIA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = true;
                break;
            case "6":// PRIMEROS AUXILIOS
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = true;
                break;
            case "7": // PRACTICA
                pnAutoDetalle.Visible = true;
                pnAulaDetalle.Visible = false;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;


            default:
                break;

        }


        listarAula();
        listarHorario();
        listarAula();
        listarAuto();
        listarTaller();
    }

    #region PROCESOS DE DETALLE DE AULA
    protected void grvAulaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusaula")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[1].Text);


            TB_AULA TB_AULA = ds.TB_AULA.SingleOrDefault(x => x.AUL_ID == lid);
            lActivo = Convert.ToBoolean(TB_AULA.AUL_ACTIVO);
           
            if (lActivo)
            {
                lActivo = false;
                
            }
            else {
                lActivo = true;
                
            }

  
            TB_AULA = ds.TB_AULA.SingleOrDefault(x => x.AUL_ID == lid);
            TB_AULA.AUL_ACTIVO = lActivo;
            ds.SubmitChanges();

              listarAula();
        }

    }
    protected void grvAulaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    #endregion
    protected void grvAutoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusveh")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[1].Text);


            VEHICULO vehiculo = da.VEHICULO.SingleOrDefault(x => x.VEH_ID == lid);
            lActivo = Convert.ToBoolean(vehiculo.VEH_ESTADO);

            if (lActivo)
            {
                lActivo = false;

            }
            else
            {
                lActivo = true;

            }


            vehiculo = da.VEHICULO.SingleOrDefault(x => x.VEH_ID == lid);
            vehiculo.VEH_ESTADO = lActivo;
            da.SubmitChanges();

            listarAuto();
        }

    }
    protected void grvAutoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvHorarioDetalle_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jushorario")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[1].Text);


            TB_HORARIO TB_HORARIO = ds.TB_HORARIO.SingleOrDefault(x => x.HOR_ID == lid);
            lActivo = Convert.ToBoolean(TB_HORARIO.HOR_ESTADO);

            if (lActivo)
            {
                lActivo = false;

            }
            else
            {
                lActivo = true;

            }


            TB_HORARIO = ds.TB_HORARIO.SingleOrDefault(x => x.HOR_ID == lid);
            TB_HORARIO.HOR_ESTADO = lActivo;
            ds.SubmitChanges();

            listarHorario();
        }
    }
    protected void grvHorarioDetalle_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvTallerDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusfecha")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvTallerDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[1].Text);


            TB_TALLER TB_TALLER = ds.TB_TALLER.SingleOrDefault(x => x.TAL_ID == lid);
            lActivo = Convert.ToBoolean(TB_TALLER.TAL_ESTADO);

            if (lActivo)
            {
                lActivo = false;

            }
            else
            {
                lActivo = true;

            }


            TB_TALLER = ds.TB_TALLER.SingleOrDefault(x => x.TAL_ID == lid);
            TB_TALLER.TAL_ESTADO = lActivo;
            ds.SubmitChanges();

            listarTaller();
        }
    }
    protected void grvTallerDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}