namespace Singleton
{
    using Domain;


    #region Interface
    public interface IConfigurationProvider
    {
        decimal TaxRate { get; }
        string Currency { get; }

    }

    #endregion
    #region Singleton
    public sealed class ConfigurationProvider : IConfigurationProvider
    {
        private static readonly Lazy<ConfigurationProvider> _lazy = new Lazy<ConfigurationProvider>(() => new ConfigurationProvider());

        public static ConfigurationProvider Instance => _lazy.Value;

        public decimal TaxRate { get; } = 0.22m;
        public string Currency { get; } = "EUR";

        private ConfigurationProvider() { }

    }
}
#endregion