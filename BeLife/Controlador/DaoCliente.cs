using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BibliotecaClases;

namespace Controlador
{
    public class DaoCliente
    {
        private SqlConnection cone;
        public DaoCliente()
        {
            cone = new Conexion().getConexion();
        }

        public Cliente Read(string rut)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_cliente";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@rut", System.Data.SqlDbType.NChar, 10).Value = rut;

                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                Cliente c = null;
                while (dr.Read())
                {
                    c = new Cliente();
                    c.Rut = dr[0].ToString();
                    c.Nombre = dr[1].ToString();
                    c.Apellido = dr[2].ToString();
                    c.FechaNacimiento = DateTime.Parse(dr[3].ToString());
                    c.Sexo = new DaoSexo().Read(int.Parse(dr[4].ToString()));
                    c.EstadoCivil = new DaoEstadoCivil().Read(int.Parse(dr[5].ToString())); 
                }
                cone.Close();
                return c;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Cliente> ReadAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_cliente";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                List<Cliente> lista = new List<Cliente>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    Cliente c = new Cliente();
                    c.Rut = dr[0].ToString();
                    c.Nombre = dr[1].ToString();
                    c.Apellido = dr[2].ToString();
                    c.FechaNacimiento = DateTime.Parse(dr[3].ToString());
                    c.Sexo = new DaoSexo().Read(int.Parse(dr[4].ToString()));
                    c.EstadoCivil = new DaoEstadoCivil().Read(int.Parse(dr[5].ToString()));
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

        public List<Cliente> ReadAllBySexo(int idSexo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_cliente_sexo";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdSexo", System.Data.SqlDbType.Int).Value = idSexo;
                cmd.Connection = cone;
                List<Cliente> lista = new List<Cliente>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente c = new Cliente();
                    c.Rut = dr[0].ToString();
                    c.Nombre = dr[1].ToString();
                    c.Apellido = dr[2].ToString();
                    c.FechaNacimiento = DateTime.Parse(dr[3].ToString());
                    c.Sexo = new DaoSexo().Read(int.Parse(dr[4].ToString()));
                    c.EstadoCivil = new DaoEstadoCivil().Read(int.Parse(dr[5].ToString()));
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

        public List<Cliente> ReadAllByEstadoCivil(int idEc)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_read_all_cliente_ec";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdEc", System.Data.SqlDbType.Int).Value = idEc;
                cmd.Connection = cone;
                List<Cliente> lista = new List<Cliente>();
                cone.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente c = new Cliente();
                    c.Rut = dr[0].ToString();
                    c.Nombre = dr[1].ToString();
                    c.Apellido = dr[2].ToString();
                    c.FechaNacimiento = DateTime.Parse(dr[3].ToString());
                    c.Sexo = new DaoSexo().Read(int.Parse(dr[4].ToString()));
                    c.EstadoCivil = new DaoEstadoCivil().Read(int.Parse(dr[5].ToString()));
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

        public bool Create(Cliente cli)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_create_cliente";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@rut", System.Data.SqlDbType.NVarChar, 10).Value = cli.Rut;
                cmd.Parameters.Add("@nombres", System.Data.SqlDbType.NVarChar, 20).Value = cli.Nombre;
                cmd.Parameters.Add("@apellidos", System.Data.SqlDbType.NVarChar, 20).Value = cli.Apellido;
                cmd.Parameters.Add("@fecha", System.Data.SqlDbType.DateTime).Value = cli.FechaNacimiento;
                cmd.Parameters.Add("@idsexo", System.Data.SqlDbType.Int, 1).Value = cli.Sexo.IdSexo;
                cmd.Parameters.Add("@idec", System.Data.SqlDbType.Int, 1).Value = cli.EstadoCivil.IdEstadoCivil;
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

        public bool Update(Cliente clie)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_update_cliente";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@rut", System.Data.SqlDbType.NVarChar, 10).Value = clie.Rut;
                cmd.Parameters.Add("@nombres", System.Data.SqlDbType.NVarChar, 20).Value = clie.Nombre;
                cmd.Parameters.Add("@apellidos", System.Data.SqlDbType.NVarChar, 20).Value = clie.Apellido;
                cmd.Parameters.Add("@fecha", System.Data.SqlDbType.DateTime).Value = clie.FechaNacimiento;
                cmd.Parameters.Add("@idsexo", System.Data.SqlDbType.Int, 1).Value = clie.Sexo.IdSexo;
                cmd.Parameters.Add("@idec", System.Data.SqlDbType.Int, 1).Value = clie.EstadoCivil.IdEstadoCivil;
                cone.Open();
                int x = cmd.ExecuteNonQuery();
                cone.Close();
                if (x > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool Delete(string rut)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_delete_cliente";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = cone;
                cmd.Parameters.Add("@rut", System.Data.SqlDbType.NChar, 10).Value = rut;
                cone.Open();
                int x = cmd.ExecuteNonQuery();
                cone.Close();
                if (x > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
