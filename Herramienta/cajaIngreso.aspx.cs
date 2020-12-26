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


public partial class Herramienta_cajaIngreso : System.Web.UI.Page
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
        pnTitulos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    #endregion

    protected void btnTodos_Click(object sender, EventArgs e)
    {
        pnListado.Visible = true;
        pnConsolidado.Visible = false;
        btnTodos_Click();
    }

    protected void btnTodos_Click()
    {

        cargaGridTodos();

    }

    private void cargaGridTodos()
    {
        lblMensaje.Text = string.Empty;
        try
        {
            string laccion = "CIERREXSUC";
            string ltipoSuc = "";
            string lsucursal = ddlSucursal2.SelectedValue.Trim();
            DateTime lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
            DateTime lfechaFin = Convert.ToDateTime(txtFechaFin.Text);


            grvListadoFac.DataSource = dc.sp_ctrlCierresxSuc2(laccion, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
            grvListadoFac.DataBind();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;

        }
        finally
        {

        }

    }

    protected void btnConsolidado_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = string.Empty;
        DateTime lfechainicio, lfechafin;
        string lsucursal = ddlSucursal2.SelectedValue;

        pnListado.Visible = false;
        pnConsolidado.Visible = true;

        try
        {

            lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechafin = Convert.ToDateTime(txtFechaFin.Text);
            int tipo = (int)Session["STipo"];



            if (tipo == 4)
            {
                grvConsolidado.DataSource = dc.sp_cajasIngresos("TODOS", lsucursal, lfechainicio, lfechafin);
                grvConsolidado.DataBind();


            }
            else
            {
                grvConsolidado.DataSource = dc.sp_cajasIngresos("XSUC", lsucursal, lfechainicio, lfechafin);
                grvConsolidado.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;

        }
        finally
        {

        }

    }

    protected void btnFacturasAneta_Click(object sender, EventArgs e)
    {

    }

    #region SEMAFORO
    protected void grvConsolidado_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        /*if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Visible = false;
        }else*/

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if (e.Row.Cells[9].Text.CompareTo("1") == 0)
            {
                //e.Row.Cells[5].CssClass = "Rojo";
                e.Row.CssClass = "Rojo";
            }
            else if (e.Row.Cells[9].Text.CompareTo("2") == 0)
            {
                //e.Row.Cells[5].CssClass = "Verde";
                e.Row.CssClass = "Amarillo";

            }
            else if (e.Row.Cells[9].Text.CompareTo("3") == 0)
            {
                //e.Row.Cells[5].CssClass = "Verde";
                e.Row.CssClass = "Verde";

            }

            // e.Row.Cells[5].Visible = false;
        }
    }

    protected void grvListadoFac_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if (e.Row.Cells[3].Text.CompareTo("NO CERRADO") == 0)
            {
                //e.Row.Cells[5].CssClass = "Rojo";
                e.Row.CssClass = "Rojo";
            }
            else
                if (e.Row.Cells[3].Text.CompareTo("CERRADO") == 0)
                {
                    //e.Row.Cells[5].CssClass = "Rojo";
                    e.Row.CssClass = "Amarillo";
                }
                else
                    if (e.Row.Cells[3].Text.CompareTo("DEPOSITADO") == 0)
                    {
                        //e.Row.Cells[5].CssClass = "Rojo";
                        e.Row.CssClass = "Verde";
                    }
                    else
                        if (e.Row.Cells[3].Text.CompareTo("SIN REGISTRAR") == 0)
                        {
                            //e.Row.Cells[5].CssClass = "Rojo";
                            e.Row.CssClass = "Rojo";
                        }

        }
    }
    #endregion

    #region EXCEL
    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelC_Click(object sender, EventArgs e)
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
            grvListadoFac.AllowPaging = false;
            ///this.retornaTodos();

            grvListadoFac.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvListadoFac.HeaderRow.Cells)
            {
                cell.BackColor = grvListadoFac.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvListadoFac.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvListadoFac.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvListadoFac.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvListadoFac.RenderControl(hw);

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
            grvConsolidado.AllowPaging = false;
            /// this.BindGrid();

            grvConsolidado.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvConsolidado.HeaderRow.Cells)
            {
                cell.BackColor = grvConsolidado.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvConsolidado.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvConsolidado.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvConsolidado.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvConsolidado.RenderControl(hw);

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