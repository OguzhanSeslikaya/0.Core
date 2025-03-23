namespace _0.Core.Helpers
{
    public static class CacheHelper
    {
        public static string GenerateKey(params object[] keyParams)
        {
            return string.Join("_", keyParams.Where(a => !string.IsNullOrEmpty(Convert.ToString(a))));
        }
    }
}
