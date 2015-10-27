using System;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
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
    }
}
