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

public partial class Cartera_carteraIngresos : System.Web.UI.Page
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
        btnExcelFe.Visible = true;
        btnExcelC.Visible = false;
    }

    private void cargaGridTodos()
    {
        string lsucursal = ddlSucursal2.SelectedValue;
        grvListadoFac.DataSource = dc.sp_cxcSucursal("XSUC", lsucursal, 2017); // dc.sp_cxcSucursales("TODOS","",2017);
        grvListadoFac.DataBind();

    }

    

    protected void btnConsolidado_Click(object sender, EventArgs e)
    {
        string lsucursal = ddlSucursal2.SelectedValue;

        pnListado.Visible = false;
        pnConsolidado.Visible = true;

        try
        {

           
            int tipo = (int)Session["STipo"];



            if (tipo == 4)
            {
                grvConsolidado.DataSource = dc.sp_cxcSucursales("TODOS", "",2017);
                grvConsolidado.DataBind();


            }
            else
            {
                grvConsolidado.DataSource = dc.sp_cxcSucursales("XSUC", lsucursal, 2017);
                grvConsolidado.DataBind();
            }

            btnExcelFe.Visible = false;
            btnExcelC.Visible = true;
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

    #region AEXEL
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