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
                dr["FechaAlta"] = DateTime.Now;
                dr["FechaAltaInt"] = DateTimeUtilities.DateToInt(DateTime.Now);
                dr["FechaEnvio"] = tesis.FechaEnvio;
                dr["FechaEnvioInt"] = DateTimeUtilities.DateToInt(tesis.FechaEnvio);
                dr["IdUsuario"] = AccesoUsuarioModel.Llave;
                
                dataSet.Tables["Tesis"].Rows.Add(dr);

                dataAdapter.InsertCommand = connection.CreateCommand();

                sSql = "INSERT INTO Tesis (IdTesis,Tesis,Rubro,RubroStr,Tatj,IdInstancia,IdSubInstancia,OrdenInstancia,IdColor,MateriaAsignada,Oficio,FechaAlta,FechaAltaInt,FechaEnvio,FechaEnvioInt,IdUsuario) " +
                       " VALUES (@IdTesis,@Tesis,@Rubro,@RubroStr,@Tatj,@IdInstancia,@IdSubInstancia,@OrdenInstancia,@IdColor,@MateriaAsignada,@Oficio,@FechaAlta,@FechaAltaInt,@FechaEnvio,@FechaEnvioInt,@IdUsuario)";

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
                dataAdapter.InsertCommand.Parameters.Add("@FechaAlta", OleDbType.Date, 0, "FechaAlta");
                dataAdapter.InsertCommand.Parameters.Add("@FechaAltaInt", OleDbType.Numeric, 0, "FechaAltaInt");
                dataAdapter.InsertCommand.Parameters.Add("@FechaEnvio", OleDbType.Date, 0, "FechaEnvio");
                dataAdapter.InsertCommand.Parameters.Add("@FechaEnvioInt", OleDbType.Numeric, 0, "FechaEnvioInt");
                dataAdapter.InsertCommand.Parameters.Add("@IdUsuario", OleDbType.Numeric, 0, "IdUsuario");

                dataAdapter.Update(dataSet, "Tesis");
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

            String sqlCadena = "SELECT * FROM Tesis  ORDER BY FechaRealInt desc, RubroStr asc";

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
                        tesis.IdTesis = Convert.ToInt32(reader["IdTesis"]);
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
        /// Obtiene todas las tesis capturadas que se enviaron en un día particular
        /// </summary>
        /// <param name="fechaEnvia"></param>
        /// <returns></returns>
        public ObservableCollection<Tesis> GetTesis(DateTime? fechaEnvia)
        {


            ObservableCollection<Tesis> listaTesis = new ObservableCollection<Tesis>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Tesis " +
                "WHERE FechaReal = @FechaReal ORDER BY FechaAltaInt desc, RubroStr asc";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                cmd.Parameters.AddWithValue("@FechaReal", fechaEnvia);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tesis tesis = new Tesis();
                        tesis.IdTesis = Convert.ToInt32(reader["IdTesis"]);
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

            String sqlCadena = "SELECT * FROM Tesis  " +
                "WHERE FechaRealInt BETWEEN @Inicio AND @Final ORDER BY FechaAltaInt desc, RubroStr asc";

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
                        tesis.IdTesis = Convert.ToInt32(reader["IdTesis"]);
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
        /// Actualiza la información de la tesis antes de que sea validada
        /// </summary>
        /// <param name="tesis"></param>
        public void UpdateTesis(Tesis tesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            string sSql;
            OleDbDataAdapter dataAdapter;

            DataSet dataSet = new DataSet();
            DataRow dr;

            try
            {

                string sqlCadena = "SELECT * FROM Tesis WHERE IdTesis = " + tesis.IdTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Tesis");

                dr = dataSet.Tables["Tesis"].Rows[0];
                dr.BeginEdit();
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
                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                sSql = "UPDATE Tesis SET Tesis = @Tesis,Rubro = @Rubro,RubroStr = @RubroStr,Tatj = @Tatj,IdInstancia = @IdInstancia, " + 
                       "IdSubInstancia = @IdSubInstancia,OrdenInstancia = @OrdenInstancia,IdColor = @IdColor," + 
                       "MateriaAsignada = @MateriaAsignada,Oficio = @Oficio,FechaEnvio = @FechaEnvio,FechaEnvioInt = @FechaEnvioInt " +
                       " WHERE IdTesis = @IdTesis";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@Tesis", OleDbType.VarChar, 0, "Tesis");
                dataAdapter.UpdateCommand.Parameters.Add("@Rubro", OleDbType.VarChar, 0, "Rubro");
                dataAdapter.UpdateCommand.Parameters.Add("@RubroStr", OleDbType.VarChar, 0, "RubroStr");
                dataAdapter.UpdateCommand.Parameters.Add("@Tatj", OleDbType.Numeric, 0, "Tatj");
                dataAdapter.UpdateCommand.Parameters.Add("@IdInstancia", OleDbType.Numeric, 0, "IdInstancia");
                dataAdapter.UpdateCommand.Parameters.Add("@IdSubInstancia", OleDbType.Numeric, 0, "IdSubInstancia");
                dataAdapter.UpdateCommand.Parameters.Add("@OrdenInstancia", OleDbType.Numeric, 0, "OrdenInstancia");
                dataAdapter.UpdateCommand.Parameters.Add("@IdColor", OleDbType.Numeric, 0, "IdColor");
                dataAdapter.UpdateCommand.Parameters.Add("@MateriaAsignada", OleDbType.VarChar, 0, "MateriaAsignada");
                dataAdapter.UpdateCommand.Parameters.Add("@Oficio", OleDbType.VarChar, 0, "Oficio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvio", OleDbType.Date, 0, "FechaEnvio");
                dataAdapter.UpdateCommand.Parameters.Add("@FechaEnvioInt", OleDbType.Numeric, 0, "FechaEnvioInt");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");

                dataAdapter.Update(dataSet, "Tesis");
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

        /// <summary>
        /// Actualiza la fecha real de entrega cuando la fecha de entrega capturada es correcta
        /// </summary>
        /// <param name="idTesis">Identificador de la tesis que se esta validando</param>
        ///<param name="fecha">Fecha capturada que se esta validando</param>
        ///<param name="tesis">Identificador del usuario que esta validando la fecha de entrega</param>
        public void UpdateTesis(int idTesis,DateTime? fecha, Tesis tesis)
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

                string sqlCadena = "SELECT * FROM Tesis WHERE IdTesis = " + idTesis;

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlCadena, connection);

                dataAdapter.Fill(dataSet, "Tesis");

                dr = dataSet.Tables[0].Rows[0];
                dr.BeginEdit();
                dr["FechaReal"] = fecha;
                dr["FechaRealInt"] = DateTimeUtilities.DateToInt(fecha);
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

        /// <summary>
        /// Elimina una tesis previamente capturada
        /// </summary>
        /// <param name="tesis"></param>
        public void DeleteTesis(Tesis tesis,ObservableCollection<Tesis> listaTesis)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd = new OleDbCommand();

            
            cmd.Connection = connection;


            try
            {
                connection.Open();

                cmd.CommandText = "DELETE FROM Tesis WHERE IdTesis = @IdTesis";
                cmd.Parameters.AddWithValue("@IdTesis", tesis.IdTesis);
                cmd.ExecuteNonQuery();

                listaTesis.Remove(tesis);
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
        /// Invalida la validación previa de una tesis 
        /// </summary>
        /// <param name="tesis"></param>
        public void InvalidaValidacion(Tesis tesis)
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
                dr["IdUsuarioValida"] = 0;

                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                string sSql = "UPDATE Tesis " +
                              "SET IdUsuarioValida = @IdUsuarioValida" +
                              " WHERE IdTesis = @IdTesis";

                dataAdapter.UpdateCommand.CommandText = sSql;

                dataAdapter.UpdateCommand.Parameters.Add("@IdUsuarioValida", OleDbType.Numeric, 0, "IdUsuarioValida");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTesis", OleDbType.Numeric, 0, "IdTesis");


                dataAdapter.Update(dataSet, "Tesis");
                dataSet.Dispose();
                dataAdapter.Dispose();

                tesis.IdUsuarioValida = 0;
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
