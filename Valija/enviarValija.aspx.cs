
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

public partial class Valija_enviarValija : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["valijaConnectionString"].ConnectionString;

    DataValijaDataContext dv = new DataValijaDataContext();
    

    #endregion

    #region INICIO
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            txtFecha.Text = Convert.ToString(lfecha);
            // txtFechaEmisionDoc.Text = Convert.ToString(lfecha);
            //txtFechaCaducDoc.Text = Convert.ToString(lfecha);

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
        pnTitulos.Enabled = true;
        pnDetallePagos.Visible = false;
        pnMenu.Visible = false;
        pnEnvios.Visible = false;
        pnBorrar.Visible = false;
        pnExportar.Visible = false;
        pnDetallePagos.Visible = false;
        pnTitulos.Enabled = true;
       
    }

    protected bool fechaValida()
    {
        bool fechaValida = false;
        var date1 = txtFecha.Text;

        DateTime dt1 = DateTime.Now;
        DateTime dt2 = dt1;

        var culture = CultureInfo.CreateSpecificCulture("es-MX");
        var styles = DateTimeStyles.None;


        fechaValida = DateTime.TryParse(date1, culture, styles, out dt1);

        return fechaValida;
    }
    #endregion
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        btnConsultar_Click();
    }

    protected void btnConsultar_Click()
    {
        pnTitulos.Enabled = false;
        lblMensaje.Text = string.Empty;

        if (!fechaValida())
        {
            lblMensaje.Text = "Error en la fecha";
        }
        else
        {
            //lblMensaje.Text = "";
            if (estadoCierre())
            {

                //lblMensaje.Text = "";
                pnDetallePagos.Visible = true;
                pnMenu.Visible = true;

                if (listarEnvios())
                {
                    pnExportar.Visible = true;
                }
                else
                {
                    pnExportar.Visible = false;
                }

            }
            else
            {
                lblMensaje.Text = "Valija enviada, no se puede modificar";
                if (listarEnvios())
                {
                    pnDetallePagos.Visible = true;
                    pnMenu.Visible = false;
                    pnExportar.Visible = true;
                }
                else
                {
                    pnDetallePagos.Visible = false;
                    pnExportar.Visible = false;
                }
            }

        }
    }

    protected bool estadoCierre()
    {
        string lnumero ;
        bool pasa = false;
        int lestado;
        /*****************************************/
  
        DateTime lfecha = DateTime.Today;
        DateTime docFecha = DateTime.Today;
        docFecha = Convert.ToDateTime(txtFecha.Text);
        string lsucursal = ddlSucursal2.SelectedValue.Trim();
        string dia, mes, ano;

        dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
        mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
        ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);
        lnumero = 'E' + lsucursal + dia + mes + ano;

        txtNumero.Text = lnumero;
        
        /****************************************/


        // consultar estado de la caja de egresos

        var cCvalija = from Cv in dv.tbl_cabValija
                         where Cv.numero == lnumero
                         select new
                         {
                             estado = Cv.enviado
                         };

        if (cCvalija.Count() == 0)
        {
            pasa = true;
        }
        else
        {
            foreach (var registro in cCvalija)
            {
                lestado = Convert.ToInt32(registro.estado);
                if (lestado == 0)
                {
                    pasa = true;
                }
                else
                {
                    pasa = false;
                }
            }
        }
        return pasa;
    }

    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }

    protected bool listarEnvios()
    {
        bool pasa = false;
        int cuantos;
        string laccion, lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        DateTime lfecha = DateTime.Today;
                DateTime docFecha = DateTime.Today;
                docFecha = Convert.ToDateTime(txtFecha.Text);
                string lsucursal = ddlSucursal2.SelectedValue.Trim();
                string dia, mes, ano;

                dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
                mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
                ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);
                string lnumero = 'E' + lsucursal + dia + mes + ano;
                txtNumero.Text = lnumero;
                string numero = txtNumero.Text;


    /*    var concultaDetEgresos = dc.sp_ConsultaValijaDetallexNumero(laccion, lnumero);

        grvDetallePagos.DataSource = concultaDetEgresos;
        grvDetallePagos.DataBind();
        cuantos = grvDetallePagos.Rows.Count;

        if (cuantos <= 0)
        {
            pasa = false;
        }
        else
        {
            pasa = true;

        }*/

        return pasa;

    }
    protected void btnNuevoRegistro_Click(object sender, EventArgs e)
    {
        pnMensaje2.Visible = true;
        lblMensaje.Text = string.Empty;
        btnIngresaProv.Visible = true;
        pnTitulos.Enabled = false;
        btnConsultar.Visible = false;
        pnMenu.Visible = false;
        pnBorrar.Visible = false;
        pnEnvios.Visible = true;
        pnDetallePagos.Visible = false;



        desactivarTextos();
        llenarListados();
        //encerarTextos();


        //ddlTipoDocumento.Focus();
    }

    protected void desactivarTextos()
    {
        pnTitulos.Enabled = false;
        pnDetallePagos.Visible = false;
        pnExportar.Visible = false;

        lblColaborador.Visible = false;
        pnEmisor.Visible = true;
        ddlEmisor.Visible = true;
    }

    protected void llenarListados()
    {
        bool lactivo = true;
        #region DOCUMENTOS
        /*******DOCUMENTOS*********************/
        var consultaTd = from Td in dc.tbl_tipoEgresos
                         where Td.activo == lactivo
                         select new
                         {
                             id = Td.id_tipoEgresos,
                             descripcion = Td.id_tipoEgresos + " " + Td.descripcion.Trim()
                         };
        /*
        ddlTipoDocumento.DataSource = consultaTd;
        ddlTipoDocumento.DataBind();*/

        ListItem liDocumento = new ListItem("Seleccione tipo de documento", "-1");
       // ddlTipoDocumento.Items.Insert(0, liDocumento);
        #endregion

        #region TERCEROS
        /*******TERCEROS*********************/
        string lsucursal = ddlSucursal2.SelectedValue.Trim();
        var cColaborador = from Ter in dc.tbl_colaborador
                           where Ter.sucursal==lsucursal
                           orderby Ter.apellidos
                           select new
                           {
                               cedula = Ter.Cedula,
                               nombres = Ter.apellidos.Trim() + " " + Ter.nombres.Trim() 
                           };

        ddlEmisor.DataSource = cColaborador;
        ddlEmisor.DataBind();

        ListItem liTercero = new ListItem("Seleccione quién envía (Emisor) ", "-1");
        ddlEmisor.Items.Insert(0, liTercero);



       
        #endregion

        
        
        #region SUCURSAL destino
        /*******SUCURSALES *********************/
        var cSucursal = from mSuc in dc.tbl_ruc
                        where mSuc.activo == true
                        orderby mSuc.sucursal
                        select new
                        {
                            sucursal = mSuc.sucursal,
                            nom_suc = mSuc.sucursal + ' ' + mSuc.nom_suc.Trim()
                        };

        ddlSucDest.DataSource = cSucursal;
        ddlSucDest.DataBind();

        ListItem liSucursal = new ListItem("Seleccione la sucursal destino ", "-1");
        ddlSucDest.Items.Insert(0, liSucursal);
        #endregion

        #region CENTRO DE COSTO AFECTADO

        /*******DEPARTAMENTO *********************/
        var cDepar = from mDep in dc.tbl_departamento
                      orderby mDep.description
                      select new
                      {
                          id_departamento = mDep.id_departamento,
                          nom_dep = mDep.description.Trim()
                      };

        ddlDepartamento.DataSource = cDepar;
        ddlDepartamento.DataBind();

        ListItem liCco = new ListItem("Seleccione Departamento ", "-1");
        ddlDepartamento.Items.Insert(0, liCco);
        #endregion
    }

    protected void tererosSucursal(string sucursal)
    {
        /*******TERCEROS*********************/

        var cReceptor = from Ter in dc.tbl_colaborador
                        where Ter.sucursal==sucursal
                        orderby Ter.apellidos
                        select new
                        {
                            cedula = Ter.Cedula,
                            nombres = Ter.apellidos.Trim() + " " + Ter.nombres.Trim() + " " + Ter.Cedula.Trim()
                        };

        ddlReceptor.DataSource = cReceptor;
        ddlReceptor.DataBind();

        ListItem liReceptor = new ListItem("Seleccione a quién se le envía (Receptor) ", "-1");
        ddlReceptor.Items.Insert(0, liReceptor);
    }


    protected void btnGrabarPago_Click(object sender, EventArgs e)
    {
        btnGrabarPago_Click();
    }

    protected void btnGrabarPago_Click()
    {
        bool lpasa = false;
        string lmensaje;
        var lrevision = validarDatos();
        lpasa = lrevision.Item1;
        lmensaje = lrevision.Item2;

        if (lpasa)
        {
            registrarCabeceraValija();
            registrarDetalleValija();
            btnCancelarpago_Click();
            btnConsultar_Click();

        }
        else
        {
            lblMensaje.Text = "NO SE PUEDE GUARDAR " + lmensaje;
        }
    }

    protected void registrarCabeceraValija()
    {
        int valorId;
        valorId = confirmarID();
        if (valorId == 0)
        {
            string accion = "AGREGAENVI";
            string sucursal = ddlSucursal2.SelectedValue;
            string departamento = ddlDepartamento.SelectedValue;
            string emisor = ddlEmisor.SelectedValue;
            string guia = txtGuia.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string usuario = Convert.ToString(Session["SUsername"]);
           
            int lid_CabValija = 0;
           // DateTime lfecha = DateTime.Today;
            bool enviado = false, recibido = false;


            txtSucursal.Text = ddlSucursal2.SelectedValue;
            txtFecha.Text = Convert.ToString(txtFecha.Text);
           // txtNumero.Text = txtSucursal.Text.Trim() + txtFecha.Text.Trim();

             DateTime lfecha = DateTime.Today;
                DateTime docFecha = DateTime.Today;
                docFecha = Convert.ToDateTime(txtFecha.Text);
                string lsucursal = ddlSucursal2.SelectedValue.Trim();
                string dia, mes, ano;

                dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
                mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
                ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);
                string lnumero = 'E' + lsucursal + dia + mes + ano;
                txtNumero.Text = lnumero;
                string numero = txtNumero.Text;


            dc.sp_abmValijaCabecera(accion, lid_CabValija, sucursal, numero, lfecha, lfecha, emisor, "", usuario,enviado,recibido);
        }

    }

    protected void registrarDetalleValija()
    {


        string lAccion = "AGREENVIA";
        int lid_DetValija = 0;
        int lid_CabValija = confirmarID();


        string sucDestino = ddlSucDest.SelectedValue;
        int departamento = Convert.ToInt32(ddlDepartamento.SelectedValue);
        string emisor = ddlReceptor.SelectedValue;
        string guia = txtGuia.Text.Trim();
        string descripcion = txtDescripcion.Text.Trim();
        
        bool entregado = false;

//        DateTime fechaEmisionDoc = Convert.ToDateTime(txtFecha.Text);
//        DateTime fechaCaducDoc = Convert.ToDateTime(txtFechaCaducDoc.Text);

/*        var cDet = from mDet in dc.tbl_DetEgresos
                   where mDet.ruc == lruc
                   && mDet.id_documento == lid_documento
                   && mDet.numeroDocumento == ldocumento
                   select new
                   {
                       id_DetEgresos = mDet.id_DetEgresos
                   };

        if (cDet.Count() <= 0)
        {*/
            
        pnMensaje2.Visible = true;

            /*GRABA DETALLE DE EGRESOS*/
            dc.sp_abmValijaDetalle(lAccion,lid_DetValija,lid_CabValija,sucDestino,departamento,emisor,guia,descripcion,"",entregado);

            

            lblMensaje.Text = "Se ha grabado con éxito";
        
        //}
        /*else
        {
            pnMensaje2.Visible = true;
            lblMensaje.Text = "Este registro (DOCUMENTO YA REGISTRADO) ya fue ingresado";
        }*/

    }
    protected void btnCancelarpago_Click(object sender, EventArgs e)
    {
        btnCancelarpago_Click();
    }


   

    protected void btnCancelarpago_Click()
    {
        btnIngresaProv.Visible = false;
        pnTitulos.Enabled = true;
        btnConsultar.Visible = true;
        pnDetallePagos.Visible = true;
        pnMenu.Visible = true;
        pnEnvios.Visible = false;
        pnBorrar.Visible = false;
        pnExportar.Visible = false;
    }

    protected int confirmarID()
    {
        int retonoId;
         DateTime lfecha = DateTime.Today;
                DateTime docFecha = DateTime.Today;
                docFecha = Convert.ToDateTime(txtFecha.Text);
                string lsucursal = ddlSucursal2.SelectedValue.Trim();
                string dia, mes, ano;

                dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
                mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
                ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);
                string lnumero = 'E' + lsucursal + dia + mes + ano;
                txtNumero.Text = lnumero;
                string numero = txtNumero.Text;




        retonoId = 0;

        var ConsultaId = from Vid in dv.tbl_cabValija
                         where Vid.numero == lnumero
                         select new
                         {
                             id = Vid.id_cabValija
                         };

        if (ConsultaId.Count() == 0)
        {
            retonoId = 0;
        }
        else
        {
            foreach (var registro in ConsultaId)
            {
                retonoId = registro.id;
            }
        }

        return retonoId;
    }


    protected Tuple<bool, string> validarDatos()
    {
        string lmensaje = string.Empty; ;
        bool pasa = true;
        bool existeProv = true;





        /* var lrevision = consultarProveedor();
         existeProv = lrevision.Item1;



         if (txtFechaEmisionDoc.Text.Length <= 0)
         {

             lmensaje = " Ingrese fecha de emisión del documento ";
             pasa = false;
         }


         if (txtFechaCaducDoc.Text.Length <= 0)
         {

             lmensaje = " Ingrese fecha de caducidad del documento ";
             pasa = false;
         }


         if (TipoDocumento == "-1")
         {
             lmensaje = " Seleccione tipo de documento ";
             pasa = false;
         }

         if (!existeProv)
         {
             lmensaje = lmensaje + " Cree el proveedor (Matriz) ";
             pasa = false;
         }

         if (NumDocumento.Length <= 0)
         {
             lmensaje = lmensaje + " Ingrese el #Doc ";
             pasa = false;
         }

         if (NumDocumento.Length < 15 || NumDocumento.Length > 15)
         {
             lmensaje = lmensaje + " El documento debe tener 15 dígitos";
             pasa = false;
         }


         if (DocAutorizacion.Length < 10)
         {
             lmensaje = lmensaje + " El número de autorización debe ser de 10 dígitos si es manual ó 49 si es digital";
             pasa = false;
         }

         if (DocAutorizacion.Length > 49)
         {
             lmensaje = lmensaje + " El número de autorización debe ser de 10 dígitos si es manual ó 49 si es digital";
             pasa = false;
         }

         if (AfectaSucursal == "-1")
         {
             lmensaje = lmensaje + " Seleccione sucursal ";
             pasa = false;
         }

         if (AfectaCcosto == "-1")
         {
             lmensaje = lmensaje + " Seleccione centro de costo ";
             pasa = false;
         }


         if (Autorizacion.Length <= 0)
         {
             lmensaje = lmensaje + " Ingrese quién autoriza ";
             pasa = false;
         }

         if (Descripcion.Length <= 0)
         {
             lmensaje = lmensaje + " Ingrese descripción del gasto";
             pasa = false;
         }


         if (TipoPago == "-1")
         {
             lmensaje = lmensaje + " Seleccione tipo de pago ";
             pasa = false;
         }


         if (Btotal <= 0 && Stotal <= 0)
         {
             lmensaje = lmensaje + " Ingrese valores en Bienes o Servicios o en ambos si es el caso ";
             pasa = false;
         }

         if (Btotal > 0)
         {
             if (BiGastos == "-1" || BiCodCble == "-1")
             {
                 lmensaje = lmensaje + " Seleccione concepto y código contable del Bien ";
                 pasa = false;
             }
             if (Bien.Length <= 0)
             {
                 lmensaje = lmensaje + " Ingrese descripción del Bien";
                 pasa = false;
             }
         }


         if (Stotal > 0)
         {
             if (SiGastos == "-1" || SiCodCble == "-1")
             {
                 lmensaje = lmensaje + " Seleccione concepto y código contable del Servicio ";
                 pasa = false;
             }
             if (Servicio.Length <= 0)
             {
                 lmensaje = lmensaje + " Ingrese descripción del Servicio";
                 pasa = false;
             }
         }

         if (new[] { "3", "5", "19", "20", "23" }.Contains(TipoDocumento))
         {
             if (ValorRetencion <= 0)
             {
                 lmensaje = lmensaje + " Valor de la retención debe ser mayor que cero";
                 pasa = false;
             }

             if (NumAutorizacion.Length <= 0)
             {
                 lmensaje = lmensaje + " Ingrese el número de autorización de la retención";
                 pasa = false;
             }
         }

         if (ValorFactura <= 0)
         {
             lmensaje = lmensaje + " Ingrese los valores del documento";
             pasa = false;
         }

         if (aPagar <= 0)
         {
             lmensaje = lmensaje + " Ingrese los valores del documento";
             pasa = false;
         }*/
        return Tuple.Create(pasa, lmensaje);
    }
    protected void grvDetallePagos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (estadoCierre())
        {
            pnTitulos.Enabled = false;
            btnConsultar.Visible = false;
            pnBorrar.Visible = true;
            pnDetallePagos.Visible = false;
            pnEnvios.Visible = false;
            pnExportar.Visible = false;

            DateTime fechaEmisionDoc = DateTime.Today;
            DateTime fechaCaducDoc = DateTime.Today;

            if (txtFecha.Text.Length > 0)
            {
                fechaEmisionDoc = Convert.ToDateTime(txtFecha.Text);
            }



            string accion = "CONSULTAR";
            int id_DetValija = Convert.ToInt32(grvDetallePagos.SelectedValue);
            //int id_DetEgresos = 0;
            //int id_DetEgresos2 = Convert.ToInt32(grvDetallePagos.SelectedDataKey);
    //        var cDet = dc.sp_abmValijaDetalle(accion,id_DetValija,0,"",0,"","","","",false);


            // if(cDet.Count() > 0)
            //{
            
            /*
            foreach (var registro in cDet)
            {
                txtBCodigo.Text = Convert.ToString(id_DetValija);
                txtBSuc.Text = registro.sucDestino;
                txtBDepartamento.Text = Convert.ToString(registro.departamento);
                txtBGuia.Text = registro.numeroGuia;
                txtBDescripcion.Text = registro.descripcionEnvio;

            }*/


            //}
        }
        else
        {
            lblMensaje.Text = "Valija se envió no se puede modificar";

        }
    }
    protected void btnBorrarPago_Click(object sender, EventArgs e)
    {
        DateTime fechaEmisionDoc = DateTime.Today;
        DateTime fechaCaducDoc = DateTime.Today;

        if (txtFecha.Text.Length > 0)
        {
            fechaEmisionDoc = Convert.ToDateTime(txtFecha.Text);
        }

        string accion = "BORRAR";
        int id_DetValija = Convert.ToInt32(grvDetallePagos.SelectedValue);
   //     var cDet = dc.sp_abmValijaDetalle(accion,id_DetValija,0,"",0,"","","","",false);
        btnRegresar3_Click();

    }
    protected void btnRegresar3_Click(object sender, EventArgs e)
    {
           btnRegresar3_Click();
    }

    protected void btnRegresar3_Click()
    {
        pnTitulos.Enabled = true;
        btnConsultar.Visible = true;
        pnBorrar.Visible = false;
        pnDetallePagos.Visible = true;
        pnEnvios.Visible = false;
        pnExportar.Visible = true;
        btnConsultar_Click();
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        perfilUsuario();
        activarObjetos();
    }


    #region EXCEL
    protected void btnExcelRe_Click(object sender, EventArgs e)
    {
        todos();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void todos()
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
            grvDetallePagos.AllowPaging = false;

            this.BindGrid();

            grvDetallePagos.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvDetallePagos.HeaderRow.Cells)
            {
                cell.BackColor = grvDetallePagos.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvDetallePagos.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvDetallePagos.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvDetallePagos.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvDetallePagos.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }

    private void BindGrid()
    {
        string laccion, lestado;

        lestado = string.Empty;
        laccion = "DETALLE";
        
         DateTime lfecha = DateTime.Today;
                DateTime docFecha = DateTime.Today;
                docFecha = Convert.ToDateTime(txtFecha.Text);
                string lsucursal = ddlSucursal2.SelectedValue.Trim();
                string dia, mes, ano;

                dia = llenarCeros(Convert.ToString(docFecha.Day), '0', 2);
                mes = llenarCeros(Convert.ToString(docFecha.Month), '0', 2);
                ano = llenarCeros(Convert.ToString(docFecha.Year), '0', 4);
                string lnumero = 'E' + lsucursal + dia + mes + ano;
                txtNumero.Text = lnumero;
                string numero = txtNumero.Text;




    /*    var concultaDetEgresos = dc.sp_ConsultaValijaDetallexNumero(laccion, lnumero);

        grvDetallePagos.DataSource = concultaDetEgresos;
        grvDetallePagos.DataBind();
        */
        pnDetallePagos.Visible = true;
    }
    #endregion


    protected void btnImprimir_Click(object sender, EventArgs e)
    {

        int lid_CabValija = confirmarID();
        string accion = "MODENVIADO";
        DateTime esteDia = DateTime.Today;
        DateTime lfechaInicio, lfechaFin;

        //string lsuc,lfechaInicio,lfechaFin;

        string lsuc;

        lfechaInicio = DateTime.Today;
        lfechaFin = DateTime.Today;
        lsuc = "";



        dc.sp_abmValijaCabecera(accion, lid_CabValija, "", "", lfechaInicio, lfechaInicio, "", "", "", false, false);
        

        lfechaInicio = Convert.ToDateTime(txtFecha.Text);
        
        lsuc = ddlSucursal2.Text.Trim();

        Session["pFechaInicio"] = lfechaInicio;
        
        Session["pSuc"] = lsuc;

        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('envioValija.aspx','','width=800,height=500') </script>");

        btnRegresar_Click();
    }
    protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlSucDest_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sucursal = ddlSucDest.SelectedValue.Trim();
        tererosSucursal(sucursal);
    }
}