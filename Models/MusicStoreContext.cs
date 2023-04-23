using MySql.Data.MySqlClient;

namespace HelloMySql.Models
{
    public class MusicStoreContext
    {
        public string ConnectionString {get;set;}
        public MusicStoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}