using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;

namespace Biblioteca.Negocio
{
    public class Contrato : IMetodos
    {
        public string Numero { get; set; }

        public DateTime Creacion { get; set; }

        public DateTime Termino { get; set; }

        private string _rutcliente;

        public string RutCliente
        {
            get { return _rutcliente; }
            set 
            {
                    if (value.ToString().Length == 10)
                    {
                        _rutcliente = value;
                    }
                    else
                    {
                        throw new Exception("El rut debe tener 10 digitos");
                    }
            }
        }

        public string IdModalidad { get; set; }

        public int IdTipoEvento { get; set; }

        public DateTime FechaHoraInicio { get; set; }

        public DateTime FechaHoraTermino { get; set; }

        private int _asistentes;

        public int Asistentes
        {
            get { return _asistentes; }
            set 
            {
                if (value>=0)
                {
                    _asistentes = value;
                }
                else
                {
                    throw new Exception("No se pueden tener menos de 0 asistentes");
                }

            }
        }

        private int _personaladicional;

        public int PersonalAdicional
        {
            get { return _personaladicional; }
            set 
            {
                if (value >= 0)
                {
                    _personaladicional = value;
                }
                else
                {
                    throw new Exception("No se pueden tener menos de 0 personales adicionales");
                }
            }
        }

        public bool Realizado { get; set; }

        public double ValorTotalContrato { get; set; }

        private string _observaciones;

        public string Observaciones
        {
            get { return _observaciones; }
            set 
            {
                if (value.Trim().Length >= 3)
                {
                    _observaciones = value;
                }
                else
                {
                    throw new Exception("Falta el campo Observaciones");
                }
            }

        }

        public Contrato()
        {

        }

        


        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
    }
}
