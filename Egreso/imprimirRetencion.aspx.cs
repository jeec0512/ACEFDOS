using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Egreso_imprimirRetencion : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    //Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    decimal subtotal = 0;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        cierreCaja();
    }

    protected void cierreCaja()
    {
        DateTime esteDia;


        esteDia = DateTime.Now;

        string usuario, lsuc,  lsucursal;


        usuario = Convert.ToString(Session["UsuarioID"]);

        lsuc = Convert.ToString(Session["pSuc"]);
        lsucursal = lsuc + " " + traeSucursal(lsuc);
        string lRetencion = Convert.ToString(Session["pRetencion"]);

        /*TITULOS*/
        lblSucursal.Text = lsucursal;
        lblHoy.Text = esteDia.ToString("d");

        //lblFechas.Text = "Del: " + lFechaInicio.ToString("d") + " al " + lFechaFin.ToString("d");

        /*********/
        //var cCabRet = dc.sp_cabeceraRtencion(lRetencion);

        var cCabRet = from mCabRet in df.COMPROBANTERETENCION
                      where mCabRet.CRE_ESTABLECIMIENTO+mCabRet.CRE_PUNTOEMISION+mCabRet.CRE_SECUENCIAL == lRetencion
                            && mCabRet.CRE_ESTADO=="A"
                      select new
                      {
                         cre_razonsocial = mCabRet.CRE_RAZONSOCIAL
				        ,cre_dirmatriz = mCabRet.CRE_DIRMATRIZ
				        ,cre_direstablecimiento = mCabRet.CRE_DIRESTABLECIMIENTO
				        ,cre_contribuyente = mCabRet.CRE_CONTRIBUYENTE
				        ,cre_obligado = mCabRet.CRE_OBLIGADO
				        ,cre_ruc = mCabRet.CRE_RUC
                        ,retencion = mCabRet.CRE_ESTABLECIMIENTO+ mCabRet.CRE_PUNTOEMISION+ mCabRet.CRE_SECUENCIAL
				        ,cre_claveacceso = mCabRet.CRE_CLAVEACCESO
				        ,cre_razonsociretenci = mCabRet.CRE_RAZONSOCIRETENCI
				        ,cre_identificasujerete = mCabRet.CRE_IDENTIFICASUJERETE
				        ,cre_fechaemision = mCabRet.CRE_FECHAEMISION
				        ,CRE_DIRECCION = mCabRet.CRE_DIRECCION
				        ,CRE_TELEFONO = mCabRet.CRE_TELEFONO
				        ,CRE_MAIL = mCabRet.CRE_MAIL
                      };


        if (cCabRet.Count() == 0)
        {
            
        }
        else
        {
            foreach (var registro in cCabRet)
            {
                lblTitulo.Text=registro.cre_razonsocial;
                lblDirMatriz.Text = lblDirMatriz.Text.Trim()+registro.cre_dirmatriz;
                lblDirSucursal.Text = lblDirSucursal.Text.Trim() + registro.cre_direstablecimiento;
                lblContibuyente.Text = lblContibuyente.Text.Trim() + registro.cre_contribuyente;
                lblObligado.Text = lblObligado.Text.Trim() + registro.cre_obligado;

                lblRuc.Text = lblRuc.Text.Trim() + registro.cre_ruc;
                lblretencion.Text = lblretencion.Text.Trim() + registro.retencion;
                lblAmbiente.Text = lblAmbiente.Text.Trim() + "Producción";
                lblEmision.Text=lblEmision.Text.Trim()+"NORMAL";

                lblNumClave.Text = registro.cre_claveacceso;

                lblRso.Text = lblRso.Text.Trim() + registro.cre_razonsociretenci;
                lblRucCliente.Text = lblRucCliente.Text.Trim() + registro.cre_identificasujerete;
                lblFechaEmision.Text = lblFechaEmision.Text.Trim() + registro.cre_fechaemision;

                lblDireccion.Text = lblDireccion.Text.Trim() + registro.CRE_DIRECCION;
                lblTelefono.Text = lblTelefono.Text.Trim() + registro.CRE_TELEFONO;
                lblEmail.Text = lblEmail.Text.Trim() + registro.CRE_MAIL;

            }
        }

        
        
        var cDetRet = dc.sp_detalleRtencion(lRetencion);


        grvDetalleretencion.DataSource = cDetRet;
        grvDetalleretencion.DataBind();

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


    
    protected void grvDetalleretencion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            subtotal += Convert.ToDecimal(e.Row.Cells[8].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total retenido";

            e.Row.Cells[8].Text = Convert.ToString(subtotal);

        }
    }
}