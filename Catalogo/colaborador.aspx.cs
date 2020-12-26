using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_colaborador : System.Web.UI.Page
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
            lblmensaje.Text = string.Empty;
        }
    }
    #endregion

    #region PROCESOS
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
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
                            };


            llenarListados();
            blanquearObjetos();

            foreach (var registro in cColabora)
            {
                lblmensaje.Text = string.Empty;
                txtCedula.Text = registro.Cedula;
                txtApellidos.Text = registro.apellidos;
                txtNombres.Text = registro.nombres;
                ddlSucursal.SelectedValue = registro.sucursal;
                ddlCcosto.SelectedValue = registro.ccosto;
                chkActivo.Checked = Convert.ToBoolean(registro.activo);
                ddlCtaCble.SelectedValue = registro.mae_cue;
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
        string Accion, Cedula, apellidos, nombres, sucursal, ccosto,mae_cue;
        bool activo;

        Accion = "GUARDAR";


        Cedula = txtCedula.Text.Trim();
        apellidos = txtApellidos.Text.Trim();
        nombres = txtNombres.Text.Trim();
        sucursal = ddlSucursal.SelectedValue;
        ccosto = ddlCcosto.SelectedValue;
        activo = chkActivo.Checked;
        mae_cue = ddlCtaCble.SelectedValue;
        if (Cedula.Length < 10
           || apellidos.Length <= 2
           || nombres.Length <= 2)
        {
            lblmensaje.Text = "Ingrese toda la información,identificación válido,nombres, apellidos,etc ";
        }
        else
        {
            if (sucursal == "-1" || ccosto == "-1")
            {
                lblmensaje.Text = "Ingrese sucursal y centro de costo ";
            }
            else
            {
                /*GUARDAR INFORMACION*/
                dc.sp_abmColaborador(Accion, Cedula, apellidos, nombres, sucursal, ccosto,activo,mae_cue,0,"","",DateTime.Now,DateTime.Now,0,0,0,"","","","","",false);
                blanquearObjetos();
                lblmensaje.Text = apellidos.Trim() + " " + nombres.Trim() + " guardado correctamente";
            }
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

        /* TRAER SUCURSAL*/
        var cSuc = from msuc in dc.tbl_ruc
                   where msuc.activo == true
                   orderby msuc.sucursal
                   select new
                   {
                       sucursal = msuc.sucursal.Trim()
                    ,
                       nom_suc = msuc.sucursal.Trim() + " " + msuc.nom_suc.Trim()
                   };


        ddlSucursal.DataSource = cSuc;
        ddlSucursal.DataBind();

        ListItem listSuc = new ListItem("Seleccione sucursal", "-1");
        ddlSucursal.Items.Insert(0, listSuc);

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
    }

    protected void blanquearObjetos()
    {
        //txtCedula.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        ddlSucursal.SelectedValue = "-1";
        ddlCcosto.SelectedValue = "-1";
        chkActivo.Checked = false;
    }
    #endregion

}