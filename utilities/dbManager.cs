using System.Configuration;
using System.Data.SqlClient;

namespace Skill_Calculator.utilities {

    public class dbManager {
        private SqlConnection _conn = new SqlConnection();
        private string _connStr = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        private string _provStr = ConfigurationManager.ConnectionStrings["localDB"].ProviderName;

        public string ConnectionString {
            get { return _connStr; }
            set { _connStr = value; }
        }

        public string Provider {
            get { return _provStr; }
            set { _provStr = value; }
        }

        public SqlConnection Connection {
            get { return _conn; }
        }

        public void Close() {
            if (Connection.State != System.Data.ConnectionState.Closed) {
                Connection.Close();
            }
        }

        public SqlConnection ConnectToDB() {
            Connection.ConnectionString = _connStr;
            Connection.Open();
            return Connection;
        }
    }
}