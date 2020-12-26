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

public partial class Egreso_controlCajas : System.Web.UI.Page
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
          
        
            grvEgresosCabecera_SelectedIndexChanged();

    }

    protected void grvEgresosCabecera_SelectedIndexChanged()
    {
       

    }



    protected void desplegarCabeceraEgresos(string lnumero)
    {
        string lAccion;

        lAccion = "LLENA";
        
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

    protected void desplegarDetalleEgresos(string lnumero,int id_Cab_detalle)
    {
        int lid_CabEgresos;
        string lAccion;

        lAccion = "TODOS";
        lid_CabEgresos = traeIdCabeceraDetalle(lnumero);
        DateTime fechaEmisionDoc = DateTime.Today;
        DateTime fechaCaducDoc = DateTime.Today;


        var consultaDet = dc.sp_abmEgresosDetalle3(lAccion, 0, lid_CabEgresos, 0, "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", 0, "", "", "", "", "", 0, 0, "", "", "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0,fechaEmisionDoc,fechaCaducDoc);

            
        grvEgresosDetalle.DataSource = consultaDet;
        grvEgresosDetalle.DataBind();

    }

    protected int traeIdCabeceraDetalle(string lnumero)
    {
        int lid;
        lid = 0;

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
        lid = traeIdCabeceraDetalle(lnumero);

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
        pnTitulos.Visible = true;
        pnDatos.Visible = true;
        pnCajas.Visible = true;
        pnCabeceraCaja.Visible = false;
        pnDetalleCaja.Visible = false;
    }




    protected void grvEgresosCabecera_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modDoc")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosCabecera.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            string ldoc = row.Cells[2].Text;
            int id_Cab_detalle = Convert.ToInt32(row.Cells[1].Text);

            pnTitulos.Visible = false;
            pnCajas.Visible = false;
            pnCabeceraCaja.Visible = true;
            pnDetalleCaja.Visible = true;
            desplegarCabeceraEgresos(ldoc);
            desplegarDetalleEgresos(ldoc, id_Cab_detalle);




        }

        if (e.CommandName == "Rev")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosCabecera.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[1].Text);


            tbl_CabEgresos tbl_CabEgresos = dc.tbl_CabEgresos.SingleOrDefault(x => x.id_CabEgresos == lid);
            tbl_CabEgresos.revisado = true;
           
            dc.SubmitChanges();
            activarObjetos();
            btnConsultar_Click();

            

        }


        
        if (e.CommandName == "Button_responder")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosCabecera.Rows[index];
            int id_pregunta = row.DataItemIndex;
            string ldoc = row.Cells[1].Text;
        }

    }


    protected void grvEgresosDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);
            
        }

        if (e.CommandName == "verRet")
        {
            string lsuc = ddlSucursal2.SelectedValue.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);
            
            string lruc = row.Cells[3].Text;
            string lnumRetencion = row.Cells[8].Text;
            string lautRetencion = row.Cells[9].Text;

            //verRetencion(lsuc,lruc, lnumRetencion, lautRetencion);
        }

        if (e.CommandName == "Jus")
        {
           
            string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);
           



            tbl_DetEgresos tbl_DetEgresos = dc.tbl_DetEgresos.SingleOrDefault(x => x.id_DetEgresos == lid);
            tbl_DetEgresos.justificado = true;

            dc.SubmitChanges();


            desplegarDetalleEgresos(ldoc,0);
            



        }

    }

    protected void verRetencion(string lsuc, string lruc, string lnumRetencion, string lautRetencion)
    {

        /*DATOS DE LA EMPRESA Y SUCURSAL*/
        txtNumRetencion.Text = lnumRetencion;

        var cMatriz = from tMatriz in dc.tbl_matriz
                      from tRuc in dc.tbl_ruc
                      where tMatriz.ruc == tRuc.ruc
                      && tRuc.sucursal == lsuc
                      select new
                      {
                          ruc = tMatriz.ruc
                          ,
                          razonsocial = tMatriz.razonsocial
                          ,
                          dirMatriz = tMatriz.dirMatriz
                          ,
                          dirEstablecimiento = tRuc.dirEstablecimiento
                          ,
                          contribuyenteEspecial = tMatriz.contribuyenteEspecial
                          ,
                          obligadoContabilidad = tMatriz.obligadoContabilidad
                      };

        if (cMatriz.Count() <= 0)
        {

        }
        else
        {
            foreach (var registro in cMatriz)
            {
                txtRuc.Text = registro.ruc;
                txtRazonSocial.Text = registro.razonsocial;
                txtDirMatriz.Text = registro.dirMatriz;
                txtDirSucursal.Text = registro.dirEstablecimiento;
                txtContEspecial.Text = registro.contribuyenteEspecial;
                txtObligado.Text = registro.obligadoContabilidad;
            }

        }

        /*DATOS  DEL PROVEEDOR*/
        var cProveedor = from tProveedor in dc.tbl_matriz
                         from tCom in dc.tbl_infoCompRetencion
                         from tTrib in dc.tbl_infotributaria
                         where tProveedor.ruc == tCom.identificacionSujetoRetenido
                         && tCom.id_infotributaria == tTrib.id_infotributaria
                         && tTrib.estab + tTrib.ptoemi + tTrib.secuencial == lnumRetencion
                         && tProveedor.ruc == lruc
                         && tTrib.cre_sri == "AUTORIZADO"
                         select new
                         {
                             razonsocial = tProveedor.razonsocial
                             ,
                             ruc = tProveedor.ruc
                             ,
                             fechaDocumento = tTrib.fechaDocumento

                         };

        if (cProveedor.Count() <= 0)
        {

        }
        else
        {
            foreach (var registro in cProveedor)
            {
                txtProveedor.Text = registro.razonsocial;

                txtRucproveedor.Text = registro.ruc;
                txtFechaEmision.Text = Convert.ToString(registro.fechaDocumento);
            }

        }

        /*DETALLE DE LAS RETENCIONES*/

        var cRetenciones = from tProveedor in dc.tbl_matriz
                           from tCom in dc.tbl_infoCompRetencion
                           from tTrib in dc.tbl_infotributaria
                           from tRet in dc.tbl_impuestosRet
                           where tProveedor.ruc == tCom.identificacionSujetoRetenido
                           && tCom.id_infotributaria == tTrib.id_infotributaria
                           && tCom.id_infoCompRetencion == tRet.id_infoCompRetencion
                           && tTrib.estab + tTrib.ptoemi + tTrib.secuencial == lnumRetencion
                           && tProveedor.ruc == lruc
                           && tTrib.cre_sri == "AUTORIZADO"
                           select new
                           {
                               codDocSustento = tRet.codDocSustento
                                 ,
                               numDocSustento = tRet.numDocSustento
                                 ,
                               fechaEmision = tCom.fechaEmision
                                 ,
                               periodoFiscal = tCom.periodoFiscal
                                 ,
                               baseImponible = tRet.baseImponible
                                 ,
                               codigo = tRet.codigo
                                 ,
                               porcentajeRetener = tRet.porcentajeRetener
                                 ,
                               valorRetenido = tRet.valorRetenido


                           };

        grvDetalleRetenciones.DataSource = cRetenciones;
        grvDetalleRetenciones.DataBind();


        /*INFORMACION ADICIONAL*/
        var cAdicional = from tProveedor in dc.tbl_matriz
                         from tCom in dc.tbl_infoCompRetencion
                         from tTrib in dc.tbl_infotributaria
                         from tAdi in dc.tbl_infoAdicional
                         where tProveedor.ruc == tCom.identificacionSujetoRetenido
                         && tCom.id_infotributaria == tTrib.id_infotributaria
                         && tCom.id_infoCompRetencion == tAdi.id_infoCompRetencion
                         && tTrib.estab + tTrib.ptoemi + tTrib.secuencial == lnumRetencion
                         && tProveedor.ruc == lruc
                         && tTrib.cre_sri == "AUTORIZADO"
                         select new
                         {
                             dirMatriz = tProveedor.dirMatriz
                             ,
                             telefono = tProveedor.telefono
                             ,
                             e_mail = tProveedor.e_mail
                             ,
                             campoAdicional = tAdi.campoAdicional


                         };

        if (cAdicional.Count() <= 0)
        {

        }
        else
        {
            foreach (var registro in cAdicional)
            {
                txtDireccionproveedor.Text = registro.dirMatriz;
                txtTelefonoProveedor.Text = registro.telefono;
                    txtEmailProveedor.Text = registro.e_mail;
                    txtAdicional.Text = registro.campoAdicional;
            }

        }


    }

   
}