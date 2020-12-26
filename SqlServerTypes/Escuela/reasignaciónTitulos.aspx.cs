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

public partial class Escuela_reasignaciónTitulos : System.Web.UI.Page
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
            listarPedidos();

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

        grvCursoDetalle.DataSource = cAuto;
        grvCursoDetalle.DataBind();

    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;



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
    }
    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        activarObjetos();
        ddlPedido_SelectedIndexChanged();
    }
    protected void ddlSucursal_SelectedIndexChanged1(object sender, EventArgs e)
    {
        activarObjetos();
        ddlPedido_SelectedIndexChanged();
    }

    protected void ddlModalidad_SelectedIndexChanged1(object sender, EventArgs e)
    {
        listarCurso();
        ddlPedido_SelectedIndexChanged();

    }
    protected void ddlSucursal_SelectedIndexChanged2(object sender, EventArgs e)
    {
        listarCurso();
        ddlPedido_SelectedIndexChanged();
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

            listarAuto();
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
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);


        var cPedidos = ds.sp_listarPedidos("CONSULTAR", sucursal, cur_id);
        ddlPedido.DataSource = cPedidos;
        ddlPedido.DataBind();

        ListItem listCon = new ListItem("Seleccione pedido", "-1");

        ddlPedido.Items.Insert(0, listCon);
        lblMensaje.Text = "";

        ddlPedido_SelectedIndexChanged();
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
        lblMensaje.Text = "";
        string sucursal = ddlSucursal.SelectedValue;
        int tit_id = 0;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();
        int desde = 0;
        int hasta = 0;
        int serie_al = 0;



        var cSerie = ds.sp_abmTitulos("UNICO", tit_id, 0, 0, "", 0, false);

        foreach (var regNum in cSerie)
        {
            desde = Convert.ToInt32(regNum.ULTNUMASIGNADO) + 1;
            serie_al = Convert.ToInt32(regNum.SERIE_AL);


        };

        var cnumReg = ds.sp_PedidoTitulos_NumRegistros("PEDIDO", sucursal, cur_id, pedido);
        foreach (var regNum in cnumReg)
        {
            hasta = Convert.ToInt32(regNum.numReg);
        };


        hasta = (desde + hasta) - 1;


        if (serie_al < hasta)
        {
            lblMensaje.Text = "No puede asignar títulos, sobrepasa el número máximo de titulos";
           
        }



    }

    protected void ddlPedido_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPedido_SelectedIndexChanged();
    }

    protected void ddlPedido_SelectedIndexChanged()
    {
        string sucursal = ddlSucursal.SelectedValue;
        int cur_id = Convert.ToInt32(ddlCurso.SelectedValue);
        string pedido = ddlPedido.SelectedValue.Trim();
        int nActa = 0;
        int registros = 0;

        var cCurso = ds.sp_PedidoTitulos("PEDIDOS", sucursal, cur_id, pedido);
        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();

        registros = grvCursoDetalle.Rows.Count;

        if (registros > 0)
        {
            var cActa = ds.sp_abmActa("CONSULTAR", 0, "");

            foreach (var regNum in cActa)
            {
                nActa = Convert.ToInt32(regNum.ACTA) + 1;
            };



        }
        else
        {

        }

        lblMensaje.Text = "";
    }

    protected void btnActa_Click(object sender, EventArgs e)
    {

        DateTime esteDia = DateTime.Today;
        DateTime lfecha;

        //string lsuc,lfechaInicio,lfechaFin;



        lfecha = Convert.ToDateTime("01/01/2019");



        //dc.sp_repCierraCaja(lfechaInicio,lfechaFin,lsuc);

        string numActa = "";
        string dia = Convert.ToString(lfecha.Day);
        string mes = obtenerNombreMesNumero(lfecha.Month);
        string ano = Convert.ToString(lfecha.Year);
        string nombresAdministrador = string.Empty;
        string lsuc = ddlSucursal.SelectedValue;
        string sucursal = string.Empty;

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

            Session["pAdministrador"] = administrador;
            Session["pFirmaAdministrador"] = firmaAdministrador;


            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirActaEntregaTitulos.aspx','','width=800,height=500') </script>");
        }
    }


    protected void btnTitulo_Click(object sender, EventArgs e)
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

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('impresionListadoTitulos.aspx','','width=800,height=500') </script>");




        lblMensaje.Text = "";
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
}