using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Ingreso_imprimirAsientoIngresos : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    //Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    decimal deb_mov = 0;
    decimal cre_mov = 0;
    decimal diferencia = 0;

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

        string usuario, lsuc, lsucursal;


        usuario = Convert.ToString(Session["UsuarioID"]);

        lsuc = Convert.ToString(Session["pSuc"]);
        lsucursal = lsuc + " " + traeSucursal(lsuc);
        lFechaInicio = Convert.ToDateTime(Session["pFechaInicio"]);
        lFechaFin = Convert.ToDateTime(Session["pFechaFin"]);
        /*TITULOS*/
        lblSucursal.Text = lsucursal;
        lblHoy.Text = esteDia.ToString("d");

        //lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " al " + lFechaFin.ToString("d");

        lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " Al: " + lFechaFin.ToString("d");

        /*********/


        string laccion = "DETALLE";


        var cEgresos = dc.sp_verContabilizacionVentas(laccion, lFechaInicio, lFechaFin, lsucursal);

        grvEgresosDetalle.DataSource = cEgresos;
        grvEgresosDetalle.DataBind();



        pnDetalleCaja.Visible = true;
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
    protected void grvEgresosDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            deb_mov += Convert.ToDecimal(e.Row.Cells[2].Text);
            cre_mov += Convert.ToDecimal(e.Row.Cells[3].Text);
           


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            diferencia += (deb_mov - cre_mov);

            e.Row.Cells[0].Text = "Totales";

            e.Row.Cells[2].Text = Convert.ToString(deb_mov);
            e.Row.Cells[3].Text = Convert.ToString(cre_mov);

            e.Row.Cells[4].Text = "Dif.";

            e.Row.Cells[5].Text = Convert.ToString(diferencia);

        }
    }
}