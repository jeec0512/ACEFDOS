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

public partial class Egreso_registroEgresos2 : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    #region INICIO
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            perfilUsuario();
            activarObjetos();
            blanquearObjetos();
            blancoxCero();
           
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
            // txtFechaEmisionDoc.Text = Convert.ToString(lfecha);
            //txtFechaCaducDoc.Text = Convert.ToString(lfecha);

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
        pnPagos.Visible = false;
        pnBorrar.Visible = false;
        pnExportar.Visible = false;
        txtFechaCaducDoc.Text = string.Empty;
        txtFechaEmisionDoc.Text = string.Empty;
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
        /*****************************************/
        string caja = ddlCaja.SelectedValue;

        if (caja == "S")
        {
            lnumero = ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();
            txtNumero.Text = lnumero;
        }
        else
        {
            DateTime docFecha = DateTime.Today;
            docFecha = Convert.ToDateTime(txtFecha.Text);
            string dia, mes, ano;

            dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
            mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
            ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);

            lnumero = caja + ddlSucursal2.SelectedValue.Trim() + dia + mes + ano;
            txtNumero.Text = lnumero;
        }
        /****************************************/


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

    protected bool listarPagos()
    {
        bool pasa = false;
        int cuantos;
        string laccion, lnumero, lestado, dia, mes, ano;
        string caja = ddlCaja.SelectedValue;
        string lsucursal = ddlSucursal2.SelectedValue.Trim();
        DateTime lfecha = Convert.ToDateTime(txtFecha.Text);

        lestado = string.Empty;
        laccion = "DETALLE";


        lnumero = ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();

        if (caja != "S")
        {
            dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
            mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
            ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);
            lnumero = caja + lsucursal + dia + mes + ano;
        }

        var concultaDetEgresos = dc.sp_ConsultaEgresosDetallexNumero2(laccion, lnumero);

        grvDetallePagos.DataSource = concultaDetEgresos;
        grvDetallePagos.DataBind();
        cuantos = grvDetallePagos.Rows.Count;

        if (cuantos <= 0)
        {
            pasa = false;
        }
        else
        {
            pasa = true;

        }

        return pasa;

    }

    protected void desactivarTextos()
    {
        pnTitulos.Enabled = false;
        pnDetallePagos.Visible = false;
        pnExportar.Visible = false;

        lblColaborador.Visible = false;
        pnColaborador.Visible = false;
        ddlColaborador.Visible = false;

        lblSerie.Visible = false;
        txtSerie.Visible = false;
        txtDocAutorizacion.Visible = true;

        lblDocRet.Visible = false;
        pnDocRet.Visible = false;
        ddlDocRet.Visible = false;

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

        ddlBiGastosM.DataSource = cCon;
        ddlBiGastosM.DataBind();

        ddlB0GastosM.DataSource = cCon;
        ddlB0GastosM.DataBind();

        ddlSiGastosM.DataSource = cCon;
        ddlSiGastosM.DataBind();

        ddlS0GastosM.DataSource = cCon;
        ddlS0GastosM.DataBind();

        ListItem listCon = new ListItem("Seleccione Concepto", "-1");

        ddlBiGastos.Items.Insert(0, listCon);
        ddlSiGastos.Items.Insert(0, listCon);
        ddlBiGastosM.Items.Insert(0, listCon);
        ddlB0GastosM.Items.Insert(0, listCon);
        ddlSiGastosM.Items.Insert(0, listCon);
        ddlS0GastosM.Items.Insert(0, listCon);
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

        ddlSiCodCble.DataSource = cCble;
        ddlSiCodCble.DataBind();


        ddlBiCodCbleM.DataSource = cCble;
        ddlBiCodCbleM.DataBind();

        ddlB0CodCbleM.DataSource = cCble;
        ddlB0CodCbleM.DataBind();

        ddlSiCodCbleM.DataSource = cCble;
        ddlSiCodCbleM.DataBind();

        ddlS0CodCbleM.DataSource = cCble;
        ddlS0CodCbleM.DataBind();

        

        

        ListItem listCble = new ListItem("Seleccione código contable", "-1");

        ddlBiCodCble.Items.Insert(0, listCble);
        ddlSiCodCble.Items.Insert(0, listCble);
        ddlBiCodCbleM.Items.Insert(0, listCble);
        ddlB0CodCbleM.Items.Insert(0, listCble);
        ddlSiCodCbleM.Items.Insert(0, listCble);
        ddlS0CodCbleM.Items.Insert(0, listCble);

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

    protected void encerarTextos()
    {
        string lsuc = ddlSucursal2.SelectedValue.Trim();
        string lcco = "-1";
        double lvalor = 0;

        ddlTipoDocumento.SelectedValue = "-1";
        ddlColaborador.SelectedValue = "-1";
        ddlDocRet.SelectedValue = "-1";
        ddlAfectaSucursal.SelectedValue = lsuc;
        ddlAfectaCcosto.SelectedValue = lcco;
        ddlTipoPago.SelectedValue = "-1";
        ddlBiGastos.SelectedValue = "-1";
        ddlBiCodCble.SelectedValue = "-1";
        ddlSiGastos.SelectedValue = "-1";
        ddlSiCodCble.SelectedValue = "-1";

        txtRuc.Text = string.Empty;
        txtNombres.Text = string.Empty;
        txtSerie.Text = string.Empty;
        txtNumDocumento.Text = string.Empty;
        txtDocAutorizacion.Text = string.Empty;
        txtFechaCaducDoc.Text = string.Empty;
        txtFechaEmisionDoc.Text = string.Empty;

        txtAutorizacion.Text = string.Empty;
        txtDescripcion.Text = string.Empty;

        txtBien.Text = string.Empty;
        txtServicio.Text = string.Empty;
        txtNumAutorizacion.Text = string.Empty;
        txtNumretencion.Text = string.Empty;


        txtBsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBtarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBtotal.Text = string.Format("{0:#,##0.##}", lvalor);

        txtSsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtStarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtStotal.Text = string.Format("{0:#,##0.##}", lvalor);

        txtValorRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
        txtIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtValorFactura.Text = string.Format("{0:#,##0.##}", lvalor);

        txtaPagar.Text = string.Format("{0:#,##0.##}", lvalor);
        txtApagarM.Text = string.Format("{0:#,##0.##}", lvalor);
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



        if (txtFechaEmisionDoc.Text.Length <= 0)
        {

            lmensaje = " Ingrese fecha de emisión del documento ";
            pasa = false;
        }


        if (txtFechaCaducDoc.Text.Length <= 0)
        {

            lmensaje = " Ingrese fecha de caducidad del documento ";
            pasa = false;
        }


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


        if (DocAutorizacion.Length < 10)
        {
            lmensaje = lmensaje + " El número de autorización debe ser de 10 dígitos si es manual ó 49 si es digital";
            pasa = false;
        }

        if (DocAutorizacion.Length > 49)
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
        if (TipoDocumento == "29") {
            pasa = true;
        }

        return Tuple.Create(pasa, lmensaje);
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

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void activarTextos()
    {


        /*************************/
        pnBienIva.Visible = true;
        pnServicioIva.Visible = true;
        pnTotales.Visible = true;
        pnValores.Visible = false;
        pnRetener.Visible = false;
        /*********************/
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
               // lblNumAutorizacion.Visible = false;
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
                //lblNumAutorizacion.Visible = false;
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
                txtValorRetencion.Enabled = false;
                //lblNumAutorizacion.Visible = true;
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
                //lblNumAutorizacion.Visible = false;
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
                txtValorRetencion.Enabled = false;
                //lblNumAutorizacion.Visible = true;
                txtNumAutorizacion.Visible = true;
                txtNumretencion.Visible = true;
                break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 25:
            case 26:
            case 28:
            case 32:

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
                //lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;

            case 11:
            case 12:
            case 27:
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
                //lblNumAutorizacion.Visible = false;
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
                //lblNumAutorizacion.Visible = false;
                txtNumAutorizacion.Visible = false;
                txtNumretencion.Visible = false;
                break;
            case 29:
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

                lblValorRetencion.Visible = true;
                txtValorRetencion.Visible = true;
                txtValorRetencion.Enabled = true;
                //lblNumAutorizacion.Visible = true;
                txtNumAutorizacion.Visible = true;
                txtNumretencion.Visible = true;

                pnBienIva.Visible = true;
                pnServicioIva.Visible = true;
                pnTotales.Visible = true;
                pnValores.Visible = true;
                pnRetener.Visible = true;
                break;
            case 30:
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

                lblValorRetencion.Visible = true;
                txtValorRetencion.Visible = true;
                txtValorRetencion.Enabled = true;
                //lblNumAutorizacion.Visible = true;
                txtNumAutorizacion.Visible = true;
                txtNumretencion.Visible = true;


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
            case 32:
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

    protected void desActivarTextos()
    { }

    #endregion

    #region PROCESOS OBJETOS
    /*PROCESOS PARA NUEVO PAGO*/
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
            //lblMensaje.Text = "";
            if (estadoCierre())
            {

                //lblMensaje.Text = "";
                pnDetallePagos.Visible = true;
                pnMenu.Visible = true;

                if (listarPagos())
                {
                    pnExportar.Visible = true;
                }
                else
                {
                    pnExportar.Visible = false;
                }

            }
            else
            {
                lblMensaje.Text = "Caja cerrada no se puede modificar";
                if (listarPagos())
                {
                    pnDetallePagos.Visible = true;
                    pnMenu.Visible = false;
                    pnExportar.Visible = true;
                }
                else
                {
                    pnDetallePagos.Visible = false;
                    pnExportar.Visible = false;
                }
            }

        }
    }


    protected void btnNuevoRegistro_Click(object sender, EventArgs e)
    {
        pnMensaje2.Visible = true;
        lblMensaje.Text = string.Empty;
        btnIngresaProv.Visible = true;
        pnTitulos.Enabled = false;
        btnConsultar.Visible = false;
        pnMenu.Visible = false;
        pnBorrar.Visible = false;
        pnPagos.Visible = true;
        pnDetallePagos.Visible = false;



        desactivarTextos();
        llenarListados();
        encerarTextos();


        ddlTipoDocumento.Focus();

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        perfilUsuario();
        activarObjetos();
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

        if (txtFechaEmisionDoc.Text.Length > 0)
        {

            DateTime fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);

        }


        if (txtFechaCaducDoc.Text.Length > 0)
        {


            DateTime fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);
        }

        if (!lpasa)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = lmensaje;
            btnGuardar.Visible = false;
        }
        else
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = string.Empty;
            btnGuardar.Visible = true;
        }
        return lpasa;
    }

    protected void btnGrabarPago_Click(object sender, EventArgs e)
    {
        btnGrabarPago_Click();
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

    /*PROCESOS PARA ELIMINAR PAGO*/
    protected void btnBorrarPago_Click(object sender, EventArgs e)
    {
        DateTime fechaEmisionDoc = DateTime.Today;
        DateTime fechaCaducDoc = DateTime.Today;

        if (txtFechaEmisionDoc.Text.Length > 0)
        {
            fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);
        }

        if (txtFechaCaducDoc.Text.Length > 0)
        {
            fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);
        }

        string accion = "BORRAR";
        int id_DetEgresos = Convert.ToInt32(grvDetallePagos.SelectedValue);
        var cDet = dc.sp_abmEgresosDetalle3(accion, id_DetEgresos, 0, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, fechaEmisionDoc, fechaCaducDoc);
        btnRegresar3_Click();

    }

    protected void btnRegresar3_Click(object sender, EventArgs e)
    {
        btnRegresar3_Click();
    }

    protected void btnRegresar3_Click()
    {
        pnTitulos.Enabled = true;
        btnConsultar.Visible = true;
        pnBorrar.Visible = false;
        pnDetallePagos.Visible = true;
        pnPagos.Visible = false;
        pnExportar.Visible = true;
        btnConsultar_Click();
    }

    protected void btnExcelRe_Click(object sender, EventArgs e)
    {
        //uno();
        todos();
    }
    /*PROCESOS PARA GUARDAR PROVEEDOR*/
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string accion, ruc, razonsocial, nombreComercial, dirMatriz, contribuyenteEspecial, obligadoContabilidad, e_mail, telefono;

        /* CONSTANTES */
        accion = "GUARDAR";

        /*VARIABLES*/
        ruc = txtProveedor.Text;
        razonsocial = txtrazonsocial.Text;
        nombreComercial = txtnombreComercial.Text;
        dirMatriz = txtdirMatriz.Text;
        contribuyenteEspecial = txtcontribuyenteEspecial.Text;
        obligadoContabilidad = ddlObligado.SelectedValue;
        e_mail = txtEmail.Text;
        telefono = txtTel.Text;

        /*VALIDAR INFORMACION*/

        if (ruc.Length < 10
            || razonsocial.Length <= 5
            || nombreComercial.Length <= 3
            || dirMatriz.Length < 20
            || telefono.Length < 9
            || e_mail.Length < 10)
        {
            lblAviso.Text = "Ingrese toda la información,identificación válido,razón social, la dirección debe tener provincia, ciudad, calles y sector, teléfono con código de provincia";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            dc.sp_abmMatriz2(accion, ruc, razonsocial, nombreComercial, dirMatriz, contribuyenteEspecial, obligadoContabilidad, e_mail, telefono);
            blanquearSucursal();
            lblMensaje.Text = razonsocial.Trim() + "guardado correctamente";
        }
    }

    protected void btnRegresar2_Click(object sender, EventArgs e)
    {
        pnIngresarProveedor.Visible = false;

        pnTitulos.Visible = true;
        pnPagos.Visible = true;
    }

    /*PROCESOS AL SELECCIONAR UN PAGO*/
    protected void grvDetallePagos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (estadoCierre())
        {
            pnTitulos.Enabled = false;
            btnConsultar.Visible = false;
            pnBorrar.Visible = true;
            pnDetallePagos.Visible = false;
            pnPagos.Visible = false;
            pnExportar.Visible = false;

            DateTime fechaEmisionDoc = DateTime.Today;
            DateTime fechaCaducDoc = DateTime.Today;

            if (txtFechaEmisionDoc.Text.Length > 0)
            {
                fechaEmisionDoc = Convert.ToDateTime(txtFechaEmisionDoc.Text);
            }


            if (txtFechaCaducDoc.Text.Length > 0)
            {
                fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);
            }

            string accion = "CONSULTAR";
            int id_DetEgresos = Convert.ToInt32(grvDetallePagos.SelectedValue);
            //int id_DetEgresos = 0;
            //int id_DetEgresos2 = Convert.ToInt32(grvDetallePagos.SelectedDataKey);
            var cDet = dc.sp_abmEgresosDetalle3(accion, id_DetEgresos, 0, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, fechaEmisionDoc, fechaCaducDoc);


            // if(cDet.Count() > 0)
            //{
            foreach (var registro in cDet)
            {
                txtBCodigo.Text = Convert.ToString(id_DetEgresos);
                txtBRuc.Text = registro.ruc;
                txtBNombres.Text = registro.nombres;
                txtBDocumento.Text = registro.descripcion;
                txtBNumDocumento.Text = registro.numeroDocumento;
                txtBAutorizacion.Text = registro.autorizacion;
                txtBConcepto.Text = registro.concepto;
                txtBValorFactura.Text = Convert.ToString(registro.valorFactura);
                txtBValorRetencion.Text = Convert.ToString(registro.valorRetencion);
                txtBAPagar.Text = Convert.ToString(registro.apagar);

            }
            //}
        }
        else
        {
            lblMensaje.Text = "Caja cerrada no se puede modificar";

        }

    }

    protected void grvDetallePagos_PageIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grvDetallePagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvDetallePagos.PageIndex = e.NewPageIndex;
        // if (tipoConsulta == 1)
        // {
        cargaGrid();
        //}

        //if (tipoConsulta == 2)
        //{
        //  cargaGridTodos();
        //}

    }

    /*AL REALIZAR CAMBIO EN TEXTOS O LISTADOS*/
    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        //consultarRetencionProveedor();
        activarTextos();
        verificarConcepto();
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
                        join mComp in dc.tbl_infoCompRetencion on mInfo.id_infotributaria equals mComp.id_infotributaria
                        join mRet in dc.tbl_impuestosRet on mComp.id_infoCompRetencion equals mRet.id_infoCompRetencion
                        where mInfo.id_infotributaria == id
                        select new
                        {
                            totRetenido = mInfo.totRetenido,
                            aPagar = mInfo.aPagar,
                            claveacceso = mInfo.claveacceso,
                            numRetencion = mInfo.estab.Trim() + mInfo.ptoemi + mInfo.secuencial.Trim(),
                            autorizacion = mRet.autorizacion,
                            fechaDoc = mRet.fechaCaducidadDocSustento,
                            fechaDocCad = mRet.fechaCaducidadDocSustento,
                            ccoAfecta = mInfo.ccoAfecta,
                            sucAfecta = mInfo.sucAfecta,


                        };

            if (cinfo.Count() <= 0)
            {
                txtValorFactura.Text = string.Format("{0:#,##0.##}", lvalor);
                txtValorRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
                txtNumretencion.Text = string.Empty;
                txtDocAutorizacion.Text = string.Empty;
                txtFechaEmisionDoc.Text = string.Empty;
                txtFechaCaducDoc.Text = string.Empty;
                txtAutorizacion.Text = string.Empty;
                txtAutorizacion.Text = string.Empty;

            }
            else
            {
                foreach (var registro in cinfo)
                {
                    retencion = Convert.ToDecimal(registro.totRetenido);
                    factura = Convert.ToDecimal(registro.aPagar);
                    secuencial = Convert.ToDecimal(registro.numRetencion);
                    txtNumretencion.Text = registro.numRetencion;
                    txtDocAutorizacion.Text = registro.autorizacion;
                    txtFechaEmisionDoc.Text = Convert.ToString(registro.fechaDoc);
                    txtFechaCaducDoc.Text = Convert.ToString(registro.fechaDocCad);
                    txtNumAutorizacion.Text = registro.claveacceso;
                    txtAutorizacion.Text = string.Empty;
                    ddlAfectaSucursal.Text = registro.sucAfecta;
                    ddlAfectaCcosto.Text = registro.ccoAfecta;

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
                            descrip = adic.campoAdicional.Trim(),
                            emision = ret.fechaEmisionDocSustento,
                            caducidad = ret.fechaCaducidadDocSustento,
                            subTotal = c.totalFactura * 100 / 112,
                            iva = c.totalFactura - (c.totalFactura * 100 / 112)

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
                    txtFechaEmisionDoc.Text = Convert.ToString(registro.emision);
                    txtFechaCaducDoc.Text = Convert.ToString(registro.caducidad);
                    txtSsubtotal.Text = string.Format("{0:#,##0.##}", registro.subTotal);
                    txtSIva.Text = string.Format("{0:#,##0.##}", registro.iva);
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
                            caducidad = ret.fechaCaducidadDocSustento,
                            subTotal = c.totalFactura * 100 / 112,
                            iva = c.totalFactura - (c.totalFactura * 100 / 112)
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
                    txtBsubtotal.Text = string.Format("{0:#,##0.##}", registro.subTotal);
                    txtBIva.Text = string.Format("{0:#,##0.##}", registro.iva);
                }
            }

            /**26/09/2020****************TRAE LAS BASES IMPONIBLES DE BIES Y SERVICIOS A LOS CASILLEROS RESPECTIVOS */
            /**SUBTOTAL BIENES*/
            decimal lsubtotalBien = 0;
            decimal lceroBien = 0;
            decimal lsubtotalServicio = 0;
            decimal lceroServicio = 0;

            var BSubTotal = from c in dc.tbl_infotributaria
                            from d in dc.tbl_infoCompRetencion
                            from ret in dc.tbl_impuestosRet
                            where c.id_infotributaria == d.id_infotributaria
                               && d.id_infoCompRetencion == ret.id_infoCompRetencion
                               && c.id_infotributaria == id
                               && ret.SB == "B12"
                               && ret.codigo == 1
                            select new
                            {
                                subTotal = ret.baseImponible
                            };
            if (BSubTotal.Count() <= 0)
            {
                lsubtotalBien = 0;
            }
            else
            {
                foreach (var registro in BSubTotal)
                {
                    lsubtotalBien = registro.subTotal;
                }
            }
            /**IVA CERO BIENES*/
            var BceroBien = from c in dc.tbl_infotributaria
                            from d in dc.tbl_infoCompRetencion
                            from ret in dc.tbl_impuestosRet
                            where c.id_infotributaria == d.id_infotributaria
                               && d.id_infoCompRetencion == ret.id_infoCompRetencion
                               && c.id_infotributaria == id
                              && ret.SB == "B0"
                              && ret.codigo == 1
                            select new
                            {
                                subTotal = ret.baseImponible
                            };
            if (BceroBien.Count() <= 0)
            {
                lceroBien = 0;
            }
            else
            {
                foreach (var registro in BceroBien)
                {
                    lceroBien = registro.subTotal;
                }
            }
            /**OTROS BIENES*/
            /*NO EXISTE*/
            /**SUBTOTAL SERVICIOS*/
            var SsubtotalServicio = from c in dc.tbl_infotributaria
                                    from d in dc.tbl_infoCompRetencion
                                    from ret in dc.tbl_impuestosRet
                                    where c.id_infotributaria == d.id_infotributaria
                                       && d.id_infoCompRetencion == ret.id_infoCompRetencion
                                       && c.id_infotributaria == id
                                       && ret.SB == "S12"
                                       && ret.codigo == 1
                                    select new
                                    {
                                        subTotal = ret.baseImponible
                                    };
            if (SsubtotalServicio.Count() <= 0)
            {
                lsubtotalServicio = 0;
            }
            else
            {
                foreach (var registro in SsubtotalServicio)
                {
                    lsubtotalServicio = registro.subTotal;
                }
            }
            /**IVA CERO SERVICIOS*/

            var SceroServicio = from c in dc.tbl_infotributaria
                                from d in dc.tbl_infoCompRetencion
                                from ret in dc.tbl_impuestosRet
                                where c.id_infotributaria == d.id_infotributaria
                                   && d.id_infoCompRetencion == ret.id_infoCompRetencion
                                   && c.id_infotributaria == id
                                  && ret.SB == "S0"
                                  && ret.codigo == 1
                                select new
                                {
                                    subTotal = ret.baseImponible
                                };
            if (SceroServicio.Count() <= 0)
            {
                lceroServicio = 0;
            }
            else
            {
                foreach (var registro in SceroServicio)
                {
                    lceroServicio = registro.subTotal;
                }
            }
            /**OTROS SERVICIOS*/
            /*NO EXISTE*/

            /*LLENAR VALORES*/

            /* **********************************************************************************************/

            txtBsubtotal.Text = string.Format("{0:#,##0.##}", lsubtotalBien); //Convert.ToString(lsubtotalBien)
            txtBtarifa0.Text = string.Format("{0:#,##0.##}", lceroBien); //Convert.ToString(lceroBien)

            txtSsubtotal.Text = string.Format("{0:#,##0.##}", lsubtotalServicio); //Convert.ToString(lsubtotalServicio)
            txtStarifa0.Text = string.Format("{0:#,##0.##}", lceroServicio); //Convert.ToString(lceroServicio)

            /******************************************************************************************************/



            txtBsubtotal_TextChanged();
            txtSsubtotal_TextChanged();
            txtDescripcion.Text = txtBien.Text.Trim() + ": " + string.Format("{0:#,##0.##}", bien) + "-" + txtServicio.Text.Trim() + ": " + string.Format("{0:#,##0.##}", servicio);
            //txtBsubtotal.Text = string.Format("{0:#,##0.##}", bien);
            //txtSsubtotal.Text = string.Format("{0:#,##0.##}", servicio);

        }

    }

    protected void txtNumDocumento_TextChanged(object sender, EventArgs e)
    {
        txtNumDocumento.Text = llenarCeros(txtNumDocumento.Text.Trim(), '0', 9);
        txtDocumento.Text = txtSerie.Text.Trim() + txtNumDocumento.Text;
    }

    protected void ddlColaborador_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRuc.Text = ddlColaborador.SelectedValue.Trim();
        txtRuc_TextChanged();
    }
    #endregion

    #region GRABAR REGISTRO DE EGRESO
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

    protected void registrarCabeceraEgresos()
    {
        int valorId;
        valorId = confirmarID();
        if (valorId == 0)
        {
            string lAccion, lnumero, lsucursal, ldescripcion, lusuario, lestado;
            int lid_CabEgresos;
            DateTime lfecha = DateTime.Today;
            string caja = ddlCaja.SelectedValue;
            string dia, mes, ano;

            txtSucursal.Text = ddlSucursal2.SelectedValue;
            txtFecha.Text = Convert.ToString(txtFecha.Text);
            txtNumero.Text = txtSucursal.Text.Trim() + txtFecha.Text.Trim();





            lAccion = "AGREGAR";
            lid_CabEgresos = 0;
            lnumero = txtNumero.Text;

            lsucursal = txtSucursal.Text;
            lfecha = Convert.ToDateTime(txtFecha.Text);
            ldescripcion = "";
            lestado = "0";
            lusuario = Convert.ToString(Session["SUsername"]);

            if (caja != "S")
            {
                dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
                mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
                ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);
                lnumero = caja + lsucursal + dia + mes + ano;
            }


            dc.sp_abmEgresosCabecera2(lAccion, lid_CabEgresos, lnumero, lsucursal, lfecha, ldescripcion, lestado, lusuario, caja, 0);
        }

    }
    /// GRABAR OTROS CAMPOS CUANDO ES RETENCIÓN MANUAL
    /// 
    /// </summary>
    /// 


    protected void registrarDetalleEgresos()
    {
        /*int ltipoDoc, lid_CabEgresos, lid_DetEgresos, lid_documento, lid_Concepto, ltipoPago;
        string lAccion, lruc, ldocumento,lnumeroDocumento, lautorizacion, ldescripcion, lnumAutorizacionRetencion;
        string lsucAfecta, lccoAfecta, lsucafecta, lccoafecta, lnombres, lobservacion;
        decimal lvalorFactura, lvalorRetencion, lapagar;
        */
        /*
         CUANDO ES TIPO DE PAGO RETENCIONES MANUALES REGISTRA VALORES ADICIONALES
         
         */
        int ltipoEgreso = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
        
        decimal ivaBien = Convert.ToDecimal(txtBivaM.Text);
        decimal ivaServicio = Convert.ToDecimal(txtSivaM.Text);
        decimal tarifaCeroBien = Convert.ToDecimal(txtBtarifa0M.Text);
        decimal tarifaCeroServicio = Convert.ToDecimal(txtStarifa0M.Text);
        decimal otrosBien = Convert.ToDecimal(txtBotrosM.Text);
        decimal otrosServicio = Convert.ToDecimal(txtSotrosM.Text);
        DateTime fechaEmisionDocumento = Convert.ToDateTime(txtFechaEmisionDoc.Text);
        

        string rmae_gas = ddlB0GastosM.SelectedValue;
        string rvar_ven = ddlB0CodCbleM.SelectedValue;

        string smae_gas = ddlSiGastosM.SelectedValue;
        string svar_ven = ddlSiCodCbleM.SelectedValue;

        string mae_gas = ddlBiGastosM.SelectedValue;
        string var_ven = ddlBiCodCbleM.SelectedValue;

        decimal subTotalBien = Convert.ToDecimal(txtSubtotalBienes.Text);
        decimal subTotalServicio = Convert.ToDecimal(txtSubtotalServicios.Text);

        decimal totalIva = Convert.ToDecimal(txtSubtotalIva.Text);
        decimal totalPagado = Convert.ToDecimal(txtaPagar.Text);

        decimal totalBien = Convert.ToDecimal(txtSubtotalBienes.Text) + Convert.ToDecimal(txtBivaM.Text);
        decimal totalServicio = Convert.ToDecimal(txtSubtotalServicios.Text) + Convert.ToDecimal(txtSivaM.Text);


        /************************************
         * FIN
        *************************************/

        int id_Concepto = 0;
        string lAccion = "AGREGAR";
        int lid_DetEgresos = 0;
        int lid_CabEgresos = confirmarID();
        int lid_documento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);

        string lruc = txtRuc.Text.Trim();
        string lnombres = txtNombres.Text.Trim();
        string ldocumento = txtDocumento.Text.Trim();
        string ldocAutorizacion = txtDocAutorizacion.Text.Trim();
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
            if (ltipoEgreso != 29)
            {
                dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                          , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                          , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, ldocAutorizacion, lsecuencial, lbiva, lsiva
                                          , lbtarifa0, lstarifa0, lbotros, lsotros, lbtotal, lstotal, liva, fechaEmisionDoc, fechaCaducDoc);
            }
           
            
            else
            {
                dc.sp_abmEgresosDetalle4(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto,lruc,lnombres,lid_documento,ldocumento,lnumAutorizacionRetencion,lautorizacion
                                            ,"",lvalorFactura,lvalorRetencion,lapagar,"",var_ven,mae_gas,"",ltipoPago,lsucAfecta,lccoAfecta,ldescripcion
                                            ,lbien,lservicio,subTotalBien,subTotalServicio,svar_ven,smae_gas,ldocAutorizacion,lsecuencial,ivaBien,ivaServicio
                                            ,tarifaCeroBien,tarifaCeroServicio,otrosBien,otrosServicio,totalBien,totalServicio,totalIva,fechaEmisionDocumento,DateTime.Now,rmae_gas,rvar_ven,""
                                            ,0,0,0,0,0);
            }

            /*GRABA TOTALES EN CABECERA*/
            dc.sp_abmEgresosTotales("TOTALIZA", lid_CabEgresos);

            lblMensaje.Text = "Se ha grabado con éxito";
        }
        else
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Este registro (DOCUMENTO YA REGISTRADO) ya fue ingresado";
        }

    }

    protected int confirmarID()
    {
        int retonoId;
        string lnumero = ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();
        string caja = ddlCaja.SelectedValue;
        string dia, mes, ano;
        DateTime lfecha = Convert.ToDateTime(txtFecha.Text);


        if (caja != "S")
        {
            dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
            mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
            ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);
            lnumero = caja + ddlSucursal2.SelectedValue.Trim() + dia + mes + ano;
        }



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
    #endregion

    #region EXPORTAR

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
            //grvDetallePagos.AllowPaging = true;
            ///this.retornaTodos();

            grvDetallePagos.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetallePagos.HeaderRow.Cells)
            {
                cell.BackColor = grvDetallePagos.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetallePagos.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetallePagos.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetallePagos.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetallePagos.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void todos()
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
            grvDetallePagos.AllowPaging = false;

            this.BindGrid();

            grvDetallePagos.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetallePagos.HeaderRow.Cells)
            {
                cell.BackColor = grvDetallePagos.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetallePagos.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetallePagos.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetallePagos.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetallePagos.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }

    private void BindGrid()
    {
        string laccion, lnumero, lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        lnumero = ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();

        var concultaDetEgresos = dc.sp_ConsultaEgresosDetallexNumero2(laccion, lnumero);

        grvDetallePagos.DataSource = concultaDetEgresos;
        grvDetallePagos.DataBind();

        pnDetallePagos.Visible = true;
    }

    #endregion

    #region PAGINACION

    public class classGasto
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

    private List<classGasto> retornaLista()
    {
        string laccion, lnumero, lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        lnumero = ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();



        var classGasto = new classGasto();

        List<classGasto> lista = new List<classGasto>();

        var concultaDetEgresos = dc.sp_ConsultaEgresosDetallexNumero2(laccion, lnumero);

        foreach (var registro in concultaDetEgresos)
        {
            //classCta.id_mae_cue = Convert.ToString(registro.id_mae_cue);

            classGasto.id_DetEgresos = Convert.ToString(registro.id_DetEgresos);
            classGasto.descripcion = registro.descripcion;
            classGasto.numeroDocumento = registro.numeroDocumento;
            classGasto.ruc = registro.ruc;
            classGasto.autorizacion = registro.autorizacion;
            classGasto.valorFactura = Convert.ToString(registro.valorFactura);
            classGasto.apagar = Convert.ToString(registro.apagar);
            classGasto.concepto = registro.concepto;

            lista.Add(new classGasto()
            {
                //id_mae_cue = classCta.id_mae_cue,
                id_DetEgresos = classGasto.id_DetEgresos,
                descripcion = classGasto.descripcion,
                numeroDocumento = classGasto.numeroDocumento,
                ruc = classGasto.ruc,
                autorizacion = classGasto.autorizacion,
                valorFactura = classGasto.valorFactura,
                valorRetencion = classGasto.valorRetencion,
                apagar = classGasto.apagar,
                concepto = classGasto.concepto
            });

        }

        return lista;
    }

    private void cargaGrid()
    {
        grvDetallePagos.DataSource = retornaLista();
        grvDetallePagos.DataBind();
    }
    #endregion

    #region CREAR PROVEEDOR
    protected void btnIngresaProv_Click(object sender, EventArgs e)
    {
        btnIngresaProv_Click();
    }

    protected void btnIngresaProv_Click()
    {
        pnIngresarProveedor.Visible = true;

        pnTitulos.Visible = false;
        pnPagos.Visible = false;


    }

    protected void blanquearSucursal()
    {
        txtRuc.Text = string.Empty;
        txtrazonsocial.Text = string.Empty;
        txtnombreComercial.Text = string.Empty;
        txtdirMatriz.Text = string.Empty;
        txtcontribuyenteEspecial.Text = string.Empty;
        ddlObligado.SelectedValue = string.Empty;
        txtEmail.Text = string.Empty;
        txtTel.Text = string.Empty;
        lblAviso.Text = string.Empty;
    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string lruc = txtProveedor.Text.Trim();

        var cMatriz = from tMatriz in dc.tbl_matriz
                      where tMatriz.ruc == lruc
                      select new
                      {
                          ruc = tMatriz.ruc,
                          razonsocial = tMatriz.razonsocial,
                          nombreComercial = tMatriz.nombreComercial,
                          dirMatriz = tMatriz.dirMatriz,
                          contribuyenteEspecial = tMatriz.contribuyenteEspecial,
                          obligadoContabilidad = tMatriz.obligadoContabilidad,
                          e_mail = tMatriz.e_mail,
                          telefono = tMatriz.telefono,
                      };

        foreach (var registro in cMatriz)
        {
            txtRuc.Text = registro.ruc;
            txtrazonsocial.Text = registro.razonsocial;
            txtnombreComercial.Text = registro.nombreComercial;
            txtdirMatriz.Text = registro.dirMatriz;
            txtcontribuyenteEspecial.Text = registro.contribuyenteEspecial;
            ddlObligado.SelectedValue = registro.obligadoContabilidad;
            txtEmail.Text = registro.e_mail;
            txtTel.Text = registro.telefono;
        }
    }
    #endregion


    protected void txtBsubtotal_TextChanged(object sender, EventArgs e)
    {
        txtBsubtotal_TextChanged();
    }

    protected void txtBsubtotal_TextChanged()
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

        txtSsubtotal_TextChanged();
    }

    protected void txtSsubtotal_TextChanged()
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
    /*************************************************************************/

    /*RETENCIONES MANUALES*/

    /***************************************************************************/


    protected void blanquearObjetos()
    {
        //dlls
        ddlBiGastosM.SelectedValue = "-1";
        ddlBiCodCbleM.SelectedValue = "-1";
        ddlB0GastosM.SelectedValue = "-1";
        ddlB0CodCbleM.SelectedValue = "-1";
        ddlSiGastosM.SelectedValue = "-1";
        ddlSiCodCbleM.SelectedValue = "-1";
        ddlS0GastosM.SelectedValue = "-1";
        ddlS0CodCbleM.SelectedValue = "-1";
        //fechas
        DateTime esteDia = DateTime.Today;

        txtFecha.Text = esteDia.ToString("d");
        //txtFechCaduc.Text = esteDia.ToString("d");
        //txtFechDoc.Text = esteDia.ToString("d");

        //txts

        txtNumretencion.Text = string.Empty;
        //txtRuc.Text = string.Empty;
        //txtrso.Text = string.Empty;
        //txtemail.Text = string.Empty;
        //txtRuc.Text = string.Empty;
        txtSerie.Text = string.Empty;
        //txtNumDoc.Text = string.Empty;
        txtAutorizacion.Text = string.Empty;
        txtBien.Text = string.Empty;

        formatoTexto();

    }

    /*FORMATO A LOS TEXTOS*/
    protected void formatoTexto()
    {
        double lvalor = 0;
        lblMensaje.Text = string.Empty;

        txtBsubtotalM.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBtarifa0M.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBotrosM.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBivaM.Text = string.Format("{0:#,##0.##}", lvalor);

        txtSsubtotalM.Text = string.Format("{0:#,##0.##}", lvalor);
        txtStarifa0M.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSotrosM.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSivaM.Text = string.Format("{0:#,##0.##}", lvalor);

        txtSubtotalBienes.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSubtotalServicios.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSubtotalGeneral.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSubtotalIva.Text = string.Format("{0:#,##0.###}", lvalor);
        txtPorcIce.Text = string.Format("{0:#,##0.##}", lvalor);


        txtTotalFuente.Text = string.Format("{0:#,##0.##}", lvalor);
        txtTotalIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtTotalRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
        txtTotalDocumento.Text = string.Format("{0:#,##0.##}", lvalor);
        txtApagarM.Text = string.Format("{0:#,##0.##}", lvalor);
    }
    protected void llenarListadosM()
    {

        

        /* TRAER CONCEPTOS*/
        /*  var cCon2 = from mCon in dc.tbl_mae_gas
                     orderby mCon.mae_gas
                     select new
                     {
                         mae_gas = mCon.mae_gas.Trim()
                      ,
                         nombre = mCon.mae_gas.Trim() + " " + mCon.nombre.Trim()
                     };

          var cCon = from mCon in dc.tbl_mae_gas
                     orderby mCon.mae_gas
                     select new
                     {
                         mae_gas = mCon.mae_gas.Trim()
                      ,
                         nombre = mCon.mae_gas.Trim() + " " + mCon.nombre.Trim()
                     };
       

          ddlBiGastosM.DataSource = cCon2;
          ddlBiGastosM.DataBind();

          ddlB0GastosM.DataSource = cCon2;
          ddlB0GastosM.DataBind();

          ddlSiGastosM.DataSource = cCon2;
          ddlSiGastosM.DataBind();

          ddlS0GastosM.DataSource = cCon2;
          ddlS0GastosM.DataBind();

          ListItem listCon = new ListItem("Seleccione Concepto", "-1");

          ddlBiGastosM.Items.Insert(0, listCon);
          ddlB0GastosM.Items.Insert(0, listCon);
          ddlSiGastosM.Items.Insert(0, listCon);
          ddlS0GastosM.Items.Insert(0, listCon); */


        /* TRAER CODIGO CONTABLE*/
       /* var cCble = from mCble in dc.tbl_var_gen
                    orderby mCble.var_gen
                    select new
                    {
                        var_gen = mCble.var_gen.Trim()
                     ,
                        nom_ic = mCble.var_gen.Trim() + " " + mCble.nom_ic.Trim()
                    };

        ddlBiCodCble.DataSource = cCble;
        ddlBiCodCble.DataBind();

        ddlB0CodCbleM.DataSource = cCble;
        ddlB0CodCbleM.DataBind();

        ddlSiCodCbleM.DataSource = cCble;
        ddlSiCodCbleM.DataBind();

        ddlS0CodCbleM.DataSource = cCble;
        ddlS0CodCbleM.DataBind();

        ListItem listCble = new ListItem("Seleccione código contable", "-1");

        ddlBiCodCbleM.Items.Insert(0, listCble);
        ddlB0CodCbleM.Items.Insert(0, listCble);
        ddlSiCodCbleM.Items.Insert(0, listCble);
        ddlS0CodCbleM.Items.Insert(0, listCble);*/
    }


    protected void txtBsubtotalM_TextChanged(object sender, EventArgs e)
    {
        txtBsubtotalM_TextChanged();
        txtBtarifa0.Focus();
    }

    protected void txtBsubtotalM_TextChanged()
    {
        double Biva = Convert.ToDouble(txtBsubtotalM.Text);
        int tarifa = devolverTarifa();

        if (tarifa == -1)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Ingrese la tarifa del I.V.A.";

        }
        else
        {
            if (Biva > 0)
            {
                pnBienIva.Visible = true;
                txtBsubtotalM.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBsubtotalM.Text));

            }
            else
            {
                pnBienIva.Visible = false;
            }

        }
        sumatoriaSubtotalesyTotales();

        txtBivaM.Text = Convert.ToString((Biva * tarifa) / 100);
        txtBivaM.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBivaM.Text));
        txtBibase.Text = txtBsubtotalM.Text;// txtSubtotalBienes.Text;
        txtBibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase.Text));
        txtBibase2.Text = txtBivaM.Text;
        txtBibase2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase2.Text));

        //cambioInteractivoPorcentajes();

    }

    protected int devolverTarifa()
    {
        int tarifa = 0;
        int codigo = 2;// Convert.ToInt32(ddlTarifa.SelectedValue);

        switch (codigo)
        {
            case -1:
                tarifa = -1;
                break;
            case 0:
                tarifa = 0;
                break;
            case 1:
                tarifa = 10;
                break;
            case 2:
                tarifa = 12;
                break;
            case 3:
                tarifa = 14;
                break;
            case 6:
                tarifa = 0;
                break;
        }
        return tarifa;
    }

    protected void sumatoriaSubtotalesyTotales()
    {
        sumaSubtotalesBienes();
        sumaSubtotalesServicios();
        sumaSubtotalGeneral();
        sumaIVA();
        totalDocumento();
        sumarRetencionesFuente();
        sumarRetencionesIVA();
        sumarTotalRetencion();
        totalApagar();
    }
    protected void totalApagar()
    {
        double retFuente = Convert.ToDouble(txtTotalFuente.Text);
        double retIva = Convert.ToDouble(txtTotalIva.Text);
        double totRet = Convert.ToDouble(txtTotalRetencion.Text);
        double totDoc = Convert.ToDouble(txtTotalDocumento.Text);
        double suma = totDoc - totRet;

        txtaPagar.Text = string.Format("{0:#,##0.##}", Convert.ToString(suma));
        txtBAPagar.Text = string.Format("{0:#,##0.##}", Convert.ToString(suma));

    }
    protected void sumarTotalRetencion()
    {
        double suma = 0;
        suma = Convert.ToDouble(txtTotalFuente.Text) + Convert.ToDouble(txtTotalIva.Text);
        txtTotalRetencion.Text = Convert.ToString(suma);
        txtTotalRetencion.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtTotalRetencion.Text));
    }
    protected void sumarRetencionesIVA()
    {
        double suma = 0;
        suma = Convert.ToDouble(txtBIvalor2.Text) + Convert.ToDouble(txtSiValor2.Text);
        txtTotalIva.Text = Convert.ToString(suma);
        txtTotalIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtTotalIva.Text));
    }
    protected void sumarRetencionesFuente()
    {
        double suma = 0;
        suma = Convert.ToDouble(txtBIvalor.Text) + Convert.ToDouble(txtB0Valor.Text) + Convert.ToDouble(txtSiValor.Text) + Convert.ToDouble(txtS0Valor.Text);
        txtTotalFuente.Text = Convert.ToString(suma);
        txtTotalFuente.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtTotalFuente.Text));

    }
    protected void totalDocumento()
    {
        double subtB = Convert.ToDouble(txtBsubtotalM.Text);
        double subtg = Convert.ToDouble(txtSubtotalGeneral.Text);
        double subtiva = Convert.ToDouble(txtSubtotalIva.Text);
        double valIce = Convert.ToDouble(txtBotrosM.Text);
        double totIce = valIce * Convert.ToDouble(txtPorcIce.Text) / 100;


        if (totIce <= 0)
        {
            txtTotalDocumento.Text = string.Format("{0:#,##0.##}", Convert.ToString(subtg + subtiva));
        }
        else
        {

            txtTotalDocumento.Text = string.Format("{0:#,##0.##}", Convert.ToString(subtB + subtiva + totIce));
        }


    }
    protected void sumaIVA()
    {
        double biva = Convert.ToDouble(txtBivaM.Text);
        double siva = Convert.ToDouble(txtSivaM.Text);
        double suma = biva + siva;
        txtSubtotalIva.Text = string.Format("{0:#,##0.###}", Convert.ToString(suma));
    }
    protected void sumaSubtotalGeneral()
    {
        double sb1 = Convert.ToDouble(txtSubtotalBienes.Text);
        double ss1 = Convert.ToDouble(txtSubtotalServicios.Text);
        txtSubtotalGeneral.Text = string.Format("{0:#,##0.##}", Convert.ToString(sb1 + ss1));
    }

    protected void sumaSubtotalesBienes()
    {
        double b1 = Convert.ToDouble(txtBsubtotalM.Text);
        double b2 = Convert.ToDouble(txtBtarifa0M.Text);
        double b3 = Convert.ToDouble(txtBotrosM.Text);

        txtSubtotalBienes.Text = string.Format("{0:#,##0.##}", Convert.ToString(b1 + b2 + b3));
    }

    protected void sumaSubtotalesServicios()
    {
        double s1 = Convert.ToDouble(txtSsubtotalM.Text);
        double s2 = Convert.ToDouble(txtStarifa0M.Text);
        double s3 = Convert.ToDouble(txtSotrosM.Text);

        txtSubtotalServicios.Text = string.Format("{0:#,##0.##}", Convert.ToString(s1 + s2 + s3));
    }

    protected void blancoxCero()
    {
        /*bienes*/
        txtBsubtotal.Text = "0";
        txtBtarifa0.Text = "0";
        txtBotros.Text = "0";
        txtBivaM.Text = "0";

        /*servicios*/
        txtSsubtotal.Text = "0";
        txtStarifa0.Text = "0";
        txtSotros.Text = "0";
        txtSivaM.Text = "0";


        /*subtotales*/
        txtSubtotalBienes.Text = "0";
        txtSubtotalServicios.Text = "0";
        txtSubtotalGeneral.Text = "0";
        txtSubtotalIva.Text = "0";

        /*totales*/
        txtTotalFuente.Text = "0";
        txtTotalIva.Text = "0";
        txtTotalRetencion.Text = "0";
        txtTotalDocumento.Text = "0";
        txtaPagar.Text = "0";
        txtBAPagar.Text = "0";

        /*porcentajes Bienes iva*/
        txtBibase.Text = "0";
        txtBibase2.Text = "0";

        txtBiporc.Text = "0";
        txtBiporc2.Text = "0";

        txtBIvalor.Text = "0";
        txtBIvalor2.Text = "0";

        txtBiCodigo.Text = "";
        txtBiCodigo2.Text = "";

        /*porcentajes Bienes 0*/
        txtB0base.Text = "0";
        //txtB0base2.Text = "0";

        txtB0porc.Text = "0";
        //txtB0porc2.Text = "0";

        txtB0Valor.Text = "0";
        //txtB0Valor2.Text = "0";

        txtB0Codigo.Text = "";
        //txtB0Codigo2.Text = "";

        /*porcentajes servicios iva*/
        txtSibase.Text = "0";
        txtSibase2.Text = "0";

        txtSiporc.Text = "0";
        txtSiporc2.Text = "0";

        txtSiValor.Text = "0";
        txtSiValor2.Text = "0";

        txtSiCodigo.Text = "";
        txtSiCodigo2.Text = "";
        /*porcentajes servicion 0*/
        txtS0base.Text = "0";
        //txtS0base2.Text = "0";

        txtS0porc.Text = "0";
        //txtS0porc2.Text = "0";

        txtS0Valor.Text = "0";
        //txtS0Valor2.Text = "0";

        txtS0Codigo.Text = "";
        //txtS0Codigo2.Text = "";
    }
    protected void ddlBiCodCbleM_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBiCodCbleM_SelectedIndexChanged();
    }
    protected void ddlBiCodCbleM_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlBiGastosM.SelectedValue.Trim();
        string lcodcble = ddlBiCodCbleM.SelectedValue.Trim();

        string valorFuente, valorIva;
        double biFuente, biIva, retencionFuente, retencionIva;
        valorFuente = "0";
        valorIva = "0";
        biFuente = 0;
        biIva = 0;

        var lcodigos = parametrizarValoresB(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtBiCodigo.Text = lFuente;
        txtBiCodigo2.Text = lIva;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        biFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        biIva = lretenciones.Item4;

        txtBiporc.Text = valorFuente;
        txtBiporc2.Text = valorIva;

        retencionFuente = (Convert.ToDouble(txtBibase.Text) * biFuente) / 100;
        txtBIvalor.Text = Convert.ToString(retencionFuente);
        txtBIvalor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBIvalor.Text));

        retencionIva = (Convert.ToDouble(txtBibase2.Text) * biIva) / 100;
        txtBIvalor2.Text = Convert.ToString(retencionIva);
        txtBIvalor2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBIvalor2.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    #region envia parametros de obtencion de  porcentajes de retencion para bienes
    /* envia parametros de obtencion de  porcentajes de retencion para bienes*/
    protected Tuple<string, string> parametrizarValoresB(string lvalor, string lcodcble)
    {
        string codFte = "0";
        string codIva = "0";
        string tipoDoc = "01";


        int lesCE; // conribuyente especial

        //lesCE = 1;
       // if (txtRuc.Text.Length > 0)
        //{
         //   lesCE = 1; //Convert.ToInt16(ddlContEsp.SelectedValue);
        //}
        //else
        //{
            lesCE = 0;
        //}

        switch (lcodcble)
        {
            case "63":
                if (lesCE == 1)
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;



            case "68":
                if (lesCE == 1)
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case "80":
                if (lesCE == 1)
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case "87":
                if (lesCE == 1)
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "303";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case "81":
                if (lesCE == 1)
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case "82":

                if (lesCE == 1)
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case "83":
                if (lesCE == 1)
                {
                    codFte = "311";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "311";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;

            case "90":
                if (lesCE == 1)
                {
                    codFte = "0";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    codFte = "0";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case "91":
                if (lesCE == 1)
                {
                    //codFte = "307";
                    codFte = "3440";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";
                }
                else
                {
                    //codFte = "307";
                    codFte = "3440";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;
            case "93":
                if (lesCE == 1)
                {
                    //codFte = "326";
                    codFte = "346";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                else
                {
                    //codFte = "326";
                    codFte = "346";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                break;

            case "11":
                if (lesCE == 1)
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";

                }
                break;
            case "10":
                if (lesCE == 1)
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";

                }
                break;
            case ".1":
                if (lesCE == 1)
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";

                }
                break;
            case ".5":
                if (lesCE == 1)
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";

                }
                break;
            case ".6":
                if (lesCE == 1)
                {
                    codFte = "308";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "308";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";

                }
                break;
            case ".A":
                if (lesCE == 1)
                {
                    codFte = "307";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "307";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";

                }
                break;
            case "12":
                if (lesCE == 1)
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";

                }
                break;
            case "13":
                if (lesCE == 1)
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";

                }
                break;
            case ":08":
                if (lesCE == 1)
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "9";

                }
                else
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";

                }
                break;
            default:
                codFte = "0";
                if (tipoDoc == "03")
                    codIva = "3";
                else
                    codIva = "0";

                break;
        }
        return Tuple.Create(codFte, codIva);
    }
    #endregion
    #region  CODIGOS FUENTE IVA
    protected Tuple<string, double, string, double> llenarValoresFuenteIva(int tipoRetencion, string sFuente, string sIva)
    {
        string valorFuente, valorIva;
        double biFuente, biIva;
        valorFuente = "0";
        valorIva = "0";
        biFuente = 0;
        biIva = 0;

        var consultaF = from TFuente in dc.tbl_ret_fte
                        where TFuente.ret_fte == sFuente
                        select TFuente;

        if (consultaF.Count() == 0)
        {

            valorFuente = "0";
            biFuente = 0;

        }
        else
        {
            foreach (var registro in consultaF)
            {
                if (registro.porce != null)
                {
                    valorFuente = Convert.ToString(registro.porce);
                    biFuente = Convert.ToDouble(registro.porce);
                }
                else
                {
                    valorFuente = "0";
                    biFuente = 0;
                }

            }
        }

        var consultaI = from TIva in dc.tbl_ret_iva
                        where TIva.ret_iva == sIva
                        select TIva;

        if (consultaI.Count() == 0)
        {

            valorIva = "0";
            biIva = 0;

        }
        else
        {
            foreach (var registro in consultaI)
            {
                if (registro.porce != null)
                {
                    valorIva = Convert.ToString(registro.porce);
                    biIva = Convert.ToDouble(registro.porce);
                }
                else
                {
                    valorIva = "0";
                    biIva = 0;
                }

            }
        }

        return Tuple.Create(valorFuente, biFuente, valorIva, biIva);

    }
    #endregion
    protected void ddlB0GastosM_SelectedIndexChanged(object sender, EventArgs e) {
        ddlB0GastosM_SelectedIndexChanged();
    }
    protected void ddlB0GastosM_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlB0GastosM.SelectedValue.Trim();
        string lcodcble = ddlB0CodCbleM.SelectedValue.Trim();

        string valorFuente, valorIva;
        double biFuente, biIva, retencionFuente;
        valorFuente = "0";
        valorIva = "0";
        biFuente = 0;
        biIva = 0;

        var lcodigos = parametrizarValoresB(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtB0Codigo.Text = lFuente;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        biFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        biIva = lretenciones.Item4;

        txtB0porc.Text = valorFuente;

        retencionFuente = (Convert.ToDouble(txtB0base.Text) * biFuente) / 100;
        txtB0Valor.Text = Convert.ToString(retencionFuente);
        txtB0Valor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtB0Valor.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }
    protected void ddlB0CodCbleM_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lFuente, lIva;

        string lvalor = ddlB0GastosM.SelectedValue.Trim();
        string lcodcble = ddlB0CodCbleM.SelectedValue.Trim();

        string valorFuente, valorIva;
        double biFuente, biIva, retencionFuente;
        valorFuente = "0";
        valorIva = "0";
        biFuente = 0;
        biIva = 0;

        var lcodigos = parametrizarValoresB(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtB0Codigo.Text = lFuente;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        biFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        biIva = lretenciones.Item4;

        txtB0porc.Text = valorFuente;

        retencionFuente = (Convert.ToDouble(txtB0base.Text) * biFuente) / 100;
        txtB0Valor.Text = Convert.ToString(retencionFuente);
        txtB0Valor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtB0Valor.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }
    protected void ddlSiGastosM_SelectedIndexChanged(object sender, EventArgs e)
    { ddlSiGastosM_SelectedIndexChanged(); }
    protected void ddlSiGastosM_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlSiGastos.SelectedValue.Trim();
        string lcodcble = ddlSiCodCble.SelectedValue.Trim();

        string valorFuente, valorIva;
        double SiFuente, SiIva, retencionFuente, retencionIva;
        valorFuente = "0";
        valorIva = "0";
        SiFuente = 0;
        SiIva = 0;

        var lcodigos = parametrizarValoresS(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtSiCodigo.Text = lFuente;
        txtSiCodigo2.Text = lIva;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        SiFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        SiIva = lretenciones.Item4;

        txtSiporc.Text = valorFuente;
        txtSiporc2.Text = valorIva;

        retencionFuente = (Convert.ToDouble(txtSibase.Text) * SiFuente) / 100;
        txtSiValor.Text = Convert.ToString(retencionFuente);
        txtSiValor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSiValor.Text));

        retencionIva = (Convert.ToDouble(txtSibase2.Text) * SiIva) / 100;
        txtSiValor2.Text = Convert.ToString(retencionIva);
        txtSiValor2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSiValor2.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }
    #region envia parametros de obtencion de  porcentajes de retencion para servicios
    /*envia parametros de obtencion de  porcentajes de retencion para servicios*/
    protected Tuple<string, string> parametrizarValoresS(string lvalor, string lcodcble)
    {
        string lnunContribuyente;
        string codFte = "0";
        string codIva = "0";
        string tipoDoc = "01";



        int lesCE; // conribuyente especial

        lnunContribuyente = txtRuc.Text.Trim();// txtcontribuyenteEspecial.Text.Trim();
       // if (lnunContribuyente.Length <= 0)
        //{
            lesCE = 0; 
        //}
        //else
        //{
          //  lesCE = 1; //Convert.ToInt16(lnunContribuyente); //Convert.ToInt16(ddlContEsp.SelectedValue);
        //}


        switch (lcodcble)
        {
            case "60":
                if (lesCE == 1)
                {
                    codFte = "321";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "321";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;


            case "61":
                if (lesCE == 1)
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                break;
            case "62":
                if (lesCE == 1)
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;
            case "63":
                if (lesCE == 1)
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;


            case "84":
                if (lesCE == 1)
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;

            case "85":
                if (lesCE == 1)
                {
                    //codFte = "307";
                    codFte = "3440";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";

                }
                else
                {
                    codFte = "307";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;

            case "86":
                if (lesCE == 1)
                {
                    codFte = "3440";
                    //codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";

                }
                else
                {
                    codFte = "3440";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;

            case "93":
                if (lesCE == 1)
                {
                    //codFte = "326";
                    codFte = "346";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                else
                {
                    //codFte = "326";
                    codFte = "346";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                break;

            case "96":
                if (lesCE == 1)
                {
                    codFte = "340";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "340";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;
            case "87":
                if (lesCE == 1)
                {
                    codFte = "303";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "303";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                break;
            case "88":
                if (lesCE == 1)
                {
                    codFte = "316";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "316";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            case "89":
                if (lesCE == 1)
                {
                    codFte = "304";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "304";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                break;
            case "90":
                if (lesCE == 1)
                {
                    codFte = "0";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "0";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;


            case "91":
                if (lesCE == 1)
                {
                    //codFte = "307";
                    codFte = "3440";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    //codFte = "307";
                    codFte = "3440";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;
            case "11":
                if (lesCE == 1)
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "3";
                }
                break;
            case "10":
                if (lesCE == 1)
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "320";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            case ".1":
                if (lesCE == 1)
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "312";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "1";
                }
                break;
            case ".5":
                if (lesCE == 1)
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "309";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            case ".6":
                if (lesCE == 1)
                {
                    codFte = "308";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "308";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            case ".A":
                if (lesCE == 1)
                {
                    codFte = "307";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "307";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            case "12":
                if (lesCE == 1)
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "310";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "0";
                }
                break;
            case "13":
                if (lesCE == 1)
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";

                }
                else
                {
                    codFte = "332";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            case "08":
                if (lesCE == 1)
                {
                    codFte = "322";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "10";
                }
                else
                {
                    codFte = "322";
                    if (tipoDoc == "03")
                        codIva = "3";
                    else
                        codIva = "2";
                }
                break;
            default:
                codFte = "0";
                if (tipoDoc == "03")
                    codIva = "3";
                else
                    codIva = "0";
                break;
        }
        //}
        return Tuple.Create(codFte, codIva);
    }
    #endregion
    protected void ddlSiCodCbleM_SelectedIndexChanged(object sender, EventArgs e)
    { ddlSiCodCbleM_SelectedIndexChanged(); }
    protected void ddlSiCodCbleM_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlSiGastosM.SelectedValue.Trim();
        string lcodcble = ddlSiCodCbleM.SelectedValue.Trim();

        string valorFuente, valorIva;
        double SiFuente, SiIva, retencionFuente, retencionIva;
        valorFuente = "0";
        valorIva = "0";
        SiFuente = 0;
        SiIva = 0;
        //string uno =   txtContribuyente.Text;

        var lcodigos = parametrizarValoresS(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtSiCodigo.Text = lFuente;
        txtSiCodigo2.Text = lIva;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        SiFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        SiIva = lretenciones.Item4;

        txtSiporc.Text = valorFuente;
        txtSiporc2.Text = valorIva;

        retencionFuente = (Convert.ToDouble(txtSibase.Text) * SiFuente) / 100;
        txtSiValor.Text = Convert.ToString(retencionFuente);
        txtSiValor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSiValor.Text));

        retencionIva = (Convert.ToDouble(txtSibase2.Text) * SiIva) / 100;
        txtSiValor2.Text = Convert.ToString(retencionIva);
        txtSiValor2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSiValor2.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }
    protected void ddlS0GastosM_SelectedIndexChanged(object sender, EventArgs e)
    { ddlS0GastosM_SelectedIndexChanged(); }

    protected void ddlS0GastosM_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlB0GastosM.SelectedValue.Trim();
        string lcodcble = ddlS0CodCbleM.SelectedValue.Trim();

        string valorFuente, valorIva;
        double biFuente, biIva, retencionFuente;
        valorFuente = "0";
        valorIva = "0";
        biFuente = 0;
        biIva = 0;

        var lcodigos = parametrizarValoresS(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtS0Codigo.Text = lFuente;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        biFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        biIva = lretenciones.Item4;

        txtS0porc.Text = valorFuente;

        retencionFuente = (Convert.ToDouble(txtS0base.Text) * biFuente) / 100;
        txtS0Valor.Text = Convert.ToString(retencionFuente);
        txtS0Valor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtS0Valor.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }
    protected void ddlS0CodCbleM_SelectedIndexChanged(object sender, EventArgs e)
    { ddlS0CodCbleM_SelectedIndexChanged(); }
    protected void ddlS0CodCbleM_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlS0GastosM.SelectedValue.Trim();
        string lcodcble = ddlS0CodCbleM.SelectedValue.Trim();

        string valorFuente, valorIva;
        double biFuente, biIva, retencionFuente;
        valorFuente = "0";
        valorIva = "0";
        biFuente = 0;
        biIva = 0;

        var lcodigos = parametrizarValoresS(lvalor, lcodcble);
        lFuente = lcodigos.Item1;
        lIva = lcodigos.Item2;

        txtS0Codigo.Text = lFuente;

        var lretenciones = llenarValoresFuenteIva(1, lFuente, lIva);
        valorFuente = lretenciones.Item1;
        biFuente = lretenciones.Item2;
        valorIva = lretenciones.Item3;
        biIva = lretenciones.Item4;

        txtS0porc.Text = valorFuente;

        retencionFuente = (Convert.ToDouble(txtS0base.Text) * biFuente) / 100;
        txtS0Valor.Text = Convert.ToString(retencionFuente);
        txtS0Valor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtS0Valor.Text));

        //cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    protected void txtBtarifa0M_TextChanged(object sender, EventArgs e)
    { txtBtarifa0M_TextChanged(); }
    protected void txtBtarifa0M_TextChanged()
    {
        double B0 = Convert.ToDouble(txtBtarifa0M.Text);

        int tarifa = devolverTarifa();

        if (tarifa == -1)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Ingrese la tarifa del I.V.A.";

        }
        else
        {
            if (B0 > 0)
            {
                pnBienCero.Visible = true;
                txtBtarifa0M.Text = Convert.ToString(B0);
                txtBtarifa0M.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBtarifa0M.Text));
            }
            else
            {
                pnBienCero.Visible = true;
            }

        }


        sumatoriaSubtotalesyTotales();
        txtB0base.Text = Convert.ToString(B0);
        txtB0base.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtB0base.Text));


        txtBibase.Text = txtBsubtotal.Text;//txtSubtotalBienes.Text;
        txtBibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase.Text));

       // cambioInteractivoPorcentajes();
    }

    protected void txtBotrosM_TextChanged(object sender, EventArgs e)
    { txtBotrosM_TextChanged(); }
    protected void txtBotrosM_TextChanged()
    {
        sumatoriaSubtotalesyTotales();
        txtBotrosM.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBotrosM.Text));
        double otros = Convert.ToDouble(txtBotrosM.Text);

        //if (otros <= 0)
        //{
        txtBibase.Text = txtSubtotalBienes.Text;
        txtBibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase.Text));
        //}
        //else 
        //{
        //  txtBibase.Text = txtBotros.Text;
        // txtBibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase.Text));
        //}

        //cambioInteractivoPorcentajes();
    }

    protected void txtSsubtotalM_TextChanged(object sender, EventArgs e)
    { txtSsubtotalM_TextChanged(); }
    protected void txtSsubtotalM_TextChanged()
    {
        double Sbien = Convert.ToDouble(txtSsubtotalM.Text);

        int tarifa = devolverTarifa();

        if (tarifa == -1)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Ingrese la tarifa del I.V.A.";

        }
        else
        {
            if (Sbien > 0)
            {
                pnServicioIva.Visible = true;
                
                txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSsubtotalM.Text));

            }
            else
            {
                pnServicioIva.Visible = false;
            }

        }

        sumatoriaSubtotalesyTotales();

        txtSIva.Text = Convert.ToString((Sbien * tarifa) / 100);
        txtSIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSIva.Text));

        double otros = Convert.ToDouble(txtSotros.Text);
        // if (otros <= 0)
        //{
        txtSibase.Text = txtSsubtotalM.Text;//txtSubtotalServicios.Text;


        txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));
        //}
        //else 
        // {
        //   txtSibase.Text = txtSotros.Text;//txtSubtotalServicios.Text;
        //  txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));
        // }

        txtSibase2.Text = txtSIva.Text;
        txtSibase2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase2.Text));


        //cambioInteractivoPorcentajes();
    }


    protected void txtStarifa0M_TextChanged(object sender, EventArgs e)
    { txtStarifa0M_TextChanged(); }
    protected void txtStarifa0M_TextChanged()
    {
        double S0 = Convert.ToDouble(txtStarifa0M.Text);

        int tarifa = devolverTarifa();

        if (tarifa == -1)
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Ingrese la tarifa del I.V.A.";

        }
        else
        {
            if (S0 > 0)
            {
                pnServicioCero.Visible = true;
                txtStarifa0M.Text = Convert.ToString(S0);
                txtStarifa0M.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtStarifa0M.Text));
            }
            else
            {
                pnServicioCero.Visible = false;
            }

        }

        sumatoriaSubtotalesyTotales();
        txtS0base.Text = Convert.ToString(S0);
        txtS0base.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtS0base.Text));

        //cambioInteractivoPorcentajes();
    }
    protected void txtSotrosM_TextChanged(object sender, EventArgs e)
    {
        sumatoriaSubtotalesyTotales();

        txtSotrosM.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSotrosM.Text));

        double otros = Convert.ToDouble(txtSotrosM.Text);
        // if (otros <= 0)
        //{
        txtSibase.Text = txtSsubtotal.Text;//txtSubtotalServicios.Text;
        txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));
        //}
        //else
        //{
        //  txtSibase.Text = txtSotros.Text;//txtSubtotalServicios.Text;
        // txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));
        //}

        // txtSibase.Text = txtSubtotalServicios.Text;
        //txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));

        //cambioInteractivoPorcentajes();
    }
}