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

public partial class Catalogo_horario : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();
            activarObjetos();
            llenarListas();
            listarMateria();
            
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
        txtHor_id.Text = "0";
        txtHoraInicio.Text = "0000";
        txtHoraFin.Text = "0000";

       
    }
    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listCon = new ListItem("Seleccione Modalidad", "-1");

        ddlModalidad.Items.Insert(0, listCon);
        listarMateriaXMod();

    }
    protected void llenarListas() {
        listarModalidad();

        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string accion = string.Empty;
        if (modalidad == 5 || modalidad == 6)
        {
            accion = "RECUPERA";
        }
        else {
            accion = "CURSO";
        }

        listarMateriaXMod();

        /*
        var cMateria = ds.sp_abmMateria(accion, 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione Materia", "-1");

        ddlMateria.Items.Insert(0, listCon);
        */

        accion = "TODOS";
        var cTurno = ds.sp_abmTurno(accion, 0, "", false, 0, DateTime.Today, 0, DateTime.Today);

        ddlTurno.DataSource = cTurno;
        ddlTurno.DataBind();

        ListItem listTur = new ListItem("Seleccione turno", "-1");

        ddlTurno.Items.Insert(0, listTur);


    }

    protected void listarMateriaXMod()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string accion = string.Empty;
        if (modalidad == 5 || modalidad == 6)
        {
            accion = "RECUPERA";
        }
        else
        {
            accion = "CURSO";
        }

        var cMateria = ds.sp_abmMateria(accion, 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione Materia", "-1");

        ddlMateria.Items.Insert(0, listCon);

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        int hor_id = Convert.ToInt32(txtHor_id.Text);
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        string sucursal = ddlSucursal.SelectedValue;
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        string horaInicio = txtHoraInicio.Text.Trim();
        string horaFin = txtHoraFin.Text.Trim();
        int turno = Convert.ToInt32(ddlTurno.SelectedValue);
        int activo = Convert.ToInt32(chkEstado.Checked);
        int nActivo = 0;
        if (activo == 0)
        {
            nActivo = 0;
        }
        else
        {
            nActivo = 1;
        }
        string usuario = (string)Session["SUsername"];
        string fechaModificacion = Convert.ToString(DateTime.Now);

        bool pasa = validarDatos(sucursal, modalidad,horaInicio,horaFin, activo, usuario, fechaModificacion,materia,turno);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            ds.sp_abmHorario2(Accion, hor_id, 0, horaInicio,horaFin, Convert.ToBoolean(nActivo), usuario, Convert.ToDateTime(fechaModificacion),sucursal, modalidad,materia,turno);
            blanquearObjetos();
            lblMensaje.Text = horaInicio.Trim() + " guardado correctamente";
        }

        listarMateria();
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtHor_id.Text = "0";
        chkEstado.Checked = false;
        txtHoraInicio.Text = "0000";
        txtHoraFin.Text = "0000";
    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, int modalidad, string horaInicio, string horaFin
                , int activo, string usuario, string fechaModificacion, int materia, int turno)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || horaInicio.Length <= 2
            || horaFin.Length <= 2
            || usuario.Length <= 2
)
        {
            pasa = false;
        };

        if (modalidad == -1
            || materia == -1)
        {
            pasa = false;
        }

        if (turno == -1
            || turno == -1)
        {
            pasa = false;
        }


        if (fechaModificacion.Length <= 2)
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
        txtHor_id.Text = Convert.ToString(id);
        int horario = Convert.ToInt32(ddlMateria.SelectedValue);


        var cAula = ds.sp_abmHorario(Accion, id, 0, "", "", false, "", DateTime.Today, sucursal, 0, horario);

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            ddlSucursal.SelectedValue = registro.SUCURSAL;
            ddlModalidad.SelectedValue = Convert.ToString(registro.MOD_ID);
            txtHoraInicio.Text = registro.HOR_INICIO;
            txtHoraFin.Text = registro.HOR_FIN;
            chkEstado.Checked = Convert.ToBoolean(registro.HOR_ESTADO);

        }


        txtHoraInicio.Focus();
    }


    protected void listarMateria()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int horario = Convert.ToInt32(ddlMateria.SelectedValue);

        var cEgresos = ds.sp_abmHorario(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal, modalidad,horario);

        grvHorarioDetalle.DataSource = cEgresos;
        grvHorarioDetalle.DataBind();
        

    }
    #endregion

    #region GRILLAS
    protected void grvHorarioDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[2].Text);


            ibConsultar_Click(lidCurso);

        }
        if (e.CommandName == "eliReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidHorario = Convert.ToInt32(row.Cells[2].Text);


            try
            {
                horarioExiste(lidHorario);
                lblMensaje.Text = "elimina registro" + lidHorario;
                ds.sp_abmHorario("BORRAR", lidHorario, 0, "", "", false, "", DateTime.Now, "", 0, 0);
                ddlMateria_SelectedIndexChanged();
            }
            catch (Exception esep)
            {
                lblMensaje.Text = "Error de eliminación: " + esep.Message;
            }

            /* tbl_secuenciales tbl_secuenciales = dc.tbl_secuenciales.SingleOrDefault(p => p.sucursal == suc);
            tbl_secuenciales.retencion = sec;
            dc.SubmitChanges();*/

            
            

        }
    }


    protected void horarioExiste(int lidHorario)
    {
        int hora = 0;
        
        /*var cHor = from mHor in ds.TB_HORARIO
                   where mHor.HOR_ID == lidHorario
                   select new
                   {
                       HOR_ID = mHor.HOR_ID
                   };
        */

        var cHor = from mHor in ds.TB_ASIGNA_MATERIA
                   where mHor.HOR_ID == lidHorario
                   select new
                   {
                       HOR_ID = mHor.HOR_ID
                   };

        foreach (var regNum in cHor)
        {
            hora = Convert.ToInt32(regNum.HOR_ID);
        };

        if (hora > 0)
        {
            throw new InvalidOperationException("El Horario está creado en cupos");
        }
    }


    protected void grvHorarioDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        listarMateria();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();

        //llenarListas();
        listarMateriaXMod();

    }
    protected void ddlSucursal_SelectedIndexChanged1(object sender, EventArgs e)
    {
        activarObjetos();
        listarMateria();
    }

    protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria_SelectedIndexChanged();
    }

    protected void ddlMateria_SelectedIndexChanged()
    {
        activarObjetos();
        listarMateria();
    }
}

