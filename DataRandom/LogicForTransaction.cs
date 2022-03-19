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

            for (int i = 0; i < 900000; i++)
            {
                var randomNuber = rnd.Next(1, accList.Count);
                var acc = accList[randomNuber];

                DataRow dr = tbl.NewRow();
                int type = rnd.Next(1, 4); 
                var currency = acc.CurrencyType;
                var date = helper.GetRandomDateTime();
                var currencyType = 0;
                var negativeAmount = rnd.Next(-10000, 0);
                if (type == 1)
                {
                    amount = rnd.Next(0, 10000);
                }
                else if (type == 2)
                {
                    amount = rnd.Next(-10000, 0);
                }
                else
                {
                    int accountTo;
                    do
                    {
                        accountTo = rnd.Next(1, accList.Count);
                        currencyType = accList[accountTo].CurrencyType;
                    } 
                    while (accountTo ==  acc.Id || currencyType == currency);

                    amount = rnd.Next(0, 10000);

                    DataRow drt = tbl.NewRow();
                    drt["Id"] = i;
                    drt["Amount"] = negativeAmount;
                    drt["Type"] = type;
                    drt["AccountId"] = accountTo;
                    drt["Date"] = date;
                    drt["Currency"] = currencyType;
                    tbl.Rows.Add(drt);
                    i++;
                }

                var currencyTypeString = "";
                switch (currencyType)
                {
                    case 107:
                        currencyTypeString = "USD";
                        break;
                    case 85:
                        currencyTypeString = "RUB";
                        break;
                    case 34:
                        currencyTypeString = "EUR";
                        break;
                    case 52:
                        currencyTypeString = "JPY";
                        break;
                    case 22:
                        currencyTypeString = "CNY";
                        break;
                    case 88:
                        currencyTypeString = "RSD";
                        break;
                    case 102:
                        currencyTypeString = "TRY";
                        break;
                }

                var currencyString = "";
                switch (currency)
                {
                    case 107:
                        currencyString = "USD";
                        break;
                    case 85:
                        currencyString = "RUB";
                        break;
                    case 34:
                        currencyString = "EUR";
                        break;
                    case 52:
                        currencyString = "JPY";
                        break;
                    case 22:
                        currencyString = "CNY";
                        break;
                    case 88:
                        currencyString = "RSD";
                        break;
                    case 102:
                        currencyString = "TRY";
                        break;
                }

                dr["Id"] = i;
                if (type == 3)
                {
                    dr["Amount"] = helper.ConvertCurrency(currencyTypeString, currencyString, Math.Abs(negativeAmount));

                }
                else
                    dr["Amount"] = amount;

                dr["Type"] = type;
                dr["AccountId"] = acc.Id;
                dr["Date"] = date;
                dr["Currency"] = currency;

                tbl.Rows.Add(dr);
            }

            string connection = "Data Source = 80.78.240.16; Database=TransactionStore.DB;User Id = student; Password=qwe!23;";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "[Transaction]";
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
