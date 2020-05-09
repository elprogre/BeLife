using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BibliotecaClases;

namespace Controlador
{
    public class DaoSexo
    {
        private SqlConnection cone;
        public DaoSexo()
        {
            cone = new Conexion().getConexion();
        }

        public Sexo Read(int idSexo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_sexo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 1).Value = idSexo;
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Sexo s = null;
                while (dr.Read())
                {
                    s = new Sexo();
                    s.IdSexo = int.Parse(dr[0].ToString());
                    s.Descripcion = dr[1].ToString();
                }
                cone.Close();
                return s;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Sexo> ReadAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_sexo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                List<Sexo> lista = new List<Sexo>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sexo s = new Sexo();
                    s.IdSexo = int.Parse(dr[0].ToString());
                    s.Descripcion = dr[1].ToString();
                    lista.Add(s);
                }
                cone.Close();
                return lista;

            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
