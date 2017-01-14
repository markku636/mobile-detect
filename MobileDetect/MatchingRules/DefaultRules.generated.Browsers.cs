using System.Collections.Generic;

namespace MobileDetect.Rules
{
    public partial class DefaultRules
    {
        public static readonly Dictionary<string, string> Browsers = new Dictionary<string, string> {
            {"Chrome", @"\bCrMo\b|CriOS|Android.*Chrome/[.0-9]* (Mobile)?" },
			{"Dolfin", @"\bDolfin\b" },
			{"Opera", @"Opera.*Mini|Opera.*Mobi|Android.*Opera|Mobile.*OPR/[0-9.]+|Coast/[0-9.]+" },
			{"Skyfire", @"Skyfire" },
			{"Edge", @"Mobile Safari/[.0-9]* Edge" },
			{"IE", @"IEMobile|MSIEMobile" },
			{"Firefox", @"fennec|firefox.*maemo|(Mobile|Tablet).*Firefox|Firefox.*Mobile|FxiOS" },
			{"Bolt", @"bolt" },
			{"TeaShark", @"teashark" },
			{"Blazer", @"Blazer" },
			{"Safari", @"Version.*Mobile.*Safari|Safari.*Mobile|MobileSafari" },
			{"UCBrowser", @"UC.*Browser|UCWEB" },
			{"baiduboxapp", @"baiduboxapp" },
			{"baidubrowser", @"baidubrowser" },
			{"DiigoBrowser", @"DiigoBrowser" },
			{"Puffin", @"Puffin" },
			{"Mercury", @"\bMercury\b" },
			{"ObigoBrowser", @"Obigo" },
			{"NetFront", @"NF-Browser" },
			{"GenericBrowser", @"NokiaBrowser|OviBrowser|OneBrowser|TwonkyBeamBrowser|SEMC.*Browser|FlyFlow|Minimo|NetFront|Novarra-Vision|MQQBrowser|MicroMessenger" },
			{"PaleMoon", @"Android.*PaleMoon|Mobile.*PaleMoon" }
        };
    }
}
