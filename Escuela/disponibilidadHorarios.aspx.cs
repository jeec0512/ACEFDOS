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

public partial class Escuela_disponibilidadHorarios : System.Web.UI.Page
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

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();
            listarCiudades();
            listarEscuelas();
            listarModalidad();
            listarCurso();
        }
    }
    #endregion

    #region PERFIL USUARIO

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

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }
    }

    #endregion
    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string laccion, lsuc, lcliente, lcontrato;
        lsuc = ddlSucursal.SelectedValue.ToString();
        lcliente = txtCliente.Text.Trim();
        lcontrato = string.Empty;
        lblMensaje.Visible = false;
        lblMensaje.Text = "MSG";
        laccion = "XCLIENTE";
        cargarDatosDeCliente(laccion, lcliente, lcontrato);
        listarFactura();
    }

    protected void cargarDatosDeCliente(string laccion, string lsocio, string lcontrato)
    {
        lblNombresCliente.Text = string.Empty;
        txtEstudiante.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        txtEmail.Text = string.Empty;

        // var traeCliente = dc.sp_cargaClienteSocio(laccion, lsocio, lcontrato);
        var traeCliente = dc.sp_consultaCliente(laccion, lsocio, lcontrato);
        foreach (var regCliente in traeCliente.ToList())
        {
            txtEstudiante.Text = regCliente.ciruc;
            /* txtApellidos.Text = regCliente.apellidos;
             txtNombres.Text = regCliente.nombres;
             txtEmail.Text = regCliente.email_contac;*/
            txtCedula.Text = regCliente.ciruc;
            lblNombresCliente.Text = regCliente.nombres.Trim() + " " + regCliente.apellidos.Trim();

        }
        DateTime esteDia = DateTime.Today;


        txtFecha.Text = esteDia.ToString("d");
        btnBuscaAlumno_Click();
        ibConsultar_Click();


    }

    protected void listarFactura()
    {
        string accion = "XRUC";
        string sucursal = ddlSucursal.SelectedValue;
        DateTime fecha = DateTime.Now;
        string ruc = txtCliente.Text.Trim();
        var cFacturas = dc.sp_FacturasEmitidasXCliente(accion, sucursal, fecha, fecha, ruc);

        ddlFactura.DataSource = cFacturas;

        ddlFactura.DataBind();
    }
    protected void btnAlumno_Click(object sender, ImageClickEventArgs e)
    {
        //Response.Redirect("~/Catalogo/alumno.aspx");
        pnHojas.Visible = false;
        pnFacturaGral.Visible = false;
        pnMatricula.Visible = false;
        pnAsignacionGral.Visible = false;
        pnAcciones.Visible = false;
        pnCreaAlumno.Visible = true;
        txtCedula.Text = txtEstudiante.Text;
        btnBuscaAlumno_Click();


    }
    protected void btnBuscaAlumno_Click(object sender, ImageClickEventArgs e)
    {
        btnBuscaAlumno_Click();
    }

    protected void btnBuscaAlumno_Click()
    {
        string laccion, lsuc, lalumno;
        lsuc = ddlSucursal.SelectedValue.ToString();
        lalumno = txtEstudiante.Text.Trim();
        lblMensaje.Visible = false;
        lblMensaje.Text = "MSG";
        laccion = "CONSULTAR";
        cargarDatosDeAlumno(laccion, lalumno);
        // listarFactura();
    }

    protected void cargarDatosDeAlumno(string laccion, string lalumno)
    {
        // lblNombresCliente.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        txtEmail.Text = string.Empty;

        // var traeCliente = dc.sp_cargaClienteSocio(laccion, lsocio, lcontrato);
        var traeAlumno = ds.sp_abmAlumnos(laccion, 0, lalumno, "", "", "", "", "", "", "", "", DateTime.Now, "", "", "", "", "");
        foreach (var regCliente in traeAlumno.ToList())
        {
            //txtEstudiante.Text = regCliente.ciruc;
            txtApellidos.Text = regCliente.ALU_APELLIDOS;
            txtNombres.Text = regCliente.ALU_NOMBRES;
            txtEmail.Text = regCliente.ALU_EMAIL;
        }
        DateTime esteDia = DateTime.Today;


        txtFecha.Text = esteDia.ToString("d");

    }

    /*LISTAR CIUDAD*/

    protected void listarCiudades()
    {

        var cCiudad = dc.sp_ListarCiudades("TIPO", "ESCUELA");

        ddlCiudad.DataSource = cCiudad;
        ddlCiudad.DataBind();

        ListItem listCiu = new ListItem("Seleccione ciudad", "-1");

        ddlCiudad.Items.Insert(0, listCiu);


    }

    /*LISTAR SUCURSAL*/

    protected void listarEscuelas()
    {
        string ciudad = ddlCiudad.SelectedValue.Trim();
        string sucursal = ddlSucursal.SelectedValue;
        //var cSucursal2 = dc.sp_listarSucursal("", "", 4, 0, sucursal);
        var cSucursal2 = dc.sp_listarSucursal2("TODOS", "", 12, 0, "", ciudad);

        ddlEscuela.DataSource = cSucursal2;
        ddlEscuela.DataBind();

        ListItem listEsc = new ListItem("Seleccione escuela", "-1");

        ddlEscuela.Items.Insert(0, listEsc);
    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
  
        string accion = string.Empty;
        int materia = 0;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        
        listarCurso();
        activarObjetos(modalidad, materia);

        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        /*ddlModEducVial.SelectedValue = ddlModalidad.SelectedValue;
        ddlModMecanica.SelectedValue = ddlModalidad.SelectedValue;
        ddlModPsicologia.SelectedValue = ddlModalidad.SelectedValue;
        ddlModPrimerosAuxilios.SelectedValue = ddlModalidad.SelectedValue;
        ddlModPractica.SelectedValue = ddlModalidad.SelectedValue;*/

        ddlModEducVial_SelectedIndexChanged();
        ddlModMecanica_SelectedIndexChanged();

        ddlModPsicologia_SelectedIndexChanged();
        ddlModPrimerosAuxilios_SelectedIndexChanged();
        ddlModPractica_SelectedIndexChanged();
    }

    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurso_SelectedIndexChanged();

    }


    protected void ddlCurso_SelectedIndexChanged()
    {
       /* ddlCurEducVial.SelectedValue = ddlCurso.SelectedValue;
        ddlCurMecanica.SelectedValue = ddlCurso.SelectedValue;
        ddlCurPsicologia.SelectedValue = ddlCurso.SelectedValue;
        ddlCurPrimerosAuxilios.SelectedValue = ddlCurso.SelectedValue;
        ddlCurPractica.SelectedValue = ddlCurso.SelectedValue;*/

        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);

        if (modalidad == 1 || modalidad == 2 || modalidad == 3 || modalidad == 9 || modalidad == 10 || modalidad == 11)
        {
            horarioEducacionVial();
            horarioMecanica();
            horarioPrimerosAuxilios();
            horarioPsicologia();
            horarioPractica();

            pnPractica1.Visible = true;
            pnEducVial1.Visible = true;
            pnTalleres.Visible = true;
            pngrvAutoDetalle.Visible = true;
        }
        if (modalidad == 5 || modalidad == 6)
        {
            rHorarioEducacionVial();
            pnPractica1.Visible = false;
            pnEducVial1.Visible = true;
            pnTalleres.Visible = false;
        }

    }

    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listCon = new ListItem("Seleccione Modalidad", "-1");

        ddlModalidad.Items.Insert(0, listCon);

    }


    protected void listarCurso()
    {
        
        lblMensaje.Text = "";
        string accion = "MODACTIVOS";
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso(accion, 0, modalidad, "", "", DateTime.Now, DateTime.Now, false, "", DateTime.Now);
        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCur = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCur);
        /*
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("MODALIDAD", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);*/
    }



    protected void activarObjetos(int modalidad, int materia)
    {

        pnPractica1.Visible = false;
        pnEducVial1.Visible = false;
        pnTalleres.Visible = false;
        pngrvAutoDetalle.Visible = false;
        
        /*
        if (modalidad == 1)
        {
            //  pnDetalleMecanica.Visible = false;
            // btnMecanica.Visible = false;
        }

        pnDetalle15.Visible = false;
        pnDetalleMecanica.Visible = false;
        pnDetallePrimeroAuxilios.Visible = false;
        pnDetallePsicologia.Visible = false;
        pnDetalleAuto.Visible = false;
        /*

        /*pnAulaDetalle15.Enabled = false;
        pnAulaDetalleMecanica.Enabled = false;
        pnAulaDetallePrimeroAuxilios.Enabled = false;
        pnAulaDetallePsicologia.Enabled = false;
        pnAutoDetalle.Enabled = false;*/

        /*

        if (materia == 3)
        {
            pnDetalle15.Visible = true;
        }

        if (materia == 4)
        {
            pnDetalleMecanica.Visible = true;
        }

        if (materia == 5)
        {
            pnDetallePrimeroAuxilios.Visible = true;
        }

        if (materia == 6)
        {
            pnDetallePsicologia.Visible = true;
        }

        if (materia == 7)
        {
            pnDetalleAuto.Visible = true;
        }
         */
    }



    #region CRECION DE ALUMNO
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();

    }

    protected void ibConsultar_Click()
    {
        string lcedula = txtCedula.Text.Trim();

        if (lcedula.Length > 0)
        {
            var cAlumno = ds.sp_abmAlumnos("CONSULTAR", 0, lcedula, "", "", "", "", "", "", "", "", Convert.ToDateTime("15/10/2018"), "", "", "", "", "");

            blanquearObjetos();



            foreach (var registro in cAlumno)
            {
                lblMensaje.Text = string.Empty;
                txtCedula.Text = registro.ALU_IDENTIFICACION;
                txtApellidoAlumno.Text = registro.ALU_APELLIDOS;
                txtNombreAlumno.Text = registro.ALU_NOMBRES;
                txtDireccion.Text = registro.ALU_DIRECCION;
                txtTelefono.Text = registro.ALU_TELEFONO;
                txtTipoSangre.Text = registro.ALU_TIPOSANGRE;
                txtNacionalidad.Text = registro.ALU_NACIONALIDAD;
                ddlEstadoCivil.SelectedValue = registro.ALU_ESTADOCIVIL;
                ddlGenero.SelectedValue = registro.ALU_GENERO;
                txtFechaNacimiento.Text = Convert.ToString(registro.ALU_FECHANACIMIENTO);
                txtEmail.Text = registro.ALU_EMAIL;
                ddlInstruccion.SelectedValue = Convert.ToString(registro.ALU_INSTRUCCION);
                ddlLicencia.SelectedValue = Convert.ToString(registro.ALU_TIPOLICENCIA);

            }


            txtApellidos.Focus();
        }
        else
        {
            txtCedula.Focus();
        }

    }



    protected void blanquearObjetos()
    {
        lblMensaje.Text = string.Empty;
        txtCedula.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        txtDireccion.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        txtTipoSangre.Text = string.Empty;
        txtNacionalidad.Text = string.Empty;
        ddlEstadoCivil.SelectedValue = "-1";
        ddlGenero.SelectedValue = "-1";
        txtFechaNacimiento.Text = Convert.ToString(DateTime.Today);
        txtEmail.Text = string.Empty;
        ddlInstruccion.SelectedValue = "-1";
        ddlLicencia.SelectedValue = "-1";

    }

    protected void btnGuardarAlumno_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";


        string sucursal = ddlSucursal.SelectedValue;
        string cedula = txtCedula.Text.Trim();
        string apellidos = txtApellidoAlumno.Text.Trim();
        string nombres = txtNombreAlumno.Text.Trim();
        string direccion = txtDireccion.Text.Trim();
        string telefono = txtTelefono.Text.Trim();
        string tipoSangre = txtTipoSangre.Text.Trim();
        string nacionalidad = txtNacionalidad.Text.Trim();
        string estadoCivil = ddlEstadoCivil.SelectedValue;
        string genero = ddlGenero.SelectedValue;
        string fechaNacimiento = Convert.ToString(txtFechaNacimiento.Text);
        string eMail = txtEmailAlumno.Text.Trim();
        string instruccionEscolar = ddlInstruccion.SelectedValue;
        string licenciaConducir = ddlLicencia.SelectedValue;
        string celular = txtCelular.Text.Trim();
        string factura = txtFactura.Text.Trim();

        bool pasa = validarDatos(sucursal, cedula, apellidos, nombres, direccion, telefono,
                                    tipoSangre, nacionalidad, estadoCivil, genero, fechaNacimiento, eMail,
                                    instruccionEscolar, licenciaConducir, celular, factura);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información,identificación válido,nombres, apellidos,etc ";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmAlumnos(Accion, 0, cedula, apellidos, nombres, direccion, telefono,
                                    tipoSangre, nacionalidad, estadoCivil, genero, Convert.ToDateTime(fechaNacimiento), eMail,
                                    instruccionEscolar, licenciaConducir, celular, factura);
            blanquearObjetosAlumno();
            lblMensaje.Text = apellidos.Trim() + " " + nombres.Trim() + " guardado correctamente";
            btnRegresar_Click();
            btnBuscaAlumno_Click();
        }


    }
    protected void blanquearObjetosAlumno()
    {
        lblMensaje.Text = string.Empty;
        txtCedula.Text = string.Empty;
        txtApellidoAlumno.Text = string.Empty;
        txtNombreAlumno.Text = string.Empty;
        txtDireccion.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        txtTipoSangre.Text = string.Empty;
        txtNacionalidad.Text = string.Empty;
        ddlEstadoCivil.SelectedValue = "-1";
        ddlGenero.SelectedValue = "-1";
        txtFechaNacimiento.Text = Convert.ToString(DateTime.Today);
        txtEmailAlumno.Text = string.Empty;
        ddlInstruccion.SelectedValue = "-1";
        ddlLicencia.SelectedValue = "-1";

    }

    protected bool validarDatos(string sucursal, string cedula, string apellidos, string nombres, string direccion, string telefono,
                                    string tipoSangre, string nacionalidad, string estadoCivil, string genero, string fechaNacimiento, string eMail,
                                    string instruccionEscolar, string licenciaConducir, string celular, string factura)
    {
        bool pasa = true;

        if (cedula.Length < 10
            || apellidos.Length <= 2
            || nombres.Length <= 2
            || direccion.Length <= 2
            || telefono.Length <= 2
            || tipoSangre.Length < 2
            || nacionalidad.Length <= 1
            || eMail.Length <= 2)
        {
            pasa = false;
        };


        if (estadoCivil == "-1"
            || genero == "-1"
            || instruccionEscolar == "-1"
            || licenciaConducir == "-1")
        {
            pasa = false;
        }


        if (fechaNacimiento.Length <= 2)
        {
            pasa = false;
        }
        return pasa;

    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        // Response.Redirect("~/Escuela/matricularAlumno2.aspx");
        pnHojas.Visible = true;
        pnFacturaGral.Visible = true;
        pnMatricula.Visible = true;
        pnAsignacionGral.Visible = true;
        pnAcciones.Visible = true;
        pnCreaAlumno.Visible = true;
        txtCedula.Text = txtEstudiante.Text;
    }

    #endregion

    #region LISTADOS DE CURSOS POR MATERIA

    protected void ddlModEducVial_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModEducVial_SelectedIndexChanged();

    }

    protected void ddlModEducVial_SelectedIndexChanged()
    {
        /*string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);*/
        //listarCursoEduc();
        horarioEducacionVial();


    }

    protected void ddlModMecanica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModMecanica_SelectedIndexChanged();
    }
    protected void ddlModMecanica_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //listarCursoMec();
        horarioMecanica();
    }
    protected void ddlModPrimerosAuxilios_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModPrimerosAuxilios_SelectedIndexChanged();
    }

    protected void ddlModPrimerosAuxilios_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //listarCursoPA();
        horarioPrimerosAuxilios();
    }

    protected void ddlModPsicologia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModPsicologia_SelectedIndexChanged();
    }

    protected void ddlModPsicologia_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //listarCursoPs();
        horarioPsicologia();
    }
    protected void ddlModPractica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModPractica_SelectedIndexChanged();
        horarioPractica();
    }

    protected void ddlModPractica_SelectedIndexChanged()
    {
        string accion = string.Empty;
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        //listarCursoPr();
    }


    /*protected void listarCursoEduc()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }*/

    /*protected void listarCursoMec()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCursoM = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCursoM;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }*/


   /* protected void listarCursoPs()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }*/

    /*protected void listarCursoPA()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }*/

    /*protected void listarCursoPr()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }*/
    #endregion

    #region HORARIOS ASIGNADOS POR CURSO Y MATERIA
    protected void horarioEducacionVial()
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*EDUCACION BASICA*/
        materia = 3;

        //var cAulas15 = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        var cAulas15 = ds.sp_DisponibilidadEducacionVial(accion, sucursal, curso, modalidad);

        grvAulaDetalle15.DataSource = cAulas15;
        grvAulaDetalle15.DataBind();


        var cHoras15 = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        //grvHorarioDetalle15.DataSource = cHoras15;
        //grvHorarioDetalle15.DataBind();

        activarObjetos(modalidad, materia);
    }

    protected void horarioMecanica()
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*MECANICA*/
        materia = 4;
        //var cAulasMec = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        var cAulasMec = ds.sp_DisponibilidadMecanica(accion, sucursal, curso, modalidad);

        grvAulaDetalleMecanica.DataSource = cAulasMec;
        grvAulaDetalleMecanica.DataBind();


        /* var cHorasMec = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

         grvHorarioDetalleMecanica.DataSource = cHorasMec;
         grvHorarioDetalleMecanica.DataBind();

         var cFechasMec = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

         grvFechasDetalleMecanica.DataSource = cFechasMec;
         grvFechasDetalleMecanica.DataBind();

         activarObjetos(modalidad, materia);*/
    }

    protected void horarioPrimerosAuxilios()
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);
        /*PRIMEROS AUXILIOS*/
        materia = 6;

        //var cAulasPA = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        var cAulasPA = ds.sp_DisponibilidadPrimerosAuxilios(accion, sucursal, curso, modalidad);


        grvAulaDetallePrimeroAuxilios.DataSource = cAulasPA;
        grvAulaDetallePrimeroAuxilios.DataBind();

        /*
        var cHorasPA = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetallePrimeroAuxilios.DataSource = cHorasPA;
        grvHorarioDetallePrimeroAuxilios.DataBind();

        var cFechasPA = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

        grvFechasDetallePrimeroAuxilios.DataSource = cFechasPA;
        grvFechasDetallePrimeroAuxilios.DataBind();

        activarObjetos(modalidad, materia);
         * */
    }

    protected void horarioPsicologia()
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);
        /*PSICOLOGIA*/
        materia = 6;

        var cAulasPs = ds.sp_DisponibilidadPsicologia(accion, sucursal, curso, modalidad);
        grvAulaDetallePsicologia.DataSource = cAulasPs;
        grvAulaDetallePsicologia.DataBind();

        /*
        var cHorasPs = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        grvHorarioDetallePsicologia.DataSource = cHorasPs;
        grvHorarioDetallePsicologia.DataBind();

        var cFechasPs = ds.sp_HorariosAsignadosTalleres(accion, sucursal, curso, materia);

        grvFechasDetallePsicologia.DataSource = cFechasPs;
        grvFechasDetallePsicologia.DataBind();

        activarObjetos(modalidad, materia);
         */
    }

    protected void horarioPractica()
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);

        /*PRACTICA*/
        materia = 7;


        //var cAuto = ds.sp_HorariosAsignadosVehiculos(accion, sucursal, curso, materia);
        var cAuto = ds.sp_DisponibilidadPractica(accion, sucursal, curso, modalidad);

        grvAutoDetalle.DataSource = cAuto;
        grvAutoDetalle.DataBind();

        // accion = "DISPAUTOS";

        accion = "DISPONIBLE";
        var cHorasAuto = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        //grvHorarioDetalleAuto.DataSource = cHorasAuto;
        //grvHorarioDetalleAuto.DataBind();

        activarObjetos(modalidad, materia);

    }
    #endregion

    #region BOTONES PESTAÑAS
    protected void btnCliente_Click(object sender, EventArgs e)
    {
        ddlCurso_SelectedIndexChanged();

        /* bool visible = pnFacturaGral.Visible;
         if (visible)
         {
             pnFacturaGral.Visible = false;
             btnCliente.Text = "Ver Cliente";
         }
         else
         {
             pnFacturaGral.Visible = true;
             btnCliente.Text = "Ocultar Cliente";
         }*/
    }
    protected void btnMatricula_Click(object sender, EventArgs e)
    {
        bool visible = pnMatricula.Visible;
        if (visible)
        {
            pnMatricula.Visible = false;
            btnMatricula.Text = "Ver Matrícula";
        }
        else
        {
            pnMatricula.Visible = true;
            btnMatricula.Text = "Ocultar Matrícula";
        }
    }
    protected void btnHorario_Click(object sender, EventArgs e)
    {
        bool visible = pnAsignacionGral.Visible;
        if (visible)
        {
            pnAsignacionGral.Visible = false;
            btnHorario.Text = "Ver Horarios";
        }
        else
        {
            pnAsignacionGral.Visible = true;
            btnHorario.Text = "Ocultar Horarios";
        }
    }
    protected void btn_abmAlumno_Click(object sender, EventArgs e)
    {


        bool visible = pnCreaAlumno.Visible;
        if (visible)
        {
            pnCreaAlumno.Visible = false;
            btn_abmAlumno.Text = "Ver Alumno";
        }
        else
        {
            pnCreaAlumno.Visible = true;
            btn_abmAlumno.Text = "Ocultar Alumno";
        }

    }
    #endregion


    #region AULAS HORARIOS TALLERES ESCOGIDOS PARA ASIGNASR

    /*EDUCACION VIAL*/
    protected void grvAulaDetalle15_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetalle15_RowDataBound(0);
    }
    protected void grvAulaDetalle15_RowDataBound(int indice)
    {
        bool estado;
        /*for (int i = 0; i < grvAulaDetalle15.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetalle15.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetalle15.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetalle15.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetalle15.Rows[i].BackColor = Color.White;
                grvAulaDetalle15.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvAulaDetalle15_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "aula15")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalle15.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);


            grvAulaDetalle15_RowDataBound(indice);
        }
    }


    protected void grvHorarioDetalle15_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalle15_RowDataBound(0);
    }
    protected void grvHorarioDetalle15_RowDataBound(int indice)
    {
        bool estado;
       /* for (int i = 0; i < grvHorarioDetalle15.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalle15.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalle15.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalle15.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalle15.Rows[i].BackColor = Color.White;
                grvHorarioDetalle15.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvHorarioDetalle15_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "horario15")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
         //   GridViewRow row = grvHorarioDetalle15.Rows[indice];
          //  row.Cells[0].Text = Convert.ToString(true);


            grvHorarioDetalle15_RowDataBound(indice);
        }
    }

    /*MECANICA*/
    protected void grvAulaDetalleMecanica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetalleMecanica_RowDataBound(0);
    }

    protected void grvAulaDetalleMecanica_RowDataBound(int indice)
    {
        /*
        bool estado;
        for (int i = 0; i < grvAulaDetalleMecanica.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetalleMecanica.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetalleMecanica.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetalleMecanica.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetalleMecanica.Rows[i].BackColor = Color.White;
                grvAulaDetalleMecanica.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }

    protected void grvAulaDetalleMecanica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mecAula")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalleMecanica.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAulaDetalleMecanica_RowDataBound(indice);
        }
    }
    protected void grvHorarioDetalleMecanica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalleMecanica_RowDataBound(0);
    }

    protected void grvHorarioDetalleMecanica_RowDataBound(int indice)
    {
        bool estado;
        /*for (int i = 0; i < grvHorarioDetalleMecanica.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalleMecanica.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalleMecanica.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalleMecanica.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalleMecanica.Rows[i].BackColor = Color.White;
                grvHorarioDetalleMecanica.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvHorarioDetalleMecanica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mecHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
           // GridViewRow row = grvHorarioDetalleMecanica.Rows[indice];
            //row.Cells[0].Text = Convert.ToString(true);

            //grvHorarioDetalleMecanica_RowDataBound(indice);
        }
    }
    protected void grvFechasDetalleMecanica_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvFechasDetalleMecanica_RowDataBound(0);
    }

    protected void grvFechasDetalleMecanica_RowDataBound(int indice)
    {
        bool estado;
        /*for (int i = 0; i < grvFechasDetalleMecanica.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvFechasDetalleMecanica.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvFechasDetalleMecanica.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvFechasDetalleMecanica.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvFechasDetalleMecanica.Rows[i].BackColor = Color.White;
                grvFechasDetalleMecanica.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvFechasDetalleMecanica_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "mecFecha")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
          //  GridViewRow row = grvHorarioDetalleMecanica.Rows[indice];
          //  row.Cells[0].Text = Convert.ToString(true);

           // grvHorarioDetalleMecanica_RowDataBound(indice);
        }
    }



    /*PRIMEROS AUXILIOS*/
    protected void grvAulaDetallePrimeroAuxilios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetallePrimeroAuxilios_RowDataBound(0);
    }
    protected void grvAulaDetallePrimeroAuxilios_RowDataBound(int indice)
    {
        /*
        bool estado;
        for (int i = 0; i < grvAulaDetallePrimeroAuxilios.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetallePrimeroAuxilios.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetallePrimeroAuxilios.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetallePrimeroAuxilios.Rows[i].BackColor = Color.White;
                grvAulaDetallePrimeroAuxilios.Rows[i].ForeColor = Color.Black;

            }
        }
         */
    }
    protected void grvAulaDetallePrimeroAuxilios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "paAula")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetallePrimeroAuxilios.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAulaDetallePrimeroAuxilios_RowDataBound(indice);
        }
    }
    protected void grvHorarioDetallePrimeroAuxilios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetallePrimeroAuxilios_RowDataBound(0);
    }
    protected void grvHorarioDetallePrimeroAuxilios_RowDataBound(int indice)
    {
        bool estado;
        /*for (int i = 0; i < grvHorarioDetallePrimeroAuxilios.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetallePrimeroAuxilios.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetallePrimeroAuxilios.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetallePrimeroAuxilios.Rows[i].BackColor = Color.White;
                grvHorarioDetallePrimeroAuxilios.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvHorarioDetallePrimeroAuxilios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "paHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
           // GridViewRow row = grvHorarioDetallePrimeroAuxilios.Rows[indice];
            //row.Cells[0].Text = Convert.ToString(true);

            //grvHorarioDetallePrimeroAuxilios_RowDataBound(indice);
        }
    }
    protected void grvFechasDetallePrimeroAuxilios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvFechasDetallePrimeroAuxilios_RowDataBound(0);
    }

    protected void grvFechasDetallePrimeroAuxilios_RowDataBound(int indice)
    {
        bool estado;
        /*for (int i = 0; i < grvFechasDetallePrimeroAuxilios.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvFechasDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvFechasDetallePrimeroAuxilios.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvFechasDetallePrimeroAuxilios.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvFechasDetallePrimeroAuxilios.Rows[i].BackColor = Color.White;
                grvFechasDetallePrimeroAuxilios.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvFechasDetallePrimeroAuxilios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "paFecha")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = grvFechasDetallePrimeroAuxilios.Rows[indice];
            //row.Cells[0].Text = Convert.ToString(true);

            grvFechasDetallePrimeroAuxilios_RowDataBound(indice);
        }
    }
    /*PSICOLOGIA*/

    protected void grvAulaDetallePsicologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetallePsicologia_RowDataBound(0);
    }
    protected void grvAulaDetallePsicologia_RowDataBound(int indice)
    {
        /*
        bool estado;
        for (int i = 0; i < grvAulaDetallePsicologia.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetallePsicologia.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAulaDetallePsicologia.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetallePsicologia.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAulaDetallePsicologia.Rows[i].BackColor = Color.White;
                grvAulaDetallePsicologia.Rows[i].ForeColor = Color.Black;

            }
        }
         */
    }
    protected void grvAulaDetallePsicologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "psiAula")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetallePsicologia.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAulaDetallePsicologia_RowDataBound(indice);
        }
    }

    protected void grvHorarioDetallePsicologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetallePsicologia_RowDataBound(0);
    }
    protected void grvHorarioDetallePsicologia_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetallePsicologia.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetallePsicologia.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetallePsicologia.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetallePsicologia.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetallePsicologia.Rows[i].BackColor = Color.White;
                grvHorarioDetallePsicologia.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvHorarioDetallePsicologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "psiHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetallePsicologia.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetallePsicologia_RowDataBound(indice);
        }
    }


    protected void grvFechasDetallePsicologia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvFechasDetallePsicologia_RowDataBound(0);
    }
    protected void grvFechasDetallePsicologia_RowDataBound(int indice)
    {
        bool estado;
        for (int i = 0; i < grvFechasDetallePsicologia.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvFechasDetallePsicologia.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvFechasDetallePsicologia.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvFechasDetallePsicologia.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvFechasDetallePsicologia.Rows[i].BackColor = Color.White;
                grvFechasDetallePsicologia.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvFechasDetallePsicologia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "psiFecha")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvFechasDetallePsicologia.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvFechasDetallePsicologia_RowDataBound(indice);
        }
    }
    /*PRACTICA*/

    protected void grvAutoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAutoDetalle_RowDataBound(0);
    }
    protected void grvAutoDetalle_RowDataBound(int indice)
    {
        /*
        bool estado;
        for (int i = 0; i < grvAutoDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAutoDetalle.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvAutoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAutoDetalle.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvAutoDetalle.Rows[i].BackColor = Color.White;
                grvAutoDetalle.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }
    protected void grvAutoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pracAuto")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutoDetalle.Rows[indice];
            row.Cells[0].Text = Convert.ToString(true);

            grvAutoDetalle_RowDataBound(indice);
        }
    }

    protected void grvHorarioDetalleAuto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalleAuto_RowDataBound(0);
    }
    protected void grvHorarioDetalleAuto_RowDataBound(int indice)
    {
        bool estado;
        /*for (int i = 0; i < grvHorarioDetalleAuto.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalleAuto.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalleAuto.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalleAuto.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalleAuto.Rows[i].BackColor = Color.White;
                grvHorarioDetalleAuto.Rows[i].ForeColor = Color.Black;

            }
        }*/
    }

    protected void grvHorarioDetalleAuto_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pracHora")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = grvHorarioDetalleAuto.Rows[indice];
            //row.Cells[0].Text = Convert.ToString(true);

            grvHorarioDetalleAuto_RowDataBound(indice);
        }
    }

    #endregion

    #region DESENLACE POR MATERIA Y CURSO

    protected void ddlCurEducVial_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurEducVial_SelectedIndexChanged();
        horarioEducacionVial();
    }

    protected void ddlCurEducVial_SelectedIndexChanged()
    {

    }


    protected void ddlCurMecanica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurMecanica_SelectedIndexChanged();
        horarioMecanica();
    }

    protected void ddlCurMecanica_SelectedIndexChanged()
    {

    }
    protected void ddlCurPrimerosAuxilios_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurPrimerosAuxilios_SelectedIndexChanged();
        horarioPrimerosAuxilios();
    }
    protected void ddlCurPrimerosAuxilios_SelectedIndexChanged()
    {

    }
    protected void ddlCurPsicologia_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurPsicologia_SelectedIndexChanged();
        horarioPsicologia();
    }
    protected void ddlCurPsicologia_SelectedIndexChanged()
    {

    }

    protected void ddlCurPractica_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurPractica_SelectedIndexChanged();
        horarioPractica();
    }

    protected void ddlCurPractica_SelectedIndexChanged()
    {

    }
    #endregion

    #region VALIDAR DATOS
    protected bool validarDatos()
    {
        bool pasa = true;
        string factura = ddlFactura.SelectedValue.Trim();
        string estudiante = txtEstudiante.Text.Trim();
        string escuela = ddlEscuela.SelectedValue.Trim();
        DateTime fecha = Convert.ToDateTime(txtFecha.Text);
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);

        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int educVial = verificarHorario(escuela, curso, 3);
        int mec = verificarHorario(escuela, curso, 4); ;
        int psic = verificarHorario(escuela, curso, 5); ;
        int primAux = verificarHorario(escuela, curso, 6); ;
        int prac = verificarHorario(escuela, curso, 7); ;
        int usuario = Convert.ToInt32(Session["SUsuarioID"]);

        if (factura.Length <= 0 || estudiante.Length <= 0 || escuela.Length <= 0)
        {
            pasa = false;
        }

        if (modalidad == 1)
        {
            pasa = true;
        }
        else
        {
            if (mec == 0)
            {
                pasa = false;
            }
        }

        if (curso == 0 || educVial == 0 || psic == 0 || primAux == 0 || prac == 0)
        {
            pasa = false;
        }

        return pasa;

    }

    protected int verificarHorario(string escuela, int curso, int materia)
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string accion = "ID";
        bool estado1 = false;
        bool estado2 = false;
        bool estado3 = false;
        int aul_id = 0;
        int veh_id = 0;
        int hor_id = 0;
        int tal_id = 0;
        int asm_id = 0;

        /*ASIGNACION POR MATERIA*/

        /*EDUCACION VIAL*/
        if (materia == 3)
        {
            /*AULA ASIGNADA*/
            for (int i = 0; i < grvAulaDetalle15.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAulaDetalle15.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    aul_id = Convert.ToInt32(grvAulaDetalle15.Rows[i].Cells[2].Text);
                }
            }

            /*HORARIO ASIGNADO*/
            /*for (int j = 0; j < grvHorarioDetalle15.Rows.Count; j++)
            {
                estado2 = Convert.ToBoolean(grvHorarioDetalle15.Rows[j].Cells[0].Text);

                if (estado2)
                {
                    hor_id = Convert.ToInt32(grvHorarioDetalle15.Rows[j].Cells[2].Text);
                }
            }*/

            var cid = ds.sp_HorarioEducacionVial(accion, escuela, curso, aul_id, hor_id);
            foreach (var registro in cid)
            {
                asm_id = Convert.ToInt32(registro.ASM_ID);

            }
        }

        /*MECANICA*/
        if (modalidad == 1)
        {
            if (materia == 4)
            {
                for (int i = 0; i < grvAulaDetalleMecanica.Rows.Count; i++)
                {
                    estado1 = Convert.ToBoolean(grvAulaDetalleMecanica.Rows[i].Cells[0].Text);

                    if (estado1)
                    {
                        aul_id = Convert.ToInt32(grvAulaDetalleMecanica.Rows[i].Cells[2].Text);
                    }
                }

               /* for (int j = 0; j < grvHorarioDetalleMecanica.Rows.Count; j++)
                {
                    estado2 = Convert.ToBoolean(grvHorarioDetalleMecanica.Rows[j].Cells[0].Text);

                    if (estado2)
                    {
                        hor_id = Convert.ToInt32(grvHorarioDetalleMecanica.Rows[j].Cells[2].Text);
                    }
                }*/

            }
            var cid = ds.sp_HorarioMecanica(accion, escuela, curso, aul_id, hor_id, 0);
            foreach (var registro in cid)
            {
                asm_id = Convert.ToInt32(registro.ASM_ID);

            }
        }
        else
        {
            if (materia == 4)
            {
                for (int i = 0; i < grvAulaDetalleMecanica.Rows.Count; i++)
                {
                    estado1 = Convert.ToBoolean(grvAulaDetalleMecanica.Rows[i].Cells[0].Text);

                    if (estado1)
                    {
                        aul_id = Convert.ToInt32(grvAulaDetalleMecanica.Rows[i].Cells[2].Text);
                    }
                }
                /*for (int j = 0; j < grvHorarioDetalleMecanica.Rows.Count; j++)
                {
                    estado2 = Convert.ToBoolean(grvHorarioDetalleMecanica.Rows[j].Cells[0].Text);

                    if (estado2)
                    {
                        hor_id = Convert.ToInt32(grvHorarioDetalleMecanica.Rows[j].Cells[2].Text);
                    }
                }
                for (int K = 0; K < grvFechasDetalleMecanica.Rows.Count; K++)
                {
                    estado3 = Convert.ToBoolean(grvFechasDetalleMecanica.Rows[K].Cells[0].Text);

                    if (estado3)
                    {
                        tal_id = Convert.ToInt32(grvFechasDetalleMecanica.Rows[K].Cells[2].Text);
                    }
                }*/
                var cid = ds.sp_HorarioMecanica(accion, escuela, curso, aul_id, hor_id, tal_id);
                foreach (var registro in cid)
                {
                    asm_id = Convert.ToInt32(registro.ASM_ID);

                }

            }
        }

        /*PSICOLOGIA*/
        if (materia == 5)
        {

            for (int i = 0; i < grvAulaDetallePsicologia.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAulaDetallePsicologia.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    aul_id = Convert.ToInt32(grvAulaDetallePsicologia.Rows[i].Cells[2].Text);
                }

                for (int j = 0; j < grvHorarioDetallePsicologia.Rows.Count; j++)
                {
                    estado2 = Convert.ToBoolean(grvHorarioDetallePsicologia.Rows[j].Cells[0].Text);

                    if (estado2)
                    {
                        hor_id = Convert.ToInt32(grvHorarioDetallePsicologia.Rows[j].Cells[2].Text);

                    }
                }
            }
            for (int K = 0; K < grvFechasDetallePsicologia.Rows.Count; K++)
            {
                estado3 = Convert.ToBoolean(grvFechasDetallePsicologia.Rows[K].Cells[0].Text);

                if (estado3)
                {
                    tal_id = Convert.ToInt32(grvFechasDetallePsicologia.Rows[K].Cells[2].Text);
                }
            }
            var cid = ds.sp_HorarioPsicologia(accion, escuela, curso, aul_id, hor_id, tal_id);
            foreach (var registro in cid)
            {
                asm_id = Convert.ToInt32(registro.ASM_ID);

            }
        }


        /*PSICOLOGIA*/
        if (materia == 6)
        {

            for (int i = 0; i < grvAulaDetallePrimeroAuxilios.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAulaDetallePrimeroAuxilios.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    aul_id = Convert.ToInt32(grvAulaDetallePrimeroAuxilios.Rows[i].Cells[2].Text);
                }

                /*for (int j = 0; j < grvHorarioDetallePrimeroAuxilios.Rows.Count; j++)
                {
                    estado2 = Convert.ToBoolean(grvHorarioDetallePrimeroAuxilios.Rows[j].Cells[0].Text);

                    if (estado2)
                    {
                        hor_id = Convert.ToInt32(grvHorarioDetallePrimeroAuxilios.Rows[j].Cells[2].Text);

                    }
                }*/
            }
            /*for (int K = 0; K < grvFechasDetallePrimeroAuxilios.Rows.Count; K++)
            {
                estado3 = Convert.ToBoolean(grvFechasDetallePrimeroAuxilios.Rows[K].Cells[0].Text);

                if (estado3)
                {
                    tal_id = Convert.ToInt32(grvFechasDetallePrimeroAuxilios.Rows[K].Cells[2].Text);
                }
            }*/
            var cid = ds.sp_HorarioPrimerosAuxilios(accion, escuela, curso, aul_id, hor_id, tal_id);
            foreach (var registro in cid)
            {
                asm_id = Convert.ToInt32(registro.ASM_ID);

            }
        }


        /*PRACTICA*/

        if (materia == 7)
        {

            for (int i = 0; i < grvAutoDetalle.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAutoDetalle.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    veh_id = Convert.ToInt32(grvAutoDetalle.Rows[i].Cells[2].Text);
                }
            }
            /*for (int j = 0; j < grvHorarioDetalleAuto.Rows.Count; j++)
            {
                estado2 = Convert.ToBoolean(grvHorarioDetalleAuto.Rows[j].Cells[0].Text);

                if (estado2)
                {
                    hor_id = Convert.ToInt32(grvHorarioDetalleAuto.Rows[j].Cells[2].Text);
                }
            }*/
            var cid = ds.sp_HorarioPractica(accion, escuela, curso, veh_id, hor_id);
            foreach (var registro in cid)
            {
                asm_id = Convert.ToInt32(registro.ASM_ID);

            }
        }
        return asm_id;
    }
    #endregion

    #region GUARDAR EN LAS TABLAS RESPECTIVAS
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;
        string accion = "AGREGAR";
        string factura = ddlFactura.SelectedValue.Trim();
        string estudiante = txtEstudiante.Text.Trim();
        string escuela = ddlEscuela.SelectedValue.Trim();
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string nombres = txtNombres.Text;
        string apellidos = txtApellidos.Text;
        DateTime fecha = Convert.ToDateTime(txtFecha.Text);
        string observacion = lblMensaje2.Text.Trim();



        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int educVial = verificarHorario(escuela, curso, 3);
        int mec = verificarHorario(escuela, curso, 4); ;
        int psic = verificarHorario(escuela, curso, 5); ;
        int primAux = verificarHorario(escuela, curso, 6); ;
        int prac = verificarHorario(escuela, curso, 7); ;
        string usuario = Convert.ToString(Session["SUsuarioID"]);
        int usu_id = Convert.ToInt32(Session["SUsuarioID"]);
        int alu_id = 0;
        int reg_id = 0;

        var cIdalumno = ds.sp_abmAlumnos("CONSULTAR", 0, estudiante, "", "", "", "", "", "", "", "", DateTime.Now, "", "", "", "", "");
        foreach (var registro in cIdalumno)
        {
            alu_id = registro.ALU_ID;

        }

        bool pasa = validarDatos();




        if (pasa)
        {
            lblMensaje.Text = "SI PASA";
            ds.sp_abmRegistroAlumno(accion, 0, alu_id, fecha, observacion, factura, usuario, fecha, curso, "", escuela, 0, "", 0, "");
            var cRegDetalle = ds.sp_abmRegistroAlumno("CONSULTAR", 0, alu_id, DateTime.Now, "", "", "", DateTime.Now, curso, "", escuela, 0, "", 0, "");
            foreach (var registro in cRegDetalle)
            {
                reg_id = registro.REG_ID;

            }

            accion = "AGREGAR";
            ds.sp_abmRegistroAlumnoDetalle(accion, 0, reg_id, educVial, "");
            if (modalidad != 1)
            {
                ds.sp_abmRegistroAlumnoDetalle(accion, 0, reg_id, mec, "");
            }
            ds.sp_abmRegistroAlumnoDetalle(accion, 0, reg_id, psic, "");
            ds.sp_abmRegistroAlumnoDetalle(accion, 0, reg_id, primAux, "");
            ds.sp_abmRegistroAlumnoDetalle(accion, 0, reg_id, prac, "");

            /*CONSOLIDADO*/
            ds.sp_abmRegistroNota_Con(accion, 0, reg_id, estudiante, apellidos, nombres, "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, factura, false, false, "", "", "", "", "", "", "", usu_id, "", 0, "", false, false);

        }
        else
        {
            lblMensaje.Text = "NO PASA";
        }
    }
    #endregion
    protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarEscuelas();
    }


    /*RECUPERACION DE PUNTOS*/
    protected void rHorarioEducacionVial()
    {
        string accion = "DISPONIBLE";
        int materia = 0;
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);

        //activarObjetos(modalidad,materia);


        /*EDUCACION BASICA*/
        materia = 8;

        //var cAulas15 = ds.sp_HorariosAsignadosAulas(accion, sucursal, curso, materia);
        var cAulas15 = ds.sp_DisponibilidadEducacionVial(accion, sucursal, curso, modalidad);

        grvAulaDetalle15.DataSource = cAulas15;
        grvAulaDetalle15.DataBind();


        var cHoras15 = ds.sp_HorariosAsignadosHoras(accion, sucursal, curso, materia);

        //grvHorarioDetalle15.DataSource = cHoras15;
        //grvHorarioDetalle15.DataBind();

        activarObjetos(modalidad, materia);
    }



}