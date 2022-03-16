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
            var accountIdFrom = 0;
            var currencyFrom = 0;
            var day = 0;
            parameter.ParameterName = "@Datetime2";
            parameter.SqlDbType = SqlDbType.DateTime2;


            for (int i = 0; i < 200; i++)
            {
                DataRow dr = tbl.NewRow();

                var year = rnd.Next(2020, 2023);
                var month = rnd.Next(1, 13);
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                {
                    day = rnd.Next(1, 32);
                }
                if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    day = rnd.Next(1, 31);
                }
                if (month == 2)
                {
                    day = rnd.Next(1, 30);
                }

                var hour = rnd.Next(1, 24);
                var minute = rnd.Next(1, 60);
                var second = rnd.Next(1, 60);
                var millisecond = rnd.Next(1, 60);
                var time = parameter.Value = DateTime.Parse($"{year}-{month}-{day} {hour}:{minute}:{second}.{millisecond}");

                var type = rnd.Next(1, 4);
                var accountId = rnd.Next(1, 1000);
                var currency = rnd.Next(1, 114);

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
                    do
                    {
                        accountIdFrom = rnd.Next(1, 1000);
                    } while (accountIdFrom == accountId);
                    do
                    {
                        currencyFrom = rnd.Next(1, 114);
                    } while (currencyFrom == currency);


                    DataRow drt = tbl.NewRow();
                    drt["Id"] = i;
                    drt["Amount"] = rnd.Next(-100000000, 0);
                    drt["Type"] = type;
                    drt["AccountId"] = accountIdFrom;
                    drt["Date"] = time;
                    drt["Currency"] = currencyFrom;
                    tbl.Rows.Add(drt);
                    i++;
                    amount = rnd.Next(0, 100000000);
                }


                dr["Id"] = i;
                dr["Amount"] = amount;
                dr["Type"] = type;
                dr["AccountId"] = accountId;
                dr["Date"] = time;
                dr["Currency"] = currency;

                tbl.Rows.Add(dr);

            }


            string connection = "Data Source=(local);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TransactionStore;";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);


            objbulk.DestinationTableName = "TransactionStore";
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
