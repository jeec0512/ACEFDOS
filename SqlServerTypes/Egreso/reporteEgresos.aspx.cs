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


public partial class Egreso_reporteEgresos : System.Web.UI.Page
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
        string ltipoSuc, lsucursal;
        DateTime lfechaInicio, lfechaFin;
        int tipo = (int)Session["STipo"];

        ltipoSuc = "";
        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        if (ltipo == "1")
        {
            if(tipo==4){
                laccion1 = "DETALLE";
                laccion2 = "TOTAL";
                grvEgresos.DataSource = dc.sp_RepEgresos(laccion1, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
                grvEgresos.DataBind();

                grvConsolidado.DataSource = dc.sp_RepTotEgresos(laccion2, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
                grvConsolidado.DataBind();
                }
            else
            {
                laccion1 = "DETSUC";
                laccion2 = "TOTSUC";
                grvEgresos.DataSource = dc.sp_RepEgresos(laccion1, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
                grvEgresos.DataBind();

                grvConsolidado.DataSource = dc.sp_RepTotEgresos(laccion2, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
                grvConsolidado.DataBind();
            }
        }

        if (ltipo == "2")
        {
            grvDetalleSuc.DataSource = dc.sp_RepxSucEgresos(laccion1, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvDetalleSuc.DataBind();

            grvConsolidado.DataSource = dc.sp_RepTotEgresos(laccion2, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvConsolidado.DataBind();
        }
    }

    protected void btnEgrTotal_Click(object sender, EventArgs e)
    {
        string laccion1, laccion2, ltipo;

       // pnEgresos.GroupingText = "Egresos por sucursal";

        ltipo = "1";
        laccion1 = "DETALLE";
        laccion2 = "TOTAL";

        listaReportes(ltipo, laccion1, laccion2);
        pnEgresos.Visible = true;
        pnConsolidado.Visible = true;
        pnDetalleSuc.Visible = false;

        btnExcelRA.Visible = true;
        btnExcelRS.Visible = false;
    }

    protected void btnEgrxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

       // pnEgresos.GroupingText = "Egresos por sucursal";

        ltipo = "2";
        laccion1 = "DETSUC";
        laccion2 = "TOTSUC";

        listaReportes(ltipo, laccion1, laccion2);
        pnEgresos.Visible = false;
        pnConsolidado.Visible = true;
        pnDetalleSuc.Visible = true;

        btnExcelRA.Visible = true;
        btnExcelRS.Visible = false;
    }

    #region EXCEL
    protected void btnExcelRA_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelRS_Click(object sender, EventArgs e)
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
            grvEgresos.AllowPaging = false;
            /// this.BindGrid();

            grvEgresos.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvEgresos.HeaderRow.Cells)
            {
                cell.BackColor = grvEgresos.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvEgresos.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvEgresos.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvEgresos.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvEgresos.RenderControl(hw);

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
            grvEgresos.AllowPaging = false;
            /// this.BindGrid();

            grvEgresos.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvEgresos.HeaderRow.Cells)
            {
                cell.BackColor = grvEgresos.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvEgresos.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvEgresos.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvEgresos.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvEgresos.RenderControl(hw);

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