using Dapper;
using DataRandom.ServiceDB.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataRandom.ServiceDB
{
    public class ServiceRepo
    {
        public string connection = "Data Source=80.78.240.16;Initial Catalog=MarvelousService.DB;User ID = student; Password = qwe!23";

        public List<ServiceToLead> GetServiceToLeadList()
        {
            using IDbConnection connectionString = new SqlConnection(connection);
            var serviceToLeads = connectionString.Query<ServiceToLead>(
                "select LeadId, Price from dbo.ServiceToLead"
            ).ToList();

            return serviceToLeads;
        }
    }
}
