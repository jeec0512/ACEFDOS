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
using acefdos;

public partial class Contabilidad_mayorizar : System.Web.UI.Page
{
    #region VARIABLES GENERALES
    decimal debe = 0;
    decimal haber = 0;
    decimal saldo = 0;
    #endregion
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();


    string conn2 = System.Configuration.ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;

    Data_DB_ESCUELADataContext ds = new Data_DB_ESCUELADataContext();

    string conn3 = System.Configuration.ConfigurationManager.ConnectionStrings["AdmBitaAutoConnectionString"].ConnectionString;
    Data_AdmBitaAutoDataContext da = new Data_AdmBitaAutoDataContext();

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["AWA_ACCOUNTINGConnectionString"].ConnectionString;
    Data_AWA_ACCOUTINGDataContext dw = new Data_AWA_ACCOUTINGDataContext();

    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensaje.Text = "";
            string accion = string.Empty;
            perfilUsuario();


           
            listarAnos();
            listarPeriodos();
         

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
                Response.Redirect("~/inicio.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];



            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/inicio.aspx");
            }

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal.DataSource = cSucursal;
            ddlSucursal.DataBind();
        }
        catch (InvalidCastException e)
        {
            Response.Redirect("~/inicio.aspx");
            lblMensaje.Text = e.Message;
        }
    }

    #endregion
   

    #region LISTAR GRIDS
    

    protected void listarAnos()
    {
        var cAno = dc.sp_ListarAnos("NOBLOQUEO", "N");
        ddlAno.DataSource = cAno;
        ddlAno.DataBind();

        ListItem listMod = new ListItem("Seleccione el año", "-1");
        ddlAno.Items.Insert(0, listMod);
    }
    protected void listarPeriodos()
    {
        var cPeriodo = dc.sp_ListarPeriodos("NOBLOQUEO", "N");
        ddlPeriodo.DataSource = cPeriodo;
        ddlPeriodo.DataBind();

        ListItem listMod = new ListItem("Seleccione el período", "-1");
        ddlPeriodo.Items.Insert(0, listMod);

    }
    
    #endregion

    /**/


   

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Boolean pasa = true;
        string accion = "GUARDAR";

       
        int usuarioIdCrea = Convert.ToInt32(Session["SUsuarioID"]);
        int usuarioIdMod = Convert.ToInt32(Session["SUsuarioID"]);
        DateTime fechaCrea = DateTime.Now;
        DateTime fechaMod = DateTime.Now;

        
        int ano = Convert.ToInt32(ddlAno.SelectedValue);
        int periodo = Convert.ToInt32(ddlPeriodo.SelectedValue);


        if (ano == -1)
        {
            pasa = false;
        }

        if (periodo == -1)
        {
            pasa = false;
        }

        
        else
        {
            lblMensaje.Text = "Ingrese año y período";
        }

    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        btnCancelar_Click();


    }

    protected void btnCancelar_Click()
    {
        divModificaRegistros.Visible = false;
        pnAno.Enabled = true;
        pnPeriodo.Enabled = true;
        divBotones.Visible = true;
        lblMensaje.Text = "";
        pnAutos.Enabled = true;
        


    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        btnNuevo_Click();
        //activarBoton();
    }

    /*protected void activarBoton() {
        string script = @"<script type='text/javascript'>
                            activarBoton();
                        </script>";

        Page.RegisterStartupScript("activarBoton", script);
    }*/

    protected void btnNuevo_Click()
    {
      /*  divModificaRegistros.Visible = true;
        pnAno.Enabled = false;
        pnPeriodo.Enabled = false;
        divBotones.Visible = false;
        pnAutos.Enabled = false;*/
        pnAutos.Visible = true;
        string lAccion;
        int ano, periodo;
        
//        string script = @"<script type='text/javascript'>
//                            desactivarBoton();
//                        </script>";
        
//        Page.RegisterStartupScript("desactivarBoton", script);

        

        dw.CommandTimeout = 360;
        object cMayores = new object();

        lAccion = "CONSULTAR";
        ano = Convert.ToInt32(ddlAno.SelectedValue);
        periodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

       // var cMayores = dc.sp_MayorizarxPeriodo(lAccion, ano, periodo);
      //  cMayores = dc.sp_MayorizacionCalculo(lAccion, ano, periodo,"","");

        cMayores = dw.usp_MayorizacionCalculo_V5(ano, periodo);
        grvMayores.DataSource = cMayores;
        grvMayores.DataBind();



    }


    protected void btnModifica_Click(object sender, EventArgs e)
    {
        divModificaRegistros.Visible = true;
    }
    protected void btnRegresa_Click(object sender, EventArgs e)
    {

    }
    
    protected void ibConsultar_Click()
    {
        
    }

    #region AEXCEL

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
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
            grvMayores.AllowPaging = false;
            /// this.BindGrid();

            grvMayores.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvMayores.HeaderRow.Cells)
            {
                cell.BackColor = grvMayores.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvMayores.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvMayores.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvMayores.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvMayores.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion
    protected void grvMayores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[3].Text);


            debe += Convert.ToDecimal(e.Row.Cells[2].Text);
            haber += Convert.ToDecimal(e.Row.Cells[3].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Totales";

            e.Row.Cells[2].Text = Convert.ToString(debe);
            e.Row.Cells[3].Text = Convert.ToString(haber);
            e.Row.Cells[4].Text = Convert.ToString(debe - haber);


        }
    }
    
}