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


public partial class Cartera_estadoProveedor : System.Web.UI.Page
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

            //DateTime lfecha = DateTime.Today;
            //txtFechaIni.Text = Convert.ToString(lfecha);
            //txtFechaFin.Text = Convert.ToString(lfecha);

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

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string lbuscar, lopcion, laccion, lcedula;

        laccion = "";

        lopcion = ddlTipoBusqueda.SelectedValue.Trim();

        lbuscar = txtBuscar.Text.Trim();

        lcedula = lbuscar;

        if (lopcion == "0")
        {
            ///lista clientes q cumplen condicipon de cédula
            ///
            laccion = "XRUC";
            grvProveedores.DataSource = dc.sp_buscaProveedor(laccion, lbuscar);

            grvProveedores.DataBind();

        }

        if (lopcion == "1")
        {
            laccion = "XNOMBRE";


            grvProveedores.DataSource = dc.sp_buscaProveedor(laccion, lbuscar);

            grvProveedores.DataBind();
        }
    }

    protected void grvProveedores_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lcedula;
        string sucursal = ddlSucursal2.SelectedValue;
        int tipo = (int)Session["STipo"];
        try
        {

        if (tipo == 4)
        {
            lcedula = "";
            laccion = "TODOS";

            ///lista las facturas emitidas
            ///
            lcedula = Convert.ToString(grvProveedores.SelectedValue);

            grvFacturasEmitidas.DataSource = dc.sp_buscaPagosProveedor2(laccion, sucursal, lcedula);
            grvFacturasEmitidas.DataBind();
        }
        else
        {

            lcedula = "";
            laccion = "XRUC";

            ///lista las facturas emitidas
            ///
            lcedula = Convert.ToString(grvProveedores.SelectedValue);

            grvFacturasEmitidas.DataSource = dc.sp_buscaPagosProveedor2(laccion, sucursal, lcedula);
            grvFacturasEmitidas.DataBind();
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


    protected void btnExcelSS_Click(object sender, EventArgs e)
    {

        dos();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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
            grvFacturasEmitidas.AllowPaging = false;
            /// this.BindGrid();

            grvFacturasEmitidas.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvFacturasEmitidas.HeaderRow.Cells)
            {
                cell.BackColor = grvFacturasEmitidas.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvFacturasEmitidas.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvFacturasEmitidas.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvFacturasEmitidas.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvFacturasEmitidas.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}