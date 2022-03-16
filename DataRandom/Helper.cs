using System.Data;
using System.Data.SqlClient;

namespace DataRandom
{
    public class Helper
    {
        public string GetPass()
        {
            string pass = "";
            var r = new Random();
            while (pass.Length < 10)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }

        public Object GetRandomDateTime()
        {

            SqlParameter parameter = new SqlParameter();
            var day = 0;
            parameter.ParameterName = "@Datetime2";
            parameter.SqlDbType = SqlDbType.DateTime2;
            Random rnd = new Random();
            var year = rnd.Next(2000, 2023);
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
            return time;
        }
    }
}
