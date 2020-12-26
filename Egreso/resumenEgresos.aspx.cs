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


public partial class Egreso_resumenEgresos : System.Web.UI.Page
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

    protected void listaReportes(string ltipo, string laccion1, string laccion2)
    {

       string accion = "TODOS";
        DateTime lfechaInicio, lfechaFin;

        string  lsucursal = ddlSucursal2.SelectedValue.Trim();
        

        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);

        if (ltipo == "1")
        {

            int tipo = (int)Session["STipo"];
            if (tipo == 4) 
            {
                accion = "TODOS";
            }
            else
            {
                accion = "SUCURSAL";
            }

                grvAnetaEgresos.DataSource = dc.sp_EgresosAneta3(accion,lsucursal,lfechaInicio, lfechaFin);
                grvAnetaEgresos.DataBind();

        }

        if (ltipo == "2")
        {
            grvDetalleEgr.DataSource = dc.sp_detalleEgresos(accion, lsucursal, lfechaInicio, lfechaFin);
            grvDetalleEgr.DataBind();

        }
    }

    protected void btnEgresosoAneta_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

       

        ltipo = "1";
        laccion1 = "DETALLE";
        laccion2 = "TOTAL";

        btnExcelAn.Visible = true;
        btnExcelCA.Visible = false;

        pnAnetaEgresos.Visible = true;
        pnDetalleEgr.Visible = false;


        listaReportes(ltipo, laccion1, laccion2);

    }

    protected void btnEgreosxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

       

        ltipo = "2";
        laccion1 = "DETSUC";
        laccion2 = "TOTSUC";

        btnExcelAn.Visible = false;
        btnExcelCA.Visible = true;

        pnAnetaEgresos.Visible = false;
        pnDetalleEgr.Visible = true;


        listaReportes(ltipo, laccion1, laccion2);

    }

    #region EXCEL
    protected void btnExcelCA_Click(object sender, EventArgs e)
    {

        uno();
    }
    protected void btnExcelAn_Click(object sender, EventArgs e)
    {
        dos();
    }
    protected void grvTotalProductos_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            grvDetalleEgr.AllowPaging = false;
            /// this.BindGrid();

            grvDetalleEgr.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetalleEgr.HeaderRow.Cells)
            {
                cell.BackColor = grvDetalleEgr.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetalleEgr.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetalleEgr.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetalleEgr.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetalleEgr.RenderControl(hw);

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
            grvAnetaEgresos.AllowPaging = false;
            /// this.BindGrid();

            grvAnetaEgresos.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvAnetaEgresos.HeaderRow.Cells)
            {
                cell.BackColor = grvAnetaEgresos.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvAnetaEgresos.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvAnetaEgresos.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvAnetaEgresos.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvAnetaEgresos.RenderControl(hw);

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