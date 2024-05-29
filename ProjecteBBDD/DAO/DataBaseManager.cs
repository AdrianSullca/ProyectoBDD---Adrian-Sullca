using MySql.Data.MySqlClient;
using ProjecteBBDD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ProjecteBBDD.DAO
{
    public static class DataBaseManager
    {
        private const string server = "db4free.net";
        private const string port = "3306";
        private const string database = "bdd_cinema";
        private const string username = "adrianrootsullca";
        private const string password = "adrianroot631";
        private const string connectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=" + username + ";Pwd=" + password + ";OldGuids=true;AllowUserVariables=true";

        private static MySqlConnection connection;

        public const string scriptCinema = "../../../Scripts/scriptCinema.sql";
        public const string scriptSPGetClienteCMT = "../../../Scripts/SPGetClienteConMasTransacciones.sql";
        public const string scriptSPGetAllDataBDD = "../../../Scripts/SPGetAllDataBDD.sql";
        public const string scriptTriggerInsert = "../../../Scripts/TriggerInsert.sql";
        public const string scriptTriggerUpdate = "../../../Scripts/TriggerActualizar.sql";
        public const string scriptTriggerDelete = "../../../Scripts/TriggerDelete.sql";
        public const string scriptDeleteTriggers = "../../../Scripts/EliminarTriggers.sql";

        public static void EjecutarScript(string pathScript)
        {
            try
            {
                string script = File.ReadAllText(pathScript);
                MySqlConnection connection = getConnection();
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(script, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar el script: " + ex.Message);
            }
        }

        public static MySqlConnection getConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }
            return connection;
        }

        public static void CerrarConexion()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
