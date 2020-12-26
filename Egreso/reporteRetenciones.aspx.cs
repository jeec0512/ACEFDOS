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

public partial class Egreso_reporteRetenciones : System.Web.UI.Page
{
    public string lcre_sri;

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

    protected void btnTodos_Click(object sender, EventArgs e)
    {
        pnListadoRet.Visible = true;
        pnConsolidado.Visible = false;
        btnTodos_Click();
        btnExcelRe.Visible = true;
        btnExcelC.Visible = false;
    }

    protected void btnTodos_Click()
    {
        lblTipoConsulta.Text = "2";
        cargaGridTodos();
    }

    private void cargaGridTodos()
    {
        grvListadoRet.DataSource = retornaTodos();
        grvListadoRet.DataBind();

    }

    private List<retenciones> retornaTodos()
    {
        var retenciones = new retenciones();

        List<retenciones> lista = new List<retenciones>();


        DateTime lfechainicio, lfechafin;
        string usuario, Username, lsucursal;
        usuario = Convert.ToString(Session["UsuarioID"]);
        Username = Convert.ToString(Session["Usuarioname"]);

        //lsucursal = Username.Substring(0, 3);

        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechafin = Convert.ToDateTime(txtFechaFin.Text);

        var consultaRET = from a in dc.tbl_matriz
                          from b in dc.tbl_ruc
                          from c in dc.tbl_infotributaria
                          from d in dc.tbl_infoCompRetencion
                          from ret in dc.tbl_impuestosRet
                          from obs in dc.tbl_infoAdicional
                          where a.ruc == b.ruc
                            && b.ruc == c.ruc
                            && b.coddoc == c.coddoc
                            && b.estab == c.estab
                            && b.ptoemi == c.ptoemi
                            && c.id_infotributaria == d.id_infotributaria
                            && d.id_infoCompRetencion == ret.id_infoCompRetencion
                            && ret.id_infoCompRetencion == obs.id_infoCompRetencion
                            && b.sucursal == lsucursal
                            && c.fechaDocumento >= lfechainicio
                            && c.fechaDocumento <= lfechafin
                          orderby b.sucursal
                          select new
                          {
                              sucursal = b.sucursal,
                              estab = Convert.ToString(b.estab),
                              ptoemi = Convert.ToString(b.ptoemi),
                              secuencial = Convert.ToString(c.secuencial),
                              cre_sri = c.cre_sri,
                              fechaDocumento = c.fechaDocumento,
                              identificacionSujetoRetenido = Convert.ToString(d.identificacionSujetoRetenido),
                              razonSocialSujetoRetenido = d.razonSocialSujetoRetenido,
                              codigo = ret.codigo,
                              codigoRetencion = ret.codigoRetencion,
                              baseImponible = ret.baseImponible,
                              porcentajeRetener = ret.porcentajeRetener,
                              valorRetenido = ret.valorRetenido,
                              numDocSustento = Convert.ToString(ret.numDocSustento),
                              campoAdicional = obs.campoAdicional
                          };

        int kont = 0;
        if (consultaRET.Count() == 0)
        {

        }
        else
        {
            foreach (var registro in consultaRET)
            {

                retenciones.sucursal = Convert.ToString(registro.sucursal);
                retenciones.estab = Convert.ToString(registro.estab);
                retenciones.ptoemi = Convert.ToString(registro.ptoemi);
                retenciones.secuencial = Convert.ToString(registro.secuencial);
                retenciones.cre_sri = registro.cre_sri;
                retenciones.fechaDocumento = Convert.ToString(registro.fechaDocumento);
                retenciones.identificacionSujetoRetenido = Convert.ToString(registro.identificacionSujetoRetenido);
                retenciones.razonSocialSujetoRetenido = registro.razonSocialSujetoRetenido;
                retenciones.codigo = Convert.ToString(registro.codigo);
                retenciones.codigoRetencion = registro.codigoRetencion;
                retenciones.baseImponible = Convert.ToString(registro.baseImponible);
                retenciones.porcentajeRetener = registro.porcentajeRetener;
                retenciones.valorRetenido = Convert.ToString(registro.valorRetenido);
                retenciones.numDocSustento = Convert.ToString(registro.numDocSustento);
                retenciones.campoAdicional = registro.campoAdicional;


                lista.Add(new retenciones()
                {
                    sucursal = retenciones.sucursal,
                    estab = Convert.ToString(retenciones.estab),
                    ptoemi = Convert.ToString(retenciones.ptoemi),
                    secuencial = Convert.ToString(retenciones.secuencial),
                    cre_sri = retenciones.cre_sri,
                    fechaDocumento = retenciones.fechaDocumento,
                    identificacionSujetoRetenido = Convert.ToString(retenciones.identificacionSujetoRetenido),
                    razonSocialSujetoRetenido = retenciones.razonSocialSujetoRetenido,
                    codigo = retenciones.codigo,
                    codigoRetencion = retenciones.codigoRetencion,
                    baseImponible = retenciones.baseImponible,
                    porcentajeRetener = retenciones.porcentajeRetener,
                    valorRetenido = retenciones.valorRetenido,
                    numDocSustento = Convert.ToString(retenciones.numDocSustento),
                    campoAdicional = retenciones.campoAdicional
                });

                kont = kont + 1;

            }
        }


        return lista;
    }


    ///CLASE PARA PAGINAR
    ///
    public class retenciones
    {
        public string sucursal { get; set; }
        public string estab { get; set; }
        public string ptoemi { get; set; }
        public string secuencial { get; set; }
        public string cre_sri { get; set; }
        public string fechaDocumento { get; set; }
        public string identificacionSujetoRetenido { get; set; }
        public string razonSocialSujetoRetenido { get; set; }
        public string codigo { get; set; }
        public string codigoRetencion { get; set; }
        public string baseImponible { get; set; }
        public string porcentajeRetener { get; set; }
        public string valorRetenido { get; set; }
        public string numDocSustento { get; set; }
        public string campoAdicional { get; set; }
    }


    protected void btnConsolidado_Click(object sender, EventArgs e)
    {
        DateTime lfechainicio, lfechafin;
        string lsucursal;

        pnListadoRet.Visible = false;
        pnConsolidado.Visible = true;

        btnExcelRe.Visible = false;
        btnExcelC.Visible = true;
        try
        {
            int tipo = (int)Session["STipo"];
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
            lfechafin = Convert.ToDateTime(txtFechaFin.Text);
            if (tipo == 4)
            {
                grvConsolidado.DataSource = dc.sp_consolidadoretencionesxfecha("TODOS", "", lfechainicio, lfechafin);
                grvConsolidado.DataBind();
            }
            else
            {
                grvConsolidado.DataSource = dc.sp_consolidadoretencionesxfecha("SUCURSAL", lsucursal, lfechainicio, lfechafin);
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

    #region EXCEL
    protected void btnExcelRe_Click(object sender, EventArgs e)
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
            grvListadoRet.AllowPaging = false;
            ///this.retornaTodos();

            grvListadoRet.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvListadoRet.HeaderRow.Cells)
            {
                cell.BackColor = grvListadoRet.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvListadoRet.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvListadoRet.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvListadoRet.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvListadoRet.RenderControl(hw);

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