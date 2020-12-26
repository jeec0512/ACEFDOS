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

    #region VARIABLES GENERALES
    string Num = string.Empty;
    string Factura = string.Empty;
    string Permiso = string.Empty;
    bool IdPetri = false;
    string PetriResultado = string.Empty;
    string Licencia = string.Empty;
    int A_EducVial = 0;
    decimal N_EducVial = 0;
    decimal S1_EducVial = 0;
    decimal S2_EducVial = 0;
    int A_Prac = 0;
    decimal N_Prac = 0;
    decimal S1_Prac = 0;
    decimal S2_Prac = 0;
    int A_Psic = 0;
    int A_Paux = 0;
    int A_Mec = 0;
    int totalAsistencia = 0;
    string Pedido = string.Empty;
    string[] nombres = new string[1];
    string mensaje = string.Empty;
    string alumno = string.Empty;
    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();
            activarObjetos();
            listarModalidad();
            listarCurso();
            listarPedidos();
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

    protected void listarModalidad()
    {
        var cModalidad = ds.sp_abmModalidad("TODOS", 0, "", "");

        ddlModalidad.DataSource = cModalidad;
        ddlModalidad.DataBind();

        ListItem listCon = new ListItem("Seleccione Modalidad", "-1");

        ddlModalidad.Items.Insert(0, listCon);

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
        lblMensaje.Text = "";
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

        /****************************************************/
        /* VALIDACION DE NOTAS ASISTENACIAS PAGOS
        /****************************************************/
        int numRep = ddlMensaje.Items.Count;
        
        /****************************************************/

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
        /*ojo revisar*/

            if (numReg > 0 && numRep <= 0)
            {
                ds.sp_abmPedidos(accion, 0, NUMPEDIDO, sucursal, NUMERAL, NUMREGISTROS, "", TIPOPEDIDO, usuario, fecha, ASIGNADO, curso);
                ds.sp_PedidoTitulos("MODIFICAR", sucursal, curso, NUMPEDIDO);
                ddlCurso_SelectedIndexChanged();
                lblMensaje.Text = "Pedido #:" + NUMPEDIDO + " realizado con éxito";
            }
            else
            {
                lblMensaje.Text = "NO EXISTEN ALUMNOS APROBADOS PARA GENERAR EL PEDIDO DE TÍTULOS O NO ESTÁN CONFIRMADOS";
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
        listarCurso();
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

            lActivo = Convert.ToBoolean(row.Cells[0].Text);
            
   
          
           /* TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
            lActivo = Convert.ToBoolean(TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION);
           */
            if (lActivo)
            {
                lActivo = false;
                row.Cells[0].Text = Convert.ToString(false);
                TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
                TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = lActivo;
                ds.SubmitChanges();
               

            }
            else
            {
                lActivo = true;
                row.Cells[0].Text = Convert.ToString(true);
                TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
                TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = lActivo;
                ds.SubmitChanges();
                
            }

            
            grvCursoDetalle_RowDataBound(indice, lActivo);

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
        int id = 0;

        for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
        {
            id = Convert.ToInt32(grvCursoDetalle.Rows[i].Cells[1].Text);
            TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(p => p.RNOTC_ID == id);
            TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = true;
            ds.SubmitChanges();
        }
         
        */

    }


    protected void grvCursoDetalle_RowDataBound(int indice, bool activo)
    {
            int id = 0;
    
            if (activo)
            {
                grvCursoDetalle.Rows[indice].BackColor = Color.FromArgb(255, 178, 115);
                grvCursoDetalle.Rows[indice].ForeColor = Color.Black;
            }
            else
            {
                grvCursoDetalle.Rows[indice].BackColor = Color.White;
                grvCursoDetalle.Rows[indice].ForeColor = Color.Black;
                id = Convert.ToInt32(grvCursoDetalle.Rows[indice].Cells[1].Text);

                /*tbl_secuenciales tbl_secuenciales = dc.tbl_secuenciales.SingleOrDefault(p => p.sucursal == suc);
                tbl_secuenciales.retencion = sec;
                dc.SubmitChanges();*/

               /* TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(p => p.RNOTC_ID == id);
                TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = false;
                ds.SubmitChanges();*/


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

        btnGuardar.Visible = true;

        listarVerificados();

       // grvCursoDetalle.DataSource = cCurso;
       // grvCursoDetalle.DataBind();


        grvPedidoTitulos.DataSource = cCurso;
        grvPedidoTitulos.DataBind();

        int filas = Convert.ToInt32(grvPedidoTitulos.Rows.Count);

        if (filas <= 0)
        {
            btnGuardar.Visible = false;
            lblMensaje.Text = "No existen alumnos aprobados";
            pnAutoDetalle.Visible = false;
            pnPedidoTitulos.Visible = false;
        }
        else
        {
            
            pnAutoDetalle.Visible = false;
            pnPedidoTitulos.Visible = true;
            btnGuardar.Visible = true;
            lblMensaje.Text = "";

            pnPedido.Visible = true;

            /*for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
            {
                bool activo = Convert.ToBoolean(grvCursoDetalle.Rows[i].Cells[0].Text); // = Convert.ToString(true);
                grvCursoDetalle_RowDataBound(i, activo);

            }*/

            
        }

        listarPedidos();
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected void ddlPedido_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();

        var cCurso = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();

        lblMensaje.Text = "";
        if (pedido == "-1")
        {
            pnPedidos.Visible = false;
            pnPedidoTitulos.Visible = false;
            pnAutoDetalle.Visible = false;
            btnGuardar.Visible = false;
            pnBaseANT.Visible = false;
        }
        else
        {
            grvBaseANT.DataSource = ds.sp_PedidoTitulos4("PEDIDOS", sucursal, cur_id, pedido);
            grvBaseANT.DataBind();

            pnPedidos.Visible = true;
            pnPedidoTitulos.Visible = false;
            pnAutoDetalle.Visible = true;
            btnGuardar.Visible = false;
            pnBaseANT.Visible = true;
        }


    }

    protected void listarPedidos()
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


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string acta = string.Empty;
        string alterno = string.Empty;

        string accion = "ACTUALIZAR";
        string sucursal = ddlSucursal.Text;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue;


        Session["pSucursal"] = sucursal;
        Session["pCur_id"] = cur_id;
        Session["pPedido"] = pedido;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('impresionPedidoTitulos.aspx','','width=800,height=500') </script>");




        lblMensaje.Text = "";
    }



    protected void btnbtnBaseANT_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string acta = string.Empty;
        string alterno = string.Empty;

        string accion = "ACTUALIZAR";
        string sucursal = ddlSucursal.Text;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue;


        Session["pSucursal"] = sucursal;
        Session["pCur_id"] = cur_id;
        Session["pPedido"] = pedido;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('impresionPedidoTitulos.aspx','','width=800,height=500') </script>");




        lblMensaje.Text = "";
    }


    /*SOLICITUD DE TITULOS CON OBSERVACIONES*/

    #region SOLICITUD DE TITULOS Y EDITAR OBSERVACIÓN
    
    /*protected void btnproveedor_Click(object sender, EventArgs e)
    {
        string proveedor = txtProveedor.Text.Trim();
        
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        pnAltas.Visible = true;
        pnCXPProveedor.Visible = false;
    }


    protected void listarCXP(string ruc)
    {
        // sp_ListarCXP 'XCEDULA' ,'1792323096001', ''
        var cCXP = dc.sp_ListarCXP("XCEDULA", ruc, "");


        grvPedidoTitulos.DataSource = cCXP;
        grvPedidoTitulos.DataBind();


        if (grvPedidoTitulos.Rows.Count <= 0)
        {
            lblMensaje.Text = "No existen facturas de cuentas por pagar para este proveedor";
        }

    }*/

    protected void grvPedidoTitulos_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName.Equals("Edit"))
            {
                lblMensaje.Text = "editar";
                lblMensaje.Text = Convert.ToString(grvPedidoTitulos.FooterRow.FindControl("txtObservacion"));
            }

            /*CONFIRMACION*/
            if (e.CommandName == "Confirmar")
            {
                bool lActivo = false;
                // string ldoc = txtNumero.Text.Trim();
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvPedidoTitulos.Rows[indice];
                int id_pregunta = row.DataItemIndex;
                int lid = Convert.ToInt32(row.Cells[0].Text);

                //string cadena = Convert.ToString(row.Cells[27].Text);
                lActivo = Convert.ToBoolean(row.Cells[27].Text);

                /* TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
                 lActivo = Convert.ToBoolean(TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION);
                */
                if (lActivo)
                {
                    lActivo = false;
                    row.Cells[0].Text = Convert.ToString(false);
                    TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
                    TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = lActivo;
                    ds.SubmitChanges();


                }
                else
                {
                    lActivo = true;
                    row.Cells[0].Text = Convert.ToString(true);
                    TB_REGISTRO_NOTA_CON TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
                    TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = lActivo;
                    ds.SubmitChanges();

                }


                grvPedidoTitulos_RowDataBound(indice, lActivo);

                /*
                TB_REGISTRO_NOTA_CON = ds.TB_REGISTRO_NOTA_CON.SingleOrDefault(x => x.RNOTC_ID == lid);
                TB_REGISTRO_NOTA_CON.RNOTC_CONFIRMACION = lActivo;
                ds.SubmitChanges();

                ddlCurso_SelectedIndexChanged();*/


            }



        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }
    protected void grvPedidoTitulos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string sucursal = ddlSucursal.SelectedValue;
        string usuario = (string)Session["SUsername"];
       // string proveedor = txtProveedor.Text.Trim();

        //
        // Obtengo el id de la entidad que se esta editando
        // en este caso de la entidad Person
        //

        grvPedidoTitulos.EditIndex = e.NewEditIndex;

        
    }

    protected void grvPedidoTitulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
       // string proveedor = txtProveedor.Text.Trim();
        grvPedidoTitulos.EditIndex = -1;
        
    }
    protected void grvPedidoTitulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sucursal = ddlSucursal.SelectedValue;
        string nombreEstablecimiento = Convert.ToString(ddlSucursal.SelectedItem);
        string usuario = (string)Session["SUsername"];
      //  string proveedor = txtProveedor.Text.Trim();
        int idDetalle = Convert.ToInt32(grvPedidoTitulos.DataKeys[e.RowIndex].Value.ToString());

        GridViewRow row = grvPedidoTitulos.Rows[e.RowIndex];

        string nombreProveedor = Convert.ToString(row.Cells[0].Text);
        string nombreSucursal = Convert.ToString(row.Cells[1].Text);
        string facturaUtilzada = Convert.ToString(row.Cells[2].Text);
       // decimal saldoCxp = Convert.ToDecimal(row.Cells[3].Text);
       



        //string documentoAfectado = lblTitulo.Text.Substring(11);

        var abono = grvPedidoTitulos.Rows[e.RowIndex].FindControl("txtObservacion") as TextBox;

        //decimal valorAbono = Convert.ToDecimal(abono.Text);
        string cValorfactura = Convert.ToString(abono.Text);

        try
        {

           // if (valorAbono <= saldoCxp)
            //{

/*
                txtValorFactura.Text = cValorfactura;
                txtDescripcion.Text = proveedor.Trim() + " " + nombreProveedor.Trim() + " #Factura" + documentoAfectado.Trim() + "-Establecimiento emisor:" + nombreSucursal.Trim() + "-Establecimiento receptor:" + nombreEstablecimiento.Trim();
                txtNumDocumento.Text = facturaUtilzada.Trim();
                dc.sp_AbonosXCruce("GUARDAR", 0, DateTime.Now, proveedor, facturaUtilzada, valorAbono, documentoAfectado, usuario, DateTime.Now, sucursal);
                dc.sp_abmEgresosDetalleCXP("MODIFICAR", idDetalle, valorAbono);
                btnRegistrar_Click();
                lblMensaje.Text = "Valor actualizado";
                enviarMail();
 * */
            //}
            //else
            //{
                lblMensaje.Text = "Valor no actualizado";
            //}

            grvPedidoTitulos.EditIndex = -1;
           
            
        }
        catch (Exception ex)
        {

            lblMensaje.Text = ex.Message;
        }
    }
    #endregion


    protected void grvPedidoTitulos_DataBound(object sender, EventArgs e)
    {

    }

    protected void grvPedidoTitulos_RowDataBound(int indice, bool activo)
    {
        int id = 0;

        if (activo)
        {
            grvPedidoTitulos.Rows[indice].BackColor = Color.FromArgb(255, 178, 115);
            grvPedidoTitulos.Rows[indice].ForeColor = Color.Black;
        }
        else
        {
            grvPedidoTitulos.Rows[indice].BackColor = Color.White;
            grvPedidoTitulos.Rows[indice].ForeColor = Color.Black;
            id = Convert.ToInt32(grvPedidoTitulos.Rows[indice].Cells[1].Text);
        }
    }

    protected void grvPedidoTitulos_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(grvPedidoTitulos.SelectedValue);
        string nombres = string.Empty;
        txtId.Text = Convert.ToString(id);
        var estudiante = ds.sp_abmRegistroNota_Con("CONSULTAR", id, 0, "", "", "", "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, false);
        foreach (var registro in estudiante)
        {
            nombres = registro.RNOTC_APELLIDOS.Trim()+" " + registro.RNOTC_NOMBRES.Trim();
            chkActivo.Checked = Convert.ToBoolean(registro.RNOTC_CONFIRMACION);
            txtObservacion.Text = registro.RNOTC_OBSERVACIONES.Trim();
        }
       


        txtEstudiante.Text = nombres;

        pnBotonera.Visible = false;
        pnPedidoTitulos.Visible = false;
        pnActiva.Visible = true;

        

    }
    protected void btnGuardaObservacion_Click(object sender, EventArgs e)
    {
        int lid = Convert.ToInt32(grvPedidoTitulos.SelectedValue);
        bool lactivo = Convert.ToBoolean(chkActivo.Checked);
        string observacion = txtObservacion.Text.Trim();

        ds.sp_actualizaRegistroNota_Con("OBSERVA",lid,0,"","","",observacion,0,"",0,"",lactivo,false);
        ddlCurso_SelectedIndexChanged();
        btnRegresar_Click();

    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        pnBotonera.Visible = true;
        pnPedidoTitulos.Visible = true;
        pnActiva.Visible = false;
    }


    #region EXCEL
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void uno()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grvCursoDetalle.AllowPaging = false;
                /// this.BindGrid();

                grvCursoDetalle.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvCursoDetalle.HeaderRow.Cells)
                {
                    cell.BackColor = grvCursoDetalle.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvCursoDetalle.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvCursoDetalle.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvCursoDetalle.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvCursoDetalle.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e) {
            lblMensaje.Text = "No existe datos";
        }
    }


    protected void dos()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grvPedidoTitulos.AllowPaging = true;
                /// this.BindGrid();

                grvPedidoTitulos.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvPedidoTitulos.HeaderRow.Cells)
                {
                    cell.BackColor = grvPedidoTitulos.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvPedidoTitulos.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvPedidoTitulos.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvPedidoTitulos.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvPedidoTitulos.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }
    }


    protected void tres()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grvBaseANT.AllowPaging = true;
                /// this.BindGrid();

                grvBaseANT.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvBaseANT.HeaderRow.Cells)
                {
                    cell.BackColor = grvBaseANT.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvBaseANT.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvBaseANT.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvBaseANT.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvBaseANT.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception e)
        {
            lblMensaje.Text = "No existe datos";
        }
    }


    


    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelRf_Click(object sender, EventArgs e)
    {
        dos();
    }

    protected void btnExcelPe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnAlumnos_Click(object sender, EventArgs e)
    {
        dos();
    }

    protected void btnBaseANT_Click(object sender, EventArgs e)
    {
        tres();
    }
    #endregion

    /********************************************/
    /* VERIFICACION NOTAS ASISTENCIAS PAGOS
    /********************************************/
    #region VERIFICACION NOTAS ASISTENCIAS PAGOS

    protected void listarVerificados()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        string tipopedido = "A";
        string pedido = ddlPedido.SelectedValue.Trim();

        var cPedido = ds.sp_ValidarAprobados(Accion, sucursal, curso, pedido);

        grvVerificar.DataSource = cPedido;
        grvVerificar.DataBind();



    }
    protected void grvVerificar_RowDataBound(object sender, GridViewRowEventArgs e)
    {




        string sucursal = ddlSucursal.SelectedValue;
        string tipo = string.Empty;




        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);

        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {


            Num = Convert.ToString(e.Row.Cells[0].Text);
            Factura = Convert.ToString(e.Row.Cells[1].Text);
            Permiso = Convert.ToString(e.Row.Cells[2].Text);
            IdPetri = Convert.ToBoolean(e.Row.Cells[3].Text);
            PetriResultado = Convert.ToString(e.Row.Cells[4].Text);
            Licencia = Convert.ToString(e.Row.Cells[5].Text);
            A_EducVial = Convert.ToInt32(e.Row.Cells[6].Text);
            N_EducVial = Convert.ToDecimal(e.Row.Cells[7].Text);
            S1_EducVial = Convert.ToDecimal(e.Row.Cells[8].Text);
            S2_EducVial = Convert.ToDecimal(e.Row.Cells[9].Text);
            A_Prac = Convert.ToInt32(e.Row.Cells[10].Text);
            N_Prac = Convert.ToDecimal(e.Row.Cells[11].Text);
            S1_Prac = Convert.ToDecimal(e.Row.Cells[12].Text);
            S2_Prac = Convert.ToDecimal(e.Row.Cells[13].Text);
            A_Psic = Convert.ToInt32(e.Row.Cells[14].Text);
            A_Paux = Convert.ToInt32(e.Row.Cells[15].Text);
            A_Mec = Convert.ToInt32(e.Row.Cells[16].Text);
            Pedido = Convert.ToString(e.Row.Cells[17].Text);
            alumno = Convert.ToString(e.Row.Cells[18].Text);
            ///
            ///VALIDAR FACTURA CANCELDA EN SU TOTALIDAD
            ///
            //Factura = llenarCeros(Factura, '0', 9);


            totalAsistencia = A_EducVial + A_Prac + A_Psic + A_Paux + A_Mec;

            verificarFactura(Factura, Num, alumno);
            verificarPermiso(Permiso, Num, alumno);

            /*MODALIDAD AUTOS*/
            if (modalidad == 1 || modalidad == 2 || modalidad == 3)
            {
                tipo = "EducVial";
                verificarAsistencias(tipo, A_EducVial, Num, alumno);
                tipo = "Prac";
                verificarAsistencias(tipo, A_Prac, Num, alumno);
                tipo = "Psic";
                verificarAsistencias(tipo, A_Psic, Num, alumno);
                tipo = "PrimAux";
                verificarAsistencias(tipo, A_Paux, Num, alumno);
                tipo = "Mec";
                verificarAsistencias(tipo, A_Mec, Num, alumno);
                tipo = "Total";
                verificarAsistencias(tipo, totalAsistencia, Num, alumno);
            }


            if (modalidad == 9 || modalidad == 10 || modalidad == 11)
            {
                tipo = "EducVial";
                verificarAsistenciasMotos(tipo, A_EducVial, Num, alumno);
                tipo = "Prac";
                verificarAsistenciasMotos(tipo, A_Prac, Num, alumno);
                tipo = "Psic";
                verificarAsistenciasMotos(tipo, A_Psic, Num, alumno);
                tipo = "PrimAux";
                verificarAsistenciasMotos(tipo, A_Paux, Num, alumno);
                tipo = "Mec";
                verificarAsistenciasMotos(tipo, A_Mec, Num, alumno);
                tipo = "Total";
                verificarAsistenciasMotos(tipo, totalAsistencia, Num, alumno);
            }

            tipo = "EducVial";
            verificarNotas(tipo, N_EducVial, S1_EducVial, S2_EducVial, Num, alumno);

            tipo = "Prac";
            verificarNotas(tipo, N_Prac, S1_Prac, S2_Prac, Num, alumno);

            verificarIdPetri(IdPetri, Num, alumno);
            //verificarPetri(PetriResultado, Num, alumno);
            verificarLicencia(Licencia, Num, alumno);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            //mensaje += Factura;
        }
    }


    protected void verificarFactura(string Factura, string num, string alumno)
    {
        df.CommandTimeout = 360;
        var cFactura = from tFactura in df.FACTURA
                       where tFactura.FAC_ESTABLECIMIENTO + "-" + tFactura.FAC_PUNTOEMISION + "-" + tFactura.FAC_SECUENCIAL == Factura
                       select new
                       {
                           recauda = tFactura.FAC_RECAUDADO,
                           total = tFactura.FAC_IMPORTETOTAL,
                           factura = tFactura.FAC_ESTABLECIMIENTO + "-" + tFactura.FAC_PUNTOEMISION + "-" + tFactura.FAC_SECUENCIAL
                       };


        if (cFactura.Count() == 0)
        {

            mensaje += "No existe factura:" + Factura;
        }
        else
        {
            foreach (var regfac in cFactura)
            {
                if (regfac.recauda != regfac.total)
                {
                    ListItem listCon = new ListItem(alumno + " Factura: " + regfac.factura + " no esta saldado en su totalidad", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                else
                {

                }
            }
        }

    }
    protected void verificarPermiso(string Permiso, string num, string alumno)
    {
        string parte = Permiso.Substring(0, 3);
        lblMensaje.Text = parte;
        if (parte != "PDA")
        {
            //mensaje += "Número de permiso inválido";

            ListItem listCon = new ListItem(alumno + " # de permiso inválido", num);

            ddlMensaje.Items.Insert(0, listCon);
        }
    }
    protected void verificarAsistencias(string tipo, int asistencia, string num, string alumno)
    {
        switch (tipo)
        {
            case "EducVial":
                if (asistencia < 8 || asistencia > 10)
                {
                    //mensaje += "Inválido por asistencia en educación víal";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en educación víal", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Prac":
                if (asistencia < 13 || asistencia > 15)
                {
                    //mensaje += "Inválido por asistencia en práctica";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en práctica", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Psic":
                if (asistencia < 2 || asistencia > 2)
                {
                    //mensaje += "Inválido por asistencia en psicología";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en psicología", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "PrimAux":
                if (asistencia < 2 || asistencia > 2)
                {
                    // mensaje += "Inválido por asistencia en primeros auxilios";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en primeros auxilios", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Mec":
                if (asistencia < 3 || asistencia > 5)
                {
                    // mensaje += "Inválido por asistencia en mecánica";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en mecánica", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Total":
                if (asistencia < 32)
                {
                    //mensaje += "Inválido por total de asistencias no menos de 32";
                    ListItem listCon = new ListItem(alumno + " Inválido por total de asistencias no menos de 32", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
        }
    }

    protected void verificarAsistenciasMotos(string tipo, int asistencia, string num, string alumno)
    {
        switch (tipo)
        {
            case "EducVial":
                if (asistencia < 8 || asistencia > 10)
                {
                    //mensaje += "Inválido por asistencia en educación víal";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en educación víal", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Prac":
                if (asistencia < 8 || asistencia > 10)
                {
                    //mensaje += "Inválido por asistencia en práctica";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en práctica", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Psic":
                if (asistencia < 2 || asistencia > 2)
                {
                    //mensaje += "Inválido por asistencia en psicología";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en psicología", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "PrimAux":
                if (asistencia < 1 || asistencia > 1)
                {
                    // mensaje += "Inválido por asistencia en primeros auxilios";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en primeros auxilios", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Mec":
                if (asistencia < 1 || asistencia > 1)
                {
                    // mensaje += "Inválido por asistencia en mecánica";
                    ListItem listCon = new ListItem(alumno + " Inválido por asistencia en mecánica", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
            case "Total":
                if (asistencia < 22)
                {
                    //mensaje += "Inválido por total de asistencias no menos de 32";
                    ListItem listCon = new ListItem(alumno + " Inválido por total de asistencias no menos de 22", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
        }
    }
    protected void verificarNotas(string tipo, decimal n1, decimal n2, decimal n3, string num, string alumno)
    {
        switch (tipo)
        {
            case "EducVial":
                if (n1 < 16 && n2 < 16 && n3 < 16)
                {
                    //mensaje += "Inválido por nota en educación víal";
                    ListItem listCon = new ListItem(alumno + " Inválido por nota en educación víal", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }

                if (n1 > 20 || n2 > 20 || n3 > 20)
                {
                    //mensaje += "Inválido por nota en educación víal";
                    ListItem listCon = new ListItem(alumno + " Inválido por nota en educación víal", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }

                break;
            case "Prac":
                if (n1 < 16 && n2 < 16 && n3 < 16)
                {
                    //mensaje += "Inválido por nota en práctica";
                    ListItem listCon = new ListItem(alumno + " Inválido por nota en práctica", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                if (n1 > 20 || n2 > 20 || n3 > 20)
                {
                    //mensaje += "Inválido por nota en educación víal";
                    ListItem listCon = new ListItem(alumno + " Inválido por nota en práctica", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                break;
        }
    }
    protected void verificarIdPetri(bool IdPetri, string num, string alumno)
    {
        if (!IdPetri)
        {
            //mensaje += "Inválido por código de petrinovic";
            ListItem listCon = new ListItem(alumno + " Inválido por no estar aprobado el examen de petrinovic", num);

            ddlMensaje.Items.Insert(0, listCon);
        }

    }
    protected void verificarPetri(string petriResultado, string num, string alumno)
    {
        if (petriResultado != "Aprobado")
        {
            //mensaje += "Inválido por no estar aprobado el examen de petrinovic";
            ListItem listCon = new ListItem(alumno + " Inválido por no estar aprobado el examen de petrinovic", num);

            ddlMensaje.Items.Insert(0, listCon);
        }

    }
    protected void verificarLicencia(string licencia, string num, string alumno)
    {
        if (licencia.Length <= 0 || licencia.Equals(null))
        {
            //mensaje += "Inválido por código de licencia";
            ListItem listCon = new ListItem(alumno + " Inválido por código de licencia", num);

            ddlMensaje.Items.Insert(0, listCon);
        }
    }
    #endregion 



}