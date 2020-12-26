using AjaxControlToolkit;
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

public partial class Herramienta_factura : System.Web.UI.Page
{

    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

        }
    }
    #endregion

    #region PROCESOS INTERNOS

    protected void perfilUsuario()
    {
        try
        {
            string grupo = (string)Session["SGrupo"];
            string sucursal = (string)Session["SSucursal"];
            if (grupo == ""
               || grupo == null
               || sucursal == ""
               || sucursal == null)
            {
                Response.Redirect("~/ingresar.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];

            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/ingresar.aspx");
            }



            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }

    protected void activarObjetos()
    {
        pnTitulos.Visible = true;
        lblMensaje.Text = string.Empty;
    }

    
    #endregion

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void desactivarPaneles()
    {
        pnTitulos.Enabled = false;
        pnListadoFactura.Visible = false;
        pnFactura.Visible = true;
    }

    protected void btnListarRuc_Click(object sender, EventArgs e)
    {
        btnListarRuc_Click();
    }

    protected void btnListarRuc_Click()
    {
        string laccion,lsecuencial, lsuc;
        int ltipo;
        decimal lfac_id;
        DateTime lfechaInicio, lfechaFin;

        var consultaSuc = new object();
        /****************************/
        lsuc = ddlSucursal2.SelectedValue.Trim();

        //llenar de ceros el secuencial
        
        txtBuscaSecuencial.Text = llenarCeros(txtBuscaSecuencial.Text.Trim(), '0', 9);
        lsecuencial = txtBuscaSecuencial.Text.Trim();
        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;

        laccion = "XSUCXFAC";
        ltipo = 0;
        lfac_id = 0;

        int tipo = (int)Session["STipo"];

        if (tipo==4)
        {
            btnAnularFac.Visible = true;
        }
        else
        {
            btnAnularFac.Visible = false;
        }

        var consultaCabFac = df.sp_FacturaCabecera(laccion, ltipo, lsuc, lsecuencial, lfechaInicio, lfechaFin, lfac_id);

        grvListadoFac.DataSource = consultaCabFac;
        grvListadoFac.DataBind();
    }

    protected void activarBotones()
    {
        decimal lrecaudado = Convert.ToDecimal(hRecaudado.Value);

        int tipo = (int)Session["STipo"];

        string lestado = hEstado.Value.ToString();
        

        btnBorrarFac.Visible = false;
        btnAnularFac.Visible = false;

        /**QUE PASA CUANDO ESTA ANULADO*/

        if (lestado == "A" || lestado == "X")
        {
            btnBorrarFac.Visible = false;
            if (tipo == 4)
            {
                if (lestado == "A")
                {
                    btnAnularFac.Visible = true;
                }
                else
                {
                    btnAnularFac.Visible = false;
                }
            }
        }
        if (lrecaudado > 0)
        {
            btnBorrarFac.Visible = false;
            btnAnularFac.Visible = false;
        }
    }

    protected void btnCancelarFac_Click()
    {
        activarPaneles();
    }

    protected void activarPaneles()
    {
        pnTitulos.Enabled = true;
        pnListadoFactura.Visible = true;
        pnFactura.Visible = false;
    }

    protected void grvListadoFac_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lsecuencial, lsuc;
        int ltipo;
        DateTime lfechaInicio, lfechaFin;
        decimal lfac_id;

        lsuc = ddlSucursal2.SelectedValue.Trim();
        ltipo = 0;
        //llenar de ceros el secuencial
        txtBuscaSecuencial.Text = llenarCeros(txtBuscaSecuencial.Text.Trim(), '0', 9);
        lsecuencial = txtBuscaSecuencial.Text.Trim();
        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;

        laccion = "XID";
        lfac_id = Convert.ToDecimal(grvListadoFac.SelectedValue);

        var consultaCabFac = df.sp_FacturaCabecera(laccion, ltipo, lsuc, lsecuencial, lfechaInicio, lfechaFin, lfac_id);
        var consultaDetFac = df.sp_FacturaDetalle(laccion, ltipo, lsuc, lsecuencial, lfechaInicio, lfechaFin, lfac_id);


        foreach (var registro in consultaCabFac)
        {
            hEstado.Value = registro.FAC_ESTADO;
            hRecaudado.Value = Convert.ToString(registro.FAC_RECAUDADO);
            txtNumFactura.Text = registro.FAC_ESTABLECIMIENTO + registro.FAC_PUNTOEMISION + registro.FAC_SECUENCIAL;
            txtFecha.Text = Convert.ToString(registro.FAC_FECHAEMISION);
            txtRucComprador.Text = registro.FAC_RUCCOMPRADOR;
            txtNombres.Text = registro.FAC_RAZONCOMPRADOR;
        };


        grFacturaDetalle.DataSource = consultaDetFac;
        grFacturaDetalle.DataBind();

        desactivarPaneles();
        activarBotones();
    }

    protected void btnBorrarFac_Click(object sender, EventArgs e)
    {
        string laccion = "BORXID";
        decimal lfac_id;

        lfac_id = Convert.ToDecimal(grvListadoFac.SelectedValue);

        try
        {
            df.sp_abmRecaudacion(laccion, "", "", 0, 0, 0, lfac_id);
            lblMensaje.Text = "Documento eliminado";
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        finally
        {
            btnCancelarFac_Click();
        }
    }

    protected void btnAnularFac_Click(object sender, EventArgs e)
    {
        /****************************/
        int tipo = (int)Session["STipo"];
        
        if (tipo == 4)
        {
            string laccion = "ANULAR";
            decimal lId_Factura = Convert.ToDecimal(grvListadoFac.SelectedValue);

            try
            {
                df.sp_abmRecaudacion(laccion, "", "", 0, 0, 0, lId_Factura);
                lblMensaje.Text = "Documento anulado";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            finally
            {
                btnCancelarFac_Click();
            }
        }
    }

    protected void btnCancelarFac_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        btnListarRuc_Click();
        activarPaneles();

    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    { }
}