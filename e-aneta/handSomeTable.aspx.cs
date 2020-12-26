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
using Newtonsoft.Json;

public partial class Escuela_handSomeTable : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

           // perfilUsuario();
            //string cadena = obtenerRegistros();
           //lblMensaje.Text = cadena;

        }
    }

    

    protected string obtenerRegistros() 
    {
        var cCalificacion = ds.sp_abmRegistroNota_Con("CALIFICAR", 0, 0, "", "", "", "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, false);
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(cCalificacion);
        return JSONString;
    }
}