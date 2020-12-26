using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Ingreso_imprimirFacturasxItems : System.Web.UI.Page
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

    decimal Icantidad = 0;
    decimal Iventabruta = 0;
    decimal Idescuento = 0;
    decimal Iventaneta = 0;
    decimal Ibaseimponible = 0;
    decimal Iiva0 = 0;
    decimal Iiva12 = 0;
    decimal Icxp = 0;
    decimal ITotal = 0;

    /******************************************************/
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

            var cEgresos = dc.sp_DetalleFacturacion(laccion, lsucursal, lFechaInicio, lFechaFin);

            grvResulItem.DataSource = cEgresos;
            grvResulItem.DataBind();
            grvResulItem.Visible = true;
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
    #region FACTURAS X ITEMS
    protected void grvResulItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            Icantidad += Convert.ToDecimal(e.Row.Cells[2].Text);

            //Iventabruta += Convert.ToDecimal(e.Row.Cells[3].Text);
            Idescuento += Convert.ToDecimal(e.Row.Cells[4].Text);
            Iventaneta += Convert.ToDecimal(e.Row.Cells[5].Text);
            Iiva0 += Convert.ToDecimal(e.Row.Cells[6].Text);
            Iiva12 += Convert.ToDecimal(e.Row.Cells[7].Text);
            Icxp += Convert.ToDecimal(e.Row.Cells[8].Text);
            ITotal += Convert.ToDecimal(e.Row.Cells[9].Text);




        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Totales";
            e.Row.Cells[2].Text = Convert.ToString(Icantidad);
            //e.Row.Cells[3].Text = Convert.ToString(Iventabruta);
            e.Row.Cells[4].Text = Convert.ToString(Idescuento);
            e.Row.Cells[5].Text = Convert.ToString(Iventaneta);
            e.Row.Cells[6].Text = Convert.ToString(Iiva0);
            e.Row.Cells[7].Text = Convert.ToString(Iiva12);
            e.Row.Cells[8].Text = Convert.ToString(Icxp);
            e.Row.Cells[9].Text = Convert.ToString(ITotal);
        }
    }
    #endregion
}