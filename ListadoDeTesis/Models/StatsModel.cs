﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScjnUtilities;

namespace ListadoDeTesis.Models
{
    public class StatsModel
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Listado"].ConnectionString;

        /// <summary>
        /// Obtiene todas las fechas en que se han enviado tesis
        /// </summary>
        private ObservableCollection<DateTime> GetFechasEnvio()
        {
            ObservableCollection<DateTime> listaFechasEnvio = new ObservableCollection<DateTime>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT  FechaReal FROM Tesis GROUP BY FechaReal Order BY FechaReal";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listaFechasEnvio.Add(Convert.ToDateTime(DateTimeUtilities.GetDateFromReader(reader, "FechaReal")));
                        
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TesisModel", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }
            return listaFechasEnvio;
        }

        /// <summary>
        /// Obtiene las fechas en que no se han enviado tesis
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<DateTime> GetEnvioBlackOutDates()
        {

            ObservableCollection<DateTime> fechasEnvio = this.GetFechasEnvio();
            ObservableCollection<DateTime> blackOutDates = new ObservableCollection<DateTime>();

            DateTime fechaTemp = fechasEnvio[0].AddDays(-60);
            DateTime fechaFin = fechasEnvio[fechasEnvio.Count - 1].AddDays(60);

            while (fechaTemp <= fechaFin)
            {
                if (!fechasEnvio.Contains(fechaTemp))
                    blackOutDates.Add(fechaTemp);

                fechaTemp = fechaTemp.AddDays(1);
            }


            return blackOutDates;
        }

    }
}