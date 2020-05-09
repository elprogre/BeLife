using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Controlador
{
    public class Conexion
    {
        private SqlConnection cone;
        private string cadena = @"Data Source=DESKTOP-M3NQ910;Initial Catalog=BeLife;Integrated Security=True";
        public Conexion()
        {
            if (cone==null)
            {
                cone = new SqlConnection(cadena);
            } 
        }

        public SqlConnection getConexion()
        {
            return cone;
        }
    }
}
