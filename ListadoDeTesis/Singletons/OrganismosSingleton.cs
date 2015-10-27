using System;
using System.Collections.ObjectModel;
using System.Linq;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;

namespace ListadoDeTesis.Singletons
{
    public class OrganismosSingleton
    {
        private static ObservableCollection<Organismos> organismos;

        private OrganismosSingleton() { }

        public static ObservableCollection<Organismos> Organismos
        {
            get
            {
                if (organismos == null)
                    organismos = new OrganismosModel().GetOrganismos();

                return organismos;
            }
        }
    }
}
