#region USING
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
    using enviarEmail;
    using System.Net.Mail;
    using System.Text;
using MessagingToolkit.QRCode.Codec;
#endregion


public partial class Tesoreria_registrarRecaudacion : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["temporalraceConnectionString"].ConnectionString;

    Data_TemporalRaceDataContext dt = new Data_TemporalRaceDataContext();

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
            

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();

            var consultaTp = from Tp in dc.tbl_TipoPago
                             where Tp.estado == true
                             select new
                             {
                                 id = Tp.id_TipoPago,
                                 descripcion = Tp.descripcion
                             };
            ddlTipoPago.DataSource = consultaTp;
            ddlTipoPago.DataBind();
            ddlTipoPago.SelectedValue = "0";

        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void activarObjetos()
    {
        pnListadoFactura.Visible = false;
        pnAltas.Visible = false;
            pnRetencion.Visible = false;
            pnRecaudacion.Visible = false;
            pnEliminar.Visible = false;
    }

    #endregion

    #region LISTAR DATOS

        protected void btnListar_Click(object sender, EventArgs e)
        {
            btnListar_Click();
        }

        protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
        {
            string conceros;

            conceros = cadenasinceros;
            conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
            return conceros;
        }

        protected bool estaActiva()
        {
            bool lactivo;
            string lsucursal, lfactura, lnumero, lestado;
            DateTime lfecha;

            lestado = "";
            lactivo = false;
            lsucursal = ddlSucursal2.Text.Trim();
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            lfactura = txtNumFactura.Text.Trim();
            lnumero = lsucursal + txtFechaIni.Text.Trim();

            var consultaRc = from Rc in dc.tbl_CabRecaudacion
                             where Rc.NUMERO == lnumero
                             select new
                             {
                                 ESTADO = Rc.ESTADO
                             };

            if (consultaRc.Count() == 0)
            {
                lestado = "0";
            }
            else
            {
                foreach (var registro in consultaRc)
                {
                    lestado = registro.ESTADO.Trim();
                }
            }
            if (lestado == "0")
            {
                lactivo = true;
            }
            else
            {
                lactivo = false;
            }
            return lactivo;
        }

        protected void listarFacturas()
        {
            string lsucursal, lfactura;
            DateTime lfecha,lfechaFin;

            ///
            /// abilitar cabecera
            /// 
            ddlSucursal2.Enabled = true;
            txtFechaIni.Enabled = true;
            txtNumFactura.Enabled = true;


            lsucursal = ddlSucursal2.Text.Trim();
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 00:00:00");
            lfechaFin = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            lfactura = txtNumFactura.Text.Trim();


            var consultaFAC = from fac in df.FACTURA
                              where fac.FAC_SUCURSAL == lsucursal &&
                                    fac.FAC_FECHAEMISION >= lfecha  && fac.FAC_FECHAEMISION <= lfechaFin &&
                                    fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE) > 0 &&
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
                                  FAC_SRI = fac.FAC_SRI,
                                  FAC_SALDO = fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE)
                              };
            grvListadoFac.DataSource = consultaFAC;
            grvListadoFac.DataBind();
        }

        protected void listarFactura()
        {
            string lsucursal, lfactura;
            DateTime lfecha;

            ///
            /// abilitar cabecera
            /// 
            ddlSucursal2.Enabled = true;
            txtFechaIni.Enabled = true;
            txtNumFactura.Enabled = true;


            lsucursal = ddlSucursal2.Text.Trim();
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            txtNumFactura.Text = llenarCeros(txtNumFactura.Text.Trim(), '0', 9);
            lfactura = llenarCeros(txtNumFactura.Text.Trim(), '0', 9);


            var consultaFAC = from fac in df.FACTURA
                              where fac.FAC_SUCURSAL == lsucursal &&
                                    fac.FAC_SECUENCIAL == lfactura &&
                                    fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE) > 0 &&
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
                                  FAC_SRI = fac.FAC_SRI,
                                  FAC_SALDO = fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE)
                              };
            grvListadoFac.DataSource = consultaFAC;
            grvListadoFac.DataBind();
        }

        protected void listarCancelacion()
        {
            string lsucursal, lfactura, lnumero;
            DateTime lfecha;



            lsucursal = ddlSucursal2.Text.Trim();
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            lfactura = txtNumFactura.Text.Trim();
            lnumero = lsucursal + txtFechaIni.Text.Trim();

            var consultaRd = from Rd in dc.tbl_DetRecaudacion
                             where Rd.numero == lnumero
                             orderby Rd.factura
                             select new
                             {
                                 id_DetRecaudacion = Rd.id_DetRecaudacion,
                                 factura = Rd.factura,
                                 valor = Rd.valor,
                                 retencionIVA = Rd.retencionIVA,
                                 retencionFUENTE = Rd.retencionFUENTE,
                                 descripcionTipoPago = Rd.descripcionTipoPago,
                                 descripcionTipoDetalle = Rd.descripcionTipoDetalle,
                                 numeroDocumento = Rd.numeroDocumento
                             };
            grvRecaudacion.DataSource = consultaRd;
            grvRecaudacion.DataBind();
        }
    #endregion

    /*PROCESOS*/
    #region PROCESOS 
        protected void btnListar_Click()
        {
            string lfactura;

            lfactura = txtNumFactura.Text.Trim();

            if (estaActiva())
            {
                lblMensaje.Text = "";
                if (lfactura.Length <= 0)
                {
                    listarFacturas();
                }
                else
                {
                    listarFactura();
                }

                listarCancelacion();
                pnRecaudacion.Visible = true;
                pnListadoFactura.Visible = true;
            }
            else
            {
                pnRecaudacion.Visible = false;
                pnListadoFactura.Visible = false;
                lblMensaje.Text = "Esta cerrado la recaudación para esta fecha";
            }

        }

        protected void grvListadoFac_SelectedIndexChanged(object sender, EventArgs e)
        {
            aLista();
            veRegistro();
        }

        private void aLista()
        {

            string clave = grvListadoFac.SelectedValue.ToString();
            string lsucursal = ddlSucursal2.Text;
            DateTime lfecha;
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");

            lblTitulo.Text = "# Factura: " + clave;
            var consultaFAC = from fac in df.FACTURA
                              where fac.FAC_SUCURSAL == lsucursal &&
                                    fac.FAC_SECUENCIAL == clave &&
                                    fac.FAC_ESTADO == "A" &&
                                    fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE) > 0
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
                                  FAC_SRI = fac.FAC_SRI,
                                  FAC_SALDO = fac.FAC_IMPORTETOTAL - (fac.FAC_RECAUDADO + fac.FAC_RETENIDOIVA + fac.FAC_RETENIDOFUENTE)
                              };

            if (consultaFAC.Count() == 0)
            {
                lblMensaje.Text = "Sin registro";
            }
            else
            {
                foreach (var registro in consultaFAC)
                {
                    txtValorFactura.Text = registro.FAC_SALDO.ToString();
                    txtValorRetencionIVA.Text = "0,00";
                    txtValorRetencionFUENTE.Text = "0,00";
                }
            }
        }

        private void veRegistro()
        {
            txtDescripcion.Text = "";
            txtNumDocumento.Text = "";
            ddlSucursal2.Enabled = false;
            txtFechaIni.Enabled = false;
            txtNumFactura.Enabled = false;
            ddlTipoPago.SelectedIndex = 0;
            pnBotones.Visible = false;
            pnAltas.Visible = true;
            pnListadoFactura.Visible = false;
            pnRecaudacion.Visible = false;
        }

        protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string clave;

            clave = ddlTipoPago.SelectedValue;
            /*cxp*/
            lblDescripcion.Visible = true;
            lblDescripcion2.Visible = false;

     
  
            switch (clave)
            {
                case "2":
                    var consultaBk = from Bk in dc.tbl_Bancos
                                     select new
                                     {
                                         id = Bk.id_Banco,
                                         descripcion = Bk.banco
                                     };
                    ddlDescripcion.DataSource = consultaBk;
                    ddlDescripcion.DataBind();

                    lblDescripcion.Visible = false;

                    txtDescripcion.Visible = false;
                    txtDescripcion.Text = "";
                    lblDescripcion2.Visible = true;
                    lblDescripcion.Text = "Descripción";
                    ddlDescripcion.Visible = true;
                    ddlTipoPago.Visible = true;
                    break;
                case "4":
                    var consultaTc1 = from Tc in dc.tbl_TarjetaCredito
                                      orderby Tc.instituto, Tc.tarjeta
                                      select new
                                      {
                                          id = Tc.id_TarjetaCredito,
                                          descripcion = Tc.instituto + " " + Tc.tarjeta + " " + Tc.planes + " " + Tc.meses
                                      };
                    ddlDescripcion.DataSource = consultaTc1;
                    ddlDescripcion.DataBind();

                    lblDescripcion2.Visible = false;
                    txtDescripcion.Visible = false;

                    txtDescripcion.Text = "";
                    ddlDescripcion.Visible = true;
                    ddlTipoPago.Visible = true;
                    break;
                case "5":
                    var consultaTc2 = from Tc in dc.tbl_TarjetaCredito
                                      orderby Tc.instituto, Tc.tarjeta
                                      select new
                                      {
                                          id = Tc.id_TarjetaCredito,
                                          descripcion = Tc.instituto + " " + Tc.tarjeta + " " + Tc.planes + " " + Tc.meses
                                      };
                    ddlDescripcion.DataSource = consultaTc2;
                    ddlDescripcion.DataBind();
                    txtDescripcion.Visible = false;
                    txtDescripcion.Text = "";
                    ddlDescripcion.Visible = true;
                    ddlTipoPago.Visible = true;
                    break;
                case "6":
                    var consultaTc3 = from Tc in dc.tbl_TarjetaCredito
                                      orderby Tc.instituto, Tc.tarjeta
                                      select new
                                      {
                                          id = Tc.id_TarjetaCredito,
                                          descripcion = Tc.instituto + " " + Tc.tarjeta + " " + Tc.planes + " " + Tc.meses
                                      };
                    ddlDescripcion.DataSource = consultaTc3;
                    ddlDescripcion.DataBind();
                    txtDescripcion.Visible = false;
                    txtDescripcion.Text = "";
                    ddlDescripcion.Visible = true;
                    ddlTipoPago.Visible = true;
                    break;
                case "7":
                case "27":
                case "28":
                case "32":
                   var cColaborador = from Ter in dc.tbl_colaborador
                                where Ter.tipoEmpleo == 1      
                               orderby Ter.apellidos
                               select new
                               {
                                   id = Ter.Cedula,
                                   descripcion = Ter.apellidos.Trim() + " " + Ter.nombres.Trim() + " " + Ter.Cedula.Trim()
                               };

                   ddlDescripcion.DataSource = cColaborador;
                    ddlDescripcion.DataBind();
                    lblDescripcion.Visible = false;
                    txtDescripcion.Visible = false;
                    txtDescripcion.Text = "";
                    lblDescripcion2.Visible = true;
                    ddlDescripcion.Visible = true;
                    ddlTipoPago.Visible = true;

                    ListItem liTercero = new ListItem("Seleccione colaborador ", "-1");
                    ddlDescripcion.Items.Insert(0, liTercero);
                    break;
                case "31":
                    pnAltas.Visible = false;
                    pnCXPProveedor.Visible = true;
                    break;
                case "33":
                    pnAltas.Visible = false;
                    pnClienteCXC.Visible = true;
                    break;
                case "34":
                    var cPorFact = from Ter in dc.tbl_colaborador
                                       where Ter.tipoEmpleo == 2
                                       orderby Ter.apellidos
                                       select new
                                       {
                                           id = Ter.Cedula,
                                           descripcion = Ter.apellidos.Trim() + " " + Ter.nombres.Trim() + " " + Ter.Cedula.Trim()
                                       };

                    ddlDescripcion.DataSource = cPorFact;
                    ddlDescripcion.DataBind();
                    lblDescripcion.Visible = false;
                    txtDescripcion.Visible = false;
                    txtDescripcion.Text = "";
                    lblDescripcion2.Visible = true;
                    ddlDescripcion.Visible = true;
                    ddlTipoPago.Visible = true;

                    ListItem liPorFact = new ListItem("Seleccione colaborador ", "-1");
                    ddlDescripcion.Items.Insert(0, liPorFact);
                    break;
               
                default:
                    txtDescripcion.Visible = true;
                    lblDescripcion2.Visible = false;
                    ddlDescripcion.Visible = false;
                    ddlTipoPago.Visible = true;
                    break;

            }



        }

        protected void btnretencion_Click(object sender, EventArgs e)
        {
            pnBotones.Visible = false;
            pnAltas.Visible = false;
            pnListadoFactura.Visible = false;
            pnRetencion.Visible = true;
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            btnRegistrar_Click();
        }



        protected void btnRegistrar_Click()
        {
            bool pasa = true;
            string lsucursal, lsecuencial, laccion;
            decimal lrecaudado, lretenidoIVA, lretenidoFUENTE;
            string ldescripcion, ldocumento;
            int ltipopago;

            ltipopago = Convert.ToInt32(ddlTipoPago.SelectedValue);

            ldescripcion = txtDescripcion.Text.Trim();
            ldocumento = txtNumDocumento.Text.Trim();

            if (ltipopago == 3 || ltipopago == 8 || ltipopago == 9 || ltipopago == 10
                               || ltipopago == 11 || ltipopago == 26 
                               || ltipopago == 31 ||  ltipopago == 33)
            {
                if (ldescripcion.Length <= 0 || ldocumento.Length <= 0)
                {
                    lblRegistro.Text = "Debe ingresar descripción y datos del documento ";
                    pasa = false;
                }
            }
            if (ltipopago == 2 || ltipopago == 4 || ltipopago == 5 || ltipopago == 6 || ltipopago == 7 || ltipopago == 27 || ltipopago == 28 || ltipopago == 32 || ltipopago == 34)
            {
                if (ldocumento.Length <= 0)
                {
                    lblRegistro.Text = "Debe ingresar datos del documento ";
                    pasa = false;
                }
            }

            if (ltipopago == 7 || ltipopago == 27 || ltipopago == 28 || ltipopago == 32 || ltipopago == 34)
            {
                string idcolaborador = Convert.ToString(ddlDescripcion.SelectedValue);
                if (idcolaborador == "-1") {
                    lblRegistro.Text = "Debe especificar el colaborador";
                    pasa = false;
                }
            }


            if (pasa)
            {
                lblRegistro.Text = "";
                ///
                ///Registrar  el pago y retención
                ///
                laccion = "MODIFICAR";
                lsucursal = ddlSucursal2.SelectedValue;
                lsecuencial = grvListadoFac.SelectedValue.ToString();
                lrecaudado = Convert.ToDecimal(txtValorFactura.Text);
                lretenidoIVA = Convert.ToDecimal(txtValorRetencionIVA.Text);
                lretenidoFUENTE = Convert.ToDecimal(txtValorRetencionFUENTE.Text);


                try
                {
                    if (ltipopago == 11)
                    {
                        /*DEBE REGISTRA EN CXC*/
                        registrarRecaudacion();
                        registraCxC();

                    }
                    else
                    {
                        /*REGISTRA EN CABECERA DE FACTURACION*/
                        saldarcxc();
                        df.sp_abmRecaudacion(laccion, lsucursal, lsecuencial, lrecaudado, lretenidoIVA, lretenidoFUENTE, 0);
                        registrarRecaudacion();

                    }

                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;

                }
                finally
                {
                    btnCancelarRegistro_Click();
                    btnListar_Click();
                }
            }
        }

        protected void registrarRecaudacion()
        {
            int lid = registrarCabeceraRecaudacion();

            if (lid == 0)
            {
                lid = registrarCabeceraRecaudacion();
            }

            registrarDetalleRecaudacion(lid);


        }

        protected void registraCxC()
        {
            string lusuario, laccion, lsucursal, lfactura;
            int lid = registrarCabeceraRecaudacion();
            decimal lvalor, lsaldo;
            DateTime lfechaActual;


            lusuario = Convert.ToString(Session["Usuarioname"]);
            laccion = "AGREGAR";
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfactura = txtFactura.Text.Trim();
            lvalor = Convert.ToDecimal(txtValorFactura.Text);
            lsaldo = Convert.ToDecimal(txtValorFactura.Text);
            lfechaActual = DateTime.Now;


            dc.sp_abmCxC(laccion, lid, lsucursal, lfactura, lvalor, lsaldo, lfechaActual, lusuario);
        }

        protected void saldarcxc()
        {
            string lusuario, laccion, lsucursal, lfactura;
            int lid = registrarCabeceraRecaudacion();
            decimal lvalor, lsaldo;
            DateTime lfechaActual;


            lusuario = Convert.ToString(Session["Usuarioname"]);
            laccion = "RESTSAL";
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfactura = txtFactura.Text;
            lvalor = Convert.ToDecimal(txtValorFactura.Text);
            lsaldo = Convert.ToDecimal(txtValorFactura.Text);
            lfechaActual = DateTime.Now;

            lblMensaje.Text = "";
            if (lfactura.Length <= 0)
            {
                Consultarcxc();
                dc.sp_abmCxC(laccion, lid, lsucursal, lfactura, lvalor, lsaldo, lfechaActual, lusuario);
            }
        }

        protected void btnCancelarRegistro_Click(object sender, EventArgs e)
        {
            btnCancelarRegistro_Click();
        }

        protected void btnCancelarRegistro_Click()
        {
            lblMensaje.Text = string.Empty;
            pnBotones.Visible = true;
            pnAltas.Visible = false;
            pnListadoFactura.Visible = true;
            pnRetencion.Visible = false;
            pnRecaudacion.Visible = true;
            pnCXPProveedor.Visible = false;
            pnClientesCXC.Visible = false;
        
            btnListar_Click();
        }

        protected int registrarCabeceraRecaudacion()
        {
            //Verificar si existe registro cabecera de recaudacion
            int lid_cabecera;
            string laccion, lano, lperiodo, ltipo, lnumero, lsucursal, lusuario;
            DateTime lfecha, lfechaActual;

            lusuario = Convert.ToString(Session["Usuarioname"]);
            laccion = "AGREGAR";
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            lfechaActual = DateTime.Now;

            lid_cabecera = 0;
            lano = Convert.ToString(lfecha.Year);
            lperiodo = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
            ltipo = "041";
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lnumero = lsucursal + txtFechaIni.Text.Trim();

            var consultaRc = from Rc in dc.tbl_CabRecaudacion
                             where Rc.ANO == lano &&
                                    Rc.PERIODO == lperiodo &&
                                    Rc.TIPO == ltipo &&
                                    Rc.NUMERO == lnumero
                             select new { id_cab_recaudacion = Rc.id_cab_recaudacion };
            if (consultaRc.Count() == 0)
            {
                // no existe , registrar nueva fila de recaudacion
                dc.sp_abmRecaudacionCabecera(laccion, lano, lperiodo, ltipo, lnumero, lsucursal, lfecha, lusuario, lfechaActual);
            }
            else
            {
                foreach (var registro in consultaRc)
                {
                    lid_cabecera = registro.id_cab_recaudacion;
                }
            }
            return lid_cabecera;
        }

        protected void registrarDetalleRecaudacion(int lid)
        {
            ///
            ///Registra pago en detalle de recaudacion
            ///
            int lidDet = 0, ltipopago, ltipoDetalle;
            string laccion, lano, lperiodo, ltipo, lnumero, lsucursal, lfactura, lregistro;
            string ldescricionTipoPago, lnumeroDocumento, ldetalle;
            decimal lvalor, lretencionIVA, lretencionFUENTE;
            DateTime lfecha, lfechaActual;
            laccion = "AGREGAR";
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            lfechaActual = DateTime.Now;


            lano = Convert.ToString(lfecha.Year);
            lperiodo = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
            ltipo = "041";
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfactura = grvListadoFac.SelectedValue.ToString();
            lnumero = lsucursal + txtFechaIni.Text.Trim();
            lregistro = "1";
            ltipopago = Convert.ToInt32(ddlTipoPago.SelectedValue);
            ldescricionTipoPago = Convert.ToString(ddlTipoPago.SelectedItem);
            ltipoDetalle = 0;
            ldetalle = "";

            if (ltipopago == 2 || ltipopago == 4 
                || ltipopago == 5 || ltipopago == 6
                || ltipopago == 7 || ltipopago == 27 || ltipopago == 28 || ltipopago == 32 || ltipopago == 34)
            {
                ltipoDetalle = Convert.ToInt32(ddlDescripcion.SelectedValue);
                ldetalle = Convert.ToString(ddlDescripcion.SelectedItem);
            }
            if (ltipopago == 1 || ltipopago == 3 || ltipopago == 8 || ltipopago == 9 || ltipopago == 26)
            {
                ltipoDetalle = 0;
                ldetalle = txtDescripcion.Text;
            }

            if (ltipopago == 31 || ltipopago == 32 || ltipopago == 33)
            {
                ltipoDetalle = 0;
                ldetalle = txtDescripcion.Text.Trim();
            }



            lnumeroDocumento = txtNumDocumento.Text.Trim();

            lvalor = Convert.ToDecimal(txtValorFactura.Text);
            lretencionIVA = Convert.ToDecimal(txtValorRetencionIVA.Text);
            lretencionFUENTE = Convert.ToDecimal(txtValorRetencionFUENTE.Text);

            dc.sp_abmRecaudacionDetalle(laccion, lid, lidDet, lano, lperiodo, ltipo, lnumero, lsucursal, lfactura,
                                                lregistro, lvalor, lretencionIVA, lretencionFUENTE,
                                                ltipopago, ldescricionTipoPago, ltipoDetalle, ldetalle, lnumeroDocumento);

        }

        protected void Consultarcxc()
        {
            string lusuario, laccion, lsucursal, lfactura;
            int lid = registrarCabeceraRecaudacion();
            decimal lvalor, lsaldo;
            DateTime lfechaActual;


            lusuario = Convert.ToString(Session["Usuarioname"]);
            laccion = "AGREGAR";
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfactura = txtFactura.Text;
            lvalor = Convert.ToDecimal(txtValorFactura.Text);
            lsaldo = Convert.ToDecimal(txtValorFactura.Text);
            lfechaActual = DateTime.Now;

            lblMensaje.Text = "";
            if (lfactura.Length <= 0)
            {
                var consultaCxc = from tcxc in dc.tbl_CuentasxCobrar
                                  where tcxc.sucursal == lsucursal &&
                                         tcxc.factura == lfactura
                                  select new { id_CuentasxCobrar = tcxc.id_CuentasxCobrar };
                if (consultaCxc.Count() == 0)
                {
                    // no existe , registrar nueva fila en cxc
                    dc.sp_abmCxC(laccion, 0, lsucursal, lfactura, lvalor, lsaldo, lfechaActual, lusuario);
                }
            }
        }

        protected void btnCancelarRetencion_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            pnBotones.Visible = false;
            pnAltas.Visible = true;
            pnListadoFactura.Visible = false;
            pnRetencion.Visible = false;
        }

        protected void grvRecaudacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            pnBotones.Visible = false;
            pnAltas.Visible = false;
            pnListadoFactura.Visible = false;
            pnRetencion.Visible = false;
            pnRecaudacion.Visible = false;
        }

        protected void grvRecaudacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lidDetalle;
            lidDetalle = Convert.ToInt32(grvRecaudacion.SelectedValue);

            var consultaRd1 = from Rd in dc.tbl_DetRecaudacion
                              where Rd.id_DetRecaudacion == lidDetalle
                              orderby Rd.factura
                              select new
                              {
                                  id_DetRecaudacion = Rd.id_DetRecaudacion,
                                  factura = Rd.factura,
                                  valor = Rd.valor,
                                  retencionFUENTE = Rd.retencionFUENTE,
                                  retencionIVA = Rd.retencionIVA,
                                  descripcionTipoPago = Rd.descripcionTipoPago,
                                  descripcionTipoDetalle = Rd.descripcionTipoDetalle,
                                  numeroDocumento = Rd.numeroDocumento
                              };

            if (consultaRd1.Count() == 0)
            {
                lblMensaje.Text = "Sin registro";
            }
            else
            {
                foreach (var registro in consultaRd1)
                {
                    txtFactura.Text = registro.factura;
                    txtValor.Text = Convert.ToString(registro.valor);
                    txtRetencionIVA.Text = Convert.ToString(registro.retencionIVA);
                    txtRetencionFUENTE.Text = Convert.ToString(registro.retencionFUENTE);
                    txtTipoDescripcion.Text = registro.descripcionTipoPago;
                    txtTipoDetalle.Text = registro.descripcionTipoDetalle;
                    txtDocumento.Text = registro.numeroDocumento;
                    txtIdDetalle.Text = Convert.ToString(registro.id_DetRecaudacion);

                }
            }
            pnTitulos.Enabled = false;
            pnBotones.Visible = false;
            pnAltas.Visible = false;
            pnRetencion.Visible = false;
            pnListadoFactura.Visible = false;
            pnRetencion.Visible = false;
            pnRecaudacion.Visible = false;
            pnEliminar.Visible = true;
        }

        protected void btnEliminarPago_Click(object sender, EventArgs e)
        {
            ///
            ///Registra pago en detalle de recaudacion
            ///
            bool pasa = true;
            int lid = 0;
            int lidDet = 0;
            int ltipopago, ltipoDetalle;
            string laccion, lano, lperiodo, ltipo, lnumero, lsucursal, lfactura, lregistro;
            string ldescricionTipoPago, lnumeroDocumento, ldetalle;
            decimal lvalor, lretencionIVA, lretencionFUENTE;
            DateTime lfecha, lfechaActual;


            laccion = "BORRAR";
            lfecha = Convert.ToDateTime(txtFechaIni.Text + " 23:59:59");
            lfechaActual = DateTime.Now;

            lidDet = Convert.ToInt32(txtIdDetalle.Text);
            lano = Convert.ToString(lfecha.Year);
            lperiodo = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
            ltipo = "041";
            lsucursal = ddlSucursal2.SelectedValue.Trim();
            lfactura = txtFactura.Text.Trim();
            string lfactRev = lsucursal+Convert.ToString(Convert.ToInt32(txtFactura.Text.Trim()));
            lblMensaje.Text = lfactRev;
            lnumero = lsucursal + txtFechaIni.Text.Trim();
            lregistro = "1";
            ltipopago = Convert.ToInt32(ddlTipoPago.SelectedValue);
            ldescricionTipoPago = Convert.ToString(ddlTipoPago.SelectedItem);
            ltipoDetalle = 0;
            ldetalle = "";
            lvalor = Convert.ToDecimal(txtValor.Text);
            lretencionIVA = Convert.ToDecimal(txtRetencionIVA.Text);
            lretencionFUENTE = Convert.ToDecimal(txtRetencionFUENTE.Text);

            lnumeroDocumento = "";

            var cContrato = from cC in dt.socios
                            where cC.factura.Trim() == lfactRev
                            select new { ncontrato_membr = cC.ncontrato_membr };
            if (cContrato.Count() == 0)
            {
                // no existe , registrar nueva fila de recaudacion
                pasa = true;
            }
            else
            {
                pasa = false;
            }

            if (pasa)
            {
                try
                {
                    if (ltipopago == 11)
                    {
                        dc.sp_abmRecaudacionDetalle(laccion, lid, lidDet, lano, lperiodo, ltipo, lnumero, lsucursal, lfactura,
                                                   lregistro, lvalor, lretencionIVA, lretencionFUENTE,
                                                   ltipopago, ldescricionTipoPago, ltipoDetalle, ldetalle, lnumeroDocumento);
                    }
                    else
                    {
                        dc.sp_abmRecaudacionDetalle(laccion, lid, lidDet, lano, lperiodo, ltipo, lnumero, lsucursal, lfactura,
                                                        lregistro, lvalor, lretencionIVA, lretencionFUENTE,
                                                        ltipopago, ldescricionTipoPago, ltipoDetalle, ldetalle, lnumeroDocumento);

                        laccion = "RESTSAL";
                        df.sp_abmRecaudacion(laccion, lsucursal, lfactura, lvalor, lretencionIVA, lretencionFUENTE, 0);
                    }

                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;

                }
                finally
                {
                    btnCancelarEliminar_Click();
                    btnListar_Click();
                }
            }
            else { 
                lblMensaje.Text = "No puede eliminar el pago, la factura está atada a una membresía";
            }
        }

        protected void btnCancelarEliminar_Click(object sender, EventArgs e)
        {
            btnCancelarEliminar_Click();
        }

        protected void btnCancelarEliminar_Click()
        {
            lblMensaje.Text = string.Empty;
            pnTitulos.Enabled = true;
            pnBotones.Visible = true;
            pnAltas.Visible = false;
            pnRetencion.Visible = false;
            pnListadoFactura.Visible = true;
            pnRetencion.Visible = false;
            pnRecaudacion.Visible = true;
            pnEliminar.Visible = false;
            ddlDescripcion.Visible = false;

        }

        protected void ddlDescripcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ltipopago = Convert.ToInt32(ddlTipoPago.SelectedValue);
            if (ltipopago == 7 || ltipopago == 27 || ltipopago == 28 || ltipopago == 32 || ltipopago == 34) 
            {
                txtNumDocumento.Text = Convert.ToString(ddlDescripcion.SelectedValue);
            }
        }
    #endregion

    /*PANTALLA DE CUENTAS POR PAGAR*/
    #region CUENTAS POR PAGAR
    protected void btnproveedor_Click(object sender, EventArgs e)
    {
        string proveedor = txtProveedor.Text.Trim();
        listarCXP(proveedor);
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        pnAltas.Visible = true;
        pnCXPProveedor.Visible = false;
    }


    protected void listarCXP(string ruc) {
        // sp_ListarCXP 'XCEDULA' ,'1792323096001', ''
        var cCXP = dc.sp_ListarCXP("XCEDULA", ruc, "");

    
        grvFacturasProveedor.DataSource = cCXP;
        grvFacturasProveedor.DataBind();


        if (grvFacturasProveedor.Rows.Count <= 0) 
        {
            lblMensaje.Text = "No existen facturas de cuentas por pagar para este proveedor";
        }

    }

    protected void grvFacturasProveedor_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName.Equals("Edit"))
            {
                lblMensaje.Text = "editar";
                lblMensaje.Text = Convert.ToString(grvFacturasProveedor.FooterRow.FindControl("txtAbono"));
            }
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }
    protected void grvFacturasProveedor_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string sucursal = ddlSucursal2.SelectedValue;
        string usuario = (string)Session["SUsername"];
        string proveedor = txtProveedor.Text.Trim();
       
       //
        // Obtengo el id de la entidad que se esta editando
        // en este caso de la entidad Person
        //
        
        grvFacturasProveedor.EditIndex = e.NewEditIndex;

        listarCXP(proveedor);
    }

    protected void grvFacturasProveedor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        string proveedor = txtProveedor.Text.Trim();
        grvFacturasProveedor.EditIndex = -1;
        listarCXP(proveedor);
    }
    protected void grvFacturasProveedor_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sucursal = ddlSucursal2.SelectedValue;
        string nombreEstablecimiento = Convert.ToString(ddlSucursal2.SelectedItem);
        string usuario = (string)Session["SUsername"];
        string proveedor = txtProveedor.Text.Trim();
        int idDetalle = Convert.ToInt32(grvFacturasProveedor.DataKeys[e.RowIndex].Value.ToString());
        
        GridViewRow row = grvFacturasProveedor.Rows[e.RowIndex];

        string nombreProveedor = Convert.ToString(row.Cells[0].Text);
        string nombreSucursal = Convert.ToString(row.Cells[1].Text);
        string facturaUtilzada = Convert.ToString(row.Cells[2].Text);
        decimal  saldoCxp = Convert.ToDecimal(row.Cells[3].Text);
        decimal valorfactura = Convert.ToDecimal(txtValorFactura.Text);

      

        string documentoAfectado = lblTitulo.Text.Substring(11);
        
        var abono = grvFacturasProveedor.Rows[e.RowIndex].FindControl("txtAbono") as TextBox;

        decimal valorAbono = Convert.ToDecimal(abono.Text);
        string cValorfactura = Convert.ToString(abono.Text);

        try
        {

            if (valorAbono <= valorfactura && valorAbono <= saldoCxp)
            {


                txtValorFactura.Text = cValorfactura;
                txtDescripcion.Text = proveedor.Trim() + " " + nombreProveedor.Trim() + " #Factura" + documentoAfectado.Trim() + "-Establecimiento emisor:" + nombreSucursal.Trim() + "-Establecimiento receptor:" + nombreEstablecimiento.Trim();
                txtNumDocumento.Text = facturaUtilzada.Trim();
                dc.sp_AbonosXCruce("GUARDAR", 0, DateTime.Now, proveedor, facturaUtilzada, valorAbono, documentoAfectado, usuario, DateTime.Now, sucursal);
                dc.sp_abmEgresosDetalleCXP("MODIFICAR", idDetalle, valorAbono);
                btnRegistrar_Click();
                lblMensaje.Text = "Valor actualizado";
                enviarMail();
            }
            else {
                lblMensaje.Text = "Valor no actualizado";
            }

            grvFacturasProveedor.EditIndex = -1;
            //
            listarCXP(proveedor);
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }
    #endregion

    #region CORREO ELECTRONICO (EMAIL)
    protected void enviarMail()
    {
        
        string colaborador = "BYRON ORTEGA";
        string documento = "Se ha realizado el registro de cancelación de facturas con las siguientes características:"
                                + " Valor:" + txtValorFactura.Text
                                + " proveedor:" + txtDescripcion.Text.Trim() 
                                + " " + txtNumDocumento.Text;
        string email = "sistemas@aneta.org.ec";

        if (enviarCorreoHtml(colaborador, email, documento))
        {
            
        }
    }

    

    public bool enviarCorreoHtml(string colaborador, string email, string documento)
    {
        bool lenvio = false;

        /*VARIABLES ESCUELA*/
        string accion = "CONSULTAR";
        string escuela = ddlSucursal2.SelectedValue;
        string administradorEscuela = string.Empty;
        string tituloAdministrador = "Director(a) de Escuela";
        string direccionEscuela = string.Empty;
        string ciudadEscuela = string.Empty;
        string telefonoEscuela = string.Empty;
        string emailEscuela = string.Empty;
        string paginaWeb = "www.aneta.org.ec";
        string caminoLogo = string.Empty;

        var cEscuela = dc.sp_abmRuc2(accion, "", "", "", "", escuela, "", "", "", "", "", "", "", "", false, "", "");


        foreach (var registro in cEscuela)
        {
            administradorEscuela = registro.administrador;
            direccionEscuela = registro.dirEstablecimiento;
            ciudadEscuela = registro.ciudad;
            telefonoEscuela = registro.telefono;
            emailEscuela = registro.email;
        }

        string mailOficina = "jose_espinosa3l@hotmail.com"; //"sistemas@aneta.org.ec"; 

        /*VARIABLES DEL ESTUDIANTE*/

        string Filename1 = string.Empty;
        string Filename2 = string.Empty;
        string Filename3 = string.Empty;
        string Filename4 = string.Empty;


        Filename1 = Server.MapPath("~//Images//socios//black//standarFront1_387.jpg");
        Filename2 = Server.MapPath("~//Images//socios//black//standarFront2_147.jpg");
        Filename3 = Server.MapPath("~//Images//socios//black//standarFront3_236.jpg");
        Filename4 = Server.MapPath("~//Images//socios//black//standarBack_387.jpg");

        caminoLogo = "~//Plantillas//mensajesInternos.html";





        StringBuilder emailHtml = new StringBuilder(File.ReadAllText(Server.MapPath(caminoLogo)));




        emailHtml.Replace("COLABORADOR", colaborador);
        emailHtml.Replace("MENSAJE", documento);
        emailHtml.Replace("ADMESCUELA", administradorEscuela);
        emailHtml.Replace("TITADMINISTRADOR", tituloAdministrador);
        emailHtml.Replace("DIRECCIONESCUELA", direccionEscuela);
        emailHtml.Replace("CIUDADESCUELA", ciudadEscuela);
        emailHtml.Replace("TELEFONOESCUELA", telefonoEscuela);

        emailHtml.Replace("PAGINAWEBESCUELA", paginaWeb);

        // emailHtml.Replace("codigoQR", codigoQR);

        string envio = "1";
        string destinatarios = string.Empty;
        string cc = string.Empty;
        string tituloEmail = "registro cruce de cuentas con proveedor";

        if (envio == "1")
        {
            destinatarios = "jeec1965@gmail.com,jose_espinosa3l@hotmail.com, sistemas@aneta.org.ec"; //emailEstudiante;
            cc = "";//"bortega@aneta.org.ec";//emailEscuela; //txtEmail.Text.Trim().ToLower() + "," + "socios@aneta.org.ec";
        }



        string anexo = "";//Server.MapPath("~//images//iconos//logo2.jpg");


        //new email().enviarCorreo("192.168.1.101", 25, "socios@serviciosaneta.org.ec", "aneta54", "MEMBRESIAS-ANETA", destinatarios, cc, "TARJETA VIRTUAL ANETA", anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4);
        //enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo, string front1, string front2, string front3, string back1)

        // if (new email().enviarCorreo("smtp.gmail.com", 25, "socios@serviciosaneta.org.ec", "lxane2k11", "MEMBRESIAS-ANETA", destinatarios, cc, tituloEmail , anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4))
        if (new email().enviarCorreo("192.168.1.110", 25, "socios@aneta.org.ec", "aneta54", "ESCUELA-ANETA", destinatarios, cc, tituloEmail, anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4))
        {
            //lblMensaje.Text = lblMensaje.Text + " Se envío el correo electrónico";
            lenvio = true;
        }
        else
        {
            //lblMensaje.Text = lblMensaje.Text + " Fallo en el envío de correo electrónico";
            lenvio = false;
        }
        //email.enviarCorreo("192.168.1.101", 25, "socios@serviciosaneta.org.ec", "aneta54", "MEMBRESIAS-ANETA", destinatarios, cc, "TARJETA VIRTUAL ANETA", anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4);
        // lblMensaje.Text = email.mensaje.
        return lenvio;
    }

    protected string formatoEstandar()
    {
        string cuerpo = string.Empty;
        
        return cuerpo;
    }



     #endregion

    #region CODIGO QR
    protected string generaQR(string clave)
    {
        string codigoQR = string.Empty;

        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap img = encoder.Encode(clave.Trim());
        System.Drawing.Image QR = (System.Drawing.Image)img;

        using (MemoryStream ms = new MemoryStream())
        {
            QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = ms.ToArray();
            codigoQR = "data:image/gif;base64," + Convert.ToBase64String(imageBytes);

        }

        return codigoQR;
    }
    #endregion

    #region CUENTAS POR COBRAR
    protected void btnClienteCXC_Click(object sender, EventArgs e)
    {
        string clienteCXC = txtClienteCXC.Text.Trim();
        listarCXC(clienteCXC);
    }

    protected void btnRegresarCXC_Click(object sender, EventArgs e)
    {
        btnRegresarCXC_Click();
    }

    protected void btnRegresarCXC_Click()
    {
        pnAltas.Visible = true;
        pnClienteCXC.Visible = false;
    }

    protected void grvClientesCXC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.Equals("EditCXC"))
            {
                lblMensaje.Text = "editarCXC";
                lblMensaje.Text = Convert.ToString(grvClientesCXC.FooterRow.FindControl("txtAbonoCXC"));
            }
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }

    protected void grvClientesCXC_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string sucursal = ddlSucursal2.SelectedValue;
        string usuario = (string)Session["SUsername"];
        string ClienteCXC = txtClienteCXC.Text.Trim();

        //
        // Obtengo el id de la entidad que se esta editando
        // en este caso de la entidad Person
        //

        grvClientesCXC.EditIndex = e.NewEditIndex;

        listarCXC(ClienteCXC);
    }
    
    protected void grvClientesCXC_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        string clienteCXC = txtClienteCXC.Text.Trim();
        grvClientesCXC.EditIndex = -1;
        listarCXC(clienteCXC);
    }
   
    
    protected void grvClientesCXC_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sucursal = ddlSucursal2.SelectedValue;
        string nombreEstablecimiento = Convert.ToString(ddlSucursal2.SelectedItem);
        string usuario = (string)Session["SUsername"];
        string clienteCXC = txtClienteCXC.Text.Trim();
        int idDetalle = Convert.ToInt32(grvClientesCXC.DataKeys[e.RowIndex].Value.ToString());

        GridViewRow row = grvClientesCXC.Rows[e.RowIndex];

        string nombreCliente = Convert.ToString(row.Cells[0].Text);
        string nombreSucursal = Convert.ToString(row.Cells[1].Text);
        string facturaUtilzada = Convert.ToString(row.Cells[2].Text);
        decimal saldoCxp = Convert.ToDecimal(row.Cells[3].Text);
        decimal valorfactura = Convert.ToDecimal(txtValorFactura.Text);

        /*NUMERO DE FACTURA Q ES UTILIZADA COMO PARTE DE PAGO*/
        string documentoAfectado = lblTitulo.Text.Substring(11);

        var abonoCXC = grvClientesCXC.Rows[e.RowIndex].FindControl("txtAbonoCXC") as TextBox;

        decimal valorAbono = Convert.ToDecimal(abonoCXC.Text);
        string cValorfactura = Convert.ToString(abonoCXC.Text);

        try
        {

            if (valorAbono <= valorfactura && valorAbono <= saldoCxp)
            {


                txtValorFactura.Text = cValorfactura;
                txtDescripcion.Text = clienteCXC.Trim() + " " + nombreCliente.Trim() + " #Factura" + documentoAfectado.Trim() + "-Establecimiento emisor:" + nombreSucursal.Trim() + "-Establecimiento receptor:" + nombreEstablecimiento.Trim();
                txtNumDocumento.Text = facturaUtilzada.Trim();
                dc.sp_AbonosXCruceCXC("GUARDAR", idDetalle,0,DateTime.Now, clienteCXC, facturaUtilzada, valorAbono, documentoAfectado, usuario, DateTime.Now, sucursal);
                dc.sp_abmAbonoClienteCabeceraCXC("MODIFICAR", idDetalle, valorAbono);
                btnRegistrar_Click();
                lblMensaje.Text = "Valor actualizado";
                enviarMail();
            }
            else
            {
                lblMensaje.Text = "Valor no actualizado";
            }

            grvFacturasProveedor.EditIndex = -1;
            //
            listarCXC(clienteCXC);
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }


    protected void listarCXC(string ruc)
    {
        // sp_ListarCXC 'XCEDULA' ,'1792323096001', ''
        var cCXP = dc.sp_ListarCXC("XCEDULA", ruc, "");


        grvClientesCXC.DataSource = cCXP;
        grvClientesCXC.DataBind();


        if (grvClientesCXC.Rows.Count <= 0)
        {
            lblMensaje.Text = "No existen facturas de cuentas por cobrar para este cliente";
        }

    }
    #endregion

    
}