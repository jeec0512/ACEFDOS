using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using acefdos;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class Catalogo_mantenimientoPersonas : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["AdmBitaAutoConnectionString"].ConnectionString;
    Data_AdmBitaAutoDataContext da = new Data_AdmBitaAutoDataContext();

    string tOpcion = "-1";

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();
            Listar();

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

            //DateTime lfecha = DateTime.Today;
            //txtFechaIni.Text = Convert.ToString(lfecha);
            //txtFechaFin.Text = Convert.ToString(lfecha);

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

    }

    #endregion

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string lbuscar, lopcion, laccion, lcedula,ltipo;

        laccion = "";

        tOpcion = ddlTipoBusqueda.SelectedValue.Trim();

        lbuscar = txtBuscar.Text.Trim();

        lcedula = lbuscar;
        ltipo = ddlTipoMantenimiento.SelectedValue;

        if (tOpcion == "0")
        {
            ///lista clientes q cumplen condicipon de cédula
            ///
            laccion = "XCEDULA";

            if (ltipo == "1")
            {
                grvClientes.DataSource = dc.sp_buscaCliente(laccion, lbuscar);

                grvClientes.DataBind();
            }
            else if(ltipo == "2") {
                grvAlumno.DataSource = dc.sp_buscaAlumno(laccion, lbuscar);

                grvAlumno.DataBind();
            }

            /// lista historial de membrecías
            /// 
            laccion = "XCEDULA";

           
        }

        if (tOpcion == "1")
        {
            laccion = "XNOMBRE";

            ///lista clientes q cumplen condicipon de nombre
            ///

            if (ltipo == "1")
            {
                grvClientes.DataSource = dc.sp_buscaCliente(laccion, lbuscar);

                grvClientes.DataBind();
            }
            else if (ltipo == "2")
            {
                grvAlumno.DataSource = dc.sp_buscaAlumno(laccion, lbuscar);

                grvAlumno.DataBind();
            }
        }
    }

    protected void grvClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lcedula;
        string sucursal = ddlSucursal2.SelectedValue;
        int tipo = (int)Session["STipo"];
        lcedula = "";


        if (tipo == 4)
        {
            ///lista las facturas emitidas
            ///
            laccion = "TODOS";
            lcedula = Convert.ToString(grvClientes.SelectedValue);

   

        }
        else
        {  ///lista las facturas emitidas
            ///
            laccion = "XSUCXFAC";
            lcedula = Convert.ToString(grvClientes.SelectedValue);

   
        }

        /// lista historial de membrecías
        /// 
        laccion = "XCEDULA";

       

    }

    protected void grvTotalSocios_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lunico, lcedula;

        laccion = "XCODIGO";
        lunico = "";
        lcedula = "";

        ///lista productos comprados de la factura
        ///

       


        ///lista factura cancelada
        ///

        
    }

    protected void grvHistorialMembrecias_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lunico, lcedula;
        laccion = "XCEDULA";
        lunico = "";
        lcedula = "";

        ///lista auxilios mecánicos
        ///

       
    }
    
    #region LISTAR DROPDOWNLIST
   
    protected void Listar()
    {
        //NACIONALIDAD
        var cTipoSangre = ds.sp_listarTipoSangre("LISTAR");

        ddlTipoSangre.DataSource = cTipoSangre;
        ddlTipoSangre.DataBind();

        ListItem listTS = new ListItem("Seleccione Tipo de Sangre", "-1");
        ddlTipoSangre.Items.Insert(0, listTS);

        //NACIONALIDAD
        var cNacionalidad = ds.sp_listarNacionalidad("LISTAR");

        ddlNacionalidad.DataSource = cNacionalidad;
        ddlNacionalidad.DataBind();

        ListItem listNac = new ListItem("Seleccione nacionalidad", "-1");
        ddlNacionalidad.Items.Insert(0, listNac);

        //ESTADO CIVIL
        var cEstadoCivil = ds.sp_listarEstadoCivil("LISTAR");

        ddlEstadoCivil.DataSource = cEstadoCivil;
        ddlEstadoCivil.DataBind();

        ListItem listEC = new ListItem("Seleccione Estado Civil", "-1");
        ddlEstadoCivil.Items.Insert(0, listEC);
        
        //GENERO
        var cGenero = ds.sp_listarGenero("LISTAR");

        ddlGenero.DataSource = cGenero;
        ddlGenero.DataBind();

        ListItem listGe = new ListItem("Seleccione Género", "-1");
        ddlGenero.Items.Insert(0, listGe);
        //INSTRUCCIÓN ESCOLALAR
        var cInstruccionEscolar = ds.sp_listarInstruccionEscolar("LISTAR");

        ddlInstruccionEscolar.DataSource = cInstruccionEscolar;
        ddlInstruccionEscolar.DataBind();

        ListItem listIE = new ListItem("Seleccione Instrucción Escolar", "-1");
        ddlInstruccionEscolar.Items.Insert(0, listIE);

        //LICENCIA
        var cTipoLicencia = ds.sp_listarTipoLicencia("LISTAR");

        ddlTipoLicencia.DataSource = cTipoLicencia;
        ddlTipoLicencia.DataBind();

        ListItem listTL = new ListItem("Seleccione Tipo de licencia", "-1");
        ddlTipoLicencia.Items.Insert(0, listTL);

        //TIPO DE DOCUMENTO O IDENTIFICACIÓN
        var cTipoide = ds.sp_listarTipoidentificacion("LISTAR");

        ddlTipoIdentificacion.DataSource = cTipoide;
        ddlTipoIdentificacion.DataBind();

        ListItem listTI = new ListItem("Seleccione Tipo de Identificación", "-1");
        ddlTipoIdentificacion.Items.Insert(0, listTI);
        
    }

    #endregion
    protected void grvAlumno_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ruc = Convert.ToString(grvAlumno.SelectedValue);
        string ltipo = ddlTipoMantenimiento.SelectedValue;
        pnDatosAlumno.Visible = true;
        pnDatosCliente.Visible = false;
        buscapersona(ltipo,ruc);
       
    }

    protected void buscapersona(string ltipo, string ruc)
    {
        /*CLIENTE*/
        if (ltipo == "1")
        {
            var cCliente = dc.sp_abmClientes("BUSCARUC", ruc, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", DateTime.Now, "", "", 0, "");

            foreach (var registro in cCliente)
            {
                lblMensaje.Text = string.Empty;

                txtCedulaC.Text = registro.CLI_RUC;
                txtApellidoP.Text = registro.CLI_APELLIDOP;
                txtApellidoM.Text = registro.CLI_APELLIDOM;
                txtNombreC.Text = registro.CLI_NOMBRE;
                txtPrincipal.Text = registro.CLI_DIRPRINCIPAL;
                txtSecundaria.Text = registro.CLI_DIRSECUNDARIA;
                txtNumeral.Text = registro.CLI_DIRNUMERAL;
                txtSector.Text = registro.CLI_SECTOR;
                txtTelefonoC.Text = registro.CLI_TELEFONO;
                txtCelularC.Text = registro.CLI_CELULAR;
                txtEmailc.Text = registro.CLI_EMAIL;
                string obligado = registro.CLI_OBLIGADO.Trim();
                if (string.IsNullOrEmpty(obligado) || string.IsNullOrEmpty(obligado))
                {
                    ddlObligado.SelectedValue = "-1";
                }
                else
                {
                    ddlObligado.SelectedValue = obligado;
                }

                string tipoIdentificacion = Convert.ToString(registro.TID_ID);
                if (string.IsNullOrEmpty(tipoIdentificacion) || string.IsNullOrEmpty(tipoIdentificacion))
                {
                    ddlTipoIdentificacion.SelectedValue = "-1";
                }
                else {
                    ddlTipoIdentificacion.SelectedValue = tipoIdentificacion;
                }
            }


        }
        /*ALUMNO*/
        if (ltipo == "2")
        {
            var cAlumno = dc.sp_abmAlumnosEscuela("BUSCARUC", 0, ruc, "", "", "", "", "", "", "", "", DateTime.Now, "", "", "", "", "", DateTime.Now, 0, "");


            //blanquearObjetos();
            string tipoSangre = string.Empty;
            string nacionalidad = string.Empty;
            string estadoCivil = string.Empty;
            string genero = string.Empty;
            string instruccion = string.Empty;
            string tipoLicencia = string.Empty;


            foreach (var registro in cAlumno)
            {
                lblMensaje.Text = string.Empty;
                txtCedula.Text = registro.ALU_IDENTIFICACION;
                txtApellidos.Text = registro.ALU_APELLIDOS;
                txtNombres.Text = registro.ALU_NOMBRES;
                txtDireccion.Text = registro.ALU_DIRECCION;
                txtTelefono.Text = registro.ALU_TELEFONO;


                tipoSangre = registro.ALU_TIPOSANGRE;


                if (string.IsNullOrEmpty(tipoSangre) || string.IsNullOrEmpty(tipoSangre))
                {
                    ddlTipoSangre.SelectedValue = "-1";
                }
                else {
                    ddlTipoSangre.SelectedValue = tipoSangre;
                }

                nacionalidad = registro.ALU_NACIONALIDAD;

                if (string.IsNullOrEmpty(nacionalidad) || string.IsNullOrEmpty(nacionalidad))
                {
                    ddlNacionalidad.SelectedValue = registro.ALU_NACIONALIDAD;
                }
                else {
                    ddlNacionalidad.SelectedValue = nacionalidad;
                }
                estadoCivil = registro.ALU_ESTADOCIVIL;
                if (string.IsNullOrEmpty(estadoCivil) || string.IsNullOrEmpty(estadoCivil))
                {
                    ddlEstadoCivil.SelectedValue = "-1";
                }
                else {
                    ddlEstadoCivil.SelectedValue = estadoCivil;
                }

                
                genero = registro.ALU_GENERO;


                if (string.IsNullOrEmpty(genero) || string.IsNullOrEmpty(genero))
                {
                    ddlGenero.SelectedValue = "-1";
                }
                else {
                    ddlGenero.SelectedValue = genero;
                }

                    txtFechaNacimiento.Text = Convert.ToString(registro.ALU_FECHANACIMIENTO);
                    txtEmail.Text = registro.ALU_EMAIL;

                    instruccion = registro.ALU_INSTRUCCION;
                    if (string.IsNullOrEmpty(instruccion) || string.IsNullOrEmpty(instruccion))
                    {
                        ddlInstruccionEscolar.SelectedValue = "-1";
                    }
                    else {
                        ddlInstruccionEscolar.SelectedValue = instruccion;
                    }

                tipoLicencia = registro.ALU_TIPOLICENCIA;
                if (string.IsNullOrEmpty(tipoLicencia) || string.IsNullOrEmpty(tipoLicencia))
                {
                    ddlTipoLicencia.SelectedValue = "-1";
                }
                else {
                    ddlTipoLicencia.SelectedValue = tipoLicencia;
                }
                    txtFactura.Text = registro.ALU_FACTURA;

                }
            }
        }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        


        try
        {
            lblMensaje.Text = string.Empty;

            string userName = Convert.ToString(Session["SUsername"]);

            string Accion = "GUARDAR";
            //lblMensaje.Text
            string cedula = txtCedula.Text;
            string apellidos = txtApellidos.Text;
            string nombres = txtNombres.Text;
            string direccion = txtDireccion.Text;
            string telefono = txtTelefono.Text;
            string celular = txtCelular.Text;
            string tipoSangre = ddlTipoSangre.SelectedValue;
            string nacionalidad = ddlNacionalidad.SelectedValue;
            string estadoCivil = ddlEstadoCivil.SelectedValue;
            string genero = ddlGenero.SelectedValue;
            DateTime fechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text);
            string email = txtEmail.Text;
            string instruccionEscolar = ddlInstruccionEscolar.SelectedValue;
            string tipoLicencia = ddlTipoLicencia.SelectedValue;
            string factura = txtFactura.Text;
            int usuarioId = Convert.ToInt32(Session["SUsuarioID"]);

            validarDatos(cedula, apellidos, nombres, direccion, telefono, tipoSangre, nacionalidad, estadoCivil
                    , genero, fechaNacimiento, email, instruccionEscolar, tipoLicencia, celular);
              /*GUARDAR INFORMACION*/
                dc.sp_abmAlumnosEscuela(Accion,0,cedula,apellidos,nombres,direccion,telefono,tipoSangre,nacionalidad,estadoCivil
                    ,genero,fechaNacimiento,email,instruccionEscolar,tipoLicencia,celular,factura,DateTime.Now,usuarioId,"");

                lblMensaje.Text = apellidos.Trim() + " " + nombres.Trim() + " guardado correctamente";
                btnRegresar_Click();
        }
        catch (Exception ex) { 
            lblMensaje.Text = ex.Message;
            
        }


    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        pnAlumnos.Visible = false;
        pnClientes.Visible = false;
        pnDatosAlumno.Visible = false;
        pnDatosCliente.Visible = false;

        ddlTipoMantenimiento.SelectedValue = "-1";
        txtBuscar.Text = string.Empty;

        /**************************************************/
        lblMensaje.Text = string.Empty;
                txtCedulaC.Text = string.Empty;
                txtApellidoP.Text = string.Empty;
                txtApellidoM.Text = string.Empty;
                txtNombres.Text = string.Empty;
                txtPrincipal.Text = string.Empty;
                txtSecundaria.Text = string.Empty;
                txtNumeral.Text = string.Empty;
                txtSector.Text = string.Empty;
                txtTelefonoC.Text = string.Empty;
                txtCelularC.Text = string.Empty;
                txtEmailc.Text = string.Empty;
                ddlObligado.SelectedValue = "-1";

        /*ALUMNO*/
                txtCedula.Text = string.Empty;
                txtApellidos.Text = string.Empty;
                txtNombres.Text = string.Empty;
                txtDireccion.Text = string.Empty;
                txtTelefono.Text = string.Empty;
                ddlTipoSangre.SelectedValue = "-1";
                ddlNacionalidad.SelectedValue= "-1";
                ddlEstadoCivil.SelectedValue = "-1";
                ddlGenero.SelectedValue = "-1";
                txtFechaNacimiento.Text = Convert.ToString(DateTime.Now);
                txtEmail.Text = string.Empty;
                ddlInstruccionEscolar.SelectedValue = "-1";
                ddlTipoLicencia.SelectedValue = "-1";
                txtFactura.Text = string.Empty;
    }

    protected  void validarDatos(string cedula, string apellidos, string nombres, string direccion, string telefono, string tipoSangre
        , string nacionalidad, string estadoCivil, string genero, DateTime fechaNacimiento, string email, string instruccionEscolar, string tipoLicencia, string celular)
    {
        lblMensaje.Text = string.Empty;

        if (cedula.Length < 10)
        {
            throw new InvalidOperationException("Ingrese documento de identificación válido");
        }

        if (apellidos.Length <= 2
           || nombres.Length <= 2)
        {
            throw new InvalidOperationException("Ingrese nombres y apellidos válidos");
        }

        if (direccion.Length <= 2)
        {
            throw new InvalidOperationException("Ingrese dirección");
        }

        if (telefono.Length <= 2)
        {
            throw new InvalidOperationException("Ingrese número telefónico convencional");
        }

        if (celular.Length <= 2)
        {
           throw new InvalidOperationException("Ingrese número de celular");
        }

        if (!email_bien_escrito(email))
        {
            throw new InvalidOperationException("Ingrese el correo personal válido");
        }

        if (fechaNacimiento.Year <= 0)
        {
            throw new InvalidOperationException("Ingrese la fecha de nacimiento");
        }

        if (tipoSangre == "-1")
        {
            throw new InvalidOperationException("Ingrese tipo de sangre");
        }

        if (nacionalidad == "-1")
        {
            throw new InvalidOperationException("Ingrese nacionalidad");
        }

        if (estadoCivil == "-1")
        {
            throw new InvalidOperationException("Ingrese estado civil");
        }

        if (genero == "-1")
        {
            throw new InvalidOperationException("Ingrese género");
        }

        if (instruccionEscolar == "-1")
        {
            throw new InvalidOperationException("Ingrese instrucción escolar");
        }

        if (tipoLicencia == "-1")
        {
            throw new InvalidOperationException("Ingrese tipo de licencia");
        }
        
    }


    protected void validarDatosC(string cedulac, string apellido1,string apellido2, string nombres,
                        string calleP, string calleS, string Numeral, string sector,string telefonoc, string celularc, string emailc, string obligado, string tipoId)
    {
        lblMensaje.Text = string.Empty;

        if (cedulac.Length < 10)
        {
            throw new InvalidOperationException("Ingrese documento de identificación válido");
        }

        if (apellido1.Length <= 2 ||  apellido2.Length <= 2 || nombres.Length <= 2)
        {
            throw new InvalidOperationException("Ingrese nombres y apellidos válidos");
        }

        if (calleP.Length <= 2 ||  Numeral.Length <= 2 ||  calleS.Length <= 2 )
        {
            throw new InvalidOperationException("Ingrese calle principal, numeral y calle secundaria");
        }

        if (sector.Length <= 2 )
        {
            throw new InvalidOperationException("Ingrese sector de residencia");
        }

        if (telefonoc.Length <= 2)
        {
            throw new InvalidOperationException("Ingrese número telefónico convencional");
        }

        if (celularc.Length <= 2)
        {
            throw new InvalidOperationException("Ingrese número de celular");
        }

        if (!email_bien_escrito(emailc))
        {
            throw new InvalidOperationException("Ingrese el correo personal válido");
        }

        if (obligado == "-1")
        {
            throw new InvalidOperationException("Ingrese si es obligado a llevar contabilidad");
        }

        if (tipoId == "-1")
        {
            throw new InvalidOperationException("Ingrese tipo de identificación");
        }

    }


    /*VALIDA CORREOS ELECTRÓNICOS*/
    private Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    protected void btnBcliente_Click(object sender, EventArgs e)
    {
        


        try
        {
            lblMensaje.Text = string.Empty;

            string userName = Convert.ToString(Session["SUsername"]);

            string Accion = "GUARDAR";
            //lblMensaje.Text
            string cedulac = txtCedulaC.Text;
            string tipoId = ddlTipoIdentificacion.SelectedValue;
            string apellido1 = txtApellidoP.Text;
            string apellido2 = txtApellidoM.Text;
            string nombres = txtNombreC.Text;
            string calleP = txtPrincipal.Text;
            string calleS = txtSecundaria.Text;
            string Numeral = txtNumeral.Text;
            string sector = txtSector.Text;
            string telefonoc = txtTelefonoC.Text;
            string celularc = txtCelularC.Text;
            string emailc = txtEmailc.Text;
            string obligado = ddlObligado.SelectedValue;

            int usuarioId = Convert.ToInt32(Session["SUsuarioID"]);
            validarDatosC(cedulac, apellido1,apellido2, nombres, calleP,calleS,Numeral,sector,telefonoc,celularc,emailc,obligado,tipoId);
            /*GUARDAR INFORMACION*/
            dc.sp_abmClientes(Accion,cedulac, apellido1, apellido2, nombres, emailc, obligado, "", "", calleP, Numeral, calleS, sector,
                            telefonoc, cedulac, "", "", "", DateTime.Now, "", "", Convert.ToInt32(tipoId), "");

            lblMensaje.Text = apellido1.Trim() + " " + nombres.Trim() + " guardado correctamente";
            btnRegresar_Click();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;

        }
    }
    protected void grvClientes_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string ruc = Convert.ToString(grvClientes.SelectedValue);
        string ltipo = ddlTipoMantenimiento.SelectedValue;
        pnDatosAlumno.Visible = false;
        pnDatosCliente.Visible = true;
        buscapersona(ltipo, ruc);
    }
    protected void ddlTipoMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ltipo = ddlTipoMantenimiento.SelectedValue;
        if (ltipo == "-1")
        {
            pnAlumnos.Visible = false;
            pnClientes.Visible = false;
            pnDatosAlumno.Visible = false;
            pnDatosCliente.Visible = false;
        }
        if (ltipo == "1")
        {
            pnAlumnos.Visible = false;
            pnClientes.Visible = true;
            pnDatosAlumno.Visible = false;
            pnDatosCliente.Visible = false;
        }

        if (ltipo == "2")
        {
            pnAlumnos.Visible = true;
            pnClientes.Visible = false;
            pnDatosAlumno.Visible = false;
            pnDatosCliente.Visible = false;
        }
    }
    protected void btnNuevoCliente_Click(object sender, EventArgs e)
    {
        pnDatosAlumno.Visible = false;
        pnDatosCliente.Visible = true;
    }
}