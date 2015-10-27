using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ListadoDeTesis.Dto;
using ScjnUtilities;

namespace ListadoDeTesis.Models
{
    public class OrganismosModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Directorio"].ConnectionString;

        public ObservableCollection<Organismos> GetOrganismos()
        {
            ObservableCollection<Organismos> listaOrganismos = new ObservableCollection<Organismos>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT O.* FROM Organismos O  WHERE TpoOrg = 1 OR TpoOrg = 4 ORDER BY TpoOrg, OrdenImpr";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //int age = reader["Age"] as int? ?? -1;
                        Organismos organismo = new Organismos();
                        organismo.IdOrganismo = Convert.ToInt32(reader["IdOrg"]);
                        organismo.IdInstancia = Convert.ToInt32(reader["TpoOrg"]);
                        organismo.Organismo = reader["Organismo"].ToString();
                        organismo.OrdenImpresion = Convert.ToInt32(reader["OrdenImpr"]);

                        listaOrganismos.Add(organismo);
                    }
                }

                this.AddOrganismosCorte(listaOrganismos);
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OrganismosModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,OrganismosModel", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaOrganismos;
        }


        private void AddOrganismosCorte(ObservableCollection<Organismos> listaOrganismos)
        {
            Organismos organismo = new Organismos();
            organismo.IdOrganismo = 10006;
            organismo.IdInstancia = 100;
            organismo.Organismo = "Pleno";
            organismo.OrdenImpresion = 1;

            listaOrganismos.Add(organismo);

            organismo = new Organismos();
            organismo.IdOrganismo = 10001;
            organismo.IdInstancia = 100;
            organismo.Organismo = "Primera Sala";
            organismo.OrdenImpresion = 2;

            listaOrganismos.Add(organismo);

            organismo = new Organismos();
            organismo.IdOrganismo = 10002;
            organismo.IdInstancia = 100;
            organismo.Organismo = "Segunda Sala";
            organismo.OrdenImpresion = 3;

            listaOrganismos.Add(organismo);

        }
    }
}
