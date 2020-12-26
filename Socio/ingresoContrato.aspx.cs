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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MessagingToolkit.QRCode.Codec;



public partial class Socio_ingresoContrato : System.Web.UI.Page
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

    protected int consultaDatosSocio(string laccion, string lsocio, string lcontrato)
    {
        int kont;

        var consultaSoc = from Tsoc in dt.socios
                          where Tsoc.ciruc == lsocio
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

    protected int consultaDatosCliente(string laccion, string lsocio, string lcontrato)
    {
        int kont;

        var consultaSoc = from TCli in df.CLIENTE
                          where TCli.CLI_RUC == lsocio
                          select new { CLI_RUC = TCli.CLI_RUC };

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
            /* txtTipopago.Text = regSocio.tipo_for_pag;
             txtbanco.Text = regSocio.banco_for_pag;
             txtCheque.Text = regSocio.ncheque_for_pag;
             txtNombresTar.Text = regSocio.nombr_tarjet_for_pag;
             txtNumTarjeta.Text = regSocio.ntarjeta_for_pag;
             txtPlazo.Text = regSocio.plazo_for_pag;
             txtNumContrato.Text = regSocio.ncontrato_membr;
            // ddlTipoMembrecia.SelectedValue = regSocio.tipo_membr;
             //ddlVendedor.SelectedValue = regSocio.vendedor_membr;
             txtFechaInicio.Text = Convert.ToString(regSocio.fecha_afiliacion_membr);
             txtVencimiento.Text = Convert.ToString(regSocio.fecha_vencimie_membr);
             txtSinIva.Text = regSocio.valor_sin_iva;
             txtConIva.Text = regSocio.valor_con_iva;
             txtEnvio.Text = regSocio.envio_corresponden;
             // = lid_nova ;
             txtTarjeta.Text = regSocio.cod_tarjeta;
             // = lporfactu ;
             // = lfactura ;
            // ddlSucursal2.SelectedValue = regSocio.cod_suc;*/
            txtRucFactura.Text = regSocio.ciruc;
            //txtTipopago.Text = "";
            //txtbanco.Text = "";
            //txtCheque.Text = "";
            //txtNombresTar.Text = "";
            //txtNumTarjeta.Text = "";
            //txtPlazo.Text = "";
            txtNumContrato.Text = "";
            //ddlTipoMembrecia.SelectedValue = "";
            //ddlVendedor.SelectedValue = "";
            txtFechaInicio.Text = "";
            txtVencimiento.Text = "";
            //txtSinIva.Text = "";
            //txtConIva.Text = "";
            //txtEnvio.Text = "";
            // = lid_nova ;
            //txtTarjeta.Text = "";
            txtRenovacion.Text = "Renovación";
        }
        DateTime esteDia = DateTime.Today;


        txtFechaInicio.Text = esteDia.ToString("d");
        txtVencimiento.Text = esteDia.ToString("d");

    }

    protected void cargarDatosSociodesdeCliente(string laccion, string lsocio, string lcontrato)
    {


        // var traeCliente = dc.sp_cargaClienteSocio(laccion, lsocio, lcontrato);
        var traeCliente = dc.sp_consultaCliente(laccion, lsocio, lcontrato);
        foreach (var regCliente in traeCliente.ToList())
        {
            txtRuc.Text = regCliente.ciruc;
            txtApellidos.Text = regCliente.apellidos;
            txtNombres.Text = regCliente.nombres;
            txtLugarNac.Text = regCliente.lugar_nacimiento;
            txtFechaNac.Text = Convert.ToString(regCliente.fecha_nacimiento);
            ddlEstadoCivil.SelectedValue = Convert.ToString(regCliente.estado_civil);
            ddlGenero.SelectedValue = regCliente.genero;
            txtProfesion.Text = regCliente.profesion;
            txtApellidosCon.Text = regCliente.apellido_conyu;
            txtNombresCon.Text = regCliente.nombre_conyu;
            txtCiudadDom.Text = regCliente.ciudad_domi;
            txtDireccionDom.Text = regCliente.direccion_domi;
            txtSectorDom.Text = regCliente.sector_domi;
            txtEdificioDom.Text = regCliente.edificio_domi;
            txtPisoDom.Text = regCliente.piso_domi;
            txtDepartamentoDom.Text = regCliente.departamento_domi;
            txtReferenciaDom.Text = regCliente.referencia_domi;
            //txtTelefonoDom = regCliente.telefono_contac ;
            txtCelularDom.Text = regCliente.celular_contac;
            txtEmailDom.Text = regCliente.email_contac;
            txtEmpresa.Text = regCliente.nombre_empr_trab;
            txtCiudad.Text = regCliente.ciudad_trab;
            txtDireccion.Text = regCliente.direccion_trab;
            txtSector.Text = regCliente.sector_trab;
            txtReferencia.Text = regCliente.referencia_trab;
            txtTelefono.Text = regCliente.telefono_trab;
            txtRucFactura.Text = regCliente.fax_trab;
            txtEmail.Text = regCliente.email_trab;
            //= lmarca_vehi ;
            // = lmodelo_vehi ;
            // = lcolor_vehi ;
            // = lplaca_vehi ;
            /*txtTipopago.Text = regCliente.tipo_for_pag;
            txtbanco.Text = regCliente.banco_for_pag;
            txtCheque.Text = regCliente.ncheque_for_pag;
            txtNombresTar.Text = regCliente.nombr_tarjet_for_pag;
            txtNumTarjeta.Text = regCliente.ntarjeta_for_pag;
            txtPlazo.Text = regCliente.plazo_for_pag;
            txtNumContrato.Text = regCliente.ncontrato_membr;
            ddlTipoMembrecia.SelectedValue = regCliente.tipo_membr;
            //ddlVendedor.SelectedValue = regCliente.vendedor_membr;
            txtFechaInicio.Text = Convert.ToString(regCliente.fecha_afiliacion_membr);
            txtVencimiento.Text = Convert.ToString(regCliente.fecha_vencimie_membr);
            txtSinIva.Text = regCliente.valor_sin_iva;
            txtConIva.Text = regCliente.valor_con_iva;
            txtEnvio.Text = regCliente.envio_corresponden;
            // = lid_nova ;
            txtTarjeta.Text = regCliente.cod_tarjeta;
            // = lporfactu ;
            // = lfactura ;
           // ddlSucursal2.SelectedValue = regCliente.cod_suc;
             * */
            txtRucFactura.Text = regCliente.ciruc;
            //txtTipopago.Text = "";
            //txtbanco.Text = "";
            //txtCheque.Text = "";
            //txtNombresTar.Text = "";
            //txtNumTarjeta.Text = "";
            //txtPlazo.Text = "";
            txtNumContrato.Text = "";
            //ddlTipoMembrecia.SelectedValue = "";
            //ddlVendedor.SelectedValue = "";
            txtFechaInicio.Text = "";
            txtVencimiento.Text = "";
            //txtSinIva.Text = "";
            //txtConIva.Text = "";
            //txtEnvio.Text = "";
            // = lid_nova ;
            //txtTarjeta.Text = "";

            txtRenovacion.Text = "Nuevo";
        }
        DateTime esteDia = DateTime.Today;


        txtFechaInicio.Text = esteDia.ToString("d");
        txtVencimiento.Text = esteDia.ToString("d");


    }

    protected void cargarDatosSociodesNuevo(string laccion, string lsocio, string lcontrato)
    {
        DateTime esteDia = DateTime.Today;


        txtFechaInicio.Text = esteDia.ToString("d");
        txtVencimiento.Text = esteDia.ToString("d");

        txtRuc.Text = lsocio;
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
        //txtTelefonoDom = regCliente.telefono_contac ;
        txtCelularDom.Text = "";
        txtEmailDom.Text = "";
        txtEmpresa.Text = "";
        txtCiudad.Text = "";
        txtDireccion.Text = "";
        txtSector.Text = "";
        txtReferencia.Text = "";
        txtTelefono.Text = "";
        txtEmail.Text = "";
        //= lmarca_vehi ;
        // = lmodelo_vehi ;
        // = lcolor_vehi ;
        // = lplaca_vehi ;
        //txtTipopago.Text = "";
        //txtbanco.Text = "";
        //txtCheque.Text = "";
        //txtNombresTar.Text = "";
        //txtNumTarjeta.Text = "";
        //txtPlazo.Text = "";
        txtNumContrato.Text = "";
        txtRucFactura.Text = lsocio;
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
        txtRenovacion.Text = "Nuevo";
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

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
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

        laccion = "AGREGAR";
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
        lemail_contac = txtEmailDom.Text.ToLower();
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
        ltipo_for_pag = string.Empty;//txtTipopago.Text;
        lbanco_for_pag = string.Empty;//txtbanco.Text;
        lncheque_for_pag = string.Empty;//txtCheque.Text;
        lnombr_tarjet_for_pag = string.Empty;//txtNombresTar.Text;
        lntarjeta_for_pag = string.Empty;//txtNumTarjeta.Text;
        lplazo_for_pag = string.Empty;//txtPlazo.Text;
        lncontrato_membr = txtNumContrato.Text;
        ltipo_membr = ddlTipoMembrecia.SelectedItem.Text.Trim();

        lvendedor_membr = Convert.ToString(ddlVendedor.SelectedItem); //ddlVendedor.SelectedValue;
        lfecha_afiliacion_membr = txtFechaInicio.Text;
        lfecha_vencimie_membr = txtVencimiento.Text;
        lvalor_sin_iva = string.Empty;// txtSinIva.Text;
        lvalor_con_iva = string.Empty;// txtConIva.Text;
        lenvio_corresponden = ddlEnvio.SelectedValue.Trim() + "/" + ddlEnvio.Text.Trim() + "-" + txtRenovacion.Text.Trim();
        lid_nova = 0;
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

    protected void guardarGuia() {
        DateTime lfecha = DateTime.Today;

        string laccion
                , lruc
                , lnombres
                , ldireccion
                , lciudad
                , lsector
                , ltelefono
                , lcontrato;



        laccion = "AGREGAR";
        lruc = txtRucGuia.Text.Trim();
        lnombres = txtNombresGuia.Text.Trim();
        ldireccion = txtDireccionGuia.Text.Trim();
        lciudad = txtCiudadGuia.Text.Trim();
        lsector = txtSectorGuia.Text.Trim();
        ltelefono = txttelefonoGuia.Text.Trim();
        lcontrato = txtNumContratoTar.Text;
        dc.sp_abmGuiaRemision(laccion, 0, lfecha, lruc, lcontrato, lnombres, lciudad, lsector, ldireccion, ltelefono, lfecha, false);
    }

    protected void guardarTarjeta()
    {

        DateTime lfecha = DateTime.Today
             , ldesde, lvence;
        string laccion,
               lruc,
               lnombres,
               ltipo,
               lcontrato,
               lvendedor;


        laccion = "AGREGAR";
        lruc = txtRucTar.Text.Trim();
        lnombres = txtNombresTar.Text;
        ldesde = Convert.ToDateTime(txtDesdeTar.Text);
        lvence = Convert.ToDateTime(txtVenceTar.Text);
        ltipo = txtTipoMembTar.Text;
        lcontrato = txtNumContratoTar.Text;
        lvendedor = ddlVendedor.SelectedValue.Trim();



        dc.sp_abmTarjetaSocio2(laccion, 0, lfecha, lruc, lcontrato, ltipo, lnombres, ldesde, lvence, lfecha, false, lvendedor);
    }

    protected bool validarDatos()
    {
        bool lpasa;

        string laccion, lsuc, lfactura, lruc, lruc2, lsucfactura, lnombres, ldireccion, lvendedor, lcontrato, lemail, lenviar, ltelefono;
        int lano1, lano2, lmes1, lmes2, ldia1, ldia2,  lrango1, lrango2, lcodigo;
        DateTime lfrango = DateTime.Today; ;

        lblMsg.Text = "";
        lpasa = true;

        // return lpasa;

        lano1 = Convert.ToInt16(txtFechaInicio.Text.Trim().Substring(6, 4));
        lano2 = Convert.ToInt16(txtVencimiento.Text.Trim().Substring(6, 4));

        lmes1 = Convert.ToInt16(txtFechaInicio.Text.Trim().Substring(3, 2));
        lmes2 = Convert.ToInt16(txtVencimiento.Text.Trim().Substring(3, 2));

        ldia1 = Convert.ToInt16(txtFechaInicio.Text.Trim().Substring(0, 2));
        ldia2 = Convert.ToInt16(txtVencimiento.Text.Trim().Substring(0, 2));


        lrango1 = lano2 - lano1;

        lrango2 = lmes2 - lmes1;

        //lrango = lano2 - lano1;

        lcodigo = 0;
        laccion = "CONMEMB";
        lsuc = ddlSucursal2.SelectedValue.Trim();
        lsucfactura = txtNumFactura.Text.Trim();

        lfactura = "";
        if (lsucfactura.Length >= 4)
        {
            lfactura = lsucfactura.Substring(3);
        }
        else
        {
            lblMsg.Text = "Ingrese correctamente el numero de factura (SUC+NUMERO DE FACTURA)";
        }

        lruc = txtRuc.Text.Trim();
        lruc2 = txtRucFactura.Text.Trim();
        lnombres = txtNombresTar.Text.Trim();
        ldireccion = txtDireccionGuia.Text.Trim();
        //lvendedor = ddlVendedor.SelectedValue.Trim();
        lvendedor = Convert.ToString(ddlVendedor.SelectedItem);
        lcontrato = txtNumContratoTar.Text.Trim();
        lemail = txtEmailDom.Text.Trim();
        lenviar = Convert.ToString(ddlEnvio.SelectedItem).Trim();
        ltelefono = txtCelularDom.Text.Trim();

        

        var facelec = dc.sp_validaExistenciaFacturaElectronica(laccion, lsuc, lfactura, lsucfactura, lruc2);
        foreach (var regfac in facelec.ToList())
        {
            lcodigo = regfac.id_Cab_Recaudacion;
            lcodigo = 1;
        }

        if (!validarContrato())
        {
            lpasa = false;
            lblMsg.Text = "El número de contrato ya fue ingresado";
        }

        if (!validarFactura())
        {
            lpasa = false;
            lblMsg.Text = "El número de factura ya fue ingresada";
        }

        if (lcodigo == 0)
        {
           lpasa = false;
           //lpasa = true;
            lblMsg.Text = "La factura ingresada no corresponde al contrato  o no se ha realizado el cierre respectivo";
        }

        if (ltelefono.Length <= 6)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese número telefónico";
        }


        if (lnombres.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese los nombres y apellidos";
        }

        if (ldireccion.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese la dirección";
        }

        if (lvendedor == "1 ACCIONE VENDEDOR" || lvendedor == "1ASIN IDENTIFICACION (FALTA)")
        {
            lpasa = false;
            lblMsg.Text = "Ingrese el vendedor";
        }

        if (lcontrato.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese el número de contrato";
        }

        if (lemail.Length <= 2)
        {
            lblMsg.Text = "Ingrese el correo electrónico";
            lpasa = false;
        }

        if (lrango1 <= 0 && lrango2 <= 0)
        {
            lblMsg.Text = "Ingrese las fechas de inicio y vencimiento , correctamente";
            lpasa = false;
        }

        if (lenviar == "ENVIAR A:")
        {
            lpasa = false;
            lblMsg.Text = "Ingrese al lugar a ser enviado";
        }

        return lpasa;
    }


    protected bool validarContrato()
    {
        bool lpasa;
        string lcontrato, lsocio;

        lpasa = true;
        lcontrato = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);



        lsocio = txtRuc.Text.Trim();
        var cUnicoCon = from TCon in dt.socios
                        where TCon.ncontrato_membr == lcontrato
                        select new { ciruc = TCon.ciruc };

        if (cUnicoCon.Count() == 0)
        {
            lpasa = true;
        }
        else
        {
            lpasa = false;
        }

        return lpasa;
    }

    protected bool validarFactura()
    {
        bool lpasa;
        string lsocio, lfactura;

        lpasa = true;
        lfactura = txtNumFactura.Text.Trim();
        lsocio = txtRuc.Text.Trim();

        var cUnicaFac = from TCon in dt.socios
                        where TCon.factura == lfactura
                        select new { ciruc = TCon.ciruc };

        if (cUnicaFac.Count() == 0)
        {
            lpasa = true;
        }
        else
        {
            lpasa = false;
        }

        return lpasa;
    }

    protected void btnCancelar_Click()
    {
        //lblMsg.Visible = false;
        //lblMsg.Text = "MSG";

        DateTime esteDia = DateTime.Today;


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
        //txtTipopago.Text = "";
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


    public void enviarCorreoHtml()
    {
        string cuerpo = formatoEstandar();
        string mailDomicilio = txtEmailDom.Text.Trim();
        string mailOficina = txtEmail.Text.Trim();

        /*VARIABLES DEL SOCIO*/


        string nombreSocio = txtNombresTar.Text.Trim();
        string cedula = txtRucTar.Text.Trim();
        string fechaInicio = "Desde: " + txtFechaInicio.Text;
        string fechaFin = "Vence: " + txtVencimiento.Text;
        string contrato = "Cont: "+txtNumContratoTar.Text;
        string factura = "FaMe:"+txtNumFactura.Text;

        DateTime fechaHoy = DateTime.Today;
        string fecha = "Fecha Ing:"+Convert.ToString(fechaHoy).Substring(0,10);

        string caminoLogo = string.Empty;
        string membresia = string.Empty;
        string tipomem = ddlTipoMembrecia.SelectedValue.Trim();

        string codigoQR = generaQR(cedula + contrato + factura);


        switch (tipomem)
        {
            case "E":
                caminoLogo = "~//Plantillas//membresia3meses.html";
                membresia = "Membresía: " + "3 MESES";             
                break;
            case "S":
                caminoLogo = "~//Plantillas//membresiaStandar.html";
                membresia = "Membresía: " + "ESTANDAR";
                break; 
            case "M":
                caminoLogo = "~//Plantillas//membresiaMoto.html";
                membresia = "Membresía: " + "MOTO";
                break;
            case "T":
                caminoLogo = "~//Plantillas//membresiaTaxi.html";
                membresia = "Membresía: " + "TAXI";
                break;
            case "P":
                caminoLogo = "~//Plantillas//membresiaPremium.html";
                membresia = "Membresía: " + "PREMIUM";
                break;
            
            case "D":
                caminoLogo = "~//Plantillas//membresiaPremium.html";
                membresia = "Membresía: " + "PREMIUM 2 AÑOS";
                break;

            case "U":
                caminoLogo = "~//Plantillas//membresiaPremium.html";
                membresia = "Membresía: " + "PREMIUM 3 AÑOS";
                break;
            case "R":
                caminoLogo = "~//Plantillas//membresiaPremium.html";
                membresia = "Membresía: " + "PROAUTO PREMIUM";
                break;
            default:
                Console.WriteLine("Default case");
                break;
        }


        StringBuilder emailHtml = new StringBuilder(File.ReadAllText(Server.MapPath(caminoLogo)));


        emailHtml.Replace("tipoMembresia", membresia);
        emailHtml.Replace("nombreSocio", nombreSocio);
        emailHtml.Replace("cedula", cedula);

        emailHtml.Replace("fechaInicio", fechaInicio);
        emailHtml.Replace("fechaFin", fechaFin);
        emailHtml.Replace("contrato", contrato);
        emailHtml.Replace("factura", factura);
        emailHtml.Replace("fecha", fecha);
        emailHtml.Replace("codigoQR", codigoQR);

        string envio = ddlEnvio.SelectedValue;
        string destinatarios = string.Empty;
        string cc = string.Empty;

        if(envio == "1"){
            destinatarios = txtEmailDom.Text.Trim().ToLower();
            cc = txtEmail.Text.Trim().ToLower() + "," + "socios@serviciosaneta.org.ec";
         }
          
        if(envio == "2"){
            destinatarios = txtEmail.Text.Trim().ToLower();
            cc = txtEmailDom.Text.Trim().ToLower() +"," + "socios@serviciosaneta.org.ec";
         }

        if (envio == "3" || envio == "4")
        {
            destinatarios = txtSucEmail.Text.Trim().ToLower();
            cc = txtEmailDom.Text.Trim().ToLower() + "," + "socios@serviciosaneta.org.ec";
        }

        string anexo = "";//Server.MapPath("~//images//iconos//logo2.jpg");

        new email().enviarCorreo("192.168.1.101", 25, "socios@serviciosaneta.org.ec", "aneta54", "MEMBRESIAS-ANETA", destinatarios, cc, "TARJETA VIRTUAL ANETA", anexo, emailHtml.ToString());

    }
    public bool enviarCorreo(string enviarA)
    {
        bool lenvio;

        string lsuc;

        lsuc = ddlSucursal2.SelectedValue.Trim();
        lenvio = false;

        

        // string from = "jeec1965@gmail.com";
        //string pass = "mishijas2";

        string from = "socios@serviciosaneta.org.ec";
        string pass = "aneta54";
        //string to = txtEmailDom.Text.Trim(); //"jose_espinosa3l@hotmail.com"; //"jeec1965@gmail.com";//"jose_espinosa3l@hotmail.com";
        string to = enviarA;
        string msm = "Felicitaciones " + txtNombresTar.Text.Trim() + " ahora es socio activo de ANETA y puede contar con los servicios ";
        string subject = lsuc + " Socio activo: " + txtNombresTar.Text.Trim();

        if (new email().enviarCorreo(from, pass, to, msm, subject))
        {
            lblMsg.Text = lblMsg.Text + " Se envío el mail";
            lenvio = false;
        }
        else
        {
            lblMsg.Text = lblMsg.Text + " Fallo en el envío de correo electrónico";
            lenvio = false;
        }

        

       


        //new email().enviarCorreo("smtp-mail.outlook.com", 587, "jose_espinosa3l@hotmail.com", "LizFranDilan", "anonimo", "sistemas@aneta.org.ec", "sistemas@aneta.org.ec", "envios con adjuntos", "C:\\tmp\\ANETA0118.JPG", "ejemplo desde asp y c#");


            //string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo
        return lenvio;

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
            laccion = "XCEDULA";

            KONT = consultaDatosSocio(laccion, lsocio, lcontrato);

            if (KONT == 1)
            {
                laccion = "XCLIENTE";

                KONT = consultaDatosCliente(laccion, lsocio, lcontrato);
                if (KONT == 1)
                {
                    bucasx = "nohay";
                }
                else
                {
                    bucasx = "cliente";
                }
            }
            else
            {
                bucasx = "cedula";
            }
        }
        else
        {
            bucasx = "contrato";
        }

        if (bucasx == "contrato")
        {
            laccion = "XCONTRATO";
            cargarDatosSocio(laccion, lsocio, lcontrato);
        }

        if (bucasx == "cedula")
        {
            laccion = "XCEDULA";
            cargarDatosSocio(laccion, lsocio, lcontrato);
        }

        if (bucasx == "cliente")
        {
            laccion = "XCLIENTE";
            cargarDatosSociodesdeCliente(laccion, lsocio, lcontrato);
        }

        if (bucasx == "nohay")
        {
            laccion = "";
            cargarDatosSociodesNuevo(laccion, lsocio, lcontrato);
        }


        tarjetaSocio();
        guiaRemision();
    }

    protected void btnGuardar_Click()
    {
        txtNumContratoTar.Text = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
        txtNumContrato.Text = ddlTipoMembrecia.SelectedValue.Trim() + llenarCeros(txtNumContrato.Text.Trim(), '0', 7);
        guardarMmebresia();
        guardarTarjeta();
        guardarGuia();

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        bool lpasa;

        lpasa = false;

        lblMsg.Visible = true;
        //lpasa = validarContrato();

        //if (lpasa)
        //{
        lpasa = validarDatos();

        if (lpasa)
        {

            btnGuardar_Click();
            lblMsg.Text = "El contrato: " + txtNumContrato.Text.Trim() + " se ha grabado correctamente";

            /*envia correos CON HTML a*/
            enviarCorreoHtml();

            /*envia correos a*/
            // Matriz
            enviarCorreo("socios@aneta.org.ec");
            // Sucursal
           enviarCorreo(txtSucEmail.Text.Trim().ToLower());
            // Cliente
            enviarCorreo(txtEmailDom.Text.Trim().ToLower());
            btnCancelar_Click();

        }
        //else
        //{
        //   lblMsg.Visible = true;
        //lblMsg.Text = "Ingrese toda la información solicitada";
        //}
        //}
        //else 
        //{
        //   lblMsg.Visible = true;
        //  lblMsg.Text = "El contrato ya fue ingresado";
        //}


    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();
    }

    protected void ddlTipoMembrecia_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ltipo;
        ltipo = ddlTipoMembrecia.SelectedValue.Trim();

        if (ltipo != "NO")
        {

            txtDesdeTar.Text = txtFechaInicio.Text;
            txtVenceTar.Text = txtVencimiento.Text;
            txtTipoMembTar.Text = ddlTipoMembrecia.SelectedItem.Text;
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

    protected string formatoHtml()
    {
        string cuerpo = "<tr><td style='background-color: #ecf0f1; text-align: left; padding: 0'><a href='http://190.63.17.119:5090/acefdos/images/iconos/anetatitulo2.jpg'><img width='20%' style='display:block; margin: 1.5% 3%' src='http://190.63.17.119:5090/acefdos/images/iconos/publicidad.jpg'></a></td></tr><tr><td style='padding: 0'><img style='padding: 0; display: block' src='http://190.63.17.119:5090/acefdos/images/iconos/aneta.jpg' width='100%'></td></tr><tr><td style='background-color: #ecf0f1'><div style='color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'><h2 style='color: #e67e22; margin: 0 0 7px'>Hola Poketrainer!</h2><p style='margin: 2px; font-size: 15px'>Somos la comunidad Poketrainers Trujillo, una comunidad de Pokémon VGC que se encuentra en la ciudad de Trujillo Perú.<br>Estando próxima la salida de Pokémon Sol y Luna en la comunidad estamos realizando una serie de actividades que nos preparara para su llegada, así que los invitamos a formar parte de la comunidad y a acompañarnos en esta nueva aventura en la región de Alola, donde muchos pokemon y aventuras nos esperan!<br>Entre las actividades tenemos:</p><ul style='font-size: 15px;  margin: 10px 0'><li>Batallas amistosas.</li><li>Torneos Oficiales.</li><li>Intercambios de Pokémon.</li><li>Actividades de integración.</li><li>Muchas sorpresas más.</li></ul><div style='width: 100%;margin:20px 0; display: inline-block;text-align: center'><img style='padding: 0; width: 200px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/iconos/dos1.ico'><img style='padding: 0; width: 200px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/iconos/anetatitulo.jpg'></div><div style='width: 100%; text-align: center'><a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #3498db' href='http://www.aneta.org.ec/'>Ir a la página</a>	</div><p style='color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0'>Poketrainers Trujillo 2016</p></div></td></tr>";
       return cuerpo;
    }

    protected string formatoEstandar() 
    {
        string cuerpo = string.Empty;

        string nombreSocio = txtNombresTar.Text.Trim();
        string cedulaSocio = txtRucTar.Text.Trim();
        string fechaInicio = txtDesdeTar.Text;
        string fechaFin = txtVenceTar.Text;
        string numeroContrato = txtNumContratoTar.Text;
        string numerofactura = txtNumFactura.Text;

         DateTime fecha = DateTime.Today;
         string fechaActual =  Convert.ToString(fecha);

        cuerpo = "<table style='max-width: 1200px; padding: 10px; margin:0 auto; border-collapse: collapse;'>";
        cuerpo = cuerpo + "<tr>";
        cuerpo = cuerpo + "<td style='background-color: white; text-align: left; padding: 0'>";
        cuerpo = cuerpo + "<a href='http://190.63.17.119'>";
        cuerpo = cuerpo + "<div style='width: 100%;margin:20px 0; display: inline-block;text-align: center'>";
        cuerpo = cuerpo + "<img tyle='padding: 0; width: 400px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/iconos/anetatitulo.jpg'>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</a>";
        cuerpo = cuerpo + "</td>";
        cuerpo = cuerpo + "</tr>";
        cuerpo = cuerpo + "<tr>";
        cuerpo = cuerpo + "<td style='background-color: white; text-align: left; padding: 0'>";
        cuerpo = cuerpo + "<div style='width: 100%;margin:5px 0; display: inline-block;text-align: center; position: RELATIVE;'>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 55px; top: 5px; z-index: 1;'>";
        cuerpo = cuerpo + "<img style='padding: 0; width: 400px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/socios/stand1.png'>";
        cuerpo = cuerpo + "</div >";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 505px; top: 5px; z-index: 2;'>";
        cuerpo = cuerpo + "<img style='padding: 0; width: 400px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/socios/stand2.png'>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 650px; top: 50px; z-index: 3; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + nombreSocio;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 650px; top: 70px; z-index: 7; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + "C.C.:";
        cuerpo = cuerpo + cedulaSocio;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 600px; top: 215px; z-index: 4; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + fechaInicio;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 800px; top: 215px; z-index: 4; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + fechaFin;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 540px; top: 150px; z-index: 5; color:white'>";
        cuerpo = cuerpo + "<P style='color :white;font-size:10px;' >";
        cuerpo = cuerpo + "# Contrato:";
        cuerpo = cuerpo + numeroContrato;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 540px; top: 170px; z-index: 6; color:white'>";
        cuerpo = cuerpo + "<P style='color :white; font-size:10px;'>";
        cuerpo = cuerpo + "# Factura:";
        cuerpo = cuerpo + numerofactura;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 540px; top: 190px; z-index: 6; color:white'>";
        cuerpo = cuerpo + "<P style='color :white; font-size:10px;'>";
        cuerpo = cuerpo + "Fecha:";
        cuerpo = cuerpo + fechaActual;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</td>";
        cuerpo = cuerpo + "</tr>";
        cuerpo = cuerpo + "<tr>";
        cuerpo = cuerpo + "<td style='background-color: #ecf0f1;'>";
        cuerpo = cuerpo + "<div style='color: #34495e; margin: 30% 10% 2%; text-align: justify;font-family: sans-serif'>";
        cuerpo = cuerpo + "<h2 style='color: #e67e22; margin: 0 0 7px'>MEMBRESIA ESTANDAR</h2>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div>";
        cuerpo = cuerpo + "<p style='margin: 15px; font-size: 15px'>";
        cuerpo = cuerpo + "Beneficios:<br>";
        cuerpo = cuerpo + "<ul style='font-size: 15px;  margin: 10px 0'>";
        cuerpo = cuerpo + "<li>3 servicios de chofer en caso de embriaguez.* </li>";
        cuerpo = cuerpo + "<li>3 servicios de Grúa o Plataforma.** </li>";
        cuerpo = cuerpo + "<li>3 servicios de Auxilio Mecánico.** </li>";
        cuerpo = cuerpo + "<li>5 servicios de Evaluación Psicosensométrica.** </li>";
        cuerpo = cuerpo + "<li>Cambio de aceite a domicilio con previa cita (aplica solo Quito y Guayaquil).** </li>";
        cuerpo = cuerpo + "<li>3 servicios de Lavado Express.** </li>";
        cuerpo = cuerpo + "<li>Precio preferencial en Cursos de Conducción.** </li>";
        cuerpo = cuerpo + "<li>Precio preferencial en Centros de Servicio Automotriz.** </li>";
        cuerpo = cuerpo + "<li>Agencia de Viajes y Turismo: Atención preferencial en compra de pasajes aéreos nacionales e internacionales y paquetes turísticos.** </li>";
        cuerpo = cuerpo + "<li>Llavero magnético de gasolina; por la compra prepago de $100 en combustible en las Estaciones de Servicio ANETA (aplica solo en Quito).** </li>";
        cuerpo = cuerpo + "<li>Tarjeta AAA (American Automobile Association) con descuentos en aproximadamente 100.00 locales de EE.UU., Canadá y Europa.* </li>";
        cuerpo = cuerpo + "</ul>";
        cuerpo = cuerpo + "<p style='margin: 15px; font-size: 15px'>Observaciones:<br>";
        cuerpo = cuerpo + "<ul style='font-size: 15px;  margin: 10px 0'>";
        cuerpo = cuerpo + "<li>Servicio válido para el Socio Titular de la tarjeta.</li>";
        cuerpo = cuerpo + "<li>**Servicio válido para el Socio Titular y su núcleo familiar hasta el cuarto grado de consanguinidad.</li>";
        cuerpo = cuerpo + "<li>Actividades de integración.</li>";
        cuerpo = cuerpo + "<li>Cobertura de 2 vehículos pertenecientes al núcleo familiar.</li>";
        cuerpo = cuerpo + "</ul>";
        cuerpo = cuerpo + "<div style='width: 100%; text-align: center'>";
        cuerpo = cuerpo + "<a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #3498db' href='http://190.63.17.119/'>Ir a la página principal de ANETA</a>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<p style='color: #b3b3b3; font-size: 12px; text-align: center;margin: 15px 0 0'>AUTOMOVIL CLUB DEL ECUADOR 2018</p>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</td>";
        cuerpo = cuerpo + "</tr>";
        cuerpo = cuerpo + "</table>";
        return cuerpo;
    }

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
}