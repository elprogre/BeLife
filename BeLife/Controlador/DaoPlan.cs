using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BibliotecaClases;

namespace Controlador
{
    public class DaoPlan
    {
        private SqlConnection cone;
        public DaoPlan()
        {
            cone = new Conexion().getConexion();
        }

        public Plan Read(string idPlan)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_plan";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@id", System.Data.SqlDbType.NVarChar, 5).Value = idPlan;

                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Plan p = null;
                while (dr.Read())
                {
                    p = new Plan();
                    p.IdPlan = dr[0].ToString();
                    p.Nombre = dr[1].ToString();
                    p.PrimaBase = float.Parse(dr[2].ToString());
                    p.PolizaActual = dr[3].ToString();
                }
                cone.Close();
                return p;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Plan> ReadAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_plan";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                List<Plan> lista = new List<Plan>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Plan p = new Plan();
                    p.IdPlan = dr[0].ToString();
                    p.Nombre = dr[1].ToString();
                    p.PrimaBase = float.Parse(dr[2].ToString());
                    p.PolizaActual = dr[3].ToString();
                    lista.Add(p);
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
