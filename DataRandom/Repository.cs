using System.Data;
using System.Data.SqlClient;
using Dapper;
using DataRandom.ServiceDB;

namespace DataRandom
{
    public class Repository
    {
        public string connection = "Data Source = 80.78.240.16; Database=CRM.Db;User Id = student; Password=qwe!23;";

        public List<Lead> GetLeadList()
        {
            using IDbConnection connectionString = new SqlConnection(connection);
            var listLeads = connectionString.Query<Lead>(
                "dbo.Lead_SelectAll",
                commandType: CommandType.StoredProcedure
            ).ToList();

            return listLeads;
        }

        public List<Account> GetAccountList()
        {
            using IDbConnection connectionString = new SqlConnection(connection);
            var listAccounts = connectionString.Query<Account>(
                "SELECT * FROM Account WHERE id BETWEEN 9500000 AND 10000000"
            ).ToList();

            return listAccounts;
        }

        public List<AccountWithLeadId> GetRubAccountsWithLeadIds()
        {
            using IDbConnection connectionString = new SqlConnection(connection);
            var leadIds = connectionString.Query<AccountWithLeadId>(
                "SELECT TOP(30000) Id, LeadId FROM dbo.[Account] WHERE CurrencyType = 85"
            ).ToList();

            return leadIds;
        }


    }
}
