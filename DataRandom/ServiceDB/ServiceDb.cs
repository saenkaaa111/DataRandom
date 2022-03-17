using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class ServiceDb
    {
        public void AddDbForService()
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
        }
    }
}
