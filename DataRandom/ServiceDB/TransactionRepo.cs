using Dapper;
using DataRandom.ServiceDB.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataRandom.ServiceDB
{
    public class TransactionRepo
    {
        public string connection = "Data Source=(local);Initial Catalog=TransactionStore.DB;Integrated Security=True;";

        public List<Transaction> GetTransactionList()
        {
            using IDbConnection connectionString = new SqlConnection(connection);
            var transactions = connectionString.Query<Transaction>(
                "select Id, AccountId from dbo.Transaction"
            ).ToList();

            return transactions;
        }
    }
}
