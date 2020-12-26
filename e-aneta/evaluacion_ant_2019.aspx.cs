using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Escuela_evaluacion_ant_2019 : System.Web.UI.Page
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
        string sucursal = (string)Session["SSucursal"]; //ddlSucursal.SelectedValue;

        Session["pCamino"] = "~/admArchivos/evaluacion_ant_2019/";
        Session["pFormulario"] = "evaluacion_ant_2019.aspx";

        Session["pSucursal"] = sucursal;

        string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";

        //ddlSucursal.SelectedValue = sucursal;
        //listarArchivos(camino);
        //btnVerArchivos_Click();


        if (!IsPostBack)
        {

            string accion = string.Empty;
            perfilUsuario();

            ddlSucursal.SelectedValue =  Convert.ToString(Session["pSucursal"]);

            btnVerArchivos_Click();

            /*ejemplo de pdf*/

            //IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            // Render an HTML document or snippet as a string
            // Renderer.RenderHtmlAsPdf("<h1>Hello World</h1>").SaveAs("~/html-string.pdf");

            // Render any HTML fragment or document to HTML
            //var Renderer = new IronPdf.HtmlToPdf();
            /* var PDF = Renderer.RenderHtmlAsPdf("<h1>Hello IronPdf</h1>");
             var OutputPath = "HtmlToPDF.pdf";
             PDF.SaveAs(OutputPath);
             // This neat trick opens our PDF file so we can see the result in our default PDF viewer
             System.Diagnostics.Process.Start(OutputPath);*/

            /* var HtmlTemplate = "<p>[[NAME]]</p>";
             var Names = new[] { "John", "James", "Jenny" };



             foreach (var name in Names)
             {
                 var HtmlInstance = HtmlTemplate.Replace("[[NAME]]", name);
                 var PDF = Renderer.RenderHtmlAsPdf(HtmlInstance);
                 PDF.SaveAs(name + ".pdf");
                 System.Diagnostics.Process.Start(name + ".pdf");
             }*/
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



    protected void btnCargar_Click(object sender, EventArgs e)
    {
        string sucursal = ddlSucursal.SelectedValue;
        //string camino = "~/admArchivos/evaluacion_ant_2019/" + sucursal.ToLower() + "/";
        string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";

        string formulario = Convert.ToString(Session["pFormulario"]);

        Session["pSucursal"] = sucursal;

        if (ulCarga.HasFile)
        {
            ulCarga.PostedFile.SaveAs(Server.MapPath(camino) + ulCarga.FileName);
        }

        Response.Redirect(formulario);

        
    }

    protected void listarArchivos(string camino)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("archivo", typeof(string));
        dt.Columns.Add("dimension", typeof(string));
        dt.Columns.Add("tipo", typeof(string));

        upLoadImage(camino, dt);

        grvArchivos.DataSource = dt;
        grvArchivos.DataBind();
    }

    private void upLoadImage(string camino, DataTable dt)
    {
       // pnImagenes.Controls.Clear();
        foreach (string srtFile in Directory.GetFiles(Server.MapPath(camino)))
        {
            FileInfo fi = new FileInfo(srtFile);

            dt.Rows.Add(fi.Name, fi.Length, getFileTypeByExtension(fi.Extension));

            if (fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".png")
            {
                ImageButton imagenBoton = new ImageButton();

                imagenBoton.ImageUrl = camino + fi.Name;
                imagenBoton.Width = Unit.Pixel(100);
                imagenBoton.Height = Unit.Pixel(100);
                imagenBoton.Style.Add("padding", "5px");
                imagenBoton.Click += new ImageClickEventHandler(imagenBoton_Click);

                pnImagenes.Controls.Add(imagenBoton);
            }

        }




    }


    void imagenBoton_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Escuela/verImagen.aspx?ImageURL=" + ((ImageButton)sender).ImageUrl);
    }

    private string getFileTypeByExtension(string extension)
    {
        switch (extension.ToLower())
        {
            case ".doc":
            case ".docx":
                return "Microsoft Word Document";

            case ".xls":
            case ".xlsx":
                return "Microsoft Excel Document";

            case ".txt":
                return "Documento texto";

            case ".pdf":
                return "Documento PDF";

            case ".jpg":
            case ".ico":
            case ".png":
                return "Imagen";

            default:
                return "Archivo no catalogado";


        }
    }

    protected void grvArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string sucursal = (string)Session["pSucursal"];//ddlSucursal.SelectedValue;
        string sucursal = ddlSucursal.SelectedValue;
        //string camino = "~/admArchivos/evaluacion_ant_2019/" + sucursal.ToLower() + "/";
        string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";

        if (e.CommandName == "Download")
        {
            Response.Clear();
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
            Response.TransmitFile(Server.MapPath(camino) + e.CommandArgument);
            Response.End();
        }
    }
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string sucursal = (string)Session["SSucursal"];//ddlSucursal.SelectedValue;

        string sucursal = ddlSucursal.SelectedValue;
        //string camino = "~/admArchivos/evaluacion_ant_2019/" + sucursal.ToLower() + "/";
        string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";

        //listarArchivos(camino);

        btnVerArchivos_Click();
    }
    protected void btnVerArchivos_Click(object sender, EventArgs e)
    {
        btnVerArchivos_Click();
    }

    protected void btnVerArchivos_Click()
    {
        string sucursal = ddlSucursal.SelectedValue;

        Session["pCamino"] = "~/admArchivos/evaluacion_ant_2019/";
        Session["pFormulario"] = "evaluacion_ant_2019.aspx";

        string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";
        ddlSucursal.SelectedValue = sucursal;
        listarArchivos(camino);
    }

}