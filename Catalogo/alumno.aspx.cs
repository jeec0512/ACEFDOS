using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_alumno : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();
    

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

    #region METODOS GENERALES
    protected void activarObjetos()
    {
        lblMensaje.Text = string.Empty;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";


        string sucursal = ddlSucursal.SelectedValue;
        string cedula = txtCedula.Text.Trim();
        string apellidos = txtApellidos.Text.Trim();
        string nombres = txtNombres.Text.Trim();
        string direccion = txtDireccion.Text.Trim();
        string telefono = txtTelefono.Text.Trim();
        string tipoSangre = txtTipoSangre.Text.Trim();
        string nacionalidad = txtNacionalidad.Text.Trim();
        string estadoCivil = ddlEstadoCivil.SelectedValue;
        string genero = ddlGenero.SelectedValue;
        string fechaNacimiento = Convert.ToString(txtFechaNacimiento.Text);
        string eMail = txtEmail.Text.Trim();
        string instruccionEscolar = ddlInstruccion.SelectedValue;
        string licenciaConducir = ddlLicencia.SelectedValue;
        string celular = txtCelular.Text.Trim();
        string factura = txtFactura.Text.Trim();

        bool pasa = validarDatos(sucursal, cedula,apellidos,nombres,direccion,telefono,
                                    tipoSangre,nacionalidad, estadoCivil,genero,fechaNacimiento,eMail,
                                    instruccionEscolar,licenciaConducir,celular,factura);

        if (!pasa)
        {
            
            lblMensaje.Text = "Ingrese toda la información,identificación válido,nombres, apellidos,etc ";
        }
        else
        {
              /*GUARDAR INFORMACION*/
            ds.sp_abmAlumnos(Accion,0, cedula, apellidos, nombres, direccion, telefono,
                                    tipoSangre, nacionalidad, estadoCivil, genero, Convert.ToDateTime(fechaNacimiento), eMail,
                                    instruccionEscolar, licenciaConducir,celular,factura);
                blanquearObjetos();
                lblMensaje.Text = apellidos.Trim() + " " + nombres.Trim() + " guardado correctamente";
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
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, string cedula,string apellidos,string nombres,string direccion,string telefono,
                                    string tipoSangre,string nacionalidad, string estadoCivil,string genero,string fechaNacimiento,string eMail,
                                    string instruccionEscolar,string licenciaConducir,string celular, string factura) {
        bool pasa = true;
        
        if (cedula.Length < 10
            || apellidos.Length <= 2
            || nombres.Length <= 2
            || direccion.Length <= 2
            || telefono.Length <= 2
            || tipoSangre.Length < 2
            || nacionalidad.Length <= 2
            || eMail.Length <= 2)
        {
            pasa = false;
        };


        if(estadoCivil == "-1"
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

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string lcedula = txtCedula.Text.Trim();

        if (lcedula.Length > 0)
        {
            var cAlumno = ds.sp_abmAlumnos("CONSULTAR", 0, lcedula, "", "", "", "", "", "", "", "", Convert.ToDateTime("15/10/2018"), "","", "","","");

            blanquearObjetos();
            


                foreach (var registro in cAlumno)
                {
                    lblMensaje.Text = string.Empty;
                    txtCedula.Text = registro.ALU_IDENTIFICACION;
                    txtApellidos.Text = registro.ALU_APELLIDOS;
                    txtNombres.Text = registro.ALU_NOMBRES;
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
    #endregion
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Escuela/matricularAlumno2.aspx");
    }
}