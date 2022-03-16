using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class LogicForTransaction
    {
        public void AddDbForTransaction()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("Id", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Amount", typeof(Decimal)));
            tbl.Columns.Add(new DataColumn("Type", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("AccountId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            tbl.Columns.Add(new DataColumn("Currency", typeof(Int32)));

            Random rnd = new Random();
            SqlParameter parameter = new SqlParameter();
            var amount = 0;
            var currencyFrom = 0;
            var helper = new Helper();
            parameter.ParameterName = "@Datetime2";
            parameter.SqlDbType = SqlDbType.DateTime2;

            var rep = new Repository();

            var accList = rep.GetAccountList();

            for (int i = 0; i < 2000; i++)
            {
                var randomNuber = rnd.Next(1, accList.Count);
                var acc = accList[randomNuber];

                DataRow dr = tbl.NewRow();
                var type = rnd.Next(1,4);
                var accountId = acc.Id;
                var currency = acc.CurrencyType;
                var date = helper.GetRandomDateTime();


                if (type == 1)
                {
                    amount = rnd.Next(0, 100000000);
                }
                if (type == 2)
                {
                    amount = rnd.Next(-100000000, 0);
                }
                if (type == 3)
                {
                    int accountTo;
                    do
                    {
                        accountTo = rnd.Next(1, accList.Count+1);
                    } while (accountTo ==  acc.Id);

                    amount = rnd.Next(0, 100000000);

                    DataRow drt = tbl.NewRow();
                    drt["Id"] = i;
                    drt["Amount"] = rnd.Next(-100000000, 0);
                    drt["Type"] = type;
                    drt["AccountId"] = accountTo;
                    drt["Date"] = date;
                    drt["Currency"] = accList[accountTo].CurrencyType;
                    tbl.Rows.Add(drt);
                    i++;
                }

                dr["Id"] = i;
                dr["Amount"] = amount;
                dr["Type"] = type;
                dr["AccountId"] = acc.Id;
                dr["Date"] = date;
                dr["Currency"] = currency;

                tbl.Rows.Add(dr);

            }


            string connection = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = MarvelousReportMicroService.DB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);


            objbulk.DestinationTableName = "Transaction";
            objbulk.ColumnMappings.Add("Id", "Id");
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
