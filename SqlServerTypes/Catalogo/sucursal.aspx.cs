using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_sucursal : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    #region CARGA INICIAL
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            verSuc();
            activaObjetos();
            pnActualizacion.Visible = false;
            pnVer.Visible = true;
        }
    }

    #endregion

    #region LISTAR SUCURSALES
    protected void verSuc()
    {
        string lruc = "1790349578001";


        

        try
        {
            string grupo = (string)Session["SGrupo"];
            string sucursal = (string)Session["SSucursal"];

            var cSucursal = new object();

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

            if (tipo == 4)
            {
                cSucursal = from Truc in dc.tbl_ruc
                                where Truc.ruc == lruc
                                orderby Truc.sucursal
                                select new { Truc.sucursal, Truc.nom_suc, Truc.estab, Truc.ptoemi, Truc.dirEstablecimiento };
    //            grvVerSuc.DataSource = cSucursal;
     //           grvVerSuc.DataBind();
            }
            else 
            {
                cSucursal = from Truc in dc.tbl_ruc
                                where Truc.ruc == lruc
                                && Truc.sucursal == sucursal
                                orderby Truc.sucursal
                                select new { Truc.sucursal, Truc.nom_suc, Truc.estab, Truc.ptoemi, Truc.dirEstablecimiento };
//                grvVerSuc.DataSource = cSucursal;
  //              grvVerSuc.DataBind();
            }



             grvVerSuc.DataSource = cSucursal;
             grvVerSuc.DataBind();
            
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            
            lblmensaje.Text = e.Message;
            
        }



    }
    #endregion

    #region VER HIJOS
    protected void verHijos(string lsuc)
    {
        //  string lruc = "1790349578001";





        var cAulas = from Taula in dc.tbl_aula
                     where Taula.mae_suc == lsuc
                     orderby Taula.cod_aula
                     select new { Taula.cod_aula, Taula.capacidad, Taula.activo };

        grvVaulas.DataSource = cAulas;
        grvVaulas.DataBind();

        var cAutos = from Tauto in dc.tbl_auto
                     where Tauto.mae_suc == lsuc
                     orderby Tauto.marca, Tauto.modelo, Tauto.numeroAuto
                     select new { Tauto.id_auto, Tauto.numeroAuto, Tauto.marca, Tauto.modelo, Tauto.placa, Tauto.activo };

        grvVautos.DataSource = cAutos;
        grvVautos.DataBind();


        var cPsico = from Tpsico in dc.tbl_psicotecnico
                     where Tpsico.mae_suc == lsuc
                     orderby Tpsico.serie
                     select new { Tpsico.serie, Tpsico.activo };

        grvVpsicotecnicos.DataSource = cPsico;
        grvVpsicotecnicos.DataBind();


    }


    protected void grvVerSuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lsucursal = Convert.ToString(grvVerSuc.SelectedValue);

        verHijos(lsucursal);
        pnHijos.Visible = true;
        btnMsuc.Visible = true;

    }

    protected void activaObjetos()
    {
        pnActualizacion.Visible = false;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = false;
        pnHijos.Visible = false;

        btnMsuc.Visible = false;
    }
    #endregion

    #region PAGINACION


    public class classRuc
    {
        public string id_ruc { get; set; }
        public string ruc { get; set; }
        public string razonsocial { get; set; }
        public string nombreComercial { get; set; }
        public string coddoc { get; set; }
        public string sucursal { get; set; }
        public string nom_suc { get; set; }
        public string estab { get; set; }
        public string ptoemi { get; set; }
        public string dirEstablecimiento { get; set; }
        public string administrador { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string idEscuela { get; set; }
        public string activo { get; set; }

    }


    /***********************************************************************/


    private List<classRuc> retornaLista()
    {
        var classRuc = new classRuc();

        List<classRuc> lista = new List<classRuc>();

        var cRuc = from tRuc in dc.tbl_ruc
                   orderby tRuc.sucursal
                   select new
                   {
                       id_ruc = tRuc.id_ruc,
                       ruc = tRuc.ruc,
                       razonsocial = tRuc.razonsocial,
                       nombreComercial = tRuc.nombreComercial,
                       coddoc = tRuc.coddoc,
                       sucursal = tRuc.sucursal,
                       nom_suc = tRuc.nom_suc,
                       estab = tRuc.estab,
                       ptoemi = tRuc.ptoemi,
                       dirEstablecimiento = tRuc.dirEstablecimiento,
                       administrador = tRuc.administrador,
                       telefono = tRuc.telefono,
                       email = tRuc.email,
                       idEscuela = tRuc.idEscuela,
                       activo = tRuc.activo,
                   };



        foreach (var registro in cRuc)
        {

            classRuc.id_ruc = Convert.ToString(registro.id_ruc);
            classRuc.ruc = registro.ruc;
            classRuc.razonsocial = registro.razonsocial;
            classRuc.nombreComercial = registro.nombreComercial;
            classRuc.coddoc = registro.coddoc;
            classRuc.sucursal = registro.sucursal;
            classRuc.nom_suc = registro.nom_suc;
            classRuc.estab = registro.estab;
            classRuc.ptoemi = registro.ptoemi;
            classRuc.dirEstablecimiento = registro.dirEstablecimiento;
            classRuc.administrador = registro.administrador;
            classRuc.telefono = registro.telefono;
            classRuc.email = registro.email;
            classRuc.idEscuela = registro.idEscuela;
            classRuc.activo = Convert.ToString(registro.activo);

            lista.Add(new classRuc()
            {

                id_ruc = classRuc.id_ruc,
                ruc = classRuc.ruc,
                razonsocial = classRuc.razonsocial,
                nombreComercial = classRuc.nombreComercial,
                coddoc = classRuc.coddoc,
                sucursal = classRuc.sucursal,
                nom_suc = classRuc.nom_suc,
                estab = classRuc.estab,
                ptoemi = classRuc.ptoemi,
                dirEstablecimiento = classRuc.dirEstablecimiento,
                administrador = classRuc.administrador,
                telefono = classRuc.telefono,
                email = classRuc.email,
                idEscuela = classRuc.idEscuela,
                activo = classRuc.activo
            });

        }
        return lista;
    }





    private List<classRuc> retornaTodos()
    {
        var classRuc = new classRuc();

        List<classRuc> lista = new List<classRuc>();

        var cRuc = from tRuc in dc.tbl_ruc
                   select new
                   {
                       id_ruc = tRuc.id_ruc,
                       ruc = tRuc.ruc,
                       razonsocial = tRuc.razonsocial,
                       nombreComercial = tRuc.nombreComercial,
                       coddoc = tRuc.coddoc,
                       sucursal = tRuc.sucursal,
                       nom_suc = tRuc.nom_suc,
                       estab = tRuc.estab,
                       ptoemi = tRuc.ptoemi,
                       dirEstablecimiento = tRuc.dirEstablecimiento,
                       administrador = tRuc.administrador,
                       telefono = tRuc.telefono,
                       email = tRuc.email,
                       idEscuela = tRuc.idEscuela,
                       activo = tRuc.activo,
                   };



        foreach (var registro in cRuc)
        {

            classRuc.id_ruc = Convert.ToString(registro.id_ruc);
            classRuc.ruc = registro.ruc;
            classRuc.razonsocial = registro.razonsocial;
            classRuc.nombreComercial = registro.nombreComercial;
            classRuc.coddoc = registro.coddoc;
            classRuc.sucursal = registro.sucursal;
            classRuc.nom_suc = registro.nom_suc;
            classRuc.estab = registro.estab;
            classRuc.ptoemi = registro.ptoemi;
            classRuc.dirEstablecimiento = registro.dirEstablecimiento;
            classRuc.administrador = registro.administrador;
            classRuc.telefono = registro.telefono;
            classRuc.email = registro.email;
            classRuc.idEscuela = registro.idEscuela;
            classRuc.activo = Convert.ToString(registro.activo);

            lista.Add(new classRuc()
            {

                id_ruc = classRuc.id_ruc,
                ruc = classRuc.ruc,
                razonsocial = classRuc.razonsocial,
                nombreComercial = classRuc.nombreComercial,
                coddoc = classRuc.coddoc,
                sucursal = classRuc.sucursal,
                nom_suc = classRuc.nom_suc,
                estab = classRuc.estab,
                ptoemi = classRuc.ptoemi,
                dirEstablecimiento = classRuc.dirEstablecimiento,
                administrador = classRuc.administrador,
                telefono = classRuc.telefono,
                email = classRuc.email,
                idEscuela = classRuc.idEscuela,
                activo = classRuc.activo
            });

        }
        return lista;
    }


    private void cargaGrid()
    {
        grvVerSuc.DataSource = retornaLista();
        grvVerSuc.DataBind();

    }

    private void cargaGridTodos()
    {
        grvVerSuc.DataSource = retornaTodos();
        grvVerSuc.DataBind();

    }



    protected void grvVerSuc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvVerSuc.PageIndex = e.NewPageIndex;
        // if (tipoConsulta == 1)
        // {
        cargaGrid();
        //}

        //if (tipoConsulta == 2)
        //{
        //  cargaGridTodos();
        //}
    }

    #endregion

    #region MANTENIMIENTO SUCURSAL
    protected void btnSuc_Click(object sender, EventArgs e)
    {
        pnVer.Visible = false;
        pnHijos.Visible = false;
        pnActualizacion.Visible = true;
        pnSucursal.Visible = true;
        txtCodSuc.Enabled = true;
        txtCodSuc.Focus();
    }
    protected void btnRSuc_Click(object sender, EventArgs e)
    {
        blanquearSucursal();
        btnRSuc_Click();
    }

    protected void btnRSuc_Click()
    {
        pnVer.Visible = true;
        pnHijos.Visible = false;
        pnActualizacion.Visible = false;
        pnSucursal.Visible = false;

        lblmensaje.Text = string.Empty;

    }

    protected void btnMsuc_Click(object sender, EventArgs e)
    {
        string lsuc = Convert.ToString(grvVerSuc.SelectedValue);
        txtCodSuc.Text = lsuc;
        var cRuc = from tRuc in dc.tbl_ruc
                   where tRuc.sucursal == lsuc
                   select new
                   {
                       id_ruc = tRuc.id_ruc,
                       ruc = tRuc.ruc,
                       razonsocial = tRuc.razonsocial,
                       nombreComercial = tRuc.nombreComercial,
                       coddoc = tRuc.coddoc,
                       sucursal = tRuc.sucursal,
                       nom_suc = tRuc.nom_suc,
                       estab = tRuc.estab,
                       ptoemi = tRuc.ptoemi,
                       dirEstablecimiento = tRuc.dirEstablecimiento,
                       administrador = tRuc.administrador,
                       telefono = tRuc.telefono,
                       email = tRuc.email,
                       idEscuela = tRuc.idEscuela,
                       activo = tRuc.activo,
                       ciudad = tRuc.ciudad,
                       convenio = tRuc.convenio,
                   };

        foreach (var registro in cRuc)
        {

            txtCodSuc.Text = registro.sucursal;
            txtNombre.Text = registro.nom_suc;
            txtEstablecimiento.Text = registro.estab;
            txtPtoVta.Text = registro.ptoemi;
            txtDir.Text = registro.dirEstablecimiento;
            txtAdmin.Text = registro.administrador;
            txtTel.Text = registro.telefono;
            txtEmail.Text = registro.email;
            ddliD.SelectedValue = registro.idEscuela.Trim();
            
            chkEstadoS.Checked = Convert.ToBoolean(registro.activo);
            txtCiudad.Text = registro.ciudad;
            txtConvenio.Text = registro.convenio;
        }

        pnVer.Visible = false;
        pnHijos.Visible = false;
        pnActualizacion.Visible = true;
        pnSucursal.Visible = true;
        txtCodSuc.Enabled = false;

        txtNombre.Focus();
    }
    protected void btnGsuc_Click(object sender, EventArgs e)
    {
        string accion, ruc, razonsocial, nombreComercial, coddoc, sucursal, nom_suc, estab, ptoemi, dirEstablecimiento, administrador, telefono, email, idEscuela;
        bool activo;

        /* CONSTANTES */
        accion = "GUARDAR";
        ruc = "1790349578001";
        razonsocial = "AUTOMOVIL CLUB DEL ECUADOR";
        nombreComercial = "ANETA";
        coddoc = "07";

        /*VARIABLES*/
        sucursal = txtCodSuc.Text.Trim();
        nom_suc = txtNombre.Text.Trim();
        estab = txtEstablecimiento.Text.Trim();
        ptoemi = txtPtoVta.Text.Trim();
        dirEstablecimiento = txtDir.Text.Trim();
        administrador = txtAdmin.Text.Trim();
        telefono = txtTel.Text.Trim();
        email = txtEmail.Text.Trim();
        idEscuela = ddliD.SelectedValue.Trim();
        activo = chkEstadoS.Checked;
        string ciudad = txtCiudad.Text.Trim();
        string convenio = llenarCeros(txtConvenio.Text.Trim(), '0', 3); ;

        /*VALIDAR INFORMACION*/

        if (sucursal.Length < 3
            || nom_suc.Length < 9
            || estab.Length < 3
            || ptoemi.Length < 3
            || dirEstablecimiento.Length <= 20
            || administrador.Length <= 10
            || telefono.Length < 9
            || email.Length <= 10)
            //|| idEscuela.Length <= 0)
        {
            lblmensaje.Text = "Ingrese toda la información, código de sucursal son 3 caracteres,nombre de la sucursal (ejemplo ANETA SUC1), establecimiento 3 caracteres (001), punto de venta 3 caracteres (001), la dirección debe constar provincia, ciudad, dirección completa (LOJA/CATAMAYO/CALLE PRINCIPAl N35Y TRANSVERSAl SECTOR LOS LIMONES, teléfono con código de provincia (07-2700150) nombres del administrador (Juan Pérez)";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            dc.sp_abmRuc2(accion, ruc, razonsocial, nombreComercial, coddoc, sucursal, nom_suc, estab, ptoemi, dirEstablecimiento, administrador, telefono, email, idEscuela, activo, ciudad, convenio);
            blanquearSucursal();
            verSuc();
            btnRSuc_Click();

        }
    }
    protected void blanquearSucursal()
    {
        lblmensaje.Text = string.Empty;
        txtCodSuc.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtEstablecimiento.Text = string.Empty;
        txtPtoVta.Text = string.Empty;
        txtDir.Text = string.Empty;
        txtAdmin.Text = string.Empty;
        txtTel.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddliD.SelectedValue = "SUCURSAL";
        chkEstadoS.Checked = false;
        txtCiudad.Text = string.Empty;

    }
    #endregion

    #region MANTENIMIENTO AULA
    protected void btnAula_Click(object sender, EventArgs e)
    {

        pnVer.Visible = true;
        grvVerSuc.Enabled = false;
        pnBsuc.Visible = false;
        pnHijos.Visible = false;

        pnActualizacion.Visible = true;
        pnSucursal.Visible = false;
        pnAula.Visible = true;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = false;
        blaquearAula();
        txtCodAula.Focus();

    }

    protected void btnGaula_Click(object sender, EventArgs e)
    {
        string accion, mae_suc, cod_aula;
        int id_aula, capacidad;
        bool activo;

        /* CONSTANTES */
        accion = "GUARDAR";
        id_aula = 0;

        /*VARIABLES*/
        mae_suc = Convert.ToString(grvVerSuc.SelectedValue);
        cod_aula = txtCodAula.Text.Trim();
        capacidad = Convert.ToInt16(txtCapAula.Text);
        activo = chkEstadoAul.Checked;


        /*VALIDAR INFORMACION*/

        if (mae_suc.Length != 3
            || cod_aula.Length != 2
            || capacidad <= 0)
        {
            lblmensaje.Text = "Ingrese toda la información , código de aula un caracter y un dígito (ejemplo A1) y la capacidad mayor que cero ";
        }
        else
        {
            dc.sp_abmAula(accion, id_aula, mae_suc, cod_aula, capacidad, activo);
            verSuc();
            verHijos(mae_suc);
            btnRauto_Click();
        }
    }
    protected void btnRaula_Click(object sender, EventArgs e)
    {
        blaquearAula();
        btnRaula_Click();
    }

    protected void btnRaula_Click()
    {
        pnVer.Visible = true;
        grvVerSuc.Enabled = true;
        pnBsuc.Visible = true;
        pnHijos.Visible = true;

        pnActualizacion.Visible = false;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = false;

        txtCodAula.Enabled = true;
    }

    protected void blaquearAula()
    {
        lblmensaje.Text = string.Empty;
        txtCodAula.Text = string.Empty;
        txtCapAula.Text = "0";
        chkEstadoAul.Checked = false;

    }
    protected void grvVaulas_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lsuc = Convert.ToString(grvVerSuc.SelectedValue);
        string laula = Convert.ToString(grvVaulas.SelectedValue);

        var cAula = from tAula in dc.tbl_aula
                    where tAula.mae_suc == lsuc
                        && tAula.cod_aula == laula
                    select new
                    {
                        id_aula = tAula.id_aula,
                        capacidad = tAula.capacidad,
                        activo = tAula.activo,
                    };

        foreach (var registro in cAula)
        {
            txtCapAula.Text = Convert.ToString(registro.capacidad);
            chkEstadoAul.Checked = Convert.ToBoolean(registro.activo);
        }

        txtCodAula.Enabled = false;
        txtCodAula.Text = laula;

        pnVer.Visible = true;
        grvVerSuc.Enabled = false;
        pnBsuc.Visible = false;
        pnHijos.Visible = false;

        pnActualizacion.Visible = true;
        pnSucursal.Visible = false;
        pnAula.Visible = true;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = false;

        txtCapAula.Focus();
    }
    #endregion

    #region MANTENIMIENTO AUTO
    protected void btnAuto_Click(object sender, EventArgs e)
    {
        pnVer.Visible = true;
        grvVerSuc.Enabled = false;
        pnBsuc.Visible = false;
        pnHijos.Visible = false;

        pnActualizacion.Visible = true;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = true;
        pnPsicotecnico.Visible = false;
        blaquearAuto();
        txtNumeroAuto.Focus();
    }

    protected void btnGauto_Click(object sender, EventArgs e)
    {
        string accion, mae_suc, numeroAuto, marca, modelo, placa;
        int id_auto;
        bool activo;

        /* CONSTANTES */
        accion = "GUARDAR";
        id_auto = 0;

        /*VARIABLES*/
        mae_suc = Convert.ToString(grvVerSuc.SelectedValue);
        numeroAuto = txtNumeroAuto.Text.Trim();
        marca = ddlMarca.SelectedValue;
        modelo = ddlModelo.SelectedValue;
        placa = txtPlaca.Text.Trim();
        activo = chkEstadoAuto.Checked;


        /*VALIDAR INFORMACION*/

        if (mae_suc.Length != 3
            || numeroAuto.Length <= 0
            || numeroAuto == "0")
        {
            lblmensaje.Text = "Ingrese toda la información , número de auto válido mayor que cero, marca , modelo, etc";
        }
        else
        {
            dc.sp_abmAuto(accion, id_auto, mae_suc, numeroAuto, marca, modelo, placa, activo);
            verSuc();
            verHijos(mae_suc);
            btnRauto_Click();
        }
    }

    protected void btnRauto_Click(object sender, EventArgs e)
    {
        blaquearAuto();
        btnRauto_Click();
    }
    protected void btnRauto_Click()
    {
        txtNumeroAuto.Enabled = true;
        ddlMarca.Enabled = true;
        ddlModelo.Enabled = true;

        pnVer.Visible = true;
        grvVerSuc.Enabled = true;
        pnBsuc.Visible = true;
        pnHijos.Visible = true;

        pnActualizacion.Visible = false;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = false;
    }
    protected void blaquearAuto()
    {
        lblmensaje.Text = string.Empty;
        txtNumeroAuto.Text = "0";
        ddlMarca.SelectedValue = "CHEVROLET";
        ddlModelo.Text = "AVEO";
        txtPlaca.Text = string.Empty;
        chkEstadoAuto.Checked = false;

    }

    protected void grvVautos_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lsuc = Convert.ToString(grvVerSuc.SelectedValue);
        int lauto = Convert.ToInt16(grvVautos.SelectedValue);

        var cAuto = from tAuto in dc.tbl_auto
                    where tAuto.id_auto == lauto
                    select new
                    {
                        id_auto = tAuto.id_auto,
                        mae_suc = tAuto.mae_suc,
                        numeroAuto = tAuto.numeroAuto,
                        marca = tAuto.marca,
                        modelo = tAuto.modelo,
                        placa = tAuto.placa,
                        activo = tAuto.activo,

                    };

        foreach (var registro in cAuto)
        {
            txtNumeroAuto.Text = registro.numeroAuto;
            ddlMarca.SelectedValue = registro.marca;
            ddlModelo.SelectedValue = registro.modelo;
            txtPlaca.Text = registro.placa;
            chkEstadoAuto.Checked = Convert.ToBoolean(registro.activo);
        }

        txtNumeroAuto.Enabled = false;
        ddlMarca.Enabled = false;
        ddlModelo.Enabled = false;


        pnVer.Visible = true;
        grvVerSuc.Enabled = false;
        pnBsuc.Visible = false;
        pnHijos.Visible = false;

        pnActualizacion.Visible = true;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = true;
        pnPsicotecnico.Visible = false;

        txtPlaca.Focus();
    }

    #endregion

    #region MANTENIMIENTO PSICOTECNICO
    protected void btnPsico_Click(object sender, EventArgs e)
    {
        pnVer.Visible = true;
        grvVerSuc.Enabled = false;
        pnBsuc.Visible = false;
        pnHijos.Visible = false;

        pnActualizacion.Visible = true;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = true;

        blanquearPsicotecnico();
        txtSerie.Focus();
    }

    protected void btnApsico_Click(object sender, EventArgs e)
    {
        string accion, mae_suc, serie;
        int id_auto;
        bool activo;

        /* CONSTANTES */
        accion = "GUARDAR";
        id_auto = 0;

        /*VARIABLES*/
        mae_suc = Convert.ToString(grvVerSuc.SelectedValue);
        serie = txtSerie.Text.Trim();
        activo = chkEstadoPsico.Checked;

        if (mae_suc.Length != 3
            || serie.Length != 5)
        {
            lblmensaje.Text = "Ingrese correctamente la serie del petrinovic son 5 caracteres";
        }
        else
        {
            dc.sp_abmPsicotecnico(accion, id_auto, mae_suc, serie, activo);
            verSuc();
            verHijos(mae_suc);
            btnRpsico_Click();
        }
    }
    protected void btnRpsico_Click(object sender, EventArgs e)
    {
        blanquearPsicotecnico();
        btnRpsico_Click();
    }

    protected void btnRpsico_Click()
    {
        txtSerie.Enabled = true;
        pnVer.Visible = true;
        grvVerSuc.Enabled = true;
        pnBsuc.Visible = true;
        pnHijos.Visible = true;

        pnActualizacion.Visible = false;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = false;
    }

    protected void blanquearPsicotecnico()
    {
        lblmensaje.Text = string.Empty;
        txtSerie.Text = string.Empty;
        chkEstadoPsico.Checked = false;
    }


    protected void grvVpsicotecnicos_SelectedIndexChanged(object sender, EventArgs e)
    {
        string lsuc = Convert.ToString(grvVerSuc.SelectedValue);
        string lserie = Convert.ToString(grvVpsicotecnicos.SelectedValue).Trim();

        var cPsico = from tPsico in dc.tbl_psicotecnico
                     where tPsico.mae_suc == lsuc
                        && tPsico.serie == lserie
                     select new
                     {
                         serie = tPsico.serie,
                         activo = tPsico.activo,
                     };

        foreach (var registro in cPsico)
        {
            chkEstadoPsico.Checked = Convert.ToBoolean(registro.activo);
        }

        txtSerie.Enabled = false;
        txtSerie.Text = lserie;

        pnVer.Visible = true;
        grvVerSuc.Enabled = false;
        pnBsuc.Visible = false;
        pnHijos.Visible = false;

        pnActualizacion.Visible = true;
        pnSucursal.Visible = false;
        pnAula.Visible = false;
        pnAuto.Visible = false;
        pnPsicotecnico.Visible = true;

        chkEstadoPsico.Focus();
    }
    #endregion

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
}