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

public partial class Socio_ImprimirContratos : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

        }
    }
    #endregion

    #region PROCESOS INTERNOS

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

            DateTime lfecha = DateTime.Today;
            txtFechaIni.Text = Convert.ToString(lfecha);
            txtFechaFin.Text = Convert.ToString(lfecha);

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void activarObjetos()
    {

    }

    #endregion


    protected void btnTarjetas_Click(object sender, EventArgs e)
    {
        btnTarjetas_Click();

    }

    protected void btnTarjetas_Click()
    {
        string laccion;
        DateTime lfecha;
        laccion = "NOENVIO";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);

        pnTarjeta.Visible = true;
        pnGuia.Visible = false;
        pnTarEnvio.Visible = false;
        pnEnvio.Visible = false;

        grvTarjeta.DataSource = dc.sp_listaTarjetasSocios(laccion, lfecha);
        grvTarjeta.DataBind();
    }

    protected void btnEnvios_Click(object sender, EventArgs e)
    {
        btnEnvios_Click(); ;
    }

    protected void btnEnvios_Click()
    {

        string laccion;
        DateTime lfecha;
        laccion = "NOENVIO";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);


        pnTarjeta.Visible = false;
        pnGuia.Visible = true;
        pnTarEnvio.Visible = false;
        pnEnvio.Visible = false;

        grvGuia.DataSource = dc.sp_listaGuias(laccion, lfecha);
        grvGuia.DataBind();
    }

    protected void btnTarEnv_Click(object sender, EventArgs e)
    {
        string laccion;
        DateTime lfecha;
        laccion = "ENVIADO";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);

        pnTarjeta.Visible = false;
        pnGuia.Visible = false;
        pnTarEnvio.Visible = true;
        pnEnvio.Visible = false;

        grvTarEnvio.DataSource = dc.sp_listaTarjetasSocios(laccion, lfecha);
        grvTarEnvio.DataBind();
    }

    protected void btnEnvio_Click(object sender, EventArgs e)
    {
        string laccion;
        DateTime lfecha;
        laccion = "ENVIADO";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);


        pnTarjeta.Visible = false;
        pnGuia.Visible = false;
        pnTarEnvio.Visible = false;
        pnEnvio.Visible = true;

   
        grvEnvio.DataSource = dc.sp_listaGuias(laccion, lfecha);
        grvEnvio.DataBind();
    }

    protected void grvTarjeta_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime lfecha = DateTime.Today
              , ldesde, lvence;

        int lid_tarjetaSocio;

        string laccion,
               lruc,
               lnombres,
               ltipo,
               lcontrato;


        laccion = "MODIFICAR";
        lid_tarjetaSocio = Convert.ToInt16(grvTarjeta.SelectedValue);
        lruc = "";
        lnombres = "";
        ldesde = lfecha;
        lvence = lfecha;
        ltipo = "";
        lcontrato = "";


        dc.sp_abmTarjetaSocio(laccion, lid_tarjetaSocio, lfecha, lruc, lcontrato, ltipo, lnombres, ldesde, lvence, lfecha, true);
        btnTarjetas_Click();

    }

    protected void grvGuia_SelectedIndexChanged(object sender, EventArgs e)
    {
        DateTime lfecha = DateTime.Today;

        int lid_guiaRemision;

        string laccion
                , lruc
                , lnombres
                , ldireccion
                , lciudad
                , lsector
                , ltelefono
                , lcontrato;



        laccion = "MODIFICAR";
        lid_guiaRemision = Convert.ToInt16(grvGuia.SelectedValue);
        lruc = "";
        lnombres = "";
        ldireccion = "";
        lciudad = "";
        lsector = "";
        ltelefono = "";
        lcontrato = "";
        dc.sp_abmGuiaRemision(laccion, lid_guiaRemision, lfecha, lruc, lcontrato, lnombres, lciudad, lsector, ldireccion, ltelefono, lfecha, true);
        btnEnvios_Click();
    }

    #region EXCEL
    protected void btnExcelTar_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExceGui_Click(object sender, EventArgs e)
    {
        dos();
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void uno()
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
            grvTarEnvio.AllowPaging = false;

            /// this.BindGrid();

            grvTarEnvio.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvTarEnvio.HeaderRow.Cells)
            {
                cell.BackColor = grvTarEnvio.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvTarEnvio.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvTarEnvio.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvTarEnvio.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvTarEnvio.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    protected void dos()
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
            grvEnvio.AllowPaging = true;
            /// this.BindGrid();

            grvEnvio.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvEnvio.HeaderRow.Cells)
            {
                cell.BackColor = grvEnvio.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvEnvio.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvEnvio.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvEnvio.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvEnvio.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    #endregion
}