using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class LogicForAccount
    {


        public void AddDbForAccount()
        {
            DataTable acc = new DataTable();
            acc.Columns.Add(new DataColumn("Id", typeof(Int32)));
            acc.Columns.Add(new DataColumn("Name", typeof(string)));
            acc.Columns.Add(new DataColumn("CurrencyType", typeof(Int32)));
            acc.Columns.Add(new DataColumn("LeadId", typeof(Int32)));
            acc.Columns.Add(new DataColumn("LockDate", typeof(DateTime)));
            acc.Columns.Add(new DataColumn("IsBlocked", typeof(bool)));


            Random rnd = new Random();
            Repository rep = new Repository();
            Helper hel = new Helper();
            var leads = rep.GetList();
            var accId = 1;


            var currencyId = new List<int> { 85, 107, 34, 52, 22, 102, 88 };
            List<int> randomCurrencyTypeList = new List<int>();
            List<int> distinctRandomCurrencyTypeList = new List<int>();

            foreach (var item in leads)
            {
                if (item.Role == 2)
                {
                    var eee = rnd.Next(1, 8);
                    for (int i = 0; i < eee; i++)
                    {
                        randomCurrencyTypeList.Add(rnd.Next(0, 8));
                    }
                    distinctRandomCurrencyTypeList = randomCurrencyTypeList.Distinct().ToList();
                }
                else if (item.Role == 3)
                {
                    var eee = rnd.Next(1, 3);
                    for (int i = 0; i < eee; i++)
                    {
                        randomCurrencyTypeList.Add(rnd.Next(0, 2));
                    }
                    distinctRandomCurrencyTypeList = randomCurrencyTypeList.Distinct().ToList();

                }
                for (int i = 0; i < distinctRandomCurrencyTypeList.Count; i++)
                {
                    DataRow accR = acc.NewRow();
                    accR["Id"] = accId;
                    accR["Name"] = hel.GetPass();
                    accR["CurrencyType"] = currencyId[distinctRandomCurrencyTypeList[i]];
                    accR["LeadId"] = item.Id;
                    accR["LockDate"] = null;
                    accR["IsBlocked"] = false;
                    acc.Rows.Add(accR);
                }


            }

            string connection = "Data Source = 80.78.240.16; Database = CRM.Db; User Id = student; Password = qwe!23;";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);


            objbulk.DestinationTableName = "TransactionStore";
            objbulk.ColumnMappings.Add("Id", "Id");
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("CurrencyType", "CurrencyType");
            objbulk.ColumnMappings.Add("LeadId", "LeadId");
            objbulk.ColumnMappings.Add("LockDate", "LockDate");
            objbulk.ColumnMappings.Add("IsBlocked", "IsBlocked");

            con.Open();
            objbulk.WriteToServer(acc);
            con.Close();


        }


    }
}
