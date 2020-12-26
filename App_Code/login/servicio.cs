using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace AuticationBDD
{
    public class Servicio
    {
        static string cadena = @"Data Source=192.168.1.106\ANETA;Initial Catalog=bddComprobantes;User ID=sa;Password=aneta2009";

        static SqlConnection conexion;

        public static Boolean validarUsuarioAutenticado(string usuario, string password) {
            Boolean autorizado = false;
            int valor = 0;
            if (!String.IsNullOrEmpty(usuario) && !String.IsNullOrEmpty(password)) {
                string sql = "SELECT TOP 1 "
                + "CASE "
                + "WHEN autorizacion = '" + Helper.EncodePassword(String.Concat(usuario, password)) + "' THEN 1 "
                + "ELSE 0 "
                + "END "
                + "FROM Usuario WHERE Username = '" + usuario + "' ";
                try
                {
                    conexion = new SqlConnection(cadena);
                    conexion.Open();

                    SqlCommand command = new SqlCommand(sql, conexion);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        valor = (int)reader.GetValue(0);
                        Console.WriteLine(valor);
                        break;
                    }
                    reader.Close();
                    command.Dispose();
                    conexion.Close();

                    if (valor == 1) {
                        autorizado = true;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return autorizado;
        }
    }
}