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


public partial class Escuela_certificadoSicopractico : System.Web.UI.Page
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

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString;
    
    Data_EscuelaDataContext de = new Data_EscuelaDataContext();
   


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

            /* DateTime lfecha = DateTime.Today;
              txtFechaIni.Text = Convert.ToString(lfecha);
              txtFechaFin.Text = Convert.ToString(lfecha);
              */
            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);


            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();

            var cSucursal2 = dc.sp_listarSucursal("", "", 4, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal2;
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

    }

    #endregion

    public bool enviarCorreo(string enviarA)
    {
        bool lenvio;

        string lsuc;

        lsuc = ddlSucursal2.SelectedValue.Trim();
        lenvio = false;

        // string from = "jeec1965@gmail.com";
        //string pass = "mishijas2";

        string from = "socios@aneta.org.ec";
        string pass = "aneta54";
        //string to = txtEmailDom.Text.Trim(); //"jose_espinosa3l@hotmail.com"; //"jeec1965@gmail.com";//"jose_espinosa3l@hotmail.com";
        string to = enviarA;
        string msm = "Felicitaciones Señor/a/ita " + txtNombres.Text.Trim() + ", el Certificado de Aprobación de las Evaluaciones Psicosensométrica y de Conducción está listo y Usted lo puede retirar en la Escuela donde realizó las evaluaciones. Atendemos de lunes a viernes de 9H00 a 18H00. Saludos cordiales, ANETA.";
        string subject = lsuc + " Certificado: " + txtNombres.Text.Trim();

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

        return lenvio;

    }

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string laccion, lsuc, lsocio, lcontrato;
        lsuc = ddlSucursal2.SelectedValue.ToString();
        lsocio = txtSocio.Text.Trim();
        lcontrato = txtContrato.Text.Trim();
        lblMsg.Visible = false;
        lblMsg.Text = "MSG";
        laccion = "XCLIENTE";
        cargarDatosDeCliente(laccion, lsocio, lcontrato);
    }

    protected void cargarDatosDeCliente(string laccion, string lsocio, string lcontrato)
    {


        // var traeCliente = dc.sp_cargaClienteSocio(laccion, lsocio, lcontrato);
        var traeCliente = dc.sp_consultaCliente(laccion, lsocio, lcontrato);
        foreach (var regCliente in traeCliente.ToList())
        {
            txtRuc.Text = regCliente.ciruc;
            txtApellidos.Text = regCliente.apellidos;
            txtNombres.Text = regCliente.nombres;
            txtEmail.Text = regCliente.email_contac;
           
        }
        DateTime esteDia = DateTime.Today;


        txtFecha.Text = esteDia.ToString("d");



    }

    protected void btnGuardar_Click() 
    {
        string accion = string.Empty;
        string sucursal = ddlSucursal.SelectedValue;
        int numero = Convert.ToInt32(txtNumero.Text);
        string tipoDocumento = ddlTipoDoc.SelectedValue;
        string cedula = txtRuc.Text;
        string factura = txtNumFactura.Text;
        DateTime fecha = Convert.ToDateTime(txtFecha.Text);
        DateTime fechaSicotecnico = Convert.ToDateTime(txtFechaPsico.Text);
        DateTime fechaPractico = Convert.ToDateTime(txtFechaPract.Text);
        decimal notaPractico = Convert.ToDecimal(txtNotaPractico.Text);
        string estadoPsico = ddlEstadoPsico.SelectedValue;
        string instructorEvaluador = txtInstructor.Text.Trim();
        string resultado = ddlResultado.SelectedValue;
        string elaborado = txtElaborado.Text.Trim();
        string iniciales = txtIniciales.Text.Trim();
        string oficio = string.Empty;
        string observacion = txtObservacion.Text.Trim();
        string directorEscuela = txtDirector.Text.Trim();
        string responsablePractico = txtRespPractico.Text.Trim();
        string responsablePsico = txtRespPsico.Text.Trim();


        dc.sp_abmSicoPractico(accion,0,sucursal,numero,tipoDocumento,cedula,factura,fecha,fechaSicotecnico,fechaPractico,notaPractico,estadoPsico,instructorEvaluador,resultado,elaborado,iniciales,oficio,observacion,directorEscuela,responsablePractico,responsablePsico);
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
            lblMsg.Text = "El certificado: " + txtNumero.Text.Trim() + " se ha grabado correctamente";
            /*envia correos a*/
            string aprobado = ddlResultado.SelectedValue;
            if(aprobado =="1")
            {
                enviarCorreo(txtEmail.Text.Trim().ToLower());
            }
            btnCancelar_Click();

        }
       

    }
    protected bool validarDatos()
    {
        bool lpasa = true;
        DateTime lfrango = DateTime.Today;
        int lcodigo = 0;

        string laccion = "CONSICO";
       
        string sucursal2 = ddlSucursal2.SelectedValue;
        string numero = txtNumero.Text;
        string tipoDocumento = ddlTipoDoc.SelectedValue;
        string cedula = txtRuc.Text;
        string factura= txtNumFactura.Text;
        string fecha= txtFecha.Text;
        string fechaSicotecnico = txtFechaPsico.Text;
        string fechaPractico = txtFechaPract.Text;
        string notaPractico = txtNotaPractico.Text;
        string estadoPsico = ddlEstadoPsico.SelectedValue;
        string instructorEvaluador = txtInstructor.Text.Trim();
        string resultado = ddlResultado.SelectedValue;
        string elaborado = txtElaborado.Text.Trim();
        string iniciales = txtIniciales.Text.Trim();
        //string oficio = txtOficio.Text;
        string observacion = txtObservacion.Text.Trim();

        lblMsg.Text = string.Empty;
       
        // return lpasa;
        //lrango = lano2 - lano1;

        if (factura.Length >= 4)
        {
            factura = factura.Substring(3);
        }
        else
        {
            lblMsg.Text = "Ingrese correctamente el numero de factura (SUC+NUMERO DE FACTURA)";
            lpasa = false;
        }

        var facelec = dc.sp_validaExistenciaFacturaElectronica2(laccion, sucursal2, factura, factura, cedula);

        foreach (var regfac in facelec.ToList())
        {
            lcodigo = regfac.id_Cab_Recaudacion;
        }

        if (!validarFactura())
        {
            lpasa = false;
            lblMsg.Text = lblMsg.Text  + " El número de factura ya fue ingresada";
        }

        if (lcodigo == 0)
        {
            lpasa = false;
            //lpasa = true;
            lblMsg.Text = lblMsg.Text + " La factura en referencia no es para PPS  o no se ha registrado la cancelación";
        }

        if (numero.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese número de documento";
        }


         if ( fecha.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese la fecha del Certificado";
        }
         if ( fechaSicotecnico.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese la fecha del Sicotécnico";
        }
         if ( fechaPractico.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese la fecha del práctico";
        }
         if ( notaPractico.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese la calificación";
        }

         if (tipoDocumento == "0")
         {
             lpasa = false;
             lblMsg.Text = "Ingresese tipo de identificación";
         }
        if ( estadoPsico == "0")
        {
            lpasa = false;
            lblMsg.Text = "Ingresese el estado del sicotécnico";
        }
         if ( instructorEvaluador.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese los nombres y apellidos del evaluador";
        }
         if ( resultado == "0")
        {
            lpasa = false;
            lblMsg.Text = "Ingrese el estado del certificado";
        }
         if ( elaborado.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese los nombres y apellidos del elaborador del documento";
        }
         if ( iniciales.Length <= 0)
        {
            lpasa = false;
            lblMsg.Text = "Ingrese las iniciales del elaborador del documento";
        }
        
         if (observacion.Length <= 0)
         {
             lpasa = false;
             lblMsg.Text = "Ingrese la observación";
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

        var cUnicaFac = from TCon in de.tbl_SicoPractico
                        where TCon.factura == lfactura
                        select new { ciruc = TCon.cedula };

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
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();
    }

    protected void btnCancelar_Click()
    {
        DateTime esteDia = DateTime.Today;
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
            string sucursal = ddlSucursal.Text.Trim();
            string factura = txtNumFactura.Text.Trim();
            string cedula = txtRuc.Text.Trim();

            Session["pSucursal"] = sucursal;
            Session["pFactura"] = factura;
            Session["pCedula"] = cedula;

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirSicopractico.aspx','','width=800,height=500') </script>");
        
    }



}