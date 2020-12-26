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

public partial class Socio_listadosContratos : System.Web.UI.Page
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

    protected void btnMembresias_Click(object sender, EventArgs e)
    {
        string laccion, lsuc; ;
        DateTime lfecha;

        laccion = "XFECHA";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);
        lsuc = ddlSucursal2.SelectedValue.Trim();

        pnMembresias.Visible = true;
        pnTarjeta.Visible = false;
        pnGuia.Visible = false;

        btnExcelMem.Visible = true;
        btnExcelTar.Visible = false;
        btnExceGui.Visible = false;


        grvMembresias.DataSource = dc.sp_listaMembresias(laccion, lsuc, lfecha);
        grvMembresias.DataBind();
    }

    protected void btnTarjetas_Click(object sender, EventArgs e)
    {
        string laccion;
        DateTime lfecha;
        laccion = "NOENVIO";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);

        pnMembresias.Visible = false;
        pnTarjeta.Visible = true;
        pnGuia.Visible = false;

        btnExcelMem.Visible = false;
        btnExcelTar.Visible = true;
        btnExceGui.Visible = false;


        grvTarjeta.DataSource = dc.sp_listaTarjetasSocios(laccion, lfecha);
        grvTarjeta.DataBind();
    }

    protected void btnGuias_Click(object sender, EventArgs e)
    {
        string laccion;
        DateTime lfecha;
        laccion = "NOENVIO";
        lfecha = Convert.ToDateTime(txtFechaIni.Text);

        pnMembresias.Visible = false;
        pnTarjeta.Visible = false;
        pnGuia.Visible = true;

        btnExcelMem.Visible = false;
        btnExcelTar.Visible = false;
        btnExceGui.Visible = true;

        grvGuia.DataSource = dc.sp_listaGuias(laccion, lfecha);
        grvGuia.DataBind();
    }


    #region EXCEL
    protected void btnExcelMem_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelTar_Click(object sender, EventArgs e)
    {
        dos();
    }
    protected void btnExceGui_Click(object sender, EventArgs e)
    {
        tres();
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
            grvMembresias.AllowPaging = false;

            /// this.BindGrid();

            grvMembresias.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvMembresias.HeaderRow.Cells)
            {
                cell.BackColor = grvMembresias.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvMembresias.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvMembresias.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvMembresias.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvMembresias.RenderControl(hw);

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
            grvTarjeta.AllowPaging = true;
            /// this.BindGrid();

            grvTarjeta.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvTarjeta.HeaderRow.Cells)
            {
                cell.BackColor = grvTarjeta.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvTarjeta.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvTarjeta.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvTarjeta.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvTarjeta.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    protected void tres()
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
            grvGuia.AllowPaging = false;
            /// this.BindGrid();

            grvGuia.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvGuia.HeaderRow.Cells)
            {
                cell.BackColor = grvGuia.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvGuia.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvGuia.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvGuia.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvGuia
                .RenderControl(hw);

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