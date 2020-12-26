using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_planCuenta : System.Web.UI.Page
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
            activar();
        }
    }

    #endregion

    #region BUSCAR CUENTA
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    protected void ibConsultar_Click()
    {
        string lbuscar = txtCta.Text.Trim();
        if (lbuscar.Length >= 3)
        {
            lblMensaje.Text = "";
            verCta(lbuscar);
        }
        else
        {
            lblMensaje.Text = "Ingrese al menos 3 caracteres";
        }
    }
    #endregion

    #region MANTENIMIENTO DE REGISTROS
    protected void grvVerCta_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnModificar.Visible = true;
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        /*DESACTIVAR DESACTIVAR OBJETOS*/

        pnVCuentaCble.Visible = false;
        pnMCuentaCble.Visible = true;
        txtMAE_CUE.Enabled = true;

        llenarListados();

        blanquearObjetos();

        txtMAE_CUE.Focus();
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        string mae_cue;


        mae_cue = Convert.ToString(grvVerCta.SelectedValue).Trim();
        //id_mae_cue = 0;

        modificarCuenta(mae_cue);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        /*VARIABLES*/
        string accion, cadena, MAE_CUE, NOM_CTA;
        int id_mae_cue, TIP_CTA, NAT_CTA;

        bool estado;


        TIP_CTA = 0;
        NAT_CTA = 0;

        /*CONSTANTES*/
        accion = "GUARDAR";

        id_mae_cue = 0;
        MAE_CUE = txtMAE_CUE.Text.Trim();
        NOM_CTA = txtNOM_CTA.Text.Trim();
        TIP_CTA = Convert.ToInt16(ddlTIP_CTA.SelectedValue);
        NAT_CTA = Convert.ToInt16(ddlNAT_CTA.SelectedValue);
        estado = chkMEstado.Checked;



        /*VALIDAR DATOS*/

        cadena = validarDatos().Trim();

        if (cadena.Length <= 0)
        {
            var cProducto = dc.sp_abmCtaContable(accion, id_mae_cue, MAE_CUE, NOM_CTA, TIP_CTA, NAT_CTA, estado);
            btnRegresar_Click();
        }
        else
        {
            lblMensaje2.Text = cadena;
        }
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        /*BLANQUEAR OBJETOS */
        blanquearObjetos();

        /*CONSULTAR PRODUCTOS*/

        ibConsultar_Click();

        /*DESACTIVAR DESACTIVAR OBJETOS*/

        pnVCuentaCble.Visible = true;
        pnVCuentaCble.Enabled = true;
        pnBotonera.Visible = true;
        pnMCuentaCble.Visible = false;
        txtMAE_CUE.Enabled = true;
    }

    #endregion

    #region PROCESOS INTERNOS
    /*PROCESOS INTERNOS*/
    #region LISTAR CUENTAS CONTABLES

    protected void verCta(string lbusca)
    {
        grvVerCta.DataSource = dc.sp_abmCtaContable("LISTAR", 0, lbusca, lbusca, 0, 0, false);
        grvVerCta.DataBind();
    }
    #endregion

    #region ACTIVAR OBJETOS DE INICIO

    protected void activar()
    {
        lblMensaje.Text = "";
        lblMensaje2.Text = "";
        btnModificar.Visible = false;
        pnVer.Visible = true;

        pnVCuentaCble.Visible = true;
        pnVCuentaCble.Enabled = true;
        pnBotonera.Visible = true;
        txtMAE_CUE.Enabled = true;
    }

    #endregion

    #region LLENAR DDL O GRV
    protected void llenarListados()
    {
        /* TRAER CUENTAS CONTABLES*/

    }
    #endregion

    #region INICIALIZA OBJETOS
    protected void blanquearObjetos()
    {
        lblMensaje.Text = string.Empty;
        lblMensaje2.Text = string.Empty;


        txtMAE_CUE.Text = string.Empty;
        txtNOM_CTA.Text = string.Empty;


        ddlTIP_CTA.SelectedValue = "1";
        ddlNAT_CTA.SelectedValue = "1";
        chkMEstado.Checked = false;
    }
    #endregion

    #region AÑADE O MODIFICA REGISTRO
    protected void modificarCuenta(string mae_cue)
    {
        /*VARIABLES*/
        string accion, MAE_CUE, NOM_CTA;
        int id_mae_cue, TIP_CTA, NAT_CTA;

        bool estado;


        /*CONSTANTES*/
        accion = "TRAER";
        id_mae_cue = 0;

        MAE_CUE = mae_cue;
        NOM_CTA = string.Empty;
        estado = false;

        TIP_CTA = 0;
        NAT_CTA = 0;

        llenarListados();

        /* TRAER DATOS DEL PRODUCTO*/
        var cProducto = dc.sp_abmCtaContable(accion, id_mae_cue, MAE_CUE, NOM_CTA, TIP_CTA, NAT_CTA, estado);


        foreach (var registro in cProducto)
        {
            txtMAE_CUE.Text = registro.MAE_CUE;
            txtNOM_CTA.Text = registro.NOM_CTA;
            ddlTIP_CTA.SelectedValue = Convert.ToString(registro.TIP_CTA);
            ddlNAT_CTA.SelectedValue = Convert.ToString(registro.NAT_CTA);
            chkMEstado.Checked = Convert.ToBoolean(registro.estado);
        }



        /*ACTIVAR DESACTIVAR OBJETOS*/

        pnVCuentaCble.Enabled = false;
        pnBotonera.Visible = false;
        pnMCuentaCble.Visible = true;
        txtMAE_CUE.Enabled = false;
        txtNOM_CTA.Focus();
    }
    #endregion

    #region VALIDA INGRESO DE DATOS A LA BASE
    protected string validarDatos()
    {
        string cadena, MAE_CUE, NOM_CTA;
        int TIP_CTA, NAT_CTA;


        MAE_CUE = txtMAE_CUE.Text.Trim();
        NOM_CTA = txtNOM_CTA.Text.Trim();

        TIP_CTA = Convert.ToInt16(ddlTIP_CTA.SelectedValue);
        NAT_CTA = Convert.ToInt16(ddlNAT_CTA.SelectedValue);





        if (MAE_CUE.Length <= 0 || NOM_CTA.Length <= 3
            || TIP_CTA <= 0 || NAT_CTA <= 0)
        {
            cadena = "El código debe tener mínimo 1 caracter, la descripción mínimo 5 caracteres";
        }
        else
        {
            cadena = string.Empty;
        }


        return cadena;


    }
    #endregion
    #endregion

    #region PAGINACION

    public class classCta
    {
        public string id_mae_cue { get; set; }
        public string MAE_CUE { get; set; }
        public string NOM_CTA { get; set; }
        public string TIP_CTA { get; set; }
        public string NAT_CTA { get; set; }
        public string estado { get; set; }

    }

    private List<classCta> retornaLista()
    {
        string lbuscar = txtCta.Text.Trim();

        var classCta = new classCta();

        List<classCta> lista = new List<classCta>();

        var cCta = dc.sp_abmCtaContable("LISTAR", 0, lbuscar, lbuscar, 0, 0, false);

        foreach (var registro in cCta)
        {
            classCta.id_mae_cue = Convert.ToString(registro.id_mae_cue);
            classCta.MAE_CUE = registro.MAE_CUE;
            classCta.NOM_CTA = registro.NOM_CTA;
            classCta.TIP_CTA = Convert.ToString(registro.TIP_CTA);
            classCta.NAT_CTA = Convert.ToString(registro.NAT_CTA);
            classCta.estado = Convert.ToString(registro.estado);



            lista.Add(new classCta()
            {
                id_mae_cue = classCta.id_mae_cue,
                MAE_CUE = classCta.MAE_CUE,
                NOM_CTA = classCta.NOM_CTA,
                TIP_CTA = classCta.TIP_CTA,
                NAT_CTA = classCta.NAT_CTA,
                estado = classCta.estado
            });

        }

        return lista;
    }

    private void cargaGrid()
    {
        grvVerCta.DataSource = retornaLista();
        grvVerCta.DataBind();
    }
    protected void grvVerCta_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void grvVerCta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvVerCta.PageIndex = e.NewPageIndex;
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
}