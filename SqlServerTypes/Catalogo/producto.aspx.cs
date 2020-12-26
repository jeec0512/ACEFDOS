using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_producto : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["temporalraceConnectionString"].ConnectionString;


    #endregion
    #region  INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            activar();
            calificarOpciones();
        }
    }

    #endregion

    #region LISTAR PRODUCTOS

    protected void verPro(string lbusca)
    {
        grvVerPro.DataSource = dc.sp_abmProducto("LISTAR", 0, lbusca, lbusca, "IVA", 0, 0, 0, "", 0, 0, "MOD", "CTA", "COS","CDES",0);
        grvVerPro.DataBind();
    }
    #endregion

    #region PAGINACION

    public class classPro
    {
        public string PRO_ID { get; set; }
        public string PRO_CODIGO { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public string PRO_IVA { get; set; }
        public string PRO_PRECIO { get; set; }
        public string PRO_PRECIO2 { get; set; }
        public string PRO_PRECIO3 { get; set; }
        public string PRO_ESTADO { get; set; }
        public string PRO_COMBO { get; set; }
        public string PVE_ID { get; set; }
        public string PRO_MODIFICACION { get; set; }
        public string PRO_CUENTACONTABLE { get; set; }
        public string PRO_CCOSTO { get; set; }
        public string PRO_CCDESCUENTO { get; set; }
        public string PRO_GRUPO { get; set; }
    }

    private List<classPro> retornaLista()
    {
        string lbuscar = txtPro.Text.Trim();

        var classPro = new classPro();

        List<classPro> lista = new List<classPro>();

        var cPro = dc.sp_abmProducto("LISTAR", 0, lbuscar, lbuscar, "IVA", 0, 0, 0, "", 0, 0, "MOD", "CTA", "COS", "CDES", 0);



        foreach (var registro in cPro)
        {

            classPro.PRO_ID = Convert.ToString(registro.PRO_ID);
            classPro.PRO_CODIGO = registro.PRO_CODIGO;
            classPro.PRO_DESCRIPCION = registro.PRO_DESCRIPCION;
            classPro.PRO_IVA = registro.PRO_IVA;
            classPro.PRO_PRECIO = Convert.ToString(registro.PRO_PRECIO);
            classPro.PRO_PRECIO2 = Convert.ToString(registro.PRO_PRECIO2);
            classPro.PRO_PRECIO3 = Convert.ToString(registro.PRO_PRECIO3);
            classPro.PRO_ESTADO = Convert.ToString(registro.PRO_ESTADO);
            classPro.PRO_COMBO = Convert.ToString(registro.PRO_COMBO);
            classPro.PVE_ID = Convert.ToString(registro.PVE_ID);
            classPro.PRO_MODIFICACION = registro.PRO_MODIFICACION;
            classPro.PRO_CUENTACONTABLE = registro.PRO_CUENTACONTABLE;
            classPro.PRO_CCOSTO = registro.PRO_CCOSTO;
            classPro.PRO_CCDESCUENTO = registro.PRO_CCDESCUENTO;
            classPro.PRO_GRUPO = Convert.ToString(registro.pro_grupo);


            lista.Add(new classPro()
            {

                PRO_ID = classPro.PRO_ID,
                PRO_CODIGO = classPro.PRO_CODIGO,
                PRO_DESCRIPCION = classPro.PRO_DESCRIPCION,
                PRO_IVA = classPro.PRO_IVA,
                PRO_PRECIO = classPro.PRO_PRECIO,
                PRO_PRECIO2 = classPro.PRO_PRECIO2,
                PRO_PRECIO3 = classPro.PRO_PRECIO3,
                PRO_ESTADO = classPro.PRO_ESTADO,
                PRO_COMBO = classPro.PRO_COMBO,
                PVE_ID = classPro.PVE_ID,
                PRO_MODIFICACION = classPro.PRO_MODIFICACION,
                PRO_CUENTACONTABLE = classPro.PRO_CUENTACONTABLE,
                PRO_CCOSTO = classPro.PRO_CCOSTO,
                PRO_CCDESCUENTO = classPro.PRO_CCDESCUENTO,
                PRO_GRUPO = classPro.PRO_GRUPO

            });

        }
        return lista;
    }

    private void cargaGrid()
    {
        grvVerPro.DataSource = retornaLista();
        grvVerPro.DataBind();
    }

    protected void grvVerPro_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void grvVerPro_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvVerPro.PageIndex = e.NewPageIndex;
        // if (tipoConsulta == 1)
        // {
        cargaGrid();
        //}

        //if (tipoConsulta == 2)
        //{
        //  cargaGridTodos();
        //}
    }
    protected void grvVerPro_PageIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    #region BUSCAR PRODUCTO
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    protected void ibConsultar_Click()
    {
        string lbuscar = txtPro.Text.Trim();
        if (lbuscar.Length >= 3)
        {
            lblMensaje.Text = "";
            verPro(lbuscar);
        }
        else
        {
            lblMensaje.Text = "Ingrese al menos 3 caracteres";
        }
    }

    #endregion

    #region MODIFICAR PRODUCTO


    protected void grvVerPro_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btnMProducto.Visible = true;
    }

    protected void btnMProducto_Click(object sender, EventArgs e)
    {
        int pro_id;

        pro_id = Convert.ToInt16(grvVerPro.SelectedValue);

        modificarProducto(pro_id);
    }


    protected void modificarProducto(int pro_id)
    {
        /*VARIABLES*/
        string accion, PRO_CODIGO, PRO_DESCRIPCION, PRO_IVA, PRO_ESTADO, PRO_MODIFICACION, PRO_CUENTACONTABLE, PRO_CCOSTO, PRO_CCDESCUENTO;
        int PRO_ID,PRO_GRUPO;
        decimal PRO_PRECIO, PRO_PRECIO2, PRO_PRECIO3, PRO_COMBO, PVE_ID;

        /*CONSTANTES*/
        accion = "TRAER";
        PRO_ID = pro_id;

        PRO_CODIGO = string.Empty;
        PRO_DESCRIPCION = string.Empty;
        PRO_IVA = string.Empty;
        PRO_ESTADO = string.Empty;
        PRO_MODIFICACION = string.Empty;
        PRO_CUENTACONTABLE = string.Empty;
        PRO_CCOSTO = string.Empty;
        PRO_CCDESCUENTO = string.Empty;

        PRO_PRECIO = 0;
        PRO_PRECIO2 = 0;
        PRO_PRECIO3 = 0;
        PRO_COMBO = 0;
        PVE_ID = 0;
        PRO_GRUPO = 0;

        llenarListados();

        /* TRAER DATOS DEL PRODUCTO*/
        var cProducto = dc.sp_abmProducto(accion, PRO_ID, PRO_CODIGO, PRO_DESCRIPCION, PRO_IVA, PRO_PRECIO, PRO_PRECIO2, PRO_PRECIO3, PRO_ESTADO, PRO_COMBO, PVE_ID, PRO_MODIFICACION, PRO_CUENTACONTABLE, PRO_CCOSTO,PRO_CCDESCUENTO,PRO_GRUPO);


        foreach (var registro in cProducto)
        {
            txtPro_codigo.Text = registro.PRO_CODIGO;
            txtPro_descripcion.Text = registro.PRO_DESCRIPCION;
            ddlIva.SelectedValue = registro.PRO_IVA;

            txtPro_precio1.Text = Convert.ToString(registro.PRO_PRECIO);
            txtPro_precio2.Text = Convert.ToString(registro.PRO_PRECIO2);
            txtPro_precio3.Text = Convert.ToString(registro.PRO_PRECIO3);
            txtPro_combo.Text = Convert.ToString(registro.PRO_COMBO);
            txtPve_id.Text = Convert.ToString(registro.PVE_ID);
            ddlPro_modificacion.SelectedValue = registro.PRO_MODIFICACION;
            ddlCtaCble.SelectedValue = registro.PRO_CUENTACONTABLE.Trim();
            ddlCcosto.SelectedValue = registro.PRO_CCOSTO.Trim();
            ddlCtaDescuento.SelectedValue = registro.PRO_CCDESCUENTO.Trim();
            ddlGrupo.SelectedValue = Convert.ToString(registro.pro_grupo);

            if (registro.PRO_ESTADO == "A")
            {
                chkEstadoP.Checked = true;

            }
            else
            {
                chkEstadoP.Checked = false;
            }

        }



        /*ACTIVAR DESACTIVAR OBJETOS*/

        pnVproducto.Enabled = false;
        pnBotonera.Visible = false;
        pnMproducto.Visible = true;
        txtPro_codigo.Enabled = false;
        txtPro_descripcion.Focus();
    }
    #endregion

    #region ACTIVAR OBJETOS DE INICIO

    protected void activar()
    {
        lblMensaje.Text = "";
        lblMensaje2.Text = "";
        btnMProducto.Visible = false;
        pnVer.Visible = true;

        pnVproducto.Visible = true;
        pnVproducto.Enabled = true;
        pnBotonera.Visible = true;
        txtPro_codigo.Enabled = true;
    }

    #endregion

    #region MANTENIMIENTO DE PRODUCTO

    protected void btnNProducto_Click(object sender, EventArgs e)
    {
        /*DESACTIVAR DESACTIVAR OBJETOS*/

        pnVproducto.Visible = false;
        pnMproducto.Visible = true;
        txtPro_codigo.Enabled = true;

        llenarListados();

        blanquearObjetos();

        txtPro_codigo.Focus();

    }

    protected void btnGpro_Click(object sender, EventArgs e)
    {
        /*VARIABLES*/
        string cadena, accion, PRO_CODIGO, PRO_DESCRIPCION, PRO_IVA, PRO_ESTADO, PRO_MODIFICACION, PRO_CUENTACONTABLE, PRO_CCOSTO;
        int PRO_ID;
        decimal PRO_PRECIO, PRO_PRECIO2, PRO_PRECIO3, PRO_COMBO, PVE_ID;

        /*CONSTANTES*/
        accion = "GUARDAR";

        PRO_ID = Convert.ToInt16(grvVerPro.SelectedValue);
        PRO_CODIGO = txtPro_codigo.Text;
        PRO_DESCRIPCION = txtPro_descripcion.Text;
        PRO_IVA = ddlIva.SelectedValue;

        PRO_PRECIO = Convert.ToDecimal(txtPro_precio1.Text);
        PRO_PRECIO2 = Convert.ToDecimal(txtPro_precio2.Text);
        PRO_PRECIO3 = Convert.ToDecimal(txtPro_precio3.Text);
        PRO_COMBO = Convert.ToDecimal(txtPro_combo.Text);
        PVE_ID = Convert.ToDecimal(txtPve_id.Text);
        PRO_MODIFICACION = ddlPro_modificacion.SelectedValue;
        PRO_CUENTACONTABLE = ddlCtaCble.SelectedValue;
        PRO_CCOSTO = ddlCcosto.SelectedValue;
        string PRO_CCDESCUENTO = ddlCtaDescuento.SelectedValue;
        int pro_grupo = Convert.ToInt32(ddlGrupo.SelectedValue);

        if (chkEstadoP.Checked)
        {
            PRO_ESTADO = "A";
        }
        else
        {
            PRO_ESTADO = "N";
        }

        /*VALIDAR DATOS*/

        cadena = validarDatos().Trim();

        if (cadena.Length <= 0)
        {
            var cProducto = dc.sp_abmProducto(accion, PRO_ID, PRO_CODIGO, PRO_DESCRIPCION, PRO_IVA, PRO_PRECIO, PRO_PRECIO2, PRO_PRECIO3, PRO_ESTADO, PRO_COMBO, PVE_ID, PRO_MODIFICACION, PRO_CUENTACONTABLE, PRO_CCOSTO, PRO_CCDESCUENTO,pro_grupo);
            btnRpro_Click();
        }
        else
        {
            lblMensaje2.Text = cadena;
        }



    }
    protected void btnRpro_Click(object sender, EventArgs e)
    {
        /*DESACTIVAR DESACTIVAR OBJETOS*/

        btnRpro_Click();
    }

    protected void btnRpro_Click()
    {
        /*BLANQUEAR OBJETOS */
        blanquearObjetos();

        /*CONSULTAR PRODUCTOS*/

        ibConsultar_Click();

        /*DESACTIVAR DESACTIVAR OBJETOS*/

        pnVproducto.Visible = true;
        pnVproducto.Enabled = true;
        pnBotonera.Visible = true;
        pnMproducto.Visible = false;
        txtPro_codigo.Enabled = true;


    }

    protected void blanquearObjetos()
    {
        lblMensaje.Text = string.Empty;
        lblMensaje2.Text = string.Empty;


        txtPro_codigo.Text = string.Empty;
        txtPro_descripcion.Text = string.Empty;
        ddlIva.SelectedValue = "S";

        txtPro_precio1.Text = "0";
        txtPro_precio2.Text = "0";
        txtPro_precio3.Text = "0";
        txtPro_combo.Text = "0";
        txtPve_id.Text = "0";

        ddlPro_modificacion.SelectedValue = "N";
        ddlCtaCble.SelectedValue = "-1";
        ddlCcosto.SelectedValue = "-1";
        chkEstadoP.Checked = false;
    }

    protected void llenarListados()
    {
        /* TRAER CUENTAS CONTABLES*/
        var cCtaCble = from mcta in dc.tbl_mae_cue
                       where mcta.EST_CTA == 1
                             && mcta.TIP_CTA == 2
                       orderby mcta.MAE_CUE
                       select new
                       {
                           mae_cue = mcta.MAE_CUE.Trim()
                        ,
                           nom_cta = mcta.MAE_CUE.Trim() + " " + mcta.NOM_CTA.Trim()
                       };


        ddlCtaCble.DataSource = cCtaCble;
        ddlCtaCble.DataBind();

        ListItem listCble = new ListItem("Seleccione Cuenta Contable", "-1");
        ddlCtaCble.Items.Insert(0, listCble);

        /* TRAER CENTRO DE COSTO*/
        var cCcosto = from mccosto in dc.tbl_mae_cco
                      select new
                      {
                          mae_cco = mccosto.mae_cco.Trim()
                       ,
                          nom_cco = mccosto.mae_cco.Trim() + " " + mccosto.nom_cco.Trim()
                      };


        ddlCcosto.DataSource = cCcosto;
        ddlCcosto.DataBind();
        ListItem listCosto = new ListItem("Seleccione Centro de Costo", "-1");
        ddlCcosto.Items.Insert(0, listCosto);

        /* TRAER CUENTAS CONTABLES de descuento*/
        var cCtaDesc = from mcta in dc.tbl_mae_cue
                       where mcta.EST_CTA == 1
                             && mcta.TIP_CTA == 2
                       orderby mcta.MAE_CUE
                       select new
                       {
                           mae_cue = mcta.MAE_CUE.Trim()
                        ,
                           nom_cta = mcta.MAE_CUE.Trim() + " " + mcta.NOM_CTA.Trim()
                       };


        ddlCtaDescuento.DataSource = cCtaDesc;
        ddlCtaDescuento.DataBind();

        ListItem listDesc = new ListItem("Seleccione Cta Descuento", "-1");
        ddlCtaDescuento.Items.Insert(0, listDesc);


        var cGrupo = from mGrupo in df.tbl_GrupoProducto
                      select new
                      {
                          id = mGrupo.id_GrupoProducto,
                          descripcion = mGrupo.codigo
                      };

        ddlGrupo.DataSource = cGrupo;
        ddlGrupo.DataBind();

        ListItem listGrupo = new ListItem("Seleccione grupo", "-1");
        ddlGrupo.Items.Insert(0, listGrupo);






    }

    protected string validarDatos()
    {
        string cadena, PRO_CODIGO, PRO_DESCRIPCION, PRO_CUENTACONTABLE, PRO_CCOSTO;
        decimal PRO_PRECIO, PRO_COMBO, PVE_ID;



        PRO_CODIGO = txtPro_codigo.Text.Trim();
        PRO_DESCRIPCION = txtPro_descripcion.Text.Trim();

        PRO_PRECIO = Convert.ToDecimal(txtPro_precio1.Text);
        PRO_COMBO = Convert.ToDecimal(txtPro_combo.Text);
        PVE_ID = Convert.ToDecimal(txtPve_id.Text);

        PRO_CUENTACONTABLE = ddlCtaCble.SelectedValue.Trim();
        PRO_CCOSTO = ddlCcosto.SelectedValue.Trim();

        string CTADESCUENTO = ddlCtaDescuento.SelectedValue;
        string GRUPO = ddlGrupo.SelectedValue;


        //|| PRO_COMBO <= 0

        if (PRO_CODIGO.Length < 3 || PRO_DESCRIPCION.Length <= 10
            || PRO_PRECIO <= 0 || PVE_ID <= 0)
        {
            cadena = "El código debe tener mínimo 3 caracteres, la descripción mínimo 10 caracteres, el precio1, combo y el PVE deben ser mayores que cero";
        }
        else
        {
            if (PRO_CUENTACONTABLE == "-1" || PRO_CCOSTO == "-1" || CTADESCUENTO == "-1" || GRUPO == "-1")
            {
                cadena = "Debe seleccionar la cuenta contable y el centro de costo";
            }
            else
            {
                cadena = string.Empty;
            }

        }
        return cadena;
    }
    #endregion

    protected void calificarOpciones()
    {
        /*CALIFICAR OPCIONES*/
        string accion, grupo, menu, submenu, boton;

        submenu = string.Empty;
        boton = string.Empty;



        accion = string.Empty;
        grupo = (string)Session["Sgrupo"];

        menu = "CATALOGO";
        submenu = "PRODUCTO";
        boton = "NUEVOPROD";
        btnNProducto.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        menu = "CATALOGO";
        submenu = "PRODUCTO";
        boton = "MODPROD";
        btnMProducto.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);



    }
}