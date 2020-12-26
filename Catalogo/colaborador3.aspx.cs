using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using acefdos;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class Catalogo_colaborador3 : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    #region  INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMensaje.Text = string.Empty;
            string accion = string.Empty;
            perfilUsuario();
            listarColaboradores();
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
    #region PROCESOS
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    protected void ibConsultar_Click()
    {
        string lcedula = txtCedula.Text.Trim();

        if (lcedula.Length > 0)
        {
            var cColabora = from tColabora in dc.tbl_colaborador
                            where tColabora.Cedula == lcedula
                            select new
                            {
                                Cedula = tColabora.Cedula
                               ,
                                apellidos = tColabora.apellidos
                                 ,
                                nombres = tColabora.nombres
                                 ,
                                sucursal = tColabora.sucursal
                                 ,
                                ccosto = tColabora.ccosto
                                 ,
                                activo = tColabora.activo
                                ,
                                mae_cue = tColabora.mae_cue.Trim()
                                ,
                                cargo = tColabora.cargo
                                 ,
                                telefono1 = tColabora.telefono1.Trim()
                                ,
                                movil = tColabora.movil.Trim()
                                ,
                                fechaNac = tColabora.fechaNac
                                ,
                                fechaIng = tColabora.fechaIng
                                ,
                                sexo = tColabora.sexo
                                ,
                                estadoCivil = tColabora.estadoCivil
                                ,
                                tipoEmpleo = tColabora.tipoEmpleo
                                ,
                                foto = tColabora.foto
                                ,
                                textoAlterno = tColabora.textoAlterno
                                ,
                                emailDomicilio = tColabora.emailDomicilio
                                ,
                                emailCorporativo = tColabora.emailCorporativo
                                ,
                                instructorPractica = tColabora.instructorPractica
                                 ,
                                nombreCorto = tColabora.nombreCorto
                                
                            };

           
            llenarListados();
            blanquearObjetos();

            foreach (var registro in cColabora)
            {
                lblMensaje.Text = string.Empty;
                txtCedula.Text = registro.Cedula;
				txtApellidos.Text = registro.apellidos;
				txtNombres.Text = registro.nombres;
				//ddlSucursal.Text = registro.sucursal;
				ddlCcosto.SelectedValue = Convert.ToString(registro.ccosto);
                chkActivo.Checked = Convert.ToBoolean(registro.activo);
				ddlCtaCble.SelectedValue = registro.mae_cue;
				ddlCargo.SelectedValue = Convert.ToString(registro.cargo);
				txtTelefono.Text = registro.telefono1;
				txtMovil.Text = registro.movil;
				txtFechaNacimiento.Text = Convert.ToString(registro.fechaNac);
				txtFechaIngreso.Text = Convert.ToString(registro.fechaIng);
				ddlGenero.SelectedValue = Convert.ToString(registro.sexo);
				ddlEstadoCivil.SelectedValue = Convert.ToString(registro.estadoCivil);
				ddlTipoEmpleo.SelectedValue = Convert.ToString(registro.tipoEmpleo);
				txtFoto.Text = registro.foto;
				txtAlterno.Text = registro.textoAlterno;
				txtEmailDomicilio.Text = registro.emailDomicilio;
				txtEmailCorporativo.Text = registro.emailCorporativo;
				txtNombreCorto.Text = registro.nombreCorto;
                bool instPrac = Convert.ToBoolean(registro.instructorPractica);
                if (instPrac)
                {
                    ddlinstructorPractica.SelectedValue = "1";
                }
                else {
                    ddlinstructorPractica.SelectedValue = "2";
                }
            }

            txtApellidos.Focus();
        }
        else
        {
            txtCedula.Focus();
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = string.Empty;

        string userName = Convert.ToString(Session["SUsername"]); 

        string Accion = "GUARDAR";
        string Cedula = txtCedula.Text.Trim();
        string apellidos = txtApellidos.Text;
        string  nombres = txtNombres.Text ;
        string sucursal = ddlSucursal.Text;
        string ccosto = ddlCcosto.SelectedValue;
        bool activo = Convert.ToBoolean(chkActivo.Checked);
        string mae_cue = ddlCtaCble.SelectedValue;
        int cargo = Convert.ToInt32(ddlCargo.SelectedValue);
        string telefono1 = txtTelefono.Text;
        string movil = txtMovil.Text;
        DateTime fechaNac = Convert.ToDateTime(txtFechaNacimiento.Text) ;
        DateTime fechaIng = Convert.ToDateTime(txtFechaIngreso.Text);
        int sexo = Convert.ToInt32(ddlGenero.SelectedValue) ;
        int estadoCivil =  Convert.ToInt32(ddlEstadoCivil.SelectedValue) ;
        int tipoEmpleo =  Convert.ToInt32(ddlTipoEmpleo.SelectedValue) ;
        string foto = "~/admArchivos/nomina/"+Cedula+".jpg";
        string textoAlterno = apellidos + " " + nombres; ;//txtAlterno.Text ;
        string emailDomicilio = txtEmailDomicilio.Text;
        string emailCorporativo = txtEmailCorporativo.Text;
        string nombreCorto = txtNombreCorto.Text ;
        int insPrac =  Convert.ToInt32(ddlinstructorPractica.SelectedValue);
        bool instructorPractica = false;

        if (validarDatos()){
                /*GUARDAR INFORMACION*/
                dc.sp_abmColaborador(Accion,Cedula,apellidos,nombres,sucursal,ccosto,activo,mae_cue,cargo,telefono1,movil,fechaNac,fechaIng,sexo,estadoCivil,tipoEmpleo,foto,textoAlterno,emailDomicilio,emailCorporativo,nombreCorto,instructorPractica);
                dc.sp_abmModificion(Accion,0,sucursal, DateTime.Now, userName);
                blanquearObjetos();
                lblMensaje.Text = apellidos.Trim() + " " + nombres.Trim() + " guardado correctamente";
                btnRegresar_Click();
            }

        
    }

    protected void llenarListados()
    {
        /* TRAER CUENTAS CONTABLES*/
        var cCtaCble = from mcta in dc.tbl_mae_cue
                       where mcta.EST_CTA == 1
                             && mcta.TIP_CTA == 2
                       orderby mcta.MAE_CUE
                       select new
                       {
                           mae_cue = mcta.MAE_CUE.Trim()
                        ,
                           nom_cta = mcta.MAE_CUE.Trim() + " " + mcta.NOM_CTA.Trim()
                       };


        ddlCtaCble.DataSource = cCtaCble;
        ddlCtaCble.DataBind();

        ListItem listCble = new ListItem("Seleccione Cuenta Contable", "-1");
        ddlCtaCble.Items.Insert(0, listCble);

        /* TRAER CARGO*/
        var cCargo = from mcar in dc.tbl_cargo
                   orderby mcar.descripcion
                   select new
                   {
                       cargo = mcar.cargo
                    ,
                       descripcion = mcar.descripcion.Trim()
                   };


        ddlCargo.DataSource = cCargo;
        ddlCargo.DataBind();

        ListItem listCar = new ListItem("Seleccione el cargo", "-1");
        ddlCargo.Items.Insert(0, listCar);

        /* TRAER CENTRO DE COSTO*/
        var cCcosto = from mccosto in dc.tbl_mae_cco
                      select new
                      {
                          mae_cco = mccosto.mae_cco.Trim()
                       ,
                          nom_cco = mccosto.mae_cco.Trim() + " " + mccosto.nom_cco.Trim()
                      };


        ddlCcosto.DataSource = cCcosto;
        ddlCcosto.DataBind();
        ListItem listCosto = new ListItem("Seleccione Centro de Costo", "-1");
        ddlCcosto.Items.Insert(0, listCosto);

        /* TRAER ESTADO CIVIL*/
        var cCivil = from mciv in dc.tbl_estadoCivil
                     orderby mciv.descripcion
                     select new
                     {
                         id_estadoCivil = mciv.id_estadoCivil
                      ,
                         descripcion = mciv.descripcion.Trim()
                     };


        ddlEstadoCivil.DataSource = cCivil;
        ddlEstadoCivil.DataBind();

        ListItem listCiv = new ListItem("Seleccione el estado civil", "-1");
        ddlEstadoCivil.Items.Insert(0, listCiv);
    }

    protected void blanquearObjetos()
    {
        //txtCedula.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        ddlCcosto.SelectedValue = "-1";
        chkActivo.Checked = false;
    }

    protected void listarColaboradores()
    {
        //var cColaborador = dc.sp_abmColaborador("CONSULTAR", "", "", "", "", "", false, "", 0, "", "", DateTime.Now, DateTime.Now, 0, 0, 0, "");
        string Accion = "CONSULTAR";
        string sucursal = ddlSucursal.SelectedValue;
        grvColaboradores.DataSource = colaboradorCapaDatos.getAllColaboradores(sucursal);
        grvColaboradores.DataBind();

        var cModi = dc.sp_abmModificion(Accion, 0, sucursal, DateTime.Now, "");
        foreach (var registro in cModi)
        {
            lblUltimaModificacion.Text = "Ultima modificacion: " + Convert.ToString(registro.fecha) + " Usuario: " + registro.Username; ;
        }
        
    }

    #endregion
    #region LISTADO DE COLABORADORES POR SUCURSAL
    protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
    {
        listarColaboradores();
    }
    #endregion

    #region CARGAR ARCHIVOS (FOTOS JPG)
    protected void btnCargar_Click(object sender, EventArgs e)
    {
        Session["pFormulario"] = "colaborador3.aspx";
        string sucursal = ddlSucursal.SelectedValue;
        string camino = "~/admArchivos/nomina/";
        //string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";

        string formulario = Convert.ToString(Session["pFormulario"]);

        Session["pSucursal"] = sucursal;

        if (ulCarga.HasFile)
        {
            ulCarga.PostedFile.SaveAs(Server.MapPath(camino) + ulCarga.FileName);
        }

        formulario = Convert.ToString(Session["pFormulario"]);
        Response.Redirect(formulario);


    }

    protected void btnImagen_Click(object sender, EventArgs e)
    {
        Session["pFormulario"] = "colaborador3.aspx";
        string sucursal = ddlSucursal.SelectedValue;
        string camino = "~/admArchivos/licenciasConducir/";
        //string camino = Convert.ToString(Session["pCamino"]) + sucursal.ToLower() + "/";

        string formulario = Convert.ToString(Session["pFormulario"]);

        Session["pSucursal"] = sucursal;

        if (ulCarga.HasFile)
        {
            ulCarga.PostedFile.SaveAs(Server.MapPath(camino) + ulCarga.FileName);
        }

        formulario = Convert.ToString(Session["pFormulario"]);
        Response.Redirect(formulario);


    }
    #endregion




    protected void grvColaboradores_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cedula = Convert.ToString(grvColaboradores.SelectedValue);
        txtCedula.Enabled = false;
        txtCedula.Text = cedula;
        ibConsultar_Click();
        pnCabecera.Visible = false;
        pnDetalleNomina.Visible = false;
        pnActualizacion.Visible = true;

    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        btnRegresar_Click();
    }

    protected void btnRegresar_Click()
    {
        pnCabecera.Visible = true;
        pnDetalleNomina.Visible = true;
        pnActualizacion.Visible = false;
        listarColaboradores();
    }
    protected void btnNuevoColaborador_Click(object sender, EventArgs e)
    {
        string cedula = Convert.ToString(grvColaboradores.SelectedValue);
        //txtCedula.Text = cedula;
        //ibConsultar_Click();
        txtCedula.Enabled = true;
        pnCabecera.Visible = false;
        pnDetalleNomina.Visible = false;
        pnActualizacion.Visible = true;
        blankearCasilleros();
        llenarListados();
        txtCedula.Focus();
    }


    /*EXCEL*/
    #region EXPORTACIONES A EXCEL
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
            grvColaboradores.AllowPaging = false;
            /// this.BindGrid();

            grvColaboradores.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvColaboradores.HeaderRow.Cells)
            {
                cell.BackColor = grvColaboradores.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvColaboradores.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvColaboradores.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvColaboradores.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvColaboradores.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }




    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        lblMensaje.Visible = true;

        string acta = string.Empty;
        string alterno = string.Empty;

        string sucursal = ddlSucursal.Text;
        string nombreSucursal = Convert.ToString(ddlSucursal.Items);
 
        Session["pSucursal"] = sucursal;
        Session["pNombreSucursal"] = sucursal;


        // Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('~/cierre.aspx','popup','width=800,height=500') </script>");

        Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>window.open('imprimirColaborador.aspx','','width=800,height=500') </script>");




        lblMensaje.Text = "";
    }
    #endregion
    protected void txtCedula_TextChanged(object sender, EventArgs e)
    {
        //ibConsultar_Click();
    }


    protected void blankearCasilleros(){
        txtCedula.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        txtNombreCorto.Text = string.Empty;
        txtEmailDomicilio.Text = string.Empty;
        txtEmailCorporativo.Text = string.Empty;
        txtTelefono.Text = string.Empty;
        txtMovil.Text = string.Empty;
        txtFechaNacimiento.Text = Convert.ToString(DateTime.Now);
        txtFechaIngreso.Text = Convert.ToString(DateTime.Now);
    }
    protected bool validarDatos() {
        lblMensaje.Text = string.Empty;
        bool pasa = true;
        
        string userName = Convert.ToString(Session["SUsername"]);

        string sucursal = ddlSucursal.Text;
        string Cedula = txtCedula.Text.Trim();
        string apellidos = txtApellidos.Text;
        string nombres = txtNombres.Text;
        string nombreCorto = txtNombreCorto.Text;
        string emailDomicilio = txtEmailDomicilio.Text;
        string emailCorporativo = txtEmailCorporativo.Text;
        string ccosto = ddlCcosto.SelectedValue;
        string cargo = ddlCargo.SelectedValue;
        string telefono1 = txtTelefono.Text;
        string movil = txtMovil.Text;
        string fechaNac = txtFechaNacimiento.Text;
        string fechaIng = txtFechaIngreso.Text;
        string sexo = ddlGenero.SelectedValue;
        string estadoCivil = ddlEstadoCivil.SelectedValue;
        string tipoEmpleo = ddlTipoEmpleo.SelectedValue;

        if (Cedula.Length < 10)
        {
            lblMensaje.Text = "Ingrese documento de identificación válido";
            pasa = false;
        }

        if (apellidos.Length <= 2
           || nombres.Length <= 2)
        {
            lblMensaje.Text = "Ingrese nombres y apellidos válidos";
            pasa = false;
        }

        if (nombreCorto.Length <= 2)
        {
            lblMensaje.Text = "Ingrese el nombre corto";
            pasa = false;
        }

        if (!email_bien_escrito(emailDomicilio))
            {
                lblMensaje.Text = "Ingrese el correo personal válido";
                pasa = false;
            }

       if (!email_bien_escrito(emailCorporativo))
            {
                lblMensaje.Text = "Ingrese el correo corporativo válido";
                pasa = false;
            }

       if (sucursal == "-1" || ccosto == "-1")
            {
                lblMensaje.Text = "Ingrese sucursal y centro de costo ";
                pasa = false;
            }
       if (cargo == "-1" )
       {
           lblMensaje.Text = "Ingrese el cargo ";
           pasa = false;
       }

       if (telefono1.Length <= 2)
       {
           lblMensaje.Text = "Ingrese número telefónico convencional";
           pasa = false;
       }

       if (movil.Length <= 2)
       {
           lblMensaje.Text = "Ingrese número de celular";
           pasa = false;
       }

       if (fechaNac.Length <= 2)
       {
           lblMensaje.Text = "Ingrese la fecha de nacimiento";
           pasa = false;
       }

       if (fechaIng.Length <= 2)
       {
           lblMensaje.Text = "Ingrese la fecha de ingreso";
           pasa = false;
       }

       if (sexo == "-1")
       {
           lblMensaje.Text = "Ingrese el género";
           pasa = false;
       }

       if (estadoCivil == "-1")
       {
           lblMensaje.Text = "Ingrese el estado civil";
           pasa = false;
       }

       if (tipoEmpleo == "-1")
       {
           lblMensaje.Text = "Ingrese el estado civil";
           pasa = false;
       }



        return pasa;
    }

    /*VALIDA CORREOS ELECTRÓNICOS*/
    private Boolean email_bien_escrito(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}