using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using ListadoDeTesis.Dto;
using MantesisVerIusCommonObjects.Dto;
using ScjnUtilities;

namespace ListadoDeTesis.Models
{
    public class TesisModel
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Listado"].ConnectionString;

        #region Tesis
        public void SetNewTesis(Tesis tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {
                tesis.IdTesis = DataBaseUtilities.GetNextIdForUse("Tesis", "IdTesis",connection);

                string sqlCadena = "SELECT * FROM Tesis WHERE IdTesis = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Tesis");

                dr = dataSet.Tables["Tesis"].NewRow();

                dr["IdTesis"] = tesis.IdTesis;
                dr["Tesis"] = tesis.ClaveTesis;
                dr["Rubro"] = tesis.Rubro;
                dr["RubroStr"] = StringUtilities.PrepareToAlphabeticalOrder(tesis.Rubro);
                dr["Tatj"] = tesis.Tatj;
                dr["IdInstancia"] = tesis.IdInstancia;
                dr["IdSubInstancia"] = tesis.IdSubInstancia;
                dr["OrdenInstancia"] = tesis.OrdenInstancia;
                dr["IdColor"] = tesis.IdColor;
                dr["MateriaAsignada"] = tesis.MateriaAsignada;
                dr["Oficio"] = tesis.Oficio;
                dr["FechaEnvio"] = tesis.FechaEnvio;
                dr["FechaEnvioInt"] = DateTimeUtilities.DateToInt(tesis.FechaEnvio);
                
                dataSet.Tables["Tesis"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Tesis (IdTesis,Tesis,Rubro,RubroStr,Tatj,IdInstancia,IdSubInstancia,OrdenInstancia,IdColor,MateriaAsignada,Oficio,FechaEnvio,FechaEnvioInt) " +
                       " VALUES (@IdTesis,@Tesis,@Rubro,@RubroStr,@Tatj,@IdInstancia,@IdSubInstancia,@OrdenInstancia,@IdColor,@MateriaAsignada,@Oficio,@FechaEnvio,@FechaEnvioInt)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@Tesis", OleDbType.VarChar, 0, "Tesis");
                dataAdapter.InsertCommand.Parameters.Add("@Rubro", OleDbType.VarChar, 0, "Rubro");
                dataAdapter.InsertCommand.Parameters.Add("@RubroStr", OleDbType.VarChar, 0, "RubroStr");
                dataAdapter.InsertCommand.Parameters.Add("@Tatj", OleDbType.Numeric, 0, "Tatj");
                dataAdapter.InsertCommand.Parameters.Add("@IdInstancia", OleDbType.Numeric, 0, "IdInstancia");
                dataAdapter.InsertCommand.Parameters.Add("@IdSubInstancia", OleDbType.Numeric, 0, "IdSubInstancia");
                dataAdapter.InsertCommand.Parameters.Add("@OrdenInstancia", OleDbType.Numeric, 0, "OrdenInstancia");
                dataAdapter.InsertCommand.Parameters.Add("@IdColor", OleDbType.Numeric, 0, "IdColor");
                dataAdapter.InsertCommand.Parameters.Add("@MateriaAsignada", OleDbType.VarChar, 0, "MateriaAsignada");
                dataAdapter.InsertCommand.Parameters.Add("@Oficio", OleDbType.VarChar, 0, "Oficio");
                dataAdapter.InsertCommand.Parameters.Add("@FechaEnvio", OleDbType.Date, 0, "FechaEnvio");
                dataAdapter.InsertCommand.Parameters.Add("@FechaEnvioInt", OleDbType.Numeric, 0, "FechaEnvioInt");

                dataAdapter.Update(dataSet, "Tesis");
                dataSet.Dispose();
                dataAdapter.Dispose();

                this.SetNewBitacoraEntry(tesis);

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
        }

        /// <summary>
        /// Obtiene todas las tesis capturadas hasta el momento
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Tesis> GetTesis()
        {
            ObservableCollection<Tesis> listaTesis = new ObservableCollection<Tesis>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT T.*,B.* FROM Tesis T INNER JOIN Bitacora B ON T.IdTesis = B.IdTesis " + 
                "ORDER BY B.FechaAltaInt desc, T.RubroStr asc";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tesis tesis = new Tesis();
                        tesis.IdTesis = Convert.ToInt32(reader["T.IdTesis"]);
                        tesis.ClaveTesis = reader["Tesis"].ToString();
                        tesis.Rubro = reader["Rubro"].ToString();
                        tesis.Tatj = Convert.ToInt32(reader["tatj"]);
                        tesis.IdInstancia = Convert.ToInt32(reader["IdInstancia"]);
                        tesis.IdSubInstancia = Convert.ToInt32(reader["IdSubInstancia"]);
                        tesis.OrdenInstancia = Convert.ToInt32(reader["OrdenInstancia"]);
                        tesis.IdColor = Convert.ToInt32(reader["IdColor"]);
                        tesis.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        tesis.MateriaAsignada = reader["MateriaAsignada"].ToString();
                        tesis.Oficio = reader["Oficio"].ToString();
                        tesis.FechaAltaSistema = DateTimeUtilities.GetDateFromReader(reader, "FechaAlta");
                        tesis.FechaEnvio = DateTimeUtilities.GetDateFromReader(reader, "FechaEnvio");
                        tesis.FechaReal = DateTimeUtilities.GetDateFromReader(reader, "FechaReal");
                        tesis.IdUsuarioValida = Convert.ToInt32(reader["IdUsuarioValida"]);
                        
                        listaTesis.Add(tesis);
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

            return listaTesis;
        }

        /// <summary>
        /// Obtiene las tesis de un periodo específico para generar el listado que se enviará
        /// </summary>
        /// <param name="fInicio">Fecha en que se envió el último listado</param>
        /// <param name="fFinal">Fecha en que se enviará el listado que se genere</param>
        /// <returns></returns>
        public ObservableCollection<Tesis> GetTesis(DateTime? fInicio, DateTime? fFinal)
        {

            string inicio = DateTimeUtilities.DateToInt( fInicio.Value.AddDays(1));
            string final = DateTimeUtilities.DateToInt(fFinal);

            ObservableCollection<Tesis> listaTesis = new ObservableCollection<Tesis>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT T.*,B.* FROM Tesis T INNER JOIN Bitacora B ON T.IdTesis = B.IdTesis " +
                "WHERE FechaRealInt BETWEEN @Inicio AND @Final ORDER BY B.FechaAltaInt desc, T.RubroStr asc";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@Inicio", inicio);
                cmd.Parameters.AddWithValue("@Final", final);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tesis tesis = new Tesis();
                        tesis.IdTesis = Convert.ToInt32(reader["T.IdTesis"]);
                        tesis.ClaveTesis = reader["Tesis"].ToString();
                        tesis.Rubro = reader["Rubro"].ToString();
                        tesis.Tatj = Convert.ToInt32(reader["tatj"]);
                        tesis.IdInstancia = Convert.ToInt32(reader["IdInstancia"]);
                        tesis.IdSubInstancia = Convert.ToInt32(reader["IdSubInstancia"]);
                        tesis.OrdenInstancia = Convert.ToInt32(reader["OrdenInstancia"]);
                        tesis.IdColor = Convert.ToInt32(reader["IdColor"]);
                        tesis.MateriaAsignada = reader["MateriaAsignada"].ToString();
                        tesis.Oficio = reader["Oficio"].ToString();
                        tesis.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
                        tesis.MateriaAsignada = reader["MateriaAsignada"].ToString();
                        tesis.Oficio = reader["Oficio"].ToString();
                        tesis.FechaAltaSistema = DateTimeUtilities.GetDateFromReader(reader, "FechaAlta");
                        tesis.FechaEnvio = DateTimeUtilities.GetDateFromReader(reader, "FechaEnvio");
                        tesis.FechaReal = DateTimeUtilities.GetDateFromReader(reader, "FechaReal");
                        tesis.IdUsuarioValida = Convert.ToInt32(reader["IdUsuarioValida"]);

                        listaTesis.Add(tesis);
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

            return listaTesis;
        }


        /// <summary>
        /// Actualiza la fecha real de entrega cuando la fecha de entrega capturada es correcta
        /// </summary>
        /// <param name="tesis"></param>
        public void UpdateTesis(Tesis tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbDataAdapter dataAdapter;
            OleDbCommand cmd;
            cmd = connection.CreateCommand();
            cmd.Connection = connection;

            try
            {
                connection.Open();

                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlCadena = "SELECT * FROM Tesis WHERE IdTesis = " + tesis.IdTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Tesis");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["FechaReal"] = tesis.FechaEnvio;
                dr["FechaRealInt"] = DateTimeUtilities.DateToInt(tesis.FechaEnvio);
                dr["IdUsuarioValida"] = AccesoUsuarioModel.Llave;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                string sSql = "UPDATE Tesis " +
                              "SET FechaReal = @FechaReal, FechaRealInt = @FechaRealInt, IdUsuarioValida = @IdUsuarioValida" +
                              " WHERE IdTesis = @IdTesis";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@FechaReal", OleDbType.Date, 0, "FechaReal");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaRealInt", OleDbType.Numeric, 0, "FechaRealInt");
                dataAdapter.UpdateCommand.Parameters.Add("@IdUsuarioValida", OleDbType.Numeric, 0, "IdUsuarioValida");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");


                dataAdapter.Update(dataSet, "Tesis");
                dataSet.Dispose();
                dataAdapter.Dispose();

                tesis.IdUsuarioValida = AccesoUsuarioModel.Llave;
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
        }

        /// <summary>
        /// Actualiza la fecha real de entrega cuando quien valida considera que la capturada no es la real
        /// </summary>
        /// <param name="tesis"></param>
        /// <param name="fechaReal"></param>
        public void UpdateTesis(Tesis tesis,DateTime? fechaReal)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbDataAdapter dataAdapter;
            OleDbCommand cmd;
            cmd = connection.CreateCommand();
            cmd.Connection = connection;

            try
            {
                connection.Open();

                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlCadena = "SELECT * FROM Tesis WHERE IdTesis = " + tesis.IdTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Tesis");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["FechaReal"] = fechaReal;
                dr["FechaRealInt"] = DateTimeUtilities.DateToInt(fechaReal);
                dr["IdUsuarioValida"] = AccesoUsuarioModel.Llave;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                string sSql = "UPDATE Tesis " +
                              "SET FechaReal = @FechaReal, FechaRealInt = @FechaRealInt, IdUsuarioValida = @IdUsuarioValida" +
                              " WHERE IdTesis = @IdTesis";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@FechaReal", OleDbType.Date, 0, "FechaReal");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaRealInt", OleDbType.Numeric, 0, "FechaRealInt");
                dataAdapter.UpdateCommand.Parameters.Add("@IdUsuarioValida", OleDbType.Numeric, 0, "IdUsuarioValida");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");


                dataAdapter.Update(dataSet, "Tesis");
                dataSet.Dispose();
                dataAdapter.Dispose();

                tesis.IdUsuarioValida = AccesoUsuarioModel.Llave;
                tesis.FechaReal = fechaReal;
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
        }

        #endregion




        #region Bitacora

        private void SetNewBitacoraEntry(Tesis tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {

                string sqlCadena = "SELECT * FROM Bitacora WHERE Id = 0";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Bitacora");

                dr = dataSet.Tables["Bitacora"].NewRow();

                dr["IdTesis"] = tesis.IdTesis;
                dr["IdUsuario"] = AccesoUsuarioModel.Llave;
                dr["FechaAlta"] = DateTime.Now;
                dr["FechaAltaInt"] = ScjnUtilities.DateTimeUtilities.DateToInt(DateTime.Now);

                dataSet.Tables["Bitacora"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Bitacora (IdTesis,IdUsuario,FechaAlta,FechaAltaInt) " +
                       " VALUES (@IdTesis,@IdUsuario,@FechaAlta,@FechaAltaInt)";

                dataAdapter.InsertCommand.CommandText = sSql;

                dataAdapter.InsertCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");
                dataAdapter.InsertCommand.Parameters.Add("@IdUsuario", OleDbType.Numeric, 0, "IdUsuario");
                dataAdapter.InsertCommand.Parameters.Add("@FechaAlta", OleDbType.Date, 0, "FechaAlta");
                dataAdapter.InsertCommand.Parameters.Add("@FechaAltaInt", OleDbType.Numeric, 0, "FechaAltaInt");

                dataAdapter.Update(dataSet, "Bitacora");
                dataSet.Dispose();
                dataAdapter.Dispose();

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
        }

        #endregion
    }
}
