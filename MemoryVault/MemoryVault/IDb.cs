using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
namespace MemoryVault
{
    public interface IDb
    {
        SqliteConnection conn { get; set; }
        void OpenConnection();
        dynamic Execute(string query);
        
    }//end of interface

}//end of namespace
