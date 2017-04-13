using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace inSyca.foundation.communication.wcf
{
    public class ImpersonateNtlmCredentialsBehaviourElement : BehaviorExtensionElement
    {
        [ConfigurationProperty("Username", DefaultValue = "<enter username>")]
        public string Username

        {
            get { return (string)base["Username"]; }
            set { base["Username"] = value; }
        }

        [ConfigurationProperty("Password", DefaultValue = "<enter password>")]
        public string Password
        {
            get { return (string)base["Password"]; }
            set { base["Password"] = value; }
        }

        [ConfigurationProperty("Active", DefaultValue = false)]
        public bool Active
        {
            get { return (bool)base["Active"]; }
            set { base["Active"] = value; }
        }

        [ConfigurationProperty("Domain", DefaultValue = "<enter Domain>")]
        public string Domain
        {
            get { return (string)base["Domain"]; }
            set { base["Domain"] = value; }
        }

        protected override object CreateBehavior()
        {
            return new ImpersonateNtlmCredentialsBehaviour(this);
        }

        public override Type BehaviorType
        {
            get { return typeof(ImpersonateNtlmCredentialsBehaviour); }
        }
    }
}