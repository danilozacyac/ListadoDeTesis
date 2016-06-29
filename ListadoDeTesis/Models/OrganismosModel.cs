using ListadoDeTesis.Dto;
using ScjnUtilities;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;

namespace ListadoDeTesis.Models
{
    public class OrganismosModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Directorio"].ConnectionString;

        public ObservableCollection<Organismos> GetOrganismos()
        {
            ObservableCollection<Organismos> listaOrganismos = new ObservableCollection<Organismos>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                connection.Open();

                cmd = new SqlCommand("SELECT O.* FROM Organismos O  WHERE IdTpoOrg = 1 OR IdTpoOrg = 4 ORDER BY IdTpoOrg, OrdenImpr", connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //int age = reader["Age"] as int? ?? -1;
                        Organismos organismo = new Organismos()
                        {
                            IdOrganismo = Convert.ToInt32(reader["IdOrganismo"]),
                            IdInstancia = Convert.ToInt32(reader["IdTpoOrg"]),
                            Organismo = reader["Organismo"].ToString(),
                            OrdenImpresion = Convert.ToInt32(reader["OrdenImpr"])
                        };

                        listaOrganismos.Add(organismo);
                    }
                }

                this.AddOrganismosCorte(listaOrganismos);
            }
            catch (SqlException ex)
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
            Organismos organismo = new Organismos() { IdOrganismo = 10006, IdInstancia = 100, Organismo = "Pleno", OrdenImpresion = 1 };

            listaOrganismos.Add(organismo);

            organismo = new Organismos() { IdOrganismo = 10001, IdInstancia = 100, Organismo = "Primera Sala", OrdenImpresion = 2 };

            listaOrganismos.Add(organismo);

            organismo = new Organismos() { IdOrganismo = 10002, IdInstancia = 100, Organismo = "Segunda Sala", OrdenImpresion = 3 };

            listaOrganismos.Add(organismo);

        }
    }
}
