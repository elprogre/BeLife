using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Cliente
    {
        private string _rut;
        private string _nombre;
        private string a_pellido;
        private DateTime _fechaNacimiento;
        
        public string Rut
        {
            get { return _rut; }
            set
            {
                if (value.ToString().Length == 10)
                {
                    _rut = value;
                }
                else
                {
                    throw new Exception("El rut debe ser de 10 digitos");
                }
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (value.Trim().Length >= 2)
                {
                    _nombre = value;
                }
                else
                {
                    throw new Exception("No existe nombre");
                }
            }
        }

        public string Apellido
        {
            get { return a_pellido; }
            set
            {
                if (value.Trim().Length >= 2)
                {
                    a_pellido = value;
                }
                else
                {
                    throw new Exception("El apellido no existe");
                }
            }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set
            {
                int annos = DateTime.Now.Date.Year - value.Year;
                if (annos >= 18)
                {
                    _fechaNacimiento = value;
                }
                else
                {
                    throw new Exception("Es menor de edad");
                }
            }
        }
        
        public Sexo Sexo { get; set; }
        public EstadoCivil EstadoCivil { get; set; }

        public Cliente()
        {

        }

        public override string ToString()
        {
            return Rut;
        }
    }
}
