using Dapper;
using DataRandom.ServiceDB.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataRandom.ServiceDB
{
    public class ServiceRepo
    {
        public string connection = "Data Source=(local);Initial Catalog=MarvelousService.DB;Integrated Security=True;";

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
