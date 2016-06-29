using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ListadoDeTesis.Dto;
using MantesisVerIusCommonObjects.Dto;
using ScjnUtilities;

namespace ListadoDeTesis.Models
{
    public class UsuariosModel
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Listado"].ConnectionString;

        public static bool ObtenerUsuarioContraseña()
        {
            bool bExisteUsuario = false;
            string sSql;

            OleDbConnection connection = new OleDbConnection(connectionString);

            OleDbCommand cmd;
            OleDbDataReader reader;


            try
            {
                connection.Open();

                sSql = "SELECT * FROM Usuarios WHERE usuario = @usuario AND Estado = 1";// AND Pass = @Pass";
                cmd = new OleDbCommand(sSql, connection);
                cmd.Parameters.AddWithValue("@usuario", Environment.UserName.ToUpper());
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    AccesoUsuarioModel.Usuario = reader["usuario"].ToString();
                    AccesoUsuarioModel.Pwd = reader["contrasena"].ToString();
                    AccesoUsuarioModel.Llave = Convert.ToInt16(reader["Llave"]);
                    AccesoUsuarioModel.Grupo = Convert.ToInt16(reader["Grupo"]);
                    AccesoUsuarioModel.Nombre = reader["nombre"].ToString();
                    bExisteUsuario = true;
                }
                else
                {
                    AccesoUsuarioModel.Llave = -1;
                }
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,UsuariosModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,UsuariosModel", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }

            return bExisteUsuario;
        }

        /// <summary>
        /// Obtiene los usuarios activos 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Usuarios> GetUsuarios()
        {
            ObservableCollection<Usuarios> listausuarios = new ObservableCollection<Usuarios>();

            Usuarios usuario = new Usuarios() { IdUsuario = Convert.ToInt32(1000), Nombre = "Todo", Usuario = "Todo" };

            listausuarios.Add(usuario);


            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            try
            {
                connection.Open();

                cmd = new OleDbCommand("SELECT * FROM Usuarios WHERE Estado = 1", connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        usuario = new Usuarios();
                        usuario.IdUsuario = Convert.ToInt32(reader["Llave"]);
                        usuario.Nombre = reader["Nombre"].ToString();
                        usuario.Usuario = reader["Usuario"].ToString();

                        listausuarios.Add(usuario);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,UsuariosModel", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,UsuariosModel", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }

            return listausuarios;
        }
    }
}
