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

public partial class Contabilidad_libroDiario : System.Web.UI.Page
{
    
    #region VARIABLES GENERALES
    decimal debe = 0;
    decimal haber = 0;
    decimal saldo = 0;
    #endregion
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

            var cTipoDoc = dc.sp_ListarTipoDoc("");
            ddlTipoDocumento.DataSource = cTipoDoc;
            ddlTipoDocumento.DataBind();


            // var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);
            //ddlSucursal2.DataSource = cSucursal;
            // ddlSucursal2.DataBind();
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
        pnDatos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    protected bool fechaValida()
    {
        bool fechaValida = false;
        var date1 = txtFechaIni.Text;

        DateTime dt1 = DateTime.Now;
        DateTime dt2 = dt1;

        var culture = CultureInfo.CreateSpecificCulture("es-MX");
        var styles = DateTimeStyles.None;


        fechaValida = DateTime.TryParse(date1, culture, styles, out dt1);

        return fechaValida;
    }
    #endregion


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        btnConsultar_Click();
    }

    protected void btnConsultar_Click()
    {
        string lAccion;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;

        lAccion = "CONSULTAR";

        lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        lFechaFin = Convert.ToDateTime(txtFechaFin.Text);

        var cDocumentos = dc.sp_libroDiarioTotal_V2(lAccion, lFechaIni, lFechaFin,sucursal);
        
        grvDocumentoCabecera.DataSource = cDocumentos;

        grvDocumentoCabecera.DataBind();

        pnDocumentos.Visible = true;

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
    protected void btnExcelDet_Click(object sender, EventArgs e)
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
            grvDocumentoCabecera.AllowPaging = false;
            /// this.BindGrid();

            grvDocumentoCabecera.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDocumentoCabecera.HeaderRow.Cells)
            {
                cell.BackColor = grvDocumentoCabecera.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDocumentoCabecera.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDocumentoCabecera.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDocumentoCabecera.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDocumentoCabecera.RenderControl(hw);

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
            grvDiarios.AllowPaging = false;
            /// this.BindGrid();

            grvDiarios.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDiarios.HeaderRow.Cells)
            {
                cell.BackColor = grvDiarios.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDiarios.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDiarios.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDiarios.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDiarios.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

       #endregion

    #region SUMA DE VALORES
    protected void grvDocumentoCabecera_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[3].Text);


            debe += Convert.ToDecimal(e.Row.Cells[3].Text);
            haber += Convert.ToDecimal(e.Row.Cells[4].Text);
            saldo += Convert.ToDecimal(e.Row.Cells[5].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Totales";

            e.Row.Cells[3].Text = Convert.ToString(debe);
            e.Row.Cells[4].Text = Convert.ToString(haber);
            e.Row.Cells[5].Text = Convert.ToString(saldo);

        }
    }

    protected void grvDiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[3].Text);


            debe += Convert.ToDecimal(e.Row.Cells[4].Text);
            haber += Convert.ToDecimal(e.Row.Cells[5].Text);
            saldo += Convert.ToDecimal(e.Row.Cells[4].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Totales";

            e.Row.Cells[4].Text = Convert.ToString(debe);
            e.Row.Cells[5].Text = Convert.ToString(haber);
            e.Row.Cells[6].Text = Convert.ToString(debe-haber);

        }
    }
    #endregion

    #region PARA SELECCIONAR UN REGISTRO
    protected void grvContabilizacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCuenta.Text = Convert.ToString(grvDiarios.SelectedValue).Trim();
        ibConsultar_Click();
        pnDocumentos.Visible = false;
        pnDiarios.Visible = true;


    }




    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        pnDocumentos.Visible = true;
        pnDiarios.Visible = false;

    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    protected void ibConsultar_Click()
    {

        btnConsultarMayor_Click();

    }

    

    protected void btnConsultarMayor_Click()
    {
        string lAccion;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;
        int cuenta = Convert.ToInt32(lblCuenta.Text);
        lAccion = "CONSULTAR";

        lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        lFechaFin = Convert.ToDateTime(txtFechaFin.Text);

        var cDocumentos = dc.sp_libroDiarioxId_V2(lAccion, lFechaIni, lFechaFin, sucursal, cuenta);

        grvDiarios.DataSource = cDocumentos;
        grvDiarios.DataBind();

        pnDocumentos.Visible = true;

    }
    #endregion


    protected void grvDocumentoCabecera_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCuenta.Text = Convert.ToString(grvDocumentoCabecera.SelectedValue).Trim();
        ibConsultar_Click();
        pnDocumentos.Visible = false;
        pnDiarios.Visible = true;
    }
    
    protected void btnRegresar_Click1(object sender, EventArgs e)
    {
        pnDocumentos.Visible = true;
        pnDiarios.Visible = false;
    }
}