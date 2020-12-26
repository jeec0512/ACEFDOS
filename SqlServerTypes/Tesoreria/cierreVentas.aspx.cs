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


public partial class Tesoreria_cierreVentas : System.Web.UI.Page
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

            var consultaCb = from Cb in dc.tbl_CuentaBancaria
                             orderby Cb.banco
                             select new
                             {
                                 id = Cb.id_cuentasBancaria,
                                 descripcion = Cb.banco.Trim() + '-' + Cb.numeroCuenta.Trim()
                             };
            ddlBanco.DataSource = consultaCb;
            ddlBanco.DataBind();
            ddlBanco.SelectedIndex = -1;
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void activarObjetos()
    {
        pnCabecerarecaudacion.Visible = false;
        pnAltas.Visible = false;
        pnRecaudacion.Visible = false;
    }

    #endregion


    protected void listarDepositos()
    {
        int lidrecauda;
        lidrecauda = Convert.ToInt32(grvRecaudacionCabecera.SelectedValue);

        /*DESPLEGAR DEPOSITOS EN GRID*/
        var consultadP = from Cb in dc.tbl_CuentaBancaria
                         join dP in dc.tbl_DepositoBancario
                         on Cb.id_cuentasBancaria equals dP.id_cuentasBancaria
                         where dP.id_cab_recaudacion == lidrecauda
                         select new
                         {
                             id_DepositoBancario = dP.id_DepositoBancario
                              ,
                             cuenta = Cb.banco.Trim() + '-' + Cb.numeroCuenta.Trim()
                              ,
                             deposito = dP.numeroDeposito
                              ,
                             valor = dP.valor
                              ,
                             descripcion = dP.descripcion
                         };


        
        //grvDepositos.DataSource = dc.sp_ListarDepositos(lidrecauda);
        grvDepositos.DataSource = consultadP;
        grvDepositos.DataBind();

    }

    protected string consultarGastoAutorizado()
    {
        string gtoAturizado, lnumero;
        lnumero = lblTitulo.Text.Trim();
        gtoAturizado = string.Empty;

        var estados = new string[] { "1", "2" };

        var consultaGto = from gto in dc.tbl_CabEgresos
                          where gto.numero == lnumero
                                && estados.Contains(gto.estado)
                          select new
                          {
                              totalPagado = gto.totalPagado
                          };
        if (consultaGto.Count() == 0)
        {
            gtoAturizado = "0,00";
        }
        else
        {
            foreach (var registro in consultaGto)
            {
                gtoAturizado = Convert.ToString(registro.totalPagado);
            };
        }

        return gtoAturizado;
    }

    protected string consultarProvision()
    {
        string lprovision, lnumero;
        lnumero = lblTitulo.Text.Trim();
        lprovision = string.Empty;

        var estados = new string[] { "1", "2" };

        var consultaGto = from gto in dc.tbl_CabEgresos
                          where gto.numero == lnumero
                               && estados.Contains(gto.estado)
                          select new
                          {
                              totalProvision = gto.totalProvision
                          };
        if (consultaGto.Count() == 0)
        {
            lprovision = "0,00";
        }
        else
        {
            foreach (var registro in consultaGto)
            {
                lprovision = Convert.ToString(registro.totalProvision);
            };
        }

        return lprovision;
    }

    private void veRegistro()
    {
        pnTitulos.Enabled = false;
        pnBotones.Visible = false;
        pnCabecerarecaudacion.Visible = false;
        pnAltas.Visible = true;
        pnRecaudacion.Visible = true;

    }

    private void aLista()
    {
        string laccion1 = "LLENA";
        int clave = Convert.ToInt32(grvRecaudacionCabecera.SelectedValue);
        string lsucursal = ddlSucursal2.Text, lestado;

        /*LLENA TOTALES EN CABECERA*/
        dc.sp_totalesRecaudacion(laccion1, clave);

        /*TRAE DATOS COMPLETOS DE CABECERA DESPUES DE CALCULOS*/

        var consultaRc = from Rc in dc.tbl_CabRecaudacion
                         where Rc.id_cab_recaudacion == clave
                         select new
                         {
                             NUMERO = Rc.NUMERO
                           ,
                             SUCURSAL = Rc.SUCURSAL
                           ,
                             FECHA = Rc.FECHA
                           ,
                             DESCRIPCION = Rc.DESCRIPCION
                           ,
                             TOTALEFECTIVO = Rc.TOTALEFECTIVO
                           ,
                             TOTALCHEQUES = Rc.TOTALCHEQUES
                           ,
                             TOTALTARJETAS = Rc.TOTALTARJETAS
                           ,
                             TOTALAUTOCONSUMO = Rc.TOTALAUTOCONSUMO
                           ,
                             TOTALNOTACREDITO = Rc.TOTALNOTACREDITO
                           ,
                             TOTALNOMINA = Rc.TOTALNOMINA
                           ,
                             TOTALTRANSFERENCIA = Rc.TOTALTRANSFERENCIA
                           ,
                             TOTALTARJETADEBITO = Rc.TOTALTARJETADEBITO
                           ,
                             TOTALCXC = Rc.TOTALCXC
                           ,
                             TOTALVARIOS = Rc.TOTALVARIOS
                           ,
                             TOTALINGRESOS = Rc.TOTALINGRESOS
                           ,
                             TOTALRETENCIONIVA = Rc.TOTALRETENCIONIVA
                           ,
                             TOTALRETENCIONFUENTE = Rc.TOTALRETENCIONFUENTE
                           ,
                             BANCO = Rc.BANCO
                           ,
                             NUMERODECUENTA = Rc.NUMERODECUENTA
                           ,
                             VALORDEPOSITO = Rc.VALORDEPOSITO
                           ,
                             NUMERODEPOSITO = Rc.NUMERODEPOSITO
                           ,
                             GTOAUTORIZADO = Rc.GTOAUTORIZADO
                           ,
                             TOTALPROVISION = Rc.TOTALPROVISION
                           ,
                             ESTADO = Rc.ESTADO
                           ,
                             id_DetRecaudacion = Rc.id_cab_recaudacion
                         };

        if (consultaRc.Count() == 0)
        {
            // no existe , registrar nueva fila de recaudacion
        }
        else
        {
            foreach (var registro in consultaRc)
            {
                lblTitulo.Text = registro.NUMERO;
                txtFecha.Text = string.Format("{0:d}", Convert.ToString(registro.FECHA));
                txtDescripcion.Text = registro.DESCRIPCION;
                txtTotalEfectivo.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALEFECTIVO));
                txtTotalCheques.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALCHEQUES));
                txtTarjetas.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALTARJETAS));
                txtTotalAutoconsumo.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALAUTOCONSUMO));
                txtNotaCredito.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALNOTACREDITO));
                txtNomina.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALNOMINA));
                txtTransferencia.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALTRANSFERENCIA));
                txtDebito.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALTARJETADEBITO));
                txtcxc.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALCXC));
                txtVarios.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALVARIOS));
                txtTotalIngresos.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALINGRESOS));
                txtTotalRetencionIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALRETENCIONIVA));
                txtTotalRetencionFuente.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALRETENCIONFUENTE));
                //txtBanco.Text = registro.BANCO;
                //txtNumeroCuenta.Text = Convert.ToString(registro.NUMERODECUENTA);
                txtValorDeposito.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.VALORDEPOSITO));
                txtNumeroDeposito.Text = registro.NUMERODEPOSITO;
                txtGtoAutorizado.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.GTOAUTORIZADO));
                txtProvision.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(registro.TOTALPROVISION));
                txtEstado.Text = registro.ESTADO;

            }
        }


        lestado = txtEstado.Text.Trim();

        switch (lestado)
        {
            case "0":
                {
                    pnCabecerarecaudacion.Enabled = true;
                    pnDepositos.Enabled = true;
                    txtDescripcion.Enabled = true;
                    txtGtoAutorizado.Enabled = true;
                    txtProvision.Enabled = false;
                    txtGtoAutorizado.Text = consultarGastoAutorizado();
                    txtProvision.Text = consultarProvision();
                    if (txtProvision.Text.Trim() == string.Empty)
                    {
                        txtProvision.Text = "0,00";
                    }
                    if (txtGtoAutorizado.Text.Trim() == string.Empty)
                    {
                        txtGtoAutorizado.Text = "0,00";
                    }
                    break;
                }
            case "1":
                {
                    pnCabecerarecaudacion.Enabled = true;
                    pnDepositos.Enabled = true;
                    txtDescripcion.Enabled = false;
                    txtGtoAutorizado.Enabled = false;
                    txtProvision.Enabled = false;
                    break;
                }
            case "2":
                {
                    pnCabecerarecaudacion.Enabled = false;
                    pnDepositos.Enabled = false;
                    txtDescripcion.Enabled = false;
                    txtGtoAutorizado.Enabled = false;
                    txtProvision.Enabled = false;
                    break;
                }
            default:
                pnCabecerarecaudacion.Enabled = true;
                break;
        }


        var consultaRd = from Rd in dc.tbl_DetRecaudacion
                         where Rd.id_CabRecaudacion == clave
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
    
    protected void btnListar_Click(object sender, EventArgs e)
    {
        btnListar_Click();
        pnCabecerarecaudacion.Visible = true;
    }

    protected void btnListar_Click()
    {
        string lsucursal;
        DateTime lfechaIni, lfechaFin;



        lsucursal = ddlSucursal2.Text.Trim();
        lfechaIni = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);

        var consultaRc = from Rc in dc.tbl_CabRecaudacion
                         where Rc.SUCURSAL == lsucursal &&
                                Rc.FECHA >= lfechaIni &&
                                Rc.FECHA <= lfechaFin
                         orderby Rc.FECHA
                         select new
                         {
                             NUMERO = Rc.NUMERO
                          ,
                             SUCURSAL = Rc.SUCURSAL
                          ,
                             FECHA = Rc.FECHA
                          ,
                             DESCRIPCION = Rc.DESCRIPCION
                          ,
                             TOTALEFECTIVO = Rc.TOTALEFECTIVO
                          ,
                             TOTALCHEQUES = Rc.TOTALCHEQUES
                          ,
                             TOTALTARJETAS = Rc.TOTALTARJETAS
                          ,
                             TOTALAUTOCONSUMO = Rc.TOTALAUTOCONSUMO
                          ,
                             TOTALNOTACREDITO = Rc.TOTALNOTACREDITO
                          ,
                             TOTALNOMINA = Rc.TOTALNOMINA
                          ,
                             TOTALVARIOS = Rc.TOTALVARIOS
                          ,
                             TOTALINGRESOS = Rc.TOTALINGRESOS
                          ,
                             TOTALRETENCIONIVA = Rc.TOTALRETENCIONIVA
                          ,
                             TOTALRETENCIONFUENTE = Rc.TOTALRETENCIONFUENTE
                          ,
                             BANCO = Rc.BANCO
                          ,
                             NUMERODECUENTA = Rc.VALORDEPOSITO
                          ,
                             VALORDEPOSITO = Rc.GTOAUTORIZADO
                          ,
                             NUMERODEPOSITO = Rc.NUMERODEPOSITO
                          ,
                             GTOAUTORIZADO = Rc.GTOAUTORIZADO
                          ,
                             TOTALPROVISION = Rc.TOTALPROVISION
                          ,
                             ESTADO = Rc.ESTADO
                          ,
                             id_DetRecaudacion = Rc.id_cab_recaudacion
                         };
        grvRecaudacionCabecera.DataSource = consultaRc;
        grvRecaudacionCabecera.DataBind();

    }

    protected void grvRecaudacionCabecera_SelectedIndexChanged(object sender, EventArgs e)
    {
        aLista();
        veRegistro();

        /*DESPLEGAR DEPOSITOS EN GRID*/
        listarDepositos();
    }

   protected void grvDepositos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int lidDeposito;
        lidDeposito = Convert.ToInt32(grvDepositos.SelectedValue);
        pnEliminarDeposito.GroupingText = "Código: " + Convert.ToString(lidDeposito);
        grvDepositos.Visible = false;
        pnEliminarDeposito.Visible = true;
    }

   protected void btnEliminarDeposito_Click(object sender, EventArgs e)
   {
       int lidDeposito;
       lidDeposito = Convert.ToInt32(grvDepositos.SelectedValue);
       dc.sp_abmDepositos("ELIMINAR", lidDeposito, 0, 0, 0, "", "");
       btnCancelarDeposito_Click();
   }

   protected void btnCancelarDeposito_Click(object sender, EventArgs e)
   {
       btnCancelarDeposito_Click();
   }

   protected void btnCancelarDeposito_Click()
   {
       pnEliminarDeposito.GroupingText = "";
       grvDepositos.Visible = true;
       pnEliminarDeposito.Visible = false;
       listarDepositos();
   }

   protected void btnVerificar_Click(object sender, EventArgs e)
   {

       lblStatus.Text = string.Empty;
       int clave = Convert.ToInt32(grvRecaudacionCabecera.SelectedValue);
       if (txtProvision.Text.Trim() == string.Empty)
       {
           txtProvision.Text = "0,00";
       }

       if (txtGtoAutorizado.Text.Trim() == string.Empty)
       {
           txtGtoAutorizado.Text = "0,00";
       }

       string lestado = txtEstado.Text.Trim();
       string ldescripcion = txtDescripcion.Text.Trim();

       decimal lgasto = Convert.ToDecimal(txtGtoAutorizado.Text);
       decimal lprovision = Convert.ToDecimal(txtProvision.Text);

       if (lestado == "0")
       {
           tbl_CabRecaudacion tbl_CabRecaudacion = dc.tbl_CabRecaudacion.SingleOrDefault(x => x.id_cab_recaudacion == clave);
           tbl_CabRecaudacion.GTOAUTORIZADO = lgasto;
           tbl_CabRecaudacion.TOTALPROVISION = lprovision;
           dc.SubmitChanges();
           aLista();
           veRegistro();
           lblStatus.Text = string.Empty;
       }
       else
       {
           lblStatus.Text = "Esta caja ha sido cerrada no se puede modificar ni verificar";
       }

   }

   protected void btnCierreParcial_Click(object sender, EventArgs e)
   {
       bool pasa = true;
       lblStatus.Text = string.Empty;
       int clave = Convert.ToInt32(grvRecaudacionCabecera.SelectedValue);
       if (txtProvision.Text.Trim() == string.Empty)
       {
           txtProvision.Text = "0,00";
       }
       if (txtGtoAutorizado.Text.Trim() == string.Empty)
       {
           txtGtoAutorizado.Text = "0,00";
       }
       string lestado = txtEstado.Text.Trim();
       string ldescripcion = txtDescripcion.Text.Trim();
       decimal lgasto = Convert.ToDecimal(txtGtoAutorizado.Text);
       decimal lprovision = Convert.ToDecimal(txtProvision.Text);
       if (ldescripcion.Length <= 0)
       {
           lblStatus.Text = "Debe ingresar en Descripción si hubo o no,novedades";
           pasa = false;
       }

       if (lestado == "0" && pasa)
       {
           /*LLENA TOTALES EN CABECERA*/
           /*GRABAR EL ESTADO 1*/

           tbl_CabRecaudacion tbl_CabRecaudacion = dc.tbl_CabRecaudacion.SingleOrDefault(x => x.id_cab_recaudacion == clave);
           tbl_CabRecaudacion.ESTADO = "1";
           tbl_CabRecaudacion.DESCRIPCION = ldescripcion;
           tbl_CabRecaudacion.GTOAUTORIZADO = lgasto;
           tbl_CabRecaudacion.TOTALPROVISION = lprovision;
           dc.SubmitChanges();
           btnCancelarRegistro_Click();
       }
       else
       {
           if (lestado == "1")
           {
               lblStatus.Text = "Ya se realizo el cierre parcial, no puede modificar";
           }
           if (lestado == "2")
           {
               lblStatus.Text = "Ya se realizo el cierre total, no puede modificar";
           }
       }
   }

   protected void btnCierreTotal_Click(object sender, EventArgs e)
   {
       bool pasa = true;
       DateTime esteDia = DateTime.Today;
       DateTime lfechaInicio, lfechaFin;

       //string lsuc,lfechaInicio,lfechaFin;

       string lsuc;
       lblStatus.Text = string.Empty;
       int clave = Convert.ToInt32(grvRecaudacionCabecera.SelectedValue);
       string lestado = txtEstado.Text.Trim();

       lfechaInicio = DateTime.Today;
       lfechaFin = DateTime.Today;
       lsuc = "";





       // string lbanco = txtBanco.Text.Trim();
       //string lnumeroCuenta = txtNumeroCuenta .Text.Trim();
       string lnumeroDeposito = txtNumeroDeposito.Text.Trim();

       if (lestado == "0" && pasa)
       {
           lblStatus.Text = "Debe realizar primero el cierre parcial";
           pasa = false;
       }

       // if (lbanco.Length <= 0 || lnumeroCuenta.Length <= 0 || lnumeroDeposito.Length <= 0)
       if (lnumeroDeposito.Length <= 0)
       {
           lblStatus.Text = "Debe ingresar los datos del depósito";
           pasa = false;
       }
       if (lestado == "1" && pasa)
       {
           /*LLENA TOTALES EN CABECERA*/
           /*GRABAR EL ESTADO 1*/

           tbl_CabRecaudacion tbl_CabRecaudacion = dc.tbl_CabRecaudacion.SingleOrDefault(x => x.id_cab_recaudacion == clave);
           tbl_CabRecaudacion.ESTADO = "2";
           //  tbl_CabRecaudacion.BANCO = lbanco;
           // tbl_CabRecaudacion.NUMERODECUENTA = lnumeroCuenta;
           tbl_CabRecaudacion.NUMERODEPOSITO = lnumeroDeposito;
           dc.SubmitChanges();

           ///var consultaFec = new object();

           var consultaFec = from mfec in dc.tbl_CabRecaudacion
                             where mfec.id_cab_recaudacion == clave
                             select new
                             {
                                 fechaInicio = mfec.FECHA,
                                 fechaFin = mfec.FECHA,
                                 suc = mfec.SUCURSAL
                             };

           if (consultaFec.Count() == 0)
           {
               // no existe , registrar nueva fila de recaudacion
           }
           else
           {
               foreach (var registro in consultaFec)
               {
                   lfechaInicio = Convert.ToDateTime(registro.fechaInicio);
                   lfechaFin = Convert.ToDateTime(registro.fechaFin);
                   lsuc = registro.suc;
               }
           }


           //dc.sp_repCierraCaja(lfechaInicio,lfechaFin,lsuc);

           Session["pFechaInicio"] = lfechaInicio;
           Session["pFechaFin"] = lfechaFin;
           Session["pSuc"] = lsuc;

           // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

           Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('cierre.aspx','','width=800,height=500') </script>");

           btnCancelarRegistro_Click();

       }
       else
       {
           lblStatus.Text = "Ya se realizo el cierre total, no puede modificar";
       }

   }

   protected void btnCancelarRegistro_Click(object sender, EventArgs e)
   {
       btnCancelarRegistro_Click();
   }

   protected void btnCancelarRegistro_Click()
   {
       pnTitulos.Enabled = true;
       pnBotones.Visible = true;
       pnCabecerarecaudacion.Visible = true;
       pnCabecerarecaudacion.Enabled = true;
       pnAltas.Visible = false;
       pnRecaudacion.Visible = false;
       btnListar_Click();
   }

   protected void btnAniadirDeposito_Click(object sender, EventArgs e)
   {
       int lidrecauda, lidbanco;
       decimal lvalor;
       string ldeposito, laccion, ldescripcion, lsucursal;

       laccion = "AGREGAR";
       lidrecauda = Convert.ToInt32(grvRecaudacionCabecera.SelectedValue);
       lidbanco = Convert.ToInt32(ddlBanco.SelectedValue);
       //lvalor = Convert.ToDecimal(txtValorDeposito.Text);
       lvalor = Convert.ToDecimal(txtValorDepositado.Text);
       ldeposito = txtNumeroDeposito.Text.Trim();
       ldescripcion = txtDescripcionDeposito.Text.Trim();
       lsucursal = ddlSucursal2.SelectedValue;

       /*AÑADIR LOS DEPOSITOS*/
       dc.sp_abmDepositos(laccion, 0, lidrecauda, lidbanco, lvalor, ldeposito, ldescripcion);


       /*DESPLEGAR DEPOSITOS EN GRID*/
       listarDepositos();
   }

    /*A EXCEL*/
    #region PAGOSREALIZADOSAEXCEL
   protected void btnExcelFe_Click(object sender, EventArgs e)
   {
       uno();
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
           grvRecaudacion.AllowPaging = false;
           /// this.BindGrid();

           grvRecaudacion.HeaderRow.BackColor = Color.White;
           foreach (TableCell cell in grvRecaudacion.HeaderRow.Cells)
           {
               cell.BackColor = grvRecaudacion.HeaderStyle.BackColor;
           }
           foreach (GridViewRow row in grvRecaudacion.Rows)
           {
               row.BackColor = Color.White;
               foreach (TableCell cell in row.Cells)
               {
                   if (row.RowIndex % 2 == 0)
                   {
                       cell.BackColor = grvRecaudacion.AlternatingRowStyle.BackColor;
                   }
                   else
                   {
                       cell.BackColor = grvRecaudacion.RowStyle.BackColor;
                   }
                   cell.CssClass = "textmode";
               }
           }

           grvRecaudacion.RenderControl(hw);

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