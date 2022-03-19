using System.Data;
using System.Data.SqlClient;

namespace DataRandom.ServiceDB
{
    public class LogicForServicePaymentTable
    {
        public void AddServiceToLeadTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("TransactionId", typeof(Int32)));
            tbl.Columns.Add(new DataColumn("ServiceToLeadId", typeof(Int32)));

            Random rnd = new Random();
            TransactionRepo transRepo = new TransactionRepo();
            var transList = transRepo.GetTransactionList();

            for (int i = 0; i < transList.Count; i++)
            {
                DataRow drt = tbl.NewRow();

                drt["TransactionId"] = transList[i].Id;
                //drt["ServiceToLeadId"] = price;

                tbl.Rows.Add(drt);
            }

            string connection = "Data Source=(local);Initial Catalog=MarvelousService.DB;Integrated Security=True;";
            SqlConnection con = new SqlConnection(connection);
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "[ServicePayment]";
            objbulk.ColumnMappings.Add("TransactionId", "TransactionId");
            objbulk.ColumnMappings.Add("ServiceToLeadId", "ServiceToLeadId");

            con.Open();
            objbulk.WriteToServer(tbl);
            con.Close();
        }
    }
}
