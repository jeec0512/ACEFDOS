using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Descripción breve de colaboradorCapaDatos
/// </summary>
/// 


namespace acefdos
{
    public class colaborador {
        public int Row { get; set; }
        public string Cedula { get; set; }
        public string apellidos { get; set; }
        public string nombres { get; set; }
        public string sucursal { get; set; }
        public string ccosto { get; set; }
        public bool activo { get; set; }
        public string mae_cue { get; set; }
        public int cargo { get; set; }
        public string telefono1 { get; set; }
        public string movil { get; set; }
        public DateTime fechaNac { get; set; }
        public DateTime fechaIng { get; set; }
        public int sexo { get; set; }
        public int estadoCivil { get; set; }
        public int tipoEmpleo { get; set; }
        public string foto { get; set; }
        public string textoAlterno { get; set; }
        public string emailDomicilio { get; set; }
        public string civil { get; set; }
        public string funcion { get; set; }
        public string emailCorporativo { get; set; }
        public string nombreCorto { get; set; }
        public bool instructorPractica { get; set; }
        public string relacion { get; set; }
        
    }


    public class colaboradorCapaDatos
    {

        public static void confirmarColaborador(List<string> colaboradoresIds)
        {
            string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {

                List<string> parameters = colaboradoresIds.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                string inClause = string.Join(",", parameters);
                string confirmaCommandText = "UPDATE tbl_colaborador SET activo = 1 WHERE Cedula IN (" + inClause + ")";

                SqlCommand cmd = new SqlCommand(confirmaCommandText, con);


                for (int i = 0; i < parameters.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i], colaboradoresIds[i]);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void confirmarColaboradores(string cedula)
        {
            string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tbl_colaborador  SET activo = 1 WHERE ceduka = @cedula", con);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@cedula";
                param.Value = cedula;
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static List<colaborador> getAllColaboradores()
        {
            List<colaborador> listColaboradores = new List<colaborador>();
            string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * FROM tbl_colaborador", con);


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
                    colaborador colaborador = new colaborador();
                    //estudiante.RNOTC_ID = Convert.ToInt32(rdr["RNOTC_ID"]);
                    colaborador.Row = Convert.ToInt32(rdr["Row"]);
                    colaborador.Cedula = Convert.ToString(rdr["cedula"]);
                    colaborador.apellidos = Convert.ToString(rdr["apellidos"]);
                    colaborador.nombres = Convert.ToString(rdr["nombres"]);
                    colaborador.sucursal = Convert.ToString(rdr["sucursal"]);
                    colaborador.ccosto = Convert.ToString(rdr["ccosto"]);
                    colaborador.activo = Convert.ToBoolean(rdr["activo"]);
                    colaborador.mae_cue = Convert.ToString(rdr["mae_cue"]);
                    colaborador.cargo = Convert.ToInt32(rdr["cargo"]);
                    colaborador.telefono1 = Convert.ToString(rdr["telefono1"]);
                    colaborador.movil = Convert.ToString(rdr["movil"]);
                    colaborador.fechaNac = Convert.ToDateTime(rdr["fechaNac"]);
                    colaborador.fechaIng = Convert.ToDateTime(rdr["fechaIng"]);
                    colaborador.sexo = Convert.ToInt32(rdr["sexo"]);
                    colaborador.estadoCivil = Convert.ToInt32(rdr["estadocivil"]);
                    colaborador.tipoEmpleo = Convert.ToInt32(rdr["tipoEmpleo"]);
                    colaborador.foto = Convert.ToString(rdr["foto"]);
                    colaborador.textoAlterno = Convert.ToString(rdr["textoAlterno"]);
                    colaborador.emailDomicilio = Convert.ToString(rdr["emailDomicilio"]);
                    colaborador.emailCorporativo = Convert.ToString(rdr["emailCorporativo"]);
                    colaborador.nombreCorto = Convert.ToString(rdr["nombreCorto"]);
                    colaborador.instructorPractica = Convert.ToBoolean(rdr["instructorPractica"]);

                  //colaborador.textoAlterno = Convert.ToString(rdr["civil"]);
                  //colaborador.textoAlterno = Convert.ToString(rdr["funcion"]);
                    //colaborador.relacion = Convert.ToString(rdr["relacion"]);
                    


                    listColaboradores.Add(colaborador);

                }

            }

            return listColaboradores;

        }

        public static List<colaborador> getAllColaboradores( string sucursal)
        {
            List<colaborador> listColaboradores = new List<colaborador>();
            string cs = ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select  ROW_NUMBER() OVER(ORDER BY APELLIDOS ASC) AS Row, b.descripcion as civil, c.descripcion as funcion, a.* , CASE WHEN tipoEmpleo = 1 THEN 'NOMINA' WHEN tipoEmpleo = 2 THEN 'FACTURA'  ELSE 'SIN ESPECIFICAR' END   AS relacion FROM tbl_colaborador A LEFT join  tbl_estadoCivil b on a. estadocivil = b.id_estadocivil left join tbl_cargo c on a.cargo = c.cargo where sucursal = @SUCURSAL AND ACTIVO = 1 ORDER BY APELLIDOS", con);


                SqlParameter param = new SqlParameter();
                //param.ParameterName = "@CUR_ID";
                //param.Value = 116;
                //cmd.Parameters.Add(param);
                param.ParameterName = "@SUCURSAL";
                param.Value = sucursal;
                cmd.Parameters.Add(param);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    colaborador colaborador = new colaborador();
                    //estudiante.RNOTC_ID = Convert.ToInt32(rdr["RNOTC_ID"]);
                    colaborador.Row = Convert.ToInt32(rdr["Row"]);
                    colaborador.Cedula = Convert.ToString(rdr["cedula"]);
                    colaborador.apellidos = Convert.ToString(rdr["apellidos"]);
                    colaborador.nombres = Convert.ToString(rdr["nombres"]);
                    colaborador.sucursal = Convert.ToString(rdr["sucursal"]);
                    colaborador.ccosto = Convert.ToString(rdr["ccosto"]);
                    colaborador.activo = Convert.ToBoolean(rdr["activo"]);
                    colaborador.mae_cue = Convert.ToString(rdr["mae_cue"]);
                    colaborador.cargo = Convert.ToInt32(rdr["cargo"]);
                    colaborador.telefono1 = Convert.ToString(rdr["telefono1"]);
                    colaborador.movil = Convert.ToString(rdr["movil"]);
                    colaborador.fechaNac = Convert.ToDateTime(rdr["fechaNac"]);
                    colaborador.fechaIng = Convert.ToDateTime(rdr["fechaIng"]);
                    colaborador.sexo = Convert.ToInt32(rdr["sexo"]);
                    colaborador.estadoCivil = Convert.ToInt32(rdr["estadocivil"]);
                    colaborador.tipoEmpleo = Convert.ToInt32(rdr["tipoEmpleo"]);
                    colaborador.foto = Convert.ToString(rdr["foto"]);
                    colaborador.textoAlterno = Convert.ToString(rdr["textoAlterno"]);
                    colaborador.emailDomicilio = Convert.ToString(rdr["emailDomicilio"]);
                    colaborador.emailCorporativo = Convert.ToString(rdr["emailCorporativo"]);
                    colaborador.nombreCorto = Convert.ToString(rdr["nombreCorto"]);
                    colaborador.instructorPractica = Convert.ToBoolean(rdr["instructorPractica"]);
                    colaborador.civil = Convert.ToString(rdr["civil"]);
                    colaborador.funcion = Convert.ToString(rdr["funcion"]);
                    colaborador.relacion = Convert.ToString(rdr["relacion"]);



                    listColaboradores.Add(colaborador);

                }

            }

            return listColaboradores;

        }

    }
}