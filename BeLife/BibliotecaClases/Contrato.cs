using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClases
{
    public class Contrato : Tarifador
    {
        public string Numero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Cliente Cliente { get; set; }
        public Plan Plan { get; set; }

        private DateTime _fechaInicioVigencia;
        public DateTime FechaFinVigencia { get; set; }
        public bool Vigente { get; set; }
        public bool DeclaracionSalud { get; set; }
        public float PrimaAnual { get; set; }
        public float PrimaMensual { get; set; }

        private string _observaciones;


        public string Observaciones
        {
            get { return _observaciones; }
            set
            {
                if (value.Trim().Length > 2)
                {
                    _observaciones = value;
                }
                else
                {
                    throw new Exception("Falta llenar el campo Observaciones");
                }

            }
        }

        public DateTime FechaInicioVigencia
        {
            get { return _fechaInicioVigencia; }
            set
            {
                int annos = DateTime.Now.Date.Year - value.Month;
                if (value <= DateTime.Now.Date.AddMonths(1) && value >= DateTime.Now.Date.AddMonths(-1))
                {
                    _fechaInicioVigencia = value;
                }
                else
                {
                    throw new Exception("El inicio de la vigencia del contrato no puede ser menor a la fecha actual y tampoco exceder de 1 mes");
                }

            }
        }

        public Contrato()
        {

        }

        public double ValorPrimalAnual()
        {
            double vpa = 0;
            this.cliente = Cliente;
            vpa = this.CalcularPrima();
            vpa += Plan.PrimaBase;
            return vpa;
        }
    }
}
