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



public partial class Catalogo_menuDinamico : System.Web.UI.Page
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
        var cMenuDinamico = dc.sp_abmMenuDinamico("CONSULTAR",0,0,0,"","","",false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false);
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(cMenuDinamico);
        return JSONString;
    }


    [System.Web.Services.WebMethod]
    public static bool modificarRegistro(List<tblExcel> tblExcel)
    {
        var nota = new Catalogo_menuDinamico();
        int id_UsuarioMenuDinamico = 0;
        int llave = 0;
        int padre = 0;
        string menu = string.Empty;
        string submenu = string.Empty;
        string boton = string.Empty;
        bool sis = false;
        bool ger = false;
        bool cntGen = false;
        bool cntAux = false;
        bool adm = false;
        bool counter = false;
        bool secCnt = false;
        bool secEscGen = false;
        bool secEsc = false;
        bool secEscTeo = false;
        bool secEscPra = false;
        bool socGen = false;
        bool socAux = false;
        bool vendedor = false;
        bool especial = false;
        bool externo = false;
        bool aud = false;
        bool tallAdm = false;
        bool socios = false;
        bool visitAneta = false;
        bool visitPub = false;
        bool secacadteoria = false;
        bool secacadpractica = false;
        bool secacadtalleres = false;
        bool certificadoIso = false;
        bool operadorSico = false;
        bool jefeFlota = false;
        bool calidad = false;
        foreach (var elemento in tblExcel)
        {
            id_UsuarioMenuDinamico = elemento.id_UsuarioMenuDinamico;
            llave = elemento.llave;
            padre = elemento.padre;
            menu = elemento.menu;
            submenu = elemento.submenu;
            boton = elemento.boton;
            sis = elemento.sis;
            ger= elemento.ger;
            cntGen = elemento.cntGen;
            cntAux = elemento.cntAux;
            adm = elemento.adm;
            counter = elemento.counter;
            secCnt = elemento.secCnt;
            secEscGen = elemento.secEscGen;
            secEsc = elemento.secEsc;
            secEscTeo = elemento.secEscTeo;
            secEscPra = elemento.secEscPra;
            socGen = elemento.socGen;
            socAux = elemento.socAux;
            vendedor = elemento.vendedor;
            especial = elemento.especial;
            externo = elemento.externo;
            aud = elemento.aud;
            tallAdm = elemento.tallAdm;
            socios = elemento.socios;
            visitAneta = elemento.visitAneta;
            visitPub = elemento.visitPub;
            secacadteoria = elemento.secacadteoria;
            secacadpractica = elemento.secacadpractica;
            secacadtalleres = elemento.secacadtalleres;
            certificadoIso = elemento.certificadoIso;
            operadorSico = elemento.operadorSico;
            jefeFlota = elemento.jefeFlota;
            calidad = elemento.calidad;
            try
            {
                //nota.ds.sp_abmRegistroNota_Con("INGNOTAS", RNOTC_ID, 0, "", "", "", "", RNOTC_EDUC_VIAL_ASIS, RNOTC_EDUC_VIAL_NOTA, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, false);
                nota.dc.sp_abmMenuDinamico("MODIFICAR",id_UsuarioMenuDinamico, llave, padre, menu, submenu, boton, sis, ger, cntGen, cntAux, adm, counter, secCnt, secEscGen, secEsc, secEscTeo, secEscPra, socGen, socAux, vendedor, especial, externo, aud, tallAdm, socios, visitAneta, visitPub, secacadteoria, secacadpractica, secacadtalleres, certificadoIso, operadorSico, jefeFlota, calidad);
            }
            catch (Exception e)
            {
            }
        }


        return true;
    }


    public class tblExcel
    {
        public tblExcel() { }
        public int id_UsuarioMenuDinamico { get; set; }
        public int llave { get; set; }
        public int padre { get; set; }
        public string menu { get; set; }
        public string submenu { get; set; }
        public string boton { get; set; }
        public bool sis { get; set; }
        public bool ger { get; set; }
        public bool cntGen { get; set; }
        public bool cntAux { get; set; }
        public bool adm { get; set; }
        public bool counter { get; set; }
        public bool secCnt { get; set; }
        public bool secEscGen { get; set; }
        public bool secEsc { get; set; }
        public bool secEscTeo { get; set; }
        public bool secEscPra { get; set; }
        public bool socGen { get; set; }
        public bool socAux { get; set; }
        public bool vendedor { get; set; }
        public bool especial { get; set; }
        public bool externo { get; set; }
        public bool aud { get; set; }
        public bool tallAdm { get; set; }
        public bool socios { get; set; }
        public bool visitAneta { get; set; }
        public bool visitPub { get; set; }
        public bool secacadteoria { get; set; }
        public bool secacadpractica { get; set; }
        public bool secacadtalleres { get; set; }
        public bool certificadoIso { get; set; }
        public bool operadorSico { get; set; }
        public bool jefeFlota { get; set; }
        public bool calidad { get; set; }

    }
}