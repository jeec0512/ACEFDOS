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

public partial class Catalogo_curso : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();
            activarObjetos();
            listarCursos();
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
        txtCur_id.Text = "0";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        int cur_id = Convert.ToInt32(txtCur_id.Text);
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string sucursal = ddlSucursal.SelectedValue;
        string nomenclatura = txtNomenclatura.Text.Trim();
        string descripcion = txtDescripcion.Text.Trim();
        string fechaInicio = txtFechaInicio.Text;
        string fechaFin = txtFechaFin.Text;
        int activo = Convert.ToInt32(chkEstado.Checked);
        int nActivo = 0;
        if (activo == 0)
        {
            nActivo = 0;
        }
        else
        {
            nActivo = 1;
        }
        string usuario = (string)Session["SUsername"];
        string fechaModificacion = Convert.ToString(DateTime.Now);

        bool pasa = validarDatos(sucursal, modalidad, nomenclatura, descripcion, fechaInicio, fechaFin, activo, usuario, fechaModificacion);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmCurso(Accion,cur_id,modalidad,nomenclatura,descripcion,Convert.ToDateTime(fechaInicio),Convert.ToDateTime(fechaFin),Convert.ToBoolean(nActivo),usuario,Convert.ToDateTime(fechaModificacion));
            blanquearObjetos();
            lblMensaje.Text = nomenclatura.Trim() + " guardado correctamente";
        }

        listarCursos();
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtCur_id.Text = "0";
        txtNomenclatura.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        chkEstado.Checked = false;

    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal,int modalidad, string nomenclatura, string descripcion, string fechaInicio, string fechaFin,int activo, string usuario, string fechaModificacion)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || nomenclatura.Length <= 2
            || descripcion.Length <= 2
            || fechaInicio.Length <= 2
            || fechaFin.Length <= 2
            || usuario.Length <= 2
)
        {
            pasa = false;
        };

        if (modalidad == -1) {
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
        txtCur_id.Text = Convert.ToString(id);


        var cAula = ds.sp_abmCurso(Accion, id,0,"","",Convert.ToDateTime(DateTime.Today),Convert.ToDateTime(DateTime.Today),false,"",Convert.ToDateTime(DateTime.Today));

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            ddlModalidad.SelectedValue = Convert.ToString(registro.MOD_ID);
            txtNomenclatura.Text = registro.CUR_NOMENCLATURA;
            txtDescripcion.Text = registro.CUR_DESCRIPCION;
            txtFechaInicio.Text = Convert.ToString(registro.CUR_FECHA_INICIO);
            txtFechaFin.Text = Convert.ToString(registro.CUR_FECHA_FIN);
            chkEstado.Checked = Convert.ToBoolean(registro.CUR_ACTIVO);

        }


        txtNomenclatura.Focus();
    }


    protected void listarCursos()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue; 
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);

        var cEgresos = ds.sp_abmCurso(Accion, 0, modalidad, "", "", Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(DateTime.Today), false, "", Convert.ToDateTime(DateTime.Today));

        grvCursoDetalle.DataSource = cEgresos;
        grvCursoDetalle.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[1].Text);


            ibConsultar_Click(lidCurso);

        }
    }


    protected void grvCursoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarCursos();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarCursos();
    }
}