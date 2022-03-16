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
    }
}
