using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class Tesoreria_repIngEgr : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

   // Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        cierreCaja();
    }

    protected void cierreCaja()
    {
        DateTime lFechaInicio, lFechaFin, esteDia;

        lFechaInicio = DateTime.Now;
        lFechaFin = DateTime.Now;
        esteDia = DateTime.Now;

        string usuario, lsuc, laccion, lsucursal;


        usuario = Convert.ToString(Session["SUsername"]);

        lsuc = Convert.ToString(Session["pSuc"]);
        lsucursal = lsuc + " " + traeSucursal(lsuc);
        lFechaInicio = Convert.ToDateTime(Session["pFechaInicio"]);
        lFechaFin = Convert.ToDateTime(Session["pFechaFin"]);
        /*TITULOS*/
        lblSucursal.Text = lsucursal;
        lblHoy.Text = esteDia.ToString("d");

        lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " al " + lFechaFin.ToString("d");

        //lblFechas.Text = "Del: " + lFechaInicio.ToString("d");

        /*********/

        laccion = "APERIODO";
        if (usuario == "" || usuario == null)
        {
            Response.Redirect("~/ingresar.aspx");
        }
        grvCierreCaja.DataSource = dc.sp_RepIngresosEgresos(laccion, lFechaInicio, lFechaFin, lsuc);
        grvCierreCaja.DataBind();

    }
    protected string traeSucursal(string lsuc)
    {
        string lSucursal;

        lSucursal = "";

        var consultaSuc = from Suc in dc.tbl_mae_suc
                          where Suc.mae_suc == lsuc
                          select new
                          {
                              nom_suc = Suc.nom_suc
                          };

        if (consultaSuc.Count() == 0)
        {
            lSucursal = "Sucursal sin descripción";
        }
        else
        {
            foreach (var registro in consultaSuc)
            {
                lSucursal = registro.nom_suc;
            }
        }

        return lSucursal;

    }
}