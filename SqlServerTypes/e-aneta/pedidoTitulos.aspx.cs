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

public partial class Escuela_pedidoTitulos : System.Web.UI.Page
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


    protected void listarAuto()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cAuto = ds.sp_abmAuto(Accion, 0, "", "", 0, "", "", "", "", 0, 0, 0, sucursal, false);

        grvCursoDetalle.DataSource = cAuto;
        grvCursoDetalle.DataBind();

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;
        string usuario = (string)Session["SUsername"];
        DateTime fecha = DateTime.Now;
        string sucursal = ddlSucursal.SelectedValue;
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        int maximo = 0;
        int numReg = 0;
        int NUMREGISTROS = 0;
        // string mcurso 

        var cnumMax = ds.sp_abmPedidosMaximo("MAXIMO", 0, "", sucursal, 0, 0, "", "A", "", fecha, false, curso);
        foreach (var regNum in cnumMax.ToList())
        {
            maximo = Convert.ToInt32(regNum.MAXIMO);
        };

        maximo = maximo + 1;


        var cnumReg = ds.sp_PedidoTitulos_NumRegistros("CONSULTAR",sucursal,curso,"");
        foreach (var regNum in cnumReg.ToList())
        {
            numReg = Convert.ToInt32(regNum.numReg);
        };

        



        string accion = "AGREGAR";
        string NUMPEDIDO  = "P"+llenarCeros(Convert.ToString(maximo), '0', 7);
		int NUMERAL = maximo;
        if (numReg > 1)
        {
            NUMREGISTROS = numReg - 1;
        }
        else
        {
            NUMREGISTROS = numReg ;
        }
        string TIPOPEDIDO = "A";
		bool ASIGNADO = false;

        if (NUMREGISTROS > 0)
        {
            ds.sp_abmPedidos(accion, 0, NUMPEDIDO, sucursal, NUMERAL, NUMREGISTROS, "", TIPOPEDIDO, usuario, fecha, ASIGNADO, curso);
            ds.sp_PedidoTitulos("MODIFICAR", sucursal, curso, NUMPEDIDO);
            ddlCurso_SelectedIndexChanged();
            lblMensaje.Text = "Pedido #:"+NUMPEDIDO+" realizado con éxito";
        }
        else 
        {
            lblMensaje.Text = "NO EXISTEN ALUMNOS APROBADOS PARA GENERAR EL PEDIDO DE TÍTULOS";
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

    }



    #endregion

    #region GRILLAS
    

    protected void grvHorarioDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    #endregion

    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {

      
        listarCurso();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
      }
    protected void ddlSucursal_SelectedIndexChanged1(object sender, EventArgs e)
    {
        activarObjetos();
      }

    protected void ddlModalidad_SelectedIndexChanged1(object sender, EventArgs e)
    {
        listarCurso();
    }
    protected void ddlSucursal_SelectedIndexChanged2(object sender, EventArgs e)
    {
        listarCurso();
    }

    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirmar")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[1].Text);

            bool lactivo = Convert.ToBoolean(row.Cells[0].Text);
            
            
            



          
           /* TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
            lActivo = Convert.ToBoolean(TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION);
           */
            if (lActivo)
            {
                row.Cells[0].Text = Convert.ToString(false);
                lActivo = false;

            }
            else
            {
                row.Cells[0].Text = Convert.ToString(true);
                lActivo = true;
            }

            
            grvCursoDetalle_RowDataBound(indice, lactivo);

            /*
            TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
            TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = lActivo;
            ds.SubmitChanges();

            ddlCurso_SelectedIndexChanged();*/


        }

        
        

    }
    protected void grvCursoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*bool estado;

        for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
        {
            estado = Convert.ToBoolean(grvCursoDetalle.Rows[i].Cells[0].Text);

            //string  estado = Convert.ToString(grvCursoDetalle.Rows[i].Cells[1].Text);

            if (estado)
            {
                grvCursoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                grvCursoDetalle.Rows[i].ForeColor = Color.White;
            }
        }
         * 
        */

    }


    protected void grvCursoDetalle_RowDataBound(int indice, bool activo)
    {

    
            if (!activo)
            {
                grvCursoDetalle.Rows[indice].BackColor = Color.FromArgb(252, 128, 5);
                grvCursoDetalle.Rows[indice].ForeColor = Color.White;
            }
            else
            {
                grvCursoDetalle.Rows[indice].BackColor = Color.White;
                grvCursoDetalle.Rows[indice].ForeColor = Color.Black;

            }
 



    }



    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCurso_SelectedIndexChanged();
    }

    protected void ddlCurso_SelectedIndexChanged() 
    {
        lblMensaje.Text = "";
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        var cCurso = ds.sp_PedidoTitulos("CONSULTAR", sucursal, cur_id, "");



        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();

        int filas = Convert.ToInt32(grvCursoDetalle.Rows.Count);

        if (filas <= 0)
        {
            btnGuardar.Visible = false;
            lblMensaje.Text = "No existen alumnos aprobados";
        }
        else
        {
            btnGuardar.Visible = true;
            lblMensaje.Text = "";

            for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
            {
                grvCursoDetalle.Rows[i].Cells[0].Text = Convert.ToString(true);
                grvCursoDetalle_RowDataBound(i, false);

            }
        }
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
}