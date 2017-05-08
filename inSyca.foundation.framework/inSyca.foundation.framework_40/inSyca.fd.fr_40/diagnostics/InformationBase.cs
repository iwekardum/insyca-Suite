using System.Reflection;

namespace inSyca.foundation.framework.diagnostics
{
    public interface IInformation
    {
        string Version { get; }
        string Location { get; }
    }

    public abstract class InformationBase <T> : IInformation
    {
        public string Version
        {
            get
            {
                return Assembly.GetAssembly(typeof(T)).GetName().Version.ToString();
            }
        }

        public string Location
        {
            get
            {
                return Assembly.GetAssembly(typeof(T)).Location.ToString();
            }
        }
    }
}
