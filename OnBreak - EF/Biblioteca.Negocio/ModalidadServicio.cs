using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca.DALC;

namespace Biblioteca.Negocio
{
    public class ModalidadServicio
    {
        public string IdModalidad { get; set; }
        public int IdTipoEvento { get; set; }
        public string Nombre { get; set; }
        public double ValorBase { get; set; }
        public int PersonalBase { get; set; }

        public ModalidadServicio()
        {

        }

        OnBreakEntities bdd = new OnBreakEntities();


        //falta verificar si esto esta bien///
        public bool Read()
        {
            try
            {
                DALC.ModalidadServicio mod =
                bdd.ModalidadServicio.First(m => m.IdModalidad == IdModalidad);
                this.IdModalidad = mod.IdModalidad;
                this.IdTipoEvento = mod.IdTipoEvento;
                this.Nombre = mod.Nombre;
                this.ValorBase = mod.ValorBase;
                this.PersonalBase = mod.PersonalBase;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
