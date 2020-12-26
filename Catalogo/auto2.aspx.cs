using AjaxControlToolkit;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Catalogo_auto2 : System.Web.UI.Page
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
            listarCiudades();
           // listarEscuelas();
            listarModalidad();
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


            ddlEscuela.DataSource = cSucursal;
            ddlEscuela.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }
    }

    #endregion

    #region LISTAR
    /*LISTAR CIUDAD*/

    protected void listarCiudades()
    {

        var cCiudad = dc.sp_ListarCiudades("TIPO", "ESCUELA");

        ddlCiudad.DataSource = cCiudad;
        ddlCiudad.DataBind();

        ListItem listCiu = new ListItem("Seleccione ciudad", "-1");

        ddlCiudad.Items.Insert(0, listCiu);


    }

    /*LISTAR SUCURSAL*/

    protected void listarEscuelas()
    {
        string ciudad = ddlCiudad.SelectedValue.Trim();
        string sucursal = ddlSucursal.SelectedValue;
        //var cSucursal2 = dc.sp_listarSucursal("", "", 4, 0, sucursal);
        var cSucursal2 = dc.sp_listarSucursal2("TODOS", "", 12, 0, "", ciudad);

        ddlEscuela.DataSource = cSucursal2;
        ddlEscuela.DataBind();

        ListItem listEsc = new ListItem("Seleccione escuela", "-1");

        ddlEscuela.Items.Insert(0, listEsc);
    }

    /*LISTAR MODALIDAD*/
    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmTipoVehiculo("TODOS", 0, "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listCon = new ListItem("Seleccione tipo", "-1");

        ddlModalidad.Items.Insert(0, listCon);

    }
    #endregion

    #region SELECTED
    protected void ddlCiudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*listarEscuelas();
        listarModalidad();
        ddlModalidad_SelectedIndexChanged();*/
        
    }

    protected void ddlEscuela_SelectedIndexChanged(object sender, EventArgs e)
    {

        listarModalidad();
        ddlModalidad_SelectedIndexChanged();
        
    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModalidad_SelectedIndexChanged();
    }

 
    protected void ddlModalidad_SelectedIndexChanged()
    {
        string accion = "XTIPO";
        string escuela = ddlEscuela.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);


        var cAutos = ds.sp_abmAuto_escuela2(accion, 0, "", "", 0, "", "", "", "", 0, false, DateTime.Now, DateTime.Now, 0, 0, "", escuela, modalidad);
        grvAutos.DataSource = cAutos;
        grvAutos.DataBind();
    }


    #endregion

    #region GRILLAS
    protected void grvAutos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutos.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[1].Text);

            abilitarObjetos();
            ibConsultar_Click(lidCurso);
            txtPlaca.Enabled = false;

        }
    }


    protected void grvAutos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarObjetos();
        //listarAutos();
    }

    protected void ddlEscuela2_SelectedIndexChanged(object sender, EventArgs e)
    {
        //activarObjetos();
        //listarAutos();
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
        //var cAula = ds.sp_abmAuto_escuela(Accion, id, "", "", 0, "", "", "", "", 0, false, DateTime.Now, DateTime.Now, 0, 0, "", sucursal);
        var cAula = ds.sp_abmAuto_escuela2(Accion,id,"","",0,"","","","",0,false,DateTime.Now,DateTime.Now,0,0,"","",0);

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
            chkEstado.Checked = Convert.ToBoolean(registro.VEH_ACTIVO);

            /*txtTve_id.Text = Convert.ToString(registro.TVE_ID);
            txtPer_id.Text = Convert.ToString(registro.PER_ID);*/


        }


        txtMarca.Focus();
    }
    #endregion
    
    #region MATENIMIENTO

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensaje.Visible = true;
            int Veh_id = 0;
            string hacer = lblNuevo.Text.Trim();
            string Accion = string.Empty;
            if (hacer == "N")
            {
                Accion = "AGREGAR";
                Veh_id = 0;
            }
            else
            {
                Accion = "MODIFICAR";
                Veh_id = Convert.ToInt32(txtVeh_id.Text);
            }
            int usuario = Convert.ToInt32(Session["SUsuarioID"]);

            //string sucursal = ddlSucursal.SelectedValue;
            string escuela = ddlEscuela.SelectedValue;
            string marca = txtMarca.Text;
            string modelo = txtModelo.Text;
            decimal ano = Convert.ToDecimal(txtAno.Text);
            string numero = txtNumero.Text;
            string chasis = txtChasis.Text;
            string motor = txtMotor.Text;
            string placa = txtPlaca.Text;
            int tipo = Convert.ToInt32(ddlModalidad.SelectedValue);


            int suc_id = 0;
            /* TRAER CONCEPTOS*/
            var cSuc = from mSuc in df.PUNTO_VENTA
                       where mSuc.PVE_SUCURSAL == escuela
                       select new
                       {
                           suc_id = mSuc.PVE_ID
                       };

            foreach (var regNum in cSuc)
            {
                suc_id = Convert.ToInt32(regNum.suc_id);
            };



            //int tve_id = Convert.ToInt32(txtTve_id.Text);
            //decimal per_id = Convert.ToDecimal(txtPer_id.Text);
            bool estado = Convert.ToBoolean(chkEstado.Checked);
            int usuid = Convert.ToInt32(Session["SUsuarioID"]);
            string userName = (string)Session["SUsername"];
            //string fechaModificacion = Convert.ToString(DateTime.Now);

            int tipoVeh = Convert.ToInt32(ddlModalidad.Text);

            bool pasa = validarDatos(escuela, marca, modelo, ano, numero, chasis, motor, placa, suc_id, tipoVeh);

            if (!pasa)
            {
                throw new InvalidCastException("Ingrese toda la información solicitada");
                //lblMensaje.Text = "Ingrese toda la información solicitada";
            }
            else
            {
                /*GUARDAR INFORMACION*/
                //ds.sp_abmAuto(Accion, Veh_id, marca, modelo, ano, numero, chasis, motor, placa, suc_id, tve_id, per_id, sucursal, estado);
                //ds.sp_abmAuto_escuela(Accion, 0, marca, modelo, ano, numero, chasis, motor, placa, suc_id, estado, DateTime.Now, DateTime.Now, usuario, usuario, "", sucursal);
                /*foreach (var regNum in cSuc)
                {
                    suc_id = Convert.ToInt32(regNum.suc_id);
                };*/

               


                ds.sp_abmAuto_escuela2(Accion, Veh_id, marca, modelo, ano, numero, chasis, motor, placa, suc_id, estado, DateTime.Now, DateTime.Now, usuid, usuid, "", "", tipo);




                if (hacer == "N")
                {
                    var cVeh = from mVeh in ds.TB_VEHICULO
                               where mVeh.VEH_PLACA == placa
                               select new
                               {
                                   veh_id = mVeh.VEH_ID
                               };
                    foreach (var regNum in cVeh)
                    {
                        Veh_id = Convert.ToInt32(regNum.veh_id);
                    };
                    if (tipoVeh == 1)
                    {
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 1, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 2, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 3, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);

                        Accion = "MODIFICAR";

                    }

                    if (tipoVeh == 2)
                    {
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 9, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 10, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 11, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);
                    }

                    if (tipoVeh == 3)
                    {
                        ds.sp_abmAutoModalidad("AGREGAR", 0, 8, Veh_id, true, DateTime.Now, DateTime.Now, userName, userName);
                    }

                    ds.sp_abmAuto_escuela2(Accion, Veh_id, marca, modelo, ano, Convert.ToString(Veh_id), chasis, motor, placa, suc_id, estado, DateTime.Now, DateTime.Now, usuid, usuid, "", "", tipo);
                }

                lblMensaje.Text = chasis.Trim() + " guardado correctamente";
            }
            btnCancelar_Click();
            ddlModalidad_SelectedIndexChanged();
        }
        catch (Exception ex) {
            lblMensaje.Text = ex.Message;
        }
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

    protected bool validarDatos(string sucursal, string marca, string modelo, decimal ano, string numero, string chasis, string motor, string placa, int suc_id, int tipo)
    {
        bool pasa = true;

        

        if (ano <= 0
            || suc_id <= 0)
        {
            pasa = false;
            throw new InvalidCastException("Ingrese año");
        }

        if (tipo == -1)
        {
            pasa = false;
            throw new InvalidCastException("Ingrese tipo de equipo");
        }
//|| numero.Length < 1
        if (sucursal.Length < 2
            || marca.Length <= 2
            || modelo.Length <= 2
            || chasis.Length <= 2
            || motor.Length <= 2
            || placa.Length <= 2
)
        {
            pasa = false;
            throw new InvalidCastException("Ingrese toda la información solicitada, marca modelo,etc");
        };

        return pasa;

    }



    protected void btnNuevoVehiculo_Click(object sender, EventArgs e)
    {
        lblNuevo.Text = "N";
        abilitarObjetos();
        txtPlaca.Enabled = true;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();
    }

    protected void btnCancelar_Click()
    {
        lblNuevo.Text = "";
        pnEscuela.Enabled = true;
        pnListaAutos.Visible = true;
        pnActualizacion.Visible = false;
        blanquearObjetos();
    }

    protected void abilitarObjetos() {
        pnEscuela.Enabled = false;
        pnListaAutos.Visible = false;
        pnActualizacion.Visible = true;
    }

    #endregion
    
}