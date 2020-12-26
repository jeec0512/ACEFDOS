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
using acefdos;

public partial class Escuela_imprimirActas : System.Web.UI.Page
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
            lblMensaje.Text = "";
            string accion = string.Empty;
            perfilUsuario();
            listarModalidad();
            listarCurso();
            listarPedido();
            listarProvincias();
            listarCiudades();

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
                Response.Redirect("~/inicio.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];



            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/inicio.aspx");
            }

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/inicio.aspx");
            lblMensaje.Text = e.Message;
        }
    }

    #endregion

    #region LISTAR HORARIOS FECHA HORAS TALLERES, MATERIAS
    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listMod = new ListItem("Seleccione la modalidad", "-1");

        ddlModalidad.Items.Insert(0, listMod);
    }

    protected void listarCurso()
    {
        var cCurso = ds.sp_abmCurso("", 0, 0, "", "", DateTime.Now, DateTime.Now, false, "", DateTime.Now);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();
        ListItem listCur = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCur);
    }

    protected void listarPedido()
    {
        string Accion = "PEDIDOS";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        string tipopedido = "A";


            var cPedido = ds.sp_abmPedidos(Accion, 0, "", sucursal, 0, 0, "", tipopedido, "", DateTime.Today, false, curso);

            ddlPedido.DataSource = cPedido;
        ddlPedido.DataBind();

        ListItem listCon = new ListItem("Seleccione pedido", "-1");

        ddlPedido.Items.Insert(0, listCon);

    }

    protected void listarCiudades()
    {
        var cCiudad = dc.sp_ListarCiudades("TIPO", "ESCUELA");

        ddlCiudad.DataSource = cCiudad;
        ddlCiudad.DataBind();

        ListItem listCiu = new ListItem("Seleccione ciudad", "-1");

        ddlCiudad.Items.Insert(0, listCiu);
    }

    protected void listarProvincias()
    {
        var cProvincia = dc.sp_ListarProvincias("TODOS", "ESCUELA");

        ddlProvincia.DataSource = cProvincia;
        ddlProvincia.DataBind();

        ListItem listPro = new ListItem("Seleccione provincia", "-1");

        ddlCiudad.Items.Insert(0, listPro);
    }
    #endregion

    /*CHECKEDS*/
    #region CONTROL DE ACTIVACIONES DISPONIBLES
    
    /***************/

    /*LISTADO DE AULAS/AUTOS ACTIVADOS*/
    protected void cbActivarHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gridViewRow in grvCursoDetalle.Rows)
        {
            ((CheckBox)gridViewRow.FindControl("cbActivarConfirmar")).Checked = ((CheckBox)sender).Checked;

        }

    }

    protected void cbActivarConfirmar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerCheckBox = (CheckBox)grvCursoDetalle.HeaderRow.FindControl("cbActivarHeader");
        if (headerCheckBox.Checked)
        {
            headerCheckBox.Checked = ((CheckBox)sender).Checked;
        }
        else
        {
            bool allCheckBoxesChecked = true;
            foreach (GridViewRow gridVieRow in grvCursoDetalle.Rows)
            {
                if (!((CheckBox)gridVieRow.FindControl("cbActivarConfirmar")).Checked)
                {
                    allCheckBoxesChecked = false;
                    break;
                }
            }
            headerCheckBox.Checked = allCheckBoxesChecked;
        }

    }

    /***************/


    #endregion

    #region VERIFICAR SI ACTIVARON AULAS,HORAS,DIAS
    protected Tuple<string, bool> verificarActivaciones(string horarios, string confirmar, string nota, GridView grid)
    {
        string respuesta = string.Empty;
        bool pasa = true;

        List<string> lstParaPedido = new List<string>();

        foreach (GridViewRow gridViewRow in grid.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl(confirmar)).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl(nota)).Text;
                lstParaPedido.Add(regId);
            }
        }
        if (lstParaPedido.Count > 0)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Navy;
            respuesta = lstParaPedido.Count.ToString() + " " + horarios + "(s) confirmado(as)";
            pasa = true;
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            respuesta = "No existen " + " " + horarios + "(s) confirmado(as)";
            pasa = false;
        }
        var tuple = new Tuple<string, bool>(respuesta, pasa);
        return tuple;
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        List<string> lstEstudiantesParaPedido = new List<string>();

        foreach (GridViewRow gridViewRow in grvCursoDetalle.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cbConfirmar")).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl("lblIdNota")).Text;
                lstEstudiantesParaPedido.Add(regId);
            }
        }
        if (lstEstudiantesParaPedido.Count > 0)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Navy;

            /*foreach (string strRegId in lstEstudiantesParaPedido) 
            {
                estudianteDataAccessLayer.confirmarEstudiantes(Convert.ToInt32(strRegId));
                
            }*/

            estudianteDataAccessLayer.confirmarEstudiantes(lstEstudiantesParaPedido);

            lblMensaje.Text = lstEstudiantesParaPedido.Count.ToString() + "fila(s) confirmadas";
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = "No existen fila(s) confirmadas";
        }
    }
    #endregion

    #region CAMBIOS AL SELECCIONAR DROPDOWNLIST
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlModalidad_SelectedIndexChanged();
    }

    protected void ddlModalidad_SelectedIndexChanged()
    {
        lblMensaje.Text = "";
        string accion = "MODACTIVOS";
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso(accion, 0, modalidad, "", "", DateTime.Now, DateTime.Now, false, "", DateTime.Now);
        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCur = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCur);
        listarPedido();
    }
    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarPedido();
    }
    #endregion

    #region LISTAR GRIDS
    
    

    #endregion


    #region BOTONES DE ACCIÓN
    
    #endregion

    #region ACCIONES SOBRE ACTIVACIONES REALIZADAS
    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMensaje.Text = string.Empty;

        if (e.CommandName == "EliminaReg")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            //int lid = Convert.ToInt32(row.Cells[1].Text);

            string regId = ((Label)row.FindControl("lblIdActivar")).Text;
            int lid = Convert.ToInt32(regId);

            var deleteOrderDetails =
                from details in ds.TB_ASIGNA_MATERIA
                where details.ASM_ID == lid
                select details;

            foreach (var detail in deleteOrderDetails)
            {
                ds.TB_ASIGNA_MATERIA.DeleteOnSubmit(detail);
            }

            try
            {
                ds.SubmitChanges();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
                lblMensaje.Text = "Se encuentra alumnos registrados en este horario.." + Convert.ToString(exp).Substring(0, 200);
                // Provide for exceptions.
            }
        }
    }
    #endregion

    #region CONFIRMARACTIVACIONES
    
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
       
        dvListado.Visible = true;
        listarModalidad();
        ddlModalidad_SelectedIndexChanged();
    }
    protected void ddlPedido_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvListado.Visible = true;
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();

        var cCurso = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();

        lblMensaje.Text = "";
        if (pedido == "-1")
        {
            lblMensaje.Text = "Seleccione un pedido";
        }
        else
        {
            grvCursoDetalle.DataSource = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
            grvCursoDetalle.DataBind();

            
        }
    }
}