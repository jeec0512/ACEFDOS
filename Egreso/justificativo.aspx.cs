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

public partial class Egreso_justificativo : System.Web.UI.Page
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
           
            string accion = string.Empty;
           
            perfilUsuario();
            activarObjetos();

            string sucursal = ddlSucursal2.SelectedValue.Trim();
            var cjustificar = dc.sp_ListarJusticar(accion, sucursal); 

            ddlDocJustificar.DataSource = cjustificar;
            ddlDocJustificar.DataBind();
            
            ListItem liDocumento = new ListItem("Seleccione documento", "-1");
            ddlDocJustificar.Items.Insert(0, liDocumento);

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
        string observacion = Convert.ToString(ddlDocJustificar.SelectedItem);

        string documento = observacion.Substring(1, 16);

        DateTime docFecha = DateTime.Today;
        docFecha = Convert.ToDateTime(txtFecha.Text);
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);



        lnumero = 'J'+ddlSucursal2.SelectedValue.Trim() + dia+mes+ano;
        txtNumero.Text = lnumero;

        // consultar estado de la caja de egresos

        var consultaCe = from Ce in dc.tbl_CabEgresos
                         where Ce.numero == lnumero
                               && Ce.numeroDocumento == documento
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
        string laccion, lnumero, lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        lnumero = 'J'+ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();

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
        txtDocAutorizacion.Visible = false;

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


        ListItem listCon = new ListItem("Seleccione Concepto", "-1");

        ddlBiGastos.Items.Insert(0, listCon);
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

        ddlSiCodCble.DataSource = cCble;
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

        txtAutorizacion.Text = string.Empty;
        txtDescripcion.Text = string.Empty;

        txtBien.Text = string.Empty;
        txtServicio.Text = string.Empty;
        txtNumAutorizacion.Text = string.Empty;
        txtNumretencion.Text = string.Empty;


        txtBsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtValorFactura.Text = string.Format("{0:#,##0.##}", lvalor);
        txtValorRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
        txtaPagar.Text = string.Format("{0:#,##0.##}", lvalor);
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
        decimal Ssubtotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal ValorFactura = Convert.ToDecimal(txtValorFactura.Text);
        decimal ValorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
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


        if (Bsubtotal <= 0 && Ssubtotal <= 0)
        {
            lmensaje = lmensaje + " Ingrese valores en Bienes o Servicios o en ambos si es el caso ";
            pasa = false;
        }

        if (Bsubtotal > 0)
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


        if (Ssubtotal > 0)
        {
            if (SiGastos == "-1" || SiCodCble == "-1")
            {
                lmensaje = lmensaje + " Seleccione concepto y código contable del Servicio ";
                pasa = false;
            }
            if (Bien.Length <= 0)
            {
                lmensaje = lmensaje + " Ingrese descripción del Servicio";
                pasa = false;
            }
        }

        if (new[] { "3", "5" }.Contains(TipoDocumento))
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


        if (aPagar <= 0)
        {
            lmensaje = lmensaje + " Ingrese el valor entregado";
            pasa = false;
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
                txtDocAutorizacion.Visible = false;
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
            lblMensaje.Text = "";
            string ldocumento = ddlDocJustificar.SelectedValue.Trim();
            


            if (estadoCierre() && ldocumento != "-1")
            {

                lblMensaje.Text = "";
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
                lblMensaje.Text = "Caja cerrada no se puede modificar o no ha seleccionado documento";
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
        string accion = "BORRAR";
        int id_DetEgresos = Convert.ToInt16(grvDetallePagos.SelectedValue);
        DateTime fechaEmisionDoc = DateTime.Today;
        DateTime fechaCaducDoc = DateTime.Today;

        var cDet = dc.sp_abmEgresosDetalle3(accion, id_DetEgresos, 0, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "",0,0,0,0,0,0,0,0,0,fechaEmisionDoc,fechaCaducDoc);
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

            string accion = "CONSULTAR";
            int id_DetEgresos = Convert.ToInt16(grvDetallePagos.SelectedValue);
            DateTime fechaEmisionDoc = DateTime.Today;
            DateTime fechaCaducDoc = DateTime.Today;

            var cDet = dc.sp_abmEgresosDetalle3(accion, id_DetEgresos, 0, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "",0,0,0,0,0,0,0,0,0,fechaEmisionDoc,fechaCaducDoc);


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
                txtNumretencion.Text = string.Format("{0:#,##0.##}", lvalor);

            }
            else
            {
                foreach (var registro in cinfo)
                {
                    retencion = Convert.ToDecimal(registro.totRetenido);
                    factura = Convert.ToDecimal(registro.aPagar);
                    secuencial = Convert.ToDecimal(registro.numRetencion);
                }
            }

            txtValorFactura.Text = string.Format("{0:#,##0.##}", factura);
            txtValorRetencion.Text = string.Format("{0:#,##0.##}", retencion);
            txtaPagar.Text = string.Format("{0:#,##0.##}", factura);
            txtNumretencion.Text = string.Format("{0:#,##0.##}", secuencial);

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

    protected void txtNumDocumento_TextChanged(object sender, EventArgs e)
    {
        txtNumDocumento.Text = llenarCeros(txtNumDocumento.Text.Trim(), '0', 9);
        txtDocumento.Text = txtSerie.Text.Trim() + txtNumDocumento.Text;
    }

    protected void txtBsubtotal_TextChanged(object sender, EventArgs e)
    {
        decimal BsubTotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal SsubTotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal valorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
        decimal totalDocumento = (BsubTotal + SsubTotal) - valorRetencion;

        txtValorFactura.Text = Convert.ToString(totalDocumento);
        txtaPagar.Text = Convert.ToString(totalDocumento);
    }

    protected void txtSsubtotal_TextChanged(object sender, EventArgs e)
    {
        decimal BsubTotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal SsubTotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal valorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
        decimal totalDocumento = (BsubTotal + SsubTotal) - valorRetencion;

        txtValorFactura.Text = Convert.ToString(totalDocumento);
        txtaPagar.Text = Convert.ToString(totalDocumento);
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
            string observacion = Convert.ToString(ddlDocJustificar.SelectedItem);
            string documento = observacion.Substring(0, 15);
            int id_detEgreso = Convert.ToInt16(ddlDocJustificar.SelectedValue);
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
            txtNumero.Text = "J"+txtSucursal.Text.Trim() + dia+mes+ano;

            lAccion = "AGREGAR";
            lid_CabEgresos = 0;
            lnumero = txtNumero.Text;
            lsucursal = txtSucursal.Text;
            lfecha = Convert.ToDateTime(txtFecha.Text);
            ldescripcion = "";
            lestado = "0";
            lusuario = Convert.ToString(Session["SUsuarioname"]);


            dc.sp_abmEgresosCabecera2(lAccion, lid_CabEgresos, lnumero, lsucursal, lfecha, ldescripcion, lestado, lusuario,documento,id_detEgreso);
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
        string lbimae_gas = ddlBiGastos.SelectedValue;
        string lbicodcble = ddlBiCodCble.SelectedValue;
        string lbien = txtBien.Text.Trim();
        decimal lbsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        string lsimae_gas = ddlSiGastos.SelectedValue;
        string lsicodcble = ddlSiCodCble.SelectedValue;
        string lservicio = txtServicio.Text.Trim();
        decimal lssubtotal = Convert.ToDecimal(txtSsubtotal.Text);

        decimal lvalorFactura = Convert.ToDecimal(txtValorFactura.Text);
        string lsecuencial = txtNumretencion.Text;
        string lnumAutorizacionRetencion = txtNumAutorizacion.Text.Trim();
        string lNumretencion = txtNumretencion.Text.Trim();
        decimal lvalorRetencion = Convert.ToDecimal(txtValorRetencion.Text);
        decimal lapagar = Convert.ToDecimal(txtaPagar.Text);
        int ltipoPago = Convert.ToInt16(ddlTipoPago.SelectedValue);

        DateTime fechaEmisionDoc = DateTime.Today;
        DateTime fechaCaducDoc = DateTime.Today;

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
            lblMensaje.Text = "Se ha grabado con éxito";
            dc.sp_abmEgresosDetalle3(lAccion, lid_DetEgresos, lid_CabEgresos, id_Concepto, lruc, lnombres, lid_documento, ldocumento, lnumAutorizacionRetencion, lautorizacion
                                      , "", lvalorFactura, lvalorRetencion, lapagar, "", lbicodcble, lbimae_gas, "", ltipoPago, lsucAfecta, lccoAfecta, ldescripcion
                                      , lbien, lservicio, lbsubtotal, lssubtotal, lsicodcble, lsimae_gas, lnumAutorizacionRetencion, lsecuencial, 0, 0, 0, 0, 0, 0, 0, 0, 0,fechaEmisionDoc,fechaCaducDoc);
        }
        else
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Este registro ya fue ingresado";
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

        string lnumero = "J"+ddlSucursal2.SelectedValue.Trim() + dia+mes+ano;



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

        DateTime lfecha = Convert.ToDateTime(txtFecha.Text);
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(lfecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(lfecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(lfecha.Year), '0', 4);


        lnumero = "J"+ddlSucursal2.SelectedValue.Trim() + dia+mes+ano;

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
        lnumero = 'J'+ddlSucursal2.SelectedValue.Trim() + txtFecha.Text.Trim();



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
}