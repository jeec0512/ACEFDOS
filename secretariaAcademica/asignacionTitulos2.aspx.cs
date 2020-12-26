using AjaxControlToolkit;
using enviarEmail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using MessagingToolkit.QRCode.Codec;
using System.Net.Mail;

public partial class secretariaAcademica_asignacionTitulos2 : System.Web.UI.Page
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
        string Num =string.Empty;
        string Factura =string.Empty;
        string Permiso  =string.Empty;
        bool IdPetri  = false;
        string PetriResultado  =string.Empty;
        string Licencia  =string.Empty;
        int A_EducVial  = 0;
        decimal N_EducVial  = 0;
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
        string Pedido =string.Empty;
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
            listarTitulos();
            listarCtrlTit();

            /*ejemplo de pdf*/

            //IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            // Render an HTML document or snippet as a string
            // Renderer.RenderHtmlAsPdf("<h1>Hello World</h1>").SaveAs("~/html-string.pdf");

            // Render any HTML fragment or document to HTML
            var Renderer = new IronPdf.HtmlToPdf();
            /* var PDF = Renderer.RenderHtmlAsPdf("<h1>Hello IronPdf</h1>");
             var OutputPath = "HtmlToPDF.pdf";
             PDF.SaveAs(OutputPath);
             // This neat trick opens our PDF file so we can see the result in our default PDF viewer
             System.Diagnostics.Process.Start(OutputPath);*/

            var HtmlTemplate = "<p>[[NAME]]</p>";
            var Names = new[] { "John", "James", "Jenny" };
            string camino = "~/admArchivos/escuela/";


            foreach (var name in Names)
            {
                var HtmlInstance = HtmlTemplate.Replace("[[NAME]]", name);
                var PDF = Renderer.RenderHtmlAsPdf(HtmlInstance);
                /*   PDF.SaveAs(camino+name + ".pdf");
                   camino = camino+name + ".pdf";

                   System.Diagnostics.Process.Start(Server.MapPath(camino));*/
            }

            certificado();
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
        /*lblMensaje.Text = string.Empty;
        pnAsignacion.Enabled = true;
        pnPedidos.Visible = false;
        pnBotonera.Enabled = true;
        btnGuardar.Visible = false;
        btnActa.Visible = false;
        btnTitulo.Visible = false;
        hlRegresar.Visible = true;
        pnAutoDetalle.Visible = true;
        pnPedidoTitulos.Visible = true;*/
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


    protected void listarTitulos()
    {
        string Accion = "CONSULTAR";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        string tipopedido = "A";

        var cPedido = ds.sp_abmTitulos(Accion, 0, 0, 0, "", 0, false);

        ddlTitulos.DataSource = cPedido;
        ddlTitulos.DataBind();

        ListItem listCon = new ListItem("Seleccione serie Título", "-1");

        ddlTitulos.Items.Insert(0, listCon);

    }

    protected void listarVerificados()
    {
        string Accion = "PEDIDO";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        string tipopedido = "A";
        string pedido = ddlPedido.SelectedValue.Trim();

        var cPedido = ds.sp_ValidarAprobados(Accion,sucursal,curso,pedido);
        
        grvVerificar.DataSource = cPedido;
        grvVerificar.DataBind();

         

    }

    protected void listarCtrlTit()
    {
        string Accion = "CONSULTAR";

        string sucursal = ddlSucursal.SelectedValue;
        int curso = Convert.ToInt32(ddlCurso.SelectedValue);
        string tipopedido = "A";
        int tit_id = Convert.ToInt32(ddlTitulos.SelectedValue);

        var cPedido = ds.sp_abmCtrlTit(Accion, 0, tit_id, DateTime.Today, 0, 0, "", "", "");

        ddlCtrlTit.DataSource = cPedido;
        ddlCtrlTit.DataBind();

        ListItem listCon = new ListItem("Seleccione serie Inicio de titulos", "-1");

        ddlCtrlTit.Items.Insert(0, listCon);

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


    protected void listarAuto()
    {
        string Accion = "TODOS";

        string sucursal = ddlSucursal.SelectedValue;

        var cAuto = ds.sp_abmAuto(Accion, 0, "", "", 0, "", "", "", "", 0, 0, 0, sucursal, false);

            pnPedidoTitulos.Visible = true;
            grvCursoDetalle.DataSource = cAuto;
            grvCursoDetalle.DataBind();
        
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            verificarDatos();
            int kont = 0;
            int desde = 0;
            int hasta = 0;
            int serie_al = 0;
            int numItems = 0;
            bool completo = false;
            string acta = string.Empty;
            string alterno = string.Empty;
            string pedido = ddlPedido.SelectedValue.Trim();

            string accion = "ACTUALIZAR";
            string sucursal = ddlSucursal.Text;
            int tit_id = Convert.ToInt32(ddlTitulos.SelectedValue);
            int ultimoNumero = Convert.ToInt32(txtHasta.Text);
            int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);

            DateTime fecha = Convert.ToDateTime(txtFecha.Text);

            var cSerie = ds.sp_abmTitulos("UNICO", tit_id, 0, 0, "", 0, false);

            foreach (var regNum in cSerie)
            {
                desde = Convert.ToInt32(regNum.ULTNUMASIGNADO) + 1;
                serie_al = Convert.ToInt32(regNum.SERIE_AL);
                alterno = regNum.ALTERNO;

            };

            if (ultimoNumero == serie_al)
            {
                completo = true;
            }
            /*TITULOS*/
            ds.sp_abmTitulos(accion, tit_id, 0, 0, "", ultimoNumero, completo);


            /*CONTROL DE TITULOS*/
            accion = "AGREGAR";
            desde = Convert.ToInt32(txtDesde.Text);
            hasta = Convert.ToInt32(txtHasta.Text);
            acta = txtActa.Text.Trim();

            ds.sp_abmCtrlTit(accion, 0, tit_id, fecha, desde, hasta, sucursal, acta, alterno);


            var cuentaAsigna = ds.sp_cuentaAsignaTitulos("ASIGNAR", sucursal, cur_id, desde, hasta, acta, pedido);
            foreach (var regNum in cuentaAsigna)
            {
                kont = Convert.ToInt32(regNum.TOTAL);

            };


            numItems = ddlMensaje.Items.Count;

            if (kont <= 0 && numItems <= 0)
            {
                ds.CommandTimeout = 360;
                /*ASIGNAR TITULOS*/
                //ds.sp_asignaTitulos("ASIGNAR", sucursal, cur_id, desde, hasta, acta, pedido);
                ds.sp_asignaTitulos2("ASIGNAR", sucursal, cur_id, desde, hasta, acta, pedido);

                ddlPedido_SelectedIndexChanged();

                lblMensaje.Text = "Asignación de títulos realizado con éxito";

                ds.sp_abmActa("MODIFICAR", 1, acta);
               // listarTitulos();
                //ddlTitulos_SelectedIndexChanged();
                
            }
            else
            {
                //lblMensaje.Text = "A este pedido ya le fueron asignados números de títulos ";
                throw new InvalidOperationException("A este pedido ya le fueron asignados números de títulos or tiene alumnos con algún inconveniente, revise el listado que le aparece en la parte superior");
            }


        }
        catch (Exception ex) 
        {
            lblMensaje.Text = ex.Message;
        }

        listarTitulos();
        ddlTitulos_SelectedIndexChanged();
    }

    private void verificarDatos()
    {
        //throw new NotImplementedException();
        lblMensaje.Text = string.Empty;

        //DEBE REFRESCAR ASIGNACIÓN DE TÍTULOS
        int modalidad = Convert.ToInt32(ddlModalidad.SelectedValue);
        int curso = Convert.ToInt32(ddlModalidad.SelectedValue);
        int pedido = Convert.ToInt32(ddlModalidad.SelectedValue);

        int titulo = Convert.ToInt32(ddlTitulos.SelectedValue);
        int numMensajes = ddlMensaje.Items.Count;

       if (numMensajes > 0)
       {
           throw new InvalidOperationException("Existen alumnos con algún inconveniente revise el listado");
       }
        if (modalidad == -1 || curso == -1 || pedido == -1)
        {
            throw new InvalidOperationException("No hay pedidos seleccionados");
        }

        if (titulo == -1)
        {
            throw new InvalidOperationException("No ha seleccionado serie para títulos");
        }

        if (txtHasta.Text == "0" || txtHasta.Text.Trim() == "")
        {
            //lblMensaje.Text = "No puede asignar títulos, sobrepasa el número máximo de titulos o no ha seleccionado serie de títulos";
            txtHasta.Text = Convert.ToString(0);
            throw new InvalidOperationException("No puede asignar títulos, sobrepasa el número máximo de titulos o no ha seleccionado serie de títulos");
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
        
        ddlPedido_SelectedIndexChanged();
        listarPedidos();
        activarObjetos();
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        ddlPedido_SelectedIndexChanged();
        listarCurso();
       // listarPedidos();
    }
   

    protected void grvCursoDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Jusveh")
        {
            bool lActivo = false;
            // string ldoc = txtNumero.Text.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[2].Text);


            VEHICULO vehiculo = da.VEHICULO.SingleOrDefault(x => x.VEH_ID == lid);
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

           // listarAuto();
        }

    }
    protected void grvCursoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        bool estado;

        /* for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
         {
             estado = Convert.ToBoolean(grvCursoDetalle.Rows[i].Cells[0].Text);

             //string  estado = Convert.ToString(grvCursoDetalle.Rows[i].Cells[1].Text);

             if (estado)
             {
                 grvCursoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                 grvCursoDetalle.Rows[i].ForeColor = Color.White;
             }
         }*/

    }



    protected void ddlCurso_SelectedIndexChanged(object sender, EventArgs e)
    {

        pnPedidos.Visible = true;
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);


        var cPedidos = ds.sp_listarPedidos("CONSULTAR", sucursal, cur_id);
        ddlPedido.DataSource = cPedidos;
        ddlPedido.DataBind();

        ListItem listCon = new ListItem("Seleccione pedido", "-1");

        ddlPedido.Items.Insert(0, listCon);
        lblMensaje.Text = string.Empty;

        ddlPedido_SelectedIndexChanged();
       // listarAuto();

        /*fechas de los cursos*/
        /*  var cCurso = from TCurso in ds.TB_CURSO
                       where TCurso.CUR_ID == cur_id
                       select new
                       {
                           fechaInicio = TCurso.CUR_FECHA_INICIO,
                           fechaFin = TCurso.CUR_FECHA_FIN
                       };


          if (cCurso.Count() == 0)
          {

              lblMensaje.Text = "No existe curso";
          }
          else
          {
              foreach (var registro in cCurso)
              {
                  txtDesde.Text += " " + Convert.ToString(registro.fechaInicio).Substring(0, 10);
                  txtHasta.Text += " " + Convert.ToString(registro.fechaFin).Substring(0, 10);
              }
          }*/
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
    protected void ddlTitulos_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlTitulos_SelectedIndexChanged();
    }

    protected void ddlTitulos_SelectedIndexChanged()
    {
        lblMensaje.Text = string.Empty;
        string sucursal = ddlSucursal.SelectedValue;
        int tit_id = Convert.ToInt32(ddlTitulos.SelectedValue);
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();
        int desde = 0;
        int hasta = 0;
        int serie_al = 0;
        string alterno = string.Empty;

        var cControl = ds.sp_abmCtrlTit("CONSULTAR", 0, tit_id, DateTime.Today, 0, 0, sucursal, "", "");

        ddlCtrlTit.DataSource = cControl;
        ddlCtrlTit.DataBind();


        var cSerie = ds.sp_abmTitulos("UNICO", tit_id, 0, 0, "", 0, false);

        foreach (var regNum in cSerie)
        {
            desde = Convert.ToInt32(regNum.ULTNUMASIGNADO) + 1;
            serie_al = Convert.ToInt32(regNum.SERIE_AL);
            alterno = regNum.ALTERNO;

        };

        var cnumReg = ds.sp_PedidoTitulos_NumRegistros("PEDIDO", sucursal, cur_id, pedido);
        foreach (var regNum in cnumReg)
        {
            hasta = Convert.ToInt32(regNum.numReg);
        };


        hasta = (desde + hasta) - 1;

        txtDesde.Text = Convert.ToString(desde);
        txtHasta.Text = Convert.ToString(hasta);
        txtAlterno.Text = alterno;

        if (serie_al < hasta)
        {
            lblMensaje.Text = "No puede asignar títulos, sobrepasa el número máximo de titulos";
            txtHasta.Text = Convert.ToString(0);
        }
    }

    protected void ddlPedido_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        
        ddlPedido_SelectedIndexChanged();
        listarVerificados();
        listarTitulos();
        lblMensaje.Text = mensaje;
    }

    protected void ddlPedido_SelectedIndexChanged()
    {
        mensaje = string.Empty;
        ddlMensaje.Items.Clear();
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();
        int nActa = 0;
        string lActa = string.Empty;
        int registros = 0;
        int Ldesde = 0, Lhasta = 0;
        string Lalterno = string.Empty; ;

        if (pedido == "-1")
        {
            btnTitulo.Visible = false;
        }
        else
        {
            pnPedidoTitulos.Visible = true;
            btnTitulo.Visible = true;

            txtActa.Text = string.Empty;

            ds.CommandTimeout = 360;
            var cCurso = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
            var cPedido = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
            foreach (var regCon in cPedido)
            {
                lActa = regCon.RNOTC_ACTA;
            };

            var cControl = ds.sp_abmCtrlTit("CONACTA", 0, 0, DateTime.Now, 0, 0, "", lActa, "");
            foreach (var regCtrl in cControl)
            {
                Ldesde = Convert.ToInt32(regCtrl.DESDE);
                Lhasta = Convert.ToInt32(regCtrl.HASTA);
                Lalterno = regCtrl.ALTERNO;
            };

            grvCursoDetalle.DataSource = cCurso;
            grvCursoDetalle.DataBind();
            /*SE DEBE CONSULTAR POR SUCURSAL Y CURSO*/
            registros = grvCursoDetalle.Rows.Count;

            if (registros > 0)
            {
                txtActa.Text = lActa;
                txtFecha.Text = Convert.ToString(DateTime.Now);



                txtDesde.Text = Convert.ToString(Ldesde);
                txtHasta.Text = Convert.ToString(Lhasta);
                txtAlterno.Text = Lalterno;


                /*var cActa = ds.sp_abmActa("CONSULTAR", 0, "");

                foreach (var regNum in cActa)
                {
                    nActa = Convert.ToInt32(regNum.ACTA) + 1;
                    //txtActa.Text = regNum.ACTA;
                };
                txtActa.Text = Convert.ToString(nActa);
                txtFecha.Text = Convert.ToString(DateTime.Now);
                */
            }
            else
            {
                txtActa.Text = string.Empty;
                /*TRAER EL NÚMERO DE ACTA DESDE EL CONSOLIDADO*/
                // var cConsolidado = ds.sp_abmRegistroNota_Con(
            }
        }

        lblMensaje.Text = "";
    }

    protected void btnActa_Click(object sender, EventArgs e)
    {

        DateTime esteDia = DateTime.Today;
        DateTime lfecha;

        //string lsuc,lfechaInicio,lfechaFin;



        lfecha = Convert.ToDateTime(txtFecha.Text);



        //dc.sp_repCierraCaja(lfechaInicio,lfechaFin,lsuc);

        string numActa = txtActa.Text.Trim();
        string dia = Convert.ToString(lfecha.Day);
        string mes = obtenerNombreMesNumero(lfecha.Month);
        string ano = Convert.ToString(lfecha.Year);
        string nombresAdministrador = string.Empty;
        string lsuc = ddlSucursal.SelectedValue;
        string sucursal = string.Empty;

        string numeroTitulos = Convert.ToString(Convert.ToInt32(txtHasta.Text) - Convert.ToInt32(txtDesde.Text) + 1);
        string del = txtDesde.Text;
        string al = txtHasta.Text;
        string alterno = txtAlterno.Text;
        string administrador = string.Empty;
        string firmaAdministrador = string.Empty;

        var cSuc = dc.sp_abmRuc2("CONSULTAR", "", "", "", "", lsuc, "", "", "", "", "", "", "", "", false, "", "");

        foreach (var regSuc in cSuc)
        {
            sucursal = regSuc.nom_suc;
            nombresAdministrador = regSuc.administrador;
            administrador = regSuc.administrador;
            firmaAdministrador = regSuc.administrador;
        };

        if (true)
        {
            Session["pFecha"] = lfecha;
            Session["pSuc"] = lsuc;
            Session["pNumActa"] = numActa;
            Session["pDia"] = dia;
            Session["pMes"] = mes;
            Session["pAno"] = ano;
            Session["pNombresAdministrador"] = nombresAdministrador;
            Session["pSucursal"] = sucursal;

            Session["pNumeroTitulos"] = numeroTitulos;
            Session["pDel"] = del;
            Session["pAl"] = al;
            Session["pAlterno"] = alterno;
            Session["pAdministrador"] = administrador;
            Session["pFirmaAdministrador"] = firmaAdministrador;


            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirActaEntregaTitulos.aspx','','width=800,height=500') </script>");
        }
    }


    protected void btnTitulo_Click(object sender, EventArgs e)
    {

        lblMensaje.Text = string.Empty;

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

       
        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('impresionListadoTitulos.aspx','','width=800,height=500') </script>");
       

        lblMensaje.Text = string.Empty;

    }


    protected void btnReasignar_Click(object sender, EventArgs e)
    {

        try
        {
            verificarDatos();
            int kont = 0;
            int desde = 0;
            int hasta = 0;
            int serie_al = 0;
            bool completo = false;
            string acta = string.Empty;
            string alterno = string.Empty;
            string pedido = ddlPedido.SelectedValue.Trim();

            string accion = "ACTUALIZAR";
            string sucursal = ddlSucursal.Text;
            int tit_id = Convert.ToInt32(ddlTitulos.SelectedValue);
            int ultimoNumero = Convert.ToInt32(txtHasta.Text);
            int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);

            DateTime fecha = Convert.ToDateTime(txtFecha.Text);

            var cSerie = ds.sp_abmTitulos("UNICO", tit_id, 0, 0, "", 0, false);

            foreach (var regNum in cSerie)
            {
                desde = Convert.ToInt32(regNum.ULTNUMASIGNADO) + 1;
                serie_al = Convert.ToInt32(regNum.SERIE_AL);
                alterno = regNum.ALTERNO;

            };

            if (ultimoNumero == serie_al)
            {
                completo = true;
            }
            /*TITULOS*/
            ds.sp_abmTitulos(accion, tit_id, 0, 0, "", ultimoNumero, completo);


            /*CONTROL DE TITULOS*/
            accion = "AGREGAR";
            desde = Convert.ToInt32(txtDesde.Text);
            hasta = Convert.ToInt32(txtHasta.Text);
            acta = txtActa.Text.Trim();

            ds.sp_abmCtrlTit(accion, 0, tit_id, fecha, desde, hasta, sucursal, acta, alterno);


            var cuentaAsigna = ds.sp_cuentaAsignaTitulos("ASIGNAR", sucursal, cur_id, desde, hasta, acta, pedido);
            foreach (var regNum in cuentaAsigna)
            {
                kont = Convert.ToInt32(regNum.TOTAL);

            };
            ds.CommandTimeout = 360;
           // if (kont <= 0)
            //{
                /*ASIGNAR TITULOS*/
                //ds.sp_asignaTitulos("ASIGNAR", sucursal, cur_id, desde, hasta, acta, pedido);
                ds.sp_asignaTitulos("ASIGNAR", sucursal, cur_id, desde, hasta, acta, pedido);

                ddlPedido_SelectedIndexChanged();

                lblMensaje.Text = "Reasignación de títulos realizado con éxito";

                ds.sp_abmActa("MODIFICAR", 1, acta);
                // listarTitulos();
                //ddlTitulos_SelectedIndexChanged();

           // }
           // else
            //{
                //lblMensaje.Text = "A este pedido ya le fueron asignados números de títulos ";
             //   throw new InvalidOperationException("A este pedido ya le fueron asignados números de títulos ");
            //}


        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        listarTitulos();
        ddlTitulos_SelectedIndexChanged();

    }

    protected void certificado()
    {
        DateTime Fecha = DateTime.Now;

        string sucursal = Convert.ToString(Session["pSucursal"]);
        int curso = Convert.ToInt32(Session["pCurso"]);
        string pedido = Convert.ToString(Session["ppedido"]);


        var cCurso = ds.sp_PedidoTitulos("TITULOS", sucursal, curso, pedido);

    }

    private string obtenerNombreMesNumero(int numeroMes)
    {
        try
        {
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
            string nombreMes = formatoFecha.GetMonthName(numeroMes);
            return nombreMes;
        }
        catch
        {
            return "Desconocido";
        }
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
            verificarPermiso(Permiso,  Num, alumno);

            /*MODALIDAD AUTOS*/
            if(modalidad == 1 || modalidad == 2 || modalidad == 3){
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
                    ListItem listCon = new ListItem(alumno + " Factura: "+regfac.factura+" no esta saldado en su totalidad", num);

                    ddlMensaje.Items.Insert(0, listCon);
                }
                else {
                   
                }
            }
        }
        
    }
    protected void  verificarPermiso(string Permiso, string num ,string alumno){
        string parte = Permiso.Substring(0, 3);
        lblMensaje.Text = parte;
        if (parte != "PDA") {
            //mensaje += "Número de permiso inválido";

            ListItem listCon = new ListItem(alumno+" # de permiso inválido", num);

            ddlMensaje.Items.Insert(0, listCon);
        }
    }
    protected void verificarAsistencias(string tipo, int asistencia, string num, string alumno)
    {
        switch (tipo)
        {
            case "EducVial":
                if(asistencia < 8 || asistencia >10)
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
                if (asistencia < 32 )
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
}