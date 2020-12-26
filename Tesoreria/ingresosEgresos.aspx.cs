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



public partial class Tesoreria_ingresosEgresos : System.Web.UI.Page
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

    protected void btnEstadoSuc_Click(object sender, EventArgs e)
    {
        DateTime lFechaInicio, lFechaFin;

        lFechaInicio = DateTime.Now;
        lFechaFin = DateTime.Now;

        string lsuc, laccion;
        //usuario = Convert.ToString(Session["UsuarioID"]);

        lsuc = ddlSucursal2.SelectedValue;
        lFechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lFechaFin = Convert.ToDateTime(txtFechaFin.Text);
        laccion = "AEXCEL";

        /*if (usuario == "" || usuario == null)
        {
            Response.Redirect("~/ingresar.aspx");
        }*/
        grvCierreCaja.DataSource = dc.sp_RepIngresosEgresos(laccion, lFechaInicio, lFechaFin, lsuc);
        grvCierreCaja.DataBind();
    }

    protected void btnExceSuc_Click(object sender, EventArgs e)
    {
        uno();
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

        Session["pFechaInicio"] = lfechaInicio;
        Session["pFechaFin"] = lfechaFin;
        Session["pSuc"] = lsuc;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('repIngEgr.aspx','','width=800,height=500') </script>");
    }


    #region EXCEL
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
            grvCierreCaja.AllowPaging = false;
            /// this.BindGrid();

            grvCierreCaja.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvCierreCaja.HeaderRow.Cells)
            {
                cell.BackColor = grvCierreCaja.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvCierreCaja.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvCierreCaja.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvCierreCaja.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvCierreCaja.RenderControl(hw);

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