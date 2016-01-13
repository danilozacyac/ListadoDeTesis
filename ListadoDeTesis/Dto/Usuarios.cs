using System;
using System.Linq;

namespace ListadoDeTesis.Dto
{
    public class Usuarios
    {
        private int idUsuario;
        private string nombre;
        private string usuario;

        public int IdUsuario
        {
            get
            {
                return this.idUsuario;
            }
            set
            {
                this.idUsuario = value;
            }
        }

        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
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
    }
}
