using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AuticationBDD
{
    public class Conexion
    {
        string cadena = @"Data Source=192.168.1.106\ANETA;Initial Catalog=bddComprobantes;User ID=sa;Password=aneta2009";

        private SqlConnection conexion;

        public Conexion() {
            obtenerConeccion();
        }

        public SqlConnection obtenerConeccion() {
            if (null == conexion) { conexion = new SqlConnection(cadena); }
            return conexion;
        }
    }
}