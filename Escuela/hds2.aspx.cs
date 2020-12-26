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

public partial class Escuela_hds2 : System.Web.UI.Page
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

    }

    protected string obtenerRegistros()
    {
        var cCalificacion = ds.sp_abmRegistroNota_Con("CALIFICAR", 0, 0, "", "", "", "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, false);
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(cCalificacion);
        return JSONString;
    }


    [System.Web.Services.WebMethod]
    public static bool modificarRegistro(List<tblExcel> tblExcel)
    {
        var nota = new Escuela_hds2();
        int RNOTC_ID = 0;
        string RNOTC_CIRUC = string.Empty;
        string RNOTC_APELLIDOS = string.Empty;
        string RNOTC_NOMBRES = string.Empty;
        string RNOTC_LICENCIA = string.Empty;
        int RNOTC_EDUC_VIAL_ASIS = 0;
        decimal RNOTC_EDUC_VIAL_NOTA = 0;
        bool RNOTC_APROBADO = false;
        foreach(var elemento in tblExcel){
            RNOTC_ID = elemento.RNOTC_ID;
            RNOTC_CIRUC = elemento.RNOTC_CIRUC;
            RNOTC_APELLIDOS = elemento.RNOTC_APELLIDOS;
            RNOTC_NOMBRES = elemento.RNOTC_NOMBRES;
            RNOTC_LICENCIA = elemento.RNOTC_LICENCIA;
            RNOTC_EDUC_VIAL_ASIS = elemento.RNOTC_EDUC_VIAL_ASIS;
            RNOTC_EDUC_VIAL_NOTA = Convert.ToDecimal(elemento.RNOTC_EDUC_VIAL_NOTA);
            RNOTC_APROBADO = elemento.RNOTC_APROBADO;
            try
            {
                nota.ds.sp_abmRegistroNota_Con("INGNOTAS", RNOTC_ID, 0, "", "", "", "", RNOTC_EDUC_VIAL_ASIS, RNOTC_EDUC_VIAL_NOTA, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, false);
            }
            catch (Exception e)
            { 
            }
        }

        
        return true;
    }

    
    public class tblExcel {
        public tblExcel() { }
        public int RNOTC_ID { get; set; }
        public string RNOTC_CIRUC { get; set; }
        public string RNOTC_APELLIDOS { get; set; }
        public string RNOTC_NOMBRES { get; set; }
        public string RNOTC_LICENCIA { get; set; }
        public int RNOTC_EDUC_VIAL_ASIS { get; set; }
        public double RNOTC_EDUC_VIAL_NOTA { get; set; }
        public bool RNOTC_APROBADO { get; set; }
    }
}