using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Ingreso_imprimirFacturasEmitidas : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    //Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    /************VARIABLES GENERALES***********************/
    decimal ventabruta = 0;
    decimal descuento = 0;
    decimal ventaneta = 0;
    decimal baseimponible = 0;
    decimal iva0 = 0;
    decimal iva12 = 0;
    decimal cxp = 0;
    decimal Total = 0;
    /******************************************************/

    protected void Page_Load(object sender, EventArgs e)
    {

        facturasEmitidas();
    }

    protected void facturasEmitidas()
    {
        lblMensaje.Text = "";
        DateTime lFechaInicio, lFechaFin, esteDia;

        lFechaInicio = DateTime.Now;
        lFechaFin = DateTime.Now;
        esteDia = DateTime.Now;

        string usuario, lsuc, lsucursal;


        usuario = Convert.ToString(Session["UsuarioID"]);

        string apellidos = Convert.ToString(Session["Sapellidos"]);
        string nombres = Convert.ToString(Session["Snombres"]);


        lsuc = Convert.ToString(Session["pSuc"]);
        lsucursal = lsuc + " " + traeSucursal(lsuc);
        lFechaInicio = Convert.ToDateTime(Session["pFechaInicio"]);
        lFechaFin = Convert.ToDateTime(Session["pFechaFin"]);
        /*TITULOS*/
        lblSucursal.Text = lsucursal;
        lblHoy.Text = esteDia.ToString("d") + " Usuario: " + nombres + " " + apellidos + " " + esteDia.ToString("t");

        //lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " al " + lFechaFin.ToString("d");

        lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " Al: " + lFechaFin.ToString("d");

        /*********/
        try
        {
			
			dc.CommandTimeout = 360;
            string laccion = "DETALLE";

            //dc.sp_FacturasEmitidas2(laccion, lsucursal, lfechaInicio, lfechaFin);

            var cEgresos = dc.sp_FacturasEmitidas3(laccion, lsucursal, lFechaInicio, lFechaFin);

            grvFacturasEmitidas.DataSource = cEgresos;
            grvFacturasEmitidas.DataBind();
            grvFacturasEmitidas.Visible = true;
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }

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
    protected void FacturasEmitidas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            ventabruta += Convert.ToDecimal(e.Row.Cells[5].Text);
            descuento += Convert.ToDecimal(e.Row.Cells[6].Text);
            ventaneta += Convert.ToDecimal(e.Row.Cells[7].Text);
            iva0 += Convert.ToDecimal(e.Row.Cells[8].Text);
            iva12 += Convert.ToDecimal(e.Row.Cells[9].Text);
            cxp += Convert.ToDecimal(e.Row.Cells[10].Text);
            Total += Convert.ToDecimal(e.Row.Cells[11].Text);




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Totales";

            e.Row.Cells[5].Text = Convert.ToString(ventabruta);
            e.Row.Cells[6].Text = Convert.ToString(descuento);
            e.Row.Cells[7].Text = Convert.ToString(ventaneta);
            e.Row.Cells[8].Text = Convert.ToString(iva0);
            e.Row.Cells[9].Text = Convert.ToString(iva12);
            e.Row.Cells[10].Text = Convert.ToString(cxp);
            e.Row.Cells[11].Text = Convert.ToString(Total);
        }
    }
}