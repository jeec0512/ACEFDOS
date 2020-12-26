using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;

public partial class Escuela_imprimirSicoPractico : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    //Data_FacDataContext df = new Data_FacDataContext();
    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    //Data_sriDataContext dc = new Data_sriDataContext();
    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["EscuelaConnectionString"].ConnectionString;

    Data_EscuelaDataContext de = new Data_EscuelaDataContext();

    decimal subtotal = 0;
    decimal tarifa0 = 0;
    decimal otros = 0;
    decimal totaliva = 0;
    decimal totaldoc = 0;
    decimal fuente = 0;
    decimal iva = 0;
    decimal totalretenido = 0;
    decimal apagar = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        certificado();
    }


    protected void certificado() 
    {
        DateTime Fecha = DateTime.Now;
        string usuario = Convert.ToString(Session["UsuarioID"]);
        string  sucursal = Convert.ToString(Session["pSucursal"]);
        string factura = Convert.ToString(Session["pFactura"]);
        string cedula = Convert.ToString(Session["pCedula"]);

     var cSicoPractico = from vSico in de.tbl_SicoPractico
                            where vSico.sucursal == sucursal
                                    && vSico.factura == factura
                                    && vSico.cedula == cedula
                          select new
                          {
                              id_SicoPractico = vSico.id_SicoPractico
                                ,sucursal = vSico.sucursal
                                ,numero = vSico.numero
                                ,tipoDocumento = vSico.tipoDocumento
                                ,cedula = vSico.cedula
                                ,factura = vSico.factura
                                ,fecha = vSico.fecha
                                ,fechaSicotecnico = vSico.fechaSicotecnico
                                ,fechaPractico = vSico.fechaPractico
                                ,notaPractico = vSico.notaPractico
                                ,estadoPsico = vSico.estadoPsico
                                ,instructorEvaluador = vSico.instructorEvaluador
                                ,resultado = vSico.resultado
                                ,elaborado = vSico.elaborado
                                ,iniciales = vSico.iniciales
                                ,oficio = vSico.oficio
                                ,observacion = vSico.observacion
                                ,directorEscuela = vSico.directorEscuela
                                ,responsablePractico = vSico.responsablePractico
                                ,responsablePsico = vSico.responsablePsico
                          };

        if (cSicoPractico.Count() == 0)
        {
            lblCuerpo.Text = "No existe datos";
        }
        else
        {
            foreach (var registro in cSicoPractico)
            {
                lblSubtitulo.Text = "No. de Convenio de Autorización " + traeConvenio(sucursal)+"-2019";
                lblCuerpo.Text = "La Escuela de Conducción " + traeSucursal(sucursal) + ", " + "certifica que la Sra./Sr. " + traeNombresCliente(registro.cedula.Trim())
                    + " con " + traeTipoDoc(registro.tipoDocumento) + " No. " + registro.cedula + ", ha " + traeResultado(registro.resultado)
                    + " los exámenes Psicosensométrico y prácticos como requisito previo a la obtención de la licencia de conducir tipo B.";

                DateTime fecha = Convert.ToDateTime(registro.fecha);

                string fechaLarga = fecha.ToLongDateString();

                lblFecha.Text = traeCiudad(sucursal) + "," + fechaLarga.Split(',').Last();

                lblDirector.Text = registro.directorEscuela;
                lblPractico.Text = registro.responsablePractico;
                lblPsico.Text = registro.responsablePsico;

                lblOficio.Text = Convert.ToString(registro.numero);
                lblFactura.Text = registro.factura ;
                lblIniciales.Text =  registro.iniciales;


                string clave = registro.id_SicoPractico + "VALIDACION CERTIFICADO EVALUACION PSICOTECNICA PRACTICA"+"\n"
                        +" NOMBRE: "+traeNombresCliente(registro.cedula.Trim())+" CEDULA: "+registro.cedula.Trim()+ " SUCURSAL: "+ traeSucursal(sucursal)
                        + " FACTURA: " + registro.factura.Trim() + " FECHA: " + traeCiudad(sucursal) + "," + fecha.ToLongDateString()
                        + " ESTADO: " + traeResultado(registro.resultado);
                
                generaQR(clave);

            }
        }




    }

    
    protected string traeSucursal(string lsuc)
    {
        string lSucursal;

        lSucursal = "";

        var consultaSuc = from Suc in dc.tbl_ruc
                          where Suc.sucursal == lsuc
                          select new
                          {
                              nom_suc = Suc.nom_suc
                          };

        if (consultaSuc.Count() == 0)
        {
            lSucursal = "Sucursal sin descripción";
        }
        else
        {
            foreach (var registro in consultaSuc)
            {
                lSucursal = registro.nom_suc;
            }
        }

        return lSucursal;

    }

    protected string traeCiudad(string lsuc)
    {
        string ciudad = string.Empty;

        var consultaSuc = from Suc in dc.tbl_ruc
                          where Suc.sucursal == lsuc
                          select new
                          {
                              ciu_suc = Suc.ciudad
                          };

        if (consultaSuc.Count() == 0)
        {
            ciudad = "Sucursal sin ciudad";
        }
        else
        {
            foreach (var registro in consultaSuc)
            {
                ciudad= registro.ciu_suc;
            }
        }

        return ciudad;

    }


    protected string traeConvenio(string lsuc)
    {
        string convenio = string.Empty;

        var consultaSuc = from Suc in dc.tbl_ruc
                          where Suc.sucursal == lsuc
                          select new
                          {
                              convenio = Suc.convenio
                          };

        if (consultaSuc.Count() == 0)
        {
            convenio = "Sucursal sin convenio";
        }
        else
        {
            foreach (var registro in consultaSuc)
            {
                convenio = registro.convenio;
            }
        }

        return convenio;

    }

    protected string traeNombresCliente(string cedula)
    {
        string nombres = string.Empty;

        
        var cNombres = from vNombres in df.CLIENTE
                       where vNombres.CLI_RUC == cedula
                          select new
                          {
                              nombres = vNombres.CLI_APELLIDOP.Trim()+" "+vNombres.CLI_APELLIDOM.Trim()+" "+vNombres.CLI_NOMBRE.Trim()
                          };

        if (cNombres.Count() == 0)
        {
            nombres = "Nombres sin descripción";
        }
        else
        {
            foreach (var registro in cNombres)
            {
                nombres = registro.nombres;
            }
        }

        return nombres;

    }


    protected string traeTipoDoc(string tipo)
    {
        string documento = string.Empty;
       switch (tipo)
       {
           case "1":
               documento = "cédula de ciudadanía";
               break;

           case "2":
                   documento = "cédula de identidad";
               break;
                                    
           case "3":
                   documento = "cédula extranjero visado por 2 años";
               break;
           case "4":
                  documento = "refugiado";
               break;
           case "5":
                documento = "pasaporte";
               break;
                                    
           case "6":
               documento = "RUC";
               break;
           case "7":
               documento = "cédula extranjero visado por 18 meses";
               break;
           case "8":
               documento = "extranjero visado por 6 meses";
               break;
           case "9":
               documento = "extranjero visado por 1 año";
               break;
           default:
               documento = "sin datos";
               break;

       }

        

        return documento;

    }


    protected string traeResultado(string estado)
    {
        string resultado = string.Empty;
        switch (estado)
        {
            case "1":
                resultado = "APROBADO";
                break;

            case "2":
                resultado = "REPROBADO";
                break;

            case "3":
                resultado = "ESPERA";
                break;
           
            default:
                resultado = "sin datos";
                break;

        }



        return resultado;

    }


    protected void generaQR(string clave)
    {
        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap img = encoder.Encode(clave.Trim());
        System.Drawing.Image QR = (System.Drawing.Image)img;

        using (MemoryStream ms = new MemoryStream()) 
        {
            QR.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageBytes = ms.ToArray();
            imgCtrl.Src = "data:image/gif;base64," + Convert.ToBase64String(imageBytes);
            imgCtrl.Height = 150;
            imgCtrl.Width = 150;
        }
    }
}