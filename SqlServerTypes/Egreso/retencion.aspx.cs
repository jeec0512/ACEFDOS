using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Egreso_retencion : System.Web.UI.Page
{

    public string pRuc = "", pCodDoc = "", pEstab = "", pPtoEmi = "", pSecuencial = "", pDirEstablecimiento = "", pObligadoContabilidad = "";

    public int pId_InfoTributaria = 0, pId_InfoCompRetencion = 0;

    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();



    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion

    #region INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //pnRetener.Visible = true;
            perfilUsuario();
            llenarListados();
            formatoTexto();
            inicializarObjetos();
            blancoxCero();
        }
    }


    #endregion

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

            var cSucursal = dc.sp_listarSucursal("", grupo.Trim(), nivel, tipo, sucursal);

            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }


    #region OBJETOS
   

    protected void txtRuc_TextChanged(object sender, EventArgs e)
    {
        txtRuc_TextChanged();
    }
    #endregion

    #region VALIDACION AL INGRESAR LOS CAMPOS
    /*VALIDACION AL INGRESAR LOS CAMPOS*/

    protected void txtNumRet_TextChanged(object sender, EventArgs e)
    {
        if (txtNumRet.Text.Length > 0)
        {
            txtNumRet.Text = llenarCeros(txtNumRet.Text.Trim(), '0', 9);
        }

        txtRuc.Focus();
    }

    protected void txtNumDoc_TextChanged(object sender, EventArgs e)
    {
        if (txtNumDoc.Text.Length > 0)
        {
            txtNumDoc.Text = llenarCeros(txtNumDoc.Text.Trim(), '0', 9);
        }
        txtAutorizacion.Focus();
    }

    protected void txtAutorizacion_TextChanged(object sender, EventArgs e)
    {
        //if (txtAutorizacion.Text.Length > 0)
        //{
        //   txtAutorizacion.Text = llenarCeros(txtAutorizacion.Text.Trim(), '0', 49);
        // }
    }

    #endregion

    #region INGRESO DE VALORES DEL DOCUMENTO
    /*INGRESO DE VALORES DEL DOCUMENTO*/
    protected void txtBsubtotal_TextChanged(object sender, EventArgs e)
    {
        txtBsubtotal_TextChanged();
        txtBtarifa0.Focus();

    }

    protected void txtBtarifa0_TextChanged(object sender, EventArgs e)
    {
        txtBtarifa0_TextChanged();
        txtBotros.Focus();

    }

    protected void txtSsubtotal_TextChanged(object sender, EventArgs e)
    {
        txtSsubtotal_TextChanged();
        txtStarifa0.Focus();
    }

    protected void txtStarifa0_TextChanged(object sender, EventArgs e)
    {
        txtStarifa0_TextChanged();
        txtSotros.Focus();
    }

    protected void txtBotros_TextChanged(object sender, EventArgs e)
    {
        txtBotros_TextChanged();
    }

    protected void txtSotros_TextChanged(object sender, EventArgs e)
    {
        txtSotros_TextChanged();
    }

    /*PROCESOS DE CAMBIOS  DE VALORES DEL DOCUMENTO*/

    protected void txtBsubtotal_TextChanged()
    {
        double Biva = Convert.ToDouble(txtBsubtotal.Text);
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
                txtBsubtotal.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBsubtotal.Text));

            }
            else
            {
                pnBienIva.Visible = false;
            }

        }
        sumatoriaSubtotalesyTotales();

        txtBiva.Text = Convert.ToString((Biva * tarifa) / 100);
        txtBiva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBiva.Text));
        txtBibase.Text = txtBsubtotal.Text;// txtSubtotalBienes.Text;
        txtBibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase.Text));
        txtBibase2.Text = txtBiva.Text;
        txtBibase2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase2.Text));

        cambioInteractivoPorcentajes();

    }

    protected void txtBtarifa0_TextChanged()
    {
        double B0 = Convert.ToDouble(txtBtarifa0.Text);

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
                txtBtarifa0.Text = Convert.ToString(B0);
                txtBtarifa0.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBtarifa0.Text));
            }
            else
            {
                pnBienCero.Visible = false;
            }

        }


        sumatoriaSubtotalesyTotales();
        txtB0base.Text = Convert.ToString(B0);
        txtB0base.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtB0base.Text));


        txtBibase.Text = txtBsubtotal.Text;//txtSubtotalBienes.Text;
        txtBibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBibase.Text));

        cambioInteractivoPorcentajes();

    }

    protected void txtSsubtotal_TextChanged()
    {
        double Sbien = Convert.ToDouble(txtSsubtotal.Text);

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
                txtSsubtotal.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSsubtotal.Text));

            }
            else
            {
                pnServicioIva.Visible = false;
            }

        }

        sumatoriaSubtotalesyTotales();

        txtSiva.Text = Convert.ToString((Sbien * tarifa) / 100);
        txtSiva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSiva.Text));

        double otros = Convert.ToDouble(txtSotros.Text);
       // if (otros <= 0)
        //{
            txtSibase.Text = txtSsubtotal.Text;//txtSubtotalServicios.Text;


            txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));
        //}
        //else 
       // {
         //   txtSibase.Text = txtSotros.Text;//txtSubtotalServicios.Text;
          //  txtSibase.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase.Text));
       // }
        
        txtSibase2.Text = txtSiva.Text;
        txtSibase2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSibase2.Text));


        cambioInteractivoPorcentajes();

    }

    protected void txtStarifa0_TextChanged()
    {
        double S0 = Convert.ToDouble(txtStarifa0.Text);

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
                txtStarifa0.Text = Convert.ToString(S0);
                txtStarifa0.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtStarifa0.Text));
            }
            else
            {
                pnServicioCero.Visible = false;
            }

        }

        sumatoriaSubtotalesyTotales();
        txtS0base.Text = Convert.ToString(S0);
        txtS0base.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtS0base.Text));

        cambioInteractivoPorcentajes();

    }

    protected void txtBotros_TextChanged()
    {
        sumatoriaSubtotalesyTotales();
        txtBotros.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBotros.Text));
        double otros = Convert.ToDouble(txtBotros.Text);
       
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

        cambioInteractivoPorcentajes();

    }

    protected void txtSotros_TextChanged()
    {
        sumatoriaSubtotalesyTotales();

        txtSotros.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtSotros.Text));

        double otros = Convert.ToDouble(txtSotros.Text);
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

        cambioInteractivoPorcentajes();

    }


    #endregion

    #region PROCESOS INTERNOS
    /*PROCESOS INTERNOS*/
    protected void llenarListados()
    {
        
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

        ListItem liSucursal = new ListItem("Seleccione la sucursal afectar ", "-1");
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

        ListItem liCco = new ListItem("Seleccione el centro de costo afectar ", "-1");
        ddlAfectaCcosto.Items.Insert(0, liCco);
        #endregion

        /* TRAER CONCEPTOS*/
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

        ddlB0Gastos.DataSource = cCon;
        ddlB0Gastos.DataBind();

        ddlSiGastos.DataSource = cCon;
        ddlSiGastos.DataBind();

        ddlS0Gastos.DataSource = cCon;
        ddlS0Gastos.DataBind();

        ListItem listCon = new ListItem("Seleccione Concepto", "-1");

        ddlBiGastos.Items.Insert(0, listCon);
        ddlB0Gastos.Items.Insert(0, listCon);
        ddlSiGastos.Items.Insert(0, listCon);
        ddlS0Gastos.Items.Insert(0, listCon);


        /* TRAER CODIGO CONTABLE*/
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

        ddlB0CodCble.DataSource = cCble;
        ddlB0CodCble.DataBind();

        ddlSiCodCble.DataSource = cCble;
        ddlSiCodCble.DataBind();

        ddlS0CodCble.DataSource = cCble;
        ddlS0CodCble.DataBind();

        ListItem listCble = new ListItem("Seleccione código contable", "-1");

        ddlBiCodCble.Items.Insert(0, listCble);
        ddlB0CodCble.Items.Insert(0, listCble);
        ddlSiCodCble.Items.Insert(0, listCble);
        ddlS0CodCble.Items.Insert(0, listCble);
    }

    /*FORMATO A LOS TEXTOS*/
    protected void formatoTexto()
    {
        double lvalor = 0;
        lblMensaje.Text = string.Empty;

        txtBsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBtarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtBiva.Text = string.Format("{0:#,##0.##}", lvalor);

        txtSsubtotal.Text = string.Format("{0:#,##0.##}", lvalor);
        txtStarifa0.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSotros.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSiva.Text = string.Format("{0:#,##0.##}", lvalor);

        txtSubtotalBienes.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSubtotalServicios.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSubtotalGeneral.Text = string.Format("{0:#,##0.##}", lvalor);
        txtSubtotalIva.Text = string.Format("{0:#,##0.###}", lvalor);
        txtPorcIce.Text = string.Format("{0:#,##0.##}", lvalor);


        txtTotalFuente.Text = string.Format("{0:#,##0.##}", lvalor);
        txtTotalIva.Text = string.Format("{0:#,##0.##}", lvalor);
        txtTotalRetencion.Text = string.Format("{0:#,##0.##}", lvalor);
        txtTotalDocumento.Text = string.Format("{0:#,##0.##}", lvalor);
        txtApagar.Text = string.Format("{0:#,##0.##}", lvalor);
    }

    /*INICIALIZAR VARIABLES*/
    protected void inicializarObjetos()
    {
        DateTime esteDia = DateTime.Today;

        txtFecha.Text = esteDia.ToString("d");
        txtFechCaduc.Text = esteDia.ToString("d");
        txtFechDoc.Text = esteDia.ToString("d");

        ddlTarifa.SelectedValue = "2";
    }

    /*BUSCA PROVEEDOR*/
    protected void txtRuc_TextChanged()
    {
        string lruc = txtRuc.Text.Trim();

        // var lciudadano = dc.sp_abmMatriz2(laccion, lruc, "", "", "", "", "", "", "");
        var lciudadano = from mMatriz in dc.tbl_matriz
                         where mMatriz.ruc == lruc
                         select new
                         {
                             razonsocial = mMatriz.razonsocial,
                             e_mail = mMatriz.e_mail,
                             contribuyenteEspecial = mMatriz.contribuyenteEspecial
                         };

        if (lciudadano.Count() <= 0)
        {
            txtrso.Text = string.Empty;
            txtemail.Text = string.Empty;
            txtContribuyente.Text = string.Empty;
            pnMensaje2.Visible = true;
            btnIngresaProv.Visible = true;
            lblMensaje.Text = "No existe proveedor...";
        }
        else
        {
            pnMensaje2.Visible = false;
            lblMensaje.Text = "";
            foreach (var registro in lciudadano)
            {
                txtrso.Text = registro.razonsocial;
                txtemail.Text = registro.e_mail;
                txtContribuyente.Text = registro.contribuyenteEspecial;
            }
        }
        ddlTipDoc.Focus();
    }

    /*INGRESAR PROVEEDOR*/


    protected void blancoxCero()
    {
        /*bienes*/
        txtBsubtotal.Text = "0";
        txtBtarifa0.Text = "0";
        txtBotros.Text = "0";
        txtBiva.Text = "0";

        /*servicios*/
        txtSsubtotal.Text = "0";
        txtStarifa0.Text = "0";
        txtSotros.Text = "0";
        txtSiva.Text = "0";


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
        txtApagar.Text = "0";

        /*porcentajes Bienes iva*/
        txtBibase.Text = "0";
        txtBibase2.Text = "0";

        txtBiporc.Text = "0";
        txtBiporc2.Text = "0";

        txtBiValor.Text = "0";
        txtBiValor2.Text = "0";

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

    protected int devolverTarifa()
    {
        int tarifa = 0;
        int codigo = Convert.ToInt32(ddlTarifa.SelectedValue);

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

    protected void sumaSubtotalesBienes()
    {
        double b1 = Convert.ToDouble(txtBsubtotal.Text);
        double b2 = Convert.ToDouble(txtBtarifa0.Text);
        double b3 = Convert.ToDouble(txtBotros.Text);

        txtSubtotalBienes.Text = string.Format("{0:#,##0.##}", Convert.ToString(b1 + b2 + b3));
    }

    protected void sumaSubtotalesServicios()
    {
        double s1 = Convert.ToDouble(txtSsubtotal.Text);
        double s2 = Convert.ToDouble(txtStarifa0.Text);
        double s3 = Convert.ToDouble(txtSotros.Text);

        txtSubtotalServicios.Text = string.Format("{0:#,##0.##}", Convert.ToString(s1 + s2 + s3));
    }

    protected void sumaSubtotalGeneral()
    {
        double sb1 = Convert.ToDouble(txtSubtotalBienes.Text);
        double ss1 = Convert.ToDouble(txtSubtotalServicios.Text);
        txtSubtotalGeneral.Text = string.Format("{0:#,##0.##}", Convert.ToString(sb1 + ss1));
    }

    protected void sumaIVA()
    {
        double biva = Convert.ToDouble(txtBiva.Text);
        double siva = Convert.ToDouble(txtSiva.Text);
        double suma = biva + siva;
        txtSubtotalIva.Text = string.Format("{0:#,##0.###}", Convert.ToString(suma));
    }

    protected void totalDocumento()
    {
        double subtB = Convert.ToDouble(txtBsubtotal.Text);
        double subtg = Convert.ToDouble(txtSubtotalGeneral.Text);
        double subtiva = Convert.ToDouble(txtSubtotalIva.Text);
        double valIce = Convert.ToDouble(txtBotros.Text);
        double totIce = valIce*Convert.ToDouble(txtPorcIce.Text) / 100;


        if (totIce <= 0)
        {
            txtTotalDocumento.Text = string.Format("{0:#,##0.##}", Convert.ToString(subtg + subtiva));
        }
        else 
        {

            txtTotalDocumento.Text = string.Format("{0:#,##0.##}", Convert.ToString(subtB + subtiva + totIce));
        }


    }

    protected void totalApagar()
    {
        double retFuente = Convert.ToDouble(txtTotalFuente.Text);
        double retIva = Convert.ToDouble(txtTotalIva.Text);
        double totRet = Convert.ToDouble(txtTotalRetencion.Text);
        double totDoc = Convert.ToDouble(txtTotalDocumento.Text);
        double suma = totDoc - totRet;

        txtApagar.Text = string.Format("{0:#,##0.##}", Convert.ToString(suma));


    }

    protected void sumarRetencionesFuente()
    {
        double suma = 0;
        suma = Convert.ToDouble(txtBiValor.Text) + Convert.ToDouble(txtB0Valor.Text) + Convert.ToDouble(txtSiValor.Text) + Convert.ToDouble(txtS0Valor.Text);
        txtTotalFuente.Text = Convert.ToString(suma);
        txtTotalFuente.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtTotalFuente.Text));

    }

    protected void sumarRetencionesIVA()
    {
        double suma = 0;
        suma = Convert.ToDouble(txtBiValor2.Text) + Convert.ToDouble(txtSiValor2.Text);
        txtTotalIva.Text = Convert.ToString(suma);
        txtTotalIva.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtTotalIva.Text));
    }

    protected void sumarTotalRetencion()
    {
        double suma = 0;
        suma = Convert.ToDouble(txtTotalFuente.Text) + Convert.ToDouble(txtTotalIva.Text);
        txtTotalRetencion.Text = Convert.ToString(suma);
        txtTotalRetencion.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtTotalRetencion.Text));
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

    protected void cambioInteractivoValores()
    {
        /*txtBsubtotal_TextChanged();
        txtBtarifa0_TextChanged();
        txtSsubtotal_TextChanged();
        txtStarifa0_TextChanged();
        txtStarifa0_TextChanged();
        txtBotros_TextChanged();
        txtSotros_TextChanged();*/
    }

    protected void cambioInteractivoPorcentajes()
    {
        /*ddlBiGastos_SelectedIndexChanged();
        ddlBiCodCble_SelectedIndexChanged();
        ddlB0Gastos_SelectedIndexChanged();
        ddlB0CodCble_SelectedIndexChanged();
        ddlSiGastos_SelectedIndexChanged();
        ddlSiCodCble_SelectedIndexChanged();
        ddlS0Gastos_SelectedIndexChanged();
        ddlS0CodCble_SelectedIndexChanged();*/
    }


    #endregion

    #region SELECCIONA CONCEPTO Y TIPO CONTABLE (porcentaje y valores a retener)
    /*BIENES*/
    /*CON IVA*/

    protected void ddlBiGastos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBiGastos_SelectedIndexChanged();

    }

    protected void ddlBiCodCble_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBiCodCble_SelectedIndexChanged();
    }
    /* BIENES*/
    /*TARIFA CERO*/
    protected void ddlB0Gastos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlB0Gastos_SelectedIndexChanged();
    }

    protected void ddlB0CodCble_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlB0CodCble_SelectedIndexChanged();
    }

    /*SERVICIOS*/
    /*CON IVA*/

    protected void ddlSiGastos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSiGastos_SelectedIndexChanged();
    }

    protected void ddlSiCodCble_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSiCodCble_SelectedIndexChanged();
    }

    /*SERVICIOS*/
    /*TARIFA CERO*/

    protected void ddlS0Gastos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlS0Gastos_SelectedIndexChanged();
    }

    protected void ddlS0CodCble_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlS0CodCble_SelectedIndexChanged();
    }

    /*CAMBIOS INTERACTIVOS*/

    protected void ddlBiGastos_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlBiGastos.SelectedValue.Trim();
        string lcodcble = ddlBiCodCble.SelectedValue.Trim();

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
        txtBiValor.Text = Convert.ToString(retencionFuente);
        txtBiValor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBiValor.Text));

        retencionIva = (Convert.ToDouble(txtBibase2.Text) * biIva) / 100;
        txtBiValor2.Text = Convert.ToString(retencionIva);
        txtBiValor2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBiValor2.Text));

        sumatoriaSubtotalesyTotales();
        cambioInteractivoValores();

    }

    protected void ddlBiCodCble_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlBiGastos.SelectedValue.Trim();
        string lcodcble = ddlBiCodCble.SelectedValue.Trim();

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
        txtBiValor.Text = Convert.ToString(retencionFuente);
        txtBiValor.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBiValor.Text));

        retencionIva = (Convert.ToDouble(txtBibase2.Text) * biIva) / 100;
        txtBiValor2.Text = Convert.ToString(retencionIva);
        txtBiValor2.Text = string.Format("{0:#,##0.##}", Convert.ToDouble(txtBiValor2.Text));

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();

    }
    /* BIENES*/
    /*TARIFA CERO*/
    protected void ddlB0Gastos_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlB0Gastos.SelectedValue.Trim();
        string lcodcble = ddlB0CodCble.SelectedValue.Trim();

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

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    protected void ddlB0CodCble_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlB0Gastos.SelectedValue.Trim();
        string lcodcble = ddlB0CodCble.SelectedValue.Trim();

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

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    /*SERVICIOS*/
    /*CON IVA*/

    protected void ddlSiGastos_SelectedIndexChanged()
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

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    protected void ddlSiCodCble_SelectedIndexChanged()
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

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    /*SERVICIOS*/
    /*TARIFA CERO*/

    protected void ddlS0Gastos_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlB0Gastos.SelectedValue.Trim();
        string lcodcble = ddlS0CodCble.SelectedValue.Trim();

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

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    protected void ddlS0CodCble_SelectedIndexChanged()
    {
        string lFuente, lIva;

        string lvalor = ddlS0Gastos.SelectedValue.Trim();
        string lcodcble = ddlS0CodCble.SelectedValue.Trim();

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

        cambioInteractivoValores();
        sumatoriaSubtotalesyTotales();
    }

    #endregion

    #region envia parametros de obtencion de  porcentajes de retencion para bienes
    /* envia parametros de obtencion de  porcentajes de retencion para bienes*/
    protected Tuple<string, string> parametrizarValoresB(string lvalor, string lcodcble)
    {
        string codFte = "0";
        string codIva = "0";
         string tipoDoc = ddlTipDoc.SelectedValue;


        int lesCE; // conribuyente especial

        //lesCE = 1;
        if (txtContribuyente.Text.Length > 0)
        {
            lesCE = 1; //Convert.ToInt16(ddlContEsp.SelectedValue);
        }
        else
        {
            lesCE = 0;
        }

        switch (lcodcble)
        {
            case "63":
                if (lesCE == 1)
                {
                    codFte = "332";
                    if (tipoDoc== "03")
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

    #region envia parametros de obtencion de  porcentajes de retencion para servicios
    /*envia parametros de obtencion de  porcentajes de retencion para servicios*/
    protected Tuple<string, string> parametrizarValoresS(string lvalor, string lcodcble)
    {
        string lnunContribuyente;
        string codFte = "0";
        string codIva = "0";
        string tipoDoc = ddlTipDoc.SelectedValue;
        
        

        int lesCE; // conribuyente especial

        lnunContribuyente = txtContribuyente.Text.Trim();// txtcontribuyenteEspecial.Text.Trim();
        if (lnunContribuyente.Length <= 0)
        { lesCE = 0; }
        else
        {
            lesCE = 1; //Convert.ToInt16(lnunContribuyente); //Convert.ToInt16(ddlContEsp.SelectedValue);
        }


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

            case "86":
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

    #region VALIDAR DATOS
    /*VALIDA TODA LA INFORMACION QUE ESTE BIEN INGRESADA ANTES DE GRABAR*/
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
            btnEnviar.Visible = false;
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
        /*VALIDA REGISTRO Y DOCUMENTO*/

        string tipoIdentificacionSujetoRetenido = ddlReceptor.SelectedValue;
        string lmensaje = string.Empty; ;
        bool pasa = true;
        string lsucursal, ltarifa, lfechaRet, lnumRet, lruc, lrso, lemail, ltipoDoc, lfechaDoc, lserie, lnumDoc, lautorizacion, lfechaCaduc, lbienServicio;
        double ltotalretenido = 0;
        lsucursal = ddlSucursal.SelectedValue;
        string lestab = string.Empty;
        string lptoemi = string.Empty;
        ltarifa = ddlTarifa.SelectedValue.Trim();
        ltipoDoc = ddlTipDoc.SelectedValue.Trim();

        lfechaRet = txtFecha.Text.Trim();
        lfechaDoc = txtFechDoc.Text.Trim();
        lfechaCaduc = txtFechCaduc.Text.Trim();

        lnumRet = txtNumRet.Text.Trim();
        lruc = txtRuc.Text.Trim();
        lrso = txtrso.Text.Trim();
        lemail = txtemail.Text.Trim();
        lserie = txtSerie.Text.Trim();
        lnumDoc = txtNumDoc.Text.Trim();
        lautorizacion = txtAutorizacion.Text.Trim();
        lbienServicio = txtBien.Text.Trim();
        ltotalretenido = Convert.ToDouble(txtTotalRetencion.Text);
        var ldatos = traerEstabPtoEmi(lsucursal);
        lestab = ldatos.Item1;
        lptoemi = ldatos.Item2;

        string sucAfecta = ddlAfectaSucursal.SelectedValue;
        string ccoAfecta = ddlAfectaCcosto.SelectedValue;

        /*VALORES PARA RETENER*/
        decimal bsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal bcero = Convert.ToDecimal(txtBtarifa0.Text);

        decimal ssubtotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal scero = Convert.ToDecimal(txtStarifa0.Text);

        string bstipo = ddlBiGastos.SelectedValue;
        string bscble = ddlBiCodCble.SelectedValue;

        string b0tipo = ddlB0Gastos.SelectedValue;
        string b0cble = ddlB0CodCble.SelectedValue;

        string sbstipo = ddlSiGastos.SelectedValue;
        string sscble = ddlSiCodCble.SelectedValue;

        string s0tipo = ddlS0Gastos.SelectedValue;
        string s0cble = ddlS0CodCble.SelectedValue;


        if (tipoIdentificacionSujetoRetenido == "-1") 
        {
            lmensaje = "Indique el tipo de identificación del Sujeto retenido ";
            pasa = false;
        }
             

        if (bsubtotal > 0 && (sucAfecta == "-1" || ccoAfecta == "-1"))
        {
            lmensaje = " Ingrese la sucursal y centro de costo al que va a afectar ";
            pasa = false;
        }


        if (bsubtotal > 0 && (bstipo == "-1" || bscble == "-1"))
        {
            lmensaje = " No se ha realizado la retención para Bienes ";
            pasa = false;
        }

        if (bcero > 0 && (b0tipo == "-1" || b0cble == "-1"))
        {
            lmensaje = " No se ha realizado la retención para Bienes ";
            pasa = false;
        }


        if (ssubtotal > 0 && (sbstipo == "-1" || sscble == "-1"))
        {
            lmensaje = " No se ha realizado la retención para Servicios";
            pasa = false;
        }

        if (scero > 0 && (s0tipo == "-1" || s0cble == "-1"))
        {
            lmensaje = " No se ha realizado la retención para Servicios ";
            pasa = false;
        }

        /**/
        bool existeRet = existeRetencion(lestab, lptoemi, lnumRet);
        bool existeDoc = existeDocumento(ltipoDoc, lserie, lnumDoc, lruc, lautorizacion);

        if (existeRet)
        {
            string lretencion = lestab + lptoemi + lnumRet;
            lmensaje = lmensaje + " La retención #" + lretencion + " existe, no se puede añadir ";
            pasa = false;
        }

        if (existeDoc)
        {
            string ldocumento = lserie + lnumDoc;
            lmensaje = lmensaje + " Al documento #" + ldocumento + " ya se le realizó la retención ";
            pasa = false;
        }

        if (lsucursal == "-1")
        {
            lmensaje = lmensaje + " Ingrese Sucursal ";
            pasa = false;
        }

        if (ltarifa == "-1")
        {
            lmensaje = lmensaje + " Ingrese tarifa ";
            pasa = false;
        }

        if (lestab.Length <= 0 || lptoemi.Length <= 0)
        {
            lmensaje = lmensaje + " La sucursal no cuenta con codigo de establecimiento o punto de venta ";
            pasa = false;
        }

        if (lfechaRet.Length <= 0 || lfechaDoc.Length <= 0 || lfechaCaduc.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese las fechas correctamente ";
            pasa = false;
        }

        /******************************************/
        if (lnumRet.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese # de retención ";
            pasa = false;
        }

        if (lruc.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese ruc válido  ";
            pasa = false;
        }

        if (lrso.Length <= 0 || lemail.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese ruc válido y toda la información del proveedor ";
            pasa = false;
        }

        if (lserie.Length <= 0 || lnumDoc.Length <= 0 || lautorizacion.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese serie , # del documento, autorización ";
            pasa = false;
        }
        if (ltipoDoc == "-1")
        {
            lmensaje = lmensaje + " Ingrese tipo de documento ";
            pasa = false;
        }

        if (lbienServicio.Length <= 0)
        {
            lmensaje = lmensaje + " Ingrese descripción del bien o servicio ";
            pasa = false;
        }
        /***********************************************************************/
        if (lserie.Length != 6 || lnumDoc.Length != 9 || lautorizacion.Length < 10 || lautorizacion.Length > 49)
        {
            lmensaje = lmensaje + " Estab/PtoVta deben tener 6 dígitos, #Documento 9 dígitos, autorización si es físico 10 caracteres y si es electrónico 49 dígitos ";
            pasa = false;
        }

        if (ltotalretenido <= 0)
        {
            lmensaje = lmensaje + " No existen valores para retener ";
            pasa = false;
        }

        return Tuple.Create(pasa, lmensaje);
    }
    #endregion

    #region GUARDAR RETENCIÓN
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        pnMensaje2.Visible = true;
        btnIngresaProv.Visible = false;

        string suc = ddlSucursal.SelectedValue.Trim();

        int sec = Convert.ToInt32(txtNumRet.Text);


        if (btnValidar_Click())
        {
            var cSec = dc.sp_secuenciales("", suc, sec);
            guardaInfotributaria();
            guardaInfoCompRetencion();
            guardaImpuestosRet();
            guardaInfoAdicional();
            btnEnviar.Visible = true;

           /* tbl_secuenciales tbl_secuenciales = dc.tbl_secuenciales.SingleOrDefault(p => p.sucursal == suc);
            tbl_secuenciales.retencion = sec;
            dc.SubmitChanges();*/

           
            lblMensaje.Text = " La retención: " + txtNumRet.Text.Trim() + " se ha grabado existosamente";
            btnEnviar.Visible = true;
        }
        else
        {
            lblMensaje.Text = lblMensaje.Text.Trim() + " La retención: " + txtNumRet.Text.Trim() + " NO se ha grabado";
            btnGuardar.Visible = false;
        }
    }
    /*GUARDA RETENCIÓN*/
    protected void guardaInfotributaria()
    {
        string lestab, lptoemi;
        /*inicializar variables*/
        string accion = "GUARDAR";
        string ruc = "1790349578001";
        char ambiente = '2';
        char tipoemision = '1';

        string suc = ddlSucursal.SelectedValue.Trim();
        var lserie = traerEstabPtoEmi(suc);
        lestab = lserie.Item1;
        lptoemi = lserie.Item2;

        string estab = lestab;
        string ptoemi = lptoemi;
        string claveacceso = traerClaveAcceso(estab, ptoemi);
        string coddoc = "07";

        string secuencial = txtNumRet.Text.Trim();
        string cre_sri = string.Empty;
        DateTime fechaDocumento = Convert.ToDateTime(txtFechDoc.Text.Trim());
        DateTime fechaCreacion = Convert.ToDateTime(txtFecha.Text.Trim());
        string Username = (string)Session["SUsername"];
        string sucursal = ddlSucursal.SelectedValue;
        decimal totRetFte = Convert.ToDecimal(txtTotalFuente.Text);
        decimal totRetIva = Convert.ToDecimal(txtTotalIva.Text);
        decimal totRetenido = Convert.ToDecimal(txtTotalRetencion.Text);
        decimal totalFactura = Convert.ToDecimal(txtTotalDocumento.Text);
        decimal aPagar = Convert.ToDecimal(txtApagar.Text);
        decimal abono = 0;
        string sucAfecta = ddlAfectaSucursal.SelectedValue;
        string ccoAfecta = ddlAfectaCcosto.SelectedValue;

        dc.sp_abmInfoTributaria2(accion, 0, ambiente, tipoemision, ruc, claveacceso, coddoc, estab, ptoemi, secuencial, cre_sri, fechaDocumento, fechaCreacion, Username, sucursal, totRetFte, totRetIva, totRetenido, totalFactura, aPagar, abono,sucAfecta,ccoAfecta);
    }

    protected void guardaInfoCompRetencion()
    {
        int mes, ano;
        string accion = "GUARDAR";
        string ruc = txtRuc.Text.Trim();
        string suc = ddlSucursal.SelectedValue.Trim();
        int id_infotributaria = traerId_infotributaria(suc);
        DateTime fechaEmision = Convert.ToDateTime(txtFecha.Text);



        string dirEstablecimiento;



        string contribuyenteEspecial;
        string obligadoContabilidad;
        string tipoIdentificacionSujetoRetenido;
        string razonSocialSujetoRetenido;
        string identificacionSujetoRetenido = txtRuc.Text.Trim();


        string periodoFiscal;


        var lserie = traerEstabPtoEmi(suc);
        dirEstablecimiento = lserie.Item3;

        var lmatriz = traerMatriz(ruc);
        razonSocialSujetoRetenido = lmatriz.Item1;
        contribuyenteEspecial = lmatriz.Item3;
        obligadoContabilidad = lmatriz.Item4;

        tipoIdentificacionSujetoRetenido = ddlReceptor.SelectedValue;

        /*
        if (ruc.Length == 13)
        {
            tipoIdentificacionSujetoRetenido = "04";
        }
        else
        {
            tipoIdentificacionSujetoRetenido = "05";
        }*/


        fechaEmision = Convert.ToDateTime(txtFecha.Text);
        mes = fechaEmision.Month;
        ano = fechaEmision.Year;

        periodoFiscal = llenarCeros(Convert.ToString(mes), '0', 2) + "/" + Convert.ToString(ano);

        dc.sp_abmInfoCompRetencion2(accion, 0, id_infotributaria, fechaEmision, dirEstablecimiento, contribuyenteEspecial, obligadoContabilidad, tipoIdentificacionSujetoRetenido, razonSocialSujetoRetenido, identificacionSujetoRetenido, periodoFiscal);


    }

    protected void guardaImpuestosRet()
    {
        //int id_infotributaria = traerId_infotributaria(suc);
        //char codigo;
        //string codigoRetencion;
        //decimal baseImponible;
        //tring porcentajeRetener;
        //decimal valorRetenido;
        //string SB;
        //string codcble;

        int id_impuestos = 0;
        string suc = ddlSucursal.SelectedValue.Trim();
        int id_infoCompRetencion = traerId_infoCompRetencion(traerId_infotributaria(suc));
        string numDocSustento = txtSerie.Text.Trim() + txtNumDoc.Text.Trim();
        DateTime fechaEmisionDocSustento = Convert.ToDateTime(txtFechDoc.Text);
        DateTime fechaCaducidadDocSustento = Convert.ToDateTime(txtFechCaduc.Text);

        string autorizacion = txtAutorizacion.Text.Trim();
        string codDocSustento = ddlTipDoc.SelectedValue;


        ubicarBienIva(id_impuestos, id_infoCompRetencion, numDocSustento, fechaEmisionDocSustento, autorizacion, codDocSustento, fechaCaducidadDocSustento);
        ubicarBienCero(id_impuestos, id_infoCompRetencion, numDocSustento, fechaEmisionDocSustento, autorizacion, codDocSustento, fechaCaducidadDocSustento);
        ubicarServicioIva(id_impuestos, id_infoCompRetencion, numDocSustento, fechaEmisionDocSustento, autorizacion, codDocSustento, fechaCaducidadDocSustento);
        ubicarServicioCero(id_impuestos, id_infoCompRetencion, numDocSustento, fechaEmisionDocSustento, autorizacion, codDocSustento, fechaCaducidadDocSustento);
    }

    protected void guardaInfoAdicional()
    {
        string accion = "GUARDAR";
        int id_infoAdicional = 0;
        int id_infofactura = 0;
        string suc = ddlSucursal.SelectedValue.Trim();
        int id_infoCompRetencion = traerId_infoCompRetencion(traerId_infotributaria(suc));
        string campoAdicional = txtBien.Text.Trim();

        dc.sp_abmInfoAdicional2(accion, id_infoAdicional, id_infofactura, id_infoCompRetencion, campoAdicional);

    }

    #region UBICAR RETENCIONES

    protected void ubicarBienIva(int id_impuestos, int id_infoCompRetencion, string numDocSustento, DateTime fechaEmisionDocSustento, string autorizacion, string codDocSustento,DateTime fechaCaducidadDocSustento)
    {
        string accion = "GUARDAR";
        char codigo = '0';
        string SB = "B12";
        decimal Bsubtotal = Convert.ToDecimal(txtBsubtotal.Text);
        decimal Bfbase, Bibase;
        string Bfporc, Biporc;
        decimal Bfval, Bival;
        string Bfcod, Bicod;
        string codCble = ddlBiCodCble.SelectedValue;
        if (Bsubtotal > 0)
        {
            Bfporc = txtBiporc.Text.Trim();
            if (Bfporc.Length > 0)
            {
                codigo = '1';
                Bfbase = Convert.ToDecimal(txtBibase.Text);
                Bfval = Convert.ToDecimal(txtBiValor.Text);
                Bfcod = txtBiCodigo.Text;
                dc.sp_abmImpuestosRet2(accion, 0, id_infoCompRetencion, codigo, Bfcod, Bfbase, Bfporc, Bfval, codDocSustento, numDocSustento, fechaEmisionDocSustento, autorizacion, SB, codCble, fechaCaducidadDocSustento);

            }

            Biporc = txtBiporc2.Text.Trim();
            if (Biporc.Length > 0)
            {
                codigo = '2';
                Bibase = Convert.ToDecimal(txtBibase2.Text);
                Bival = Convert.ToDecimal(txtBiValor2.Text);
                Bicod = txtBiCodigo2.Text;
                dc.sp_abmImpuestosRet2(accion, 0, id_infoCompRetencion, codigo, Bicod, Bibase, Biporc, Bival, codDocSustento, numDocSustento, fechaEmisionDocSustento, autorizacion, SB, codCble, fechaCaducidadDocSustento);
            }

        }
    }

    protected void ubicarBienCero(int id_impuestos, int id_infoCompRetencion, string numDocSustento, DateTime fechaEmisionDocSustento, string autorizacion, string codDocSustento, DateTime fechaCaducidadDocSustento)
    {
        string accion = "GUARDAR";
        char codigo = '0';
        string SB = "B0";
        decimal Btarifa0 = Convert.ToDecimal(txtBtarifa0.Text);
        decimal Bfbase;
        string Bfporc;
        decimal Bfval;
        string Bfcod;
        string codCble = ddlB0CodCble.SelectedValue;

        if (Btarifa0 > 0)
        {
            Bfporc = txtB0porc.Text.Trim();
            if (Bfporc.Length > 0)
            {
                codigo = '1';
                Bfbase = Convert.ToDecimal(txtB0base.Text);
                Bfval = Convert.ToDecimal(txtB0Valor.Text);
                Bfcod = txtB0Codigo.Text;
                dc.sp_abmImpuestosRet2(accion, 0, id_infoCompRetencion, codigo, Bfcod, Bfbase, Bfporc, Bfval, codDocSustento, numDocSustento, fechaEmisionDocSustento, autorizacion, SB, codCble, fechaCaducidadDocSustento);
            }
        }
    }

    protected void ubicarServicioIva(int id_impuestos, int id_infoCompRetencion, string numDocSustento, DateTime fechaEmisionDocSustento, string autorizacion, string codDocSustento, DateTime fechaCaducidadDocSustento)
    {
        string accion = "GUARDAR";
        char codigo = '0';
        string SB = "S12";
        decimal Ssubtotal = Convert.ToDecimal(txtSsubtotal.Text);
        decimal Bfbase, Bibase;
        string Sfporc, Siporc;
        decimal Bfval, Bival;
        string Bfcod, Bicod;
        string codCble = ddlSiCodCble.SelectedValue;
        if (Ssubtotal > 0)
        {
            Sfporc = txtSiporc.Text.Trim();
            decimal nSfporc = Convert.ToDecimal(txtSiporc.Text);

            if (nSfporc > 0)
            {
                codigo = '1';
                Bfbase = Convert.ToDecimal(txtSibase.Text);
                Bfval = Convert.ToDecimal(txtSiValor.Text);
                Bfcod = txtSiCodigo.Text;
                dc.sp_abmImpuestosRet2(accion, 0, id_infoCompRetencion, codigo, Bfcod, Bfbase, Sfporc, Bfval, codDocSustento, numDocSustento, fechaEmisionDocSustento, autorizacion, SB, codCble, fechaCaducidadDocSustento);

            }

            Siporc = txtSiporc2.Text.Trim();
            decimal nSiporc = Convert.ToDecimal(txtSiporc2.Text);

            if (nSiporc > 0)
            {
                codigo = '2';
                Bibase = Convert.ToDecimal(txtSibase2.Text);
                Bival = Convert.ToDecimal(txtSiValor2.Text);
                Bicod = txtSiCodigo2.Text;
                dc.sp_abmImpuestosRet2(accion, 0, id_infoCompRetencion, codigo, Bicod, Bibase, Siporc, Bival, codDocSustento, numDocSustento, fechaEmisionDocSustento, autorizacion, SB, codCble, fechaCaducidadDocSustento);
            }

        }
    }

    protected void ubicarServicioCero(int id_impuestos, int id_infoCompRetencion, string numDocSustento, DateTime fechaEmisionDocSustento, string autorizacion, string codDocSustento, DateTime fechaCaducidadDocSustento)
    {
        string accion = "GUARDAR";
        char codigo = '0';
        string SB = "S0";
        decimal Btarifa0 = Convert.ToDecimal(txtStarifa0.Text);
        decimal Bfbase;
        string Bfporc;
        decimal Bfval;
        string Bfcod;
        string codCble = ddlS0CodCble.SelectedValue;

        if (Btarifa0 > 0)
        {
            Bfporc = txtS0porc.Text.Trim();
            if (Bfporc.Length > 0)
            {
                codigo = '1';
                Bfbase = Convert.ToDecimal(txtS0base.Text);
                Bfval = Convert.ToDecimal(txtS0Valor.Text);
                Bfcod = txtS0Codigo.Text;
                dc.sp_abmImpuestosRet2(accion, 0, id_infoCompRetencion, codigo, Bfcod, Bfbase, Bfporc, Bfval, codDocSustento, numDocSustento, fechaEmisionDocSustento, autorizacion, SB, codCble, fechaCaducidadDocSustento);
            }
        }
    }

    #endregion

    #endregion

    #region AUXILIARES DE LLENADO DE TABLAS
    /*AUXILIARES DE LLENADO DE TABLAS*/
    protected Tuple<string, string, string> traerEstabPtoEmi(string lsuc)
    {
        string lestab = string.Empty;
        string lptoemi = string.Empty;
        string ldirEstablecimiento = string.Empty;

        var cSuc = from mSuc in dc.tbl_ruc
                   where mSuc.sucursal == lsuc
                   select new
                   {
                       estab = mSuc.estab,
                       ptoemi = mSuc.ptoemi,
                       dirEstablecimiento = mSuc.dirEstablecimiento
                   };

        if (cSuc.Count() <= 0)
        {
            lestab = string.Empty;
            lptoemi = string.Empty;
            ldirEstablecimiento = string.Empty;
        }
        else
        {
            foreach (var registro in cSuc)
            {
                lestab = registro.estab;
                lptoemi = registro.ptoemi;
                ldirEstablecimiento = registro.dirEstablecimiento;
            }
        }

        return Tuple.Create(lestab, lptoemi, ldirEstablecimiento);
    }

    protected string traerClaveAcceso(string lestab, string lptoemi)
    {
        int ldigitoverificador;
        string lfecha, ldia, lmes, lano, lserie;
        string lruc, lclaveAcceso, lsecuencial;
        DateTime lfechaDocumento, lfechaCreacion;


        ///
        /// CALCULO DE LA CLAVE ACCESO
        ///
        lruc = txtRuc.Text.Trim();
        lfechaDocumento = Convert.ToDateTime(txtFecha.Text.Trim());
        lfechaCreacion = DateTime.Now;
        ldia = txtFecha.Text.Trim().Substring(0, 2);
        lmes = txtFecha.Text.Trim().Substring(3, 2);
        lano = txtFecha.Text.Trim().Substring(6, 4);
        lfecha = ldia + lmes + lano;
        lserie = lestab + lptoemi;
        lsecuencial = txtNumRet.Text.Trim();
        lclaveAcceso = lfecha + "07" + lruc + "1" + lserie + lsecuencial + "13245678" + "1";
        ldigitoverificador = sumaDigitos(invertirCadena(lclaveAcceso));
        return lclaveAcceso;
    }

    private string invertirCadena(string lcadena)
    {
        int i;
        string cadenaInvertida = "";
        for (i = cadenaInvertida.Length; i > 0; i--)
        {
            cadenaInvertida = cadenaInvertida + lcadena.Substring(i, 1);
        }

        return cadenaInvertida;
    }

    private int sumaDigitos(string lcadena)
    {
        int i, longitudCadena, temporal, cantidadTotal = 0;
        int pivote = 2;
        int b = 2;
        longitudCadena = lcadena.Length;

        for (i = 1; i <= longitudCadena; i++)
        {
            if (pivote == 8)
            {
                pivote = 2;
            }
            temporal = Convert.ToInt16(lcadena.Substring(i, b));
            b++;
            temporal = temporal * pivote;
            pivote = pivote + 1;
            cantidadTotal = cantidadTotal + temporal;
        }

        cantidadTotal = 11 - cantidadTotal % 11;

        switch (cantidadTotal)
        {
            case 10:
                cantidadTotal = 1;
                break;
            case 11:
                cantidadTotal = 0;
                break;
        }

        return cantidadTotal;
    }

    private int traerId_infotributaria(string suc)
    {
        string lcoddoc = "07";
        string lestab = string.Empty;
        string lptoemi = string.Empty;
        string lsecuencial = txtNumRet.Text.Trim();
        int Id_infotributaria = 0;

        var lptoestab = traerEstabPtoEmi(suc);
        lestab = lptoestab.Item1;
        lptoemi = lptoestab.Item2;

        var cInfo = from mInfo in dc.tbl_infotributaria
                    where mInfo.estab == lestab
                        && mInfo.ptoemi == lptoemi
                        && mInfo.coddoc == lcoddoc
                        && mInfo.secuencial == lsecuencial
                    select new
                    {
                        id_infotributaria = mInfo.id_infotributaria
                    };

        if (cInfo.Count() <= 0)
        {
            Id_infotributaria = 0;
        }
        else
        {
            foreach (var registro in cInfo)
            {
                Id_infotributaria = registro.id_infotributaria;
            }
        }
        return Id_infotributaria;

    }

    private int traerId_infoCompRetencion(int id_infotributaria)
    {
        int Id_infoCompRetencion = 0;

        var cComp = from mComp in dc.tbl_infoCompRetencion
                    where mComp.id_infotributaria == id_infotributaria
                    select new
                    {
                        id_infoCompRetencion = mComp.id_infoCompRetencion
                    };

        if (cComp.Count() <= 0)
        {
            Id_infoCompRetencion = 0;
        }
        else
        {
            foreach (var registro in cComp)
            {
                Id_infoCompRetencion = registro.id_infoCompRetencion;
            }
        }


        return Id_infoCompRetencion;
    }

    protected Tuple<string, string, string, string, string, string> traerMatriz(string lruc)
    {
        string razonsocial = string.Empty;
        string dirMatriz = string.Empty;
        string contribuyenteEspecial = string.Empty;
        string obligadoContabilidad = string.Empty;
        string e_mail = string.Empty;
        string telefono = string.Empty;

        var cMatriz = from mMatriz in dc.tbl_matriz
                      where mMatriz.ruc == lruc
                      select new
                      {
                          razonsocial = mMatriz.razonsocial,
                          dirMatriz = mMatriz.dirMatriz,
                          contribuyenteEspecial = mMatriz.contribuyenteEspecial,
                          obligadoContabilidad = mMatriz.obligadoContabilidad,
                          e_mail = mMatriz.e_mail,
                          telefono = mMatriz.telefono

                      };

        if (cMatriz.Count() <= 0)
        {
            razonsocial = string.Empty;
            dirMatriz = string.Empty;
            contribuyenteEspecial = string.Empty;
            obligadoContabilidad = string.Empty;
            e_mail = string.Empty;
            telefono = string.Empty;
        }
        else
        {
            foreach (var registro in cMatriz)
            {
                razonsocial = registro.razonsocial;
                dirMatriz = registro.dirMatriz;
                contribuyenteEspecial = registro.contribuyenteEspecial;
                obligadoContabilidad = registro.obligadoContabilidad;
                e_mail = registro.e_mail;
                telefono = registro.telefono;
            }
        }

        return Tuple.Create(razonsocial, dirMatriz, contribuyenteEspecial, obligadoContabilidad, e_mail, telefono);
    }

    protected bool existeRetencion(string lestab, string lptoemi, string lnumRet)
    {
        string coddoc = "07";
        bool pasa = true;

        var cRet = from mRret in dc.tbl_infotributaria
                   where mRret.coddoc == coddoc
                        && mRret.estab == lestab
                        && mRret.ptoemi == lptoemi
                        && mRret.secuencial == lnumRet
                   select new
                   {
                       secuencial = mRret.secuencial
                   };

        if (cRet.Count() <= 0)
        {
            pasa = false;
        }
        else
        {
            pasa = true;
        }

        return pasa;

    }
    protected bool existeDocumento(string ltipoDoc, string lserie, string lnumDoc, string lruc)
    {
        bool pasa = true;
        string numDocSustento = lserie + lnumDoc;
        var cDoc = from TinfTri in dc.tbl_infotributaria
                   from TinfRet in dc.tbl_infoCompRetencion
                   from TimpRet in dc.tbl_impuestosRet
                   where TinfTri.id_infotributaria == TinfRet.id_infotributaria
                        && TinfRet.id_infoCompRetencion == TimpRet.id_infoCompRetencion
                        && TinfRet.identificacionSujetoRetenido == lruc
                        && TimpRet.numDocSustento == numDocSustento
                   select new { numDocSustento = TimpRet.numDocSustento };


        if (cDoc.Count() <= 0)
        {
            pasa = false;
        }
        else
        {
            pasa = true;
        }

        return pasa;
    }

    protected bool existeDocumento(string ltipoDoc, string lserie, string lnumDoc, string lruc, string lautorizacion)
    {
        bool pasa = true;
        string numDocSustento = lserie + lnumDoc;
        var cDoc = from TinfTri in dc.tbl_infotributaria
                   from TinfRet in dc.tbl_infoCompRetencion
                   from TimpRet in dc.tbl_impuestosRet
                   where TinfTri.id_infotributaria == TinfRet.id_infotributaria
                        && TinfRet.id_infoCompRetencion == TimpRet.id_infoCompRetencion
                        && TinfRet.identificacionSujetoRetenido == lruc
                        && TimpRet.numDocSustento == numDocSustento
                        && TimpRet.autorizacion == lautorizacion
                   select new { numDocSustento = TimpRet.numDocSustento };


        if (cDoc.Count() <= 0)
        {
            pasa = false;
        }
        else
        {
            pasa = true;
        }

        return pasa;
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void llenarCeros()
    {
        string conceros;

        conceros = Convert.ToString(txtNumRet.Text);
        conceros = conceros.PadLeft(9, '0');
        txtNumRet.Text = conceros;
    }

    #endregion

    #region ENVIAR AL SRI

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        int pid_infotributaria = 0, lgrabado;
        string ltipoDocumento;

        ltipoDocumento = ddlTipDoc.SelectedValue;

        if (new[] { "02", "04", "05" }.Contains(ltipoDocumento))
        {
            lblMensaje.Text = "Documento sin retención solo debe ser grabado";
        }
        else
        {
            pid_infotributaria = traerIdInfoTributaria();

            //lgrabado = cosultarRetencion(pid_infotributaria);
            lgrabado = grabarRetencion(pid_infotributaria);

            if (lgrabado == 0)
            {
                lblMensaje.Text = "Se ha enviado retención al SRI";
                ListarRetencion(pid_infotributaria);
                desactivarBotones();
            }
        }
    }
    
    /// <summary>
    /// GRABA LA RETENCION DIRECTAMENTE EN LAS CABCERA Y DETALLE DEL DATACORE SIN WEBSERVICES
    /// </summary>
    /// <param name="pId_InfoTributaria"></param>
    /// <returns></returns>
    #region GRABAR CABECERA Y DETALLE  DE LA RETENCION

    protected int grabarRetencion(int pId_InfoTributaria)
    {
        int kont = 0, nGrabados = -1;
        string sucursal = ddlSucursal.SelectedValue;
        string ldi1 = string.Empty; ;
        string lte1 = string.Empty; ;
        string le_mail = string.Empty; ;
        string Accion = "AGREGAR";
        string lcadena = "0";
        string cadena1 = "0";
        string cadena2 = "0";
        string cadena3 = "0";
        string cadena4 = "0";
        string cadena5 = "0";
        string cadena6 = "0";
        string cadena7 = "";

       
        string lambiente = string.Empty;
        string ltipoemision = string.Empty;
        string lrazonsocial = string.Empty;
        string lnombrecomercial = string.Empty;
        string lruc = string.Empty;
        string pruc = txtRuc.Text.Trim();
        string lsujetoRuc = string.Empty;
        string lclaveacceso = string.Empty;
        string lcoddoc = string.Empty;
        string lestab = string.Empty;
        string lptoemi = string.Empty;
        string lsecuencial = string.Empty;
        string ldirMatriz = string.Empty;
        string lfechaemision = string.Empty;
        string ldirestablecimiento = string.Empty;
        string lcontribuyenteEspecial = string.Empty;
        string lobligadoContabilidad = string.Empty;
        string ltipoIdentificacionSujetoRetenido = string.Empty;
        string lrazonSocialSujetoRetenido = string.Empty;
        string lidentificacionSujetoRetenido = string.Empty;
        string lperiodoFiscal = string.Empty;

        double lcodigo;
        string lcodigoRetencion = string.Empty;
        double lbaseImponible = 0;
        double lporcentajeRetener = 0;
        double lvalorRetenido = 0;
        double lcodDocSustento = 0;
        string lnumDocSustento = string.Empty;
        DateTime lfechaEmisionDocSustento = DateTime.Today;



        /*VARIABLES PARA CADA POSIBILIDAD DE RETENCION SON 6*/
        double lcodigo1 = 0;
        string lcodigoRetencion1 = string.Empty;
        double lbaseImponible1 = 0;
        double lporcentajeRetener1 = 0;
        double lvalorRetenido1 = 0;
        double lcodDocSustento1 = 0;
        string lnumDocSustento1 = string.Empty;
        DateTime lfechaEmisionDocSustento1 = DateTime.Today;

        double lcodigo2 = 0;
        string lcodigoRetencion2 = string.Empty;
        double lbaseImponible2 = 0;
        double lporcentajeRetener2 = 0;
        double lvalorRetenido2 = 0;
        double lcodDocSustento2 = 0;
        string lnumDocSustento2 = string.Empty;
        DateTime lfechaEmisionDocSustento2 = DateTime.Today;


        double lcodigo3 = 0;
        string lcodigoRetencion3 = string.Empty;
        double lbaseImponible3 = 0;
        double lporcentajeRetener3 = 0;
        double lvalorRetenido3 = 0;
        double lcodDocSustento3 = 0;
        string lnumDocSustento3 = string.Empty;
        DateTime lfechaEmisionDocSustento3 = DateTime.Today;

        double lcodigo4 = 0;
        string lcodigoRetencion4 = string.Empty;
        double lbaseImponible4 = 0;
        double lporcentajeRetener4 = 0;
        double lvalorRetenido4 = 0;
        double lcodDocSustento4 = 0;
        string lnumDocSustento4 = string.Empty;
        DateTime lfechaEmisionDocSustento4 = DateTime.Today;

        double lcodigo5 = 0;
        string lcodigoRetencion5 = string.Empty;
        double lbaseImponible5 = 0;
        double lporcentajeRetener5 = 0;
        double lvalorRetenido5 = 0;
        double lcodDocSustento5 = 0;
        string lnumDocSustento5 = string.Empty;
        DateTime lfechaEmisionDocSustento5 = DateTime.Today;

        double lcodigo6 = 0;
        string lcodigoRetencion6 = string.Empty;
        double lbaseImponible6 = 0;
        double lporcentajeRetener6 = 0;
        double lvalorRetenido6 = 0;
        double lcodDocSustento6 = 0;
        string lnumDocSustento6 = string.Empty;
        DateTime lfechaEmisionDocSustento6 = DateTime.Today;


        /************************************************************/


        var consultaRC = from a in dc.tbl_matriz
                         from b in dc.tbl_ruc
                         from c in dc.tbl_infotributaria
                         from d in dc.tbl_infoCompRetencion
                         where a.ruc == b.ruc
                            && b.ruc == c.ruc
                            && b.coddoc == c.coddoc
                            && b.estab == c.estab
                            && b.ptoemi == c.ptoemi
                            && c.id_infotributaria == d.id_infotributaria
                            && c.id_infotributaria == pId_InfoTributaria
                         select new
                         {
                             ambiente = c.ambiente,
                             tipoemision = c.tipoemision,
                             razonsocial = b.razonsocial,
                             nombrecomercial = b.nombreComercial,
                             ruc = b.ruc,
                             claveacceso = c.claveacceso,
                             coddoc = b.coddoc,
                             estab = b.estab,
                             ptoemi = b.ptoemi,
                             secuencial = c.secuencial,
                             dirMatriz = a.dirMatriz,
                             fechaemision = d.fechaEmision,
                             direstablecimiento = b.dirEstablecimiento,
                             contribuyenteEspecial = a.contribuyenteEspecial,
                             obligadoContabilidad = a.obligadoContabilidad,
                             tipoIdentificacionSujetoRetenido = d.tipoIdentificacionSujetoRetenido,
                             razonSocialSujetoRetenido = d.razonSocialSujetoRetenido,
                             identificacionSujetoRetenido = d.identificacionSujetoRetenido,
                             periodoFiscal = d.periodoFiscal
                         };




        /****************/

        var consultaRD = dc.sp_TraerRetencion("",pId_InfoTributaria);
            /*from a in dc.tbl_matriz
                         from b in dc.tbl_ruc
                         from c in dc.tbl_infotributaria
                         from d in dc.tbl_infoCompRetencion
                         from e in dc.tbl_impuestosRet
                         where a.ruc == b.ruc
                           && b.ruc == c.ruc
                           && b.coddoc == c.coddoc
                           && b.estab == c.estab
                           && b.ptoemi == c.ptoemi
                           && c.id_infotributaria == d.id_infotributaria
                           && d.id_infoCompRetencion == e.id_infoCompRetencion
                           && c.id_infotributaria == pId_InfoTributaria
                         select new
                         {
                             cre_sri = c.cre_sri,
                             sujetoRuc = d.identificacionSujetoRetenido,
                             codigo = Convert.ToChar(e.codigo),
                             codigoRetencion = e.codigoRetencion,
                             baseImponible = e.baseImponible,
                             porcentajeRetener = e.porcentajeRetener,
                             valorRetenido = e.valorRetenido,
                             codDocSustento = e.codDocSustento,
                             numDocSustento = e.numDocSustento,
                             fechaEmisionDocSustento = e.fechaEmisionDocSustento
                         };*/
        /*********************/
        /*VERIFICA DATOS */
        /*********************/

        if (consultaRC.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaRC)
            {
                lambiente = Convert.ToString(registro.ambiente).Trim();
                ltipoemision = Convert.ToString(registro.tipoemision).Trim();
                lrazonsocial = registro.razonsocial.Trim();
                lnombrecomercial = registro.nombrecomercial.Trim();
                lruc = registro.ruc.Trim();
                lclaveacceso = registro.claveacceso.Trim();
                lcoddoc = registro.coddoc.Trim();
                lestab = registro.estab.Trim();
                lptoemi = registro.ptoemi.Trim();
                lsecuencial = registro.secuencial.Trim();
                ldirMatriz = registro.dirMatriz.Trim();
                lfechaemision = Convert.ToString(registro.fechaemision).Substring(0, 10);
                ldirestablecimiento = registro.direstablecimiento.Trim();
                lcontribuyenteEspecial = registro.contribuyenteEspecial.Trim();
                lobligadoContabilidad = registro.obligadoContabilidad.Trim();
                ltipoIdentificacionSujetoRetenido = registro.tipoIdentificacionSujetoRetenido.Trim();
                lrazonSocialSujetoRetenido = registro.razonSocialSujetoRetenido.Trim();
                lidentificacionSujetoRetenido = registro.identificacionSujetoRetenido.Trim();
                lperiodoFiscal = registro.periodoFiscal.Trim();
            }
        }

    
        
        /*   if (consultaRD.Count() == 0)
        {


        }
        else
        {*/


        foreach (var registro in consultaRD)
        {
            lcadena = "";
            lsujetoRuc = registro.identificacionSujetoRetenido;
            lcodigo = Convert.ToDouble(registro.codigo);
            lcodigoRetencion = registro.codigoRetencion;
            lbaseImponible = Convert.ToDouble(registro.baseImponible);
            lporcentajeRetener = Convert.ToDouble(registro.porcentajeRetener);
            lvalorRetenido = Convert.ToDouble(registro.valorRetenido);
            lcodDocSustento = Convert.ToDouble(registro.codDocSustento);
            lnumDocSustento = registro.numDocSustento;
            lfechaEmisionDocSustento = registro.fechaEmisionDocSustento;
            lcadena = "*z%" + "*y%" +
                        "*a%" + lcodigo + "*/a%" +
                        "*b%" + lcodigoRetencion + "*/b%" +
                        "*c%" + lbaseImponible + "*/c%" +
                        "*d%" + lporcentajeRetener + "*/d%" +
                        "*e%" + lvalorRetenido + "*/e%" +
                        "*f%" + lcodDocSustento + "*/f%" +
                        "*g%" + lnumDocSustento + "*/g%" +
                        "*h%" + lfechaEmisionDocSustento + "*/h%" +
                        "*/y%" + "*/z%";

            kont++;

            if (kont == 1)
            {
                cadena1 = lcadena;
                lcodigo1 = Convert.ToDouble(registro.codigo);
                lcodigoRetencion1 = registro.codigoRetencion.Trim();
                lbaseImponible1 = Convert.ToDouble(registro.baseImponible);
                lporcentajeRetener1 = Convert.ToDouble(registro.porcentajeRetener);
                lvalorRetenido1 = Convert.ToDouble(registro.valorRetenido);
                lcodDocSustento1 = Convert.ToDouble(registro.codDocSustento);
                lnumDocSustento1 = registro.numDocSustento;
                lfechaEmisionDocSustento1 = registro.fechaEmisionDocSustento;


            }
            if (kont == 2)
            {
                cadena2 = lcadena;
                lcodigo2 = Convert.ToDouble(registro.codigo);
                lcodigoRetencion2 = registro.codigoRetencion.Trim();
                lbaseImponible2 = Convert.ToDouble(registro.baseImponible);
                lporcentajeRetener2 = Convert.ToDouble(registro.porcentajeRetener);
                lvalorRetenido2 = Convert.ToDouble(registro.valorRetenido);
                lcodDocSustento2 = Convert.ToDouble(registro.codDocSustento);
                lnumDocSustento2 = registro.numDocSustento;
                lfechaEmisionDocSustento2 = registro.fechaEmisionDocSustento;


            }
            if (kont == 3)
            {
                cadena3 = lcadena;
                lcodigo3 = Convert.ToDouble(registro.codigo);
                lcodigoRetencion3 = registro.codigoRetencion.Trim();
                lbaseImponible3 = Convert.ToDouble(registro.baseImponible);
                lporcentajeRetener3 = Convert.ToDouble(registro.porcentajeRetener);
                lvalorRetenido3 = Convert.ToDouble(registro.valorRetenido);
                lcodDocSustento3 = Convert.ToDouble(registro.codDocSustento);
                lnumDocSustento3 = registro.numDocSustento;
                lfechaEmisionDocSustento3 = registro.fechaEmisionDocSustento;
            }
            if (kont == 4)
            {
                cadena4 = lcadena;
                lcodigo4 = Convert.ToDouble(registro.codigo);
                lcodigoRetencion4 = registro.codigoRetencion.Trim();
                lbaseImponible4 = Convert.ToDouble(registro.baseImponible);
                lporcentajeRetener4 = Convert.ToDouble(registro.porcentajeRetener);
                lvalorRetenido4 = Convert.ToDouble(registro.valorRetenido);
                lcodDocSustento4 = Convert.ToDouble(registro.codDocSustento);
                lnumDocSustento4 = registro.numDocSustento;
                lfechaEmisionDocSustento4 = registro.fechaEmisionDocSustento;
            }
            if (kont == 5)
            {
                cadena5 = lcadena;
                lcodigo5 = Convert.ToDouble(registro.codigo);
                lcodigoRetencion5 = registro.codigoRetencion.Trim();
                lbaseImponible5 = Convert.ToDouble(registro.baseImponible);
                lporcentajeRetener5 = Convert.ToDouble(registro.porcentajeRetener);
                lvalorRetenido5 = Convert.ToDouble(registro.valorRetenido);
                lcodDocSustento5 = Convert.ToDouble(registro.codDocSustento);
                lnumDocSustento5 = registro.numDocSustento;
                lfechaEmisionDocSustento5 = registro.fechaEmisionDocSustento;
            }
            if (kont == 6)
            {
                cadena6 = lcadena;
                lcodigo6 = Convert.ToDouble(registro.codigo);
                lcodigoRetencion6 = registro.codigoRetencion.Trim();
                lbaseImponible6 = Convert.ToDouble(registro.baseImponible);
                lporcentajeRetener6 = Convert.ToDouble(registro.porcentajeRetener);
                lvalorRetenido6 = Convert.ToDouble(registro.valorRetenido);
                lcodDocSustento6 = Convert.ToDouble(registro.codDocSustento);
                lnumDocSustento6 = registro.numDocSustento;
                lfechaEmisionDocSustento6 = registro.fechaEmisionDocSustento;
            }
        }
          //  }
        //}
        /**********CONSULTA DATOS DEL SUJETO RETENIDO*******/
        var consultaP = from provee in dc.tbl_matriz
                        where provee.ruc == lsujetoRuc
                        select new { provee.dirMatriz, provee.e_mail, provee.telefono };
        if (consultaP.Count() == 0)
        {

        }
        else
        {
            foreach (var registro in consultaP)
            {
                ldi1 = registro.dirMatriz.Trim();
                lte1 = registro.telefono.Trim();
                le_mail = registro.e_mail.Trim();
               
                
                cadena7 = "*z%" + "*y%" +
                    "*a%" + ldi1 + "*/a%" +
                    "*b%" + lte1 + "*/b%" +
                    "*c%" + le_mail + "*/c%" +
                    "*d%" + "" + "*/d%" +
                    "*e%" + "" + "*/e%" +
                    "*f%" + "" + "*/f%" +
                    "*g%" + "" + "*/g%" +
                    "*/y%" + "*/z%";
            }
        }


        if (lclaveacceso == null)
        {
            lclaveacceso = "";
        }

        try
        {
            
            /*
            wsSRI.WebService1SoapClient servicio = new wsSRI.WebService1SoapClient();

            servicio.obNomarticulo(lambiente, ltipoemision, lrazonsocial, lnombrecomercial, lruc, lclaveacceso, lcoddoc, lestab, lptoemi, lsecuencial, ldirMatriz,
                lfechaemision, ldirestablecimiento, lcontribuyenteEspecial, lobligadoContabilidad, ltipoIdentificacionSujetoRetenido, lrazonSocialSujetoRetenido, lidentificacionSujetoRetenido,
                lperiodoFiscal, cadena1, cadena2, cadena3, cadena4, cadena5, cadena6, cadena7);*/

               /*GUARDA CABECERA DE LA RETENCION DATACORE*/
                dc.sp_abmRetencionCabecera(Accion,
                                            0,
                                            lambiente,
                                            ltipoemision,
                                            lrazonsocial,
                                            lnombrecomercial,
                                            lruc,
                                            lclaveacceso,
                                            lcoddoc,
                                            lestab,
                                            lptoemi,
                                            lsecuencial,
                                            ldirMatriz,
                                            Convert.ToDateTime(lfechaemision),
                                            ldirestablecimiento,
                                            lcontribuyenteEspecial,
                                            lobligadoContabilidad,
                                            ltipoIdentificacionSujetoRetenido,
                                            lrazonSocialSujetoRetenido,
                                            lidentificacionSujetoRetenido,
                                            lperiodoFiscal,
                                            ldi1,
                                            lte1,
                                            le_mail,
                                            "C",
                                            "CREADO",
                                            "",
                                            "",
                                            DateTime.Today,
                                            sucursal);

            /*GRABA EL DETALLE DE LAS RETENCIONES SI NO ESTA VACÍO*/

                double cre_id = traerIdCabeceraretencion();

                if (lcodigo1 > 0)
                {
                    dc.sp_abmRetencionDetalle(Accion, 0, Convert.ToDecimal(lcodigo1), lcodigoRetencion1, Convert.ToDecimal(lbaseImponible1), Convert.ToDecimal(lporcentajeRetener1), Convert.ToDecimal(lvalorRetenido1), Convert.ToDecimal(lcodDocSustento1), lnumDocSustento1, lfechaEmisionDocSustento1, Convert.ToDecimal(cre_id));
                }

                if (lcodigo2 > 0)
                {
                    dc.sp_abmRetencionDetalle(Accion, 0, Convert.ToDecimal(lcodigo2), lcodigoRetencion2, Convert.ToDecimal(lbaseImponible2), Convert.ToDecimal(lporcentajeRetener2), Convert.ToDecimal(lvalorRetenido2), Convert.ToDecimal(lcodDocSustento2), lnumDocSustento2, lfechaEmisionDocSustento2, Convert.ToDecimal(cre_id));
                }
                if (lcodigo3 > 0)
                {
                    dc.sp_abmRetencionDetalle(Accion, 0, Convert.ToDecimal(lcodigo3), lcodigoRetencion3, Convert.ToDecimal(lbaseImponible3), Convert.ToDecimal(lporcentajeRetener3), Convert.ToDecimal(lvalorRetenido3), Convert.ToDecimal(lcodDocSustento3), lnumDocSustento3, lfechaEmisionDocSustento3, Convert.ToDecimal(cre_id));
                }
                if (lcodigo4 > 0)
                {
                    dc.sp_abmRetencionDetalle(Accion, 0, Convert.ToDecimal(lcodigo4), lcodigoRetencion4, Convert.ToDecimal(lbaseImponible4), Convert.ToDecimal(lporcentajeRetener4), Convert.ToDecimal(lvalorRetenido4), Convert.ToDecimal(lcodDocSustento4), lnumDocSustento4, lfechaEmisionDocSustento4, Convert.ToDecimal(cre_id));
                }
                if (lcodigo5 > 0)
                {
                    dc.sp_abmRetencionDetalle(Accion, 0, Convert.ToDecimal(lcodigo5), lcodigoRetencion1, Convert.ToDecimal(lbaseImponible5), Convert.ToDecimal(lporcentajeRetener5), Convert.ToDecimal(lvalorRetenido5), Convert.ToDecimal(lcodDocSustento5), lnumDocSustento5, lfechaEmisionDocSustento5, Convert.ToDecimal(cre_id));
                }
                if (lcodigo6 > 0)
                {
                    dc.sp_abmRetencionDetalle(Accion, 0, Convert.ToDecimal(lcodigo6), lcodigoRetencion6, Convert.ToDecimal(lbaseImponible6), Convert.ToDecimal(lporcentajeRetener6), Convert.ToDecimal(lvalorRetenido6), Convert.ToDecimal(lcodDocSustento6), lnumDocSustento6, lfechaEmisionDocSustento6, Convert.ToDecimal(cre_id));
                }




            /*GUARDA OBSERVACION QUE EMITE EL SRI*/

            try
            {
                dc.sp_observacionSRI(lruc, lcoddoc, lestab, lptoemi, lsecuencial, "");
                nGrabados = 0;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                nGrabados = -1;
            }
            finally
            {

            }
            /*******************************************/

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            nGrabados = -1;
        }

        finally
        {
        }

        /*wsComprobantesSRI.WSComprobantesElectronicos retencion = new wsComprobantesSRI.WSComprobantesElectronicosClient();
        

         wsComprobantesSRI.comprobantesRetencionRequest comprobantes  = new  wsComprobantesSRI.comprobantesRetencionRequest(lambiente, ltipoemision, lrazonsocial, lnombrecomercial, lruc, 
             lclaveacceso, lcoddoc, lestab, lptoemi, lsecuencial, ldirMatriz, lfechaemision, ldirestablecimiento, lcontribuyenteEspecial, lobligadoContabilidad, ltipoIdentificacionSujetoRetenido, 
             lrazonSocialSujetoRetenido, lidentificacionSujetoRetenido, lperiodoFiscal, direccion, telefono, mail, codigo, codigoRetencion, baseImponible, porcentajeRetener, valorRetenido, codDocSustento, numDocSustento, fechaEmisionDocSustento)



       /// ds.Tables(0).Rows.Add(obj.comprobantesRetencion(comprobantes).return)*/

        return nGrabados;
    }

    
    #endregion

    protected int cosultarRetencion(int pId_InfoTributaria)
    {
        int kont = 0, nGrabados = -1;
        string ldi1, lte1, le_mail;
        string lambiente, ltipoemision,
                lcodigo, lcadena, cadena1, cadena2, cadena3, cadena4, cadena5, cadena6, cadena7,
                lrazonsocial,
                lnombrecomercial,
                lruc,
                pruc,
                lsujetoRuc,
                lclaveacceso,
                lcoddoc,
                lestab,
                lptoemi,
                lsecuencial,
                ldirMatriz,
                lfechaemision,
                ldirestablecimiento,
                lcontribuyenteEspecial,
                lobligadoContabilidad,
                ltipoIdentificacionSujetoRetenido,
                lrazonSocialSujetoRetenido,
                lidentificacionSujetoRetenido,
                lperiodoFiscal,
                lcodigoRetencion,
                lbaseImponible,
                lporcentajeRetener,
                lvalorRetenido,
                lcodDocSustento,
                lnumDocSustento,
                lfechaEmisionDocSustento;



        lcadena = "";
        lcadena = "0";
        cadena1 = "0";
        cadena2 = "0";
        cadena3 = "0";
        cadena4 = "0";
        cadena5 = "0";
        cadena6 = "0";
        cadena7 = "";


        lambiente = "";
        ltipoemision = "";
        lrazonsocial = "";
        lnombrecomercial = "";
        lruc = "";
        pruc = txtRuc.Text.Trim();
        lsujetoRuc = "";
        lclaveacceso = "";
        lcoddoc = "";
        lestab = "";
        lptoemi = "";
        lsecuencial = "";
        ldirMatriz = "";
        lfechaemision = "";
        ldirestablecimiento = "";
        lcontribuyenteEspecial = "";
        lobligadoContabilidad = "";
        ltipoIdentificacionSujetoRetenido = "";
        lrazonSocialSujetoRetenido = "";
        lidentificacionSujetoRetenido = "";
        lperiodoFiscal = "";
        lcodigoRetencion = "";
        lbaseImponible = "";
        lporcentajeRetener = "";
        lvalorRetenido = "";
        lcodDocSustento = "";
        lnumDocSustento = "";
        lfechaEmisionDocSustento = "";

        var consultaRC = from a in dc.tbl_matriz
                         from b in dc.tbl_ruc
                         from c in dc.tbl_infotributaria
                         from d in dc.tbl_infoCompRetencion
                         where a.ruc == b.ruc
                            && b.ruc == c.ruc
                            && b.coddoc == c.coddoc
                            && b.estab == c.estab
                            && b.ptoemi == c.ptoemi
                            && c.id_infotributaria == d.id_infotributaria
                            && c.id_infotributaria == pId_InfoTributaria
                         select new
                         {
                             ambiente = c.ambiente,
                             tipoemision = c.tipoemision,
                             razonsocial = b.razonsocial,
                             nombrecomercial = b.nombreComercial,
                             ruc = b.ruc,
                             claveacceso = c.claveacceso,
                             coddoc = b.coddoc,
                             estab = b.estab,
                             ptoemi = b.ptoemi,
                             secuencial = c.secuencial,
                             dirMatriz = a.dirMatriz,
                             fechaemision = d.fechaEmision,
                             direstablecimiento = b.dirEstablecimiento,
                             contribuyenteEspecial = a.contribuyenteEspecial,
                             obligadoContabilidad = a.obligadoContabilidad,
                             tipoIdentificacionSujetoRetenido = d.tipoIdentificacionSujetoRetenido,
                             razonSocialSujetoRetenido = d.razonSocialSujetoRetenido,
                             identificacionSujetoRetenido = d.identificacionSujetoRetenido,
                             periodoFiscal = d.periodoFiscal
                         };




        /****************/

        var consultaRD = from a in dc.tbl_matriz
                         from b in dc.tbl_ruc
                         from c in dc.tbl_infotributaria
                         from d in dc.tbl_infoCompRetencion
                         from e in dc.tbl_impuestosRet
                         where a.ruc == b.ruc
                           && b.ruc == c.ruc
                           && b.coddoc == c.coddoc
                           && b.estab == c.estab
                           && b.ptoemi == c.ptoemi
                           && c.id_infotributaria == d.id_infotributaria
                           && d.id_infoCompRetencion == e.id_infoCompRetencion
                           && c.id_infotributaria == pId_InfoTributaria
                         select new
                         {
                             cre_sri = c.cre_sri,
                             sujetoRuc = d.identificacionSujetoRetenido,
                             codigo = e.codigo,
                             codigoRetencion = e.codigoRetencion,
                             baseImponible = e.baseImponible,
                             porcentajeRetener = e.porcentajeRetener,
                             valorRetenido = e.valorRetenido,
                             codDocSustento = e.codDocSustento,
                             numDocSustento = e.numDocSustento,
                             fechaEmisionDocSustento = e.fechaEmisionDocSustento
                         };
        /*********************/
        /*VERIFICA DATOS */
        /*********************/

        if (consultaRC.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaRC)
            {
                lambiente = Convert.ToString(registro.ambiente).Trim();
                ltipoemision = Convert.ToString(registro.tipoemision).Trim();
                lrazonsocial = registro.razonsocial.Trim();
                lnombrecomercial = registro.nombrecomercial.Trim();
                lruc = registro.ruc.Trim();
                lclaveacceso = registro.claveacceso.Trim();
                lcoddoc = registro.coddoc.Trim();
                lestab = registro.estab.Trim();
                lptoemi = registro.ptoemi.Trim();
                lsecuencial = registro.secuencial.Trim();
                ldirMatriz = registro.dirMatriz.Trim();
                lfechaemision = Convert.ToString(registro.fechaemision).Substring(0, 10);
                ldirestablecimiento = registro.direstablecimiento.Trim();
                lcontribuyenteEspecial = registro.contribuyenteEspecial.Trim();
                lobligadoContabilidad = registro.obligadoContabilidad.Trim();
                ltipoIdentificacionSujetoRetenido = registro.tipoIdentificacionSujetoRetenido.Trim();
                lrazonSocialSujetoRetenido = registro.razonSocialSujetoRetenido.Trim();
                lidentificacionSujetoRetenido = registro.identificacionSujetoRetenido.Trim();
                lperiodoFiscal = registro.periodoFiscal.Trim();
            }
        }

        if (consultaRD.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaRD)
            {
                lcadena = "";
                lsujetoRuc = Convert.ToString(registro.sujetoRuc).Trim();
                lcodigo = Convert.ToString(registro.codigo);
                lcodigoRetencion = registro.codigoRetencion.Trim();
                lbaseImponible = Convert.ToString(registro.baseImponible).Replace(',', '.').Trim();
                lporcentajeRetener = registro.porcentajeRetener.Replace(',', '.').Trim();
                lvalorRetenido = Convert.ToString(registro.valorRetenido).Replace(',', '.').Trim();
                lcodDocSustento = registro.codDocSustento;
                lnumDocSustento = registro.numDocSustento;
                lfechaEmisionDocSustento = Convert.ToString(registro.fechaEmisionDocSustento).Substring(0, 10);
                lcadena = "*z%" + "*y%" +
                            "*a%" + lcodigo + "*/a%" +
                            "*b%" + lcodigoRetencion + "*/b%" +
                            "*c%" + lbaseImponible + "*/c%" +
                            "*d%" + lporcentajeRetener + "*/d%" +
                            "*e%" + lvalorRetenido + "*/e%" +
                            "*f%" + lcodDocSustento + "*/f%" +
                            "*g%" + lnumDocSustento + "*/g%" +
                            "*h%" + lfechaEmisionDocSustento + "*/h%" +
                            "*/y%" + "*/z%";

                kont++;
                if (kont == 1)
                {
                    cadena1 = lcadena;
                }
                if (kont == 2)
                {
                    cadena2 = lcadena;
                }
                if (kont == 3)
                {
                    cadena3 = lcadena;
                }
                if (kont == 4)
                {
                    cadena4 = lcadena;
                }
                if (kont == 5)
                {
                    cadena5 = lcadena;
                }
                if (kont == 6)
                {
                    cadena6 = lcadena;
                }
            }
        }
        /**********CONSULTA DATOS DEL SUJETO RETENIDO*******/
        var consultaP = from provee in dc.tbl_matriz
                        where provee.ruc == lsujetoRuc
                        select new { provee.dirMatriz, provee.e_mail, provee.telefono };
        if (consultaP.Count() == 0)
        {

        }
        else
        {
            foreach (var registro in consultaP)
            {
                ldi1 = registro.dirMatriz.Trim();
                lte1 = registro.telefono.Trim();
                le_mail = registro.e_mail.Trim();
                cadena7 = "*z%" + "*y%" +
                    "*a%" + ldi1 + "*/a%" +
                    "*b%" + lte1 + "*/b%" +
                    "*c%" + le_mail + "*/c%" +
                    "*d%" + "" + "*/d%" +
                    "*e%" + "" + "*/e%" +
                    "*f%" + "" + "*/f%" +
                    "*g%" + "" + "*/g%" +
                    "*/y%" + "*/z%";
            }
        }


        if (lclaveacceso == null)
        {
            lclaveacceso = "";
        }

        try
        {
            wsSRI.WebService1SoapClient servicio = new wsSRI.WebService1SoapClient();

            servicio.obNomarticulo(lambiente, ltipoemision, lrazonsocial, lnombrecomercial, lruc, lclaveacceso, lcoddoc, lestab, lptoemi, lsecuencial, ldirMatriz,
                lfechaemision, ldirestablecimiento, lcontribuyenteEspecial, lobligadoContabilidad, ltipoIdentificacionSujetoRetenido, lrazonSocialSujetoRetenido, lidentificacionSujetoRetenido,
                lperiodoFiscal, cadena1, cadena2, cadena3, cadena4, cadena5, cadena6, cadena7);

            /*GUARDA OBSERVACION QUE EMITE EL SRI*/

            try
            {
                dc.sp_observacionSRI(lruc, lcoddoc, lestab, lptoemi, lsecuencial, "");
                nGrabados = 0;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
                nGrabados = -1;
            }
            finally
            {

            }
            /*******************************************/

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
            nGrabados = -1;
        }

        finally
        {
        }

        /*wsComprobantesSRI.WSComprobantesElectronicos retencion = new wsComprobantesSRI.WSComprobantesElectronicosClient();
        

         wsComprobantesSRI.comprobantesRetencionRequest comprobantes  = new  wsComprobantesSRI.comprobantesRetencionRequest(lambiente, ltipoemision, lrazonsocial, lnombrecomercial, lruc, 
             lclaveacceso, lcoddoc, lestab, lptoemi, lsecuencial, ldirMatriz, lfechaemision, ldirestablecimiento, lcontribuyenteEspecial, lobligadoContabilidad, ltipoIdentificacionSujetoRetenido, 
             lrazonSocialSujetoRetenido, lidentificacionSujetoRetenido, lperiodoFiscal, direccion, telefono, mail, codigo, codigoRetencion, baseImponible, porcentajeRetener, valorRetenido, codDocSustento, numDocSustento, fechaEmisionDocSustento)



       /// ds.Tables(0).Rows.Add(obj.comprobantesRetencion(comprobantes).return)*/

        return nGrabados;
    }

    protected void ListarRetencion(int pid_infotributaria)
    {
        var consultaRET = from a in dc.tbl_matriz
                          from b in dc.tbl_ruc
                          from c in dc.tbl_infotributaria
                          from d in dc.tbl_infoCompRetencion
                          from ret in dc.tbl_impuestosRet
                          where a.ruc == b.ruc
                            && b.ruc == c.ruc
                            && b.coddoc == c.coddoc
                            && b.estab == c.estab
                            && b.ptoemi == c.ptoemi
                            && c.id_infotributaria == d.id_infotributaria
                            && d.id_infoCompRetencion == ret.id_infoCompRetencion
                            && c.id_infotributaria == pid_infotributaria
                          select new
                          {
                              cre_sri = c.cre_sri,
                              codigo = ret.codigo,
                              codigoRetencion = ret.codigoRetencion,
                              baseImponible = ret.baseImponible,
                              porcentajeRetener = ret.porcentajeRetener,
                              valorRetenido = ret.valorRetenido,
                              codDocSustento = ret.codDocSustento,
                              numDocSustento = ret.numDocSustento,
                              fechaEmisionDocSustento = ret.fechaEmisionDocSustento
                          };


        grvRetencionSRI.DataSource = consultaRET;
        grvRetencionSRI.DataBind();
    }

    #endregion

    #region ACTIVAR Y DESACTIVAR BOTONES

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        desactivarBotones();

        blanquearObjetos();

        desactivarObjetos();
    }

    protected void desactivarBotones()
    {
        btnValidar.Visible = true;
        btnGuardar.Visible = false;
        btnEnviar.Visible = false;
        btnRegresar.Visible = true;
    }

    protected void activarBotones()
    {
        btnValidar.Visible = false;
        btnGuardar.Visible = false;
        btnEnviar.Visible = true;
        btnRegresar.Visible = true;
    }

    protected void blanquearObjetos()
    {
        //dlls
        ddlTarifa.SelectedValue = "2";
        ddlTipDoc.SelectedValue = "-1";
        ddlBiGastos.SelectedValue = "-1";
        ddlBiCodCble.SelectedValue = "-1";
        ddlB0Gastos.SelectedValue = "-1";
        ddlB0CodCble.SelectedValue = "-1";
        ddlSiGastos.SelectedValue = "-1";
        ddlSiCodCble.SelectedValue = "-1";
        ddlS0Gastos.SelectedValue = "-1";
        ddlS0CodCble.SelectedValue = "-1";
        //fechas
        DateTime esteDia = DateTime.Today;

        txtFecha.Text = esteDia.ToString("d");
        txtFechCaduc.Text = esteDia.ToString("d");
        txtFechDoc.Text = esteDia.ToString("d");

        //txts

        txtNumRet.Text = string.Empty;
        txtRuc.Text = string.Empty;
        txtrso.Text = string.Empty;
        txtemail.Text = string.Empty;
        txtContribuyente.Text = string.Empty;
        txtSerie.Text = string.Empty;
        txtNumDoc.Text = string.Empty;
        txtAutorizacion.Text = string.Empty;
        txtBien.Text = string.Empty;

        formatoTexto();





    }

    protected void desactivarObjetos()
    {
        pnMensaje2.Visible = false;
        pnBienIva.Visible = false;
        pnBienCero.Visible = false;
        pnServicioIva.Visible = false;
        pnServicioCero.Visible = false;

    }

    #endregion


    /*PROCESOS PARA GUARDAR PROVEEDOR*/
    #region MANTENIMIENTO PROVEEDOR
    protected void btnGuardaProv_Click(object sender, EventArgs e)
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
        e_mail = txtMailProv.Text;
        telefono = txtTel.Text;

        /*VALIDAR INFORMACION*/

        if (ruc.Length < 10
            || razonsocial.Length <= 5
            || nombreComercial.Length <= 3
            || dirMatriz.Length <= 9            || telefono.Length < 9
            || e_mail.Length <= 10)
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
        pnSucursal.Visible = true;
        pnDocumento.Visible = true;
        pnValores.Visible = true;
        //pnRetener.Visible = false;
        pnBotonera.Visible = true;
    }


    #region CREAR PROVEEDOR
   

    protected void blanquearSucursal()
    {
        txtRuc.Text = string.Empty;
        txtrazonsocial.Text = string.Empty;
        txtnombreComercial.Text = string.Empty;
        txtdirMatriz.Text = string.Empty;
        txtcontribuyenteEspecial.Text = string.Empty;
        ddlObligado.SelectedValue = string.Empty;
        txtMailProv.Text = string.Empty;
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
            txtMailProv.Text = registro.e_mail;
            txtTel.Text = registro.telefono;
        }
    }
    #endregion

    protected void btnIngresaProv_Click1(object sender, EventArgs e)
    {
        btnIngresaProv_Click1();
    }

    protected void btnIngresaProv_Click1()
    {
        pnIngresarProveedor.Visible = true;

        pnSucursal.Visible = false;
        pnDocumento.Visible = false;
        pnValores.Visible = false;
        //pnRetener.Visible = false;
        pnBotonera.Visible = false;

    }

#endregion

    protected Tuple<int, string> parametrizarDatosInfoTributaria()
    {

        pCodDoc = "07";
        var consulta = from TinfT in dc.tbl_infotributaria
                       where TinfT.ruc == pRuc && TinfT.estab == pEstab && TinfT.ptoemi == pPtoEmi && TinfT.coddoc == pCodDoc && TinfT.secuencial == pSecuencial
                       select TinfT;

        var consulta2 = from Tmatriz in dc.tbl_matriz
                        where Tmatriz.ruc == pRuc
                        select Tmatriz;


        if (consulta.Count() == 0)
        {

            lblMensaje.Text = "No existe registro en información tributaria";
        }
        else
        {
            foreach (var registro in consulta)
            {
                if (registro.id_infotributaria > 0)
                {
                    pId_InfoTributaria = registro.id_infotributaria;
                    lblpId_InfoTributaria.Text = Convert.ToString(registro.id_infotributaria);
                }
                else
                {
                    lblMensaje.Text = "No existe id de información tributaria";
                }

            }
        }



        if (consulta2.Count() == 0)
        {

            lblMensaje.Text = "No existe registro si es obligado a llevar contabilidad";
        }
        else
        {
            foreach (var registro2 in consulta2)
            {
                if (registro2.obligadoContabilidad != null)
                {
                    pObligadoContabilidad = registro2.obligadoContabilidad;
                }
                else
                {
                    lblMensaje.Text = "No existe dato si es obligado a llevar contabilidad";
                }

            }
        }

        return Tuple.Create(pId_InfoTributaria, pObligadoContabilidad);
    }


    protected Tuple<int> parametrizarDatosInfoCompRetencion()
    {

        var consulta = from TinfCRet in dc.tbl_infoCompRetencion
                       where TinfCRet.id_infotributaria == pId_InfoTributaria
                       select TinfCRet;


        if (consulta.Count() == 0)
        {

            lblMensaje.Text = "No existe registro en información de comprobantes de retención";
        }
        else
        {
            foreach (var registro in consulta)
            {
                if (registro.id_infoCompRetencion > 0)
                {
                    pId_InfoCompRetencion = registro.id_infoCompRetencion;
                }
                else
                {
                    lblMensaje.Text = "No existe id de información de comprobantes de retención";
                }

            }
        }


        return Tuple.Create(pId_InfoCompRetencion);
    }

    protected int traerIdInfoTributaria()
    {
        int lid = 0;
        string lnumero = txtNumRet.Text.Trim();
        string sucursal = ddlSucursal.SelectedValue.Trim();

        var consultaId = from Did in dc.tbl_infotributaria
                         where Did.secuencial == lnumero
                                && Did.sucursal == sucursal
                         select new
                         {
                             id = Did.id_infotributaria
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


    protected double traerIdCabeceraretencion()
    {
        double lid = 0;
        string lnumero = txtNumRet.Text.Trim();
        string sucursal = ddlSucursal.SelectedValue.Trim();

        var cIdRetencion = from Did in df.COMPROBANTERETENCION
                         where Did.CRE_SECUENCIAL == lnumero
                                && Did.SUCURSAL == sucursal
                         select new
                         {
                             id = Did.CRE_ID
                         };

        if (cIdRetencion.Count() == 0)
        {
            // no existe id
            lid = 0;
        }
        else
        {
            foreach (var registro in cIdRetencion)
            {
                lid = Convert.ToDouble(registro.id);
            }
        }

        return lid;
    }



    protected void btnSecuencial_Click(object sender, EventArgs e)
    {
        int lsec = 0;

        string lnumero = txtNumRet.Text.Trim();
        string sucursal = ddlSucursal.SelectedValue.Trim();

        var cSec = from Dsec in dc.tbl_secuenciales
                   where Dsec.sucursal == sucursal
                         select new
                         {
                             sec = Dsec.retencion
                         };

        if (cSec.Count() == 0)
        {
            // no existe sec
            lsec = 1;
        }
        else
        {
            foreach (var registro in cSec)
            {
                lsec = Convert.ToInt32(registro.sec);
            }
        }
        lsec = lsec + 1;
        txtNumRet.Text = Convert.ToString(lsec);

        if (txtNumRet.Text.Length > 0)
        {
            txtNumRet.Text = llenarCeros(txtNumRet.Text.Trim(), '0', 9);
        }

        txtRuc.Focus();
    }
}