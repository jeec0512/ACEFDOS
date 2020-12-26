using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhatsAppApi;

public partial class Escuela_mensajeWhatsApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        WhatsApp wp = new WhatsApp(txtTelefono.Text,txtContrasena.Text,txtNombre.Text,true);
       /* wp.OnConnectSuccess += () =>
        {
            txtEstatus.Text = "Conectando ...";
            wp.OnConnectSuccess += (phone, data) =>
            {
                txtEstatus.Text += "\r\nConnection sucess !";
                wp.SendMessage(txtA.Text, txtMemsaje.Text);
                txtEstatus.Text += "\r\Mensaje enviado !";

            };

            wp.OnLoginFailed += (data) =>
                {
                    txtEstatus.Text += string.Format("\r\nLogin failed {0}",data);
                };
            wp.Login();
        };

            wp.OnConnectFailed += (ex) =>
            {
                txtEstatus.Text += string.Format("\r\nLogin failed {0}",ex.StackTrace);
            };

        wp.Connect();*/
    }
}