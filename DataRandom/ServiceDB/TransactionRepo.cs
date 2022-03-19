using Dapper;
using DataRandom.ServiceDB.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataRandom.ServiceDB
{
    public class TransactionRepo
    {
        public string connection = "Data Source = 80.78.240.16; Database=TransactionStore.DB;User Id = student; Password=qwe!23;";

        public List<Transaction> GetTransactionList()
        {
            using IDbConnection connectionString = new SqlConnection(connection);
            var transactions = connectionString.Query<Transaction>(
                "select Id, AccountId from dbo.Transaction where Type = 4"
            ).ToList();

            return transactions;
        }
    }
}
