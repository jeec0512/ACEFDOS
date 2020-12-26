using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_matriz : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblmensaje.Text = string.Empty;

        }
    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        string lruc = txtRuc.Text.Trim();

        var cMatriz = from tMatriz in dc.tbl_matriz
                      where tMatriz.ruc == lruc
                      select new
                      {
                          ruc = tMatriz.ruc,
                          razonsocial = tMatriz.razonsocial,
                          nombreComercial = tMatriz.nombreComercial,
                          dirMatriz = tMatriz.dirMatriz,
                          contribuyenteEspecial = tMatriz.contribuyenteEspecial,
                          obligadoContabilidad = tMatriz.obligadoContabilidad,
                          e_mail = tMatriz.e_mail,
                          telefono = tMatriz.telefono,
                      };

        foreach (var registro in cMatriz)
        {
            txtRuc.Text = registro.ruc;
            txtrazonsocial.Text = registro.razonsocial;
            txtnombreComercial.Text = registro.nombreComercial;
            txtdirMatriz.Text = registro.dirMatriz;
            txtcontribuyenteEspecial.Text = registro.contribuyenteEspecial;
            ddlObligado.SelectedValue = registro.obligadoContabilidad;
            txtEmail.Text = registro.e_mail;
            txtTel.Text = registro.telefono;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string accion, ruc, razonsocial, nombreComercial, dirMatriz, contribuyenteEspecial, obligadoContabilidad, e_mail, telefono;

        /* CONSTANTES */
        accion = "GUARDAR";

        /*VARIABLES*/
        
        ruc = txtRuc.Text;
        razonsocial = txtrazonsocial.Text;
        nombreComercial = txtnombreComercial.Text;
        dirMatriz = txtdirMatriz.Text;
        contribuyenteEspecial = txtcontribuyenteEspecial.Text;
        obligadoContabilidad = ddlObligado.SelectedValue;
        e_mail = txtEmail.Text;
        telefono = txtTel.Text;

        /*VALIDAR INFORMACION*/

        if (ruc.Length < 10
            || razonsocial.Length <= 1
            || nombreComercial.Length <= 3
            || dirMatriz.Length <= 20
            || telefono.Length <= 8
            || e_mail.Length <= 10
            || ruc == "1790349578001")
        {
            if (ruc == "1790349578001")
            {
                lblmensaje.Text = "LA INFORMACIÓN DE ANETA NO SE DEBE MODIFICAR";
            }
            else { lblmensaje.Text = "Ingrese toda la información,identificación válido,razón social, la dirección debe tener provincia, ciudad, calles y sector, teléfono con código de provincia"; }
        }
        else
        {
            /*GUARDAR INFORMACION*/
            dc.sp_abmMatriz2(accion, ruc, razonsocial, nombreComercial, dirMatriz, contribuyenteEspecial, obligadoContabilidad, e_mail, telefono);
            blanquearSucursal();
            lblmensaje.Text = razonsocial.Trim() + "guardado correctamente";
        }
    }

    protected void blanquearSucursal()
    {
        txtRuc.Text = string.Empty;
        txtrazonsocial.Text = string.Empty;
        txtnombreComercial.Text = string.Empty;
        txtdirMatriz.Text = string.Empty;
        txtcontribuyenteEspecial.Text = string.Empty;
        ddlObligado.SelectedValue = string.Empty;
        txtEmail.Text = string.Empty;
        txtTel.Text = string.Empty;
    }
}