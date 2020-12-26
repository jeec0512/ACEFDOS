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

public partial class Catalogo_titulo : System.Web.UI.Page
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
        txtTit_id.Text = "0";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        int tit_id = Convert.ToInt32(txtTit_id.Text);
        string sucursal = ddlSucursal.SelectedValue;
        int del = Convert.ToInt32(txtDel.Text);
        int al = Convert.ToInt32(txtAl.Text);
        int asignado = 0;
        string alterno = txtAlterno.Text;
        bool completo = false;

        bool pasa = validarDatos(sucursal, del,al,alterno,asignado);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmTitulos(Accion,tit_id,del,al,alterno,asignado,completo);
            blanquearObjetos();
            lblMensaje.Text = "Serie: Del:"+txtDel.Text + " al "+txtAl.Text + " guardado correctamente";
        }

        listarMaterias();
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtTit_id.Text = "0";
        txtDel.Text = "0";
        txtAl.Text = "0";
        txtAlterno.Text = "0";
        txtUlimoNum.Text = "0";
        



    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, int del, int al, string alterno, int asignado)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || alterno.Length < 1
)
        {
            pasa = false;
        };

        if (del <= 0 || al <= 0)
        {
            pasa = false;
        }

        if (del >  al)
        {
            pasa = false;
        }

        if (asignado != 0)
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
        txtTit_id.Text = Convert.ToString(id);


        var cTitulo = ds.sp_abmTitulos(Accion, Convert.ToInt32(txtTit_id.Text), 0, 0, "", 0, false);

        foreach (var registro in cTitulo)
        {
            lblMensaje.Text = string.Empty;
            txtDel.Text = Convert.ToString(registro.SERIE_DEL);
            txtAl.Text = Convert.ToString(registro.SERIE_AL);
            txtAlterno.Text = registro.ALTERNO;
            txtUlimoNum.Text = Convert.ToString(registro.ULTNUMASIGNADO);


        }


        txtDel.Focus();
    }


    protected void listarMaterias()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cEgresos = ds.sp_abmTitulos(Accion, 0,0,0,"",0,false);

        grvTitulos.DataSource = cEgresos;
        grvTitulos.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvTitulos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            lblMensaje.Text = "";
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvTitulos.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidTitulo = Convert.ToInt32(row.Cells[1].Text);
            int lvalor = Convert.ToInt32(row.Cells[5].Text);

            if (lvalor == 0)
            {
                ibConsultar_Click(lidTitulo);
            }
            else {
                lblMensaje.Text = "No puede modificar series con títulos ya asignados";
            }

        }
    }


    protected void grvTitulos_RowDataBound(object sender, GridViewRowEventArgs e)
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