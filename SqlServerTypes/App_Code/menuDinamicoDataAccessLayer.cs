using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Descripción breve de menuDinamicoDataAccessLayer
/// </summary>
/// 

namespace acefdos
{
    public class menuDinamico
    {
        public int id_UsuarioMenuDinamico { get; set; }
        public int llave { get; set; }
        public int padre { get; set; }
        public string menu { get; set; }
        public string submenu { get; set; }
        public string boton	{ get; set; }
        public bool sis { get; set; }
        public bool ger { get; set; }
        public bool cntGen { get; set; }
        public bool cntAux { get; set; }
        public bool adm { get; set; }
        public bool counter { get; set; }
        public bool secCnt { get; set; }
        public bool secEscGen { get; set; }
        public bool secEsc { get; set; }
        public bool secEscTeo { get; set; }
        public bool secEscPra { get; set; }
        public bool socGen { get; set; }
        public bool socAux { get; set; }
        public bool vendedor { get; set; }
        public bool especial { get; set; }
        public bool externo { get; set; }
        public bool aud { get; set; }
        public bool tallAdm { get; set; }
        public bool socios { get; set; }
        public bool visitAneta { get; set; }
        public bool visitPub { get; set; }
        public bool secacadteoria { get; set; }
        public bool secacadpractica { get; set; }
        public bool secacadtalleres { get; set; }
        public bool certificadoIso { get; set; }
        public bool operadorSico { get; set; }
        public bool jefeFlota { get; set; }
        public bool calidad { get; set; }
	
    }
    public class menuDinamicoDataAccessLayer
    {

            public static void confirmarMenuDinamico(List<string> menuDinamicoIds)
            
             {
                string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    /*string strInClause = string.Empty;
                    foreach (string str in estudiantesIds) 
                    {
                        strInClause += str + ",";
                    }
                    strInClause = strInClause.Remove(strInClause.LastIndexOf(","));
                    string confirmaCommandText = "UPDATE TB_REGISTRO_NOTA_CON SET RNOTC_CONFIRMACION = 1 WHERE RNOTC_ID IN (" + strInClause + ")";
                    */

                    List<string> parameters =   menuDinamicoIds.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                    string inClause = string.Join(",", parameters);
                    string confirmaCommandText = "UPDATE tbl_UsuarioMenuDinamico SET sis = 1 WHERE id_UsuarioMenuDinamico IN (" + inClause + ")";

                   SqlCommand cmd = new SqlCommand(confirmaCommandText, con);
              

                    for (int i = 0; i < parameters.Count; i++) 
                    {
                        cmd.Parameters.AddWithValue(parameters[i], menuDinamicoIds[i]);
                    }

                      con.Open();
                    cmd.ExecuteNonQuery();
                }
            }


        public static void confirmarMenuDinamico(int  id_UsuarioMenuDinamico)
        {
            string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_UsuarioMenuDinamico SET sis = 1 WHERE  id_UsuarioMenuDinamico = @id_UsuarioMenuDinamico", con);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@id_UsuarioMenuDinamico";
                param.Value = id_UsuarioMenuDinamico;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static List<menuDinamico> getAllMenuDinamico() {
            List<menuDinamico> listMenuDinamico = new List<menuDinamico>();
            string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs)) {
                SqlCommand cmd = new SqlCommand("Select * FROM tbl_UsuarioMenuDinamico", con);
                
                
                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@CUR_ID";
                //param.Value = 116;
                //cmd.Parameters.Add(param);
                //param.ParameterName = "@SUCURSAL";
                //param.Value = "QTT";
                //cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) 
                {
                    menuDinamico menuDinamico = new menuDinamico();
                    menuDinamico.id_UsuarioMenuDinamico=Convert.ToInt32(rdr["id_UsuarioMenuDinamico"]);
                    menuDinamico.llave=Convert.ToInt32(rdr["llave"]);
                    menuDinamico.padre=Convert.ToInt32(rdr["padre"]);
                    menuDinamico.menu=rdr["menu"].ToString();
                    menuDinamico.submenu=rdr["submenu"].ToString();
                    menuDinamico.boton=rdr["boton"].ToString();
                    menuDinamico.sis=Convert.ToBoolean(rdr["sis"]);
                    menuDinamico.ger=Convert.ToBoolean(rdr["ger"]);
                   /* menuDinamico.cntGen=Convert.ToBoolean(rdr["cntGen"]);
                    menuDinamico.cntAux=Convert.ToBoolean(rdr["cntAux"]);
                    menuDinamico.adm=Convert.ToBoolean(rdr["adm"]);
                    menuDinamico.counter=Convert.ToBoolean(rdr["counter"]);
                    menuDinamico.secCnt=Convert.ToBoolean(rdr["secCnt"]);
                    menuDinamico.secEscGen=Convert.ToBoolean(rdr["secEscGen"]);
                    menuDinamico.secEsc=Convert.ToBoolean(rdr["secEsc"]);
                    menuDinamico.secEscTeo=Convert.ToBoolean(rdr["secEscTeo"]);
                    menuDinamico.secEscPra=Convert.ToBoolean(rdr["secEscPra"]);
                    menuDinamico.socGen=Convert.ToBoolean(rdr["socGen"]);
                    menuDinamico.socAux=Convert.ToBoolean(rdr["socAux"]);
                    menuDinamico.vendedor=Convert.ToBoolean(rdr["vendedor"]);
                    menuDinamico.especial=Convert.ToBoolean(rdr["especial"]);
                    menuDinamico.externo=Convert.ToBoolean(rdr["externo"]);
                    menuDinamico.aud=Convert.ToBoolean(rdr["aud"]);
                    menuDinamico.tallAdm=Convert.ToBoolean(rdr["tallAdm"]);
                    menuDinamico.socios=Convert.ToBoolean(rdr["socios"]);
                    menuDinamico.visitAneta=Convert.ToBoolean(rdr["visitAneta"]);
                    menuDinamico.visitPub=Convert.ToBoolean(rdr["visitPub"]);
                    menuDinamico.secacadteoria=Convert.ToBoolean(rdr["secacadteoria"]);
                    menuDinamico.secacadpractica=Convert.ToBoolean(rdr["secacadpractica"]);
                    menuDinamico.secacadtalleres=Convert.ToBoolean(rdr["secacadtalleres"]);
                    menuDinamico.certificadoIso=Convert.ToBoolean(rdr["certificadoIso"]);
                    menuDinamico.operadorSico=Convert.ToBoolean(rdr["operadorSico"]);
                    menuDinamico.jefeFlota=Convert.ToBoolean(rdr["jefeFlota"]);
                    menuDinamico.calidad=Convert.ToBoolean(rdr["calidad"]);*/

                    listMenuDinamico.Add(menuDinamico);

                }

            }

            return listMenuDinamico;

        }
    }
}