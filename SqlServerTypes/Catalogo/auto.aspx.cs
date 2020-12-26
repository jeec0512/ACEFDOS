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

public partial class Catalogo_auto : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();


    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["AdmBitaAutoConnectionString"].ConnectionString;

    Data_AdmBitaAutoDataContext da = new Data_AdmBitaAutoDataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();
            activarObjetos();
            listarAutos();
        }
    }
    #endregion

    #region PERFIL USUARIO

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

            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }
    }

    #endregion

    #region METODOS GENERALES
    protected void activarObjetos()
    {
        lblMensaje.Text = string.Empty;
        txtVeh_id.Text = "0";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";
        int usuario = Convert.ToInt32(Session["SUsuarioID"]);
        int Veh_id = Convert.ToInt32(txtVeh_id.Text);
        string sucursal = ddlSucursal.SelectedValue;
        string marca = txtMarca.Text;
        string modelo = txtModelo.Text;
        decimal ano = Convert.ToDecimal(txtAno.Text);
        string numero = txtNumero.Text;
        string chasis = txtChasis.Text;
        string motor = txtMotor.Text;
        string placa = txtPlaca.Text;
        decimal suc_id = Convert.ToDecimal(txtSuc_Id.Text);
        decimal tve_id = Convert.ToDecimal(txtTve_id.Text);
        decimal per_id = Convert.ToDecimal(txtPer_id.Text);
        bool estado = Convert.ToBoolean(chkEstado.Checked);
        
        //string usuario = (string)Session["SUsername"];
        //string fechaModificacion = Convert.ToString(DateTime.Now);

        bool pasa = validarDatos(sucursal, marca,modelo,ano,numero,chasis,motor,placa,suc_id,tve_id,per_id);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            //ds.sp_abmAuto(Accion, Veh_id, marca, modelo, ano, numero, chasis, motor, placa, suc_id, tve_id, per_id, sucursal, estado);
            ds.sp_abmAuto_escuela(Accion,0,marca,modelo,ano,numero,chasis,motor,placa,suc_id,estado,DateTime.Now,DateTime.Now,usuario,usuario,"",sucursal);
            blanquearObjetos();
            lblMensaje.Text = chasis.Trim() + " guardado correctamente";
        }

        listarAutos();
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtVeh_id.Text = "0";
        txtMarca.Text = string.Empty;
        txtModelo.Text = string.Empty;
        txtAno.Text = string.Empty;
        txtNumero.Text = string.Empty;
        txtChasis.Text = string.Empty;
        txtMotor.Text = string.Empty;
        txtPlaca.Text = string.Empty;
        txtSuc_Id.Text = string.Empty;
        txtTve_id.Text = string.Empty;
        txtPer_id.Text = string.Empty;

        

    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, string marca,string modelo,decimal ano,string numero,string chasis,string motor,string placa,decimal suc_id,decimal tve_id,decimal per_id)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || marca.Length <= 2
            || modelo.Length <= 2
            || numero.Length <= 2
            || chasis.Length <= 2
            || motor.Length <= 2
            || placa.Length <= 2
)
        {
            pasa = false;
        };

        if (ano <= 0
            || suc_id <= 0
            || tve_id <= 0
            || per_id <= 0)
        {
            pasa = false;
        }

        return pasa;

    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ibConsultar_Click(int id)
    {
        string Accion = "CONSULTAR";

        string sucursal = ddlSucursal.SelectedValue;
        txtVeh_id.Text = Convert.ToString(id);


        //var cAula = ds.sp_abmAuto(Accion, id, "", "", 0, "", "", "", "", 0, 0, 0, sucursal,false);
        var cAula = ds.sp_abmAuto_escuela(Accion, id, "", "", 0, "", "", "", "", 0, false, DateTime.Now, DateTime.Now, 0, 0, "", sucursal);

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            txtMarca.Text = registro.VEH_MARCA;
            txtModelo.Text = registro.VEH_MODELO;
            txtAno.Text = Convert.ToString(registro.VEH_ANIO);
            txtNumero.Text = registro.VEH_NUMERO;
            txtChasis.Text = registro.VEH_CHASIS;
            txtMotor.Text = registro.VEH_MOTOR;
            txtPlaca.Text = registro.VEH_PLACA;
            txtSuc_Id.Text = Convert.ToString(registro.SUC_ID);
           
            /*txtTve_id.Text = Convert.ToString(registro.TVE_ID);
            txtPer_id.Text = Convert.ToString(registro.PER_ID);*/


        }


        txtMarca.Focus();
    }


    protected void listarAutos()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cEgresos = ds.sp_abmAuto_escuela(Accion, 0, "", "", 0, "", "", "", "", 0, false, DateTime.Now, DateTime.Now, 0, 0, "",sucursal);

        grvAutoDetalle.DataSource = cEgresos;
        grvAutoDetalle.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvAutoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[1].Text);


            ibConsultar_Click(lidCurso);

        }
    }


    protected void grvAutoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        listarAutos();
    }
}