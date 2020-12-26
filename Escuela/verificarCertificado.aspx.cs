using AjaxControlToolkit;
using enviarEmail;
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


public partial class Escuela_verificarCertificado : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["temporalraceConnectionString"].ConnectionString;


    Data_TemporalRaceDataContext dt = new Data_TemporalRaceDataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["COMISIONESConnectionString"].ConnectionString;

    DataComisionesDataContext ds = new DataComisionesDataContext();

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString;

    Data_EscuelaDataContext de = new Data_EscuelaDataContext();



    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

        }
    }

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

            /* DateTime lfecha = DateTime.Today;
              txtFechaIni.Text = Convert.ToString(lfecha);
              txtFechaFin.Text = Convert.ToString(lfecha);
              */
            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);


            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();

            

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

        int codigo = Convert.ToInt32(txtCodigo.Text);

        string sucursal = string.Empty;
        string factura = string.Empty;
        string cedula = string.Empty;

        var cCertif = from TCer in de.tbl_SicoPractico
                      where TCer.id_SicoPractico == codigo
                      select new { sucursal = TCer.sucursal,
                                   factura = TCer.factura,
                                   cedula = TCer.cedula
                                    
                      };

        if (cCertif.Count() == 0)
        {
            
        }
        else
        {
            foreach (var reg in cCertif)
            {
                sucursal = reg.sucursal;
                factura = reg.factura;
                cedula = reg.cedula;

            }
        }


        Session["pSucursal"] = sucursal;
        Session["pFactura"] = factura;
        Session["pCedula"] = cedula;

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirSicopractico.aspx','','width=800,height=500') </script>");
        //pnCertificado.Visible = true;
        

        
    }
}