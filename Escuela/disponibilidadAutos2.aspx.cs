using AjaxControlToolkit;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Escuela_disponibilidadAutos2 : System.Web.UI.Page
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
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        listarCurso();
       
    }

    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurso_SelectedIndexChanged();

    }


    protected void ddlCurso_SelectedIndexChanged()
    {
        /*VARIABLES OBJETO*/
        var cAutos15 = new object();

        /**/
        lblFechIni.Text = "Fecha de inicio:";
        lblFechFin.Text = "Fecha de finalización:";

        string accion = "consulta";
        string sucursal = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = 7;

        try
        {
            var cCurso = from TCurso in ds.TB_CURSO
                         where TCurso.CUR_ID == curso
                         select new
                         {
                             fechaInicio = TCurso.CUR_FECHA_INICIO,
                             fechaFin = TCurso.CUR_FECHA_FIN
                         };


            if (cCurso.Count() == 0)
            {

                lblMensaje.Text = "No existe curso";
            }
            else
            {
                foreach (var registro in cCurso)
                {
                    lblFechIni.Text += " " + Convert.ToString(registro.fechaInicio).Substring(0, 10);
                    lblFechFin.Text += " " + Convert.ToString(registro.fechaFin).Substring(0, 10);
                }
            }
/****************MODALIDADES POR SUCURSAL***********************************************************/
            /*************AUTOS*/
            if (modalidad == 1)
            {
                pnAuto15.Visible = true;
                pnAuto7.Visible = false;
                pnAutoFS.Visible = false;

                pnMotoAM10.Visible = false;
                pnMotoAM10.Visible = false;
                pnMotoMF.Visible = false;
                // string sp = "ds.sp_autosUtilizadosEjecutar15x"+sucursal+"(accion, sucursal, materia, curso)";

                try
                {
                    //cAutos15 = ds.sp_autosUtilizadosEjecutar15xSuc(accion, sucursal, materia, curso);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=192.168.1.106\ANETA;Initial Catalog=Escuela;Persist Security Info=True;User ID=sistemaerp;Password=s1st3m43rp");

                    SqlCommand sqlComm = new SqlCommand();

                    sqlComm.Connection = sqlCon;

                    sqlComm.CommandTimeout = 180;

                    sqlComm.CommandText = "sp_autosUtilizadosEjecutar15x" + sucursal;
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("accion", accion);
                    sqlComm.Parameters.AddWithValue("sucursal", sucursal);
                    sqlComm.Parameters.AddWithValue("materia", materia);
                    sqlComm.Parameters.AddWithValue("curso", curso);

                    sqlCon.Open();
                    lblMensaje.Text = "Consulta válida";
                    grvAuto15.DataSource = sqlComm.ExecuteReader();
                    grvAuto15.DataBind();
					sqlCon.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
                }

            }

            if (modalidad == 2)
            {
                pnAuto15.Visible = false;
                pnAuto7.Visible = true;
                pnAutoFS.Visible = false;

                pnMotoAM10.Visible = false;
                pnMotoAM10.Visible = false;
                pnMotoMF.Visible = false;
                // string sp = "ds.sp_autosUtilizadosEjecutar15x"+sucursal+"(accion, sucursal, materia, curso)";

                try
                {
                    //cAutos15 = ds.sp_autosUtilizadosEjecutar15xSuc(accion, sucursal, materia, curso);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=192.168.1.106\ANETA;Initial Catalog=Escuela;Persist Security Info=True;User ID=sistemaerp;Password=s1st3m43rp");

                    SqlCommand sqlComm = new SqlCommand();

                    sqlComm.Connection = sqlCon;

                    sqlComm.CommandTimeout = 180;

                    sqlComm.CommandText = "sp_autosUtilizadosEjecutar7x" + sucursal;
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("accion", accion);
                    sqlComm.Parameters.AddWithValue("sucursal", sucursal);
                    sqlComm.Parameters.AddWithValue("materia", materia);
                    sqlComm.Parameters.AddWithValue("curso", curso);

                    sqlCon.Open();
                    lblMensaje.Text = "Consulta válida";
                    grvAuto7.DataSource = sqlComm.ExecuteReader();
                    grvAuto7.DataBind();
					sqlCon.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
                }

            }


            if (modalidad == 3)
            {
                pnAuto15.Visible = false;
                pnAuto7.Visible = false;
                pnAutoFS.Visible = true;

                pnMotoAM10.Visible = false;
                pnMotoAM10.Visible = false;
                pnMotoMF.Visible = false;
                // string sp = "ds.sp_autosUtilizadosEjecutar15x"+sucursal+"(accion, sucursal, materia, curso)";

                try
                {
                    //cAutos15 = ds.sp_autosUtilizadosEjecutar15xSuc(accion, sucursal, materia, curso);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=192.168.1.106\ANETA;Initial Catalog=Escuela;Persist Security Info=True;User ID=sistemaerp;Password=s1st3m43rp");

                    SqlCommand sqlComm = new SqlCommand();

                    sqlComm.Connection = sqlCon;

                    sqlComm.CommandTimeout = 180;

                    sqlComm.CommandText = "sp_autosUtilizadosEjecutarFSx" + sucursal;
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("accion", accion);
                    sqlComm.Parameters.AddWithValue("sucursal", sucursal);
                    sqlComm.Parameters.AddWithValue("materia", materia);
                    sqlComm.Parameters.AddWithValue("curso", curso);

                    sqlCon.Open();
                    lblMensaje.Text = "Consulta válida";
                    grvAutoFS.DataSource = sqlComm.ExecuteReader();
                    grvAutoFS.DataBind();
					sqlCon.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
                }

            }
            /*MOTOS************/
            if (modalidad == 9)
            {
                pnAuto15.Visible = false;
                pnAuto7.Visible = false;
                pnAutoFS.Visible = false;

                pnMotoAM10.Visible = true;
                pnMotoMI5.Visible = false;
                pnMotoMF.Visible = false;
                // string sp = "ds.sp_autosUtilizadosEjecutar15x"+sucursal+"(accion, sucursal, materia, curso)";

                try
                {
                    //cAutos15 = ds.sp_autosUtilizadosEjecutar15xSuc(accion, sucursal, materia, curso);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=192.168.1.106\ANETA;Initial Catalog=Escuela;Persist Security Info=True;User ID=sistemaerp;Password=s1st3m43rp");

                    SqlCommand sqlComm = new SqlCommand();

                    sqlComm.Connection = sqlCon;

                    sqlComm.CommandTimeout = 180;

                    sqlComm.CommandText = "sp_autosUtilizadosEjecutarAMx" + sucursal;
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("accion", accion);
                    sqlComm.Parameters.AddWithValue("sucursal", sucursal);
                    sqlComm.Parameters.AddWithValue("materia", materia);
                    sqlComm.Parameters.AddWithValue("curso", curso);

                    sqlCon.Open();
                    lblMensaje.Text = "Consulta válida";
                    grvMotoAM10.DataSource = sqlComm.ExecuteReader();
                    grvMotoAM10.DataBind();
					sqlCon.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
                }

            }


            if (modalidad == 10)
            {
                pnAuto15.Visible = false;
                pnAuto7.Visible = false;
                pnAutoFS.Visible = false;

                pnMotoAM10.Visible = false;
                pnMotoMI5.Visible = true;
                pnMotoMF.Visible = false;
                // string sp = "ds.sp_autosUtilizadosEjecutar15x"+sucursal+"(accion, sucursal, materia, curso)";

                try
                {
                    //cAutos15 = ds.sp_autosUtilizadosEjecutar15xSuc(accion, sucursal, materia, curso);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=192.168.1.106\ANETA;Initial Catalog=Escuela;Persist Security Info=True;User ID=sistemaerp;Password=s1st3m43rp");

                    SqlCommand sqlComm = new SqlCommand();

                    sqlComm.Connection = sqlCon;

                    sqlComm.CommandTimeout = 180;

                    sqlComm.CommandText = "sp_autosUtilizadosEjecutarMIx" + sucursal;
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("accion", accion);
                    sqlComm.Parameters.AddWithValue("sucursal", sucursal);
                    sqlComm.Parameters.AddWithValue("materia", materia);
                    sqlComm.Parameters.AddWithValue("curso", curso);

                    sqlCon.Open();
                    lblMensaje.Text = "Consulta válida";
                    grvMotoMI5.DataSource = sqlComm.ExecuteReader();
                    grvMotoMI5.DataBind();
					sqlCon.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
                }

            }

            if (modalidad == 11)
            {
                pnAuto15.Visible = false;
                pnAuto7.Visible = false;
                pnAutoFS.Visible = false;

                pnMotoAM10.Visible = false;
                pnMotoMI5.Visible = false;
                pnMotoMF.Visible = true;
                // string sp = "ds.sp_autosUtilizadosEjecutar15x"+sucursal+"(accion, sucursal, materia, curso)";

                try
                {
                    //cAutos15 = ds.sp_autosUtilizadosEjecutar15xSuc(accion, sucursal, materia, curso);
                    SqlConnection sqlCon = new SqlConnection(@"Data Source=192.168.1.106\ANETA;Initial Catalog=Escuela;Persist Security Info=True;User ID=sistemaerp;Password=s1st3m43rp");

                    SqlCommand sqlComm = new SqlCommand();

                    sqlComm.Connection = sqlCon;

                    sqlComm.CommandTimeout = 180;

                    sqlComm.CommandText = "sp_autosUtilizadosEjecutarMFx" + sucursal;
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("accion", accion);
                    sqlComm.Parameters.AddWithValue("sucursal", sucursal);
                    sqlComm.Parameters.AddWithValue("materia", materia);
                    sqlComm.Parameters.AddWithValue("curso", curso);

                    sqlCon.Open();
                    lblMensaje.Text = "Consulta válida";
                    grvMotoMF.DataSource = sqlComm.ExecuteReader();
                    grvMotoMF.DataBind();
					sqlCon.Close();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
                }

            }
/*************************************************************************/
        }
        catch (Exception ex)
        {
            lblMensaje.Text = "Consulta inválida" + ex.Message.Trim();
        }
    }

           


        

    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listCon = new ListItem("Seleccione Modalidad", "-1");

        ddlModalidad.Items.Insert(0, listCon);

    }


    protected void listarCurso()
    {

        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("MODACTIVOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);
    }

    #region BOTONES PESTAÑAS
    protected void btnCliente_Click(object sender, EventArgs e)
    {
        ddlCurso_SelectedIndexChanged();
    }

    protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarEscuelas();
    }
    #endregion

    private void ejecutar()
{
    string sucursal = ddlEscuela.SelectedValue;

    // Declaramos la clase CSharpCodeProvider.
    CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
    // Declaramos la clase CodeDomProvider.
    CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");
    CompilerParameters compilerParameters = new CompilerParameters();
    // Generamos el codigo del ensamblado en memoria.
    compilerParameters.GenerateInMemory = true;
    // No generamos un fichero ejecutable.
    compilerParameters.GenerateExecutable = false;
    // Indicamos las referencias al ensamblado.
    compilerParameters.ReferencedAssemblies.Add("system.dll");
    // Recuperamos el valor de la expresique queremos ejecutar internamente.
    int expression;
    string sp = "ds.sp_autosUtilizadosEjecutar15x" + sucursal + "(accion, sucursal, materia, curso)";
    bool resultExpression = Int32.TryParse(sp, out expression);
    if (resultExpression)
    {
        // Preparamos las instrucciones de codigo de forma dinamica
        // de acuerdo al valor de una variable o en este caso de un 
        // control de tipo radioButton.
        string source = "";
      
            source = sp;
      
        // Despues de recuperar las instrucciones de codigo, 
        // preparamos el codigo junto al valor de la expresion.
        source = source.Replace("{0}", expression.ToString());
        // Compilamos y obtenemos los resultados de la compilacion.
        CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, source);
        // Miramos si hay errores o no.
        if (compilerResults.Errors.Count > 0)
        {
            foreach (CompilerError compilerError in compilerResults.Errors)
            {
                lblMensaje.Text ="Error compilando."
                                + Environment.NewLine
                                + Environment.NewLine
                                + String.Format("Error en linea {0} y columna {1}.", compilerError.Line, compilerError.Column)
                                + Environment.NewLine;
            }
        }
        else
        {
            // Obtenemos el valor 
            Type type = compilerResults.CompiledAssembly.GetType("ClaseCalculo");
            Object objectEvaluator = Activator.CreateInstance(type);
            MethodInfo methodInfo = type.GetMethod("MiMetodo");
            var result = methodInfo.Invoke(objectEvaluator, new object[0]);
            lblMensaje.Text =String.Format("Resultado: {0}", result);
        }
    }
     else
    {
        
         lblMensaje.Text = String.Format("El valor de la expresion {0} no es valido.");
    }
}

    private Assembly BuildAssembly(string code)
    {
        Microsoft.CSharp.CSharpCodeProvider provider =
           new CSharpCodeProvider();
        ICodeCompiler compiler = provider.CreateCompiler();
        CompilerParameters compilerparams = new CompilerParameters();
        compilerparams.GenerateExecutable = false;
        compilerparams.GenerateInMemory = true;
        CompilerResults results =
           compiler.CompileAssemblyFromSource(compilerparams, code);
        if (results.Errors.HasErrors)
        {
            StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
            foreach (CompilerError error in results.Errors)
            {
                errors.AppendFormat("Line {0},{1}\t: {2}\n",
                       error.Line, error.Column, error.ErrorText);
            }
            throw new Exception(errors.ToString());
        }
        else
        {
            return results.CompiledAssembly;
        }
    }

    public object ExecuteCode(string code,
    string namespacename, string classname,
    string functionname, bool isstatic, params object[] args)
    {
        object returnval = null;
        Assembly asm = BuildAssembly(code);
        object instance = null;
        Type type = null;
        if (isstatic)
        {
            type = asm.GetType(namespacename + "." + classname);
        }
        else
        {
            instance = asm.CreateInstance(namespacename + "." + classname);
            type = instance.GetType();
        }
        MethodInfo method = type.GetMethod(functionname);
        returnval = method.Invoke(instance, args);
        return returnval;
    }
}