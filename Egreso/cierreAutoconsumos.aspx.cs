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

public partial class Egreso_cierreAutoconsumos : System.Web.UI.Page
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
        pnDatos.Visible = true;
        pnCajas.Visible = false;
        pnCabeceraCaja.Visible = false;
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





    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        btnConsultar_Click();
    }

    protected void btnConsultar_Click()
    {
        string lAccion, lsucursal;
        int lid_CabEgresos;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;

        lAccion = "CONSULTAR";
        lid_CabEgresos = 0;
        lsucursal = ddlSucursal2.SelectedValue; ;
        lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        lFechaFin = Convert.ToDateTime(txtFechaFin.Text);

        var concultaCabEgresos = dc.sp_ListarEgresosCabecera2(lAccion, lid_CabEgresos, lsucursal, lFechaIni, lFechaFin);

        grvEgresosCabecera.DataSource = concultaCabEgresos;
        grvEgresosCabecera.DataBind();

        pnCajas.Visible = true;

    }


    protected void grvEgresosCabecera_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnTitulos.Visible = false;
        pnCajas.Visible = false;
        pnCabeceraCaja.Visible = true;
        pnDetalleCaja.Visible = true;
        desplegarCabeceraEgresos();
        desplegarDetalleEgresos();

    }


    protected void desplegarCabeceraEgresos()
    {
        string lAccion, lnumero;

        lAccion = "LLENA";
        lnumero = Convert.ToString(grvEgresosCabecera.SelectedValue);
        dc.sp_totalesEgresos2(lAccion, lnumero);

        var consultaCeg = from Ce in dc.tbl_CabEgresos
                          where Ce.numero == lnumero
                          select new
                          {
                              id_CabEgresos = Ce.id_CabEgresos
                              ,
                              sucursal = Ce.sucursal
                              ,
                              numero = Ce.numero
                              ,
                              fecha = Ce.fecha
                              ,
                              estado = Ce.estado
                              ,
                              descripcion = Ce.descripcion
                              ,
                              totalPagado = Ce.totalPagado
                              ,
                              totalPagadoOtros = Ce.totalPagadoOtros
                              ,
                              totalEgreso = Ce.totalEgreso
                              ,
                              totalRetencion = Ce.totalRetencion
                              ,
                              totalPermiso = Ce.totalPermisos
                              ,
                              totalMatrVehiculo = Ce.totalMatriculacion
                              ,
                              totalRevVehicular = Ce.totalRevisionVehicular
                              ,
                              totalMultaVehicular = Ce.totalMultasTransito
                              ,
                              totalAnticipoPagoprov = Ce.totalAnticipoPagoProveedor
                              ,
                              totalPrestamoNomina = Ce.totalPrestamoAnticipoNomina
                              ,
                              totalViaticos = Ce.totalViaticos
                              ,
                              totalRepCajaChica = Ce.totalReposiscionCajaChica
                              ,
                              totalSinDocumento = Ce.totalEgresoSinDocContable
                              ,
                              totalAutoconsumo = Ce.totalAutoConsumo
                              ,
                              totalDevolucion = Ce.totalDevolucion
                              ,
                              totalProvision = Ce.totalProvision

                          };

        if (consultaCeg.Count() == 0)
        {
            // no existe , registrar nueva fila de egresos
        }
        else
        {
            foreach (var registro in consultaCeg)
            {
                txtNumero.Text = registro.numero;
                txtFecha.Text = Convert.ToString(registro.fecha);
                txtEstado.Text = registro.estado;
                txtDescripcion.Text = registro.descripcion;

                txtTdocumento.Text = Convert.ToString(registro.totalEgreso);
                txtTretenido.Text = Convert.ToString(registro.totalRetencion);
                txtTefectivo.Text = Convert.ToString(registro.totalPagado);
                txtTnoefectivo.Text = Convert.ToString(registro.totalPagadoOtros);



            }
        }
    }

    protected void desplegarDetalleEgresos()
    {
        int lid_CabEgresos;
        string lAccion;
        
        

        DateTime fechaEmisionDoc = DateTime.Today;
        DateTime fechaCaducDoc = DateTime.Today;


        lAccion = "TODOS";
        lid_CabEgresos = traeIdCabeceraDetalle();
        

        var consultaDet = dc.sp_abmEgresosDetalle3(lAccion, 0, lid_CabEgresos, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, fechaEmisionDoc, fechaCaducDoc);
        grvEgresosDetalle.DataSource = consultaDet;
        grvEgresosDetalle.DataBind();

    }

    protected int traeIdCabeceraDetalle()
    {
        int lid;
        string lnumero;


        lid = 0;

        lnumero = Convert.ToString(grvEgresosCabecera.SelectedValue);


        var consultaId = from Did in dc.tbl_CabEgresos
                         where Did.numero == lnumero
                         select new
                         {
                             id = Did.id_CabEgresos
                         };

        if (consultaId.Count() == 0)
        {
            // no existe id
            lid = 0;
        }
        else
        {
            foreach (var registro in consultaId)
            {
                lid = registro.id;
            }
        }

        return lid;
    }

    protected void btnCerrar_Click(object sender, EventArgs e)
    {
        bool pasa;
        string ldescripcion, lestado, lnumero;
        int lid;

        lnumero = Convert.ToString(grvEgresosCabecera.SelectedValue);
        pasa = true;
        ldescripcion = txtDescripcion.Text.Trim();
        lestado = txtEstado.Text.Trim();
        lid = traeIdCabeceraDetalle();

        if (ldescripcion.Length <= 0)
        {
            lblMensaje.Text = "Debe ingresar en Descripción si hubo o no,novedades";
            pasa = false;
        }
        else
        {
            lblMensaje.Text = "";
        }

        if (lestado == "0" && pasa)
        {
            /*LLENA TOTALES EN CABECERA*/
            /*GRABAR EL ESTADO 1*/

            tbl_CabEgresos tbl_CabEgresos = dc.tbl_CabEgresos.SingleOrDefault(x => x.id_CabEgresos == lid);
            tbl_CabEgresos.estado = "1";
            tbl_CabEgresos.descripcion = ldescripcion;
            dc.SubmitChanges();
            activarObjetos();
            btnConsultar_Click();

        }
        else
        {
            if (lestado == "1")
            {
                lblMensaje.Text = " Ya se realizo el cierre, no puede modificar";
            }
        }

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        activarObjetos();
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        DateTime esteDia = DateTime.Today;
        DateTime lfechaInicio, lfechaFin;

        //string lsuc,lfechaInicio,lfechaFin;

        string lsuc;

        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;
        lsuc = "";

        //dc.sp_repCierraCaja(lfechaInicio,lfechaFin,lsuc);

        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        lsuc = ddlSucursal2.Text.Trim();

        if (comprobarCierre(lfechaInicio, lfechaFin, lsuc))
        {
            Session["pFechaInicio"] = lfechaInicio;
            Session["pFechaFin"] = lfechaFin;
            Session["pSuc"] = lsuc;

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirAutoconsumos.aspx','','width=800,height=500') </script>");
        }
    }


    protected bool comprobarCierre(DateTime lfechaInicio, DateTime lfechaFin, string lsuc)
    {
        bool pasa;
        System.Int32 kont = (from mkont in dc.tbl_CabEgresos
                             where mkont.sucursal == lsuc
                                 && mkont.fecha >= lfechaInicio
                                 && mkont.fecha <= lfechaFin
                                 && mkont.estado == "0"
                             select mkont).Count();


        if (kont > 0 )
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = "NO PUEDE REALIZAR LA IMPRESIÓN, NO HA CERRADO TODAS LAS CAJAS EN ESTE RANGO DE FECHAS";
            pasa = false;
        }
        else
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = string.Empty;
            pasa = true;
        }
        return pasa;
    }
}