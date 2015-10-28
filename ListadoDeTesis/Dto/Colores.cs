using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using ScjnUtilities;

namespace ListadoDeTesis.Dto
{
    public class Colores
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Listado"].ConnectionString;

        private int idColor;
        private string color;
        private string descripcion;

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

        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
            }
        }

        

        public ObservableCollection<Colores> GetColores()
        {
            ObservableCollection<Colores> listaColores = new ObservableCollection<Colores>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;

            String sqlCadena = "SELECT * FROM Colores ORDER BY IdColor";

            try
            {
                connection.Open();

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Colores color = new Colores();
                        color.idColor = Convert.ToInt32(reader["IdColor"]);
                        color.Color = reader["Color"].ToString();
                        color.descripcion = reader["Descripcion"].ToString();

                        listaColores.Add(color);
                    }
                }
                cmd.Dispose();
                reader.Close();
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,Colores", "ListadoDeTesis");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,Colores", "ListadoDeTesis");
            }
            finally
            {
                connection.Close();
            }

            return listaColores;
        }
    }
}
