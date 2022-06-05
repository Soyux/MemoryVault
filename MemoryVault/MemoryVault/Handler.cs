using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MemoryVault
{
    public class Handler:IHandler
    {
        private SqliteConnection memoryconn;
        private SqliteConnection GetColdConnection { 
            get => new SqliteConnection(MemoryVault.Properties.Resources.ConnectionString); }
        
        public  async void Bootup() {
          
            //var conn = GetColdConnection;
            
            //var connstring = String.Format(@"Data Source=C:\sqlite\GVault.db;Version=3", AppDomain.CurrentDomain.BaseDirectory);
            //var conn = new SqliteConnection(connstring);
            var conn = new SqliteConnectionStringBuilder();
            conn.DataSource = @"C:\sqlite\GVault.db";
            var conn1 = new SqliteConnection();
            conn1.ConnectionString = conn.ConnectionString;
            conn1.Open();
            conn.DataSource = ":memory:";
            memoryconn = new SqliteConnection(conn.ConnectionString);
            
            conn1.BackupDatabase(memoryconn);
            await memoryconn.OpenAsync();
        }//end of Bootup

        public async Task<string> GetValue(string hash, SqliteConnection conn) {
            var query = "select value from Vault where hash=\"$hash\"";
         
            if (conn.State != System.Data.ConnectionState.Open) {
                await conn.OpenAsync();
            }

            List<object[]> resultado = new List<object[]>();
            using (SqliteCommand cmd = new SqliteCommand(query, conn))
            {
                using (SqliteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        object[] row = new object[rdr.FieldCount];
                        rdr.GetValues(row);
                        resultado.Add(row);
                    }//while
                }//using
            }//using
            return "";
        }

        public async Task<string> GetValueMemory(string hash) {
            return await GetValue(hash, memoryconn);
        }//end of GetValueMemory

        public async Task<string> GetValueCold(string hash){
            return await GetValue(hash, GetColdConnection);
        }//end of GetValueMemory

    }//end of class

}//end of namespace
