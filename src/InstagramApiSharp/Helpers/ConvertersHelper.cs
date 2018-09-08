using InstagramApiSharp.Converters;

namespace InstagramApiSharp.Helpers
{
    public static class ConvertersHelper
    {
        public static IConvertersFabric GetDefaultFabric()
        {
            return ConvertersFabric.Instance;
        }
    }
}