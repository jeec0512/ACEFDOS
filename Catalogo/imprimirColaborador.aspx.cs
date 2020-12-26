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
using acefdos;

public partial class Catalogo_imprimirColaborador : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        string sucursal = Convert.ToString(Session["pSucursal"]);

        /* TRAER CUENTAS CONTABLES*/
        var cSucursal = from mSuc in dc.tbl_ruc
                        where mSuc.sucursal == sucursal
                       select new
                       {
                           nombreSucursal = mSuc.nom_suc.Trim()
                       };

        foreach (var registro in cSucursal)
        {
            txtSucursal.Text = registro.nombreSucursal;
        }
       
         
        listarColaboradores();
    }

    protected void listarColaboradores()
    {
        //var cColaborador = dc.sp_abmColaborador("CONSULTAR", "", "", "", "", "", false, "", 0, "", "", DateTime.Now, DateTime.Now, 0, 0, 0, "");
        //string sucursal = ddlSucursal.SelectedValue;
        string sucursal = Convert.ToString(Session["pSucursal"]);
        grvColaboradores.DataSource = colaboradorCapaDatos.getAllColaboradores(sucursal);
        grvColaboradores.DataBind();
    }
}