using System;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace inSyca.foundation.communication.wcf
{
    public class ImpersonateNtlmCredentialsBehaviour : IEndpointBehavior
    {
        private ImpersonateNtlmCredentialsBehaviourElement config = null;

        public ImpersonateNtlmCredentialsBehaviour(ImpersonateNtlmCredentialsBehaviourElement config)
        {
            this.config = config;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            if (bindingParameters == null)
            {
                throw new ArgumentNullException("bindingParameters");
            }

            if (config.Active == true)
            {
                SecurityCredentialsManager manager = bindingParameters.Find<ClientCredentials>();

                if (manager != null)

                {
                    var cc = endpoint.Behaviors.Find<ClientCredentials>();

                    cc.UserName.Password = config.Password;

                    cc.UserName.UserName = config.Domain + @"\" + config.Username;

                    cc.Windows.ClientCredential.UserName = config.Username;

                    cc.Windows.ClientCredential.Password = config.Password;

                    cc.Windows.ClientCredential.Domain = config.Domain;
                }
                else
                {
                    var cc = endpoint.Behaviors.Find<ClientCredentials>();

                    cc.UserName.Password = config.Password;

                    cc.UserName.UserName = config.Domain + @"\" + config.Username;

                    cc.Windows.ClientCredential.UserName = config.Username;

                    cc.Windows.ClientCredential.Password = config.Password;

                    cc.Windows.ClientCredential.Domain = config.Domain;

                    bindingParameters.Add(this);
                }
            }
        }

       public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        { }

        public void Validate(ServiceEndpoint endpoint)
        { }

        #endregion

    }

}
