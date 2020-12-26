using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Drawing;
using MessagingToolkit.QRCode.Codec;

public partial class Escuela_generarCodigoQR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        QRCodeEncoder encoder = new QRCodeEncoder();
        Bitmap img = encoder.Encode(txtCode.Text.Trim());
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