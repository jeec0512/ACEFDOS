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

public partial class Herramienta_activarCajaVentas : System.Web.UI.Page
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

    protected void btnEstadoSuc_Click(object sender, EventArgs e)
    {
        btnEstadoSuc_Click();
    }

    protected void btnEstadoSuc_Click()
    {
        string laccion;
        string ltipoSuc, lsucursal;
        DateTime lfechaInicio, lfechaFin;

        pnEstadoSuc.Visible = true;

        ltipoSuc = "";
        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);

       // pnEstadoSuc.GroupingText = "Estado de cierre de la sucursal: " + lsucursal;

        laccion = "CIERREXSUC";

        grvEstadoSuc.DataSource = dc.sp_ctrlCierresxSuc3(laccion, ltipoSuc, lsucursal, lfechaInicio, lfechaFin);
        grvEstadoSuc.DataBind();

    }

    protected void grvEstadoSuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        int tipo = (int)Session["STipo"];

        int lid = Convert.ToInt32(grvEstadoSuc.SelectedValue);

        try
        {
            if (tipo == 4)
            {
                if (lid == 0 )
                {

                }
                else
                {
                    tbl_CabRecaudacion tbl_CabRecaudacion = dc.tbl_CabRecaudacion.SingleOrDefault(x => x.id_cab_recaudacion == lid);
                    tbl_CabRecaudacion.ESTADO = "0";
                }
                dc.SubmitChanges();
                btnEstadoSuc_Click();
            }
            else
            {
                lblMensaje.Text = "No tiene permisos para activar cajas";
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

    #region EXCEL
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    

    protected void btnExcexSuc_Click(object sender, EventArgs e)
    {
        dos();
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
            grvEstadoSuc.AllowPaging = false;
            /// this.BindGrid();

            grvEstadoSuc.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvEstadoSuc.HeaderRow.Cells)
            {
                cell.BackColor = grvEstadoSuc.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvEstadoSuc.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvEstadoSuc.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvEstadoSuc.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvEstadoSuc.RenderControl(hw);

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