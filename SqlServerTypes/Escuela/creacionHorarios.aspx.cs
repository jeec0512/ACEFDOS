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

public partial class Escuela_asignacionHorarios : System.Web.UI.Page
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
        var cCurso = ds.sp_abmCurso("MODALIDAD", 0, modalidad, "", "", DateTime.Today, DateTime.Today, false, "", DateTime.Today);

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

    protected void listarAuto()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cAuto = ds.sp_abmAuto_escuela(Accion, 0, "", "", 0, "", "", "", "", 0, false, DateTime.Now, DateTime.Now,0,0,"",sucursal);

        grvAutoDetalle.DataSource = cAuto;
        grvAutoDetalle.DataBind();

    }

    protected void listarAula()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
       
        var cAula = ds.sp_abmAula(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal,0);

        grvAulaDetalle.DataSource = cAula;
        grvAulaDetalle.DataBind();

    }

    protected void listarHorario()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var cHorario = ds.sp_abmHorario(Accion, 0, 0, "", "", false, "", DateTime.Today, sucursal, modalidad, materia);

        grvHorarioDetalle.DataSource = cHorario;
        grvHorarioDetalle.DataBind();

    }


    protected void listarTaller()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);

        var ctaller = ds.sp_abmTaller(Accion, 0, 0, curso, materia, sucursal, DateTime.Today, "", DateTime.Today, false);

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
                            var cuentaE =ds.sp_cuentaHorariosEducVial(accion, sucursal, modalidad, curso, materia, aul_id, veh_id, hor_id, tal_id, usuario, fecha);
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


            if (materia == 5 )
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


            if ( materia == 6)
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


        /*RECUPERACION DE PUNTOS*/
        /*LEYES DE TRANSITO*/
            if (materia == 8)
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

            /*EDUCACION VIAL*/
            if (materia == 10)
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

            /*PRIMEROS AUXILIOS*/
            if (materia == 12)
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

            /*PSICOLOGIA*/
            if (materia == 13)
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
        verAsignaciones();
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
        string modalidad = ddlModalidad.SelectedValue;
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
                if (modalidad == "2" || modalidad == "3" || modalidad == "4")
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
            /*RECUPERACION DE PUNTOS*/
            case "8": //LEYES DE TRANSITO
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "10": //EDUCACION VIAL
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "12": //PRIMEROS AUXILIOS
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
                pnHorarioDetalle.Visible = true;
                pnFechasDetalle.Visible = false;
                break;
            case "13": //PSICOLOGIA
                pnAutoDetalle.Visible = false;
                pnAulaDetalle.Visible = true;
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
        verAsignaciones();
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

            if (lestado)
            {
                row.Cells[0].Text = Convert.ToString(false);

            }
            else
            {
                row.Cells[0].Text = Convert.ToString(true);

            }

            grvAulaDetalle_RowDataBound();

           /* TB_AULA TB_AULA = ds.TB_AULA.SingleOrDefault(x => x.AUL_ID == lid);
            lActivo = Convert.ToBoolean(TB_AULA.AUL_ACTIVO);
           
            if (lActivo)
            {
                lActivo = false;
                
            }
            else {
                lActivo = true;
                
            }

  
            TB_AULA = ds.TB_AULA.SingleOrDefault(x => x.AUL_ID == lid);
            TB_AULA.AUL_ACTIVO = lActivo;
            ds.SubmitChanges();

              listarAula();*/
        }

    }
    protected void grvAulaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAulaDetalle_RowDataBound();
    }

    protected void grvAulaDetalle_RowDataBound()
    {
        bool estado;
        for (int i = 0; i < grvAulaDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAulaDetalle.Rows[i].Cells[0].Text);

            //string  estado = Convert.ToString(grvAutoDetalle.Rows[i].Cells[1].Text);

            if (estado)
            {
                grvAulaDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAulaDetalle.Rows[i].ForeColor = Color.White;
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

            if (lestado)
            {
                row.Cells[0].Text = Convert.ToString(false);

            }
            else
            {
                row.Cells[0].Text = Convert.ToString(true);

            }

            grvAutoDetalle_RowDataBound();

            /*VEHICULO vehiculo = da.VEHICULO.SingleOrDefault(x => x.VEH_ID == lid);
            lActivo = Convert.ToBoolean(vehiculo.VEH_ESTADO);

            if (lActivo)
            {
                lActivo = false;

            }
            else
            {
                lActivo = true;

            }


            vehiculo = da.VEHICULO.SingleOrDefault(x => x.VEH_ID == lid);
            vehiculo.VEH_ESTADO = lActivo;
            da.SubmitChanges();

            listarAuto();*/
        }

    }
    protected void grvAutoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvAutoDetalle_RowDataBound();
    }

    protected void grvAutoDetalle_RowDataBound()
    {
        bool estado;

        for (int i = 0; i < grvAutoDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvAutoDetalle.Rows[i].Cells[0].Text);

            //string  estado = Convert.ToString(grvAutoDetalle.Rows[i].Cells[1].Text);

            if (estado)
            {
                grvAutoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvAutoDetalle.Rows[i].ForeColor = Color.White;
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

            if (lestado)
            {
                row.Cells[0].Text = Convert.ToString(false);

            }
            else
            {
                row.Cells[0].Text = Convert.ToString(true);

            }

            grvHorarioDetalle_RowDataBound();

            /*
            TB_HORARIO TB_HORARIO = ds.TB_HORARIO.SingleOrDefault(x => x.HOR_ID == lid);
            lActivo = Convert.ToBoolean(TB_HORARIO.HOR_ESTADO);

            if (lActivo)
            {
                lActivo = false;

            }
            else
            {
                lActivo = true;

            }


            TB_HORARIO = ds.TB_HORARIO.SingleOrDefault(x => x.HOR_ID == lid);
            TB_HORARIO.HOR_ESTADO = lActivo;
            ds.SubmitChanges();

            listarHorario();*/
        }
    }

    protected void grvHorarioDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        grvHorarioDetalle_RowDataBound();
    }

    protected void grvHorarioDetalle_RowDataBound()
    {
        bool estado;
        for (int i = 0; i < grvHorarioDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvHorarioDetalle.Rows[i].Cells[0].Text);

            //string  estado = Convert.ToString(grvAutoDetalle.Rows[i].Cells[1].Text);

            if (estado)
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

            if (lestado)
            {
                row.Cells[0].Text = Convert.ToString(false);

            }
            else
            {
                row.Cells[0].Text = Convert.ToString(true);

            }

            grvTallerDetalle_RowDataBound();



            /*
            TB_TALLER TB_TALLER = ds.TB_TALLER.SingleOrDefault(x => x.TAL_ID == lid);
            lActivo = Convert.ToBoolean(TB_TALLER.TAL_ESTADO);

            if (lActivo)
            {
                lActivo = false;

            }
            else
            {
                lActivo = true;

            }


            TB_TALLER = ds.TB_TALLER.SingleOrDefault(x => x.TAL_ID == lid);
            TB_TALLER.TAL_ESTADO = lActivo;
            ds.SubmitChanges();

            listarTaller();*/
        }
    }
    protected void grvTallerDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        grvTallerDetalle_RowDataBound();


    }


    protected void grvTallerDetalle_RowDataBound()
    {

        bool estado;

        for (int i = 0; i < grvTallerDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvTallerDetalle.Rows[i].Cells[0].Text);

            //string  estado = Convert.ToString(grvAutoDetalle.Rows[i].Cells[1].Text);

            if (estado)
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

    protected void verAsignaciones()
    {
        
        string accion = "AULAS";
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int materia = Convert.ToInt32(ddlMateria.SelectedValue);
        var cAsignaciones = ds.sp_VerHorarios2(accion, sucursal, 0, curso, materia, 0, 0, 0, 0, "", DateTime.Now);

        grvAsignaciones.DataSource = cAsignaciones;
        grvAsignaciones.DataBind();


        int columnas = grvAsignaciones.Columns.Count;

        if (materia==3) {
            grvAsignaciones.Columns[1].ItemStyle.CssClass = "Display";
            grvAsignaciones.Columns[1].HeaderStyle.CssClass = "Display";
            grvAsignaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
            grvAsignaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
            grvAsignaciones.Columns[7].ItemStyle.CssClass = "DisplayNone";
            grvAsignaciones.Columns[7].HeaderStyle.CssClass = "DisplayNone";

        }


        if (materia == 7)
        {
            grvAsignaciones.Columns[1].ItemStyle.CssClass = "DisplayNone";
            grvAsignaciones.Columns[1].HeaderStyle.CssClass = "DisplayNone";
            grvAsignaciones.Columns[2].ItemStyle.CssClass = "Display";
            grvAsignaciones.Columns[2].HeaderStyle.CssClass = "Display";
            grvAsignaciones.Columns[7].ItemStyle.CssClass = "DisplayNone";
            grvAsignaciones.Columns[7].HeaderStyle.CssClass = "DisplayNone";

        }


        if (modalidad == 1) 
        {
            if (materia == 4)
            {
                grvAsignaciones.Columns[1].ItemStyle.CssClass = "Display";
                grvAsignaciones.Columns[1].HeaderStyle.CssClass = "Display";
                grvAsignaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[7].ItemStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[7].HeaderStyle.CssClass = "DisplayNone";

            }else if (materia == 5 || materia == 6)
            {
                grvAsignaciones.Columns[1].ItemStyle.CssClass = "Display";
                grvAsignaciones.Columns[1].HeaderStyle.CssClass = "Display";
                grvAsignaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[7].ItemStyle.CssClass = "Display";
                grvAsignaciones.Columns[7].HeaderStyle.CssClass = "Display";
            }
        }
        else if (materia == 4 || materia == 5 || materia == 6)
            {
                grvAsignaciones.Columns[1].ItemStyle.CssClass = "Display";
                grvAsignaciones.Columns[1].HeaderStyle.CssClass = "Display";
                grvAsignaciones.Columns[2].ItemStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[2].HeaderStyle.CssClass = "DisplayNone";
                grvAsignaciones.Columns[7].ItemStyle.CssClass = "Display";
                grvAsignaciones.Columns[7].HeaderStyle.CssClass = "Display";
            }

        }




    protected void grvAsignaciones_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMensaje.Text = string.Empty;

        if (e.CommandName == "EliminaReg")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvAsignaciones.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[0].Text);



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
                lblMensaje.Text = "Se encuentra alumnos registrados en este horario.."+Convert.ToString(exp).Substring(0,200);
                // Provide for exceptions.
            }

            verAsignaciones();
        }
    }
    protected void grvAsignaciones_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}


 


        

