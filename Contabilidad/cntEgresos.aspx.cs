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

public partial class Contabilidad_cntEgresos : System.Web.UI.Page
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


            iniciarVariables();
            listarAnos();
            listarPeriodos();
            listarSemanas();
            //listarFechas();

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
    protected void iniciarVariables()
    {
        txtFechaInicio.Text = Convert.ToString(DateTime.Now);
        txtFechaFin.Text = Convert.ToString(DateTime.Now);
        txtFechaExportacion.Text = Convert.ToString(DateTime.Now);
        chkActivo.Checked = false;
    }

    #region LISTAR GRIDS
    protected void listarFechas()
    {
        int ano = Convert.ToInt32(ddlAno.SelectedValue);
        int periodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

        var cfechas = dc.sp_ListarEtiquetaDiariosContables("VERFECHASE", ano, periodo);

        grvFechas.DataSource = cfechas;
        grvFechas.DataBind();

    }

    protected void listarAnos()
    {
        var cAno = dc.sp_ListarAnos("NOBLOQUEO", "N");
        ddlAno.DataSource = cAno;
        ddlAno.DataBind();

        ListItem listMod = new ListItem("Seleccione el año", "-1");
        ddlAno.Items.Insert(0, listMod);
    }
    protected void listarPeriodos()
    {
        var cPeriodo = dc.sp_ListarPeriodos("NOBLOQUEO", "N");
        ddlPeriodo.DataSource = cPeriodo;
        ddlPeriodo.DataBind();

        ListItem listMod = new ListItem("Seleccione el período", "-1");
        ddlPeriodo.Items.Insert(0, listMod);

    }
    protected void listarSemanas()
    {
        var cSemana = dc.sp_ListarSemanas("NOBLOQUEO", "N");
        ddlSemana.DataSource = cSemana;
        ddlSemana.DataBind();

        ListItem listMod = new ListItem("Seleccione la semana", "-1");
        ddlSemana.Items.Insert(0, listMod);
    }
    #endregion

    /**/


    protected void ddlAno_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarFechas();
    }
    protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarFechas();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Boolean pasa = true;
        string accion = "GUARDAR";

        DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
        DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);
        int usuarioIdCrea = Convert.ToInt32(Session["SUsuarioID"]);
        int usuarioIdMod = Convert.ToInt32(Session["SUsuarioID"]);
        DateTime fechaCrea = DateTime.Now;
        DateTime fechaMod = DateTime.Now;

        Boolean estado = Convert.ToBoolean(chkActivo.Checked);

        string semana = ddlSemana.SelectedValue;
        DateTime fechaExp = Convert.ToDateTime(txtFechaExportacion.Text);
        int ano = Convert.ToInt32(ddlAno.SelectedValue);
        int periodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

        if (semana == "-1")
        {
            pasa = false;
        }
        if (fechaInicio > fechaFin)
        {
            pasa = false;
        }

        if (fechaInicio.Year != ano)
        {
            pasa = false;
        }

        if (fechaInicio.Month != periodo)
        {
            pasa = false;
        }

        if (fechaFin.Year != ano)
        {
            pasa = false;
        }

        if (fechaFin.Month != periodo)
        {
            pasa = false;
        }

        if (pasa)
        {
            dc.sp_abmEtiquetaDiariosContables(accion, 0, "E","", fechaInicio, fechaFin, usuarioIdCrea, usuarioIdMod, fechaCrea, fechaMod, estado, semana, fechaExp, ano, periodo);
            btnCancelar_Click();
        }
        else
        {
            lblMensaje.Text = "Verifique que las fechas sean las correctas y esten dentro del año y período seleccionados";
        }

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();


    }

    protected void btnCancelar_Click()
    {
        divModificaRegistros.Visible = false;
        pnAno.Enabled = true;
        pnPeriodo.Enabled = true;
        divBotones.Visible = true;
        lblMensaje.Text = "";
        pnAutos.Enabled = true;
        listarFechas();


    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        btnNuevo_Click();


    }

    protected void btnNuevo_Click()
    {
        divModificaRegistros.Visible = true;
        pnAno.Enabled = false;
        pnPeriodo.Enabled = false;
        divBotones.Visible = false;
        pnAutos.Enabled = false;


    }


    protected void btnModifica_Click(object sender, EventArgs e)
    {
        divModificaRegistros.Visible = true;
    }
    protected void btnRegresa_Click(object sender, EventArgs e)
    {

    }
    protected void grvFechas_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = Convert.ToString(grvFechas.SelectedValue);
        txtId.Enabled = false;
        txtId.Text = id;
        ibConsultar_Click();
        btnNuevo_Click();
    }

    protected void ibConsultar_Click()
    {
        int lId = Convert.ToInt32(txtId.Text);

        if (lId > 0)
        {
            var cFechas = from tFechas in dc.tbl_EtiquetaDiariosContables
                          where tFechas.id_etiquetaDiariosContables == lId
                          select new
                          {
                              codigoGenerico = tFechas.codigoGenerico
                              ,
                              sucursal = tFechas.sucursal
                              ,
                              fechaInicio = tFechas.fechaInicio
                              ,
                              fechaFin = tFechas.fechaFin
                              ,
                              usuarioModifica = tFechas.usuarioModifica
                              ,
                              fechaModifica = tFechas.fechaModifica
                              ,
                              estado = tFechas.estado
                              ,
                              semana = tFechas.semana
                              ,
                              fechaExportacion = tFechas.fechaExportacion

                          };


            // llenarListados();
            //blanquearObjetos();

            foreach (var registro in cFechas)
            {


                txtFechaInicio.Text = Convert.ToString(registro.fechaInicio);

                txtFechaFin.Text = Convert.ToString(registro.fechaFin);

                chkActivo.Checked = Convert.ToBoolean(registro.estado);
                ddlSemana.SelectedValue = registro.semana;
                txtFechaExportacion.Text = Convert.ToString(registro.fechaExportacion);

            }

            txtFechaInicio.Focus();
        }
        else
        {
            txtFechaInicio.Focus();
        }
    }
}