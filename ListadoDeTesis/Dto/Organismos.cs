using System;
using System.Linq;

namespace ListadoDeTesis.Dto
{
    public class Organismos
    {
        /// <summary>
        /// Indica si pertence a Corte, Plenos de Circuito o Tribunales Colegiados
        /// </summary>
        private int idInstancia;

        /// <summary>
        /// Identificador del Organismo que emite la tesis
        /// </summary>
        private int idOrganismo;
        private string organismo;

        /// <summary>
        /// Orden en que se imprimirá en el reporte, se obtiene del programa del directorio
        /// </summary>
        private int ordenImpresion;

        public int IdInstancia
        {
            get
            {
                return this.idInstancia;
            }
            set
            {
                this.idInstancia = value;
            }
        }

        public int IdOrganismo
        {
            get
            {
                return this.idOrganismo;
            }
            set
            {
                this.idOrganismo = value;
            }
        }

        public string Organismo
        {
            get
            {
                return this.organismo;
            }
            set
            {
                this.organismo = value;
            }
        }

        public int OrdenImpresion
        {
            get
            {
                return this.ordenImpresion;
            }
            set
            {
                this.ordenImpresion = value;
            }
        }
    }
}
