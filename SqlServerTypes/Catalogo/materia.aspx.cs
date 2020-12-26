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

public partial class Catalogo_materia : System.Web.UI.Page
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
            listarMaterias();
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
        txtMat_id.Text = "0";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        int mat_id = Convert.ToInt32(txtMat_id.Text);
        string sucursal = ddlSucursal.SelectedValue;
        string descripcion = txtDescripcion.Text.Trim();
        decimal valor = Convert.ToDecimal(txtValor.Text);
        string usuario = (string)Session["SUsername"];
        string fechaModificacion = Convert.ToString(DateTime.Now);

        bool pasa = validarDatos(sucursal, descripcion,valor, usuario, fechaModificacion);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmMateria(Accion, 0, descripcion,valor, usuario, Convert.ToDateTime(fechaModificacion));
            blanquearObjetos();
            lblMensaje.Text = descripcion.Trim() + " guardado correctamente";
        }

        listarMaterias();
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtMat_id.Text = "0";
        txtDescripcion.Text = string.Empty;
        txtValor.Text = string.Empty;


    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, string descripcion,decimal valor, string usuario, string fechaModificacion)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || descripcion.Length <= 2
            || usuario.Length <= 2
)
        {
            pasa = false;
        };

        if (valor <= 0)
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
        txtMat_id.Text = Convert.ToString(id);


        var cAula = ds.sp_abmMateria(Accion, id, "", 0, "",DateTime.Today);

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            txtDescripcion.Text = registro.MAT_DESCRIPCION;
            txtValor.Text = Convert.ToString(registro.MAT_VALOR);

        }


        txtDescripcion.Focus();
    }


    protected void listarMaterias()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cEgresos = ds.sp_abmMateria(Accion, 0, "", 0, "", DateTime.Today);

        grvMateriaDetalle.DataSource = cEgresos;
        grvMateriaDetalle.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvMateriaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvMateriaDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[2].Text);


            ibConsultar_Click(lidCurso);

        }
        if (e.CommandName == "eliReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvMateriaDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidMateria = Convert.ToInt32(row.Cells[2].Text);


            try
            {
                lblMensaje.Text = "elimina registro" + lidMateria;
                ds.sp_abmMateria("BORRAR", lidMateria,"", 0, "", DateTime.Now);
            }
            catch (InvalidCastException esep)
            {
                lblMensaje.Text = "Error de eliminación: " + esep;
            }

            /* tbl_secuenciales tbl_secuenciales = dc.tbl_secuenciales.SingleOrDefault(p => p.sucursal == suc);
            tbl_secuenciales.retencion = sec;
            dc.SubmitChanges();*/




        }
    }


    protected void grvMateriaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        listarMaterias();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        listarMaterias();
    }
    protected void ddlSucursal_SelectedIndexChanged1(object sender, EventArgs e)
    {
        activarObjetos();
        listarMaterias();
    }
}