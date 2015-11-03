using System;
using System.ComponentModel;
using System.Linq;

namespace ListadoDeTesis.Dto 
{
    public class Tesis : INotifyPropertyChanged
    {
        private int idTesis;
        private string claveTesis;
        private string rubro;
        private int idInstancia;
        private int idSubInstancia;
        private int ordenInstancia;
        private int idColor;
        private int idUsuario;
        private int tatj;
        private string materiaAsignada;
        private string oficio;
        private DateTime? fechaAltaSistema;
        private DateTime? fechaEnvio;
        private DateTime? fechaReal;

        private int idUsuarioValida;
        
        public string Oficio
        {
            get
            {
                return this.oficio;
            }
            set
            {
                this.oficio = value;
            }
        }

        public string MateriaAsignada
        {
            get
            {
                return this.materiaAsignada;
            }
            set
            {
                this.materiaAsignada = value;
            }
        }

        public int IdTesis
        {
            get
            {
                return this.idTesis;
            }
            set
            {
                this.idTesis = value;
            }
        }

        public string ClaveTesis
        {
            get
            {
                return this.claveTesis;
            }
            set
            {
                this.claveTesis = value;
            }
        }

        public string Rubro
        {
            get
            {
                return this.rubro;
            }
            set
            {
                this.rubro = value;
            }
        }

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

        public int IdSubInstancia
        {
            get
            {
                return this.idSubInstancia;
            }
            set
            {
                this.idSubInstancia = value;
            }
        }

        public int OrdenInstancia
        {
            get
            {
                return this.ordenInstancia;
            }
            set
            {
                this.ordenInstancia = value;
            }
        }

        public int IdColor
        {
            get
            {
                return this.idColor;
            }
            set
            {
                this.idColor = value;
            }
        }

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

        public int Tatj
        {
            get
            {
                return this.tatj;
            }
            set
            {
                this.tatj = value;
                this.OnPropertyChanged("Tatj");
            }
        }

        public DateTime? FechaAltaSistema
        {
            get
            {
                return this.fechaAltaSistema;
            }
            set
            {
                this.fechaAltaSistema = value;
            }
        }

        public DateTime? FechaEnvio
        {
            get
            {
                return this.fechaEnvio;
            }
            set
            {
                this.fechaEnvio = value;
            }
        }

        public DateTime? FechaReal
        {
            get
            {
                return this.fechaReal;
            }
            set
            {
                this.fechaReal = value;
            }
        }

        public int IdUsuarioValida
        {
            get
            {
                return this.idUsuarioValida;
            }
            set
            {
                this.idUsuarioValida = value;
                this.OnPropertyChanged("IdUsuarioValida");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}
