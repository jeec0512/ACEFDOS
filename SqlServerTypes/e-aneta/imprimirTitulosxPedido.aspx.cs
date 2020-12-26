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
public partial class Escuela_imprimirTitulosxPedido : System.Web.UI.Page
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

        string sucursal = Convert.ToString(Session["pSucursal"] ) ;
        int curso = Convert.ToInt32(Session["pCurso"]);
        string pedido = Convert.ToString(Session["ppedido"]);
        int ID = Convert.ToInt32(Session["pId"]);


        var cCurso = ds.sp_imprimirTitulos("", sucursal, curso, pedido, ID);


            foreach (var registro in cCurso)
            {

                lblPermiso.Text = registro.reg_numpermiso;
                lblNombres.Text = registro.alumno;
                lblFecha.Text = registro.fecha;
                lblCurso.Text = registro.cur_nomenclatura;
                lblActa.Text = registro.rnotc_acta;
                lblCalificacion.Text = Convert.ToString(registro.rnotc_prac_nota);
                lblFecha2.Text = registro.fecha;



               

            
        }




    }
}