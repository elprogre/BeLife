using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Tarifador
    {
        public Cliente cliente { get; set; }
        public Tarifador()
        {

        }
        public double CalcularPrima()
        {
            double recargo = 0;
            int annos = DateTime.Now.Year - cliente.FechaNacimiento.Year;
            if (annos >= 18 && annos <= 25)
            {
                recargo += 0.036;
            }
            if (annos >= 26 && annos <= 45)
            {
                recargo += 0.024;
            }
            if (annos > 45)
            {
                recargo += 0.06;
            }
            if (cliente.Sexo.Descripcion.Equals("Hombre"))
            {
                recargo += 0.024;
            }
            if (cliente.Sexo.Descripcion.Equals("Mujer"))
            {
                recargo += 0.012;
            }

            if (cliente.EstadoCivil.Descripcion.Equals("Soltero"))
            {
                recargo += 0.048;
            }
            if (cliente.EstadoCivil.Descripcion.Equals("Casado"))
            {
                recargo += 0.024;
            }
            else
            {
                recargo += 0.036;
            }
            return recargo;


        }
    }
}
