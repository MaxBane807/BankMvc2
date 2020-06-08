namespace Bank.Web.Extensions
{
    public static class Extension
    {
        public static bool IsInteger(this string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
    }
}
