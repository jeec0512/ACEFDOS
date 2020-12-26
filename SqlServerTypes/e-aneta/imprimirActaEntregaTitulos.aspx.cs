using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;

public partial class Escuela_imprimirActaEntregaTitulos : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    //Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString;

    Data_EscuelaDataContext de = new Data_EscuelaDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["AdmBitaAutoConnectionString"].ConnectionString;
    Data_AdmBitaAutoDataContext da = new Data_AdmBitaAutoDataContext();

    decimal subtotal = 0;
    decimal tarifa0 = 0;
    decimal otros = 0;
    decimal totaliva = 0;
    decimal totaldoc = 0;
    decimal fuente = 0;
    decimal iva = 0;
    decimal totalretenido = 0;
    decimal apagar = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        certificado();
    }


    protected void certificado()
    {
        DateTime Fecha = DateTime.Now;

        string sucursal = Convert.ToString(Session["pSucursal"]);
        int curso = Convert.ToInt32(Session["pCurso"]);
        string pedido = Convert.ToString(Session["ppedido"]);


        var cCurso = ds.sp_PedidoTitulos("TITULOS", sucursal, curso, pedido);




        lblTitulo.Text = "ACTA ENTREGA RECEPCIÓN DE TÍTULOS DE CONDUCTOR NO PROFESIONAL";
        lblSubtitulo.Text = "No. 23398";

        lblCuerpo.Text = "En la ciudad de Quito a los";
        string msm1 = Convert.ToString(DateTime.Now.Day);
        string msm2 = "días del mes de ";
        string msm3 = Convert.ToString(DateTime.Now.Month);
        string msm4 = "del año ";
        string msm5 = Convert.ToString(DateTime.Now.Year);
        string msm6 = ", el ";
        string msm7 = "ING. Fabio Esteban Tamayo Proaño";
        string msm8 = ", Director General de escuelas de conducción ANETA, y el Señor (a) ";
        string msm9 = "Srta. Lourdes Catalina Maldonado Novoa";
        string msm10 = "hacen entrega al señor (a)" ;
        string msm11 = "PAMELA LANDY" ;
        string msm12 = ",para la sucursal de ANETA ";
        string msm13 = sucursal ;
        string msm14 = ", de ";
        string msm15 = "10 ";
        string msm16 = "TÍTULOS DE CONDUCTOR NO PROFESIONAL del No. ";
        string msm17 = "4373";
        string msm18 = " al No. ";
        string msm19 = "4382";
        string msm20 = ", quien los recibe a su entera y absoluta conformidad.";
        
       string msm21 = "Con la firma de esta acta, el señor ";
       string msm22 = "PAMELA LANDY";
       string msm23 = ", asume la responsabilidad del custodio, buen uso y emisión de los mencionados TÍTULOS DE CONDUCTOR NO PROFESIONAL, ";
       string msm24 = "cumpliendo con todos los reuisitos y procedimientos establecidos por ANETA.";

       string msm25 = "Declaro bajo juramento y acepto libre y voluntariamente someterme a las acciones legales a que hubiere lugar por el uso indebido o la emisión incorrecta de los referidos documentos";



    }
}