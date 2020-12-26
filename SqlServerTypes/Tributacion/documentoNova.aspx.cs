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
public partial class Tributacion_documentoNova : System.Web.UI.Page
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

        lAccion = "CONSULTAR";

        lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        lFechaFin = Convert.ToDateTime(txtFechaFin.Text);

        var cDocumentos = dc.sp_ListarDocumnetoNova(lAccion, tipoDoc, lFechaIni, lFechaFin);

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


    #endregion
}