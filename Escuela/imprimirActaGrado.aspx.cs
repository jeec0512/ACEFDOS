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

public partial class Escuela_imprimirActaGrado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string suc = Convert.ToString(Session["psuc"])  ;
        string Ciudad = Convert.ToString(Session["pCiudad"])  ;
        string Provincia = Convert.ToString(Session["pProvincia"])  ;
        string Dias = Convert.ToString(Session["pDias"])  ;
        string Mes = Convert.ToString(Session["pMes"])  ;
        string Ano = Convert.ToString(Session["pAno"])  ;
        string DirectorEscuela1 = Convert.ToString(Session["pDirectorEscuela1"])  ;
        string Escuela = Convert.ToString(Session["pEscuela"])  ;
        string Supervistring = Convert.ToString(Session["pSupervisor"])  ;
        string SecretariaAcademica1 = Convert.ToString(Session["pSecretariaAcademica1"])  ;
        string Estudiante = Convert.ToString(Session["pEstudiante"])  ;
        string NotaTeoria = Convert.ToString(Session["pNotaTeoria"])  ;
        string Equivalenteteoria = Convert.ToString(Session["pEquivalenteteoria"])  ;
        string NotaPractica = Convert.ToString(Session["pNotaPractica"])  ;
        string EquivalentePractica = Convert.ToString(Session["pEquivalentePractica"])  ;
        string Titulo = Convert.ToString(Session["pTitulo"])  ;
        string Curso = Convert.ToString(Session["pCurso"])  ;
        string SupervisorTeoria = Convert.ToString(Session["pSupervisorTeoria"])  ;
        string SupervisorPractica = Convert.ToString(Session["pSupervisorPractica"])  ;
        string DirectorEscuela2 = Convert.ToString(Session["pDirectorEscuela2"])  ;
        string SecretariaAcademica2 = Convert.ToString(Session["pSecretariaAcademica2"])  ;
        string factura = Convert.ToString(Session["pfactura"])  ;
        string cedula = Convert.ToString(Session["pcedula"])  ;





        spanCiudad.InnerText = Ciudad;
        spanProvincia.InnerText = Provincia;
        spanDias.InnerText = Dias;
        spanMes.InnerText = Mes;
        spanAno.InnerText = Ano;
        spanDirectorEscuela1.InnerText = DirectorEscuela1;
        spanEscuela.InnerText = Escuela;
        spanSupervisor.InnerText = SupervisorPractica;
        spanSecretariaAcademica1.InnerText = SecretariaAcademica1;
        spanEstudiante.InnerText = Estudiante;
        spanNotaTeoria.InnerText = NotaTeoria;
        spanEquivalenteteoria.InnerText = Equivalenteteoria;
        spanNotaPractica.InnerText = NotaPractica;
        spanEquivalentePractica.InnerText = EquivalentePractica;
        spanTitulo.InnerText = Titulo;
        spanCurso.InnerText = Curso;
        spanSupervisorTeoria.InnerText = SupervisorTeoria;
        spanSupervisorPractica.InnerText = SupervisorPractica;
        spanDirectorEscuela2.InnerText = DirectorEscuela2;
        spanSecretariaAcademica2.InnerText = SecretariaAcademica2;
        string clave = "md" + cedula + factura;
                
                generaQR(clave);

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
            imgCtrl.Height = 80;
            imgCtrl.Width = 80;
        }
    }
}