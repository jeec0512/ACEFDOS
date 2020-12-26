using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;






namespace enviarEmail
{
    public class email
    {
        MailMessage m = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        char[] delimitador_cc = { ',' };
        char[] delimitador_adjunto = {'|'};

        public email()
           
        {
            
        }

        public bool enviarCorreo(string from, string password, string to, string mensaje, string subject)
        {
            try
            {

                m.From = new MailAddress(from);
                m.To.Add(new MailAddress(to));
                m.Body = mensaje;
                m.Subject = subject;
                m.Priority = MailPriority.Normal;
                m.IsBodyHtml = false;

                // smtp.Host = "smtp.gmail.com";
                smtp.Host = "192.168.1.110";
                smtp.Port = 25;
                //smtp.Credentials = new NetworkCredential(from, password);
                //smtp.EnableSsl = true;
                smtp.Send(m);


                // m.IsBodyHtml = true;

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public void enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo)
        {
            try
            {
            /************** ENVIO DE IMAGENES CON CID***************************/
               

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(cuerpo,
                             Encoding.UTF8,
                             MediaTypeNames.Text.Html);

                //http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackFront2_147.png

                LinkedResource img =
                        new LinkedResource(@"http://190.63.17.119:5090/acefdos/images/socios/black/premiumBlackFront1_387.jpg",
                             MediaTypeNames.Image.Jpeg);
                            img.ContentId = "imgFront1";
                // Lo incrustamos en la vista HTML...

                 htmlView.LinkedResources.Add(img);


                SmtpClient cliente = new SmtpClient(host,puerto);
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(remitente, nombre);
                correo.Body = cuerpo;
                correo.Subject = asunto;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;


               



                if (destinatarios == "")
                {
                    
                }else{
                    string[] cadena = destinatarios.Split(delimitador_cc);
                    foreach (string word in cadena) correo.To.Add(word.Trim());
                }

                if (cc == "") { }
                else
                {
                    string[] cadena1 = cc.Split(delimitador_cc);
                    foreach (string word in cadena1) correo.CC.Add(word.Trim());
                }

                if (adjuntos == "") { }
                else
                {
                    string[] cadena2 = adjuntos.Split(delimitador_adjunto);
                    foreach (string word in cadena2) correo.Attachments.Add(new Attachment(word));
                }



                cliente.Credentials = new NetworkCredential(remitente, contraseña);
                cliente.EnableSsl = false;
                cliente.Send(correo);

               // MessageBox.Show("El correo se ha enviado correctamente");
                
            }
            catch (Exception ex)
            {
               
            }
        }
      

        /*****************/
        public bool enviarCorreo(string host, int puerto, string remitente, string contraseña, string nombre, string destinatarios, string cc, string asunto, string adjuntos, string cuerpo, string front1, string front2, string front3, string back1)
        {

            try
            {
                /************** ENVIO DE IMAGENES CON CID***************************/


                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(cuerpo, Encoding.UTF8, MediaTypeNames.Text.Html);

                // LinkedResource img = new LinkedResource(@"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg",MediaTypeNames.Image.Jpeg);

                LinkedResource img = new LinkedResource(front1, MediaTypeNames.Image.Jpeg);

                //"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg"

                img.ContentId = "imgFront1";

                // Lo incrustamos en la vista HTML...

                htmlView.LinkedResources.Add(img);


                // LinkedResource img = new LinkedResource(@"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg",MediaTypeNames.Image.Jpeg);

                LinkedResource img2 = new LinkedResource(front2, MediaTypeNames.Image.Jpeg);

                //"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg"

                img2.ContentId = "imgFront2";

                // Lo incrustamos en la vista HTML...

                htmlView.LinkedResources.Add(img2);


                // LinkedResource img = new LinkedResource(@"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg",MediaTypeNames.Image.Jpeg);

                LinkedResource img3 = new LinkedResource(front3, MediaTypeNames.Image.Jpeg);

                //"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg"

                img3.ContentId = "imgFront3";

                // Lo incrustamos en la vista HTML...

                htmlView.LinkedResources.Add(img3);


                // LinkedResource img = new LinkedResource(@"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg",MediaTypeNames.Image.Jpeg);

                LinkedResource img4 = new LinkedResource(back1, MediaTypeNames.Image.Jpeg);

                //"C:\inetpub\wwwroot\acefdos\Images\socios\black\premiumBlackFront1_387.jpg"

                img4.ContentId = "imgBack1";

                // Lo incrustamos en la vista HTML...

                htmlView.LinkedResources.Add(img4);

                // Por último, vinculamos ambas vistas al mensaje...

                // mail.AlternateViews.Add(plainView);



                SmtpClient cliente = new SmtpClient(host, puerto);
                MailMessage correo = new MailMessage();

                correo.AlternateViews.Add(htmlView);
                correo.From = new MailAddress(remitente, nombre);
                correo.Body = cuerpo;
                correo.Subject = asunto;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
  
                if (destinatarios == "")
                {

                }
                else
                {
                    string[] cadena = destinatarios.Split(delimitador_cc);
                    foreach (string word in cadena) correo.To.Add(word.Trim());
                }

                if (cc == "") { }
                else
                {
                    string[] cadena1 = cc.Split(delimitador_cc);
                    foreach (string word in cadena1) correo.CC.Add(word.Trim());
                }

                if (adjuntos == "") { }
                else
                {
                    string[] cadena2 = adjuntos.Split(delimitador_adjunto);
                    foreach (string word in cadena2) correo.Attachments.Add(new Attachment(word));
                }


                //cliente.Host= "smtp.gmail.com";
               
               // cliente.UseDefaultCredentials = false; // para uso de gmail
                
                cliente.UseDefaultCredentials = true; 
               // cliente.Credentials = new NetworkCredential(remitente, contraseña);
               // cliente.EnableSsl = true;
                cliente.Send(correo);

                // MessageBox.Show("El correo se ha enviado correctamente");
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}