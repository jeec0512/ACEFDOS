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

public partial class Tesoreria_corrientes : System.Web.UI.Page
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
        pnDetalleDep.Visible = false;
        pnTotalDep.Visible = false;
    }

    #endregion

    protected void btnDepositos_Click(object sender, EventArgs e)
    {
        string laccion;
        string lsucursal;
        DateTime lfechaInicio, lfechaFin;
        int tipo = (int)Session["STipo"];
        pnDetalleDep.Visible = true;
        pnTotalDep.Visible = true;

     

     //   pnDetalleDep.GroupingText = "Depósitos";

        if(tipo==4)
        {
        laccion = "DETALLE";



        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        grvDetalleDep.DataSource = dc.sp_Depositos(laccion, "", lsucursal, lfechaInicio, lfechaFin);
        grvDetalleDep.DataBind();

        laccion = "TOTAL";

        grvTotalDep.DataSource = dc.sp_TotalesDepositos(laccion, "", lsucursal, lfechaInicio, lfechaFin);
        grvTotalDep.DataBind();
        }
        else
        {
            laccion = "SUCDET";



            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
            grvDetalleDep.DataSource = dc.sp_Depositos(laccion, "", lsucursal, lfechaInicio, lfechaFin);
            grvDetalleDep.DataBind();

            laccion = "TOTSUC";

            grvTotalDep.DataSource = dc.sp_TotalesDepositos(laccion, "", lsucursal, lfechaInicio, lfechaFin);
            grvTotalDep.DataBind();
        }


    }

    protected void btnDepxSuc_Click(object sender, EventArgs e)
    {
        string laccion;
        string lsucursal;
        DateTime lfechaInicio, lfechaFin;

        pnDetalleDep.Visible = true;
        pnTotalDep.Visible = true;

       

       // pnDetalleDep.GroupingText = "Depósitos";

        laccion = "SUCDET";



        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        grvDetalleDep.DataSource = dc.sp_Depositos(laccion, "", lsucursal, lfechaInicio, lfechaFin);
        grvDetalleDep.DataBind();

        laccion = "SUCTOT";

        grvTotalDep.DataSource = dc.sp_TotalesDepositos(laccion, "", lsucursal, lfechaInicio, lfechaFin);
        grvTotalDep.DataBind();
    }


    #region EXCEL
    protected void btnExcelDA_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelDS_Click(object sender, EventArgs e)
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
            grvDetalleDep.AllowPaging = false;
            /// this.BindGrid();

            grvDetalleDep.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetalleDep.HeaderRow.Cells)
            {
                cell.BackColor = grvDetalleDep.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetalleDep.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetalleDep.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetalleDep.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetalleDep.RenderControl(hw);

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
            grvDetalleDep.AllowPaging = false;
            /// this.BindGrid();

            grvDetalleDep.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetalleDep.HeaderRow.Cells)
            {
                cell.BackColor = grvDetalleDep.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetalleDep.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetalleDep.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetalleDep.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetalleDep.RenderControl(hw);

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