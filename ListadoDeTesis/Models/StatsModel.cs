﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ListadoDeTesis.Dto;
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

            try
            {
                connection.Open();

                cmd = new OleDbCommand("SELECT  FechaReal FROM Tesis GROUP BY FechaReal Order BY FechaReal", connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!String.IsNullOrEmpty(reader["FechaReal"].ToString()))
                        {
                            listaFechasEnvio.Add(Convert.ToDateTime(DateTimeUtilities.GetDateFromReader(reader, "FechaReal")));
                        }
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,StatsModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,StatsModel", "ListadoDeTesis");
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


        /// <summary>
        /// Obtiene el número de tesis enviadas por abogado en fecha determinada
        /// </summary>
        /// <param name="fechaEnvio"></param>
        /// <returns></returns>
        public List<Estadistica> GetTesis(DateTime? fechaEnvio)
        {
            List<Estadistica> listaTesis = new List<Estadistica>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT Usuario, COUNT(Tesis.FechaReal) AS Total " +
                                "FROM Tesis " + 
                                " INNER JOIN Usuarios ON Tesis.IdUsuario = Usuarios.Llave " +
                                " WHERE FechaReal = @FechaReal  GROUP BY Usuario";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@FechaReal", fechaEnvio);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Estadistica stat = new Estadistica()
                        {
                            Usuario = reader["Usuario"].ToString(),
                            TotalTesis = Convert.ToInt32(reader["Total"])
                        };
                        listaTesis.Add(stat);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,StatsModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,StatsModel", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaTesis;
        }

        /// <summary>
        /// Devuelve el número de tesis enviadas por instancia en un año en específico por abogado
        /// </summary>
        /// <param name="param"></param>
        /// <param name="valor"></param>
        /// <param name="year">Año del cual se quieren consultar el número de tesis</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Revisar consultas SQL para comprobar si tienen vulnerabilidades de seguridad")]
        public List<Estadistica> GetTesisPorInstanciaPorAbogado(string param, int valor,string year)
        {
            List<Estadistica> listaTesis = new List<Estadistica>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = String.Format("SELECT Usuario, COUNT(Tesis.FechaReal) AS Total  FROM Tesis INNER JOIN Usuarios ON Tesis.IdUsuario = Usuarios.Llave  " +
                                             "WHERE {0} = @Valor AND YEAR(FechaEnvio) = @Year GROUP BY Usuario", param);

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@Year", year);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Estadistica stat = new Estadistica()
                        {
                            Usuario = reader["Usuario"].ToString(),
                            TotalTesis = Convert.ToInt32(reader["Total"])
                        };
                        listaTesis.Add(stat);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,StatsModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,StatsModel", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaTesis;
        }

    }
}
