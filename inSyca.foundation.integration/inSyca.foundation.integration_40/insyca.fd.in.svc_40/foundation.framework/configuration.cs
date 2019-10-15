using inSyca.foundation.framework.configuration;

namespace inSyca.foundation.integration.service
{
    [RegistryKeySource(@"SOFTWARE\inSyca\foundation.integration.biztalk")]
    internal class Configuration : ConfigurationBase<Configuration>
    {
    }
}
