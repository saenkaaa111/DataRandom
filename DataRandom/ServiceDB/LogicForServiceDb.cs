using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class LogicForServiceDb
    {
        public void AddServiceToLeadTable()
        {
            DataTable serviceToLead = new DataTable();
            serviceToLead.Columns.Add(new DataColumn("Id", typeof(Int32)));
            serviceToLead.Columns.Add(new DataColumn("Period", typeof(Int32)));
            serviceToLead.Columns.Add(new DataColumn("Price", typeof(Int32)));
            serviceToLead.Columns.Add(new DataColumn("Status", typeof(Int32)));
            serviceToLead.Columns.Add(new DataColumn("LeadId", typeof(Int32)));
            serviceToLead.Columns.Add(new DataColumn("ServiceId", typeof(Int32)));

            Random rnd = new Random();
            Repository CrmRepo = new Repository();
            var rubAccounts = CrmRepo.GetRubAccountsWithLeadIds(); 

            List<int> prices = new List<int> { 1500, 3000, 5000, 7500, 10000 };

            for (int i = 0; i < rubAccounts.Count; i++)
            {
                int status = 1;
                int typeId = rnd.Next(1, 3);
                int randomPrice = rnd.Next(0, prices.Count);
                int serviceId = rnd.Next(1, 6);
                int price = prices[randomPrice];
                int period = rnd.Next(1, 5);

                if (period == 1 && typeId == 1)
                {
                    price = prices[0];
                    serviceId = rnd.Next(1, 3);
                }
                else if (period == 2 && typeId == 2)
                {
                    price = prices[1];
                    serviceId = rnd.Next(3, 6);
                }
                else if (period == 3 && typeId == 2)
                {
                    randomPrice = rnd.Next(2, 4);
                    price = prices[randomPrice];
                    serviceId = rnd.Next(3, 6);
                }
                else if (period == 4 && typeId == 2)
                {
                    price = prices[4];
                    serviceId = rnd.Next(3, 6);
                }
                else
                {
                    period = 1;
                    price = 1500;
                    serviceId = rnd.Next(1, 2);
                }

                if ((i + 1) % 1600 == 0 && period != 1) // 500 лидов из 800тыс имеют неактивную подписку
                {
                    status = 2;
                    serviceId = rnd.Next(3, 6);
                    randomPrice = rnd.Next(1, prices.Count);
                    price = prices[randomPrice];
                }

                DataRow dr = serviceToLead.NewRow();

                dr["Id"] = i + 1;
                dr["Period"] = period;
                dr["Price"] = price;
                dr["LeadId"] = rubAccounts[i].LeadId;
                dr["ServiceId"] = serviceId;
                dr["Status"] = status;

                serviceToLead.Rows.Add(dr);
            }

            string connection = "Data Source=80.78.240.16;Initial Catalog=MarvelousService.DB;User ID = student; Password = qwe!23";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "[ServiceToLead]";
            objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("Period", "Period");
            objbulk.ColumnMappings.Add("Price", "Price");
            objbulk.ColumnMappings.Add("LeadId", "LeadId");
            objbulk.ColumnMappings.Add("ServiceId", "ServiceId");
            objbulk.ColumnMappings.Add("Status", "Status");

            con.Open();
            objbulk.WriteToServer(serviceToLead);
            con.Close();
        }
    }
}
