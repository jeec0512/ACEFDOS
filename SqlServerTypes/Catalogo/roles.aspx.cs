using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_roles : System.Web.UI.Page
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
            listarRoles(); 
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

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string Accion = "AGREGAR";

        
        string sucursal = ddlSucursal.SelectedValue;
        string codigo = txtCodigo.Text.Trim();
        string descripcion = txtDescripcion.Text.Trim();
        int nActivo = 0;

        string usuario = (string)Session["SUsername"];
        string fechaModificacion = Convert.ToString(DateTime.Now);

        bool pasa = validarDatos(sucursal, codigo, descripcion);

        if (!pasa)
        {

            lblMensaje.Text = "Ingrese toda la información solicitada";
        }
        else
        {
            /*GUARDAR INFORMACION*/
            dc.sp_abmRoles(Accion,0,codigo,descripcion);
            blanquearObjetos();
            lblMensaje.Text = descripcion.Trim() + " guardado correctamente";
        }

        listarRoles();
    }
    protected void blanquearObjetos()
    {

        lblMensaje.Text = string.Empty;
        txtCodigo.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
    }
    #endregion

    #region METODOS ESPECIFICOS
    protected bool validarDatos(string sucursal, string codigo, string descripcion)
    {
        bool pasa = true;

        if (sucursal.Length < 2
            || codigo.Length <= 2
            || descripcion.Length <= 2)
        {
            pasa = false;
        };

        return pasa;

    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ibConsultar_Click(int id)
    {
        string Accion = "CONSULTAR";

        string sucursal = ddlSucursal.SelectedValue;
        txtCodigo.Text = Convert.ToString(id);


        var cRol = dc.sp_abmRoles(Accion, id,"","");

        foreach (var registro in cRol)
        {
            lblMensaje.Text = string.Empty;
            txtCodigo.Text = registro.codigo;
            txtDescripcion.Text = registro.descripcion;

        }


        txtCodigo.Focus();
    }


    protected void listarRoles()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
       
        var cEgresos = dc.sp_abmRoles(Accion, 0, "", "");

        grvRolDetalle.DataSource = cEgresos;
        grvRolDetalle.DataBind();

    }
    #endregion

    #region GRILLAS
    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvRolDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;

            int lidCurso = Convert.ToInt32(row.Cells[1].Text);

            alterarTabla();

            ibConsultar_Click(lidCurso);

        }
    }


    protected void grvCursoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarRoles();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarRoles();
    }
    protected void alterarTabla() 
    {
        
        /*
        var ddl = "ALTER TABLE tbl_UsuarioMenuDinamico ADD col1  bit default 0 NOT NUL";
        using (var conn = new SqlConnection("bddComprobantesConnectionString"))
        using (var command = new SqlCommand(ddl, conn))
        {
            conn.Open();
            command.Connection = conn;
            command.ExecuteNonQuery();
        }
        
        */

       
    }
    protected void btnNuevoRol_Command(object sender, CommandEventArgs e)
    {
        lblMensaje.Enabled = true;
        string accion = "GUARDAR";
        string codigo = txtCodigo.Text.Trim();
        string descripcion = txtDescripcion.Text.Trim();
        if (codigo.Length <= 4 && descripcion.Length <= 4)
        {
            lblMensaje.Text = "Ingrese código y descripción para generar el nuevo rol";
        }
        else
        {
            dc.sp_abmRoles(accion, 0, codigo, descripcion);
        }

    }
}