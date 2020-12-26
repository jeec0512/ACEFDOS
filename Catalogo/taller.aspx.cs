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
public partial class Catalogo_taller : System.Web.UI.Page
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
            activarObjetos();
            listarModalidad();
            listarCurso();
            listarMateria();
            txtTal_id.Text = "0";

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
        var cCurso = ds.sp_abmCurso("MODALIDAD", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }

    protected void listarMateria()
    {

        var cMateria = ds.sp_abmMateria("TALLER", 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione materia", "-1");

        ddlMateria.Items.Insert(0, listCon);

    }
    

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        int Tal_id = Convert.ToInt32(txtTal_id.Text);
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        string fecha = txtFecha.Text.Trim();
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

        bool pasa = validarDatos(sucursal, modalidad, curso, materia, fecha, activo, usuario, fechaModificacion);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmTaller(Accion,Tal_id,0,curso,materia,sucursal,Convert.ToDateTime(fecha),usuario,Convert.ToDateTime(fechaModificacion),Convert.ToBoolean(activo));
            blanquearObjetos();
            lblMensaje.Text = fecha.Trim() + " guardado correctamente";
        }
        listarTaller();

    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtFecha.Text = string.Empty;
        chkEstado.Checked = false;
       
    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, int modalidad, int curso,int  materia, string fecha, int activo, string usuario, string fechaModificacion)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || fecha.Length <= 2
            || usuario.Length <= 2
)
        {
            pasa = false;
        };

        if (modalidad == -1
            || curso == -1
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
        txtTal_id.Text = Convert.ToString(id);



        var cTaller = ds.sp_abmTaller(Accion, id, 0, 0, 0, "", DateTime.Today, "", DateTime.Today,false);

        foreach (var registro in cTaller)
        {
            lblMensaje.Text = string.Empty;
            ddlSucursal.SelectedValue = registro.sucursal;
            ddlModalidad.SelectedValue = Convert.ToString(registro.mod_id);
            ddlCurso.SelectedValue = Convert.ToString(registro.CUR_ID);
            ddlMateria.SelectedValue = Convert.ToString(registro.MAT_ID);
            txtFecha.Text = Convert.ToString(registro.TAL_FECHA);
            chkEstado.Checked = Convert.ToBoolean(registro.TAL_ESTADO);

        }


        txtFecha.Focus();
    }


    protected void listarTaller()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue); 
         int curso = Convert.ToInt32(ddlCurso.SelectedValue ); 
        int materia = Convert.ToInt32( ddlMateria.SelectedValue); 
        
       
       

        var ctaller = ds.sp_abmTaller(Accion,0,0,curso,materia,sucursal,DateTime.Today,"",DateTime.Today,false);

        grvTallerDetalle.DataSource = ctaller;
        grvTallerDetalle.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvTallerDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvTallerDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[2].Text);


            ibConsultar_Click(lidCurso);

        }
        if (e.CommandName == "eliReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvTallerDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidtaller = Convert.ToInt32(row.Cells[2].Text);


            try
            {
                tallerExiste(lidtaller);
                lblMensaje.Text = "elimina registro" + lidtaller;
                ds.sp_abmTaller("BORRAR", lidtaller, 0, 0, 0, "", DateTime.Now, "", DateTime.Now, false);
            }
            catch (Exception esep)
            {
                lblMensaje.Text = "Error de eliminación: " + esep.Message;
            }
        }
        listarTaller();
    }

    protected void tallerExiste(int lidTal)
    {
        int taller = 0;
        /*var cTal = from mTal in ds.TB_TALLER
                   where mTal.TAL_ID == lidTal
                   select new
                   {
                       TAL_ID = mTal.TAL_ID
                   };
        */
        var cTal = from mTal in ds.TB_ASIGNA_MATERIA
                   where mTal.TAL_ID == lidTal
                   select new
                   {
                       TAL_ID = mTal.TAL_ID
                   };

        

        foreach (var regNum in cTal)
        {
            taller = Convert.ToInt32(regNum.TAL_ID);
        };

        if (taller > 0)
        {
            throw new InvalidOperationException("Fecha de taller está creado en cupos");
        }
    }


    protected void grvTallerDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
    #endregion

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarCurso();
    }
    protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarTaller();
    }
    
}