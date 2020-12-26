using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Valija_reciboValija : System.Web.UI.Page
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
        /*TITULOS*/
        lblSucursal.Text = lsucursal;
        lblHoy.Text = esteDia.ToString("d");

        lblFechas.Text = "Del: " + lFechaInicio.ToString("d");

        //lblFechas.Text = "Del: " + lFechaInicio.ToString("d");

        /*********/

        //laccion = "APERIODO";
        if (usuario == "" || usuario == null)
        {
            Response.Redirect("~/ingresar.aspx");
        }

        laccion = "DETALLE";

        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(lFechaInicio.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(lFechaInicio.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(lFechaInicio.Year), '0', 4);
        string lnumero = 'R' + lsuc.Trim() + dia + mes + ano;

//        string lnumero = lsuc.Trim() + Convert.ToString(lFechaInicio).Substring(0, 10).Trim();
        
        lblDocumento.Text = lnumero;

        /*var concultaDetEgresos = dc.sp_ConsultaValijaDetalleRecibe(laccion, lnumero);

        grvDetallePagos.DataSource = concultaDetEgresos;
        grvDetallePagos.DataBind();*/

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

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
}