using DataRandom.ServiceDB;
using DataRandom.ServiceDB.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class LogicForServiceTransaction
    {
        public void AddDbForTransaction()
        {
            DataTable tbl = new DataTable();
            //tbl.Columns.Add(new DataColumn("Id", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Amount", typeof(Decimal)));
            tbl.Columns.Add(new DataColumn("Type", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("AccountId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("Currency", typeof(Int32)));

            Random rnd = new Random();
            SqlParameter parameter = new SqlParameter();
            var helper = new Helper();
            parameter.ParameterName = "@Datetime2";
            parameter.SqlDbType = SqlDbType.DateTime2;

            var rep = new Repository();
            var serviceRepo = new ServiceRepo();
            int lastTransactionId = 100;

            var rubAccounts = rep.GetRubAccountsWithLeadIds();
            List<ServiceToLead> serviceToLeads = serviceRepo.GetServiceToLeadList();


            for (int i = 0; i < serviceToLeads.Count; i++)
            {
                var date = helper.GetRandomDateTime();
                DataRow drt = tbl.NewRow();
                var rndTime = rnd.Next(1, 7);
                int delta = 0;

                if (serviceToLeads[i].Price == 1500)
                {
                    //drt["Id"] = lastTransactionId + i;
                    drt["Amount"] = -serviceToLeads[i - delta].Price;
                    drt["Type"] = 4;
                    drt["AccountId"] = rubAccounts[i - delta].Id;
                    drt["Date"] = date;
                    drt["Currency"] = 85;
                    tbl.Rows.Add(drt);
                }
                else
                {
                    for (int j = 0; j < rndTime; j++)
                    {
                        //drt["Id"] = lastTransactionId + j + i + 1;
                        drt["Amount"] = -serviceToLeads[i].Price;
                        drt["Type"] = 4;
                        drt["AccountId"] = rubAccounts[i].Id;
                        drt["Date"] = date;
                        drt["Currency"] = 85;
                        tbl.Rows.Add(drt);

                        if (serviceToLeads[i].Price == 3000)
                        {
                            var stringDate = date.ToString();
                            var initDate = DateTime.Parse(stringDate);
                            var nextDate = initDate.AddDays(7);
                            date = parameter.Value = nextDate;
                        }
                        else if (serviceToLeads[i].Price == 5000 || serviceToLeads[i].Price == 7500)
                        {
                            var stringDate = date.ToString();
                            var initDate = DateTime.Parse(stringDate);
                            var nextDate = initDate.AddMonths(1);
                            date = parameter.Value = nextDate;
                        }
                        else if (serviceToLeads[i].Price == 10000)
                        {
                            var stringDate = date.ToString();
                            var initDate = DateTime.Parse(stringDate);
                            var nextDate = initDate.AddYears(1);
                            date = parameter.Value = nextDate;
                        }

                        if (j + 1 < rndTime)
                        {
                            drt = tbl.NewRow();
                        }
                        //    delta = 1 + j;
                        //}
                        //else
                        //{
                        //    i += delta;
                        //}
                    }
                }
            }

            string connection = "Data Source=(local);Initial Catalog=TransactionStore.DB;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "[Transaction]";
            //objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("Amount", "Amount");
            objbulk.ColumnMappings.Add("Type", "Type");
            objbulk.ColumnMappings.Add("AccountId", "AccountId");
            objbulk.ColumnMappings.Add("Date", "Date");
            objbulk.ColumnMappings.Add("Currency", "Currency");

            con.Open();
            objbulk.WriteToServer(tbl);
            con.Close();


        }
    }
}

