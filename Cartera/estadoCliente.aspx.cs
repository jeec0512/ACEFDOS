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


public partial class Cartera_estadoCliente : System.Web.UI.Page
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

            //DateTime lfecha = DateTime.Today;
            //txtFechaIni.Text = Convert.ToString(lfecha);
            //txtFechaFin.Text = Convert.ToString(lfecha);

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

    }

    #endregion

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        string lbuscar, lopcion, laccion, lcedula;

        laccion = "";

        lopcion = ddlTipoBusqueda.SelectedValue.Trim();

        lbuscar = txtBuscar.Text.Trim();

        lcedula = lbuscar;

        if (lopcion == "0")
        {
            ///lista clientes q cumplen condicipon de cédula
            ///
            laccion = "XCEDULA";
            grvClientes.DataSource = dc.sp_buscaCliente(laccion, lbuscar);

            grvClientes.DataBind();

            /// lista historial de membrecías
            /// 
            laccion = "XCEDULA";

            grvHistorialMembrecias.DataSource = dc.sp_buscaMembrecias(laccion, lcedula);
            grvHistorialMembrecias.DataBind();

            grvProductos.Visible = true;
            grvTesoreria.Visible = true;
        }

        if (lopcion == "1")
        {
            laccion = "XNOMBRE";

            ///lista clientes q cumplen condicipon de nombre
            ///

            grvClientes.DataSource = dc.sp_buscaCliente(laccion, lbuscar);

            grvClientes.DataBind();
        }
    }

    protected void grvClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lcedula;
        string sucursal = ddlSucursal2.SelectedValue;
        int tipo = (int)Session["STipo"];
        lcedula = "";


        if (tipo == 4)
        {
            ///lista las facturas emitidas
            ///
            laccion = "TODOS";
            lcedula = Convert.ToString(grvClientes.SelectedValue);

            grvFacturasEmitidas.DataSource = dc.sp_buscaFacturas2(laccion, sucursal, lcedula);
            grvFacturasEmitidas.DataBind();

        }
        else
        {  ///lista las facturas emitidas
            ///
            laccion = "TODOS";
            lcedula = Convert.ToString(grvClientes.SelectedValue);

            grvFacturasEmitidas.DataSource = dc.sp_buscaFacturas2(laccion, sucursal, lcedula);
            grvFacturasEmitidas.DataBind();
        }

        /// lista historial de membrecías
        /// 
        laccion = "XCEDULA";

        grvHistorialMembrecias.DataSource = dc.sp_buscaMembrecias(laccion, lcedula);
        grvHistorialMembrecias.DataBind();

        grvProductos.Visible = true;
        grvTesoreria.Visible = true;

    }

    protected void grvTotalSocios_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lunico, lcedula;

        laccion = "XCODIGO";
        lunico = "";
        lcedula = "";

        ///lista productos comprados de la factura
        ///

        lunico = Convert.ToString(grvFacturasEmitidas.SelectedValue);

        grvProductos.DataSource = dc.sp_buscaProductosxfactura(laccion, lunico, lcedula);
        grvProductos.DataBind();


        ///lista factura cancelada
        ///

        grvTesoreria.DataSource = dc.sp_buscaPagosxfactura(laccion, lunico, lcedula);
        grvTesoreria.DataBind();

        grvProductos.Visible = true;
        grvTesoreria.Visible = true;
    }

    protected void grvHistorialMembrecias_SelectedIndexChanged(object sender, EventArgs e)
    {
        string laccion, lunico, lcedula;
        laccion = "XCEDULA";
        lunico = "";
        lcedula = "";

        ///lista auxilios mecánicos
        ///

        lunico = Convert.ToString(grvHistorialMembrecias.SelectedValue);
        lcedula = txtBuscar.Text.Trim();

        grvAuxilio.DataSource = dc.sp_buscaAuxiliosPrestados(laccion, lcedula, lunico);
        grvAuxilio.DataBind();
    }
}