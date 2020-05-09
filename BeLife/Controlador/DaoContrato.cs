using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BibliotecaClases;

namespace Controlador
{
    public class DaoContrato
    {
        private SqlConnection cone;
        public DaoContrato()
        {
            cone = new Conexion().getConexion();
        }


        public Contrato Read(string numero)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_contrato";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@numero", System.Data.SqlDbType.NVarChar, 14).Value = numero;

                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Contrato c = null;
                while (dr.Read())
                {
                    c = new Contrato();
                    c.Numero = dr[0].ToString();
                    c.FechaCreacion = DateTime.Parse(dr[1].ToString());
                    c.Cliente = new DaoCliente().Read(dr[2].ToString());
                    c.Plan = new DaoPlan().Read(dr[3].ToString());
                    c.FechaInicioVigencia = DateTime.Parse(dr[4].ToString());
                    c.FechaFinVigencia = DateTime.Parse(dr[5].ToString());
                    c.Vigente = dr[6].ToString().Equals("1") ? true : false;
                    c.DeclaracionSalud = dr[7].ToString().Equals("1") ? true : false;
                    c.PrimaAnual = float.Parse(dr[8].ToString());
                    c.PrimaMensual= float.Parse(dr[9].ToString());
                    c.Observaciones = dr[10].ToString();
                }
                cone.Close();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<Contrato> ReadAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_contrato";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                List<Contrato> lista = new List<Contrato>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Contrato c = new Contrato();
                    c.Numero = dr[0].ToString();
                    c.FechaCreacion = DateTime.Parse(dr[1].ToString());
                    c.Cliente = new DaoCliente().Read(dr[2].ToString());
                    c.Plan = new DaoPlan().Read(dr[3].ToString());
                    c.FechaInicioVigencia = DateTime.Parse(dr[4].ToString());
                    c.FechaFinVigencia = DateTime.Parse(dr[5].ToString());
                    c.Vigente = dr[6].ToString().Equals("1") ? true : false;
                    c.DeclaracionSalud = dr[7].ToString().Equals("1") ? true : false;
                    c.PrimaAnual = float.Parse(dr[8].ToString());
                    c.PrimaMensual = float.Parse(dr[9].ToString());
                    c.Observaciones = dr[10].ToString();
                    lista.Add(c);
                }
                cone.Close();
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool CREATE(Contrato c)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_create_contrato";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@numero", System.Data.SqlDbType.NVarChar, 14).Value = c.Numero;
                cmd.Parameters.Add("@fechac", System.Data.SqlDbType.DateTime).Value = c.FechaCreacion;
                cmd.Parameters.Add("@rut", System.Data.SqlDbType.NVarChar, 10).Value = c.Cliente.Rut;
                cmd.Parameters.Add("@plan", System.Data.SqlDbType.NVarChar, 5).Value = c.Plan.IdPlan;
                cmd.Parameters.Add("@fechaivi", System.Data.SqlDbType.DateTime).Value = c.FechaInicioVigencia;
                cmd.Parameters.Add("@fechatvi", System.Data.SqlDbType.DateTime).Value = c.FechaFinVigencia;
                cmd.Parameters.Add("@vigencia", System.Data.SqlDbType.Bit).Value = c.Vigente;
                cmd.Parameters.Add("@salud", System.Data.SqlDbType.Bit).Value = c.DeclaracionSalud;
                cmd.Parameters.Add("@primaanual", System.Data.SqlDbType.Float).Value = c.PrimaAnual;
                cmd.Parameters.Add("@primamensual", System.Data.SqlDbType.Float).Value = c.PrimaMensual;
                cmd.Parameters.Add("@obser", System.Data.SqlDbType.NVarChar).Value = c.Observaciones;
                cone.Open();
                int x = cmd.ExecuteNonQuery();
                cone.Close();
                if (x > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool UPDATE(Contrato c)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_actualizar_contrato";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@numero", System.Data.SqlDbType.NVarChar, 14).Value = c.Numero;
                cmd.Parameters.Add("@fechac", System.Data.SqlDbType.DateTime).Value = c.FechaCreacion;
                cmd.Parameters.Add("@rut", System.Data.SqlDbType.NVarChar, 10).Value = c.Cliente.Rut;
                cmd.Parameters.Add("@plan", System.Data.SqlDbType.NVarChar, 5).Value = c.Plan.IdPlan;
                cmd.Parameters.Add("@fechaivi", System.Data.SqlDbType.DateTime).Value = c.FechaInicioVigencia;
                cmd.Parameters.Add("@fechatvi", System.Data.SqlDbType.DateTime).Value = c.FechaFinVigencia;
                cmd.Parameters.Add("@vigencia", System.Data.SqlDbType.Bit).Value = c.Vigente;
                cmd.Parameters.Add("@salud", System.Data.SqlDbType.Bit).Value = c.DeclaracionSalud;
                cmd.Parameters.Add("@primaanual", System.Data.SqlDbType.Float).Value = c.PrimaAnual;
                cmd.Parameters.Add("@primamensual", System.Data.SqlDbType.Float).Value = c.PrimaMensual;
                cmd.Parameters.Add("@obser", System.Data.SqlDbType.NVarChar).Value = c.Observaciones;
                cone.Open();
                int x = cmd.ExecuteNonQuery();
                cone.Close();
                if (x > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
