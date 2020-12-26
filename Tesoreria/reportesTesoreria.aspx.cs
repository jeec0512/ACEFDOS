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

public partial class Tesoreria_reportesTesoreria : System.Web.UI.Page
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
       pnRecaudacion.Visible = false;
       pnDetalleSuc.Visible = false;
       pnConsolidado.Visible = false;
    }

    #endregion

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
            grvRecaudacion.DataSource = dc.sp_RepTesoreria(laccion1, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvRecaudacion.DataBind();

            grvConsolidado.DataSource = dc.sp_RepTotTesoreria(laccion2, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvConsolidado.DataBind();
        }

        if (ltipo == "2")
        {
            grvDetalleSuc.DataSource = dc.sp_RepxSucTesoreria(laccion1, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvDetalleSuc.DataBind();

            grvConsolidado.DataSource = dc.sp_RepTotTesoreria(laccion2, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvConsolidado.DataBind();
        }
    }


    protected void btnRecTotal_Click(object sender, EventArgs e)
    {

        string laccion1, laccion2, ltipo;
        int tipo = (int)Session["STipo"];
        //pnRecaudacion.GroupingText = "Recaudación por sucursal";
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
        pnRecaudacion.Visible = true;
        pnConsolidado.Visible = true;
        pnDetalleSuc.Visible = false;
    }

    protected void btnRecxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        //pnRecaudacion.GroupingText = "Recaudación por sucursal";

        ltipo = "2";
        laccion1 = "DETSUC";
        laccion2 = "TOTSUC";

        listaReportes(ltipo, laccion1, laccion2);
        pnRecaudacion.Visible = false;
        pnConsolidado.Visible = true;
        pnDetalleSuc.Visible = true;

        btnExcelRA.Visible = false;
        btnExcelRS.Visible = true;

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
            grvRecaudacion.AllowPaging = false;
            /// this.BindGrid();

            grvRecaudacion.HeaderRow.BackColor = Color.White;

            foreach (TableCell cell in grvRecaudacion.HeaderRow.Cells)
            {
                cell.BackColor = grvRecaudacion.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvRecaudacion.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvRecaudacion.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvRecaudacion.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvRecaudacion.RenderControl(hw);

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
            grvDetalleSuc.AllowPaging = false;
            /// this.BindGrid();

            grvDetalleSuc.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetalleSuc.HeaderRow.Cells)
            {
                cell.BackColor = grvDetalleSuc.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetalleSuc.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetalleSuc.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetalleSuc.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetalleSuc.RenderControl(hw);

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