using inSyca.foundation.communication.wcf.diagnostics;
using System;
using System.ServiceModel.Configuration;

namespace inSyca.foundation.communication.wcf
{
    public class BizTalkParameterBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            Log.Debug("CreateBehavior()");

            return new BizTalkParameterBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                Log.Debug("_getBehaviorType");

                return typeof(BizTalkParameterBehavior);
            }
        }
    }


}
