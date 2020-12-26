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

public partial class Egreso_autoConsumo : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion

    #region INICIO
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

            string sucursal = ddlSucursal2.SelectedValue.Trim();
            var cjustificar = dc.sp_ListarJusticar(accion, sucursal);


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
            txtFecha.Text = Convert.ToString(lfecha);
            txtFechaEmisionDoc.Text = Convert.ToString(lfecha);
            txtFechaCaducDoc.Text = Convert.ToString(lfecha);

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
        pnTitulos.Enabled = true;
        pnDetallePagos.Visible = false;
        pnMenu.Visible = false;
        pnBorrar.Visible = false;
        pnExportar.Visible = false;
    }
    #endregion

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        btnConsultar_Click();
    }

    protected void btnConsultar_Click()
    {

        if (!fechaValida())
        {
            lblMensaje.Text = "Error en la fecha";
        }
        else
        {
            lblMensaje.Text = "";
            if (estadoCierre())
            {

                lblMensaje.Text = "";
                pnDetallePagos.Visible = true;
                pnMenu.Visible = true;

                    pnExportar.Visible = true;
                    guardarCabecera();
                    listarDetalle();

            }
            
        }

       
    }

    protected void guardarCabecera() 
    {
        int valorId;
        valorId = confirmarID();
        if (valorId == 0)
        {
            string lAccion, lnumero, lsucursal, ldescripcion, lusuario, lestado;
            string documento = string.Empty;
            int id_detEgreso = 0;
            int lid_CabEgresos = 0;
            DateTime lfecha = DateTime.Today;
            DateTime docFecha = DateTime.Today;
            docFecha = Convert.ToDateTime(txtFecha.Text);
            
            string dia, mes, ano;

            dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
            mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
            ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);

            lsucursal = ddlSucursal2.SelectedValue;
            txtFecha.Text = Convert.ToString(txtFecha.Text);
            lnumero = 'A' + lsucursal + dia+mes+ano;

            lAccion = "AGREGAR";
            lfecha = Convert.ToDateTime(txtFecha.Text);
            ldescripcion = "";
            lestado = "0";
            lusuario = Convert.ToString(Session["SUsuarioname"]);


            dc.sp_abmEgresosCabecera2(lAccion, lid_CabEgresos, lnumero, lsucursal, lfecha, ldescripcion, lestado, lusuario, documento, id_detEgreso);
        }
    }

    protected int confirmarID()
    {
        int retonoId;
        DateTime lfecha = Convert.ToDateTime(txtFecha.Text);
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);

        string lnumero = 'A' + ddlSucursal2.SelectedValue.Trim() + dia + mes + ano; 



        retonoId = 0;

        var ConsultaId = from Eid in dc.tbl_CabEgresos
                         where Eid.numero == lnumero
                         select new
                         {
                             id = Eid.id_CabEgresos
                         };

        if (ConsultaId.Count() == 0)
        {
            retonoId = 0;
        }
        else
        {
            foreach (var registro in ConsultaId)
            {
                retonoId = registro.id;
            }
        }

        return retonoId;
    }

    protected void listarDetalle() 
    {
        pnDetallePagos.Visible = true;

        string accion2 = "INSERTAR";
        int id_CabEgreso = confirmarID();
        string ruc = "1790349578001";
        string sucursal =  ddlSucursal2.SelectedValue.Trim();
        DateTime fecha1 = Convert.ToDateTime(txtFecha.Text);
        DateTime fecha2 = Convert.ToDateTime(txtFecha.Text);

        int cuantos;
        string laccion,  lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        DateTime lfecha = Convert.ToDateTime(txtFecha.Text);
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);

        string lnumero = 'A' + ddlSucursal2.SelectedValue.Trim() + dia + mes + ano;


        var concultaDetEgresos = dc.sp_ConsultaEgresosDetallexNumero3(laccion, lnumero, accion2, id_CabEgreso, ruc, sucursal, fecha1, fecha2);
        grvDetallePagos.DataSource = concultaDetEgresos;
        grvDetallePagos.DataBind();
        cuantos = grvDetallePagos.Rows.Count;

    }
    
    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
    
    /*MODIFICAR PAGOS*/
    protected void grvDetallePagos_SelectedIndexChanged(object sender, EventArgs e)
    {

        decimal lvalor = 0;
       

        txtBsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBtarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBtotal.Text = string.Format("{0:#,##0.##}", lvalor);

        txtRsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtRtarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtRotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtRIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtRtotal.Text = string.Format("{0:#,##0.##}", lvalor);

        txtSsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtStarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtStotal.Text = string.Format("{0:#,##0.##}", lvalor);

        txtValorRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
        txtIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtValorFactura.Text = string.Format("{0:#,##0.##}", lvalor);

        txtaPagar.Text = string.Format("{0:#,##0.##}", lvalor);

        DateTime fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);
        DateTime fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);

        /**********llenar con informacion ya existente******************/
        if (estadoCierre())
        {
            llenarListados();
            pnTitulos.Enabled = false;
            btnConsultar.Visible = false;
            //pnBorrar.Visible = true;
            pnPagos.Visible = true;
            pnDetallePagos.Visible = false;
            pnExportar.Visible = false;

           

            string accion = "CONSULTAR";
            int id_DetEgresos = Convert.ToInt32(grvDetallePagos.SelectedValue);

            //var cDet = dc.sp_abmEgresosDetalle3(accion, id_DetEgresos, 0, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0,fechaEmisionDoc,fechaCaducDoc);
            var cDet = dc.sp_abmEgresosDetalle4(accion, id_DetEgresos, 0, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, fechaEmisionDoc, fechaCaducDoc,"","","",0,0,0,0,0);

            // if(cDet.Count() > 0)
            //{
            foreach (var registro in cDet)
            {
                ddlTipoDocumento.SelectedValue = "14"; //Convert.ToString(registro.id_documento);
                if (registro.doc_autorizacion == null)
                {
                    txtDocAutorizacion.Text = string.Empty;
                }
                else 
                {
                    txtDocAutorizacion.Text = registro.doc_autorizacion;
                }
                
                txtRuc.Text = registro.ruc;
                txtNombres.Text = registro.nombres;
                txtDocumento.Text = registro.numeroDocumento;
                ddlAfectaSucursal.SelectedValue = registro.sucAfecta;
                txtDescripcion.Text = registro.concepto;
                ddlTipoPago.SelectedValue = "5";
                txtValorFactura.Text = Convert.ToString(registro.valorFactura);
                txtaPagar.Text = Convert.ToString(registro.apagar);
            }


            string documentoNum = txtDocumento.Text.Trim();
            
                        var fdet = from mfacd in df.FACTURA
                                   where mfacd.FAC_ESTABLECIMIENTO + mfacd.FAC_PUNTOEMISION + mfacd.FAC_SECUENCIAL == documentoNum
                                   select new
                                   {
                                       FAC_FECHAEMISION = mfacd.FAC_FECHAEMISION
                                      , FAC_AUTORIZACION = mfacd.FAC_AUTORIZACION 
                                   };

                        if (fdet.Count() < 0)
                        {

                        }
                        else
                        {
							//SI EXISTEN DOS FACTURAS CON EL MISMO SECUENCIAL AQUI SALDRÁ ERROR
                            foreach (var regfac in fdet) 
                            {
                                txtDocAutorizacion.Text = regfac.FAC_AUTORIZACION.Trim();
                                txtFechaEmisionDoc.Text = Convert.ToString(regfac.FAC_FECHAEMISION);
                                txtFechaCaducDoc.Text = Convert.ToString(regfac.FAC_FECHAEMISION);
                                    
                            }
                        }



             
            //}
        }
        else
        {
            lblMensaje.Text = "Caja cerrada no se puede modificar";

        }
        /****************************/
    }


    /*AL REALIZAR CAMBIO EN TEXTOS O LISTADOS*/
    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        //consultarRetencionProveedor();
       
        activarTextos();
        verificarConcepto();
    }

    protected void activarTextos()
    {
        int ltipoEgreso;
        ltipoEgreso = Convert.ToInt16(ddlTipoDocumento.SelectedValue);
        lblMensaje.Text = Convert.ToString(ltipoEgreso);

        /*************VUELVE AL ESTADO ORIGINAL PRA ACTIVAR DE ACUERDO AL TIPO DE DOCUMENTO*****************/
        desActivarTextos();
        /**************************************************************************************************/


        switch (ltipoEgreso)
        {
            case -1:

                break;
            case 1:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = false;
                pnDocRet.Visible = false;
                ddlDocRet.Visible = false;

                lblSerie.Visible = true;
                txtSerie.Visible = true;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = true;
                txtNumDocumento.Visible = true;

                lblValorRetencion.Visible = false;
                txtValorRetencion.Visible = false;
                lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;
            case 2:
            case 18:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = false;
                pnDocRet.Visible = false;
                ddlDocRet.Visible = false;

                lblSerie.Visible = true;
                txtSerie.Visible = true;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = true;
                txtNumDocumento.Visible = true;

                lblValorRetencion.Visible = false;
                txtValorRetencion.Visible = false;
                lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;
            case 3:
            case 17:
            case 19:
            case 20:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = true;
                pnDocRet.Visible = true;
                ddlDocRet.Visible = true;

                lblSerie.Visible = false;
                txtSerie.Visible = false;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = false;
                txtNumDocumento.Visible = false;

                lblValorRetencion.Visible = true;
                txtValorRetencion.Visible = true;
                lblNumAutorizacion.Visible = true;
                txtNumAutorizacion.Visible = true;
                break;
            case 4:
            case 21:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = false;
                pnDocRet.Visible = false;
                ddlDocRet.Visible = false;

                lblSerie.Visible = true;
                txtSerie.Visible = true;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = true;
                txtNumDocumento.Visible = true;

                lblValorRetencion.Visible = false;
                txtValorRetencion.Visible = false;
                lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;
            case 5:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = true;
                pnDocRet.Visible = true;
                ddlDocRet.Visible = true;

                lblSerie.Visible = false;
                txtSerie.Visible = false;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = false;
                txtNumDocumento.Visible = false;

                lblValorRetencion.Visible = true;
                txtValorRetencion.Visible = true;
                lblNumAutorizacion.Visible = true;
                txtNumAutorizacion.Visible = true;
                txtNumretencion.Visible = true;
                break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = false;
                pnDocRet.Visible = false;
                ddlDocRet.Visible = false;

                lblSerie.Visible = true;
                txtSerie.Visible = true;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = true;
                txtNumDocumento.Visible = true;

                lblValorRetencion.Visible = false;
                txtValorRetencion.Visible = false;
                lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;

            case 11:
            case 12:
                lblColaborador.Visible = true;
                pnColaborador.Visible = true;
                ddlColaborador.Visible = true;

                lblDocRet.Visible = false;
                pnDocRet.Visible = false;
                ddlDocRet.Visible = false;

                lblSerie.Visible = true;
                txtSerie.Visible = true;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = true;
                txtNumDocumento.Visible = true;

                lblValorRetencion.Visible = false;
                txtValorRetencion.Visible = false;
                lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;

            case 13:
            case 14:
            case 15:
            case 16:
            case 22:
                lblColaborador.Visible = false;
                pnColaborador.Visible = false;
                ddlColaborador.Visible = false;

                lblDocRet.Visible = false;
                pnDocRet.Visible = false;
                ddlDocRet.Visible = false;

                lblSerie.Visible = true;
                txtSerie.Visible = true;
                txtDocAutorizacion.Visible = true;
                lblNumDocumento.Visible = true;
                txtNumDocumento.Visible = true;

                lblValorRetencion.Visible = false;
                txtValorRetencion.Visible = false;
                lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;

            default:
                desActivarTextos();
                break;
        }
    }

    protected void desActivarTextos()
    { }

    protected void verificarConcepto()
    {
        int ltipoEgreso;
        ltipoEgreso = Convert.ToInt16(ddlTipoDocumento.SelectedValue);
        lblMensaje.Text = Convert.ToString(ltipoEgreso);

        switch (ltipoEgreso)
        {
            case -1:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "-1";
                break;
            case 6:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "74";
                ddlSiGastos.SelectedValue = "239";

                break;
            case 7:
            case 8:
            case 9:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "74";
                ddlSiGastos.SelectedValue = "234";
                break;
            case 10:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "74";
                ddlSiGastos.SelectedValue = "023";
                break;

            case 11:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "007";
                break;

            case 12:
                ddlBiCodCble.SelectedValue = "74";
                ddlBiGastos.SelectedValue = "009";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "-1";
                break;

            case 13:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "008";
                break;

            case 14:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "243";
                break;

            case 15:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "603";
                break;

            case 16:

            case 17:
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "-1";
                break;
            case 18:
            case 19:
            case 20:
            default:
                ddlBiGastos.SelectedValue = "-1";
                break;
        }
    }

    protected void ddlColaborador_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRuc.Text = ddlColaborador.SelectedValue.Trim();
        txtRuc_TextChanged();
    }

    protected void txtRuc_TextChanged(object sender, EventArgs e)
    {
        txtRuc_TextChanged();

    }

    protected void txtNumDocumento_TextChanged(object sender, EventArgs e)
    {
        txtNumDocumento.Text = llenarCeros(txtNumDocumento.Text.Trim(), '0', 9);
        txtDocumento.Text = txtSerie.Text.Trim() + txtNumDocumento.Text;
    }

    protected void ddlDocRet_SelectedIndexChanged(object sender, EventArgs e)
    {
        string suc = ddlSucursal2.SelectedValue.Trim();
        string doc = ddlDocRet.SelectedItem.Text;
        string cod = ddlDocRet.SelectedValue;
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string docRet = string.Empty;
        int id = Convert.ToInt16(ddlDocRet.SelectedValue);
        decimal lvalor = 0, bien = 0, servicio = 0, factura = 0, retencion = 0, secuencial = 0;

        if (tipoDoc == "3")
        {
            docRet = "01";
        }

        if (tipoDoc == "5")
        {
            docRet = "07";
        }


        if (cod == "-1")
        {
            txtDocumento.Text = string.Empty;
        }
        else
        {
            /*total retencion y total a pagar*/
            txtDocumento.Text = doc;
            var cinfo = from mInfo in dc.tbl_infotributaria
                        where mInfo.id_infotributaria == id
                        select new
                        {
                            totRetenido = mInfo.totRetenido,
                            aPagar = mInfo.aPagar,
                            claveacceso = mInfo.claveacceso,
                            numRetencion = mInfo.estab.Trim() + mInfo.ptoemi + mInfo.secuencial.Trim(),
                        };

            if (cinfo.Count() <= 0)
            {
                txtValorFactura.Text = string.Format("{0:#,##0.##}", lvalor);
                txtValorRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
                txtNumretencion.Text = string.Empty;

            }
            else
            {
                foreach (var registro in cinfo)
                {
                    retencion = Convert.ToDecimal(registro.totRetenido);
                    factura = Convert.ToDecimal(registro.aPagar);
                    secuencial = Convert.ToDecimal(registro.numRetencion);
                    txtNumretencion.Text = registro.numRetencion;
                }
            }

            txtValorFactura.Text = string.Format("{0:#,##0.##}", factura);
            txtValorRetencion.Text = string.Format("{0:#,##0.##}", retencion);
            txtaPagar.Text = string.Format("{0:#,##0.##}", factura);
            //txtNumretencion.Text = string.Empty;

            /*subtotales del documento en la retencion*/
            //var cSubT = dc.sp_ListarSubtotalesRetencion(accion, cod, suc);

            var SSubT = from c in dc.tbl_infotributaria
                        from d in dc.tbl_infoCompRetencion
                        from ret in dc.tbl_impuestosRet
                        from adic in dc.tbl_infoAdicional
                        where c.id_infotributaria == d.id_infotributaria
                           && d.id_infoCompRetencion == ret.id_infoCompRetencion
                           && d.id_infoCompRetencion == adic.id_infoCompRetencion
                           && c.id_infotributaria == id
                           && ret.codDocSustento == docRet
                           && ret.SB.Substring(0, 1) == "S"
                        select new
                        {
                            servicio = ret.baseImponible,
                            mae_gas = ret.mae_gas,
                            codcble = ret.codcble,
                            descrip = adic.campoAdicional.Trim()
                        };



            if (SSubT.Count() <= 0)
            {
                servicio = 0;

                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "-1";
                txtServicio.Text = string.Empty;
            }
            else
            {
                foreach (var registro in SSubT)
                {
                    servicio = servicio + registro.servicio;
                    ddlSiCodCble.SelectedValue = registro.codcble;
                    ddlSiGastos.SelectedValue = registro.mae_gas;
                    txtServicio.Text = registro.descrip;

                }
            }


            var BSubT = from c in dc.tbl_infotributaria
                        from d in dc.tbl_infoCompRetencion
                        from ret in dc.tbl_impuestosRet
                        from adic in dc.tbl_infoAdicional
                        where c.id_infotributaria == d.id_infotributaria
                           && d.id_infoCompRetencion == ret.id_infoCompRetencion
                           && d.id_infoCompRetencion == adic.id_infoCompRetencion
                           && c.id_infotributaria == id
                           && ret.codDocSustento == docRet
                          && ret.SB.Substring(0, 1) == "B"
                        select new
                        {
                            bien = ret.baseImponible,
                            mae_gas = ret.mae_gas,
                            codcble = ret.codcble,
                            descrip = adic.campoAdicional.Trim()
                        };



            if (BSubT.Count() <= 0)
            {
                bien = 0;
                ddlBiCodCble.SelectedValue = "-1";
                ddlBiGastos.SelectedValue = "-1";
                txtBien.Text = string.Empty;
            }
            else
            {
                foreach (var registro in BSubT)
                {
                    bien = bien + registro.bien;
                    ddlBiCodCble.SelectedValue = registro.codcble;
                    ddlBiGastos.SelectedValue = registro.mae_gas;
                    txtBien.Text = registro.descrip;
                }
            }

            txtBsubtotal.Text = string.Format("{0:#,##0.##}", bien);
            txtSsubtotal.Text = string.Format("{0:#,##0.##}", servicio);

        }

    }




    protected void btnGrabarPago_Click(object sender, EventArgs e)
    {
        btnGrabarPago_Click();
    }

    protected void btnGrabarPago_Click()
    {
        bool lpasa = false;
        string lmensaje;
        var lrevision = validarDatos();
        lpasa = lrevision.Item1;
        lmensaje = lrevision.Item2;

        if (lpasa)
        {
            registrarCabeceraEgresos();
            registrarDetalleEgresos();
            btnCancelarpago_Click();
            btnConsultar_Click();

        }
        else
        {
            lblMensaje.Text = "NO SE PUEDE GUARDAR " + lmensaje;
        }
    }

    protected void txtRuc_TextChanged()
    {
        pnMensaje2.Visible = true;

        string accion = "PORSUC";
        bool lpasa = false;
        string lrso;
        var lrevision = consultarProveedor();
        lpasa = lrevision.Item1;
        lrso = lrevision.Item2;
        txtNombres.Text = lrso;

        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string docRet = string.Empty;


        if (tipoDoc == "3")
        {
            docRet = "01";
        }

        if (tipoDoc == "5")
        {
            docRet = "07";
        }

        var cRetenciones = dc.sp_ListarRetenciones(accion, txtRuc.Text.Trim(), ddlSucursal2.SelectedValue.Trim(), "", docRet);
        ddlDocRet.DataSource = cRetenciones;
        ddlDocRet.DataBind();
        ListItem liDocumento = new ListItem("Seleccione documento", "-1");
        ddlDocRet.Items.Insert(0, liDocumento);

    }

    protected Tuple<bool, string> consultarProveedor()
    {
        bool siExiste;
        string lidentificacionSujetoRetenido, razonsocial = string.Empty;

        siExiste = true;

        // realizar la consulta del cliente
        lidentificacionSujetoRetenido = txtRuc.Text.Trim();

        var consultaPr = from provee in dc.tbl_matriz
                         where provee.ruc == lidentificacionSujetoRetenido
                         select new
                         {
                             razonsocial = provee.razonsocial
                         };
        if (consultaPr.Count() == 0)
        {
            siExiste = false;
        }
        else
        {
            foreach (var registro in consultaPr)
            {
                siExiste = true;
                razonsocial = registro.razonsocial;
            }
        }


        return Tuple.Create(siExiste, razonsocial);
    }


    /*CONJUNTO DE PROCESOS PARA GRABAR LOS DATOS*/
    protected void registrarCabeceraEgresos()
    {
        int valorId;
        valorId = confirmarID();
        if (valorId == 0)
        {
            string documento = "A";
            int id_detEgreso = 0;
            string lAccion, lnumero, lsucursal, ldescripcion, lusuario, lestado;
            int lid_CabEgresos;
            DateTime lfecha = DateTime.Today;
            DateTime docFecha = DateTime.Today;
            docFecha = Convert.ToDateTime(txtFecha.Text);
            string dia, mes, ano;

            dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
            mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
            ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);


            txtSucursal.Text = ddlSucursal2.SelectedValue;
            txtFecha.Text = Convert.ToString(txtFecha.Text);
            txtNumero.Text = "A" + txtSucursal.Text.Trim() + dia + mes + ano;

            lAccion = "AGREGAR";
            lid_CabEgresos = 0;
            lnumero = txtNumero.Text;
            lsucursal = txtSucursal.Text;
            lfecha = Convert.ToDateTime(txtFecha.Text);
            ldescripcion = "";
            lestado = "0";
            lusuario = Convert.ToString(Session["SUsername"]);


            dc.sp_abmEgresosCabecera2(lAccion, lid_CabEgresos, lnumero, lsucursal, lfecha, ldescripcion, lestado, lusuario, documento, id_detEgreso);


            

        }

    }

    protected void registrarDetalleEgresos()
    {
        /*DATOS GENERALES*/
        int id_Concepto = 0;
        string lAccion = "AGREGAR";
        int lid_DetEgresos = 0;
        int lid_CabEgresos = confirmarID();
        int lid_documento = Convert.ToInt16(ddlTipoDocumento.SelectedValue);

        string lruc = txtRuc.Text.Trim();
        string lnombres = txtNombres.Text.Trim();
        string ldocumento = txtDocumento.Text.Trim();


        string lsucAfecta = ddlAfectaSucursal.SelectedValue;
        string lccoAfecta = ddlAfectaCcosto.SelectedValue;
        string lautorizacion = txtAutorizacion.Text.Trim();
        string ldescripcion = txtDescripcion.Text.Trim();
        
        /*BIENES O LUBRICANTES*/
        string lbimae_gas = ddlBiGastos.SelectedValue;
        string lbicodcble = ddlBiCodCble.SelectedValue;
        string lbien = txtBien.Text.Trim();

        decimal lbsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal lbtarifa0 = Convert.ToDecimal(txtBtarifa0.Text);
        decimal lbotros = Convert.ToDecimal(txtBotros.Text);
        decimal lbiva = Convert.ToDecimal(txtBIva.Text);
        decimal lbtotal = Convert.ToDecimal(txtBtotal.Text);

        /*REPUESTOS*/
        string lbrmae_gas = ddlBrGastos.SelectedValue;
        string lbrcodcble = ddlBrCodCble.SelectedValue;
        string lrepuesto = txtRepuesto.Text.Trim();

        decimal lrsubtotal = Convert.ToDecimal(txtRsubtotal.Text);
        decimal lrtarifa0 = Convert.ToDecimal(txtRtarifa0.Text);
        decimal lrotros = Convert.ToDecimal(txtRotros.Text);
        decimal lriva = Convert.ToDecimal(txtRIva.Text);
        decimal lrtotal = Convert.ToDecimal(txtRtotal.Text);

        /*SERVICIOS*/
        string lsimae_gas = ddlSiGastos.SelectedValue;
        string lsicodcble = ddlSiCodCble.SelectedValue;
        string lservicio = txtServicio.Text.Trim();

        decimal lssubtotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal lstarifa0 = Convert.ToDecimal(txtStarifa0.Text);
        decimal lsotros = Convert.ToDecimal(txtSotros.Text);
        decimal lsiva = Convert.ToDecimal(txtSIva.Text);
        decimal lstotal = Convert.ToDecimal(txtStotal.Text);

        /*TOTALES*/
        string lNumretencion = txtNumretencion.Text.Trim();
        string lnumAutorizacionRetencion = txtNumAutorizacion.Text.Trim();
        decimal lvalorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
        decimal liva = Convert.ToDecimal(txtIva.Text);
        decimal lvalorFactura = Convert.ToDecimal(txtValorFactura.Text);
        decimal lapagar = Convert.ToDecimal(txtaPagar.Text);

        string lsecuencial = txtNumretencion.Text;
        int ltipoPago = Convert.ToInt16(ddlTipoPago.SelectedValue);


        string docAutorizacion = txtDocAutorizacion.Text.Trim();
        DateTime fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);
        DateTime fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);

        //verificarDetall();

        var cDet = from mDet in dc.tbl_DetEgresos
                   where mDet.ruc == lruc
                   && mDet.id_CabEgresos == lid_CabEgresos
                   && mDet.numeroDocumento == ldocumento
                   select new
                   {
                       id_DetEgresos = mDet.id_DetEgresos
                   };

  
        if (cDet.Count() <= 0)
        {
            pnMensaje2.Visible = true;
           
            /*GRABA DETALLE DE EGRESOS*/
           /* dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, docAutorizacion, lsecuencial, lbiva, lsiva, lbtarifa0, lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva, fechaEmisionDoc, fechaCaducDoc);
            */

            dc.sp_abmEgresosDetalle4(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, docAutorizacion, lsecuencial, lbiva, lsiva, lbtarifa0,
                                      lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva, fechaEmisionDoc, fechaCaducDoc, lbrmae_gas, lbrcodcble, lrepuesto,
                                      lrsubtotal, lrtarifa0, lrotros, lriva, lrtotal);

            
            /*GRABA TOTALES EN CABECERA*/
            dc.sp_abmEgresosTotales("TOTALIZA", lid_CabEgresos);


            /*GRABA DETALLE REPUESTOS*/
           /* lAccion = "AGREGAR";
            dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbrcodcble, lbrmae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lrepuesto, "", 0, 0, "", "", docAutorizacion, lsecuencial, lriva, 0, lrtarifa0, 0, lrotros, 0, lrtotal, 0, 0, fechaEmisionDoc, fechaCaducDoc);
            */
            lblMensaje.Text = "Se ha grabado con éxito";
        }
        else
        {
            foreach (var registro in cDet) 
            {
                lid_DetEgresos = registro.id_DetEgresos;
            }

            lAccion = "MODIFICAR";

            /*MODIFICA DETALLE DE EGRESOS*/
           /* dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                       , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                       , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, lnumAutorizacionRetencion, lsecuencial, lbiva, lsiva, lbtarifa0, lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva,fechaEmisionDoc,fechaCaducDoc);
            */
            /*GRABA DETALLE REPUESTOS*/
            /*lAccion = "AGREGAR";
            dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbrcodcble, lbrmae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lrepuesto, "", 0, 0, "", "", docAutorizacion, lsecuencial, lriva, 0, lrtarifa0, 0, lrotros, 0, lrtotal, 0, 0, fechaEmisionDoc, fechaCaducDoc);
            */
            dc.sp_abmEgresosDetalle4(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, docAutorizacion, lsecuencial, lbiva, lsiva, lbtarifa0,
                                      lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva, fechaEmisionDoc, fechaCaducDoc, lbrmae_gas, lbrcodcble, lrepuesto,
                                      lrsubtotal, lrtarifa0, lrotros, lriva, lrtotal);

            /*GRABA TOTALES EN CABECERA*/
            dc.sp_abmEgresosTotales("TOTALIZA", lid_CabEgresos);

            
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Este registro se ha modificado correctamente";
        }

    }

    protected void btnCancelarpago_Click(object sender, EventArgs e)
    {
        btnCancelarpago_Click();
    }

    protected void btnCancelarpago_Click()
    {
        btnIngresaProv.Visible = false;
        pnTitulos.Enabled = true;
        btnConsultar.Visible = true;
        pnDetallePagos.Visible = true;
        pnMenu.Visible = true;
        pnPagos.Visible = false;
        pnBorrar.Visible = false;
        pnExportar.Visible = false;
    }

    /*PROCESO PARA VALIDAR DATOS INGRESADOS*/
    protected void btnValidar_Click(object sender, EventArgs e)
    {
        btnValidar_Click();
    }

    protected bool btnValidar_Click()
    {
        bool lpasa = false;
        string lmensaje;
        var lrevision = validarDatos();
        lpasa = lrevision.Item1;
        lmensaje = lrevision.Item2;

        if (!lpasa)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = lmensaje;
            btnGuardar.Visible = false;
        }
        else
        {
            pnMensaje2.Visible = false;
            lblMensaje.Text = string.Empty;
            btnGuardar.Visible = true;
        }
        return lpasa;
    }

    protected Tuple<bool, string> validarDatos()
    {
        string lmensaje = string.Empty; ;
        bool pasa = true;
        bool existeProv = true;

        string TipoDocumento = ddlTipoDocumento.SelectedValue;
        string Colaborador = ddlColaborador.SelectedValue;
        string DocRet = ddlDocRet.SelectedValue;
        string AfectaSucursal = ddlAfectaSucursal.SelectedValue;
        string AfectaCcosto = ddlAfectaCcosto.SelectedValue;
        string TipoPago = ddlTipoPago.SelectedValue;
        
        string BiGastos = ddlBiGastos.SelectedValue;
        string BiCodCble = ddlBiCodCble.SelectedValue;

        string BrGastos = ddlBrGastos.SelectedValue;
        string BrCodCble = ddlBrCodCble.SelectedValue;

        string SiGastos = ddlSiGastos.SelectedValue;
        string SiCodCble = ddlSiCodCble.SelectedValue;

        string Ruc = txtRuc.Text;
        string Nombres = txtNombres.Text;
        string Serie = txtSerie.Text;
        string DocAutorizacion = txtDocAutorizacion.Text;
        string NumDocumento = txtDocumento.Text.Trim();

        string Autorizacion = txtAutorizacion.Text;
        string Descripcion = txtDescripcion.Text;

        string Bien = txtBien.Text;
        string Repuesto = txtRepuesto.Text;
        string Servicio = txtServicio.Text;
        string NumAutorizacion = txtNumAutorizacion.Text;
        string Numretencion = txtNumretencion.Text;


        decimal Bsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal Btarifa0 = Convert.ToDecimal(txtBtarifa0.Text);
        decimal Botros = Convert.ToDecimal(txtBotros.Text);
        decimal Biva = Convert.ToDecimal(txtBIva.Text);
        decimal Btotal = Convert.ToDecimal(txtBtotal.Text);

       
        decimal Rsubtotal = Convert.ToDecimal(txtRsubtotal.Text);
        decimal Rtarifa0 = Convert.ToDecimal(txtRtarifa0.Text);
        decimal Rotros = Convert.ToDecimal(txtRotros.Text);
        decimal Riva = Convert.ToDecimal(txtRIva.Text);
        decimal Rtotal = Convert.ToDecimal(txtRtotal.Text);


        decimal Ssubtotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal Starifa0 = Convert.ToDecimal(txtStarifa0.Text);
        decimal Sotros = Convert.ToDecimal(txtSotros.Text);
        decimal Siva = Convert.ToDecimal(txtSIva.Text);
        decimal Stotal = Convert.ToDecimal(txtStotal.Text);

        decimal ValorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
        decimal iva = Convert.ToDecimal(txtIva.Text);
        decimal ValorFactura = Convert.ToDecimal(txtValorFactura.Text);
        decimal aPagar = Convert.ToDecimal(txtaPagar.Text);


        var lrevision = consultarProveedor();
        existeProv = lrevision.Item1;



        if (TipoDocumento == "-1")
        {
            lmensaje = " Seleccione tipo de documento ";
            pasa = false;
        }

        if (!existeProv)
        {
            lmensaje = lmensaje + " Cree el proveedor (Matriz) ";
            pasa = false;
        }

        if (NumDocumento.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese el #Doc ";
            pasa = false;
        }

        if (NumDocumento.Length < 15 || NumDocumento.Length > 15)
        {
            lmensaje = lmensaje + " El documento debe tener 15 dígitos";
            pasa = false;
        }
        if (DocAutorizacion.Length < 10 || DocAutorizacion.Length > 49)
        {
            lmensaje = lmensaje + " El número de autorización debe ser de 10 dígitos si es manual ó 49 si es digital";
            pasa = false;
        }

        if (AfectaSucursal == "-1")
        {
            lmensaje = lmensaje + " Seleccione sucursal ";
            pasa = false;
        }

        if (AfectaCcosto == "-1")
        {
            lmensaje = lmensaje + " Seleccione centro de costo ";
            pasa = false;
        }


        if (Autorizacion.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese quién autoriza ";
            pasa = false;
        }

        if (Descripcion.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese descripción del gasto";
            pasa = false;
        }


        if (TipoPago == "-1")
        {
            lmensaje = lmensaje + " Seleccione tipo de pago ";
            pasa = false;
        }


        if (Btotal <= 0 && Stotal <= 0 && Rtotal <= 0)
        {
            lmensaje = lmensaje + " Ingrese valores en Bienes (Repuestos, Lubricantes) o Servicios o en ambos si es el caso ";
            pasa = false;
        }

        if (Btotal > 0)
        {
            if (BiGastos == "-1" || BiCodCble == "-1")
            {
                lmensaje = lmensaje + " Seleccione concepto y código contable del Bien ";
                pasa = false;
            }
            if (Bien.Length <= 0)
            {
                lmensaje = lmensaje + " Ingrese descripción del Bien";
                pasa = false;
            }
        }

        if (Rtotal > 0)
        {
            if (BrGastos == "-1" || BrCodCble == "-1")
            {
                lmensaje = lmensaje + " Seleccione concepto y código contable del Repuesto";
                pasa = false;
            }
            if (Repuesto.Length <= 0)
            {
                lmensaje = lmensaje + " Ingrese descripción del Repuesto";
                pasa = false;
            }
        }

        if (Stotal > 0)
        {
            if (SiGastos == "-1" || SiCodCble == "-1")
            {
                lmensaje = lmensaje + " Seleccione concepto y código contable del Servicio ";
                pasa = false;
            }
            if (Servicio.Length <= 0)
            {
                lmensaje = lmensaje + " Ingrese descripción del Servicio";
                pasa = false;
            }
        }

        if (new[] { "3", "5", "19", "20", "23" }.Contains(TipoDocumento))
        {
            if (ValorRetencion <= 0)
            {
                lmensaje = lmensaje + " Valor de la retención debe ser mayor que cero";
                pasa = false;
            }

            if (NumAutorizacion.Length <= 0)
            {
                lmensaje = lmensaje + " Ingrese el número de autorización de la retención";
                pasa = false;
            }
        }

        if (ValorFactura <= 0)
        {
            lmensaje = lmensaje + " Ingrese los valores del documento";
            pasa = false;
        }

        if (aPagar <= 0)
        {
            lmensaje = lmensaje + " Ingrese los valores del documento";
            pasa = false;
        }
        return Tuple.Create(pasa, lmensaje);
    }

    protected bool fechaValida()
    {
        bool fechaValida = false;
        var date1 = txtFecha.Text;

        DateTime dt1 = DateTime.Now;
        DateTime dt2 = dt1;

        var culture = CultureInfo.CreateSpecificCulture("es-MX");
        var styles = DateTimeStyles.None;


        fechaValida = DateTime.TryParse(date1, culture, styles, out dt1);

        return fechaValida;
    }

    protected bool estadoCierre()
    {
        string lnumero, lestado;
        bool pasa = false;
        
        DateTime docFecha = DateTime.Today;
        docFecha = Convert.ToDateTime(txtFecha.Text);
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);

        lnumero = 'A' + ddlSucursal2.SelectedValue.Trim() + dia+mes+ano;
        txtNumero.Text = lnumero;

        // consultar estado de la caja de egresos

        var consultaCe = from Ce in dc.tbl_CabEgresos
                         where Ce.numero == lnumero
                         select new
                         {
                             estado = Ce.estado
                         };

        if (consultaCe.Count() == 0)
        {
            pasa = true;
        }
        else
        {
            foreach (var registro in consultaCe)
            {
                lestado = registro.estado.Trim();
                if (lestado == "0")
                {
                    pasa = true;
                }
                else
                {
                    pasa = false;
                }
            }
        }
        return pasa;
    }


    protected void llenarListados()
    {
        bool lactivo = true;
        #region DOCUMENTOS
        /*******DOCUMENTOS*********************/
        var consultaTd = from Td in dc.tbl_tipoEgresos
                         where Td.activo == lactivo
                         select new
                         {
                             id = Td.id_tipoEgresos,
                             descripcion = Td.id_tipoEgresos + " " + Td.descripcion.Trim()
                         };

        ddlTipoDocumento.DataSource = consultaTd;
        ddlTipoDocumento.DataBind();

        ListItem liDocumento = new ListItem("Seleccione tipo de documento", "-1");
        ddlTipoDocumento.Items.Insert(0, liDocumento);
        #endregion

        #region TERCEROS
        /*******TERCEROS*********************/

        var cColaborador = from Ter in dc.tbl_colaborador
                           orderby Ter.apellidos
                           select new
                           {
                               cedula = Ter.Cedula,
                               nombres = Ter.apellidos.Trim() + " " + Ter.nombres.Trim() + " " + Ter.Cedula.Trim()
                           };

        ddlColaborador.DataSource = cColaborador;
        ddlColaborador.DataBind();

        ListItem liTercero = new ListItem("Seleccione colaborador ", "-1");
        ddlColaborador.Items.Insert(0, liTercero);
        #endregion

        #region CONCEPTOS BIENES (LUBRICANTES Y REPUESTOS) Y SERVICIOS
        /*******CONCEPTOS (Concepto)*********************/
        var cCon = from mCon in dc.tbl_mae_gas
                   orderby mCon.mae_gas
                   select new
                   {
                       mae_gas = mCon.mae_gas.Trim()
                    ,
                       nombre = mCon.mae_gas.Trim() + " " + mCon.nombre.Trim()
                   };

        ddlBiGastos.DataSource = cCon;
        ddlBiGastos.DataBind();

        ddlBrGastos.DataSource = cCon;
        ddlBrGastos.DataBind();

        ddlSiGastos.DataSource = cCon;
        ddlSiGastos.DataBind();


        ListItem listCon = new ListItem("Seleccione Concepto", "-1");

        ddlBiGastos.Items.Insert(0, listCon);
        ddlBrGastos.Items.Insert(0, listCon);
        ddlSiGastos.Items.Insert(0, listCon);



        #endregion

        #region CODIGO CONTABLE
        /*CODIGO CONTABLE*/
        var cCble = from mCble in dc.tbl_var_gen
                    orderby mCble.var_gen
                    select new
                    {
                        var_gen = mCble.var_gen.Trim()
                     ,
                        nom_ic = mCble.var_gen.Trim() + " " + mCble.nom_ic.Trim()
                    };

        ddlBiCodCble.DataSource = cCble;
        ddlBiCodCble.DataBind();

        ddlBrCodCble.DataSource = cCble;
        ddlBrCodCble.DataBind();

        ddlSiCodCble.DataSource = cCble;
        ddlSiCodCble.DataBind();

        ListItem listCble = new ListItem("Seleccione código contable", "-1");

        ddlBiCodCble.Items.Insert(0, listCble);
        ddlBrCodCble.Items.Insert(0, listCble);
        ddlSiCodCble.Items.Insert(0, listCble);

        #endregion

        #region SUCURSAL AFECTADA
        /*******SUCURSALES *********************/
        var cSucursal = from mSuc in dc.tbl_ruc
                        where mSuc.activo == true
                        orderby mSuc.sucursal
                        select new
                        {
                            sucursal = mSuc.sucursal,
                            nom_suc = mSuc.sucursal + ' ' + mSuc.nom_suc.Trim()
                        };

        ddlAfectaSucursal.DataSource = cSucursal;
        ddlAfectaSucursal.DataBind();

        ListItem liSucursal = new ListItem("Seleccione la sucursal ", "-1");
        ddlAfectaSucursal.Items.Insert(0, liSucursal);
        #endregion

        #region CENTRO DE COSTO AFECTADO

        /*******CENTRO DE COSTO *********************/
        var cCcosto = from mCos in dc.tbl_mae_cco
                      orderby mCos.mae_cco
                      select new
                      {
                          mae_cco = mCos.mae_cco,
                          nom_cco = mCos.mae_cco + ' ' + mCos.nom_cco.Trim()
                      };

        ddlAfectaCcosto.DataSource = cCcosto;
        ddlAfectaCcosto.DataBind();

        ListItem liCco = new ListItem("Seleccione el centro de costo ", "-1");
        ddlAfectaCcosto.Items.Insert(0, liCco);
        #endregion
    }


    #region PAGINACION

    public class classAuto
    {
        public string id_DetEgresos { get; set; }
        public string descripcion { get; set; }
        public string numeroDocumento { get; set; }
        public string ruc { get; set; }
        public string autorizacion { get; set; }
        public string valorFactura { get; set; }
        public string valorRetencion { get; set; }
        public string apagar { get; set; }
        public string concepto { get; set; }

    }

    private List<classAuto> retornaLista()
    {
        /***********************/
        string accion2 = "INSERTAR";
        int id_CabEgreso = confirmarID();
        string ruc = "1790349578001";
        string sucursal = ddlSucursal2.SelectedValue.Trim();
        DateTime fecha1 = Convert.ToDateTime(txtFecha.Text);
        DateTime fecha2 = Convert.ToDateTime(txtFecha.Text);

        //int cuantos;
        string laccion, lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        DateTime lfecha = Convert.ToDateTime(txtFecha.Text);
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);

        string lnumero = 'A' + ddlSucursal2.SelectedValue.Trim() + dia + mes + ano;


        //var concultaDetEgresos = dc.sp_ConsultaEgresosDetallexNumero3(laccion, lnumero, accion2, id_CabEgreso, ruc, sucursal, fecha1, fecha2);

        /***********************/

        var classAuto = new classAuto();

        List<classAuto> lista = new List<classAuto>();

        var cAuto = dc.sp_ConsultaEgresosDetallexNumero3(laccion, lnumero, accion2, id_CabEgreso, ruc, sucursal, fecha1, fecha2);

        foreach (var registro in cAuto)
        {
           /* classCta.id_mae_cue = Convert.ToString(registro.id_mae_cue);
            classCta.MAE_CUE = registro.MAE_CUE;
            classCta.NOM_CTA = registro.NOM_CTA;
            classCta.TIP_CTA = Convert.ToString(registro.TIP_CTA);
            classCta.NAT_CTA = Convert.ToString(registro.NAT_CTA);
            classCta.estado = Convert.ToString(registro.estado);
            */

            classAuto.id_DetEgresos  = Convert.ToString(registro.id_DetEgresos);
            classAuto.descripcion  = registro.descripcion;
            classAuto.numeroDocumento  = registro.numeroDocumento;
            classAuto.ruc = registro.ruc;
            classAuto.autorizacion = registro.autorizacion;
            classAuto.valorFactura = Convert.ToString(registro.valorFactura);
            classAuto.valorRetencion = Convert.ToString(registro .valorRetencion);
            classAuto.apagar = Convert.ToString(registro.apagar);
            classAuto.concepto = registro.concepto;



            lista.Add(new classAuto()
            {
               

                id_DetEgresos = classAuto.id_DetEgresos,
                descripcion = classAuto.descripcion,
                numeroDocumento = classAuto.numeroDocumento ,
                ruc = classAuto.ruc ,
                autorizacion = classAuto.autorizacion ,
                valorFactura = classAuto.valorFactura ,
                valorRetencion = classAuto.valorRetencion ,
                apagar = classAuto.apagar ,
                concepto = classAuto.concepto 
            });

        }

        return lista;
    }

    private void cargaGrid()
    {
        grvDetallePagos.DataSource = retornaLista();
        grvDetallePagos.DataBind();
      
    }

  
   

   

    protected void grvDetallePagos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void grvDetallePagos_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvDetallePagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDetallePagos.PageIndex = e.NewPageIndex;
        cargaGrid();
    }

    #endregion

    protected void txtBsubtotal_TextChanged(object sender, EventArgs e)
    {
        double iva = 0.12;

        txtBIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBsubtotal.Text) * iva);
        txtBtotal.Text = string.Format("{0:#,##0.##}", (Convert.ToDouble(txtBsubtotal.Text) + Convert.ToDouble(txtBtarifa0.Text)
                        + Convert.ToDouble(txtBotros.Text) + Convert.ToDouble(txtBIva.Text)));



        double Btotal = Convert.ToDouble(txtBtotal.Text);
        double Rtotal = Convert.ToDouble(txtRtotal.Text);
        double Stotal = Convert.ToDouble(txtStotal.Text);
        double valorRetencion = Convert.ToDouble(txtValorRetencion.Text);
        double totalIva = (Convert.ToDouble(txtBIva.Text) + Convert.ToDouble(txtRIva.Text) + Convert.ToDouble(txtSIva.Text));
        double totalDocumento = (Btotal + Rtotal + Stotal);

        txtIva.Text = string.Format("{0:#,##0.##}", totalIva); //Convert.ToString(totalIva);

        txtValorFactura.Text = string.Format("{0:#,##0.##}", totalDocumento); //Convert.ToString(totalDocumento);
        txtaPagar.Text = string.Format("{0:#,##0.##}", totalDocumento - valorRetencion);
    }


    protected void txtRsubtotal_TextChanged(object sender, EventArgs e)
    {
        double iva = 0.12;

        txtRIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtRsubtotal.Text) * iva);
        txtRtotal.Text = string.Format("{0:#,##0.##}", (Convert.ToDouble(txtRsubtotal.Text) + Convert.ToDouble(txtRtarifa0.Text)
                        + Convert.ToDouble(txtRotros.Text) + Convert.ToDouble(txtRIva.Text)));


        double Btotal = Convert.ToDouble(txtBtotal.Text);
        double Rtotal = Convert.ToDouble(txtRtotal.Text);
        double Stotal = Convert.ToDouble(txtStotal.Text);
        double valorRetencion = Convert.ToDouble(txtValorRetencion.Text);
        double totalIva = (Convert.ToDouble(txtBIva.Text) + Convert.ToDouble(txtRIva.Text) + Convert.ToDouble(txtSIva.Text));
        double totalDocumento = (Btotal+Rtotal + Stotal);

        txtIva.Text = string.Format("{0:#,##0.##}", totalIva); //Convert.ToString(totalIva);

        txtValorFactura.Text = string.Format("{0:#,##0.##}", totalDocumento); //Convert.ToString(totalDocumento);
        txtaPagar.Text = string.Format("{0:#,##0.##}", totalDocumento - valorRetencion);
    }


    protected void txtSsubtotal_TextChanged(object sender, EventArgs e)
    {

        double iva = 0.12;

        txtSIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSsubtotal.Text) * iva);

        txtStotal.Text = string.Format("{0:#,##0.##}", (Convert.ToDouble(txtSsubtotal.Text) + Convert.ToDouble(txtStarifa0.Text)
                        + Convert.ToDouble(txtSotros.Text) + Convert.ToDouble(txtSIva.Text)));



        double Btotal = Convert.ToDouble(txtBtotal.Text);
        double Rtotal = Convert.ToDouble(txtRtotal.Text);
        double Stotal = Convert.ToDouble(txtStotal.Text);
        double valorRetencion = Convert.ToDouble(txtValorRetencion.Text);
        double totalIva = (Convert.ToDouble(txtBIva.Text) + Convert.ToDouble(txtRIva.Text) + Convert.ToDouble(txtSIva.Text));
        double totalDocumento = (Btotal + Rtotal + Stotal);

        txtIva.Text = string.Format("{0:#,##0.##}", totalIva); //Convert.ToString(totalIva);

        txtValorFactura.Text = string.Format("{0:#,##0.##}", totalDocumento); //Convert.ToString(totalDocumento);
        txtaPagar.Text = string.Format("{0:#,##0.##}", totalDocumento - valorRetencion);
    }
}