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



public partial class Socio_modificarContrato : System.Web.UI.Page
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

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();


            string laccion = "ACTIVOS";

            ddlVendedor.DataSource = dc.sp_listaVendedores(laccion);
            ddlVendedor.DataBind();

            /*TIPO DE MEMBRESIAS*/

            var consultaMem = from tmem in dc.tbl_tipoMembresia
                              select new
                              {
                                  prefijo = tmem.prefijo,
                                  descripcion = tmem.descripcion
                              };


            ddlTipoMembrecia.DataSource = consultaMem;
            // ddlTipoMembrecia.DataValueField = "prefijo";
            //ddlTipoMembrecia.DataTextField = "descripcion";
            ddlTipoMembrecia.DataBind();

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

    protected void cargarDatosSocio(string laccion, string lsocio, string lcontrato)
    {
        // var traeSocio = dc.sp_cargaSocioSocio(laccion, lsocio, lcontrato);
        var traeSocio = dc.sp_consultaSocios(laccion, lsocio, lcontrato);
        foreach (var regSocio in traeSocio.ToList())
        {
            txtRuc.Text = regSocio.ciruc;
            txtApellidos.Text = regSocio.apellidos;
            txtNombres.Text = regSocio.nombres;
            txtLugarNac.Text = regSocio.lugar_nacimiento;
            txtFechaNac.Text = Convert.ToString(regSocio.fecha_nacimiento);
            ddlEstadoCivil.SelectedValue = Convert.ToString(regSocio.estado_civil);
            ddlGenero.SelectedValue = regSocio.genero;
            txtProfesion.Text = regSocio.profesion;
            txtApellidosCon.Text = regSocio.apellido_conyu;
            txtNombresCon.Text = regSocio.nombre_conyu;
            txtCiudadDom.Text = regSocio.ciudad_domi;
            txtDireccionDom.Text = regSocio.direccion_domi;
            txtSectorDom.Text = regSocio.sector_domi;
            txtEdificioDom.Text = regSocio.edificio_domi;
            txtPisoDom.Text = regSocio.piso_domi;
            txtDepartamentoDom.Text = regSocio.departamento_domi;
            txtReferenciaDom.Text = regSocio.referencia_domi;
            txtTelefonoDom.Text = regSocio.telefono_contac;
            txtCelularDom.Text = regSocio.celular_contac;
            txtEmailDom.Text = regSocio.email_contac;
            txtEmpresa.Text = regSocio.nombre_empr_trab;
            txtCiudad.Text = regSocio.ciudad_trab;
            txtDireccion.Text = regSocio.direccion_trab;
            txtSector.Text = regSocio.sector_trab;
            txtReferencia.Text = regSocio.referencia_trab;
            txtTelefono.Text = regSocio.telefono_trab;
            txtRucFactura.Text = regSocio.fax_trab;
            txtEmail.Text = regSocio.email_trab;
            //= lmarca_vehi ;
            // = lmodelo_vehi ;
            // = lcolor_vehi ;
            // = lplaca_vehi ;
            txtNumFactura.Text = regSocio.factura.Trim();
            //txtTipopago.Text =  regSocio.tipo_for_pag;
            //txtbanco.Text = regSocio.banco_for_pag;
            //txtCheque.Text = regSocio.ncheque_for_pag;
            txtNombresTar.Text = regSocio.nombr_tarjet_for_pag;
            //txtNumTarjeta.Text = regSocio.ntarjeta_for_pag;
            //txtPlazo.Text = regSocio.plazo_for_pag;
            txtNumContrato.Text = regSocio.ncontrato_membr;
            //ddlTipoMembrecia.SelectedValue = regSocio.tipo_membr;
            //txttipomembrecia.Text = regSocio.tipo_membr;
            //txtVendedor.Text = regSocio.vendedor_membr;
            txtFechaInicio.Text = Convert.ToString(regSocio.fecha_afiliacion_membr);
            txtVencimiento.Text = Convert.ToString(regSocio.fecha_vencimie_membr);
            //txtSinIva.Text = regSocio.valor_sin_iva;
            //txtConIva.Text = regSocio.valor_con_iva;
            //txtEnvio.Text = regSocio.envio_corresponden.Substring(0, 3);
            // = lid_nova ;
            //txtTarjeta.Text = regSocio.cod_tarjeta;
            // = lporfactu ;
            // = lfactura ;
            ddlSucursal2.SelectedValue = regSocio.cod_suc;
            txtRucFactura.Text = regSocio.ciruc;
            //txtTipopago.Text = regSocio.tipo_for_pag;
            //txtbanco.Text = regSocio.banco_for_pag;
            //txtCheque.Text = regSocio.ncheque_for_pag;
            //txtNombresTar.Text = regSocio.nombr_tarjet_for_pag;
            //txtNumTarjeta.Text = regSocio.cod_tarjeta;
            //txtPlazo.Text = regSocio.plazo_for_pag;
            txtIdNova.Text = Convert.ToString(regSocio.id_nova);
            txtRenovacion.Text = regSocio.envio_corresponden.Substring(4);
        }


    }

    protected int consultaDatosSocioContrato(string laccion, string lsocio, string lcontrato)
    {
        int kont;

        var consultaSoc = from Tsoc in dt.socios
                          where Tsoc.ciruc == lsocio &&
                                Tsoc.ncontrato_membr == lcontrato
                          select new { ciruc = Tsoc.ciruc };

        if (consultaSoc.Count() == 0)
        {
            kont = 1;
        }
        else
        {
            kont = 0;

        }

        return kont;

    }

    protected void cargarDatosTarjeta(string laccion, string lsocio, string lcontrato)
    {

        var traeTarjeta = dc.sp_consultaTarjetasSocio(laccion, lsocio, lcontrato);
        foreach (var regTarjeta in traeTarjeta.ToList())
        {
            txtRucTar.Text = regTarjeta.ciruc;
            txtNombresTar.Text = regTarjeta.nombres;
            txtDesdeTar.Text = Convert.ToString(regTarjeta.fecha_afiliacion_membr);
            txtVenceTar.Text = Convert.ToString(regTarjeta.fecha_vencimie_membr);
            txtTipoMembTar.Text = regTarjeta.tipo_membr;
            txtNumContratoTar.Text = regTarjeta.ncontrato_membr;

        }

    }

    protected void cargarDatosGuia(string laccion, string lsocio, string lcontrato)
    {

        var traeGuia = dc.sp_consultaGuiaRemision(laccion, lsocio, lcontrato);
        foreach (var regGuia in traeGuia.ToList())
        {
            txtRucGuia.Text = regGuia.ciruc;
            txtNombresGuia.Text = regGuia.nombres;
            txtDireccionGuia.Text = regGuia.direccion;
            txtCiudadGuia.Text = regGuia.ciudad;
            txtSector.Text = regGuia.telefono;
        }

    }

    protected void tarjetaSocio()
    {
        string lnombre, lapellido;

        int linicio, lfin;

        lnombre = txtNombres.Text.Trim();

        linicio = 0;
        lfin = lnombre.IndexOf(" ");

        if (lfin > 0)
        {
            lnombre = lnombre.Substring(linicio, lfin);
        }



        lapellido = txtApellidos.Text.Trim();

        linicio = 0;
        lfin = lapellido.IndexOf(" ");

        if (lfin > 0)
        {
            lapellido = lapellido.Substring(linicio, lfin);
        }
        txtRucTar.Text = txtRuc.Text;
        txtNombresTar.Text = lnombre + " " + lapellido;

        txtDesdeTar.Text = txtFechaInicio.Text;
        txtVenceTar.Text = txtVencimiento.Text;
    }

    protected void guiaRemision()
    {
        string lnombre, lapellido;

        int linicio, lfin;

        lnombre = txtNombres.Text.Trim();

        linicio = 0;
        lfin = lnombre.IndexOf(" ");

        if (lfin > 0)
        {
            lnombre = lnombre.Substring(linicio, lfin);
        }


        lapellido = txtApellidos.Text.Trim();

        linicio = 0;
        lfin = lapellido.IndexOf(" ");

        if (lfin > 0)
        {
            lapellido = lapellido.Substring(linicio, lfin);
        }


        txtRucGuia.Text = txtRuc.Text;
        txtNombresGuia.Text = txtNombres.Text.Trim() + " " + txtApellidos.Text.Trim();
        txtDireccionGuia.Text = txtDireccionDom.Text.Trim() + " " + txtEdificioDom.Text.Trim() + " " + txtPisoDom.Text.Trim() + " " + txtDepartamentoDom.Text.Trim();
        txtSectorGuia.Text = txtSectorDom.Text.Trim();
        txtCiudadGuia.Text = txtCiudadDom.Text.Trim();
        txttelefonoGuia.Text = txtTelefonoDom.Text.Trim() + "/" + txtCelularDom.Text.Trim();


    }

    protected bool validarContrato()
    {
        bool lpasa;
        string lcontrato, lsocio;

        lpasa = true;
        lcontrato = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
        //lcontrato = txtNumContrato.Text.Trim();
        lsocio = txtRuc.Text.Trim();
        var consultaUnico = from TCon in dt.socios
                            where TCon.ciruc == lsocio &&
                                  TCon.ncontrato_membr == lcontrato
                            select new { ciruc = TCon.ciruc };

        if (consultaUnico.Count() == 0)
        {
            lpasa = true;
        }
        else
        {
            lpasa = true;
        }

        return lpasa;
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {

        int KONT;
        string laccion, lsuc, lsocio, lcontrato, bucasx;


        KONT = 0;
        bucasx = "";



        laccion = "XCONTRATO";

        lsuc = ddlSucursal2.SelectedValue.ToString();
        lsocio = txtSocio.Text.Trim();
        lcontrato = txtContrato.Text.Trim();
        lblMsg.Visible = false;
        lblMsg.Text = "MSG";


        KONT = consultaDatosSocioContrato(laccion, lsocio, lcontrato);

        if (KONT == 1)
        {
            bucasx = "nohay";
        }
        else
        {
            bucasx = "contrato";
        }

        if (bucasx == "contrato")
        {
            laccion = "XCONTRATO";
            cargarDatosSocio(laccion, lsocio, lcontrato);
            cargarDatosTarjeta(laccion, lsocio, lcontrato);
            cargarDatosGuia(laccion, lsocio, lcontrato);
        }

        if (bucasx == "nohay")
        {

            laccion = "";
            btnCancelar_Click();
        }
        else
        {
            tarjetaSocio();
            guiaRemision();
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();
    }

    protected void btnCancelar_Click()
    {
        //lblMsg.Visible = false;
        //lblMsg.Text = "MSG";

        DateTime esteDia = DateTime.Today;

        txtIdNova.Text = "";
        txtFechaInicio.Text = esteDia.ToString("d");
        txtVencimiento.Text = esteDia.ToString("d");

        txtRuc.Text = "";
        txtApellidos.Text = "";
        txtNombres.Text = "";
        txtLugarNac.Text = "";
        txtFechaNac.Text = "";
        ddlEstadoCivil.SelectedValue = "";
        ddlGenero.SelectedValue = "";
        txtProfesion.Text = "";
        txtApellidosCon.Text = "";
        txtNombresCon.Text = "";
        txtCiudadDom.Text = "";
        txtDireccionDom.Text = "";
        txtSectorDom.Text = "";
        txtEdificioDom.Text = "";
        txtPisoDom.Text = "";
        txtDepartamentoDom.Text = "";
        txtReferenciaDom.Text = "";
        txtTelefonoDom.Text = "";
        txtCelularDom.Text = "";
        txtEmailDom.Text = "";
        txtEmpresa.Text = "";
        txtCiudad.Text = "";
        txtDireccion.Text = "";
        txtSector.Text = "";
        txtReferencia.Text = "";
        txtTelefono.Text = "";
        txtRucFactura.Text = "";
        txtEmail.Text = "";
        //= lmarca_vehi ;
        // = lmodelo_vehi ;
        // = lcolor_vehi ;
        // = lplaca_vehi ;
        //        txtTipopago.Text = "";
        //txtbanco.Text = "";
        //txtCheque.Text = "";
        txtNombresTar.Text = "";
        //txtNumTarjeta.Text = "";
        //txtPlazo.Text = "";
        txtNumContrato.Text = "";
        txtNumFactura.Text = "";
        //ddlTipoMembrecia.SelectedValue = "";
        //ddlVendedor.SelectedValue = "";
        // txtFechaInicio.Text = "";
        // txtVencimiento.Text = "";
        //txtSinIva.Text = "";
        //txtConIva.Text = "";
        //txtEnvio.Text = "";
        // = lid_nova ;
        //txtTarjeta.Text = "";
        // = lporfactu ;
        // = lfactura ;
        //txtSucursal.Text = "";
        txtRenovacion.Text = "";
        /**********************/
        txtRucGuia.Text = "";
        txtNombresGuia.Text = "";
        txtDireccionGuia.Text = "";
        txtCiudadGuia.Text = "";
        txtSectorGuia.Text = "";
        txttelefonoGuia.Text = "";
        txtNumContratoTar.Text = "";
        txtRucTar.Text = "";
        txtNombresTar.Text = "";
        txtDesdeTar.Text = "";
        txtVenceTar.Text = "";
        txtTipoMembTar.Text = "";
        txtNumContratoTar.Text = "";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        bool lpasa;

        lpasa = false;
        lpasa = validarContrato();
        if (lpasa)
        {
            // lpasa = validarDatos();
            lpasa = true;
            if (lpasa)
            {
                lblMsg.Visible = true;
                btnGuardar_Click();
                lblMsg.Text = "El contrato: " + txtNumContrato.Text.Trim() + " se ha grabado correctamente";
                /*envia correos a*/
                // Matriz
                //enviarCorreo("socios@aneta.org.ec");
                // Sucursal
                //enviarCorreo(txtSucEmail.Text.Trim());
                // Cliente
                //enviarCorreo(txtEmailDom.Text.Trim());
                btnCancelar_Click();

            }
            else
            {
                lblMsg.Visible = true;
                //lblMsg.Text = "Ingrese toda la información solicitada";
            }
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "El contrato ya fue ingresado";
        }


    }
    protected void btnGuardar_Click()
    {
        //txtNumContratoTar.Text = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
        //txtNumContrato.Text = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
        guardarMmebresia();
        //guardarTarjeta();
        //guardarGuia();

    }

    protected void guardarMmebresia()
    {
        decimal lid_nova;
        string laccion,
                lciruc,
                lapellidos,
                lnombres,
                llugar_nacimiento,
                lfecha_nacimiento,
                lestado_civil,
                lgenero,
                lprofesion,
                lapellido_conyu,
                lnombre_conyu,
                lciudad_domi,
                ldireccion_domi,
                lsector_domi,
                ledificio_domi,
                lpiso_domi,
                ldepartamento_domi,
                lreferencia_domi,
                ltelefono_contac,
                lcelular_contac,
                lemail_contac,
                lnombre_empr_trab,
                lciudad_trab,
                ldireccion_trab,
                lsector_trab,
                lreferencia_trab,
                ltelefono_trab,
                lfax_trab,
                lemail_trab,
                lmarca_vehi,
                lmodelo_vehi,
                lcolor_vehi,
                lplaca_vehi,
                ltipo_for_pag,
                lbanco_for_pag,
                lncheque_for_pag,
                lnombr_tarjet_for_pag,
                lntarjeta_for_pag,
                lplazo_for_pag,
                lncontrato_membr,
                ltipo_membr,
                lvendedor_membr,
                lfecha_afiliacion_membr,
                lfecha_vencimie_membr,
                lvalor_sin_iva,
                lvalor_con_iva,
                lenvio_corresponden,
                lcod_tarjeta,
                lporfactu,
                lfactura,
                lcod_suc;

        laccion = "MODIFICAR";
        lciruc = txtRuc.Text;
        lapellidos = txtApellidos.Text;
        lnombres = txtNombres.Text;
        llugar_nacimiento = txtLugarNac.Text;
        lfecha_nacimiento = txtFechaNac.Text;
        lestado_civil = ddlEstadoCivil.SelectedValue;
        lgenero = ddlGenero.SelectedValue;
        lprofesion = txtProfesion.Text;
        lapellido_conyu = txtApellidosCon.Text;
        lnombre_conyu = txtNombresCon.Text;
        lciudad_domi = txtCiudadDom.Text;
        ldireccion_domi = txtDireccionDom.Text;
        lsector_domi = txtSectorDom.Text;
        ledificio_domi = txtEdificioDom.Text;
        lpiso_domi = txtPisoDom.Text;
        ldepartamento_domi = txtDepartamentoDom.Text;
        lreferencia_domi = txtReferenciaDom.Text;
        ltelefono_contac = txtTelefonoDom.Text;
        lcelular_contac = txtCelularDom.Text;
        lemail_contac = txtEmailDom.Text;
        lnombre_empr_trab = txtEmpresa.Text;
        lciudad_trab = txtCiudad.Text;
        ldireccion_trab = txtDireccion.Text;
        lsector_trab = txtSector.Text;
        lreferencia_trab = txtReferencia.Text;
        ltelefono_trab = txtTelefono.Text;
        lfax_trab = txtRucFactura.Text;
        lemail_trab = txtEmail.Text;
        lmarca_vehi = "";
        lmodelo_vehi = "";
        lcolor_vehi = "";
        lplaca_vehi = "";
        ltipo_for_pag = string.Empty;// txtTipopago.Text;
        lbanco_for_pag = string.Empty;//txtbanco.Text;
        lncheque_for_pag = string.Empty;//txtCheque.Text;
        lnombr_tarjet_for_pag = txtNombresTar.Text;
        lntarjeta_for_pag = string.Empty;//txtNumTarjeta.Text;
        lplazo_for_pag = string.Empty;//txtPlazo.Text;
        lncontrato_membr = txtNumContrato.Text;
        ltipo_membr = ddlTipoMembrecia.SelectedItem.Text.Trim();

        lvendedor_membr = ddlVendedor.SelectedValue;
        lfecha_afiliacion_membr = txtFechaInicio.Text;
        lfecha_vencimie_membr = txtVencimiento.Text;
        lvalor_sin_iva = string.Empty;//txtSinIva.Text;
        lvalor_con_iva = string.Empty;//txtConIva.Text;
        lenvio_corresponden = ddlEnvio.SelectedValue.Trim() + "/" + ddlEnvio.Text.Trim() + "-" + txtRenovacion.Text.Trim();
        lid_nova = Convert.ToDecimal(txtIdNova.Text.Trim());
        lcod_tarjeta = string.Empty;//txtTarjeta.Text;
        lporfactu = "";
        lfactura = txtNumFactura.Text.Trim();
        lcod_suc = ddlSucursal2.SelectedValue;

        dc.sp_abmMembresia(laccion, lciruc, lapellidos, lnombres, llugar_nacimiento, lfecha_nacimiento, lestado_civil,
                                     lgenero, lprofesion, lapellido_conyu, lnombre_conyu, lciudad_domi, ldireccion_domi, lsector_domi,
                                     ledificio_domi, lpiso_domi, ldepartamento_domi, lreferencia_domi, ltelefono_contac, lcelular_contac,
                                     lemail_contac, lnombre_empr_trab, lciudad_trab, ldireccion_trab, lsector_trab, lreferencia_trab,
                                     ltelefono_trab, lfax_trab, lemail_trab, lmarca_vehi, lmodelo_vehi, lcolor_vehi, lplaca_vehi, ltipo_for_pag,
                                     lbanco_for_pag, lncheque_for_pag, lnombr_tarjet_for_pag, lntarjeta_for_pag, lplazo_for_pag, lncontrato_membr
                                     , ltipo_membr, lvendedor_membr, lfecha_afiliacion_membr, lfecha_vencimie_membr, lvalor_sin_iva
                                     , lvalor_con_iva, lenvio_corresponden, lid_nova, lcod_tarjeta, lporfactu, lfactura, lcod_suc);
    }

    protected void ddlTipoMembrecia_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ltipo;
        ltipo = ddlTipoMembrecia.SelectedValue.Trim();

        if (ltipo != "NO")
        {

            txtDesdeTar.Text = txtFechaInicio.Text;
            txtVenceTar.Text = txtVencimiento.Text;
            txtTipoMembTar.Text = ddlTipoMembrecia.SelectedItem.Text.Trim();
            //txtNumContratoTar.Text = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
            txtNumContrato.Text = txtNumContrato.Text.Trim();
            tarjetaSocio();
        }
    }

    protected void ddlEnvio_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ltipo, lenvio, lsuc;
        string lnombre, lapellido;

        int linicio, lfin;


        ltipo = ddlTipoMembrecia.SelectedValue.Trim();
        lenvio = ddlEnvio.SelectedValue;
        lsuc = ddlSucursal2.SelectedValue.Trim();

        lnombre = txtNombres.Text.Trim();
        linicio = 0;
        lfin = lnombre.IndexOf(" ");


        /*DATOS DE LA SUCURSAL*/

        var consultaSuc = from TSuc in dc.tbl_mae_suc
                          where TSuc.mae_suc == lsuc
                          select new
                          {
                              ciu_suc = TSuc.ciu_suc,
                              dir_suc = TSuc.dir_suc,
                              te1_suc = TSuc.te1_suc,
                              e_mail = TSuc.e_mail
                          };

        if (consultaSuc.Count() == 0)
        {
            txtciusuc.Text = "";
            txtdirsuc.Text = "";
            txtte1suc.Text = "";
            txtEmail.Text = "";
        }
        else
        {
            foreach (var regSuc in consultaSuc)
            {
                txtciusuc.Text = regSuc.ciu_suc;
                txtdirsuc.Text = regSuc.dir_suc;
                txtte1suc.Text = regSuc.te1_suc;
                txtEmail.Text = regSuc.e_mail;
            }
        }
        /*************************/



        if (lfin > 0)
        {
            lnombre = lnombre.Substring(linicio, lfin);
        }

        lapellido = txtApellidos.Text.Trim();

        linicio = 0;
        lfin = lapellido.IndexOf(" ");

        if (lfin > 0)
        {
            lapellido = lapellido.Substring(linicio, lfin);
        }
        txtRucGuia.Text = txtRuc.Text;
        txtNombresGuia.Text = txtNombres.Text.Trim() + " " + txtApellidos.Text.Trim();



        if (ltipo != "NO")
        {

            txtDesdeTar.Text = txtFechaInicio.Text;
            txtVenceTar.Text = txtVencimiento.Text;
            txtTipoMembTar.Text = ddlTipoMembrecia.SelectedItem.Text.Trim();

            txtNumContratoTar.Text = txtNumContrato.Text.Trim();
            // txtNumContrato.Text = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
            // tarjetaSocio();
        }



        switch (lenvio)
        {
            case "0":
                txtDireccionGuia.Text = "";
                txtSectorGuia.Text = "";
                txtCiudadGuia.Text = "";
                txttelefonoGuia.Text = txtTelefono.Text.Trim() + "/" + txtTelefonoDom.Text.Trim() + "/" + txtCelularDom.Text.Trim();
                break;
            case "1":

                txtDireccionGuia.Text = txtDireccionDom.Text.Trim() + " " + txtEdificioDom.Text.Trim() + " " + txtPisoDom.Text.Trim() + " " + txtDepartamentoDom.Text.Trim();
                txtSectorGuia.Text = txtSectorDom.Text.Trim();
                txtCiudadGuia.Text = txtCiudadDom.Text.Trim();
                txttelefonoGuia.Text = txtTelefonoDom.Text.Trim() + "/" + txtCelularDom.Text.Trim();
                break;
            case "2":
                txtDireccionGuia.Text = txtDireccion.Text.Trim();
                txtSectorGuia.Text = txtSector.Text.Trim() + " " + txtReferencia.Text.Trim();
                txtCiudadGuia.Text = txtCiudad.Text.Trim();
                txttelefonoGuia.Text = txtTelefono.Text.Trim() + "/" + txtTelefonoDom.Text.Trim() + "/" + txtCelularDom.Text.Trim();
                break;
            case "3":
                txtDireccionGuia.Text = txtdirsuc.Text.Trim();
                txtSectorGuia.Text = ddlSucursal2.SelectedValue.Trim();
                txtCiudadGuia.Text = txtciusuc.Text.Trim();
                txttelefonoGuia.Text = txtTelefono.Text.Trim() + "/" + txtTelefonoDom.Text.Trim() + "/" + txtCelularDom.Text.Trim() + "/" + txtte1suc.Text.Trim();
                break;
            case "4":
                txtDireccionGuia.Text = "";
                txtSectorGuia.Text = "";
                txtCiudadGuia.Text = "";
                txttelefonoGuia.Text = txtTelefono.Text.Trim() + "/" + txtTelefonoDom.Text.Trim() + "/" + txtCelularDom.Text.Trim();
                break;
            default:
                break;
        }
    }
}