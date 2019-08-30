using System;
using System.Data;
using System.Data.SqlClient;
using DemoStore.Shared;


namespace DemoStore.Infra.DataContexts
{
    public class MssqlDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public MssqlDataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}