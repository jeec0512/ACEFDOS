using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Escuela_verImagen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgGrande.ImageUrl = Request.QueryString["ImageURL"];
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        string formulario = Convert.ToString(Session["pFormulario"]);
        string camino = "~/Escuela/" + formulario;
        Response.Redirect(camino);
    }
};