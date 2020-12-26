using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Tesoreria_registrarClienteCXC : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();



    #endregion  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensaje.Text = string.Empty;
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
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string lruc = txtRuc.Text.Trim();

        var cCliente = from tCliente in df.CLIENTE
                       where tCliente.CLI_RUC == lruc
                      select new
                      {
                          ruc = tCliente.CLI_RUC,
                          nombre = tCliente.CLI_APELLIDOP.Trim()+" " +tCliente.CLI_APELLIDOM.Trim()+" "+tCliente.CLI_NOMBRE.Trim(),
                      };

        foreach (var registro in cCliente)
        {
            txtRuc.Text = registro.ruc;
            txtNombreCliente.Text = registro.nombre;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string accion, ruc, razonsocial,descripcion,usuario;

        /* CONSTANTES */
        accion = "GUARDAR";
        usuario = (string)Session["SUsername"];
        /*VARIABLES*/
        string sucursal = ddlSucursal2.SelectedValue;
        string documentoUtilizado = txtDocumento.Text.Trim();
        ruc = txtRuc.Text;
        razonsocial = txtNombreCliente.Text;
        decimal totalFactura = Convert.ToDecimal(txtTotal.Text);
        descripcion = txtDescripcion.Text.Trim();


        /*VALIDAR INFORMACION*/

        if (ruc.Length < 10
            || razonsocial.Length <= 1
            || descripcion.Length <= 3
            || ruc == "1790349578001")
        {
            if (ruc == "1790349578001")
            {
                lblMensaje.Text = "ANETA NO PUEDE GENERARSE CXC";
            }
            else { lblMensaje.Text = "Ingrese toda la información,identificación válido,razón social, descripción"; }
        }
        else
        {
            /*GUARDAR INFORMACION*/
            dc.sp_abmClientesCXC(accion,0,DateTime.Now,ruc,documentoUtilizado,totalFactura,0,usuario,DateTime.Now,sucursal,descripcion,false);
            blanquearSucursal();
            lblMensaje.Text = razonsocial.Trim() + "guardado correctamente";
        }
    }

    protected void blanquearSucursal()
    {
        txtRuc.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
    }

   
    protected void txtTotal_TextChanged(object sender, EventArgs e)
    {

    }
}