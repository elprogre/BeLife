using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BibliotecaClases;

namespace Controlador
{
    public class DaoEstadoCivil
    {
        private SqlConnection cone;
        public DaoEstadoCivil()
        {
            cone = new Conexion().getConexion();
        }

        public EstadoCivil Read(int idEc)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_estadocivil";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 1).Value = idEc;
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                EstadoCivil e = null;
                while (dr.Read())
                {
                    e = new EstadoCivil();
                    e.IdEstadoCivil = int.Parse(dr[0].ToString());
                    e.Descripcion = dr[1].ToString();
                }
                cone.Close();
                return e;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<EstadoCivil> ReadAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_estadocivil";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                List<EstadoCivil> lista = new List<EstadoCivil>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    EstadoCivil e = new EstadoCivil();
                    e.IdEstadoCivil = int.Parse(dr[0].ToString());
                    e.Descripcion = dr[1].ToString();
                    lista.Add(e);
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
