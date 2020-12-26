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

public partial class Tributacion_anexoSri : System.Web.UI.Page
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
            //activarObjetos();
        }
    }

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
    #endregion
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        string accion = string.Empty;
        string sucursal = ddlSucursal2.SelectedValue;
        int ano = Convert.ToInt32(ddlAno.SelectedValue);
        int periodo = Convert.ToInt32(ddlMes.SelectedValue);

        var cSri = dc.sp_anexoSRI(accion, ano, periodo, sucursal);

        grvEgresosDetalle.DataSource = cSri;
        grvEgresosDetalle.DataBind();

    }

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
            grvEgresosDetalle.AllowPaging = false;
            /// this.BindGrid();

            grvEgresosDetalle.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvEgresosDetalle.HeaderRow.Cells)
            {
                cell.BackColor = grvEgresosDetalle.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvEgresosDetalle.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvEgresosDetalle.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvEgresosDetalle.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvEgresosDetalle.RenderControl(hw);

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