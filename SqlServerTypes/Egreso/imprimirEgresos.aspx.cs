using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Egreso_imprimirEgresos : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    //Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

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
        cierreCaja();
    }

    protected void cierreCaja()
    {
        DateTime lFechaInicio, lFechaFin, esteDia;

        lFechaInicio = DateTime.Now;
        lFechaFin = DateTime.Now;
        esteDia = DateTime.Now;

        string usuario, lsuc, lsucursal,lCaja;


        usuario = Convert.ToString(Session["UsuarioID"]);

        lsuc = Convert.ToString(Session["pSuc"]);
        lsucursal = lsuc + " " + traeSucursal(lsuc);
        lFechaInicio = Convert.ToDateTime(Session["pFechaInicio"]);
        lFechaFin = Convert.ToDateTime(Session["pFechaFin"]);
        lCaja = Convert.ToString(Session["pCaja"]);
        /*TITULOS*/
        lblSucursal.Text = lsucursal;
        lblHoy.Text = esteDia.ToString("d");

        //lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " al " + lFechaFin.ToString("d");

        lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " Al: " + lFechaFin.ToString("d");

        /*********/

       
        string lAccion = "XFECHA";


        var cEgresos = dc.sp_ListarGastosyRetenciones(lAccion, lsucursal, lFechaInicio, lFechaFin, lCaja, 0);

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
            subtotal += Convert.ToDecimal(e.Row.Cells[5].Text);
            tarifa0 += Convert.ToDecimal(e.Row.Cells[6].Text);
            otros += Convert.ToDecimal(e.Row.Cells[7].Text);
            totaliva += Convert.ToDecimal(e.Row.Cells[8].Text);
            totaldoc += Convert.ToDecimal(e.Row.Cells[9].Text);



            fuente += Convert.ToDecimal(e.Row.Cells[11].Text); 
            
            iva += Convert.ToDecimal(e.Row.Cells[12].Text);
            totalretenido += Convert.ToDecimal(e.Row.Cells[13].Text);
            apagar += Convert.ToDecimal(e.Row.Cells[14].Text);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Totales";

            e.Row.Cells[5].Text = Convert.ToString(subtotal);
            e.Row.Cells[6].Text = Convert.ToString(tarifa0);
            e.Row.Cells[7].Text = Convert.ToString(otros);
            e.Row.Cells[8].Text = Convert.ToString(totaliva);
            e.Row.Cells[9].Text = Convert.ToString(totaldoc);
            e.Row.Cells[11].Text = Convert.ToString(fuente);
            e.Row.Cells[12].Text = Convert.ToString(iva);
            e.Row.Cells[13].Text = Convert.ToString(totalretenido);
            e.Row.Cells[14].Text = Convert.ToString(apagar);
        }
    }
}