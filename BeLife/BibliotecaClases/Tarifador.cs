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
                recargo += 3.6;
            }
            if (annos >= 26 && annos <= 45)
            {
                recargo += 2.4;
            }
            if (annos > 45)
            {
                recargo += 6;
            }
            if (cliente.Sexo.Descripcion.Equals("Hombre"))
            {
                recargo += 2.4;
            }
            if (cliente.Sexo.Descripcion.Equals("Mujer"))
            {
                recargo += 1.2;
            }

            if (cliente.EstadoCivil.Descripcion.Equals("Soltero"))
            {
                recargo += 4.8;
            }
            else if (cliente.EstadoCivil.Descripcion.Equals("Casado"))
            {
                recargo += 2.4;
            }
            else
            {
                recargo += 3.6;
            }
            return recargo;


        }
    }
}
