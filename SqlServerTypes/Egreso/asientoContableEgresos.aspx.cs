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

public partial class Egreso_asientoContableEgresos : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();



        }
    }
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

    protected void btnAsiento_Click(object sender, EventArgs e)
    {
        string laccion = "DETALLE";
        string lsucursal = ddlSucursal2.SelectedValue.Trim();
        string caja = ddlCaja.SelectedValue;
        DateTime lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lfechaFin = Convert.ToDateTime(txtFechaFin.Text);

       // grvContabilizacion.DataSource = dc.sp_verContabilizacionGastos(laccion, lfechaInicio, lfechaFin, lsucursal);
        grvContabilizacion.DataSource = dc.sp_verContabilizacionGastosDocsSRI(laccion, lfechaInicio, lfechaFin, lsucursal,caja);
        grvContabilizacion.DataBind();


        laccion = "TOTAL";
        //grvCuadre.DataSource = dc.sp_verContabilizacionGastos(laccion, lfechaInicio, lfechaFin, lsucursal);
        grvCuadre.DataSource = dc.sp_verContabilizacionGastosDocsSRI(laccion, lfechaInicio, lfechaFin, lsucursal,caja);
        grvCuadre.DataBind();
    }


    /********************************EXCEL*******************************/
    #region AEXCEL

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    
    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
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


    #endregion
}