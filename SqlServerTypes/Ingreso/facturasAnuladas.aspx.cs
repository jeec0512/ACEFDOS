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


public partial class Ingreso_facturasAnuladas : System.Web.UI.Page
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
        pnTotalAnuladas.Visible = false;
        pnAnuladas.Visible = false;
    }

    #endregion

    protected void btnAnuTotal_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        int tipo = (int)Session["STipo"];

        if (tipo == 4)
        {
            ltipo = "1";
            laccion1 = "DETALLE";
            laccion2 = "TOTAL";
        }
        else
        {
            ltipo = "1";
            laccion1 = "DETSUC";
            laccion2 = "TOTSUC";
        }

        listaReportes(ltipo, laccion1, laccion2);
        pnAnuladas.Visible = true;
        pnTotalAnuladas.Visible = true;

       
    }

    protected void btnAnuxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

      
        ltipo = "2";
        laccion1 = "DETSUC";
        laccion2 = "TOTSUC";

        listaReportes(ltipo, laccion1, laccion2);
        pnAnuladas.Visible = true;
        pnTotalAnuladas.Visible = true;

    }
    
    protected void listaReportes(string ltipo, string laccion1, string laccion2)
    {
        string ltipoSuc, lsucursal;
        DateTime lfechaInicio, lfechaFin;

        ltipoSuc = "";
        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        if (ltipo == "1")
        {
            grvAnuladas.DataSource = dc.sp_FacturasAnuladas(laccion1, ltipoSuc, lsucursal, 0, lfechaInicio, lfechaFin);
            grvAnuladas.DataBind();

            grvTotalAnuladas.DataSource = dc.sp_TotalFacturasAnuladas(laccion2, ltipoSuc, lsucursal, 0, lfechaInicio, lfechaFin);
            grvTotalAnuladas.DataBind();
        }

        if (ltipo == "2")
        {
            grvAnuladas.DataSource = dc.sp_FacturasAnuladas(laccion1, ltipoSuc, lsucursal, 0, lfechaInicio, lfechaFin);
            grvAnuladas.DataBind();

            grvTotalAnuladas.DataSource = dc.sp_TotalFacturasAnuladas(laccion2, ltipoSuc, lsucursal, 0, lfechaInicio, lfechaFin);
            grvTotalAnuladas.DataBind();
        }
    }

    #region EXCEL
    protected void btnExcelAA_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelAS_Click(object sender, EventArgs e)
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
            grvAnuladas.AllowPaging = false;
            /// this.BindGrid();

            grvAnuladas.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvAnuladas.HeaderRow.Cells)
            {
                cell.BackColor = grvAnuladas.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvAnuladas.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvAnuladas.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvAnuladas.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvAnuladas.RenderControl(hw);

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
            grvTotalAnuladas.AllowPaging = false;
            /// this.BindGrid();

            grvTotalAnuladas.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvTotalAnuladas.HeaderRow.Cells)
            {
                cell.BackColor = grvTotalAnuladas.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvTotalAnuladas.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvTotalAnuladas.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvTotalAnuladas.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvTotalAnuladas.RenderControl(hw);

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