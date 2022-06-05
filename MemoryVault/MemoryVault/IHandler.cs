using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryVault
{
    public interface IHandler
    {
        public void Bootup();
        public Task<string> GetValue(string hash, SqliteConnection conn);
        public Task<string> GetValueMemory(string hash);
        public Task<string> GetValueCold(string hash);


    }//end of interface
}//end of namespace
