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
public partial class Egreso_verRetencion : System.Web.UI.Page
{
    public string lcre_sri;

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

    protected int traerCodigo()
    {
        int lId_InfoTributaria = 0;
        string lruc, lsuc, lestab, lptoemi, lsecuencial, lcoddoc;

        lcoddoc = "07";
        lsuc = ddlSucursal2.SelectedValue;
        lruc = txtBuscaRuc.Text.Trim();
        lestab = "";
        lptoemi = "";
        //traer serie de la sucursal

        var consultaSerie = from msuc in dc.tbl_ruc
                            where msuc.sucursal == lsuc
                            select new
                            {
                                estab = msuc.estab,
                                ptoemi = msuc.ptoemi
                            };
        if (consultaSerie.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaSerie)
            {
                lestab = registro.estab;
                lptoemi = registro.ptoemi;
            }
        }
        ;

        //llenar de ceros el secuencial
        txtBuscaSecuencial.Text = llenarCeros(txtBuscaSecuencial.Text.Trim(), '0', 9);
        lsecuencial = txtBuscaSecuencial.Text.Trim();

        var consulta = from Tinftrib in dc.tbl_infotributaria
                       where Tinftrib.coddoc == lcoddoc &&
                                Tinftrib.ruc == lruc &&
                                Tinftrib.estab == lestab &&
                                Tinftrib.ptoemi == lptoemi &&
                                Tinftrib.secuencial == lsecuencial
                       select new { idInfo = Tinftrib.id_infotributaria };
        if (consulta.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consulta)
            {
                lId_InfoTributaria = registro.idInfo;
            }
        }
        return lId_InfoTributaria;
    }

    protected void encerarValores()
    {
        lblMensaje.Text = "";
        txtBuscaRuc.Text = "";
        txtBuscaSecuencial.Text = "";
    }
    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
    protected int traer_Id_infoCompRetencion(int lid_infotributaria)
    {
        int lId_infoCompRetencion = 0;
        var consulta = from Tcompret in dc.tbl_infoCompRetencion
                       where Tcompret.id_infotributaria == lid_infotributaria
                       select new { idcompret = Tcompret.id_infoCompRetencion };
        if (consulta.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consulta)
            {
                lId_infoCompRetencion = registro.idcompret;
            }
        }
        return lId_infoCompRetencion;
    }
    #endregion

    protected void btnTraerRetencion_Click(object sender, EventArgs e)
    {
        btnTraerRetencion_Click();
    }

    protected void btnTraerRetencion_Click()
    {
        var consultaSuc = new object();
        /****************************/

        int tipo = (int)Session["STipo"];

        if (tipo == 4)
        {
            btnAnularRet.Visible = true;
        }
        else
        {
            btnAnularRet.Visible = false;
        }

        int lId_InfoTributaria = traerCodigo(), sihay = 0;
        var consultaRET = from a in dc.tbl_matriz
                          from b in dc.tbl_ruc
                          from c in dc.tbl_infotributaria
                          from d in dc.tbl_infoCompRetencion
                          from ret in dc.tbl_impuestosRet
                          where a.ruc == b.ruc
                            && b.ruc == c.ruc
                            && b.coddoc == c.coddoc
                            && b.estab == c.estab
                            && b.ptoemi == c.ptoemi
                            && c.id_infotributaria == d.id_infotributaria
                            && d.id_infoCompRetencion == ret.id_infoCompRetencion
                            && c.id_infotributaria == lId_InfoTributaria
                          select new
                          {
                              retencion =  c.estab+ c.ptoemi+ c.secuencial,
                              cre_sri = c.cre_sri,
                              codigo = ret.codigo,
                              codigoRetencion = ret.codigoRetencion,
                              baseImponible = ret.baseImponible,
                              porcentajeRetener = ret.porcentajeRetener,
                              valorRetenido = ret.valorRetenido,
                              codDocSustento = ret.codDocSustento,
                              numDocSustento = ret.numDocSustento,
                              fechaEmisionDocSustento = ret.fechaEmisionDocSustento
                          };


        if (consultaRET.Count() == 0)
        {
        }
        else
        {
            foreach (var registro in consultaRET)
            {
                lcre_sri = Convert.ToString(registro.cre_sri).Trim();
                sihay = 1;
            }
        }

        grvListadoRuc.DataSource = consultaRET;
        grvListadoRuc.DataBind();



        if (lcre_sri == "AUTORIZADO" || lcre_sri == "ANULADO")
        {
            desactivarBotones();
            if (lcre_sri == "ANULADO")
            {
                btnAnularRet.Visible = false;
            }

            if (lcre_sri == "AUTORIZADO" && tipo == 4)
            {
                btnAnularRet.Visible = true;
            }

        }
        else
        {

            if (sihay == 1)
            {
                activarBotones();
            }
            else
            {
                desactivarBotones();
            }
            btnAnularRet.Visible = false;
        }
    }

    protected void activarBotones()
    {
        btnEnviarRet.Visible = false;
        btnBorrarRet.Visible = true;
        btnCancelarRet.Visible = true;
    }

    protected void desactivarBotones()
    {
        btnEnviarRet.Visible = false;
        btnBorrarRet.Visible = false;
        btnCancelarRet.Visible = false;
        btnAnularRet.Visible = false;
    }


    protected void btnEnviarRet_Click(object sender, EventArgs e)
    {
       

        int lId_InfoTributaria = traerCodigo();
        int kont = 0;
        string ldi1, lte1, le_mail;
        string lambiente, ltipoemision,
                lcodigo, lcadena, cadena1, cadena2, cadena3, cadena4, cadena5, cadena6, cadena7,
                lrazonsocial,
                lnombrecomercial,
                lruc,
                lsujetoRuc,
                lclaveacceso,
                lcoddoc,
                lestab,
                lptoemi,
                lsecuencial,
                ldirMatriz,
                lfechaemision,
                ldirestablecimiento,
                lcontribuyenteEspecial,
                lobligadoContabilidad,
                ltipoIdentificacionSujetoRetenido,
                lrazonSocialSujetoRetenido,
                lidentificacionSujetoRetenido,
                lperiodoFiscal,
                lcodigoRetencion,
                lbaseImponible,
                lporcentajeRetener,
                lvalorRetenido,
                lcodDocSustento,
                lnumDocSustento,
                lfechaEmisionDocSustento;

        lcadena = "";
        lcadena = "0";
        cadena1 = "0";
        cadena2 = "0";
        cadena3 = "0";
        cadena4 = "0";
        cadena5 = "0";
        cadena6 = "0";
        cadena7 = "";


        lambiente = "";
        ltipoemision = "";
        lrazonsocial = "";
        lnombrecomercial = "";
        lruc = "";
        //
        ///pruc = txtProveedor.Text.Trim();
        ///
        lsujetoRuc = "";
        lclaveacceso = "";
        lcoddoc = "";
        lestab = "";
        lptoemi = "";
        lsecuencial = "";
        ldirMatriz = "";
        lfechaemision = "";
        ldirestablecimiento = "";
        lcontribuyenteEspecial = "";
        lobligadoContabilidad = "";
        ltipoIdentificacionSujetoRetenido = "";
        lrazonSocialSujetoRetenido = "";
        lidentificacionSujetoRetenido = "";
        lperiodoFiscal = "";
        lcodigoRetencion = "";
        lbaseImponible = "";
        lporcentajeRetener = "";
        lvalorRetenido = "";
        lcodDocSustento = "";
        lnumDocSustento = "";
        lfechaEmisionDocSustento = "";

        var consultaRC = from a in dc.tbl_matriz
                         from b in dc.tbl_ruc
                         from c in dc.tbl_infotributaria
                         from d in dc.tbl_infoCompRetencion
                         where a.ruc == b.ruc
                            && b.ruc == c.ruc
                            && b.coddoc == c.coddoc
                            && b.estab == c.estab
                            && b.ptoemi == c.ptoemi
                            && c.id_infotributaria == d.id_infotributaria
                            && c.id_infotributaria == lId_InfoTributaria
                         select new
                         {
                             ambiente = c.ambiente,
                             tipoemision = c.tipoemision,
                             razonsocial = b.razonsocial,
                             nombrecomercial = b.nombreComercial,
                             ruc = b.ruc,
                             claveacceso = c.claveacceso,
                             coddoc = b.coddoc,
                             estab = b.estab,
                             ptoemi = b.ptoemi,
                             secuencial = c.secuencial,
                             dirMatriz = a.dirMatriz,
                             fechaemision = d.fechaEmision,
                             direstablecimiento = b.dirEstablecimiento,
                             contribuyenteEspecial = a.contribuyenteEspecial,
                             obligadoContabilidad = a.obligadoContabilidad,
                             tipoIdentificacionSujetoRetenido = d.tipoIdentificacionSujetoRetenido,
                             razonSocialSujetoRetenido = d.razonSocialSujetoRetenido,
                             identificacionSujetoRetenido = d.identificacionSujetoRetenido,
                             periodoFiscal = d.periodoFiscal
                         };




        /****************/

        var consultaRD = from a in dc.tbl_matriz
                         from b in dc.tbl_ruc
                         from c in dc.tbl_infotributaria
                         from d in dc.tbl_infoCompRetencion
                         from ret in dc.tbl_impuestosRet
                         where a.ruc == b.ruc
                           && b.ruc == c.ruc
                           && b.coddoc == c.coddoc
                           && b.estab == c.estab
                           && b.ptoemi == c.ptoemi
                           && c.id_infotributaria == d.id_infotributaria
                           && d.id_infoCompRetencion == ret.id_infoCompRetencion
                           && c.id_infotributaria == lId_InfoTributaria
                         select new
                         {
                             sujetoRuc = d.identificacionSujetoRetenido,
                             codigo = ret.codigo,
                             codigoRetencion = ret.codigoRetencion,
                             baseImponible = ret.baseImponible,
                             porcentajeRetener = ret.porcentajeRetener,
                             valorRetenido = ret.valorRetenido,
                             codDocSustento = ret.codDocSustento,
                             numDocSustento = ret.numDocSustento,
                             fechaEmisionDocSustento = ret.fechaEmisionDocSustento
                         };



        /*********************/
        /*VERIFICA DATOS */
        /*********************/

        if (consultaRC.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaRC)
            {
                lambiente = Convert.ToString(registro.ambiente);
                ltipoemision = Convert.ToString(registro.tipoemision);
                lrazonsocial = registro.razonsocial;
                lnombrecomercial = registro.nombrecomercial;
                lruc = registro.ruc;
                lclaveacceso = registro.claveacceso;
                lcoddoc = registro.coddoc;
                lestab = registro.estab;
                lptoemi = registro.ptoemi;
                lsecuencial = registro.secuencial;
                ldirMatriz = registro.dirMatriz;
                lfechaemision = Convert.ToString(registro.fechaemision).Substring(0, 10);
                ldirestablecimiento = registro.direstablecimiento.Trim();
                lcontribuyenteEspecial = registro.contribuyenteEspecial;
                lobligadoContabilidad = registro.obligadoContabilidad;
                ltipoIdentificacionSujetoRetenido = registro.tipoIdentificacionSujetoRetenido;
                lrazonSocialSujetoRetenido = registro.razonSocialSujetoRetenido;
                lidentificacionSujetoRetenido = registro.identificacionSujetoRetenido.Trim();
                lperiodoFiscal = registro.periodoFiscal;
            }
        }

        if (consultaRD.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaRD)
            {
                lcadena = "";
                lsujetoRuc = Convert.ToString(registro.sujetoRuc).Trim();
                lcodigo = Convert.ToString(registro.codigo);
                lcodigoRetencion = registro.codigoRetencion.Trim();
                lbaseImponible = Convert.ToString(registro.baseImponible).Replace(',', '.').Trim();
                lporcentajeRetener = registro.porcentajeRetener.Replace(',', '.').Trim();
                lvalorRetenido = Convert.ToString(registro.valorRetenido).Replace(',', '.').Trim();
                lcodDocSustento = registro.codDocSustento;
                lnumDocSustento = registro.numDocSustento;
                lfechaEmisionDocSustento = Convert.ToString(registro.fechaEmisionDocSustento).Substring(0, 10);
                lcadena = "*z%" + "*y%" +
                            "*a%" + lcodigo + "*/a%" +
                            "*b%" + lcodigoRetencion + "*/b%" +
                            "*c%" + lbaseImponible + "*/c%" +
                            "*d%" + lporcentajeRetener + "*/d%" +
                            "*e%" + lvalorRetenido + "*/e%" +
                            "*f%" + lcodDocSustento + "*/f%" +
                            "*g%" + lnumDocSustento + "*/g%" +
                            "*h%" + lfechaEmisionDocSustento + "*/h%" +
                            "*/y%" + "*/z%";

                kont++;
                if (kont == 1)
                {
                    cadena1 = lcadena;
                }
                if (kont == 2)
                {
                    cadena2 = lcadena;
                }
                if (kont == 3)
                {
                    cadena3 = lcadena;
                }
                if (kont == 4)
                {
                    cadena4 = lcadena;
                }
                if (kont == 5)
                {
                    cadena5 = lcadena;
                }
                if (kont == 6)
                {
                    cadena6 = lcadena;
                }
            }
        }

        /**********CONSULTA DATOS DEL SUJETO RETENIDO*******/
        var consultaP = from provee in dc.tbl_matriz
                        where provee.ruc == lsujetoRuc
                        select new { provee.dirMatriz, provee.e_mail, provee.telefono };

        if (consultaP.Count() == 0)
        {


        }
        else
        {
            foreach (var registro in consultaP)
            {
                ldi1 = registro.dirMatriz.Trim();
                lte1 = registro.telefono.Trim();
                le_mail = registro.e_mail.Trim();
                cadena7 = "*z%" + "*y%" +
                    "*a%" + ldi1 + "*/a%" +
                    "*b%" + lte1 + "*/b%" +
                    "*c%" + le_mail + "*/c%" +
                    "*d%" + "" + "*/d%" +
                    "*e%" + "" + "*/e%" +
                    "*f%" + "" + "*/f%" +
                    "*g%" + "" + "*/g%" +
                    "*/y%" + "*/z%";
            }
        }


        if (lclaveacceso == null)
        {
            lclaveacceso = "";
        }

        try
        {
            wsSRI.WebService1SoapClient servicio = new wsSRI.WebService1SoapClient();

            servicio.obNomarticulo(lambiente, ltipoemision, lrazonsocial, lnombrecomercial, lruc, lclaveacceso, lcoddoc, lestab, lptoemi, lsecuencial, ldirMatriz,
                lfechaemision, ldirestablecimiento, lcontribuyenteEspecial, lobligadoContabilidad, ltipoIdentificacionSujetoRetenido, lrazonSocialSujetoRetenido, lidentificacionSujetoRetenido,
                lperiodoFiscal, cadena1, cadena2, cadena3, cadena4, cadena5, cadena6, cadena7);

            /*GUARDA OBSERVACION QUE EMITE EL SRI*/

            ///int nGrabados = -1;
            try
            {
                dc.sp_observacionSRI(lruc, lcoddoc, lestab, lptoemi, lsecuencial, "");
                btnTraerRetencion_Click();
                ///nGrabados = 0;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
            finally
            {

            }
            /*******************************************/

        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }

        finally
        {
        }

    }


    protected void btnBorrarRet_Click(object sender, EventArgs e)
    {
        string laccion = "BORRAR";
        int lId_InfoTributaria = traerCodigo();
        int lId_infoCompRetencion = traer_Id_infoCompRetencion(lId_InfoTributaria);

        try
        {
            dc.sp_abmCascada(laccion, lId_InfoTributaria, lId_infoCompRetencion);
            lblMensaje.Text = "Documento eliminado";
            desactivarBotones();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        finally
        {
        }

    }

    protected void btnAnularRet_Click(object sender, EventArgs e)
    {
        string laccion = "ANULAR";
        int lId_InfoTributaria = traerCodigo();
        int lId_infoCompRetencion = traer_Id_infoCompRetencion(lId_InfoTributaria);

        try
        {
            dc.sp_abmCascada(laccion, lId_InfoTributaria, lId_infoCompRetencion);
            lblMensaje.Text = "Documento anulado";
            desactivarBotones();
        }
        catch (Exception ex)
        {
            lblMensaje.Text = ex.Message;
        }
        finally
        {
        }
    }

    protected void btnCancelarRet_Click(object sender, EventArgs e)
    {
        desactivarBotones();
        encerarValores();
    }
    protected void grvListadoRuc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grvListadoRuc_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string estado = string.Empty;
        string descEstado = string.Empty;

        if (e.CommandName == "modReg")
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvListadoRuc.Rows[indice];
            int id_pregunta = row.DataItemIndex;
            string lretencion = Convert.ToString(row.Cells[10].Text).Trim();

            var cAutoriz = from mAutoriz in df.COMPROBANTERETENCION
                        where mAutoriz.CRE_ESTABLECIMIENTO + mAutoriz.CRE_PUNTOEMISION + mAutoriz.CRE_SECUENCIAL == lretencion
                        select new 
                        {
                            cre_estado = mAutoriz.CRE_ESTADO
                        };

            if (cAutoriz.Count() <= 0)
            {

            }
            else
            {
                foreach (var registro in cAutoriz)
                {
                    estado = registro.cre_estado.Trim();
                }
            }


            lblMensaje.Text = lretencion + "-" + estado;

            if(estado =="A"){
                    descEstado = "AUTORIZADO";
                    tbl_infotributaria tbl_infotributaria = dc.tbl_infotributaria.SingleOrDefault(p => p.estab + p.ptoemi + p.secuencial == lretencion);
                    tbl_infotributaria.cre_sri = descEstado;
                    dc.SubmitChanges();

            }

            btnTraerRetencion_Click();

        }

        
    }
}