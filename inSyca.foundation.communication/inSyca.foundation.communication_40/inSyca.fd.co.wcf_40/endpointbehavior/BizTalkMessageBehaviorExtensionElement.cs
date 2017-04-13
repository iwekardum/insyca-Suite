using inSyca.foundation.communication.wcf.diagnostics;
using inSyca.foundation.framework;
using System;
using System.ServiceModel.Configuration;

namespace inSyca.foundation.communication.wcf
{
    public class BizTalkMessageBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            Log.Debug("CreateBehavior()");

            return new BizTalkMessageBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                Log.Debug("_getBehaviorType");

                return typeof(BizTalkMessageBehavior);
            }
        }
    }
}
