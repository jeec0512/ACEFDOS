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
using System.Globalization; 

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

        /*
        lblNumActa.Text = "No " + "23484";
        lblDia.Text = Convert.ToString(DateTime.Now.Day);
        lblMes.Text = obtenerNombreMesNumero(DateTime.Now.Month);
        lblAno.Text = Convert.ToString(DateTime.Now.Year);
        lblNombresAdministrador.Text = "Juan Pérez";
        lblSucursal.Text = "QUERO";
        lblNumeroTitulos.Text = "51";
        lblDel.Text = "500";
        lblAl.Text = "550";
        lblAdministrador.Text = "Juan Pérez";
        lblFirmaAdministrador.Text = "Juan Pérez";*/

        Fecha = Convert.ToDateTime(Session["pFecha"]);
        //Session["pSuc"] ;
        lblNumActa.Text = "No " + Convert.ToString(Session["pNumActa"]);
        lblDia.Text = Convert.ToString(Session["pDia"]) ;
        lblMes.Text = Convert.ToString(Session["pMes"]);
        lblAno.Text = Convert.ToString(Session["pAno"]) ;
        lblNombresAdministrador.Text =  Convert.ToString(Session["pNombresAdministrador"]);
        lblSucursal.Text =   Convert.ToString(Session["pSucursal"]);

        lblNumeroTitulos.Text = Convert.ToString(Session["pNumeroTitulos"]);
        lblDel.Text = Convert.ToString(Session["pDel"]);
        lblAl.Text = Convert.ToString(Session["pAl"]);
        lblAdministrador.Text = Convert.ToString(Session["pAdministrador"]);
        lblFirmaAdministrador.Text = Convert.ToString(Session["pFirmaAdministrador"]);








        
       
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.print('imprimirActaEntregaTitulos.aspx','','width=100vh,height=100vh') </script>");


    }

    private string obtenerNombreMesNumero(int numeroMes)
    {
        try
        {
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            string nombreMes = formatoFecha.GetMonthName(numeroMes);
            return nombreMes;
        }
        catch
        {
            return "Desconocido";
        }
    } 
}