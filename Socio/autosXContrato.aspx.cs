using AjaxControlToolkit;
using enviarEmail;
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
public partial class Socio_autosXContrato : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["temporalraceConnectionString"].ConnectionString;


    Data_TemporalRaceDataContext dt = new Data_TemporalRaceDataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["COMISIONESConnectionString"].ConnectionString;

    DataComisionesDataContext ds = new DataComisionesDataContext();

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

            /* DateTime lfecha = DateTime.Today;
              txtFechaIni.Text = Convert.ToString(lfecha);
              txtFechaFin.Text = Convert.ToString(lfecha);
              */
            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            //ddlSucursal2.DataSource = cSucursal;
            //ddlSucursal2.DataBind();

            //ddlVendedor.DataSource = dc.sp_listaVendedores(laccion);
            //ddlVendedor.DataBind();

            /*TIPO DE MEMBRESIAS*/

            var consultaMem = from tmem in dc.tbl_tipoMembresia
                              select new
                              {
                                  prefijo = tmem.prefijo,
                                  descripcion = tmem.descripcion
                              };


            //ddlTipoMembrecia.DataSource = consultaMem;
            // ddlTipoMembrecia.DataValueField = "prefijo";
            //ddlTipoMembrecia.DataTextField = "descripcion";
            //ddlTipoMembrecia.DataBind();

            /*
            var consultaCb = from Cb in dc.tbl_CuentaBancaria
                             orderby Cb.banco
                             select new
                             {
                                 id = Cb.id_cuentasBancaria,
                                 descripcion = Cb.banco.Trim() + '-' + Cb.numeroCuenta.Trim()
                             };
            ddlBanco.DataSource = consultaCb;
            ddlBanco.DataBind();
            ddlBanco.SelectedIndex = -1;*/
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void activarObjetos()
    {

    }

    #endregion

    protected int consultaDatosSocioContrato(string laccion, string lsocio, string lcontrato)
    {
        int kont;

        var consultaSoc = from Tsoc in dt.socios
                          where Tsoc.ciruc == lsocio &&
                                Tsoc.ncontrato_membr == lcontrato
                          select new
                          {
                              ciruc = Tsoc.ciruc,
                              fecini = Tsoc.fecha_vencimie_membr,
                              fecfin = Tsoc.fecha_vencimie_membr
                          };

        if (consultaSoc.Count() == 0)
        {
            kont = 1;
        }
        else
        {
            kont = 0;
            foreach (var regSoc in consultaSoc)
            {
                txtCedula.Text = regSoc.ciruc;
                txtFecIni.Text = regSoc.fecini;
                txtFecFin.Text = regSoc.fecini;
            }

        }

        return kont;

    }

    protected int consultaDatosAutosContrato(string laccion, string lsocio, string lcontrato)
    {
        int kont;

        var consultaAuto = from Tauto in dt.socios_vehiculos
                           where Tauto.codveh == lsocio &&
                                Tauto.contrato == lcontrato
                           select new
                           {
                               codveh = Tauto.codveh
                              ,
                               fecini = Tauto.fecini
                              ,
                               vigmem = Tauto.vigmem
                              ,
                               marca = Tauto.marca
                               ,
                               modelo = Tauto.modelo
                               ,
                               ano = Tauto.ano
                               ,
                               placa = Tauto.placa
                               ,
                               color = Tauto.color
                               ,
                               chasis = Tauto.chasis
                           };

        if (consultaAuto.Count() == 0)
        {
            kont = 1;
        }
        else
        {
            kont = 0;
            foreach (var regSoc in consultaAuto)
            {
                grvListadoAutos.DataSource = consultaAuto;
                grvListadoAutos.DataBind();
            }

        }

        return kont;

    }

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {



        int KONT, CONT;
        string laccion, lsocio, lcontrato, bucasx;


        KONT = 0;
        bucasx = "";



        laccion = "XCONTRATO";

        lsocio = txtSocio.Text.Trim();
        lcontrato = txtContrato.Text.Trim();
        lblMsg.Visible = true;
        lblMsg.Text = "MSG";


        KONT = consultaDatosSocioContrato(laccion, lsocio, lcontrato);
        CONT = consultaDatosAutosContrato(laccion, lsocio, lcontrato);

        if (KONT == 1)
        {
            bucasx = "nohay";
            lblMsg.Text = "No existe Contrato";
            pnGuardar.Visible = false;
            pnCabecera.Enabled = true;
        }
        else
        {
            bucasx = "contrato";
            lblMsg.Text = "Ingrese lo autos para el contrato";
            pnGuardar.Visible = true;
            pnCabecera.Enabled = false;
            //cargarAutos();
        }

        if (bucasx == "contrato")
        {
            laccion = "XCONTRATO";
        }

        if (bucasx == "nohay")
        {

            laccion = "";
            //btnCancelar_Click();
        }
        else
        {
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        btnGuardar_Click();

    }

    protected void btnGuardar_Click()
    {
        DateTime lfecha = DateTime.Today;
        decimal lid_veh;
        string laccion, lcodveh, lfactura
                        , lfecini
                        , lmarca
                        , lmodelo
                        , lano
                        , lplaca
                        , lcolor
                        , lchasis
                        , lvigmem
                        , lcod_mem
                        , lcarmen
                        , lanomem
                        , lcontrato;

        laccion = "AGREGAR";
        lcodveh = txtCedula.Text;
        lfactura = "";
        lmarca = txtMarca.Text.Trim();
        lmodelo = txtModelo.Text.Trim();
        lano = txtAno.Text.Trim();
        lplaca = txtPlaca.Text.Trim();
        lcolor = txtColor.Text.Trim();
        lchasis = txtChasis.Text.Trim();
        lfecini = txtFecIni.Text;
        lvigmem = txtFecFin.Text;
        lcod_mem = "";
        lcarmen = "0";
        lanomem = "0";
        lid_veh = 0;
        lcontrato = txtContrato.Text;


        if (lmarca.Length > 0 &&
                        lmarca.Length > 0 &&
                         lmodelo.Length > 0 &&
                         lano.Length > 0 &&
                         lplaca.Length > 0 &&
                         lcolor.Length > 0 &&
                         lchasis.Length > 0)
        {

            dc.sp_abmVehiculosxContrato(laccion
                , lcodveh
                , lfactura
                , lfecini
                , lmarca
                , lmodelo
                , lano
                , lplaca
                , lcolor
                , lchasis
                , lvigmem
                , lcod_mem
                , lcarmen
                , lanomem
                , lcontrato
                , lid_veh);
            lblMsg.Text = "Vehículo guardado";

            consultaDatosAutosContrato(laccion, lcodveh, lcontrato);

            btnCancelar_Click();
        }
        else
        {
            lblMsg.Text = "Llene todos los casilleros";
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {

        btnCancelar_Click();
    }

    protected void btnCancelar_Click()
    {
        pnGuardar.Visible = false;
        pnCabecera.Enabled = true;

        txtCedula.Text = "";

        txtMarca.Text = "";
        txtModelo.Text = "";
        txtAno.Text = "";
        txtPlaca.Text = "";
        txtColor.Text = "";
        txtChasis.Text = "";
        txtFecIni.Text = "";
        txtFecFin.Text = "";
        txtContrato.Text = "";
    }
}