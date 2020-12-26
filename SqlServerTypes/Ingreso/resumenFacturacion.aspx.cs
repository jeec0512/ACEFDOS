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


public partial class Ingreso_resumenFacturacion : System.Web.UI.Page
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
        pnDetalleFac.Visible = false;
        pnTotalesFacturacion.Visible = false;
        pnResulItem.Visible = false;
        pnTotalProductos.Visible = false;
        pnContabilizacion.Visible = false;
        pnCuadre.Visible = false;
    }

    #endregion

    protected void btnFacxFecxSuc_Click(object sender, EventArgs e)
    {
        try{
        string laccion;
        string lsucursal;
        DateTime lfechaInicio, lfechaFin;

        pnDetalleFac.Visible = true;
        pnTotalesFacturacion.Visible = true;
        pnResulItem.Visible = false;
        pnTotalProductos.Visible = false;
        pnContabilizacion.Visible = false;
        pnCuadre.Visible = false;

        laccion = "DETALLE";



        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        grvDetalleFac.DataSource = dc.sp_FacturasEmitidas(laccion, lsucursal, lfechaInicio, lfechaFin);
        grvDetalleFac.DataBind();

        laccion = "TOTAL";

        grvTotalesFacturacion.DataSource = dc.sp_FacturasEmitidas(laccion, lsucursal, lfechaInicio, lfechaFin);
        grvTotalesFacturacion.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "No existe datos: " + ex.Message.Trim();
        }

    }

    protected void btnItemVendidos_Click(object sender, EventArgs e)
    {
        try{
        string laccion;
        string lsucursal;
        DateTime lfechaInicio, lfechaFin;

        pnDetalleFac.Visible = false;
        pnTotalesFacturacion.Visible = false;
        pnResulItem.Visible = true;
        pnTotalProductos.Visible = true;
        pnContabilizacion.Visible = false;
        pnCuadre.Visible = false;

        laccion = "DETALLE";



        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        grvResulItem.DataSource = dc.sp_DetalleFacturacion(laccion, lsucursal, lfechaInicio, lfechaFin);
        grvResulItem.DataBind();



        laccion = "TOTAL";
        grvTotalProductos.DataSource = dc.sp_DetalleFacturacion(laccion, lsucursal, lfechaInicio, lfechaFin);
        grvTotalProductos.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "No existe datos: " + ex.Message.Trim();
        }


    }

    protected void btnContabilizacion_Click(object sender, EventArgs e)
    {
        try
        {
            string laccion;
            string lsucursal;
            DateTime lfechaInicio, lfechaFin;

            pnDetalleFac.Visible = false;
            pnTotalesFacturacion.Visible = false;
            pnResulItem.Visible = false;
            pnTotalProductos.Visible = false;
            pnContabilizacion.Visible = true;
            pnCuadre.Visible = true;

            laccion = "DETALLE";



            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
            grvContabilizacion.DataSource = dc.sp_verContabilizacionVentas(laccion, lfechaInicio, lfechaFin, lsucursal);
            grvContabilizacion.DataBind();


            laccion = "TOTAL";
            grvCuadre.DataSource = dc.sp_verContabilizacionVentas(laccion, lfechaInicio, lfechaFin, lsucursal);
            grvCuadre.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "No existe datos: "+ex.Message.Trim();
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        DateTime esteDia = DateTime.Today;
        DateTime lfechaInicio, lfechaFin;

        //string lsuc,lfechaInicio,lfechaFin;

        string lsuc;

        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;
        lsuc = "";

        //dc.sp_repCierraCaja(lfechaInicio,lfechaFin,lsuc);

        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        lsuc = ddlSucursal2.Text.Trim();

        if (comprobarCierre(lfechaInicio, lfechaFin, lsuc))
        {
            Session["pFechaInicio"] = lfechaInicio;
            Session["pFechaFin"] = lfechaFin;
            Session["pSuc"] = lsuc;

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirAsientoIngresos.aspx','','width=800,height=500') </script>");
        }
    }


    protected bool comprobarCierre(DateTime lfechaInicio, DateTime lfechaFin, string lsuc)
    {
        bool pasa;
        
        System.Int32 kont = (from mkont in dc.tbl_CabRecaudacion
                             where mkont.SUCURSAL == lsuc
                                 && mkont.FECHA >= lfechaInicio
                                 && mkont.FECHA <= lfechaFin
                                 && (mkont.ESTADO == "0"
                                 || mkont.ESTADO == "1")
                             select mkont).Count();


        if (kont > 0 )
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = "NO PUEDE REALIZAR LA IMPRESIÓN, NO HA CERRADO TODAS LAS CAJAS EN ESTE RANGO DE FECHAS";
            pasa = false;
        }
        else
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = string.Empty;
            pasa = true;
        }
        return pasa;
    }

    #region EXCEL
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void uno()
    {
        try
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
                grvDetalleFac.AllowPaging = false;
                /// this.BindGrid();

                grvDetalleFac.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvDetalleFac.HeaderRow.Cells)
                {
                    cell.BackColor = grvDetalleFac.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvDetalleFac.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvDetalleFac.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvDetalleFac.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvDetalleFac.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }
    }


    protected void dos()
    {
        try
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
                grvResulItem.AllowPaging = true;
                /// this.BindGrid();

                grvResulItem.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvResulItem.HeaderRow.Cells)
                {
                    cell.BackColor = grvResulItem.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvResulItem.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvResulItem.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvResulItem.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvResulItem.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }
    }


    protected void tres()
    {
        try
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
                grvContabilizacion.AllowPaging = false;
                /// this.BindGrid();

                grvContabilizacion.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvContabilizacion.HeaderRow.Cells)
                {
                    cell.BackColor = grvContabilizacion.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvContabilizacion.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvContabilizacion.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvContabilizacion.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvContabilizacion.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }
    }


    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelRf_Click(object sender, EventArgs e)
    {
        dos();
    }

    protected void btnExcelC_Click(object sender, EventArgs e)
    {
        tres();
    }
    #endregion
}