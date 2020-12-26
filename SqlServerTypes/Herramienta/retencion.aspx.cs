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


public partial class Herramienta_retencion : System.Web.UI.Page
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


    protected int traer_Id_infoCompRetencion(int lid_infotributaria)
    {
        int lId_infoCompRetencion = 0;
        var consulta = from Tcompret in dc.tbl_infoCompRetencion
                       where Tcompret.id_infotributaria == lid_infotributaria
                       select new { idcompret = Tcompret.id_infoCompRetencion };
        if (consulta.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consulta)
            {
                lId_infoCompRetencion = registro.idcompret;
            }
        }
        return lId_infoCompRetencion;
    }

    protected void btnCancelarFac_Click()
    {
        activarPaneles();
    }

    protected void activarPaneles()
    {
        pnTitulos.Enabled = true;
        pnListadoRetencion.Visible = true;
        pnRetencion.Visible = false;
    }

    protected void desactivarPaneles()
    {
        pnTitulos.Enabled = false;
        pnListadoRetencion.Visible = false;
        pnRetencion.Visible = true;
    }

    protected void activarBotones()
    {
        string lestado = hEstado.Value.ToString();;

        /***************/
        int tipo = (int)Session["STipo"];

        /**************/

        // lrecaudado = Convert.ToDecimal(hRecaudado.Value);

        btnBorrarRet.Visible = true;
        btnAnularRet.Visible = false;
        /**QUE PASA CUANDO ESTA ANULADO*/
        if (lestado == "AUTORIZADO" || lestado == "ANULADO")
        {
            btnBorrarRet.Visible = false;
            if (tipo == 4)
            {
                if (lestado == "AUTORIZADO")
                {
                    btnAnularRet.Visible = true;
                }
                else
                {
                    btnAnularRet.Visible = false;
                }
            }
        }

    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected int traerId_infocompretencion()
    {
        int lid_infoCompRetencion = 0;

        int lid_infotributaria = Convert.ToInt32(grvListadoRetencion.SelectedValue);

        //traer serie de la sucursal

        var consultaId = from Ic in dc.tbl_infoCompRetencion
                         where Ic.id_infotributaria == lid_infotributaria
                         select new
                         {
                             id_infoCompRetencion = Ic.id_infoCompRetencion
                         };
        if (consultaId.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaId)
            {
                lid_infoCompRetencion = registro.id_infoCompRetencion;
            }
        }
        ;
        return lid_infoCompRetencion;
    }

    protected void btnListarRuc_Click(object sender, EventArgs e)
    {
        btnListarRuc_Click();
    }

    protected void btnListarRuc_Click()
    {
        string laccion, lsecuencial, lsuc;
        int ltipo;
        int lid_infotributaria;
        DateTime lfechaInicio, lfechaFin;

        var consultaSuc = new object();
        /****************************/
        int tipo = (int)Session["STipo"];

        lsuc = ddlSucursal2.SelectedValue.Trim();
        //llenar de ceros el secuencial
        txtBuscaSecuencial.Text = llenarCeros(txtBuscaSecuencial.Text.Trim(), '0', 9);
        lsecuencial = txtBuscaSecuencial.Text.Trim();
        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;

        laccion = "XSUCXFAC";
        ltipo = 0;
        lid_infotributaria = 0;


        if (tipo == 4)
        {
            btnAnularRet.Visible = true;
        }
        else
        {
            btnAnularRet.Visible = false;
        }

        var consultaCabRet = dc.sp_retencionCabecera(laccion, ltipo, lsuc, lsecuencial, lfechaInicio, lfechaFin, lid_infotributaria);

        grvListadoRetencion.DataSource = consultaCabRet;
        grvListadoRetencion.DataBind();
    }

    protected void grvListadoRetencion_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lsecuencial, lsuc;
        int ltipo;
        DateTime lfechaInicio, lfechaFin;
        int lid_infotributaria = Convert.ToInt32(grvListadoRetencion.SelectedValue);

        lsuc = ddlSucursal2.SelectedValue.Trim();
        ltipo = 0;
        //llenar de ceros el secuencial
        txtBuscaSecuencial.Text = llenarCeros(txtBuscaSecuencial.Text.Trim(), '0', 9);
        lsecuencial = txtBuscaSecuencial.Text.Trim();
        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;

        laccion = "XID";


        var consultaCabFac = dc.sp_retencionCabecera(laccion, ltipo, lsuc, lsecuencial, lfechaInicio, lfechaFin, lid_infotributaria);
        var consultaDetFac = dc.sp_retencionDetalle(laccion, ltipo, lsuc, lsecuencial, lfechaInicio, lfechaFin, lid_infotributaria);


        foreach (var registro in consultaCabFac)
        {
            hEstado.Value = registro.cre_sri;
            txtDocumento.Text = registro.documento;
            txtFecha.Text = Convert.ToString(registro.fechaDocumento);
            txtRucComprador.Text = registro.identificacionSujetoRetenido;
            txtNombres.Text = registro.razonSocialSujetoRetenido;
            txtObservacion.Text = registro.campoAdicional;
        };


        grRetencionDetalle.DataSource = consultaDetFac;
        grRetencionDetalle.DataBind();

        desactivarPaneles();
        activarBotones();
    }

    protected void btnBorrarRet_Click(object sender, EventArgs e)
    {
        string laccion = "BORXID";

        char lambiente = '0';
        char ltipoemision = '0';

        int lid_infotributaria = Convert.ToInt32(grvListadoRetencion.SelectedValue);
        int lid_infocompretencion = traerId_infocompretencion();


        try
        {
            dc.sp_abmInfoTributaria(laccion, lid_infotributaria, lambiente, ltipoemision, "", "", "", "", "", "", "", lid_infocompretencion);
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

    protected void btnAnularRet_Click(object sender, EventArgs e)
    {
        
        string laccion = "ANULAR";
        int lId_InfoTributaria = Convert.ToInt32(grvListadoRetencion.SelectedValue); 
        int lId_infoCompRetencion = traer_Id_infoCompRetencion(lId_InfoTributaria);

        try
        {
            dc.sp_abmCascada(laccion, lId_InfoTributaria, lId_infoCompRetencion);
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

    protected void btnCancelarRet_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        btnListarRuc_Click();
        activarPaneles();
    }
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        Session["pRetencion"] = txtDocumento.Text.Trim();
        Session["pSuc"] = ddlSucursal2.SelectedValue;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirRetencion.aspx','','width=800,height=750') </script>");
    }
}