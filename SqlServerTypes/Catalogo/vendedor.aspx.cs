using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_vendedor : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    #region INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string lcedula = txtCedula.Text.Trim();

        if (lcedula.Length > 0)
        {
            var cVendedor = dc.sp_abmVendedor("CONSULTAR", 0, "", "", false, 0, lcedula, 0, "");


            llenarListados();
            blanquearObjetos();

            foreach (var registro in cVendedor)
            {
                lblmensaje.Text = string.Empty;
                txtCedula.Text = registro.VEN_IDENTIFICACION;
                txtVEN_APELLIDO.Text = registro.VEN_APELLIDO;
                txtVEN_NOMBRE.Text = registro.VEN_NOMBRE;
                ddlSucursal.SelectedValue = registro.SUC_DESCRIPCION;
                ddlTPC_ID.SelectedValue = Convert.ToString(registro.TPC_ID);
                ddlSUC_ID.SelectedValue = Convert.ToString(registro.SUC_ID);
                chkActivo.Checked = Convert.ToBoolean(registro.VEN_ACTIVO);
            }

            txtVEN_APELLIDO.Focus();
        }
        else
        {
            txtCedula.Focus();
        }

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string Accion, VEN_NOMBRE, VEN_APELLIDO, VEN_IDENTIFICACION, SUC_DESCRIPCION;
        int VEN_ID, TPC_ID, SUC_ID;
        bool VEN_ACTIVO;

        Accion = "GUARDAR";
        VEN_ID = 0;


        VEN_IDENTIFICACION = txtCedula.Text.Trim();
        VEN_APELLIDO = txtVEN_APELLIDO.Text.Trim();
        VEN_NOMBRE = txtVEN_NOMBRE.Text.Trim();
        SUC_DESCRIPCION = ddlSucursal.SelectedValue;
        TPC_ID = Convert.ToInt16(ddlTPC_ID.SelectedValue);
        SUC_ID = Convert.ToInt16(ddlSUC_ID.SelectedValue);
        VEN_ACTIVO = chkActivo.Checked;

        if (VEN_IDENTIFICACION.Length < 10
           || VEN_APELLIDO.Length <= 2
           || VEN_NOMBRE.Length <= 2)
        {
            lblmensaje.Text = "Ingrese toda la información,identificación válido,nombres, apellidos,etc ";
        }
        else
        {
            if (SUC_DESCRIPCION == "-1" || TPC_ID == -1 || SUC_ID == -1)
            {
                lblmensaje.Text = "Ingrese sucursal , tipo de comisión e Id de sucursal";
            }
            else
            {
                /*GUARDAR INFORMACION*/
                dc.sp_abmVendedor(Accion, VEN_ID, VEN_NOMBRE, VEN_APELLIDO, VEN_ACTIVO, TPC_ID, VEN_IDENTIFICACION, SUC_ID, SUC_DESCRIPCION);
                blanquearObjetos();
                lblmensaje.Text = VEN_APELLIDO.Trim() + " " + VEN_NOMBRE.Trim() + " guardado correctamente";
            }
        }
    }

    protected void llenarListados()
    {
        /* TRAER CUENTAS CONTABLES*/
        var cSuc = from msuc in dc.tbl_ruc
                   where msuc.activo == true
                   orderby msuc.sucursal
                   select new
                   {
                       sucursal = msuc.sucursal.Trim()
                    ,
                       nom_suc = msuc.sucursal.Trim() + " " + msuc.nom_suc.Trim()
                   };


        ddlSucursal.DataSource = cSuc;
        ddlSucursal.DataBind();

        ListItem listSuc = new ListItem("Seleccione sucursal", "-1");
        ddlSucursal.Items.Insert(0, listSuc);

        /* TRAER TIPO DE COMISION*/
        var cTipoComision = dc.sp_listarTipoComision();

        ddlTPC_ID.DataSource = cTipoComision;
        ddlTPC_ID.DataBind();
        ListItem listComision = new ListItem("Seleccione tipo de comisión", "-1");
        ddlTPC_ID.Items.Insert(0, listComision);

        /* TRAER ID DE SUCURSAL PUNTO DE VENTA*/

        var cIdPtoVta = dc.sp_listarPuntoVenta();

        ddlSUC_ID.DataSource = cIdPtoVta;
        ddlSUC_ID.DataBind();
        ListItem listID = new ListItem("Seleccione Punto de Venta", "-1");
        ddlSUC_ID.Items.Insert(0, listID);

    }

    protected void blanquearObjetos()
    {

        //txtCedula.Text = string.Empty;
        txtVEN_APELLIDO.Text = string.Empty;
        txtVEN_NOMBRE.Text = string.Empty;
        ddlSucursal.SelectedValue = "-1";
        ddlTPC_ID.SelectedValue = "-1";
        ddlSUC_ID.SelectedValue = "-1";
        chkActivo.Checked = false;

    }
}