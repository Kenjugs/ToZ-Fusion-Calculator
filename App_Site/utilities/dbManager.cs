using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Skill_Calculator.utilities {

    public class DbManager {

        private static string ConnectionString { get; set; } = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;

        public static string Provider { get; set; } = ConfigurationManager.ConnectionStrings["localDB"].ProviderName;

        public static SqlConnection Connection { get; set; } = new SqlConnection(ConnectionString);

        private SqlTransaction CurrentTransaction { get; set; } = null;

        public static SqlConnection ConnectToDatabase() {

            if (Connection.State != ConnectionState.Open) {
                Connection.Open();
            }

            return Connection;
        }

        public static void CloseConnection() {

            if (Connection.State != ConnectionState.Closed) {
                Connection.Close();
            }
        }

        public void BeginTransaction() {

            if (Connection.State != ConnectionState.Closed) {
                CurrentTransaction = Connection.BeginTransaction();
            } else {
                CurrentTransaction = null;
            }
        }

        public void RollbackTransaction() {

            if (CurrentTransaction != null) {
                CurrentTransaction.Rollback();
            }

            CurrentTransaction = null;
        }

        public void CommitTransaction() {

            if (CurrentTransaction != null) {
                CurrentTransaction.Commit();
            }

            CurrentTransaction = null;
        }

        public static void ExecuteNonQuery(ref SqlCommand cmd, List<SqlParameter> paramList) {

            foreach (SqlParameter param in paramList) {
                cmd.Parameters.Add(param);
            }

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public static DataSet GetDataSet(SqlCommand cmd, List<SqlParameter> paramList) {

            foreach (SqlParameter param in paramList) {
                cmd.Parameters.Add(param);
            }

            DataSet retval = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(retval);
            cmd.Parameters.Clear();

            return retval;
        }

        public static void AppendDataSet(SqlCommand cmd, List<SqlParameter> paramList, ref DataSet ds) {

            foreach (SqlParameter param in paramList) {
                cmd.Parameters.Add(param);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            cmd.Parameters.Clear();
        }

        public static void AppendDataTable(SqlCommand cmd, List<SqlParameter> paramList, ref DataTable dt) {

            foreach (SqlParameter param in paramList) {
                cmd.Parameters.Add(param);
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            cmd.Parameters.Clear();
        }
    }
}