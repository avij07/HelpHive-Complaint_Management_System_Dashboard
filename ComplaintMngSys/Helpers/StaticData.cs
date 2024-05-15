
namespace ComplaintMngSys.Helpers
{
    public static class StaticData
    {
        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public static string GetUniqueID(string Prefix)
        {
            Random _Random = new Random();
            var result = Prefix + DateTime.Now.ToString("yyyyMMddHHmmss") + _Random.Next(1, 1000);
            return result;
        }
        public static class ConnectionStrings
        {
            public const string connMSSQLNoCred = "connMSSQLNoCred";
            public const string connMSSQL = "connMSSQL";
            public const string connPostgreSQL = "connPostgreSQL";
            public const string connMySQL = "connMySQL";
            public const string connDockerBase = "connDockerBase";
            public const string connMSSQLProd = "connMSSQLProd";
            public const string connOthers = "connOthers";
        }
    }
}
