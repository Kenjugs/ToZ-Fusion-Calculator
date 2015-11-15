using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Skill_Calculator.utilities {
    public class DbManager {
        public string ConnectionString { get; set; } =
            ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;

        public string Provider { get; set; } = ConfigurationManager.ConnectionStrings["localDB"].ProviderName;

        public SqlConnection Connection { get; } = new SqlConnection();

        public void Close() {
            if (Connection.State != ConnectionState.Closed) {
                Connection.Close();
            }
        }

        public SqlConnection ConnectToDatabase() {
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
            return Connection;
        }
    }
}