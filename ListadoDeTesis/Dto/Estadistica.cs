using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListadoDeTesis.Dto
{
    public class Estadistica
    {
        private int idusuario;
        private string usuario;
        private int totalTesis;
        public int Idusuario
        {
            get
            {
                return this.idusuario;
            }
            set
            {
                this.idusuario = value;
            }
        }

        public string Usuario
        {
            get
            {
                return this.usuario;
            }
            set
            {
                this.usuario = value;
            }
        }

        public int TotalTesis
        {
            get
            {
                return this.totalTesis;
            }
            set
            {
                this.totalTesis = value;
            }
        }
    }
}
