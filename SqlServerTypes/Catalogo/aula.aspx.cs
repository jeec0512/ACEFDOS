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
public partial class Catalogo_aula : System.Web.UI.Page
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
            listarAulas();
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
        txtAul_id.Text = "0";
        txtCapacidad.Text = "0";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        int aul_id = Convert.ToInt32(txtAul_id.Text);
        string sucursal = ddlSucursal.SelectedValue;
        string escuela = txtEscuela.Text.Trim();
        string descripcion = txtDescripcion.Text.Trim();
        int capacidad = Convert.ToInt32(txtCapacidad.Text);
        int activo = Convert.ToInt32(chkEstado.Checked);
        int nActivo = 0;
        if (activo==0)
        {
            nActivo = 0;
        }
        else
        {
            nActivo = 1;
        }
        string usuario = (string)Session["SUsername"];
        string fechaModificacion = Convert.ToString(DateTime.Now);

        bool pasa = validarDatos(sucursal, escuela,descripcion,activo,usuario,fechaModificacion,capacidad);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmAula(Accion, aul_id, 0, escuela, descripcion, Convert.ToBoolean(nActivo), usuario, Convert.ToDateTime(fechaModificacion), sucursal,capacidad);
            blanquearObjetos();
            lblMensaje.Text = descripcion.Trim() + " guardado correctamente";
        }

        listarAulas();
    }
    protected void blanquearObjetos()
    {
       
        lblMensaje.Text = string.Empty;
        txtAul_id.Text = "0";
        txtEscuela.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        chkEstado.Checked = false;

    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, string escuela, string descripcion, int activo, string usuario, string fechaModificacion, int capacidad)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || escuela.Length <= 2
            || descripcion.Length <= 2
            || usuario.Length <= 2
)
        {
            pasa = false;
        };

        if (capacidad <= 0)
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
        txtAul_id.Text = Convert.ToString(id);
        int capacidad = Convert.ToInt32(txtCapacidad.Text);

        var cAula = ds.sp_abmAula(Accion, id, 1, "", "", false, "", Convert.ToDateTime(DateTime.Today), sucursal,capacidad);

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            txtEscuela.Text = registro.AUL_ESCUELA;
            txtDescripcion.Text = registro.AUL_DESCRIPCION;
            chkEstado.Checked = Convert.ToBoolean(registro.AUL_ACTIVO);

        }


        txtEscuela.Focus();
    }


    protected void listarAulas() 
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue; ;

        var cEgresos = ds.sp_abmAula(Accion, 0, 1, "", "", false, "", Convert.ToDateTime(DateTime.Today), sucursal,0);

        grvAulaDetalle.DataSource = cEgresos;
        grvAulaDetalle.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvAulaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            
            int lidAula = Convert.ToInt32(row.Cells[2].Text);


            ibConsultar_Click(lidAula);

        }
        if (e.CommandName == "eliReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidAula = Convert.ToInt32(row.Cells[2].Text);


            try
            {
                lblMensaje.Text = "elimina registro" + lidAula;
                ds.sp_abmAula("BORRAR", lidAula, 0,"", "", false, "", DateTime.Now, "", 0);
            }
            catch (InvalidCastException esep)
            {
                lblMensaje.Text = "Error de eliminación: " + esep;
            }

            listarAulas();
            /* tbl_secuenciales tbl_secuenciales = dc.tbl_secuenciales.SingleOrDefault(p => p.sucursal == suc);
            tbl_secuenciales.retencion = sec;
            dc.SubmitChanges();*/




        }
    }


    protected void grvAulaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarAulas();
    }
}