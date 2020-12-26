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

public partial class Egreso_controlCajas2 : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion


    decimal subtotal = 0;
    decimal tarifa0 = 0;
    decimal otros = 0;
    decimal totaliva = 0;
    decimal totaldoc = 0;
    decimal fuente = 0;
    decimal iva = 0;
    decimal totalretenido = 0;
    decimal apagar = 0;


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

            txtFechaEmisionDoc.Text = Convert.ToString(lfecha);
            txtFechaCaducDoc.Text = Convert.ToString(lfecha);



            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();

            llenarListados();
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
        pnDetalleCaja.Visible = false;

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

        #region CONCEPTOS BIENES Y SERVICIOS
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

        ddlSiGastos.DataSource = cCon;
        ddlSiGastos.DataBind();


        ListItem listCon = new ListItem("Seleccione Concepto", "-1");

        ddlBiGastos.Items.Insert(0, listCon);
        ddlSiGastos.Items.Insert(0, listCon);
        #endregion

        #region CODIGO CONTABLE
        /*CODIGO CONTABLE*/
        var cBCble = from mCble in dc.tbl_var_gen
                    where mCble.grupo == '1' || mCble.grupo == '0'
                    orderby mCble.var_gen

                    select new
                    {
                        var_gen = mCble.var_gen.Trim()
                     ,
                        nom_ic = mCble.var_gen.Trim() + " " + mCble.nom_ic.Trim()
                    };

        ddlBiCodCble.DataSource = cBCble;
        ddlBiCodCble.DataBind();


        var cSCble = from mCble in dc.tbl_var_gen
                    where mCble.grupo == '2' || mCble.grupo == '0'
                    orderby mCble.var_gen

                    select new
                    {
                        var_gen = mCble.var_gen.Trim()
                     ,
                        nom_ic = mCble.var_gen.Trim() + " " + mCble.nom_ic.Trim()
                    };

        ddlSiCodCble.DataSource = cSCble;
        ddlSiCodCble.DataBind();

        ListItem listCble = new ListItem("Seleccione código contable", "-1");

        ddlBiCodCble.Items.Insert(0, listCble);
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

    protected void desActivarTextos()
    { }

    protected void activarTextos()
    {
        int ltipoEgreso;
        ltipoEgreso = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
        lblMensaje.Text = Convert.ToString(ltipoEgreso);

        /*************VUELVE AL ESTADO ORIGINAL PRA ACTIVAR DE ACUERDO AL TIPO DE DOCUMENTO*****************/
        desActivarTextos();
        /**************************************************************************************************/


        switch (ltipoEgreso)
        {
            case -1:

                break;
            case 1:
            case 24:
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
            case 23:
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

    protected void verificarConcepto()
    {
        int ltipoEgreso;
        ltipoEgreso = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
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
                ddlBiCodCble.SelectedValue = "63";
                ddlBiGastos.SelectedValue = "209";
                ddlSiCodCble.SelectedValue = "-1";
                ddlSiGastos.SelectedValue = "-1";
                break;
            case 19:
            case 20:
            default:
                ddlBiGastos.SelectedValue = "-1";
                break;
        }
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
        string Servicio = txtServicio.Text;
        string NumAutorizacion = txtNumAutorizacion.Text;
        string Numretencion = txtNumretencion.Text;


        decimal Bsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal Btarifa0 = Convert.ToDecimal(txtBtarifa0.Text);
        decimal Botros = Convert.ToDecimal(txtBotros.Text);
        decimal Biva = Convert.ToDecimal(txtBIva.Text);
        decimal Btotal = Convert.ToDecimal(txtBtotal.Text);

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


        if (DocAutorizacion.Trim().Length < 10)
        {
            lmensaje = lmensaje + " El número de autorización debe ser de 10 dígitos si es manual ó 49 si es digital";
            pasa = false;
        }

        if (DocAutorizacion.Trim().Length > 49)
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


        if (Btotal <= 0 && Stotal <= 0)
        {
            lmensaje = lmensaje + " Ingrese valores en Bienes o Servicios o en ambos si es el caso ";
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

    protected void registrarCabeceraEgresos()
    {
        int valorId;
        valorId = confirmarID();
        if (valorId == 0)
        {
            string lAccion, lnumero, lsucursal, ldescripcion, lusuario, lestado;
            int lid_CabEgresos;
            DateTime lfecha = DateTime.Today;


            txtSucursal.Text = ddlSucursal2.SelectedValue;
            //txtFecha.Text = Convert.ToString(txtFecha.Text);
            //txtNumero.Text = txtSucursal.Text.Trim() + txtFecha.Text.Trim();

            lAccion = "AGREGAR";
            lid_CabEgresos = 0;
            lnumero = txtNumero.Text;
            lsucursal = txtSucursal.Text;
           // lfecha = Convert.ToDateTime(txtFecha.Text);
            ldescripcion = "";
            lestado = "0";
            lusuario = Convert.ToString(Session["SUsername"]);

            dc.sp_abmEgresosCabecera2(lAccion, lid_CabEgresos, lnumero, lsucursal, lfecha, ldescripcion, lestado, lusuario, "", 0);
        }

    }

    protected void registrarDetalleEgresos()
    {
        /*int ltipoDoc, lid_CabEgresos, lid_DetEgresos, lid_documento, lid_Concepto, ltipoPago;
        string lAccion, lruc, ldocumento,lnumeroDocumento, lautorizacion, ldescripcion, lnumAutorizacionRetencion;
        string lsucAfecta, lccoAfecta, lsucafecta, lccoafecta, lnombres, lobservacion;
        decimal lvalorFactura, lvalorRetencion, lapagar;
        */
        int id_Concepto = 0;
        string lAccion = "MODIFICAR";
        int lid_DetEgresos = Convert.ToInt32(txtIdDetalle.Text);
        int lid_CabEgresos = Convert.ToInt32(txtIdCabecera.Text); ;
        int lid_documento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
        string DocAutorizacion = txtDocAutorizacion.Text;
        string lruc = txtRuc.Text.Trim();
        string lnombres = txtNombres.Text.Trim();
        string ldocumento = txtDocumento.Text.Trim();
        string lsucAfecta = ddlAfectaSucursal.SelectedValue;
        string lccoAfecta = ddlAfectaCcosto.SelectedValue;
        string lautorizacion = txtAutorizacion.Text.Trim();
        string ldescripcion = txtDescripcion.Text.Trim();
        string lbimae_gas = ddlBiGastos.SelectedValue;
        string lbicodcble = ddlBiCodCble.SelectedValue;
        string lbien = txtBien.Text.Trim();

        decimal lbsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal lbtarifa0 = Convert.ToDecimal(txtBtarifa0.Text);
        decimal lbotros = Convert.ToDecimal(txtBotros.Text);
        decimal lbiva = Convert.ToDecimal(txtBIva.Text);
        decimal lbtotal = Convert.ToDecimal(txtBtotal.Text);

        string lsimae_gas = ddlSiGastos.SelectedValue;
        string lsicodcble = ddlSiCodCble.SelectedValue;
        string lservicio = txtServicio.Text.Trim();
        decimal lssubtotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal lstarifa0 = Convert.ToDecimal(txtStarifa0.Text);
        decimal lsotros = Convert.ToDecimal(txtSotros.Text);
        decimal lsiva = Convert.ToDecimal(txtSIva.Text);
        decimal lstotal = Convert.ToDecimal(txtStotal.Text);


        string lNumretencion = txtNumretencion.Text.Trim();
        string lnumAutorizacionRetencion = txtNumAutorizacion.Text.Trim();
        decimal lvalorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
        decimal liva = Convert.ToDecimal(txtIva.Text);
        decimal lvalorFactura = Convert.ToDecimal(txtValorFactura.Text);
        decimal lapagar = Convert.ToDecimal(txtaPagar.Text);

        string lsecuencial = txtNumretencion.Text;
        int ltipoPago = Convert.ToInt32(ddlTipoPago.SelectedValue);

        
        DateTime fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);
        DateTime fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);

        //verificarDetall();

        var cDet = from mDet in dc.tbl_DetEgresos
                   where mDet.ruc == lruc
                   && mDet.id_documento == lid_documento
                   && mDet.numeroDocumento == ldocumento
                   select new
                   {
                       id_DetEgresos = mDet.id_DetEgresos
                   };

        if (cDet.Count() <= 0)
        {
            pnMensaje2.Visible = true;



            /*GRABA DETALLE DE EGRESOS*/
            dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, DocAutorizacion, lsecuencial, lbiva, lsiva, lbtarifa0, lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva, fechaEmisionDoc, fechaCaducDoc);

            /*GRABA TOTALES EN CABECERA*/
            dc.sp_abmEgresosTotales("TOTALIZA", lid_CabEgresos);

            lblMensaje.Text = "Se ha grabado con éxito";
        }
        else
        {
           // pnMensaje2.Visible = true;
            //lblMensaje.Text = "Este registro (DOCUMENTO YA REGISTRADO) ya fue ingresado";
            pnMensaje2.Visible = true;



            /*GRABA DETALLE DE EGRESOS*/
            dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, DocAutorizacion, lsecuencial, lbiva, lsiva, lbtarifa0, lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva, fechaEmisionDoc, fechaCaducDoc);

            /*GRABA TOTALES EN CABECERA*/
            dc.sp_abmEgresosTotales("TOTALIZA", lid_CabEgresos);

            lblMensaje.Text = "Se ha grabado con éxito";
        }

    }

    protected int confirmarID()
    {
        int retonoId;
       // string lnumero = ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();
        string lnumero = txtNumero.Text;



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

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void llenarCabecera(int idCab) 
    {
        txtIdCabecera.Text = Convert.ToString(idCab);

      //  var cCabEgreso = from vCabEgreso in dc.tbl_CabEgresos
        //                 where 

        var cCabEgreso = from vCabEgreso in dc.tbl_CabEgresos
                         where vCabEgreso.id_CabEgresos == idCab
                    select new
                    {
                        numero = vCabEgreso.numero
                    };

        if (cCabEgreso.Count() <= 0)
        {

        }
        else
        {
            foreach (var registro in cCabEgreso)
            {
                txtNumero.Text = registro.numero;

            }
        }
    }


    protected void llenarDetalle(int idDet)
    {
        //  var cCabEgreso = from vCabEgreso in dc.tbl_CabEgresos
        //                 where 

        var cDetEgreso = from vDetEgreso in dc.tbl_DetEgresos
                         where vDetEgreso.id_DetEgresos == idDet
                         select new { 
                             id_documento = vDetEgreso.id_documento,
                             ruc = vDetEgreso.ruc,
                             nombres = vDetEgreso.nombres,
                             serie = vDetEgreso.numeroDocumento.Substring(0,6),
                             numDocumento = vDetEgreso.numeroDocumento.Substring(6,9),
                             doc_autorizacion = vDetEgreso.doc_autorizacion,
                             fechaEmisionDoc = vDetEgreso.fechaEmisionDoc,
                             fechaCaducDoc = vDetEgreso.fechaCaducDoc,
                             documento = vDetEgreso.numeroDocumento,
                             sucAfecta = vDetEgreso.sucAfecta,
                             ccoAfecta = vDetEgreso.ccoAfecta,
                             autorizacion = vDetEgreso.autorizacion,
                             descripcion = vDetEgreso.Observacion,
                             tipoPago = vDetEgreso.tipoPago,
                             mae_gas = vDetEgreso.mae_gas,
                             var_gen = vDetEgreso.var_gen,
                             descripbien = vDetEgreso.descripBien,
                             subotalBien = vDetEgreso.subtotalBien,
                             tarifaCeroBien = vDetEgreso.tarifaCeroBien,
                             bOtros = vDetEgreso.otrosBien,
                             bIva = vDetEgreso.ivaBien,
                             totalBien = vDetEgreso.totalBien,
                             smae_gas = vDetEgreso.smae_gas,
                             svar_gen = vDetEgreso.svar_gen,
                             descripServicio = vDetEgreso.descripServicio,
                             subotalServicio = vDetEgreso.subtotalServicio,
                             tarifaCeroServicio = vDetEgreso.tarifaCeroServicio,
                             sOtros = vDetEgreso.otrosServicio,
                             sIva = vDetEgreso.ivaServicio,
                             totalServicio = vDetEgreso.totalServicio,
                             numRetencion = vDetEgreso.numRetencion,
                             numAutretencion = vDetEgreso.numAutorizacionRetencion,
                             retencion = vDetEgreso.numRetencion,
                             valorRetencion = vDetEgreso.valorRetencion,
                             iva = vDetEgreso.totalIva,
                             factura = vDetEgreso.valorFactura,
                             pagado = vDetEgreso.apagar,



                         };

        if (cDetEgreso.Count() <= 0)
        {

        }
        else
        {
            foreach (var registro in cDetEgreso)
            {
                ddlTipoDocumento.SelectedValue = Convert.ToString(registro.id_documento);
                txtRuc.Text = registro.ruc;
                txtNombres.Text = registro.nombres;
                txtSerie.Text = registro.serie;
                txtNumDocumento.Text = registro.numDocumento;
                txtDocAutorizacion.Text = registro.doc_autorizacion;
                txtFechaEmisionDoc.Text = Convert.ToString(registro.fechaEmisionDoc);
                txtFechaCaducDoc.Text = Convert.ToString(registro.fechaCaducDoc);
                txtDocumento.Text = registro.documento;
                ddlAfectaSucursal.SelectedValue = registro.sucAfecta;
                ddlAfectaCcosto.SelectedValue = registro.ccoAfecta;
                txtAutorizacion.Text = registro.autorizacion;
                txtDescripcion.Text = registro.descripcion;
                ddlTipoPago.SelectedValue = Convert.ToString(registro.tipoPago);
                ddlBiGastos.SelectedValue = registro.mae_gas;
                ddlBiCodCble.SelectedValue = registro.var_gen;
                txtBien.Text = registro.descripbien;
                txtBsubtotal.Text = Convert.ToString(registro.subotalBien);
                txtBtarifa0.Text = Convert.ToString(registro.tarifaCeroBien);
                txtBotros.Text=Convert.ToString(registro.bOtros);
                txtBIva.Text=Convert.ToString(registro.bIva);
                txtBtotal.Text=Convert.ToString(registro.totalBien);
                ddlSiGastos.SelectedValue = registro.smae_gas;
                ddlSiCodCble.SelectedValue = registro.svar_gen;
                txtServicio.Text = registro.descripServicio;
                txtSsubtotal.Text = Convert.ToString(registro.subotalServicio);
                txtStarifa0.Text = Convert.ToString(registro.tarifaCeroServicio);
                txtSotros.Text=Convert.ToString(registro.sOtros);
                txtSIva.Text=Convert.ToString(registro.sIva);
                txtStotal.Text=Convert.ToString(registro.totalServicio);

                txtNumretencion.Text = registro.numRetencion;
                txtNumAutorizacion.Text = registro.numAutretencion;
                txtValorRetencion.Text = Convert.ToString(registro.valorRetencion);
                txtIva.Text = Convert.ToString(registro.iva);
                txtValorFactura.Text = Convert.ToString(registro.factura);
                txtaPagar.Text = Convert.ToString(registro.pagado);
            }

            txtIdDetalle.Text = Convert.ToString(idDet);
            activarTextos();
            //verificarConcepto();
            //txtRuc.Text = ddlColaborador.SelectedValue.Trim();
            txtRuc_TextChanged();
        }
    }


    /***************************************/


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        btnConsultar_Click();
    }

    protected void btnConsultar_Click()
    {
        string lAccion, lsucursal;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;

        lAccion = "XFECHA";

        lsucursal = ddlSucursal2.SelectedValue; ;
        lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        lFechaFin = Convert.ToDateTime(txtFechaFin.Text);

        string caja = ddlCaja.SelectedValue;


        var cEgresos = dc.sp_ListarGastosyRetenciones(lAccion, lsucursal, lFechaIni, lFechaFin, caja, 0);

        grvEgresosDetalle.DataSource = cEgresos ;
        grvEgresosDetalle.DataBind();

        pnDetalleCaja.Visible = true;


    }


    protected void grvEgresosDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lidCab = Convert.ToInt32(row.Cells[4].Text);
            int lidDet = Convert.ToInt32(row.Cells[5].Text);

            pnDetalleCaja.Visible = false;
            pnPagos.Visible = true;
            llenarCabecera(lidCab);
            llenarDetalle(lidDet);

        }

        if (e.CommandName == "verRet")
        {
            string lsuc = ddlSucursal2.SelectedValue.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[4].Text);

            string lruc = row.Cells[3].Text;
            string lnumRetencion = row.Cells[17].Text;
            string lautRetencion = row.Cells[9].Text;


            Session["pRetencion"] = lnumRetencion;
            Session["pSuc"] = lsuc;

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirRetencion.aspx','','width=800,height=750') </script>");



            //verRetencion(lsuc,lruc, lnumRetencion, lautRetencion);
        }

        if (e.CommandName == "Jus")
        {

           // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[4].Text);




            tbl_DetEgresos tbl_DetEgresos = dc.tbl_DetEgresos.SingleOrDefault(x => x.id_DetEgresos == lid);
            tbl_DetEgresos.justificado = true;

            dc.SubmitChanges();


          //  desplegarDetalleEgresos(ldoc, 0);
        }

    }

    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        //consultarRetencionProveedor();
        activarTextos();
        verificarConcepto();
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

        DateTime fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);
        DateTime fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);

        if (!lpasa)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = lmensaje;
            //btnGuardar.Visible = false;
        }
        else
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = string.Empty;
            //btnGuardar.Visible = true;
        }
        return lpasa;
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

    protected void btnCancelarpago_Click(object sender, EventArgs e)
    {
        btnCancelarpago_Click();
    }

    protected void btnCancelarpago_Click()
    {
        btnIngresaProv.Visible = false;
        pnTitulos.Enabled = true;
        btnConsultar.Visible = true;
        //pnDetallePagos.Visible = true;
        //pnMenu.Visible = true;
        pnPagos.Visible = false;
        //pnBorrar.Visible = false;
        //pnExportar.Visible = false;
        pnDetalleCaja.Visible = true;
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
        int id = Convert.ToInt32(ddlDocRet.SelectedValue);
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
                            descrip = adic.campoAdicional.Trim(),
                            emision = ret.fechaEmisionDocSustento,
                            caducidad = ret.fechaCaducidadDocSustento
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
                    txtFechaEmisionDoc.Text = Convert.ToString(registro.emision);
                    txtFechaCaducDoc.Text = Convert.ToString(registro.caducidad);


                }
            }

            txtDescripcion.Text = txtBien.Text.Trim() + ": " + string.Format("{0:#,##0.##}", bien) + "-" + txtServicio.Text.Trim() + ": " + string.Format("{0:#,##0.##}", servicio);
            //txtBsubtotal.Text = string.Format("{0:#,##0.##}", bien);
            //txtSsubtotal.Text = string.Format("{0:#,##0.##}", servicio);

        }

    }

    protected void txtBsubtotal_TextChanged(object sender, EventArgs e)
    {
        double iva = 0.12;

        txtBIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBsubtotal.Text) * iva);
        txtBtotal.Text = string.Format("{0:#,##0.##}", (Convert.ToDouble(txtBsubtotal.Text) + Convert.ToDouble(txtBtarifa0.Text)
                        + Convert.ToDouble(txtBotros.Text) + Convert.ToDouble(txtBIva.Text)));



        double Btotal = Convert.ToDouble(txtBtotal.Text);
        double Stotal = Convert.ToDouble(txtStotal.Text);
        double valorRetencion = Convert.ToDouble(txtValorRetencion.Text);
        double totalIva = (Convert.ToDouble(txtBIva.Text) + Convert.ToDouble(txtSIva.Text));
        double totalDocumento = (Btotal + Stotal);

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
        double Stotal = Convert.ToDouble(txtStotal.Text);
        double valorRetencion = Convert.ToDouble(txtValorRetencion.Text);
        double totalIva = (Convert.ToDouble(txtBIva.Text) + Convert.ToDouble(txtSIva.Text));
        double totalDocumento = (Btotal + Stotal);

        txtIva.Text = string.Format("{0:#,##0.##}", totalIva);

        txtValorFactura.Text = string.Format("{0:#,##0.##}", totalDocumento); //Convert.ToString(totalDocumento);
        txtaPagar.Text = string.Format("{0:#,##0.##}", totalDocumento - valorRetencion);
    }
    protected void grvEgresosDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            subtotal += Convert.ToDecimal(e.Row.Cells[11].Text);
           tarifa0 += Convert.ToDecimal(e.Row.Cells[12].Text);
            otros += Convert.ToDecimal(e.Row.Cells[13].Text);
            totaliva += Convert.ToDecimal(e.Row.Cells[14].Text);
            totaldoc += Convert.ToDecimal(e.Row.Cells[15].Text);



            fuente += Convert.ToDecimal(e.Row.Cells[24].Text);

            iva += Convert.ToDecimal(e.Row.Cells[25].Text);
            totalretenido += Convert.ToDecimal(e.Row.Cells[26].Text);
            apagar += Convert.ToDecimal(e.Row.Cells[27].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Totales";

            e.Row.Cells[8].Text = Convert.ToString(subtotal);
            e.Row.Cells[9].Text = Convert.ToString(tarifa0);
            e.Row.Cells[10].Text = Convert.ToString(otros);
            e.Row.Cells[11].Text = Convert.ToString(totaliva);
            e.Row.Cells[12].Text = Convert.ToString(totaldoc);
            e.Row.Cells[21].Text = Convert.ToString(fuente);
            e.Row.Cells[22].Text = Convert.ToString(iva);
            e.Row.Cells[23].Text = Convert.ToString(totalretenido);
            e.Row.Cells[24].Text = Convert.ToString(apagar);
        }
    }
}