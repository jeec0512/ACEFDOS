using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// Descripción breve de Login
/// </summary>

    public static class LoginService
    {


        public static bool Autenticar(string usuario, string password)
        {
            //Declaramos la sentencia SQL
            string sql = @"SELECT COUNT(*)
                       FROM Usuario
                       WHERE Username = @usuario AND Contraseña = @password";

            //utilizamos using para indicarle al compilador que dentro de este bloque se llame al Método Dispose.
            //para así liberar recursos cuanto antes mejor. en este caso no ocupamos decirle que cierre la conexión a la base de datos.
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@usuario", usuario);
                string hash = Helper.EncodePassword(string.Concat(usuario, password));
                command.Parameters.AddWithValue("@password", hash);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count == 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool Autenticar2(string usuario, string password)
        {
            #region CONEXION BASE DE DATOS
            string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

            Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

            #endregion

            bool pasa = false;

            DateTime fecha = Convert.ToDateTime("09/09/2017");

            string contrasena = Helper.EncodePassword(string.Concat(usuario, password));

            var cAutentico = dc.sp_abmUsuario("AUTENTICAR", 0, 0, "", "", "", "", "", usuario, contrasena, "", fecha, 0, false, "", false);

            if (cAutentico.Count() <= 0)
            {
                pasa = false;
            }
            else
            {
                pasa = true;
            }
            return pasa;
        }

        public static bool inicializarSesion(string usuario, string password)
        {



            return true;
        }

        public static void Security(int userid, string usuario, DateTime ultimoacc, string ip)
        {
            string sql = @"INSERT INTO UsuarioSecurity(
                            UsuarioID
                           ,Username
                           ,UltimoAcceso
                           ,IPAcceso)
                        VALUES(
                            @UsuarioID,
                            @Username,
                            @UltimoAcceso,
                            @IPAcceso)
                            SELECT SCOPE_IDENTITY()";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.Parameters.AddWithValue("@UsuarioID", userid);
                command.Parameters.AddWithValue("@Username", usuario);
                command.Parameters.AddWithValue("@UltimoAcceso", ultimoacc);
                command.Parameters.AddWithValue("@IPAcceso", ip);

                conn.Open();

                int resultado = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public static DataTable prConsultaUsuario(string usuario, string password)
        {
            string sql = @"SELECT UsuarioID,Estado,Tipo
                            FROM Usuario
                            WHERE Username = @Username AND Contraseña = @password";


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@Username", usuario);
                string hash = Helper.EncodePassword(string.Concat(usuario, password));
                command.Parameters.AddWithValue("@password", hash);

                conn.Open();
                SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
                DataTable dtDatos = new DataTable();
                daAdaptador.Fill(dtDatos);
                return dtDatos;
            }
        }

        public static int prIngresarUsuario(string username, string contraseña, string email, DateTime fecharegistro, int estado, int tipo)
        {
            int resultado = -1;
            string sql = string.Format(@" INSERT INTO [Usuario]
                                                ([Username]
                                                ,[Contraseña]
                                                ,[Email]
                                                ,[FechaRegistro]
                                                ,[Estado]
                                                ,[Tipo])
                                            VALUES
                                                (@username
                                                ,@contraseña
                                                ,@email
                                                ,@fechaRegistro
                                                ,@estado 
                                                ,@tipo)");


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {

                SqlCommand command = new SqlCommand(sql, conn);

                command.Parameters.AddWithValue("@username", username);
                string hash = Helper.EncodePassword(string.Concat(username, contraseña));
                command.Parameters.AddWithValue("@contraseña", hash);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@fecharegistro", fecharegistro);
                command.Parameters.AddWithValue("@estado", estado);
                command.Parameters.AddWithValue("@tipo", tipo);


                conn.Open();
                resultado = Convert.ToInt32(command.ExecuteScalar());
                return resultado;
            }
        }

        public static bool calificarOpcion(string accion, string grupo, string menu, string submenu, string boton)
        {
            #region CONEXION BASE DE DATOS
            string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

            Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

            #endregion
            
          bool califica = false;

           using (SqlConnection conexion = new SqlConnection(conn1))
            {
                SqlCommand cmd = new SqlCommand("sp_CalificarOpcion", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", accion);
                cmd.Parameters.AddWithValue("@grupo", grupo);
                cmd.Parameters.AddWithValue("@menu", menu);
                cmd.Parameters.AddWithValue("@submenu", submenu);
                cmd.Parameters.AddWithValue("@boton", boton);
                
                try
                {
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //while(rdr.Read())    // En caso de que exista varios valores de retorno sin usar DataTable
                    // {
                    //califica = rdr.GetInt32(rdr.GetOrdinal("sp_CalificaOpcion"));
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        califica = rdr.GetBoolean(0);
                    }
                    
                    conexion.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
                return califica;
            }
        }

    }
