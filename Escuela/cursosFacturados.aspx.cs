using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Escuela_cursosFacturados : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

        }
    }
    #endregion

    #region PROCESOS INTERNOS

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

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();

            listarCurso();
           /* var consultaCur = from mcur in df.CURSO
                          orderby mcur.CUR_ID descending
                          select mcur.CUR_DESCRIPCION;*/


           
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }

    protected void listarCurso() 
    {
        var cCurso = df.sp_listarCursos();

        ddlCursos.DataSource = cCurso;
        ddlCursos.DataBind();
    }
    protected void activarObjetos()
    {
        pnTitulos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    
    #endregion

    protected decimal codigoCurso(string mcurso)
    {
        decimal lcurid;
        lcurid = 0;
        var consultaCur = from mcur in df.CURSO
                          where mcur.CUR_DESCRIPCION == mcurso
                          select mcur;


        if (consultaCur.Count() == 0)
        {

            lcurid = 0;
        }
        else
        {
            foreach (var registro in consultaCur)
            {
                lcurid = registro.CUR_ID;
            }
        }


        return lcurid;
    }

    protected void listaReportes(string ltipo, string laccion1, string laccion2)
    {
        decimal lcurso;
        string ltipoSuc, lsucursal, mcurso;
        DateTime lfechaInicio, lfechaFin;
        int tipo = (int)Session["STipo"];

        ltipoSuc = "";
        lsucursal = ddlSucursal2.SelectedValue.Trim();
        mcurso = ddlCursos.SelectedValue.Trim();
        lcurso = codigoCurso(mcurso);
        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;

        if (ltipo == "1")
        {
            if (tipo == 4)
            {
                laccion1 = "DETALLE";
            }
            else
            {
                laccion1 = "DETSUC";
            }

            //grvEscuela.DataSource = dc.sp_RepCursosFcaturados(laccion1, ltipoSuc, lsucursal, lcurso, lfechaInicio, lfechaFin);
            grvEscuela.DataSource = dc.sp_RepCursosFcaturados(laccion1, ltipoSuc, lsucursal, Convert.ToInt32(mcurso), lfechaInicio, lfechaFin);
            grvEscuela.DataBind();
        }

        if (ltipo == "2")
        {
            grvEscuela.DataSource = dc.sp_RepCursosFcaturados(laccion1, ltipoSuc, lsucursal, Convert.ToInt32(mcurso), lfechaInicio, lfechaFin);
            grvEscuela.DataBind();

        }
    }

    protected void btnEscTotal_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        pnEscuela.GroupingText = "Cursos ANETA";

        ltipo = "1";
        laccion1 = "DETALLE";
        laccion2 = "TOTAL";

        listaReportes(ltipo, laccion1, laccion2);
        pnEscuela.Visible = true;

        btnExcelCA.Visible = true;

    }

    protected void btnEscxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        pnEscuela.GroupingText = "Cursos por sucursal";

        ltipo = "2";
        laccion1 = "DETSUC";
        laccion2 = "TOTSUC";

        listaReportes(ltipo, laccion1, laccion2);
        pnEscuela.Visible = true;

        btnExcelCA.Visible = true;
    }

    #region EXCEL
    protected void btnExcelCA_Click(object sender, EventArgs e)
    {
        uno();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void uno()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grvEscuela.AllowPaging = false;
            /// this.BindGrid();

            grvEscuela.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvEscuela.HeaderRow.Cells)
            {
                cell.BackColor = grvEscuela.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvEscuela.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvEscuela.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvEscuela.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvEscuela.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion
}