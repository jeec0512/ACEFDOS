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

public partial class Escuela_envioEmailTitulos : System.Web.UI.Page
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
            grvCursoDetalle.EditIndex = 0;
            grvCursoDetalle.EditIndex = 1;
            grvCursoDetalle.EditIndex = 2;
            grvCursoDetalle.EditIndex = 3;
            grvCursoDetalle.EditIndex = 4;
            grvCursoDetalle.EditIndex = 5;
            grvCursoDetalle.EditIndex = 6;
            grvCursoDetalle.EditIndex = 7;
            grvCursoDetalle.EditIndex = 8;
            grvCursoDetalle.EditIndex = 9;
            grvCursoDetalle.EditIndex = 10;
            grvCursoDetalle.EditIndex = 11;
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

     //   grvCursoDetalle.DataSource = cAuto;
  //      grvCursoDetalle.DataBind();
//
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string sucursal = ddlSucursal.Text.Trim();
        string curso = ddlCurso.SelectedValue.Trim();
        string pedido = ddlPedido.SelectedValue;

        Session["pSucursal"] = sucursal;
        Session["pCurso"] = curso;
        Session["ppedido"] = pedido;

        //  Session["pCedula"] = cedula;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirTitulosxPedido.aspx','','width=1200,height=800') </script>");


        lblMensaje.Text = "";
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
        if (e.CommandName == "Jusveh")
        {
            bool lActivo = false;
        //    string ldoc = txtNumero.Text.Trim();
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

        if (e.CommandName == "EnviaMail")
        {
           int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            
            int id = Convert.ToInt32(row.Cells[0].Text);
            string alumno = Convert.ToString(row.Cells[6].Text);
            string fatura = Convert.ToString(row.Cells[7].Text);
            string email = Convert.ToString(row.Cells[10].Text).ToLower();

           /* enviarCorreo("sistemas@aneta.org.ec", alumno);

            lblMensaje.Visible = true;
            lblMensaje.Text = "Se envío el mail a:" +alumno;*/

            if (enviarCorreoHtml(alumno, email, fatura)) {
                ds.sp_abmRegistroNota_Con("ENVIADO", id, 0, "", "", "", "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, true);
            }

        }

        if (e.CommandName == "Confirmar")
        {
            lblMensaje.Text = "CONFIRMADO";

            grvCursoDetalle.BackImageUrl = "~/images/iconos/45.ico";

            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvCursoDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            
            

            string alumno = Convert.ToString(row.Cells[6].Text);
            string fatura = Convert.ToString(row.Cells[7].Text);
            string email = Convert.ToString(row.Cells[10].Text).ToLower();

            /* enviarCorreo("sistemas@aneta.org.ec", alumno);

             lblMensaje.Visible = true;
             lblMensaje.Text = "Se envío el mail a:" +alumno;*/

           /* enviarCorreoHtml(alumno, email, fatura);*/
        }

    }
    protected void grvCursoDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        bool estado;

         for (int i = 0; i < grvCursoDetalle.Rows.Count; i++)
         {
             estado = Convert.ToBoolean(grvCursoDetalle.Rows[i].Cells[15].Text);

             //string  estado = Convert.ToString(grvCursoDetalle.Rows[i].Cells[1].Text);

             if (estado)
             {
                 grvCursoDetalle.Rows[i].BackColor = Color.FromArgb(252, 128, 5);
                 grvCursoDetalle.Rows[i].ForeColor = Color.White;
             }
         }

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

        var cCurso = ds.sp_PedidoTitulos("TITULOS", sucursal, cur_id, pedido);
        grvCursoDetalle.DataSource = cCurso;
        grvCursoDetalle.DataBind();



        lblMensaje.Text = "";
    }
    protected void btnActa_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string sucursal = ddlSucursal.Text.Trim();
        string curso = ddlCurso.SelectedValue.Trim();
        string pedido = ddlPedido.SelectedValue;

        Session["pSucursal"] = sucursal;
        Session["pCurso"] = curso;
        Session["ppedido"] = pedido;

        //  Session["pCedula"] = cedula;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirActaEntregaTitulos.aspx','','width=1200,height=800') </script>");


        lblMensaje.Text = "";
    }
    /*ENVIAR CORREOS*/
    #region ENVIAR CORREOS
    public bool enviarCorreo(string enviarA, string alumno)
    {
        bool lenvio;

        string lsuc;

        lsuc = ddlSucursal.SelectedValue.Trim();
        lenvio = false;



        // string from = "jeec1965@gmail.com";
        //string pass = "mishijas2";

        string from = "socios@serviciosaneta.org.ec";
        string pass = "aneta54";
        //string to = txtEmailDom.Text.Trim(); //"jose_espinosa3l@hotmail.com"; //"jeec1965@gmail.com";//"jose_espinosa3l@hotmail.com";
        string to = enviarA;
        string msm = "Estimado/a " + alumno +  " Felicidades! Haz terminado y aprobado tu curso de conducción exitosamente!  Tu certificado de aprobación se encuentra listo y puedes retirarlo de lunes a viernes, de 9h00 a 18H00, en la Secretaría Académica de la escuela donde realizaste el curso. El siguiente paso es obtener tu licencia, mayor información puedes obtener en el siguiente link:  Emisión de Licencia por primera vez Te agradecemos por haber escogido ANETA! y además por responder la siguiente encuesta que nos ayudará a mejorar el servicio que prestamos a nuestros usuarios: Encuesta de Satisfacción del Cliente No olvides conducir con responsabilidad y aplicando los conocimientos adquiridos que contribuirán a la movilidad sostenible y a la reducción de siniestros de tránsito en el País y en el mundo! Suerte! Saludos cordiales, Fabio Tamayo / ANETA Director Nacional Escuelas de Conducción Av. Gaspar de Villarroel E5-35 e Isla Isabela Quito - Ecuador Fijo (593) 2941210 Ext. 510 / Celular (593) 0999806803 ";

        string subject = lsuc + " Título ANETA: " + alumno;

        if (new email().enviarCorreo(from, pass, to, msm, subject))
        {
            lblMensaje.Text = lblMensaje.Text + " Se envío el mail";
            lenvio = false;
        }
        else
        {
            lblMensaje.Text = lblMensaje.Text + " Fallo en el envío de correo electrónico";
            lenvio = false;
        }

       //new email().enviarCorreo("smtp-mail.outlook.com", 587, "jose_espinosa3l@hotmail.com", "LizFranDilan", "anonimo", "sistemas@aneta.org.ec", "sistemas@aneta.org.ec", "envios con adjuntos", "C:\\tmp\\ANETA0118.JPG", "ejemplo desde asp y c#");
       //string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo
        return lenvio;

    }

    public bool enviarCorreoHtml(string alumno, string emailEstudiante, string factura)
    {
        bool lenvio = false;
       // string cuerpo = formatoEstandar();
       // string mailDomicilio = emailEstudiante; //txtEmailDom.Text.Trim();


        /*VARIABLES ESCUELA*/
        string accion = "CONSULTAR";
        string escuela = ddlSucursal.SelectedValue;
        string administradorEscuela = string.Empty;
        string tituloAdministrador = "Director(a) de Escuela";
        string direccionEscuela = string.Empty;
        string ciudadEscuela = string.Empty;
        string telefonoEscuela = string.Empty;
        string emailEscuela = string.Empty;
        string paginaWeb = "www.aneta.org.ec";
        string caminoLogo = string.Empty;

        var cEscuela = dc.sp_abmRuc2(accion, "", "", "", "", escuela, "", "", "", "", "", "", "", "", false, "", "");


        foreach (var registro in cEscuela) 
        {
            administradorEscuela = registro.administrador;
            direccionEscuela = registro.dirEstablecimiento;
            ciudadEscuela = registro.ciudad;
            telefonoEscuela = registro.telefono;
            emailEscuela = registro.email;
        }

        string mailOficina = "jose_espinosa3l@hotmail.com"; //"sistemas@aneta.org.ec"; 

        /*VARIABLES DEL ESTUDIANTE*/


       /* string nombreSocio = "EJEMPLO";
        string cedula = "CI: 1708851181" ;
        string fechaInicio = "SOCIO DESDE: " ;
        string fechaFin = "VENCE: " ;
        string contrato = "Cont: " ;
        string factura = "FaMe:" ;
        string facturaTit = "FACTURA";

        string factTitulo = "TITULO";

        DateTime fechaHoy = DateTime.Today;
        string fecha = "Fecha Ing:" + Convert.ToString(fechaHoy).Substring(0, 10);

        string caminoLogo = string.Empty;
        string membresia = string.Empty;
        string tipomem = "MEMBRESIA";

        string codigoQR = generaQR(cedula + contrato + factura);

        string titulo = "Felicitaciones";
        string bienvenida = nombreSocio + " YA ESTÁ ";*/

        string Filename1 = string.Empty;
        string Filename2 = string.Empty;
        string Filename3 = string.Empty;
        string Filename4 = string.Empty;


        Filename1 = Server.MapPath("~//Images//socios//black//standarFront1_387.jpg");
        Filename2 = Server.MapPath("~//Images//socios//black//standarFront2_147.jpg");
        Filename3 = Server.MapPath("~//Images//socios//black//standarFront3_236.jpg");
        Filename4 = Server.MapPath("~//Images//socios//black//standarBack_387.jpg");
        
        caminoLogo = "~//Plantillas//entregaTituloAcademico.html";
        




        StringBuilder emailHtml = new StringBuilder(File.ReadAllText(Server.MapPath(caminoLogo)));




        emailHtml.Replace("ALUMNO", alumno);
        emailHtml.Replace("ADMESCUELA", administradorEscuela);
        emailHtml.Replace("TITADMINISTRADOR", tituloAdministrador);
        emailHtml.Replace("DIRECCIONESCUELA", direccionEscuela);
        emailHtml.Replace("CIUDADESCUELA", ciudadEscuela);
        emailHtml.Replace("TELEFONOESCUELA", telefonoEscuela);

        emailHtml.Replace("PAGINAWEBESCUELA", paginaWeb);

        // LinkedResource LinkedImage = new LinkedResource(@Filename);
        // LinkedImage.ContentId = "front1";
        //emailHtml.Replace("front1", Filename);




        /*emailHtml.Replace("front2", "http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackFront2.png");
        emailHtml.Replace("front3", "http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackFront3.png");

        emailHtml.Replace("back1", "http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackBack1.png");
        emailHtml.Replace("back2", "http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackBack2.png");
        emailHtml.Replace("back3", "http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackBack3.png");*/



        // emailHtml.Replace("codigoQR", codigoQR);

        string envio = "1";
        string destinatarios = string.Empty;
        string cc = string.Empty;
        string tituloEmail = "ENTREGA DE DOCUMENTOS ANETA del: " + alumno + "  N°: " + factura;

        if (envio == "1")
        {
            destinatarios = "jeec1965@gmail.com,jose_espinosa3l@hotmail.com, sistemas@aneta.org.ec"; //emailEstudiante;
            cc = emailEscuela; //txtEmail.Text.Trim().ToLower() + "," + "socios@aneta.org.ec";
        }

       

        string anexo = "";//Server.MapPath("~//images//iconos//logo2.jpg");


        //new email().enviarCorreo("192.168.1.101", 25, "socios@serviciosaneta.org.ec", "aneta54", "MEMBRESIAS-ANETA", destinatarios, cc, "TARJETA VIRTUAL ANETA", anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4);
        //enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo, string front1, string front2, string front3, string back1)

        // if (new email().enviarCorreo("smtp.gmail.com", 25, "socios@serviciosaneta.org.ec", "lxane2k11", "MEMBRESIAS-ANETA", destinatarios, cc, tituloEmail , anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4))
        if (new email().enviarCorreo("192.168.1.110", 25, "socios@aneta.org.ec", "aneta54", "ESCUELA-ANETA", destinatarios, cc, tituloEmail, anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4))
        {
            lblMensaje.Text = lblMensaje.Text + " Se envío el correo electrónico";
            lenvio = true;
        }
        else
        {
            lblMensaje.Text = lblMensaje.Text + " Fallo en el envío de correo electrónico";
            lenvio = false;
        }
        //email.enviarCorreo("192.168.1.101", 25, "socios@serviciosaneta.org.ec", "aneta54", "MEMBRESIAS-ANETA", destinatarios, cc, "TARJETA VIRTUAL ANETA", anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4);
        // lblMensaje.Text = email.mensaje.
        return lenvio;
    }

    protected string formatoEstandar()
    {
        string cuerpo = string.Empty;
        /*
        string nombreSocio = txtNombresTar.Text.Trim();
        string cedulaSocio = txtRucTar.Text.Trim();
        string fechaInicio = txtDesdeTar.Text;
        string fechaFin = txtVenceTar.Text;
        string numeroContrato = txtNumContratoTar.Text;
        string numerofactura = txtNumFactura.Text;

        DateTime fecha = DateTime.Today;
        string fechaActual = Convert.ToString(fecha);

        cuerpo = "<table style='max-width: 1200px; padding: 10px; margin:0 auto; border-collapse: collapse;'>";
        cuerpo = cuerpo + "<tr>";
        cuerpo = cuerpo + "<td style='background-color: white; text-align: left; padding: 0'>";
        cuerpo = cuerpo + "<a href='http://190.63.17.119'>";
        cuerpo = cuerpo + "<div style='width: 100%;margin:20px 0; display: inline-block;text-align: center'>";
        cuerpo = cuerpo + "<img tyle='padding: 0; width: 400px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/iconos/anetatitulo.jpg'>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</a>";
        cuerpo = cuerpo + "</td>";
        cuerpo = cuerpo + "</tr>";
        cuerpo = cuerpo + "<tr>";
        cuerpo = cuerpo + "<td style='background-color: white; text-align: left; padding: 0'>";
        cuerpo = cuerpo + "<div style='width: 100%;margin:5px 0; display: inline-block;text-align: center; position: RELATIVE;'>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 55px; top: 5px; z-index: 1;'>";
        cuerpo = cuerpo + "<img style='padding: 0; width: 400px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/socios/stand1.png'>";
        cuerpo = cuerpo + "</div >";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 505px; top: 5px; z-index: 2;'>";
        cuerpo = cuerpo + "<img style='padding: 0; width: 400px; margin: 5px' src='http://190.63.17.119:5090/acefdos/images/socios/stand2.png'>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 650px; top: 50px; z-index: 3; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + nombreSocio;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 650px; top: 70px; z-index: 7; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + "C.C.:";
        cuerpo = cuerpo + cedulaSocio;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 600px; top: 215px; z-index: 4; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + fechaInicio;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 800px; top: 215px; z-index: 4; color:white'>";
        cuerpo = cuerpo + "<P style='color :white'>";
        cuerpo = cuerpo + fechaFin;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 540px; top: 150px; z-index: 5; color:white'>";
        cuerpo = cuerpo + "<P style='color :white;font-size:10px;' >";
        cuerpo = cuerpo + "# Contrato:";
        cuerpo = cuerpo + numeroContrato;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 540px; top: 170px; z-index: 6; color:white'>";
        cuerpo = cuerpo + "<P style='color :white; font-size:10px;'>";
        cuerpo = cuerpo + "# Factura:";
        cuerpo = cuerpo + numerofactura;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div style='position: absolute;  left: 540px; top: 190px; z-index: 6; color:white'>";
        cuerpo = cuerpo + "<P style='color :white; font-size:10px;'>";
        cuerpo = cuerpo + "Fecha:";
        cuerpo = cuerpo + fechaActual;
        cuerpo = cuerpo + "</P>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</td>";
        cuerpo = cuerpo + "</tr>";
        cuerpo = cuerpo + "<tr>";
        cuerpo = cuerpo + "<td style='background-color: #ecf0f1;'>";
        cuerpo = cuerpo + "<div style='color: #34495e; margin: 30% 10% 2%; text-align: justify;font-family: sans-serif'>";
        cuerpo = cuerpo + "<h2 style='color: #e67e22; margin: 0 0 7px'>MEMBRESIA ESTANDAR</h2>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<div>";
        cuerpo = cuerpo + "<p style='margin: 15px; font-size: 15px'>";
        cuerpo = cuerpo + "Beneficios:<br>";
        cuerpo = cuerpo + "<ul style='font-size: 15px;  margin: 10px 0'>";
        cuerpo = cuerpo + "<li>3 servicios de chofer en caso de embriaguez.* </li>";
        cuerpo = cuerpo + "<li>3 servicios de Grúa o Plataforma.** </li>";
        cuerpo = cuerpo + "<li>3 servicios de Auxilio Mecánico.** </li>";
        cuerpo = cuerpo + "<li>5 servicios de Evaluación Psicosensométrica.** </li>";
        cuerpo = cuerpo + "<li>Cambio de aceite a domicilio con previa cita (aplica solo Quito y Guayaquil).** </li>";
        cuerpo = cuerpo + "<li>3 servicios de Lavado Express.** </li>";
        cuerpo = cuerpo + "<li>Precio preferencial en Cursos de Conducción.** </li>";
        cuerpo = cuerpo + "<li>Precio preferencial en Centros de Servicio Automotriz.** </li>";
        cuerpo = cuerpo + "<li>Agencia de Viajes y Turismo: Atención preferencial en compra de pasajes aéreos nacionales e internacionales y paquetes turísticos.** </li>";
        cuerpo = cuerpo + "<li>Llavero magnético de gasolina; por la compra prepago de $100 en combustible en las Estaciones de Servicio ANETA (aplica solo en Quito).** </li>";
        cuerpo = cuerpo + "<li>Tarjeta AAA (American Automobile Association) con descuentos en aproximadamente 100.00 locales de EE.UU., Canadá y Europa.* </li>";
        cuerpo = cuerpo + "</ul>";
        cuerpo = cuerpo + "<p style='margin: 15px; font-size: 15px'>Observaciones:<br>";
        cuerpo = cuerpo + "<ul style='font-size: 15px;  margin: 10px 0'>";
        cuerpo = cuerpo + "<li>Servicio válido para el Socio Titular de la tarjeta.</li>";
        cuerpo = cuerpo + "<li>**Servicio válido para el Socio Titular y su núcleo familiar hasta el cuarto grado de consanguinidad.</li>";
        cuerpo = cuerpo + "<li>Actividades de integración.</li>";
        cuerpo = cuerpo + "<li>Cobertura de 2 vehículos pertenecientes al núcleo familiar.</li>";
        cuerpo = cuerpo + "</ul>";
        cuerpo = cuerpo + "<div style='width: 100%; text-align: center'>";
        cuerpo = cuerpo + "<a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #3498db' href='http://190.63.17.119/'>Ir a la página principal de ANETA</a>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "<p style='color: #b3b3b3; font-size: 12px; text-align: center;margin: 15px 0 0'>AUTOMOVIL CLUB DEL ECUADOR 2018</p>";
        cuerpo = cuerpo + "</div>";
        cuerpo = cuerpo + "</td>";
        cuerpo = cuerpo + "</tr>";
        cuerpo = cuerpo + "</table>";*/
        return cuerpo;
    }

    protected string generaQR(string clave)
    {
        string codigoQR = string.Empty;

        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap img = encoder.Encode(clave.Trim());
        System.Drawing.Image QR = (System.Drawing.Image)img;

        using (MemoryStream ms = new MemoryStream())
        {
            QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = ms.ToArray();
            codigoQR = "data:image/gif;base64," + Convert.ToBase64String(imageBytes);

        }

        return codigoQR;
    }

    #endregion
    protected void grvCursoDetalle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grvCursoDetalle.EditIndex = e.NewEditIndex;
       
        
    }
}