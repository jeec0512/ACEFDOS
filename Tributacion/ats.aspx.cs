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

public partial class Tributacion_ats : System.Web.UI.Page
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
        string ano = ddlAno.SelectedValue;
        string periodo = ddlMes.SelectedValue;

        lblMensaje.Visible = true;
        //var cSri = dc.sp_creacionDocumentoATS(ano, periodo);
        lblMensaje.Text = "Verificando datos";
        dc.sp_atsBorrar(ano, periodo);
        lblMensaje.Text = "Exportando del financiero";
        dc.sp_atsNova(ano, periodo);
        /*lblMensaje.Text = "Exportando del acefdos";
        dc.sp_atsAcefdos(ano, periodo);
        lblMensaje.Text = "Formateando tablas ATS";
        dc.sp_atsFormato(ano, periodo);
        lblMensaje.Text = "";*/

        

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

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        dos();
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
           grvCabeceraAts.AllowPaging = false;
            /// this.BindGrid();

           grvCabeceraAts.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvCabeceraAts.HeaderRow.Cells)
            {
                cell.BackColor =grvCabeceraAts.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvCabeceraAts.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor =grvCabeceraAts.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor =grvCabeceraAts.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

           grvCabeceraAts.RenderControl(hw);

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
            grvDetalleATS.AllowPaging = false;
            /// this.BindGrid();

            grvDetalleATS.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetalleATS.HeaderRow.Cells)
            {
                cell.BackColor = grvDetalleATS.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetalleATS.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetalleATS.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetalleATS.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetalleATS.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    #endregion
   
    protected void btnListar_Click(object sender, EventArgs e)
    {
        string ano = ddlAno.SelectedValue;
        string periodo = llenarCeros(ddlMes.SelectedValue.Trim(), '0', 2);
        lblMensaje.Text = periodo + "    "+ano;

       var cabAts = dc.sp_listarCabeceraAts(ano, periodo);
       grvCabeceraAts.DataSource = cabAts;
       grvCabeceraAts.DataBind();

       var detAts = dc.sp_listarDetalleAts(ano, periodo);
       grvDetalleATS.DataSource = detAts;
       grvDetalleATS.DataBind();
    }


    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
    
}