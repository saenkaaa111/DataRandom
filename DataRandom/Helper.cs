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
            var year = rnd.Next(2018, 2023);
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
                day = rnd.Next(1, 29);
            }

            var hour = rnd.Next(1, 24);
            var minute = rnd.Next(1, 60);
            var second = rnd.Next(1, 60);
            var millisecond = rnd.Next(1, 60);
            var time = parameter.Value = DateTime.Parse($"{year}-{month}-{day} {hour}:{minute}:{second}.{millisecond}");
            return time;
        }

        public const string BaseCurrency = "USD";
        public decimal ConvertCurrency(string currencyFrom, string currencyTo, decimal amount)
        {
            var rates = GetRates();

            rates.TryGetValue($"{BaseCurrency}{currencyFrom}", out var currencyFromValue);
            rates.TryGetValue($"{BaseCurrency}{currencyTo}", out var currencyToValue);
            if (currencyFrom == BaseCurrency)
                currencyFromValue = 1m;

            if (currencyTo == BaseCurrency)
                currencyToValue = 1m;

            var convertAmount = decimal.Round(currencyToValue / currencyFromValue * amount, 2);

            return convertAmount;
        }

        public Dictionary<string, decimal> GetRates()
        {
            return new Dictionary<string, decimal>
            {
                { "USDRUB", 105m },
                { "USDEUR", 0.9m },
                { "USDJPY", 118m },
                { "USDCNY", 6m },
                { "USDTRY", 14m },
                { "USDRSD", 106m },
                { "RUBUSD", 0.009m },
                { "EURUSD", 1.11m },
                { "JPYUSD", 0.008m },
                { "CNYUSD", 0.16m },
                { "TRYUSD", 0.068m },
                { "RSDUSD", 2.65m },

            };
        }
    }
}

