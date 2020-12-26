using enviarEmail;
using MessagingToolkit.QRCode.Codec;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string usuario, Username;
        var consultaSuc = new object();
        DateTime esteDia = DateTime.Today;

        if (!IsPostBack)
        {
            usuario = Convert.ToString(Session["SUsuarioID"]);
            if (usuario == "" || usuario == null)
                Response.Redirect("~/ingresar.aspx");


            /****************************/
            ((Principal)Master).lblUsuarioEnMaster.Text = usuario;

            //((Principal)Master).
            /****************************/


            string tipo = (string)Session["Tipo"];

            Username = Convert.ToString(Session["SUsuarioname"]);
            //lusuario = Username.Substring(0, 3);
            //lsucursal = Username.Substring(0, 3);

            /****************************/
            ((Principal)Master).lblUsuarioEnMaster.Text = "USUARIO: " + Username;

            setImageUrl();
            listarArchivos();

            var cCumpleanios = dc.sp_nominaCumpleanieros("TODOS", "");
            grvCumplaniero.DataSource = cCumpleanios;
            grvCumplaniero.DataBind();
            lblMensaje.Text = obtenerIP();
          
        }
    }


    protected string obtenerIP() 
    {
        string host = Dns.GetHostName();
        IPAddress[] ip = Dns.GetHostAddresses(host);
        IPHostEntry ipEntry = new IPHostEntry();
        ipEntry = Dns.GetHostEntry(host);


        return host + " " + ip[1].ToString() + " " + ip[0].ToString()+" " +
            Convert.ToString(ipEntry.AddressList[ipEntry.AddressList.Length -1])
            +" "+Convert.ToString(ipEntry.HostName);
    }

    protected void btnImgP_Click(object sender, EventArgs e)
    {
        if (Timer1.Enabled)
        {
            Timer1.Enabled = false;
            btnImgP.Text = "Iniciar secuencia de imágenes";
        }
        else
        {
            Timer1.Enabled = true;
            btnImgP.Text = "Parar secuencia de imágenes";
        }
    }

    #region CON XML

    /*CON XML*/
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        int i = (int)ViewState["ImageDisplayed"];
        i = i + 1;
        ViewState["ImageDisplayed"] = i;

        DataRow imageDataRow = ((DataSet)ViewState["ImageData"]).Tables["image"].Select().FirstOrDefault(x => x["order"].ToString() == i.ToString());
        if (imageDataRow != null)
        {
            Image1.ImageUrl = "~/images/avisos/" + imageDataRow["name"].ToString();
            lblImageName.Text = imageDataRow["name"].ToString();
            lblImageOrder.Text = imageDataRow["order"].ToString();
        }
        else
        {
           setImageUrl();
          

        }
    }


    private void setImageUrl()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("~/xml/images/imageData.xml"));
        ViewState["ImageData"] = ds;
        ViewState["ImageDisplayed"] = 1;
        DataRow imageDataRow = ds.Tables["image"].Select().FirstOrDefault(x => x["order"].ToString() == "1");
        Image1.ImageUrl = "~/images/iconos/" + imageDataRow["name"].ToString();
        lblImageName.Text = imageDataRow["name"].ToString();
        lblImageOrder.Text = imageDataRow["order"].ToString();


    }

    

    

    #endregion
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fuInicio.HasFile) 
        {
            fuInicio.PostedFile.SaveAs(Server.MapPath("~/Data/Informativos/") + fuInicio.FileName);
        }

        listarArchivos() ;
    }
    protected void listarArchivos() 
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Archivo", typeof(string));
        dt.Columns.Add("Dimension", typeof(string));
        dt.Columns.Add("Tipo", typeof(string));

        foreach (string strFile in Directory.GetFiles(Server.MapPath("~/Data/Informativos")))
        {
            FileInfo fi = new FileInfo(strFile);
            dt.Rows.Add(fi.Name, fi.Length, getFileTypeByExtension(fi.Extension));
        }

        grvArchivos.DataSource = dt;
        grvArchivos.DataBind();
    }


    private string getFileTypeByExtension(string extension) 
    {
        switch(extension.ToLower())
        {
            case ".doc":
            case ".docx":
                return "Word";
            case ".xls":
            case ".xlsx":
                return "Exel";
            case ".ppt":
            case ".pptx":
                return "Presentador";
            case ".jpg":
            case ".png":
                return "Imagen";
            case ".mp3":
                return "Sonido";
            case ".mp4":
                return "Video";
            case ".txt":
                return "Texto";
            case ".pdf":
                return "Adobe-Acrobat";
            case ".zip":
            case ".rar":
                return "Empaquetado";
            case ".vcx":
                return "ClasesFox";
            case ".vct":
                return "ClasesFox";
            default:
                return "Desconocido";

        }
    }
    protected void grvArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "downLoad") 
        {
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
            Response.TransmitFile(Server.MapPath("~/Data/Informativos/") + e.CommandArgument);
            Response.End();
        }
    }
    protected void grvCumplaniero_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cedula = Convert.ToString(grvCumplaniero.SelectedValue);

        string  nombreCorto = string.Empty;
        DateTime fechaIng = DateTime.Now;
        DateTime fechaNac = DateTime.Now;
        int sexo = 0;
        string emailDomicilio = string.Empty;
        string emailCorporativo = string.Empty;
        string sucursal = string.Empty;


        var cColabora = dc.sp_abmColaborador("CONSULTAR", cedula, "", "", "", "", false, "", 0, "", "", DateTime.Now, DateTime.Now, 0, 0, 0, "", "", "", "", "", false);

        foreach (var registro in cColabora)
        {
            nombreCorto = registro.nombreCorto;
            fechaIng = Convert.ToDateTime(registro.fechaIng);
            fechaNac = Convert.ToDateTime(registro.fechaNac);
            sexo = Convert.ToInt32(registro.sexo);
            emailDomicilio = registro.emailDomicilio;
            emailCorporativo = registro.emailCorporativo;
            sucursal = registro.sucursal;



        }
        if (enviarCorreoHtml(sucursal, nombreCorto, emailDomicilio, emailCorporativo,sexo,fechaIng,fechaNac))
        {
            //ds.sp_abmRegistroNota_Con("ENVIADO", id, 0, "", "", "", "", 0, 0, 0, 0, false, 0, 0, 0, 0, false, 0, false, 0, false, 0, false, false, "", false, false, "", "", "", "", "", "", "", 0, "", 0, "", false, true);
        }
    }

    /*ENVIAR CORREOS*/
    #region ENVIAR CORREOS (EMAIL)
  /*  public bool enviarCorreo(string sucursal, string nombreCorto, string emailDomicilio, string emailCorporativo,int sexo,DateTime fechaIng, DateTime fechaNac)
    {
        bool lenvio;

        string lsuc;

        lsuc = "";// ddlSucursal.SelectedValue.Trim();
        lenvio = false;

        string from = "socios@serviciosaneta.org.ec";
        string pass = "aneta54";
        //string to = txtEmailDom.Text.Trim(); //"jose_espinosa3l@hotmail.com"; //"jeec1965@gmail.com";//"jose_espinosa3l@hotmail.com";
        string to = enviarA;
        string msm = "Estimado/a " + alumno + " Felicidades! Haz terminado y aprobado tu curso de conducción exitosamente!  Tu certificado de aprobación se encuentra listo y puedes retirarlo de lunes a viernes, de 9h00 a 18H00, en la Secretaría Académica de la escuela donde realizaste el curso. El siguiente paso es obtener tu licencia, mayor información puedes obtener en el siguiente link:  Emisión de Licencia por primera vez Te agradecemos por haber escogido ANETA! y además por responder la siguiente encuesta que nos ayudará a mejorar el servicio que prestamos a nuestros usuarios: Encuesta de Satisfacción del Cliente No olvides conducir con responsabilidad y aplicando los conocimientos adquiridos que contribuirán a la movilidad sostenible y a la reducción de siniestros de tránsito en el País y en el mundo! Suerte! Saludos cordiales, Fabio Tamayo / ANETA Director Nacional Escuelas de Conducción Av. Gaspar de Villarroel E5-35 e Isla Isabela Quito - Ecuador Fijo (593) 2941210 Ext. 510 / Celular (593) 0999806803 ";

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

    }*/

    public bool enviarCorreoHtml(string sucursal, string nombreCorto, string emailDomicilio, string emailCorporativo, int sexo, DateTime fechaIng, DateTime fechaNac)
    {
        bool lenvio = false;
        /*VARIABLES ESCUELA*/
        string accion = "CONSULTAR";
        string escuela = sucursal;
        string administradorEscuela = string.Empty;
        string tituloAdministrador = "Director(a) de Escuela";
        string direccionEscuela = string.Empty;
        string ciudadEscuela = string.Empty;
        string telefonoEscuela = string.Empty;
        string emailEscuela = string.Empty;
        string paginaWeb = "www.aneta.org.ec";
        string caminoLogo = string.Empty;
        string nombreSucursal = string.Empty;



        var cEscuela = dc.sp_abmRuc2(accion, "", "", "", "", escuela, "", "", "", "", "", "", "", "", false, "", "");


        foreach (var registro in cEscuela)
        {
            nombreSucursal = registro.nom_suc;
            administradorEscuela = registro.administrador;
            direccionEscuela = registro.dirEstablecimiento;
            ciudadEscuela = registro.ciudad;
            telefonoEscuela = registro.telefono;
            emailEscuela = registro.email;
        }

        string mailOficina = "jose_espinosa3l@hotmail.com"; //"sistemas@aneta.org.ec"; 

        
        /*CALCULO DE EDAD*/
         int edad = DateTime.Today.AddTicks(-fechaIng.Ticks).Year - 1;
        
        
        /*ADJUNTAR IMAGENES*/
        string Filename1 = string.Empty;
        string Filename2 = string.Empty;
        string Filename3 = string.Empty;
        string Filename4 = string.Empty;


        Filename1 = Server.MapPath("~//Images//nomina//CUMPLE1.jpg");
        Filename2 = Server.MapPath("~//Images//nomina//CUMPLE2.jpg");
        if(sexo==2){
        Filename3 = Server.MapPath("~//Images//nomina//CUMPLE3.jpg");
        }
        else{
        Filename3 = Server.MapPath("~//Images//nomina//CUMPLE4.jpg");
        }
        Filename4 = Server.MapPath("~//Images//nomina//CUMPLE5.jpg");
        

        /*PLANTILLA EN HTML PARA EL ENVÍO DE MAIL*/
        caminoLogo = "~//Plantillas//tarjetaCumpleanios.html";


        /*CAMBIO DE VARIABLES EN EL HTML*/
        StringBuilder emailHtml = new StringBuilder(File.ReadAllText(Server.MapPath(caminoLogo)));



        emailHtml.Replace("NOMBRECORTO", "FELIZ CUMPLEAÑOS "+ nombreCorto);
        emailHtml.Replace("NOMBRECORTO", nombreCorto);
        emailHtml.Replace("ADMESCUELA", administradorEscuela);
        emailHtml.Replace("TITADMINISTRADOR", tituloAdministrador);
        emailHtml.Replace("DIRECCIONESCUELA", direccionEscuela);
        emailHtml.Replace("CIUDADESCUELA", ciudadEscuela);
        emailHtml.Replace("TELEFONOESCUELA", telefonoEscuela);
        emailHtml.Replace("TIEMPO",Convert.ToString(edad));
        
        emailHtml.Replace("PAGINAWEBESCUELA", paginaWeb);

        // emailHtml.Replace("codigoQR", codigoQR);

        string envio = "1";
        string destinatarios = string.Empty;
        string cc = string.Empty;
        string tituloEmail = "FELIZ CUMPLEAÑOS " + nombreCorto + "  " + fechaNac;

        if (envio == "1")
        {
            destinatarios = emailDomicilio.Trim();
            cc = "nomina@aneta.org.ec";//emailEscuela; //txtEmail.Text.Trim().ToLower() + "," + "socios@aneta.org.ec";
        }



        string anexo = "";//Server.MapPath("~//images//iconos//logo2.jpg");


        //new email().enviarCorreo("192.168.1.101", 25, "socios@serviciosaneta.org.ec", "aneta54", "MEMBRESIAS-ANETA", destinatarios, cc, "TARJETA VIRTUAL ANETA", anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4);
        //enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo, string front1, string front2, string front3, string back1)

        // if (new email().enviarCorreo("smtp.gmail.com", 25, "socios@serviciosaneta.org.ec", "lxane2k11", "MEMBRESIAS-ANETA", destinatarios, cc, tituloEmail , anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4))
        if (new email().enviarCorreo("192.168.1.110", 25, "nomina@aneta.org.ec", "aneta1010", "ESCUELA-ANETA", destinatarios, cc, tituloEmail, anexo, emailHtml.ToString(), Filename1, Filename2, Filename3, Filename4))
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

      protected int calcularEdad(DateTime nacimiento){
        int edad ;
        edad = DateTime.Now.Year - nacimiento.Year;
        if (nacimiento > DateTime.Now.AddYears(-edad)) edad -= 1;
        return edad;



      }

    #endregion
}