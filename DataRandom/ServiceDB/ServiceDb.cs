using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class ServiceDb
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
            int serviceToLeadId = 1;
            int LeadId = 300200;
            List<int> prices = new List<int> { 1500, 3000, 5000, 7500, 10000 };

            for (int i = 0; i < 200000; i++)
            {
                int status = 1;
                int randomPrice = rnd.Next(0, prices.Count);
                int serviceId = rnd.Next(1, 6);
                int price = prices[randomPrice];
                int period = rnd.Next(1, 5);

                if (period == 1)
                {
                    price = prices[0];
                    serviceId = rnd.Next(1, 3);
                }
                else if (period == 2)
                {
                    price = prices[1];
                }
                else if (period == 3)
                {
                    randomPrice = rnd.Next(3, 5);
                    price = prices[randomPrice];
                }
                else if (period == 4)
                {
                    price = prices[4];
                }

                if (i % 1600 == 0 && period != 1) // каждому 1600 лиду неактивную подписку
                {
                    status = 2;
                    serviceId = rnd.Next(3, 6);
                    randomPrice = rnd.Next(1, prices.Count);
                    price = prices[randomPrice];
                }

                DataRow dr = serviceToLead.NewRow();

                dr["Id"] = serviceToLeadId + i;
                dr["Period"] = period;
                dr["Price"] = price;
                dr["LeadId"] = LeadId + i;
                dr["ServiceId"] = serviceId;
                dr["Status"] = status;

                serviceToLead.Rows.Add(dr);
            }

            string connection = "Data Source=(local);Initial Catalog=MarvelousService.DB;Integrated Security=True;";
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
