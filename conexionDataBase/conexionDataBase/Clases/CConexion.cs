using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conexionDataBase.Clases
{
    class CConexion
    {
        MySqlConnection conex = new MySqlConnection();

        static string servidor = "localhost";
        static string bd = "prueba_bd";
        static string usuario = "root";
        static string password = "12345678";
        static string port = "3306";

        string cadenaConexion = "server=" + servidor + ";" + "port=" + port + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + bd + ";";
        
        public MySqlConnection establecerConexion()
        {
            try {
                conex.ConnectionString = cadenaConexion;
                conex.Open();
            }
            catch(MySqlException e)
            {
                MessageBox.Show("No se pudo conectar, error: " + e);
            }
            return conex;

    }
        public MySqlConnection cerrarConexion()
        {
            conex.Close();
            return conex;
        }

}
}