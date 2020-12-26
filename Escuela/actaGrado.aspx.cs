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

public partial class Escuela_actaGrado : System.Web.UI.Page
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

        ddlProvincia.Items.Insert(0, listPro);
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
    protected void ffgrvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMensaje.Text = string.Empty;
        lblMensaje.Text = string.Empty;
        string cedula = string.Empty;
        string nombres = string.Empty;
        string factura = string.Empty;
        string titulo = string.Empty;
        string curso = string.Empty;
        decimal nedu = 0;
        decimal nedus1 = 0;
        decimal nedus2 = 0;
        decimal nprac = 0;
        decimal npracs1 = 0;
        decimal npracs2 = 0;
        int reg_id = 0;
        if (e.CommandName == "EliminaReg")
        {
            //bool lActivo = false;
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

        if (e.CommandName == "imprimeReg")
        {
            //bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            //int lid = Convert.ToInt32(row.Cells[1].Text);

            string regId = ((Label)row.FindControl("lblIdActivar")).Text;
            int lid = Convert.ToInt32(regId);

            var cEstudiante =
                from details in ds.TB_REGISTRO_NOTA_CON
                where details.RNOTC_ID == lid
                select details;

            foreach (var dEstudiante in cEstudiante)
            {
                cedula = dEstudiante.RNOTC_CIRUC.Trim();
                nombres = dEstudiante.RNOTC_APELLIDOS.Trim() + " " + dEstudiante.RNOTC_NOMBRES.Trim();
                factura = dEstudiante.RNOTC_FACT.Trim();
                titulo = Convert.ToString(dEstudiante.RNOTC_TITULO).Trim() + "L" ;//" " + dEstudiante.RNOTC_ACTA.Trim(); ;
                nedu = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_NOTA);
                nedus1 = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_SUP1);
                nedus2 = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_SUP2);
                nprac = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_NOTA);
                npracs1 = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_SUP1);
                npracs2 = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_SUP2);
                reg_id = Convert.ToInt32(dEstudiante.REG_ID);
            }

            var cCursos = from pAlumno in ds.TB_REGISTRO_ALUMNO
                          join pCurso in ds.TB_CURSO on pAlumno.CUR_ID equals pCurso.CUR_ID
                          where pAlumno.REG_ID == reg_id
                          select pCurso;

            foreach (var cCurso in cCursos)
            {
                curso = cCurso.CUR_NOMENCLATURA.Trim();
            }

            imprimirActa(cedula, nombres, factura, titulo, nedu, nedus1, nedus2, nprac, npracs1, npracs2, curso);
            verAsignaciones();
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
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        string suc = ddlSucursal.SelectedValue;
        string Ciudad = ddlCiudad.SelectedValue;
        string Provincia = ddlProvincia.SelectedValue;
        int Dias = Convert.ToDateTime(txtFecha.Text).Day;
        int Mes = Convert.ToDateTime(txtFecha.Text).Month;
        int Ano = Convert.ToDateTime(txtFecha.Text).Year;
        string DirectorEscuela1 = txtDirector.Text.Trim();
        string Escuela = traeEscuela(suc);
        string Supervisor = txtSupTeoria.Text.Trim();
        string SecretariaAcademica1 = txtSecAcad.Text.Trim();
        string Estudiante = "juanperez";
        decimal NotaTeoria = 18;
        string Equivalenteteoria = equivalenteNota(NotaTeoria);
        decimal NotaPractica = 18;
        string EquivalentePractica = equivalenteNota(NotaPractica);
        string Titulo = "27272 L";
        string Curso = "C023-19";
        string SupervisorTeoria = txtSupTeoria.Text.Trim();
        string SupervisorPractica = txtSupPractica.Text.Trim();
        string DirectorEscuela2 = txtDirector.Text.Trim();
        string SecretariaAcademica2 = txtSecAcad.Text.Trim();
        string factura = "042-002-000109527";
        string cedula = "0850475260";
        Session["psuc"] = suc;
        Session["pCiudad"] = Ciudad;
        Session["pProvincia"] = Provincia;
        Session["pDias"] = Dias;
        Session["pMes"] = Mes;
        Session["pAno"] = Ano;
        Session["pDirectorEscuela1"] = DirectorEscuela1;
        Session["pEscuela"] = Escuela;
        Session["pSupervisor"] = Supervisor;
        Session["pSecretariaAcademica1"] = SecretariaAcademica1;
        Session["pEstudiante"] = Estudiante;
        Session["pNotaTeoria"] = NotaTeoria;
        Session["pEquivalenteteoria"] = Equivalenteteoria;
        Session["pNotaPractica"] = NotaPractica;
        Session["pEquivalentePractica"] = EquivalentePractica;
        Session["pTitulo"] = Titulo;
        Session["pCurso"] = Curso;
        Session["pSupervisorTeoria"] = SupervisorTeoria;
        Session["pSupervisorPractica"] = SupervisorPractica;
        Session["pDirectorEscuela2"] = DirectorEscuela2;
        Session["pSecretariaAcademica2"] = SecretariaAcademica2;
        Session["pfactura"] = factura;
        Session["pcedula"] = cedula;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirActaGrado.aspx','','width=800,height=500') </script>");
        
     }


    protected string traeEscuela(string lsuc)
    {
        string lSucursal;

        lSucursal = "";

        var consultaSuc = from Suc in dc.tbl_ruc
                          where Suc.sucursal == lsuc
                          select new
                          {
                              nom_suc = Suc.nom_suc
                          };

        if (consultaSuc.Count() == 0)
        {
            lSucursal = "Sucursal sin descripción";
        }
        else
        {
            foreach (var registro in consultaSuc)
            {
                lSucursal = registro.nom_suc;
            }
        }

        return lSucursal;
    }

    protected string equivalenteNota (decimal nota)
    {
        string equivalente = string.Empty;
        if(nota>=16 && nota<17){
            equivalente = "BUENA";
        }else if(nota>=17 && nota <19){
            equivalente = "MUY BUENA";
        }else{
            equivalente = "SOBRESALIENTE";
        }
        return equivalente;
    }
    #region ACCIONES SOBRE ACTIVACIONES REALIZADAS
    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMensaje.Text = string.Empty;
        lblMensaje.Text = string.Empty;
        string cedula = string.Empty;
        string nombres = string.Empty;
        string factura = string.Empty;
        string titulo = string.Empty;
        string curso = string.Empty;
        decimal nedu = 0;
        decimal nedus1 = 0;
        decimal nedus2 = 0;
        decimal nprac = 0;
        decimal npracs1 = 0;
        decimal npracs2 = 0;
        int reg_id = 0;
        if (e.CommandName == "EliminaReg")
        {
            //bool lActivo = false;
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

        if (e.CommandName == "imprimeReg")
        {
            //bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            //int lid = Convert.ToInt32(row.Cells[1].Text);

            string regId = ((Label)row.FindControl("lblIdActivar")).Text;
            int lid = Convert.ToInt32(regId);

            var cEstudiante =
                from details in ds.TB_REGISTRO_NOTA_CON
                where details.RNOTC_ID == lid
                select details;

            foreach (var dEstudiante in cEstudiante)
            {
                cedula = dEstudiante.RNOTC_CIRUC.Trim();
                nombres = dEstudiante.RNOTC_APELLIDOS.Trim() + " " + dEstudiante.RNOTC_NOMBRES.Trim();
                factura = dEstudiante.RNOTC_FACT.Trim();
                titulo = Convert.ToString(dEstudiante.RNOTC_TITULO).Trim() + "L";// " " + dEstudiante.RNOTC_ACTA.Trim(); ;
                nedu = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_NOTA);
                nedus1 = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_SUP1);
                nedus2 = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_SUP2);
                nprac = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_NOTA);
                npracs1 = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_SUP1);
                npracs2 = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_SUP2);
                reg_id = Convert.ToInt32(dEstudiante.REG_ID);
            }

            var cCursos = from pAlumno in ds.TB_REGISTRO_ALUMNO
                          join pCurso in ds.TB_CURSO on pAlumno.CUR_ID equals pCurso.CUR_ID
                          where pAlumno.REG_ID == reg_id
                          select pCurso;

            foreach (var cCurso in cCursos)
            {
                curso = cCurso.CUR_NOMENCLATURA.Trim();
            }

            try
            {
                imprimirActa(cedula, nombres, factura, titulo, nedu, nedus1, nedus2, nprac, npracs1, npracs2, curso);
                verAsignaciones();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

    }
    #endregion

    protected void verAsignaciones()
    {

    }
    protected void btnImprimir_Click1(object sender, EventArgs e)
    {
        lblMensaje.Text = string.Empty;
        string cedula = string.Empty;
        string nombres = string.Empty;
        string factura = string.Empty;
        string titulo = string.Empty;
        string curso = string.Empty;
        decimal nedu = 0;
        decimal nedus1 = 0;
        decimal nedus2 = 0;
        decimal nprac = 0;
        decimal npracs1 = 0;
        decimal npracs2 = 0;
        int reg_id = 0;
        



        foreach (GridViewRow gridViewRow in grvCursoDetalle.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("cbActivarConfirmar")).Checked)
            {
                string regId = ((Label)gridViewRow.FindControl("lblIdActivar")).Text;
                int lid = Convert.ToInt32(regId);

                
                var cEstudiante =
                from details in ds.TB_REGISTRO_NOTA_CON
                where details.RNOTC_ID == lid
                select details;

                foreach (var dEstudiante in cEstudiante)
                {
                   cedula = dEstudiante.RNOTC_CIRUC.Trim();
                   nombres = dEstudiante.RNOTC_APELLIDOS.Trim()+" "+dEstudiante.RNOTC_NOMBRES.Trim();
                   factura = dEstudiante.RNOTC_FACT.Trim();
                   titulo =  Convert.ToString(dEstudiante.RNOTC_TITULO).Trim()+ "L";//" " + dEstudiante.RNOTC_ACTA.Trim();;
                   nedu = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_NOTA);
                   nedus1 = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_SUP1);
                   nedus2 = Convert.ToDecimal(dEstudiante.RNOTC_EDUC_VIAL_SUP2);
                   nprac = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_NOTA);
                   npracs1 = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_SUP1);
                   npracs2 = Convert.ToDecimal(dEstudiante.RNOTC_PRAC_SUP2);
                   reg_id = Convert.ToInt32(dEstudiante.REG_ID);
                }

                var cCursos = from pAlumno in ds.TB_REGISTRO_ALUMNO
                                join pCurso in ds.TB_CURSO on pAlumno.CUR_ID equals pCurso.CUR_ID
                             where pAlumno.REG_ID == reg_id
                             select pCurso;

                foreach (var cCurso in cCursos)
                {
                    curso = cCurso.CUR_NOMENCLATURA.Trim();
                }

                try
                {
                    imprimirActa(cedula, nombres, factura, titulo, nedu, nedus1, nedus2, nprac, npracs1, npracs2, curso);
                    verAsignaciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = ex.Message;
                }

            }
        }
    }
    protected decimal mayor (decimal uno, decimal dos, decimal tres){
        decimal mayor = 0;
        if (uno >= dos)
        {
            mayor = uno;
        }
        else {
            mayor = dos;
        }
        if (mayor >= tres)
        {
           
        }else {
            mayor = tres;
        }
        return mayor;
    }
    protected void imprimirActa(string cedula,string nombres,string factura,string titulo,decimal nedu,decimal nedus1,decimal nedus2,decimal nprac,decimal npracs1,decimal npracs2,string curso) 
    {
        //try
        //{
            string suc = ddlSucursal.SelectedValue;
            string Ciudad = ddlCiudad.SelectedValue;
            string Provincia = ddlProvincia.SelectedValue;
            string fecha = txtFecha.Text;
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;

            int Dias = Convert.ToDateTime(txtFecha.Text).Day;
            int Mes = Convert.ToDateTime(txtFecha.Text).Month;
            int Ano = Convert.ToDateTime(txtFecha.Text).Year;
            string cMes = formatoFecha.GetMonthName(Mes);
            string DirectorEscuela1 = txtDirector.Text.Trim();
            string Escuela = traeEscuela(suc);
            string Supervisor = txtSupTeoria.Text.Trim();
            string SecretariaAcademica1 = txtSecAcad.Text.Trim();
            string Estudiante = nombres;
            decimal NotaTeoria = mayor(nedu, nedus1, nedus2);
            string Equivalenteteoria = equivalenteNota(NotaTeoria);
            decimal NotaPractica = mayor(nprac, npracs1, npracs2);
            string EquivalentePractica = equivalenteNota(NotaPractica);
            string Titulo = titulo;
            string Curso = curso;
            string SupervisorTeoria = txtSupTeoria.Text.Trim();
            string SupervisorPractica = txtSupPractica.Text.Trim();
            string DirectorEscuela2 = txtDirector.Text.Trim();
            string SecretariaAcademica2 = txtSecAcad.Text.Trim();
            //string factura = factura;
            //string cedula = "0850475260";

            if (Provincia == "-1" || Ciudad == "-1")
            {
                throw new InvalidCastException("ingrese provincia y ciudad");
            }
            if (fecha.Length <= 0 || DirectorEscuela1.Length <= 0 || SupervisorTeoria.Length <= 0 || SupervisorPractica.Length <= 0 || SecretariaAcademica1.Length <= 0) 
            {
                throw new InvalidCastException("ingrese toda la información solicitada");
            }

        
            Session["psuc"] = suc;
            Session["pCiudad"] = Ciudad;
            Session["pProvincia"] = Provincia;
            Session["pDias"] = Dias;
            Session["pMes"] = cMes;
            Session["pAno"] = Ano;
            Session["pDirectorEscuela1"] = DirectorEscuela1;
            Session["pEscuela"] = Escuela;
            Session["pSupervisor"] = Supervisor;
            Session["pSecretariaAcademica1"] = SecretariaAcademica1;
            Session["pEstudiante"] = Estudiante;
            Session["pNotaTeoria"] = NotaTeoria;
            Session["pEquivalenteteoria"] = Equivalenteteoria;
            Session["pNotaPractica"] = NotaPractica;
            Session["pEquivalentePractica"] = EquivalentePractica;
            Session["pTitulo"] = Titulo;
            Session["pCurso"] = Curso;
            Session["pSupervisorTeoria"] = SupervisorTeoria;
            Session["pSupervisorPractica"] = SupervisorPractica;
            Session["pDirectorEscuela2"] = DirectorEscuela2;
            Session["pSecretariaAcademica2"] = SecretariaAcademica2;
            Session["pfactura"] = factura;
            Session["pcedula"] = cedula;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirActaGrado.aspx','','width=800,height=500') </script>");
        //}
        //catch (Exception exp)
        //{
          //  Console.WriteLine(exp);
           // lblMensaje.Text = "Algo pasó" + Convert.ToString(exp).Substring(0, 200);
            // Provide for exceptions.
       // }
    }
}