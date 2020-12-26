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

public partial class Escuela_imprimirTitulos : System.Web.UI.Page
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
            listarPedidos();
   
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


   


    protected void listarPedidos()
    {
        string Accion = "PEDIDOS";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        string tipopedido = "A";

        var cPedido = ds.sp_abmPedidos(Accion, 0, "", sucursal, 0, 0, "", tipopedido, "", DateTime.Today, false, curso);

        ddlPedido.DataSource = cPedido;
        ddlPedido.DataBind();

        ListItem listCon = new ListItem("Seleccione pedido", "-1");

        ddlPedido.Items.Insert(0, listCon);

    }


    protected void listarAuto()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cAuto = ds.sp_abmAuto(Accion, 0, "", "", 0, "", "", "", "", 0, 0, 0, sucursal, false);

        grvCursoDetalle.DataSource = cAuto;
        grvCursoDetalle.DataBind();

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string sucursal = ddlSucursal.Text.Trim();
        string curso = ddlCurso.SelectedValue.Trim();
        string pedido = ddlPedido.SelectedValue;
        int id = 0;

         Session["pSucursal"] = sucursal;
         Session["pCurso"] = curso;
         Session["ppedido"] = pedido;
         Session["ppedido"] = pedido;
         Session["pId"] = pedido;

      //  Session["pCedula"] = cedula;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirTitulosxPedido.aspx','','width=1200,height=800') </script>");


         for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
         {
             id = Convert.ToInt32(grvCursoDetalle.Rows[i].Cells[0].Text);
             Session["pId"] = id;
             //Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirTitulosxPedido.aspx','','width=1200,height=800',false) </script>");
             Response.Redirect("~/Escuela/imprimirTitulosxPedido.aspx",true);
         }


        lblMensaje.Text = "";
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

    }



    #endregion

    #region GRILLAS


    protected void grvHorarioDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {


        listarCurso();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
    }
    protected void ddlSucursal_SelectedIndexChanged1(object sender, EventArgs e)
    {
        activarObjetos();
    }

    protected void ddlModalidad_SelectedIndexChanged1(object sender, EventArgs e)
    {
        listarCurso();
    }
    protected void ddlSucursal_SelectedIndexChanged2(object sender, EventArgs e)
    {
        listarCurso();
    }

    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusveh")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);


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
    protected void grvCursoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        bool estado;

        /* for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
         {
             estado = Convert.ToBoolean(grvCursoDetalle.Rows[i].Cells[0].Text);

             //string  estado = Convert.ToString(grvCursoDetalle.Rows[i].Cells[1].Text);

             if (estado)
             {
                 grvCursoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                 grvCursoDetalle.Rows[i].ForeColor = Color.White;
             }
         }*/

    }



    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);


        var cPedidos = ds.sp_listarPedidos("CONSULTAR", sucursal, cur_id);
        ddlPedido.DataSource = cPedidos;
        ddlPedido.DataBind();

        ListItem listCon = new ListItem("Seleccione pedido", "-1");

        ddlPedido.Items.Insert(0, listCon);
        lblMensaje.Text = "";
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
    
    protected void ddlPedido_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();

        var cCurso = ds.sp_PedidoTitulos("TITULOS", sucursal, cur_id, pedido);
        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();



        lblMensaje.Text = "";
    }
    protected void btnActa_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string sucursal = ddlSucursal.Text.Trim();
        string curso = ddlCurso.SelectedValue.Trim();
        string pedido = ddlPedido.SelectedValue;

        Session["pSucursal"] = sucursal;
        Session["pCurso"] = curso;
        Session["ppedido"] = pedido;

        //  Session["pCedula"] = cedula;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirActaEntregaTitulos.aspx','','width=1200,height=800') </script>");


        lblMensaje.Text = "";
    }
}