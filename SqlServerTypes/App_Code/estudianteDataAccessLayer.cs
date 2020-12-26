using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Descripción breve de estudianteDataAccessLayer
/// </summary>
/// 

namespace acefdos
{
    public class estudiante { 
        public int RNOTC_ID { get; set;}
        public int REG_ID { get; set;}
        public string RNOTC_CIRUC { get; set;}
        public string RNOTC_APELLIDOS { get; set;}
        public string RNOTC_NOMBRES { get; set;}
        public string RNOTC_LICENCIA { get; set;}
        public int RNOTC_EDUC_VIAL_ASIS { get; set;}
        public decimal RNOTC_EDUC_VIAL_NOTA { get; set;}
        public decimal RNOTC_EDUC_VIAL_SUP1 { get; set;}
        public decimal RNOTC_EDUC_VIAL_SUP2 { get; set;}
        public bool RNOTC_EDUC_VIAL_ESTA { get; set;}
        public int RNOTC_PRAC_ASIS { get; set;}
        public decimal RNOTC_PRAC_NOTA { get; set;}
        public decimal RNOTC_PRAC_SUP1 { get; set;}
        public decimal RNOTC_PRAC_SUP2 { get; set;}
        public bool RNOTC_PRAC_ESTA { get; set;}
        public int RNOTC_PSIC_ASIS { get; set;}
        public bool RNOTC_PSIC_ESTA { get; set;}
        public int RNOTC_PAUX_ASIS { get; set;}
        public bool RNOTC_PAUX_ESTA { get; set;}
        public int RNOTC_MEC_ASIS { get; set;}
        public bool RNOTC_MEC_ESTA { get; set;}
        public bool RNOTC_PETR_ESTA { get; set;}
        public string RNOTC_FACT { get; set;}
        public bool RNOTC_FACT_ESTA { get; set;}
        public bool RNOTC_APROBADO { get; set;}
        public string RNOTC_OP1 { get; set;}
        public string RNOTC_OP3 { get; set;}
        public string RNOTC_OP4 { get; set;}
        public string RNOTC_OP5 { get; set;}
        public string RNOTC_OP6 { get; set;}
        public string RNOTC_OP7 { get; set;}
        public string RNOTC_OBSERVACIONES { get; set;}
        public int RNOTC_USU_MODIFICA { get; set;}
        public string RNOTC_PEDIDO_TITULOS { get; set;}
        public int RNOTC_TITULO { get; set;}
        public string RNOTC_ACTA { get; set; }
        public bool RNOTC_CONFIRMACION { get; set;}
        public bool RNOTC_BLOQUEO_RNOTAS { get; set;}
        public bool RNOTC_ENVIADO { get; set;}
        public int CUR_ID { get; set; }
    }
    public class estudianteDataAccessLayer
    {

        public static void confirmarEstudiantes(List<string> estudiantesIds)
        {
            string cs = ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;
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

                List<string> parameters = estudiantesIds.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                string inClause = string.Join(",", parameters);
                string confirmaCommandText = "UPDATE TB_REGISTRO_NOTA_CON SET RNOTC_CONFIRMACION = 1 WHERE RNOTC_ID IN (" + inClause + ")";

               SqlCommand cmd = new SqlCommand(confirmaCommandText, con);
              

                for (int i = 0; i < parameters.Count; i++) 
                {
                    cmd.Parameters.AddWithValue(parameters[i], estudiantesIds[i]);
                }

                  con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void confirmarEstudiantes(int RNOTC_ID)
        {
            string cs = ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("UPDATE TB_REGISTRO_NOTA_CON SET RNOTC_CONFIRMACION = 1 WHERE RNOTC_ID = @RNOTC_ID", con);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@RNOTC_ID";
                param.Value = RNOTC_ID;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static List<estudiante> getAllEstudiantes() {
            List<estudiante> listEstudiantes = new List<estudiante>();
            string cs = ConfigurationManager.ConnectionStrings["DB_ESCUELAConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs)) {
                SqlCommand cmd = new SqlCommand("Select * FROM TB_CURSO a inner join TB_REGISTRO_ALUMNO b  inner join TB_ALUMNO d  on d.alu_id = b.alu_id on a.cur_id = b.cur_id inner join TB_REGISTRO_NOTA_CON  c on b.reg_id = c.reg_id where reg_sucursal = @SUCURSAL and a.cur_id=116", con);
                
                
                SqlParameter param = new SqlParameter();
                //param.ParameterName = "@CUR_ID";
                //param.Value = 116;
                //cmd.Parameters.Add(param);
                param.ParameterName = "@SUCURSAL";
                param.Value = "QTT";
                cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) 
                {
                    estudiante estudiante = new estudiante();
                    estudiante.RNOTC_ID	=	Convert.ToInt32(rdr["RNOTC_ID"]);
                    estudiante.REG_ID	=	Convert.ToInt32(rdr["REG_ID"]);
                    estudiante.RNOTC_CIRUC	=	rdr["RNOTC_CIRUC"].ToString();
                    estudiante.RNOTC_APELLIDOS	=	rdr["RNOTC_APELLIDOS"].ToString();
                    estudiante.RNOTC_NOMBRES = rdr["RNOTC_NOMBRES"].ToString();
                    estudiante.RNOTC_LICENCIA = rdr["RNOTC_LICENCIA"].ToString();
                    estudiante.RNOTC_EDUC_VIAL_ASIS	=	Convert.ToInt32(rdr["RNOTC_EDUC_VIAL_ASIS"]);
                    estudiante.RNOTC_EDUC_VIAL_NOTA	=	Convert.ToDecimal(rdr["RNOTC_EDUC_VIAL_NOTA"]);
                    estudiante.RNOTC_EDUC_VIAL_SUP1	=	Convert.ToDecimal(rdr["RNOTC_EDUC_VIAL_SUP1"]);
                    estudiante.RNOTC_EDUC_VIAL_SUP2	=	Convert.ToDecimal(rdr["RNOTC_EDUC_VIAL_SUP2"]);
                    estudiante.RNOTC_EDUC_VIAL_ESTA	=	Convert.ToBoolean(rdr["RNOTC_EDUC_VIAL_ESTA"]);
                    estudiante.RNOTC_PRAC_ASIS	=	Convert.ToInt32(rdr["RNOTC_PRAC_ASIS"]);
                    estudiante.RNOTC_PRAC_NOTA	=	Convert.ToDecimal(rdr["RNOTC_PRAC_NOTA"]);
                    estudiante.RNOTC_PRAC_SUP1	=	Convert.ToDecimal(rdr["RNOTC_PRAC_SUP1"]);
                    estudiante.RNOTC_PRAC_SUP2	=	Convert.ToDecimal(rdr["RNOTC_PRAC_SUP2"]);
                    estudiante.RNOTC_PRAC_ESTA	=	Convert.ToBoolean(rdr["RNOTC_PRAC_ESTA"]);
                    estudiante.RNOTC_PSIC_ASIS	=	Convert.ToInt32(rdr["RNOTC_PSIC_ASIS"]);
                    estudiante.RNOTC_PSIC_ESTA	=	Convert.ToBoolean(rdr["RNOTC_PSIC_ESTA"]);
                    estudiante.RNOTC_PAUX_ASIS	=	Convert.ToInt32(rdr["RNOTC_PAUX_ASIS"]);
                    estudiante.RNOTC_PAUX_ESTA	=	Convert.ToBoolean(rdr["RNOTC_PAUX_ESTA"]);
                    estudiante.RNOTC_MEC_ASIS	=	Convert.ToInt32(rdr["RNOTC_MEC_ASIS"]);
                    estudiante.RNOTC_MEC_ESTA	=	Convert.ToBoolean(rdr["RNOTC_MEC_ESTA"]);
                    estudiante.RNOTC_PETR_ESTA	=	Convert.ToBoolean(rdr["RNOTC_PETR_ESTA"]);
                    estudiante.RNOTC_FACT	=	rdr["RNOTC_FACT"].ToString();
                    estudiante.RNOTC_FACT_ESTA = Convert.ToBoolean(rdr["RNOTC_FACT_ESTA"]);
                    estudiante.RNOTC_APROBADO	=	Convert.ToBoolean(rdr["RNOTC_APROBADO"]);
                    estudiante.RNOTC_OP1	=	rdr["RNOTC_OP1"].ToString();
                    estudiante.RNOTC_OP3	=	rdr["RNOTC_OP3"].ToString();
                    estudiante.RNOTC_OP4	=	rdr["RNOTC_OP4"].ToString();
                    estudiante.RNOTC_OP5	=	rdr["RNOTC_OP5"].ToString();
                    estudiante.RNOTC_OP6	=	rdr["RNOTC_OP6"].ToString();
                    estudiante.RNOTC_OP7	=	rdr["RNOTC_OP7"].ToString();
                    estudiante.RNOTC_OBSERVACIONES	=	rdr["RNOTC_OBSERVACIONES"].ToString();
                    estudiante.RNOTC_USU_MODIFICA	=	Convert.ToInt32(rdr["RNOTC_USU_MODIFICA"]);
                    estudiante.RNOTC_PEDIDO_TITULOS	=	rdr["RNOTC_PEDIDO_TITULOS"].ToString();
                    estudiante.RNOTC_TITULO = Convert.ToInt32(rdr["RNOTC_TITULO"]);
                    estudiante.RNOTC_ACTA	=	rdr["RNOTC_ACTA"].ToString();
                    estudiante.RNOTC_CONFIRMACION	=	Convert.ToBoolean(rdr["RNOTC_CONFIRMACION"]);
                    estudiante.RNOTC_BLOQUEO_RNOTAS	=	Convert.ToBoolean(rdr["RNOTC_BLOQUEO_RNOTAS"]);
                    estudiante.RNOTC_ENVIADO = Convert.ToBoolean(rdr["RNOTC_ENVIADO"]);
                    estudiante.CUR_ID = Convert.ToInt32(rdr["CUR_ID"]);

                    listEstudiantes.Add(estudiante);

                }

            }

            return listEstudiantes;

        }

    }


}