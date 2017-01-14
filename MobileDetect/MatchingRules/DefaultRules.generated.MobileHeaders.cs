using System.Collections.Generic;

namespace MobileDetect.Rules
{
    public partial class DefaultRules
    {
        public static Dictionary<string, string[]> MobileHeaders = new Dictionary<string, string[]> {
            {"HTTP_ACCEPT", new [] {@"application/x-obml2d", @"application/vnd.rim.html", @"text/vnd.wap.wml", @"application/vnd.wap.xhtml+xml"} },
			{"HTTP_X_WAP_PROFILE", null },
			{"HTTP_X_WAP_CLIENTID", null },
			{"HTTP_WAP_CONNECTION", null },
			{"HTTP_PROFILE", null },
			{"HTTP_X_OPERAMINI_PHONE_UA", null },
			{"HTTP_X_NOKIA_GATEWAY_ID", null },
			{"HTTP_X_ORANGE_ID", null },
			{"HTTP_X_VODAFONE_3GPDPCONTEXT", null },
			{"HTTP_X_HUAWEI_USERID", null },
			{"HTTP_UA_OS", null },
			{"HTTP_X_MOBILE_GATEWAY", null },
			{"HTTP_X_ATT_DEVICEID", null },
			{"HTTP_UA_CPU", new [] {@"ARM"} }
        };
    }
}
