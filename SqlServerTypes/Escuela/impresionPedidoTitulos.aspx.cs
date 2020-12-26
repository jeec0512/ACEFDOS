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

public partial class Escuela_impresionPeditoTitulos : System.Web.UI.Page
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
        anexo4();
    }

    protected void anexo4() 
    {
        DateTime Fecha = DateTime.Now;
        string usuario = Convert.ToString(Session["UsuarioID"]);
        string sucursal = Convert.ToString(Session["pSucursal"]);
        int cur_id = Convert.ToInt32(Session["pCur_id"]);
        string pedido = Convert.ToString(Session["ppedido"]);

        var cCurso = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();


        var cEscuela = ds.sp_abmEscuela("CONSULTAR", 0, "", "", "", 0, sucursal);
        foreach (var regCliente in cEscuela.ToList())
        {
            txtEscuela.Text = regCliente.ESC_DESCRIPCION;

        }

        var cFechaCur = ds.sp_abmCurso("CONSULTAR", cur_id, 0, "", "", DateTime.Now, DateTime.Now, false, "", DateTime.Now);
        foreach (var regCliente in cFechaCur.ToList())
        {
            txtCurso.Text = regCliente.CUR_NOMENCLATURA;
            txtFechaInicio.Text = Convert.ToString(regCliente.CUR_FECHA_INICIO).Substring(0,10);
            txtFechaFin.Text = Convert.ToString(regCliente.CUR_FECHA_FIN).Substring(0, 10);

        }


    }
}