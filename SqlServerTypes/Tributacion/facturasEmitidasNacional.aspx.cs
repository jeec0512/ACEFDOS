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

public partial class Tributacion_facturasEmitidasNacional : System.Web.UI.Page
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



        btnExcelFe.Visible = true;
        btnExcelC.Visible = true;
    }

    private void cargaGridTodos()
    {
        string laccion;
        string lsucursal;
        DateTime lfechaInicio, lfechaFin;

       

        laccion = "DETALLE";



        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        grvDetalleFac.DataSource = dc.sp_FacturasEmitidasNacional(laccion, lsucursal, lfechaInicio, lfechaFin);
        grvDetalleFac.DataBind();

        laccion = "TOTAL";

        grvTotalesFacturacion.DataSource = dc.sp_FacturasEmitidasNacional(laccion, lsucursal, lfechaInicio, lfechaFin);
        grvTotalesFacturacion.DataBind();


    }

    private List<facturas> retornaTodos()
    {
        var facturas = new facturas();

        List<facturas> lista = new List<facturas>();

        DateTime lfechainicio, lfechafin;
        string usuario, Username, lsucursal;


        usuario = Convert.ToString(Session["UsuarioID"]);
        Username = Convert.ToString(Session["Usuarioname"]);

        //lsucursal = Username.Substring(0, 3);

        lsucursal = ddlSucursal2.SelectedValue.Trim();

        lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechafin = Convert.ToDateTime(txtFechaFin.Text);
        /*
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
                              FAC_SRI = f.FAC_SRI,

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
                              FAC_SRI = f.FAC_SRI,

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
                              FAC_SRI = f.FAC_SRI,

                          };
        }

        int kont = 0;
        decimal totalDescuento = 0, totalPrecio = 0, totalIva = 0, total = 0;
        */

        string accion = "TODOS";
        string sucursal = ddlSucursal2.SelectedValue;
        DateTime fecIni = Convert.ToDateTime(txtFechaIni.Text);
        DateTime fecFin = Convert.ToDateTime(txtFechaFin.Text);

        var cFacEmi = dc.sp_ListarFacturasEmitidasNacional(accion, sucursal, fecIni, fecFin);
        grvTotalesFacturacion.DataSource = cFacEmi;
        grvTotalesFacturacion.DataBind();


        if (cFacEmi.Count() == 0)
        {

        }
        else
        {
            foreach (var registro in cFacEmi)
            {

               /* totalDescuento = totalDescuento + registro.FAC_TOTALDESCUENTO;
                totalPrecio = totalPrecio + registro.FAC_BASEIMPONIBLE;
                totalIva = totalIva + registro.FAC_VALORIMPUESTO;
                total = total + registro.FAC_IMPORTETOTAL;*/

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
                facturas.FAC_SUCURSAL = registro.FAC_SUCURSAL;


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
                    FAC_SRI = facturas.FAC_SRI,

                });
               // kont = kont + 1;
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
        int ltipoConcepto = 0;

        pnListado.Visible = false;
        pnConsolidado.Visible = true;

        try
        {

            lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechafin = Convert.ToDateTime(txtFechaFin.Text);
            int tipo = (int)Session["STipo"];



            if (tipo == 4)
            {
                grvConsolidado.DataSource = df.sp_facturacionxfechaxsucursalxtipo("CONSOLIDADO", "", ltipoConcepto, lfechainicio, lfechafin);
                grvConsolidado.DataBind();


            }
            else
            {
                grvConsolidado.DataSource = df.sp_facturacionxfechaxsucursalxtipo("XSUCURSAL", lsucursal, ltipoConcepto, lfechainicio, lfechafin);
                grvConsolidado.DataBind();
            }

            btnExcelFe.Visible = true;
            btnExcelC.Visible = true;
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

    }



    #region PAGINACION

    public class classfac
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

    private List<classfac> retornaLista()
    {
        string lsucursal = ddlSucursal2.SelectedValue;
        DateTime lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lfechafin = Convert.ToDateTime(txtFechaFin.Text);
        int tipo = (int)Session["STipo"];
        
        var classFac = new classfac();

        List<classfac> lista = new List<classfac>();

        var cFac = retornaTodos();



        foreach (var registro in cFac)
        {
            classFac.FAC_SECUENCIAL = registro.FAC_SECUENCIAL;
            classFac.FAC_FECHAEMISION = registro.FAC_FECHAEMISION;
            classFac.FAC_RAZONCOMPRADOR = registro.FAC_RAZONCOMPRADOR;
            classFac.FAC_TOTALSINIMP = registro.FAC_TOTALSINIMP;
            classFac.FAC_TOTALDESCUENTO = registro.FAC_TOTALDESCUENTO;
            classFac.FAC_BASEIMPONIBLE = registro.FAC_BASEIMPONIBLE;
            classFac.FAC_VALORIMPUESTO = registro.FAC_VALORIMPUESTO;
            classFac.FAC_IMPORTETOTAL = registro.FAC_IMPORTETOTAL;
            classFac.FAC_VENDEDOR = registro.FAC_VENDEDOR;
            classFac.FAC_TIPOCONCEPTO = registro.FAC_TIPOCONCEPTO;
            classFac.FAC_OBSERVACION = registro.FAC_OBSERVACION;
            classFac.FAC_SRI = registro.FAC_SRI;
            classFac.FAC_SUCURSAL = registro.FAC_SUCURSAL;


            lista.Add(new classfac()
            {

                FAC_SECUENCIAL = classFac.FAC_SECUENCIAL,
                FAC_FECHAEMISION = classFac.FAC_FECHAEMISION,
                FAC_RAZONCOMPRADOR = classFac.FAC_RAZONCOMPRADOR,
                FAC_TOTALSINIMP = classFac.FAC_TOTALSINIMP,
                FAC_TOTALDESCUENTO = classFac.FAC_TOTALDESCUENTO,
                FAC_BASEIMPONIBLE = classFac.FAC_BASEIMPONIBLE,
                FAC_VALORIMPUESTO = classFac.FAC_VALORIMPUESTO,
                FAC_IMPORTETOTAL = classFac.FAC_IMPORTETOTAL,
                FAC_VENDEDOR = classFac.FAC_VENDEDOR,
                FAC_TIPOCONCEPTO = classFac.FAC_TIPOCONCEPTO,
                FAC_OBSERVACION = classFac.FAC_OBSERVACION,
                FAC_SRI = classFac.FAC_SRI,
                FAC_SUCURSAL = classFac.FAC_SUCURSAL,

            });

        }
        return lista;
    }

    private void cargaGrid()
    {
        grvTotalesFacturacion.DataSource = retornaLista();
        grvTotalesFacturacion.DataBind();
    }




    protected void grvListadoFac_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void grvListadoFac_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvListadoFac_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvTotalesFacturacion.PageIndex = e.NewPageIndex;
        // if (tipoConsulta == 1)
        // {
        cargaGrid();
        //}

        //if (tipoConsulta == 2)
        //{
        //  cargaGridTodos();
        //}
    }



    #endregion


    #region AEXEL
    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelC_Click(object sender, EventArgs e)
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
            grvDetalleFac.AllowPaging = false;
            ///this.retornaTodos();

            grvDetalleFac.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetalleFac.HeaderRow.Cells)
            {
                cell.BackColor = grvDetalleFac.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetalleFac.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetalleFac.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetalleFac.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetalleFac.RenderControl(hw);

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
    #endregion
}