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

public partial class Socio_sociosInactivos : System.Web.UI.Page
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

            DateTime lfecha = DateTime.Today;
            txtFechaIni.Text = Convert.ToString(lfecha);
            txtFechaFin.Text = Convert.ToString(lfecha);

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }

    protected void activarObjetos()
    {
        pnTitulos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    #endregion

    protected void btnTodos_Click(object sender, EventArgs e)
    {
        pnConsolidado.Visible = true;
        btnTodos_Click();
    }

    protected void btnTodos_Click()
    {

        cargaGridTodos();

    }

    private void cargaGridTodos()
    {
        string accion = "SUC";
        DateTime lfechainicio, lfechafin;
        string lsucursal;


        lsucursal = ddlSucursal2.SelectedValue.Trim();

        lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechafin = Convert.ToDateTime(txtFechaFin.Text);

        grvConsolidado.DataSource = dc.sp_sociosInactivos(accion, lfechainicio, lfechafin, lsucursal,"STA");
        grvConsolidado.DataBind();

    }



    #region A EXCEL
    protected void btnExcelC_Click(object sender, EventArgs e)
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
            grvConsolidado.AllowPaging = false;
            ///this.retornaTodos();

            grvConsolidado.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvConsolidado.HeaderRow.Cells)
            {
                cell.BackColor = grvConsolidado.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvConsolidado.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvConsolidado.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvConsolidado.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvConsolidado.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    #endregion

    #region PAGINACION

    public class Socios
    {
        public string CLI_ID { get; set; }
        public string CLI_RUC { get; set; }
        public string CLI_NOMBRES { get; set; }
        public string CLI_EMAIL { get; set; }
        public string CLI_OBLIGADO { get; set; }
        public string CLI_DIRECCION { get; set; }
        public string CLI_SECTOR { get; set; }
        public string CLI_TELEFONO { get; set; }
        public string CLI_CELULAR { get; set; }
        public string CLI_TIPOSANGRE { get; set; }
        public string CLI_NACIONALIDAD { get; set; }
        public string CLI_ESTADOCIVIL { get; set; }
        public string CLI_FECHANACIMIENTO { get; set; }
        public string CLI_ENCUESTA { get; set; }
        public string CLI_GENERO { get; set; }
    }



    private List<Socios> retornaTodos()
    {
        var Socios = new Socios();

        List<Socios> lista = new List<Socios>();

        string accion = "TODOS";
        DateTime lfechainicio, lfechafin;
        string usuario, Username, lsucursal;
       // int ltipoConcepto = 0;

        usuario = Convert.ToString(Session["UsuarioID"]);
        Username = Convert.ToString(Session["Usuarioname"]);

        //lsucursal = Username.Substring(0, 3);

        lsucursal = ddlSucursal2.SelectedValue.Trim();

        lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechafin = Convert.ToDateTime(txtFechaFin.Text);

        var cSocios = dc.sp_sociosInactivos(accion, lfechainicio, lfechafin, lsucursal, "STA");

        
        int kont = 0;
        
            foreach (var registro in cSocios)
            {

                Socios.CLI_ID = Convert.ToString(registro.CLI_ID);
                Socios.CLI_RUC = registro.CLI_RUC;
                Socios.CLI_NOMBRES = registro.CLI_NOMBRES;
                Socios.CLI_EMAIL = registro.CLI_EMAIL;
                Socios.CLI_OBLIGADO = registro.CLI_OBLIGADO;
                Socios.CLI_DIRECCION = registro.CLI_DIRECCION;
                Socios.CLI_SECTOR = registro.CLI_SECTOR;
                Socios.CLI_TELEFONO = registro.CLI_TELEFONO;
                Socios.CLI_CELULAR = registro.CLI_CELULAR;
                Socios.CLI_TIPOSANGRE = registro.CLI_TIPOSANGRE;
                Socios.CLI_NACIONALIDAD = registro.CLI_NACIONALIDAD;
                Socios.CLI_ESTADOCIVIL = registro.CLI_ESTADOCIVIL;
                Socios.CLI_FECHANACIMIENTO = Convert.ToString(registro.CLI_FECHANACIMIENTO);
                Socios.CLI_ENCUESTA = registro.CLI_ENCUESTA;
                Socios.CLI_GENERO = registro.CLI_GENERO;

                lista.Add(new Socios()
                {

                    CLI_ID = Socios.CLI_ID,
                    CLI_RUC = Socios.CLI_RUC,
                    CLI_NOMBRES = Socios.CLI_NOMBRES,
                    CLI_EMAIL = Socios.CLI_EMAIL,
                    CLI_OBLIGADO = Socios.CLI_OBLIGADO,
                    CLI_DIRECCION = Socios.CLI_DIRECCION,
                    CLI_SECTOR = Socios.CLI_SECTOR,
                    CLI_TELEFONO = Socios.CLI_TELEFONO,
                    CLI_CELULAR = Socios.CLI_CELULAR,
                    CLI_TIPOSANGRE = Socios.CLI_TIPOSANGRE,
                    CLI_NACIONALIDAD = Socios.CLI_NACIONALIDAD,
                    CLI_ESTADOCIVIL = Socios.CLI_ESTADOCIVIL,
                    CLI_FECHANACIMIENTO = Socios.CLI_FECHANACIMIENTO,
                    CLI_ENCUESTA = Socios.CLI_ENCUESTA,
                    CLI_GENERO = Socios.CLI_GENERO


                });
                kont = kont + 1;
            }



        return lista;
    }



    public class classSocio
    {
        public string CLI_ID { get; set; }
        public string CLI_RUC { get; set; }
        public string CLI_NOMBRES { get; set; }
        public string CLI_EMAIL { get; set; }
        public string CLI_OBLIGADO { get; set; }
        public string CLI_DIRECCION { get; set; }
        public string CLI_SECTOR { get; set; }
        public string CLI_TELEFONO { get; set; }
        public string CLI_CELULAR { get; set; }
        public string CLI_TIPOSANGRE { get; set; }
        public string CLI_NACIONALIDAD { get; set; }
        public string CLI_ESTADOCIVIL { get; set; }
        public string CLI_FECHANACIMIENTO { get; set; }
        public string CLI_ENCUESTA { get; set; }
        public string CLI_GENERO { get; set; }




    }

    private List< classSocio> retornaLista()
    {
        string lsucursal = ddlSucursal2.SelectedValue;
        DateTime lfechainicio = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lfechafin = Convert.ToDateTime(txtFechaFin.Text);
        int tipo = (int)Session["STipo"];

        var classSocio = new classSocio();

        List<classSocio> lista = new List<classSocio>();

        var cFac = retornaTodos();



        foreach (var registro in cFac)
        {
            
           classSocio.CLI_ID = registro.CLI_ID;
            classSocio.CLI_RUC = registro.CLI_RUC;
            classSocio.CLI_NOMBRES = registro.CLI_NOMBRES;
            classSocio.CLI_EMAIL = registro.CLI_EMAIL;
            classSocio.CLI_OBLIGADO = registro.CLI_OBLIGADO;
            classSocio.CLI_DIRECCION = registro.CLI_DIRECCION;
            classSocio.CLI_SECTOR = registro.CLI_SECTOR;
            classSocio.CLI_TELEFONO = registro.CLI_TELEFONO;
            classSocio.CLI_CELULAR = registro.CLI_CELULAR;
            classSocio.CLI_TIPOSANGRE = registro.CLI_TIPOSANGRE;
            classSocio.CLI_NACIONALIDAD = registro.CLI_NACIONALIDAD;
            classSocio.CLI_ESTADOCIVIL = registro.CLI_ESTADOCIVIL;
            classSocio.CLI_FECHANACIMIENTO = registro.CLI_FECHANACIMIENTO;
            classSocio.CLI_ENCUESTA = registro.CLI_ENCUESTA;
            classSocio.CLI_GENERO = registro.CLI_GENERO;


            lista.Add(new classSocio()
            {

                
                CLI_ID = classSocio.CLI_ID,
                CLI_RUC = classSocio.CLI_RUC,
                CLI_NOMBRES = classSocio.CLI_NOMBRES,
                CLI_EMAIL = classSocio.CLI_EMAIL,
                CLI_OBLIGADO = classSocio.CLI_OBLIGADO,
                CLI_DIRECCION = classSocio.CLI_DIRECCION,
                CLI_SECTOR = classSocio.CLI_SECTOR,
                CLI_TELEFONO = classSocio.CLI_TELEFONO,
                CLI_CELULAR = classSocio.CLI_CELULAR,
                CLI_TIPOSANGRE = classSocio.CLI_TIPOSANGRE,
                CLI_NACIONALIDAD = classSocio.CLI_NACIONALIDAD,
                CLI_ESTADOCIVIL = classSocio.CLI_ESTADOCIVIL,
                CLI_FECHANACIMIENTO = classSocio.CLI_FECHANACIMIENTO,
                CLI_ENCUESTA = classSocio.CLI_ENCUESTA,
                CLI_GENERO = classSocio.CLI_GENERO

            });

        }
        return lista;
    }

    protected void grvConsolidado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvConsolidado.PageIndex = e.NewPageIndex;
        // if (tipoConsulta == 1)
        // {
        cargaGrid();
        //}

        //if (tipoConsulta == 2)
        //{
        //  cargaGridTodos();
        //}
    }


    protected void grvConsolidado_PageIndexChanged(object sender, EventArgs e)
    {

    }
    
    protected void grvConsolidado_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    

    private void cargaGrid()
    {
        grvConsolidado.DataSource = retornaLista();
        grvConsolidado.DataBind();
    }

    protected void grvConsolidado_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    #endregion
   
}