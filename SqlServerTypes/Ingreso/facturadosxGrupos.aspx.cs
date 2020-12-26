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

public partial class Ingreso_facturadosxGrupos : System.Web.UI.Page
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

    protected void btnTodos_Click(object sender, EventArgs e)
    {
        pnListado.Visible = true;
        pnConsolidado.Visible = false;
        btnTodos_Click();
    }

    protected void btnTodos_Click()
    {

        cargaGridTodos();
       
    }

    private void cargaGridTodos()
    {
        string accion = "SUC";
        DateTime lfechainicio, lfechafin;
        string lsucursal;
        

        lsucursal = ddlSucursal2.SelectedValue.Trim();

        lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechafin = Convert.ToDateTime(txtFechaFin.Text);

        grvListadoFac.DataSource = dc.sp_ListarGrupoItemxSucursal(accion, lsucursal, lfechainicio, lfechafin);//retornaTodos();
        grvListadoFac.DataBind();

    }

    private List<facturas> retornaTodos()
    {
        var facturas = new facturas();

        List<facturas> lista = new List<facturas>();

        DateTime lfechainicio, lfechafin;
        string usuario, Username, lsucursal;
        int ltipoConcepto = 0;

        usuario = Convert.ToString(Session["UsuarioID"]);
        Username = Convert.ToString(Session["Usuarioname"]);

        //lsucursal = Username.Substring(0, 3);

        lsucursal = ddlSucursal2.SelectedValue.Trim();

        lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechafin = Convert.ToDateTime(txtFechaFin.Text);

        var consultaFAC = from f in df.FACTURA
                          where f.FAC_SUCURSAL == lsucursal
                            && f.FAC_TIPOCONCEPTO == ltipoConcepto
                            && f.FAC_FECHAEMISION >= lfechainicio
                            && f.FAC_FECHAEMISION <= lfechafin
                          orderby f.FAC_SECUENCIAL
                          select new
                          {
                              FAC_SUCURSAL = f.FAC_SUCURSAL,
                              FAC_SECUENCIAL = f.FAC_SECUENCIAL,
                              FAC_FECHAEMISION = f.FAC_FECHAEMISION,
                              FAC_RAZONCOMPRADOR = f.FAC_RAZONCOMPRADOR,
                              FAC_TOTALSINIMP = f.FAC_TOTALSINIMP,
                              FAC_TOTALDESCUENTO = f.FAC_TOTALDESCUENTO,
                              FAC_BASEIMPONIBLE = f.FAC_BASEIMPONIBLE,
                              FAC_VALORIMPUESTO = f.FAC_VALORIMPUESTO,
                              FAC_IMPORTETOTAL = f.FAC_IMPORTETOTAL,
                              FAC_VENDEDOR = f.FAC_VENDEDOR,
                              FAC_TIPOCONCEPTO = f.FAC_TIPOCONCEPTO,
                              FAC_OBSERVACION = f.FAC_OBSERVACION,
                              FAC_SRI = f.FAC_SRI
                          };

        if (ltipoConcepto != 0)
        {
            consultaFAC = from f in df.FACTURA
                          where f.FAC_SUCURSAL == lsucursal
                            && f.FAC_TIPOCONCEPTO == ltipoConcepto
                            && f.FAC_FECHAEMISION >= lfechainicio
                            && f.FAC_FECHAEMISION <= lfechafin
                          orderby f.FAC_SECUENCIAL
                          select new
                          {
                              FAC_SUCURSAL = f.FAC_SUCURSAL,
                              FAC_SECUENCIAL = f.FAC_SECUENCIAL,
                              FAC_FECHAEMISION = f.FAC_FECHAEMISION,
                              FAC_RAZONCOMPRADOR = f.FAC_RAZONCOMPRADOR,
                              FAC_TOTALSINIMP = f.FAC_TOTALSINIMP,
                              FAC_TOTALDESCUENTO = f.FAC_TOTALDESCUENTO,
                              FAC_BASEIMPONIBLE = f.FAC_BASEIMPONIBLE,
                              FAC_VALORIMPUESTO = f.FAC_VALORIMPUESTO,
                              FAC_IMPORTETOTAL = f.FAC_IMPORTETOTAL,
                              FAC_VENDEDOR = f.FAC_VENDEDOR,
                              FAC_TIPOCONCEPTO = f.FAC_TIPOCONCEPTO,
                              FAC_OBSERVACION = f.FAC_OBSERVACION,
                              FAC_SRI = f.FAC_SRI
                          };
        }
        else
        {
            consultaFAC = from f in df.FACTURA
                          where f.FAC_SUCURSAL == lsucursal
                            && f.FAC_FECHAEMISION >= lfechainicio
                            && f.FAC_FECHAEMISION <= lfechafin
                          orderby f.FAC_SECUENCIAL
                          select new
                          {
                              FAC_SUCURSAL = f.FAC_SUCURSAL,
                              FAC_SECUENCIAL = f.FAC_SECUENCIAL,
                              FAC_FECHAEMISION = f.FAC_FECHAEMISION,
                              FAC_RAZONCOMPRADOR = f.FAC_RAZONCOMPRADOR,
                              FAC_TOTALSINIMP = f.FAC_TOTALSINIMP,
                              FAC_TOTALDESCUENTO = f.FAC_TOTALDESCUENTO,
                              FAC_BASEIMPONIBLE = f.FAC_BASEIMPONIBLE,
                              FAC_VALORIMPUESTO = f.FAC_VALORIMPUESTO,
                              FAC_IMPORTETOTAL = f.FAC_IMPORTETOTAL,
                              FAC_VENDEDOR = f.FAC_VENDEDOR,
                              FAC_TIPOCONCEPTO = f.FAC_TIPOCONCEPTO,
                              FAC_OBSERVACION = f.FAC_OBSERVACION,
                              FAC_SRI = f.FAC_SRI
                          };
        }

        int kont = 0;
        decimal totalDescuento = 0, totalPrecio = 0, totalIva = 0, total = 0;

        if (consultaFAC.Count() == 0)
        {

        }
        else
        {
            foreach (var registro in consultaFAC)
            {

                totalDescuento = totalDescuento + registro.FAC_TOTALDESCUENTO;
                totalPrecio = totalPrecio + registro.FAC_BASEIMPONIBLE;
                totalIva = totalIva + registro.FAC_VALORIMPUESTO;
                total = total + registro.FAC_IMPORTETOTAL;

                facturas.FAC_SUCURSAL = registro.FAC_SUCURSAL;
                facturas.FAC_SECUENCIAL = registro.FAC_SECUENCIAL;
                facturas.FAC_FECHAEMISION = Convert.ToString(registro.FAC_FECHAEMISION);
                facturas.FAC_RAZONCOMPRADOR = registro.FAC_RAZONCOMPRADOR;
                facturas.FAC_TOTALSINIMP = Convert.ToString(registro.FAC_TOTALSINIMP);
                facturas.FAC_TOTALDESCUENTO = Convert.ToString(registro.FAC_TOTALDESCUENTO);
                facturas.FAC_BASEIMPONIBLE = Convert.ToString(registro.FAC_BASEIMPONIBLE);
                facturas.FAC_VALORIMPUESTO = Convert.ToString(registro.FAC_VALORIMPUESTO);
                facturas.FAC_IMPORTETOTAL = Convert.ToString(registro.FAC_IMPORTETOTAL);
                facturas.FAC_VENDEDOR = registro.FAC_VENDEDOR;
                facturas.FAC_TIPOCONCEPTO = Convert.ToString(registro.FAC_TIPOCONCEPTO);
                facturas.FAC_OBSERVACION = registro.FAC_OBSERVACION;
                facturas.FAC_SRI = registro.FAC_SRI;


                lista.Add(new facturas()
                {
                    FAC_SUCURSAL = facturas.FAC_SUCURSAL,
                    FAC_SECUENCIAL = facturas.FAC_SECUENCIAL,
                    FAC_FECHAEMISION = facturas.FAC_FECHAEMISION,
                    FAC_RAZONCOMPRADOR = facturas.FAC_RAZONCOMPRADOR,
                    FAC_TOTALSINIMP = facturas.FAC_TOTALSINIMP,
                    FAC_TOTALDESCUENTO = facturas.FAC_TOTALDESCUENTO,
                    FAC_BASEIMPONIBLE = facturas.FAC_BASEIMPONIBLE,
                    FAC_VALORIMPUESTO = facturas.FAC_VALORIMPUESTO,
                    FAC_IMPORTETOTAL = facturas.FAC_IMPORTETOTAL,
                    FAC_VENDEDOR = facturas.FAC_VENDEDOR,
                    FAC_TIPOCONCEPTO = facturas.FAC_TIPOCONCEPTO,
                    FAC_OBSERVACION = facturas.FAC_OBSERVACION,
                    FAC_SRI = facturas.FAC_SRI
                });
                kont = kont + 1;
            }
        }


        return lista;
    }

    public class facturas
    {
        public string FAC_SUCURSAL { get; set; }
        public string FAC_SECUENCIAL { get; set; }
        public string FAC_FECHAEMISION { get; set; }
        public string FAC_RAZONCOMPRADOR { get; set; }
        public string FAC_TOTALSINIMP { get; set; }
        public string FAC_TOTALDESCUENTO { get; set; }
        public string FAC_BASEIMPONIBLE { get; set; }
        public string FAC_VALORIMPUESTO { get; set; }
        public string FAC_IMPORTETOTAL { get; set; }
        public string FAC_VENDEDOR { get; set; }
        public string FAC_TIPOCONCEPTO { get; set; }
        public string FAC_OBSERVACION { get; set; }
        public string FAC_SRI { get; set; }
    }

    protected void btnConsolidado_Click(object sender, EventArgs e)
    {
        DateTime lfechainicio, lfechafin;
        string lsucursal = ddlSucursal2.SelectedValue;
        string laccion = string.Empty;

        pnListado.Visible = false;
        pnConsolidado.Visible = true;

        try
        {

            lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechafin = Convert.ToDateTime(txtFechaFin.Text);
            int tipo = (int)Session["STipo"];



            if (tipo == 4)
            {
                laccion = "TODOS";
                grvConsolidado.DataSource = dc.sp_ListarValorxGrupoItem2(laccion, lsucursal, lfechainicio, lfechafin); 
                grvConsolidado.DataBind();


            }
            else
            {

                laccion = "SUC";
                grvConsolidado.DataSource = dc.sp_ListarValorxGrupoItem2(laccion, lsucursal, lfechainicio, lfechafin);
                grvConsolidado.DataBind();
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

    protected void btnFacturasAneta_Click(object sender, EventArgs e)
    {
        DateTime lfechainicio, lfechafin;
        string lsucursal = ddlSucursal2.SelectedValue;
        string laccion = string.Empty;

        pnListado.Visible = false;
        pnConsolidado.Visible = false;
        pnCantidad.Visible = true;

        try
        {

            lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechafin = Convert.ToDateTime(txtFechaFin.Text);
            int tipo = (int)Session["STipo"];



            if (tipo == 4)
            {
                laccion = "TODOS";
                grvCantidad.DataSource = dc.sp_ListarCantidaxGrupoItem2(laccion, lsucursal, lfechainicio, lfechafin);
                grvCantidad.DataBind();


            }
            else
            {

                laccion = "SUC";
                grvCantidad.DataSource = dc.sp_ListarCantidaxGrupoItem2(laccion, lsucursal, lfechainicio, lfechafin);
                grvCantidad.DataBind();
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


    #region AEXEL
    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelC_Click(object sender, EventArgs e)
    {
        dos();
    }

    protected void btnExcelA_Click(object sender, EventArgs e)
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
            grvListadoFac.AllowPaging = false;
            ///this.retornaTodos();

            grvListadoFac.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvListadoFac.HeaderRow.Cells)
            {
                cell.BackColor = grvListadoFac.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvListadoFac.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvListadoFac.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvListadoFac.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvListadoFac.RenderControl(hw);

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
            grvConsolidado.AllowPaging = false;
            /// this.BindGrid();

            grvConsolidado.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvConsolidado.HeaderRow.Cells)
            {
                cell.BackColor = grvConsolidado.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvConsolidado.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvConsolidado.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvConsolidado.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvConsolidado.RenderControl(hw);

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
            grvCantidad.AllowPaging = false;
            /// this.BindGrid();

            grvCantidad.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvCantidad.HeaderRow.Cells)
            {
                cell.BackColor = grvCantidad.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvCantidad.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvCantidad.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvCantidad.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvCantidad.RenderControl(hw);

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