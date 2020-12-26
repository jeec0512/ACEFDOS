using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using acefdos;

public partial class Escuela_multiplesFilas : System.Web.UI.Page
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
        if (!IsPostBack) {
            getData();
           
        }

        
    }

    private void getData() 
    {
        grvEstudiantes.DataSource = ds.sp_PedidoTitulos("CONSULTAR", "QTT", 93, ""); //estudianteDataAccessLayer.getAllEstudiantes();
        grvEstudiantes.DataBind();
    }

   
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        List<string> lstEstudiantesParaPedido = new List<string>();

        foreach(GridViewRow gridViewRow in grvEstudiantes.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cbConfirmar")).Checked) 
            {
                string regId = ((Label)gridViewRow.FindControl("lblIdNota")).Text;
                lstEstudiantesParaPedido.Add(regId);
            }
        }
        if (lstEstudiantesParaPedido.Count > 0)
        {
            lblMessage.ForeColor = System.Drawing.Color.Navy;

            foreach (string strRegId in lstEstudiantesParaPedido) 
            {
                estudianteDataAccessLayer.confirmarEstudiantes(Convert.ToInt32(strRegId));
                
            }

            estudianteDataAccessLayer.confirmarEstudiantes(lstEstudiantesParaPedido);

            lblMessage.Text = lstEstudiantesParaPedido.Count.ToString() + "fila(s) confirmadas";
            getData();
        }else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "No existen fila(s) confirmadas";
        }
    }
}