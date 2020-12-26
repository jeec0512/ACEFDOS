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

public partial class Ingreso_factura : System.Web.UI.Page
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

    protected decimal traerCodigo()
    {
        decimal lId_Factura = 0;
        string lruc, lsuc, lestab, lptoemi, lsecuencial, lcoddoc;

        lcoddoc = "01";
        lsuc = ddlSucursal2.SelectedValue;
        lruc = txtBuscaRuc.Text.Trim();
        lestab = "";
        lptoemi = "";
        //traer serie de la sucursal

        var consultaSerie = from msuc in dc.tbl_ruc
                            where msuc.sucursal == lsuc
                            select new
                            {
                                estab = msuc.estab,
                                ptoemi = msuc.ptoemi
                            };
        if (consultaSerie.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaSerie)
            {
                lestab = registro.estab;
                lptoemi = registro.ptoemi;
            }
        }
        ;

        //llenar de ceros el secuencial
        txtBuscaSecuencial.Text = llenarCeros(txtBuscaSecuencial.Text.Trim(), '0', 9);
        lsecuencial = txtBuscaSecuencial.Text.Trim();

        var consulta = from Tfac in df.FACTURA
                       where Tfac.FAC_CODDOCUMENTO == lcoddoc &&
                                Tfac.FAC_RUC == lruc &&
                                Tfac.FAC_ESTABLECIMIENTO == lestab &&
                                Tfac.FAC_PUNTOEMISION == lptoemi &&
                                Tfac.FAC_SECUENCIAL == lsecuencial
                       select new { idInfo = Tfac.FAC_ID };
        if (consulta.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consulta)
            {
                lId_Factura = registro.idInfo;
            }
        }
        return lId_Factura;
    }

    /// <summary>
    /// llena a la izquierda con caracteres asignados 
    /// </summary>
    /// <param name="cadenasinceros">cadena a ser llanado</param>
    /// <param name="llenarCon">caracter con lo q se rellenara</param>
    /// <param name="numeroDecaracteres">numero total de la cadena</param>
    /// <returns></returns>
    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void activarBotones()
    {
        btnEnviarFac.Visible = true;
        btnBorrarFac.Visible = false;
        btnCancelarFac.Visible = true;
    }


    protected void desactivarBotones()
    {
        btnEnviarFac.Visible = false;
        btnBorrarFac.Visible = false;
        btnCancelarFac.Visible = false;
        btnAnularFac.Visible = false;
    }

    protected void encerarValores()
    {
        lblMensaje.Text = "";
        // txtBuscaRuc.Text = "";
        //txtBuscaSecuencial.Text = "";
    }
    #endregion

    #region TRAER FACTURA
    protected void btnTraerFactura_Click(object sender, EventArgs e)
    {
        btnTraerFactura_Click();
    }

    protected void btnTraerFactura_Click()
    {
        int tipo = (int)Session["STipo"];

        string  lsecuencial, lfac_sri, lfac_estado;

        var consultaSuc = new object();
        /****************************/
        
        lsecuencial = txtBuscaSecuencial.Text.Trim();
        lfac_sri = "";
        lfac_estado = "";

        if (tipo == 4)
        {
            btnAnularFac.Visible = true;
        }
        else
        {
            btnAnularFac.Visible = false;
        }

        decimal lId_Factura = traerCodigo(), sihay = 0;

        var consultaFAC = from fac in df.FACTURA
                          where fac.FAC_ID == lId_Factura &&
                                fac.FAC_ESTADO == "A"
                          orderby fac.FAC_SECUENCIAL
                          select new
                          {
                              FAC_SUCURSAL = fac.FAC_SUCURSAL,
                              FAC_SECUENCIAL = fac.FAC_SECUENCIAL,
                              FAC_FECHAEMISION = fac.FAC_FECHAEMISION,
                              FAC_RAZONCOMPRADOR = fac.FAC_RAZONCOMPRADOR,
                              FAC_TOTALSINIMP = fac.FAC_TOTALSINIMP,
                              FAC_TOTALDESCUENTO = fac.FAC_TOTALDESCUENTO,
                              FAC_BASEIMPONIBLE = fac.FAC_BASEIMPONIBLE,
                              FAC_VALORIMPUESTO = fac.FAC_VALORIMPUESTO,
                              FAC_IMPORTETOTAL = fac.FAC_IMPORTETOTAL,
                              FAC_RECAUDADO = fac.FAC_RECAUDADO,
                              FAC_RETENIDOIVA = fac.FAC_RETENIDOIVA,
                              FAC_RETENIDOFUENTE = fac.FAC_RETENIDOFUENTE,
                              FAC_VENDEDOR = fac.FAC_VENDEDOR,
                              FAC_TIPOCONCEPTO = fac.FAC_TIPOCONCEPTO,
                              FAC_OBSERVACION = fac.FAC_OBSERVACION,
                              FAC_ESTADO = fac.FAC_ESTADO,
                              FAC_SRI = fac.FAC_SRI,
                              FAC_SALDO = fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE)
                          };
        grvListadoFac.DataSource = consultaFAC;
        grvListadoFac.DataBind();

        if (consultaFAC.Count() == 0)
        {
        }
        else
        {
            foreach (var registro in consultaFAC)
            {
                lfac_sri = Convert.ToString(registro.FAC_SRI).Trim();
                lfac_estado = Convert.ToString(registro.FAC_ESTADO).Trim();

                sihay = 1;
            }
        }

        grvListadoFac.DataSource = consultaFAC;
        grvListadoFac.DataBind();



        if (lfac_estado == "A" || lfac_sri == "Anulado")
        {
            desactivarBotones();
            if (lfac_sri == "Anulado")
            {
                btnAnularFac.Visible = false;
            }

            if (lfac_estado == "A" && tipo == 4)
            {
                btnAnularFac.Visible = true;
            }



        }
        else
        {

            if (sihay == 1)
            {
                activarBotones();
            }
            else
            {
                desactivarBotones();
            }
            btnAnularFac.Visible = false;
        }
    }

    #endregion

    protected void btnBorrarFac_Click(object sender, EventArgs e)
    {
        ///  string laccion = "BORRAR";
        decimal lId_InfoTributaria = traerCodigo();


        try
        {

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        finally
        {
        }

    }


    protected void btnAnularFac_Click(object sender, EventArgs e)
    {
        bool pasa;
        string lsuc, lfactura;
       
        decimal lId_Factura = traerCodigo();

        pasa = true;
        lsuc = ddlSucursal2.SelectedValue.Trim();
        lfactura = txtBuscaSecuencial.Text.Trim();

        var cPago = from mPago in dc.tbl_DetRecaudacion
                    where mPago.sucursal == lsuc
                            && mPago.factura == lfactura
                    select new
                    {
                        id_DetRecaudacion = mPago.id_CabRecaudacion
                    };

        if (cPago.Count() == 0)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = "";
            pasa = true;
        }
        else
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = "No se puede anular , esta factura presenta pagos en tesorería";
            pasa = false;
            /*foreach (var registro in cPago)
            {
                lestab = registro.estab;
                lptoemi = registro.ptoemi;l
            }*/
        }
        ;
        if (pasa)
        {
            try
            {
               // df.sp_abmRecaudacion(laccion, "", "", 0, 0, 0, lId_Factura);
                lblMensaje.Text = "Documento anulado";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            finally
            {
                btnCancelarFac_Click();
            }
        }
    }

    protected void btnCancelarFac_Click(object sender, EventArgs e)
    {
        btnCancelarFac_Click();
    }
    protected void btnCancelarFac_Click()
    {
        desactivarBotones();
        encerarValores();
        btnTraerFactura_Click();
    }
}