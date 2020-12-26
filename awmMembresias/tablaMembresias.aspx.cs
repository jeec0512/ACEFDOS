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

public partial class awmMembresias_tablaMembresias : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

    #endregion


    #region variablelGenerales
    decimal tresN = 0;
    decimal tresR = 0;
    decimal standN = 0;
    decimal standR = 0;
    decimal motoN = 0;
    decimal motoR = 0;
    decimal premN = 0;
    decimal premNR = 0;
    decimal prem2N = 0;
    decimal prem2R = 0;
    decimal prem3N = 0;
    decimal prem3R = 0;
    decimal proautoN = 0;
    decimal proautoR = 0;
    decimal taxiN = 0;
    decimal taxiR = 0;
    decimal total = 0;
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

    }

    #endregion

    protected void listaReportes(string ltipo, string laccion1, string laccion2)
    {
        int tipo = (int)Session["STipo"];
        string ltipoSuc, lsucursal;
        DateTime lfechaInicio, lfechaFin;

        ltipoSuc = "";
        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);



        if (ltipo == "1")
        {
            if (tipo == 3 || tipo == 4)
            {
                laccion1 = "TODOS";
                laccion2 = "VENDEDOR";
                //grvSocios.DataSource = dc.sp_membreisasxSucursal(laccion2, lsucursal, lfechaInicio, lfechaFin);
                //grvSocios.DataBind();

                grvTotalSocios.DataSource = dc.sp_membreisasxSucursal(laccion1, lsucursal, lfechaInicio, lfechaFin);
                grvTotalSocios.DataBind();
            }
            else
            {
                laccion1 = "TODOS";
                laccion2 = "VENDEDOR";

                //grvSocios.DataSource = dc.sp_membreisasxSucursal(laccion2, lsucursal, lfechaInicio, lfechaFin);
                //grvSocios.DataBind();

                grvTotalSocios.DataSource = dc.sp_membreisasxSucursal(laccion1, lsucursal, lfechaInicio, lfechaFin);
                grvTotalSocios.DataBind();
            }

        }

        if (ltipo == "2")
        {
            grvSocios.DataSource = dc.sp_membreisasxSucursal(laccion2, lsucursal, lfechaInicio, lfechaFin);

            grvTotalSocios.DataSource = dc.sp_membreisasxSucursal(laccion1, lsucursal, lfechaInicio, lfechaFin);
            grvTotalSocios.DataBind();


        }
    }


    protected void listaReportesVendedor(string ltipo, string laccion1, string laccion2)
    {
        int tipo = (int)Session["STipo"];
        string ltipoSuc, lsucursal;
        DateTime lfechaInicio, lfechaFin;

        ltipoSuc = "";
        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);



        if (ltipo == "1")
        {
            if (tipo == 3 || tipo == 4)
            {
                laccion1 = "TODOS";
                laccion2 = "VENDEDOR";
                //grvSocios.DataSource = dc.sp_membreisasxSucursal(laccion2, lsucursal, lfechaInicio, lfechaFin);
                //grvSocios.DataBind();

                grvVendedorTotal.DataSource = dc.sp_membreisasxVendedor_awm(laccion1, lsucursal, lfechaInicio, lfechaFin);
                grvVendedorTotal.DataBind();
            }
            else
            {
                laccion1 = "TODOS";
                laccion2 = "VENDEDOR";

                //grvSocios.DataSource = dc.sp_membreisasxSucursal(laccion2, lsucursal, lfechaInicio, lfechaFin);
                //grvSocios.DataBind();

                grvVendedorTotal.DataSource = dc.sp_membreisasxVendedor_awm(laccion1, lsucursal, lfechaInicio, lfechaFin);
                grvVendedorTotal.DataBind();
            }

        }

        if (ltipo == "2")
        {
            grvVendedor.DataSource = dc.sp_membreisasxVendedor_awm(laccion2, lsucursal, lfechaInicio, lfechaFin);

            grvVendedorTotal.DataSource = dc.sp_membreisasxVendedor_awm(laccion1, lsucursal, lfechaInicio, lfechaFin);
            grvVendedorTotal.DataBind();


        }
    }


    protected void btnSocTotal_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        //pnSocios.GroupingText = "Socios ANETA";

        ltipo = "1";
        laccion1 = "DETALLE";
        laccion2 = "TOTAL";

        listaReportes(ltipo, laccion1, laccion2);

        pnSocios.Visible = false;
        pnTotalSocios.Visible = true;

        pnVendedor.Visible = false;
        pnVendedorTotal.Visible = false;


        btnExcelSA.Visible = true;
        btnExcelSS.Visible = true;
    }

    protected void btnSocxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        // pnSocios.GroupingText = "Socios por sucursal";

        ltipo = "2";
        laccion1 = "TODOS";
        laccion2 = "VENDEDOR";

        listaReportesVendedor(ltipo, laccion1, laccion2);

        pnSocios.Visible = false;
        pnTotalSocios.Visible = false;

        pnVendedor.Visible = false;
        pnVendedorTotal.Visible = true;

        btnExcelvendedorTotal.Visible = true;
        btnExcelVendedor.Visible = true;
    }

    #region EXCEL
    protected void btnExcelSA_Click(object sender, EventArgs e)
    {
        uno();

    }
    protected void btnExcelSS_Click(object sender, EventArgs e)
    {

        dos();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnExcelvendedorTotal_Click(object sender, EventArgs e)
    {
        tres();
    }
    protected void btnExcelVendedor_Click(object sender, EventArgs e)
    {
        cuatro();
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
            grvSocios.AllowPaging = false;
            /// this.BindGrid();

            grvSocios.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvSocios.HeaderRow.Cells)
            {
                cell.BackColor = grvSocios.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvSocios.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvSocios.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvSocios.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvSocios.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    protected void dos()
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
            grvTotalSocios.AllowPaging = false;
            /// this.BindGrid();

            grvTotalSocios.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvTotalSocios.HeaderRow.Cells)
            {
                cell.BackColor = grvTotalSocios.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvTotalSocios.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvTotalSocios.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvTotalSocios.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvTotalSocios.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }



    protected void tres()
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
            grvVendedorTotal.AllowPaging = false;
            /// this.BindGrid();

            grvVendedorTotal.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvVendedorTotal.HeaderRow.Cells)
            {
                cell.BackColor = grvVendedorTotal.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvVendedorTotal.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvVendedorTotal.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvVendedorTotal.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvVendedorTotal.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    protected void cuatro()
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
            grvVendedor.AllowPaging = false;
            /// this.BindGrid();

            grvVendedor.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvVendedor.HeaderRow.Cells)
            {
                cell.BackColor = grvVendedor.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvVendedor.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvVendedor.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvVendedor.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvVendedor.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion

    #region totales
    protected void grvTotalSocios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            tresN += Convert.ToDecimal(e.Row.Cells[3].Text);
            tresR += Convert.ToDecimal(e.Row.Cells[4].Text);
            standN += Convert.ToDecimal(e.Row.Cells[5].Text);
            standR += Convert.ToDecimal(e.Row.Cells[6].Text);
            motoN += Convert.ToDecimal(e.Row.Cells[7].Text);
            motoR += Convert.ToDecimal(e.Row.Cells[8].Text);
            premN += Convert.ToDecimal(e.Row.Cells[9].Text);
            premNR += Convert.ToDecimal(e.Row.Cells[10].Text);
            prem2N += Convert.ToDecimal(e.Row.Cells[11].Text);
            prem2R += Convert.ToDecimal(e.Row.Cells[12].Text);
            prem3N += Convert.ToDecimal(e.Row.Cells[13].Text);
            prem3R += Convert.ToDecimal(e.Row.Cells[14].Text);
            proautoN += Convert.ToDecimal(e.Row.Cells[15].Text);
            proautoR += Convert.ToDecimal(e.Row.Cells[16].Text);
            taxiN += Convert.ToDecimal(e.Row.Cells[17].Text);
            taxiR += Convert.ToDecimal(e.Row.Cells[18].Text);
            total += Convert.ToDecimal(e.Row.Cells[20].Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Totales";

            e.Row.Cells[3].Text = Convert.ToString(tresN);
            e.Row.Cells[4].Text = Convert.ToString(tresR);
            e.Row.Cells[5].Text = Convert.ToString(standN);
            e.Row.Cells[6].Text = Convert.ToString(standR);
            e.Row.Cells[7].Text = Convert.ToString(motoN);
            e.Row.Cells[8].Text = Convert.ToString(motoR);
            e.Row.Cells[9].Text = Convert.ToString(premN);
            e.Row.Cells[10].Text = Convert.ToString(premNR);
            e.Row.Cells[11].Text = Convert.ToString(prem2N);
            e.Row.Cells[12].Text = Convert.ToString(prem2R);
            e.Row.Cells[13].Text = Convert.ToString(prem3N);
            e.Row.Cells[14].Text = Convert.ToString(prem3R);
            e.Row.Cells[15].Text = Convert.ToString(proautoN);
            e.Row.Cells[16].Text = Convert.ToString(proautoR);
            e.Row.Cells[17].Text = Convert.ToString(taxiN);
            e.Row.Cells[18].Text = Convert.ToString(taxiR);
            e.Row.Cells[20].Text = Convert.ToString(total);
        }
    }
    protected void grvTotalSocios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "verReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvTotalSocios.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            string lidSuc = Convert.ToString(row.Cells[0].Text);


            pnTotalSocios.Visible = false;
            pnSocios.Visible = true;
            listarDetalleSocios(lidSuc);

        }

        /*if (e.CommandName == "verRet")
        {
            string lsuc = ddlSucursal2.SelectedValue.Trim();
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvEgresosDetalle.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            int lid = Convert.ToInt32(row.Cells[4].Text);

            string lruc = row.Cells[3].Text;
            string lnumRetencion = row.Cells[17].Text;
            string lautRetencion = row.Cells[9].Text;


            Session["pRetencion"] = lnumRetencion;
            Session["pSuc"] = lsuc;

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirRetencion.aspx','','width=800,height=750') </script>");
        }*/

    }

    protected void listarDetalleSocios(string Suc)
    {
        string lsucursal = Suc;
        DateTime lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        string laccion2 = "VENDEDOR";
        grvSocios.DataSource = dc.sp_membreisasxSucursalDetalle(laccion2, lsucursal, lfechaInicio, lfechaFin);
        grvSocios.DataBind();

    }
    protected void btnCancelarDetalle_Click(object sender, EventArgs e)
    {
        pnTotalSocios.Visible = true;
        pnSocios.Visible = false;
    }
    protected void grvVendedorTotal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "verReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvVendedorTotal.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            string lVendedor = Convert.ToString(row.Cells[2].Text);


            pnTotalSocios.Visible = false;
            pnSocios.Visible = false;
            pnVendedorTotal.Visible = false;
            pnVendedor.Visible = true;

            listarDetalleVendedor(lVendedor);

        }
    }
    protected void grvVendedorTotal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            tresN += Convert.ToDecimal(e.Row.Cells[3].Text);
            tresR += Convert.ToDecimal(e.Row.Cells[4].Text);
            standN += Convert.ToDecimal(e.Row.Cells[5].Text);
            standR += Convert.ToDecimal(e.Row.Cells[6].Text);
            motoN += Convert.ToDecimal(e.Row.Cells[7].Text);
            motoR += Convert.ToDecimal(e.Row.Cells[8].Text);
            premN += Convert.ToDecimal(e.Row.Cells[9].Text);
            premNR += Convert.ToDecimal(e.Row.Cells[10].Text);
            prem2N += Convert.ToDecimal(e.Row.Cells[11].Text);
            prem2R += Convert.ToDecimal(e.Row.Cells[12].Text);
            prem3N += Convert.ToDecimal(e.Row.Cells[13].Text);
            prem3R += Convert.ToDecimal(e.Row.Cells[14].Text);
            proautoN += Convert.ToDecimal(e.Row.Cells[15].Text);
            proautoR += Convert.ToDecimal(e.Row.Cells[16].Text);
            taxiN += Convert.ToDecimal(e.Row.Cells[17].Text);
            taxiR += Convert.ToDecimal(e.Row.Cells[18].Text);
            total += Convert.ToDecimal(e.Row.Cells[20].Text);


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Totales";

            e.Row.Cells[3].Text = Convert.ToString(tresN);
            e.Row.Cells[4].Text = Convert.ToString(tresR);
            e.Row.Cells[5].Text = Convert.ToString(standN);
            e.Row.Cells[6].Text = Convert.ToString(standR);
            e.Row.Cells[7].Text = Convert.ToString(motoN);
            e.Row.Cells[8].Text = Convert.ToString(motoR);
            e.Row.Cells[9].Text = Convert.ToString(premN);
            e.Row.Cells[10].Text = Convert.ToString(premNR);
            e.Row.Cells[11].Text = Convert.ToString(prem2N);
            e.Row.Cells[12].Text = Convert.ToString(prem2R);
            e.Row.Cells[13].Text = Convert.ToString(prem3N);
            e.Row.Cells[14].Text = Convert.ToString(prem3R);
            e.Row.Cells[15].Text = Convert.ToString(proautoN);
            e.Row.Cells[16].Text = Convert.ToString(proautoR);
            e.Row.Cells[17].Text = Convert.ToString(taxiN);
            e.Row.Cells[18].Text = Convert.ToString(taxiR);
            e.Row.Cells[20].Text = Convert.ToString(total);
        }
    }

    protected void listarDetalleVendedor(string vendedor)
    {
        string lvendedor = vendedor;
        DateTime lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lfechaFin = Convert.ToDateTime(txtFechaFin.Text);
        string laccion2 = "VENDEDOR";
        grvVendedor.DataSource = dc.sp_membreisasxVendedorDetalle(laccion2, vendedor, lfechaInicio, lfechaFin);
        grvVendedor.DataBind();

    }
    #endregion


    protected void btnregresarVendedor_Click(object sender, EventArgs e)
    {
        pnTotalSocios.Visible = false;
        pnSocios.Visible = false;

        pnVendedorTotal.Visible = true;
        pnVendedor.Visible = false;
    }
}