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


public partial class Escuela_ingresoNotas : System.Web.UI.Page
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
            listarCurso();
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
    }

    protected void listarCurso()
    {
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        var cCurso = ds.sp_abmCurso("TODOS", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

        ddlCurso.DataSource = cCurso;
        ddlCurso.DataBind();

        ListItem listCon = new ListItem("Seleccione Curso", "-1");

        ddlCurso.Items.Insert(0, listCon);

    }

    protected void listarModalidad()
    {
        ddlModalidad.SelectedValue = "-1";

    }


    protected void listarMateria()
    {
        var cMateria = ds.sp_abmMateria("TODOS", 0, "", 0, "", DateTime.Today);

        ddlMateria.DataSource = cMateria;
        ddlMateria.DataBind();

        ListItem listCon = new ListItem("Seleccione Materia", "-1");

        ddlMateria.Items.Insert(0, listCon);

    }

    protected void listarAuto()
    {
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var cAuto = ds.sp_HorariosAsignadosVehiculos(Accion,sucursal,curso,materia);

        grvAutoDetalle.DataSource = cAuto;
        grvAutoDetalle.DataBind();

    }

    protected void listarAula()
    {
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var cAula = ds.sp_HorariosAsignadosAulas(Accion,sucursal,curso,materia);

        grvAulaDetalle.DataSource = cAula;
        grvAulaDetalle.DataBind();

    }

    protected void listarHorario()
    {

        var cHorario = new object();
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        int auto = 0; // traer el auto
        int aula = 0; // traer el aula

        if (materia == 7)
        {
            Accion = "REGAUTOS";
            cHorario = ds.sp_HorariosAsignadosXAuto(Accion, sucursal, curso, materia, auto, 0);
        }
        else
        { 
            Accion = "REGAULAS";
            cHorario = ds.sp_HorariosAsignadosXMateria(Accion, sucursal, curso, materia, aula, 0); 
        }

       

        grvHorarioDetalle.DataSource = cHorario;
        grvHorarioDetalle.DataBind();

    }


    protected void listarTaller()
    {
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var ctaller = ds.sp_HorariosAsignadosTalleres(Accion, sucursal, curso, materia);

        grvTallerDetalle.DataSource = ctaller;
        grvTallerDetalle.DataBind();

    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;
        string usuario = (string)Session["SUsername"];
        DateTime fecha = DateTime.Now;


        string accion = "nada";
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        int kont = 0;
        //  var cAsignaciones = ds.sp_VerHorarios(accion, sucursal, modalidad, curso, materia,usuario,fecha);



        bool estado1 = false;
        bool estado2 = false;
        bool estado3 = false;
        int aul_id = 0;
        int veh_id = 0;
        int hor_id = 0;
        int tal_id = 0;

        /*ASIGNACION POR MATERIA*/
        /*EDUCACION VIAL*/
        if (materia == 3)
        {

            for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    aul_id = Convert.ToInt32(grvAulaDetalle.Rows[i].Cells[2].Text);

                    for (int j = 0; j < grvHorarioDetalle.Rows.Count; j++)
                    {
                        estado2 = Convert.ToBoolean(grvHorarioDetalle.Rows[j].Cells[0].Text);

                        if (estado2)
                        {
                            hor_id = Convert.ToInt32(grvHorarioDetalle.Rows[j].Cells[2].Text);
                            kont = 0;
                            var cuentaE = ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                            foreach (var registro in cuentaE)
                            {
                                kont = Convert.ToInt32(registro.educvial);

                            }
                            if (kont <= 0)
                            {
                                ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                            }
                        }
                    }

                }
            }
        }


        if (modalidad == 1)
        {
            if (materia == 4)
            {
                accion = "QUINCE";
                for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
                {
                    estado1 = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

                    if (estado1)
                    {
                        aul_id = Convert.ToInt32(grvAulaDetalle.Rows[i].Cells[2].Text);

                        for (int j = 0; j < grvHorarioDetalle.Rows.Count; j++)
                        {
                            estado2 = Convert.ToBoolean(grvHorarioDetalle.Rows[j].Cells[0].Text);

                            if (estado2)
                            {
                                hor_id = Convert.ToInt32(grvHorarioDetalle.Rows[j].Cells[2].Text);
                                kont = 0;
                                var cuentaM = ds.sp_cuentaHorariosMecanica(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                foreach (var registro in cuentaM)
                                {
                                    kont = Convert.ToInt32(registro.mecanica);

                                }
                                if (kont <= 0)
                                {
                                    ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                }
                            }
                        }

                    }
                }
            }
        }
        else
        {
            if (materia == 4)
            {
                accion = "SIETE";
                for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
                {
                    estado1 = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

                    if (estado1)
                    {
                        aul_id = Convert.ToInt32(grvAulaDetalle.Rows[i].Cells[2].Text);

                        for (int j = 0; j < grvHorarioDetalle.Rows.Count; j++)
                        {
                            estado2 = Convert.ToBoolean(grvHorarioDetalle.Rows[j].Cells[0].Text);

                            if (estado2)
                            {
                                hor_id = Convert.ToInt32(grvHorarioDetalle.Rows[j].Cells[2].Text);

                                for (int K = 0; K < grvTallerDetalle.Rows.Count; K++)
                                {
                                    estado3 = Convert.ToBoolean(grvTallerDetalle.Rows[K].Cells[0].Text);

                                    if (estado3)
                                    {
                                        tal_id = Convert.ToInt32(grvTallerDetalle.Rows[K].Cells[2].Text);
                                        kont = 0;
                                        var cuentaM = ds.sp_cuentaHorariosMecanica(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                        foreach (var registro in cuentaM)
                                        {
                                            kont = Convert.ToInt32(registro.mecanica);

                                        }
                                        if (kont <= 0)
                                        {


                                            ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        /*PSICOLOGIA PRIMEROS AUXILIOS*/

        /*PSICOLOGIA */
        estado1 = false;
        estado2 = false;
        estado3 = false;
        aul_id = 0;
        veh_id = 0;
        hor_id = 0;
        tal_id = 0;


        if (materia == 5)
        {

            for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    aul_id = Convert.ToInt32(grvAulaDetalle.Rows[i].Cells[2].Text);

                    for (int j = 0; j < grvHorarioDetalle.Rows.Count; j++)
                    {
                        estado2 = Convert.ToBoolean(grvHorarioDetalle.Rows[j].Cells[0].Text);

                        if (estado2)
                        {
                            hor_id = Convert.ToInt32(grvHorarioDetalle.Rows[j].Cells[2].Text);

                            for (int K = 0; K < grvTallerDetalle.Rows.Count; K++)
                            {
                                estado3 = Convert.ToBoolean(grvTallerDetalle.Rows[K].Cells[0].Text);

                                if (estado3)
                                {
                                    tal_id = Convert.ToInt32(grvTallerDetalle.Rows[K].Cells[2].Text);
                                    kont = 0;
                                    var cuentaPs = ds.sp_cuentaHorariosPsicologia(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                    foreach (var registro in cuentaPs)
                                    {
                                        kont = Convert.ToInt32(registro.psico);

                                    }
                                    if (kont <= 0)
                                    {
                                        ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        /*PRIMEROS AUXILIOS*/
        estado1 = false;
        estado2 = false;
        estado3 = false;
        aul_id = 0;
        veh_id = 0;
        hor_id = 0;
        tal_id = 0;


        if (materia == 6)
        {

            for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    aul_id = Convert.ToInt32(grvAulaDetalle.Rows[i].Cells[2].Text);

                    for (int j = 0; j < grvHorarioDetalle.Rows.Count; j++)
                    {
                        estado2 = Convert.ToBoolean(grvHorarioDetalle.Rows[j].Cells[0].Text);

                        if (estado2)
                        {
                            hor_id = Convert.ToInt32(grvHorarioDetalle.Rows[j].Cells[2].Text);

                            for (int K = 0; K < grvTallerDetalle.Rows.Count; K++)
                            {
                                estado3 = Convert.ToBoolean(grvTallerDetalle.Rows[K].Cells[0].Text);

                                if (estado3)
                                {
                                    tal_id = Convert.ToInt32(grvTallerDetalle.Rows[K].Cells[2].Text);
                                    kont = 0;
                                    var cuentaPA = ds.sp_cuentaHorariosPrimerosAuxilios(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                    foreach (var registro in cuentaPA)
                                    {
                                        kont = Convert.ToInt32(registro.primaux);

                                    }
                                    if (kont <= 0)
                                    {
                                        ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        /*PRACTICA*/

        accion = "PRACTICA";
        estado1 = false;
        estado2 = false;
        estado3 = false;
        aul_id = 0;
        veh_id = 0;
        hor_id = 0;
        tal_id = 0;

        if (materia == 7)
        {

            for (int i = 0; i < grvAutoDetalle.Rows.Count; i++)
            {
                estado1 = Convert.ToBoolean(grvAutoDetalle.Rows[i].Cells[0].Text);

                if (estado1)
                {
                    veh_id = Convert.ToInt32(grvAutoDetalle.Rows[i].Cells[2].Text);

                    for (int j = 0; j < grvHorarioDetalle.Rows.Count; j++)
                    {
                        estado2 = Convert.ToBoolean(grvHorarioDetalle.Rows[j].Cells[0].Text);

                        if (estado2)
                        {
                            hor_id = Convert.ToInt32(grvHorarioDetalle.Rows[j].Cells[2].Text);
                            kont = 0;
                            var cuentaPrac = ds.sp_cuentaHorariosPractica(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                            foreach (var registro in cuentaPrac)
                            {
                                kont = Convert.ToInt32(registro.practica);

                            }
                            if (kont <= 0)
                            {
                                ds.sp_generaHorarios2(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
                            }
                        }
                    }

                }
            }
        }



       
    }

    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;

    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, int modalidad, string horaInicio, string horaFin, int activo, string usuario, string fechaModificacion, int materia)
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
        int horario = Convert.ToInt32(ddlMateria.SelectedValue);


        var cAula = ds.sp_abmHorario(Accion, id, 0, "", "", false, "", DateTime.Today, sucursal, 0, horario);

        foreach (var registro in cAula)
        {
            lblMensaje.Text = string.Empty;
            ddlSucursal.SelectedValue = registro.SUCURSAL;


        }


        //txtHoraInicio.Focus();
    }



    #endregion

    #region GRILLAS

    #endregion

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarModalidad();
        listarCurso();
        listarMateria();
        listarHorario();
        ddlMateria_SelectedIndexChanged();
        activarObjetos();




    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarCurso();
        listarMateria();
        listarHorario();
        ddlMateria_SelectedIndexChanged();
        activarObjetos();


    }

    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarMateria();
        listarHorario();
        ddlMateria_SelectedIndexChanged();
        activarObjetos();
    }

    protected void ddlMateria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMateria_SelectedIndexChanged();
    }


    protected void ddlMateria_SelectedIndexChanged()
    {
        string curso = ddlModalidad.SelectedValue;
        string materia = ddlMateria.SelectedValue;
        switch (materia)
        {
            case "-1": //SIN MATERIA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = false;
                pnHorarioDetalle.Visible = false;
                pnFechasDetalle.Visible = false;
                break;
            case "3": //EDUCACION VIAL
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "4": // MECANICA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                if (curso == "2" || curso == "3" || curso == "4")
                {
                    pnFechasDetalle.Visible = true;
                }
                else
                {
                    pnFechasDetalle.Visible = false;
                }
                break;
            case "5": // PSICOLOGIA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = true;
                break;
            case "6":// PRIMEROS AUXILIOS
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = true;
                break;
            case "7": // PRACTICA
                pnAutoDetalle.Visible = true;
                pnAulaDetalle.Visible = false;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;


            default:
                break;

        }

        listarAula();
        listarHorario();
        listarAuto();
        listarTaller();
        
    }

    #region PROCESOS DE DETALLE DE AULA
    protected void grvAulaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusaula")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAulaDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);
            bool lestado = Convert.ToBoolean(row.Cells[0].Text);

            grvAulaDetalle_RowDataBound(indice);

           
        }

    }
    protected void grvAulaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetalle_RowDataBound(0);
    }

    protected void grvAulaDetalle_RowDataBound(int indice)
    {
        var cHora = new object();
        
        bool estado;
        int aula=0;
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                Accion = "REGAULAS";
                grvAulaDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetalle.Rows[i].ForeColor = Color.White;
                aula = Convert.ToInt32(grvAulaDetalle.Rows[i].Cells[2].Text);
                cHora = ds.sp_HorariosAsignadosXMateria(Accion, sucursal, curso, materia, aula, 0);

                grvHorarioDetalle.DataSource = cHora;
                grvHorarioDetalle.DataBind();

            }
            else
            {
                grvAulaDetalle.Rows[i].BackColor = Color.White;
                grvAulaDetalle.Rows[i].ForeColor = Color.Black;

            }
        }
    }




    #endregion
    protected void grvAutoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusveh")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAutoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);


            bool lestado = Convert.ToBoolean(row.Cells[0].Text);


            grvAutoDetalle_RowDataBound(indice);

          
        }

    }
    protected void grvAutoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAutoDetalle_RowDataBound(0);
    }

    protected void grvAutoDetalle_RowDataBound(int indice)
    {
        
        var cHora = new object();

        bool estado;
        int auto = 0;
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        for (int i = 0; i < grvAutoDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAutoDetalle.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                Accion = "REGAUTOS";
                grvAutoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAutoDetalle.Rows[i].ForeColor = Color.White;
                auto = Convert.ToInt32(grvAutoDetalle.Rows[i].Cells[2].Text);

                cHora = ds.sp_HorariosAsignadosXAuto(Accion, sucursal, curso, materia, auto, 0);

                grvHorarioDetalle.DataSource = cHora;
                grvHorarioDetalle.DataBind();

            }
            else
            {
                grvAutoDetalle.Rows[i].BackColor = Color.White;
                grvAutoDetalle.Rows[i].ForeColor = Color.Black;

            }
        }

    }

    protected void grvHorarioDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jushorario")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvHorarioDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);

            bool lestado = Convert.ToBoolean(row.Cells[0].Text);

            

            grvHorarioDetalle_RowDataBound(indice);

            
        }
    }

    protected void grvHorarioDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalle_RowDataBound(0);
    }

    protected void grvHorarioDetalle_RowDataBound(int indice)
    {
        var cHora = new object();

        bool estado;
        int aula = 0;
        string Accion = "REGISTRADO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        for (int i = 0; i < grvHorarioDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalle.Rows[i].Cells[0].Text);

            if (i == indice)
            {
                grvHorarioDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvHorarioDetalle.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvHorarioDetalle.Rows[i].BackColor = Color.White;
                grvHorarioDetalle.Rows[i].ForeColor = Color.Black;

            }
        }
    }
    protected void grvTallerDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusfecha")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvTallerDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);

            bool lestado = Convert.ToBoolean(row.Cells[0].Text);

            

            grvTallerDetalle_RowDataBound(indice);



        }
    }
    protected void grvTallerDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        grvTallerDetalle_RowDataBound(0);


    }


    protected void grvTallerDetalle_RowDataBound(int indice)
    {

        bool estado;

        for (int i = 0; i < grvTallerDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvTallerDetalle.Rows[i].Cells[0].Text);

            //string  estado = Convert.ToString(grvAutoDetalle.Rows[i].Cells[1].Text);

            if (i == indice)
            {
                grvTallerDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvTallerDetalle.Rows[i].ForeColor = Color.White;
            }
            else
            {
                grvTallerDetalle.Rows[i].BackColor = Color.White;
                grvTallerDetalle.Rows[i].ForeColor = Color.Black;

            }
        }


    }

    



    
}