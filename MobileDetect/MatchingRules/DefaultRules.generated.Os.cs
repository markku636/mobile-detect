using System.Collections.Generic;

namespace MobileDetect.Rules
{
    public partial class DefaultRules
    {
        public static readonly Dictionary<string, string> Os = new Dictionary<string, string> {
            {"AndroidOS", @"Android" },
			{"BlackBerryOS", @"blackberry|\bBB10\b|rim tablet os" },
			{"PalmOS", @"PalmOS|avantgo|blazer|elaine|hiptop|palm|plucker|xiino" },
			{"SymbianOS", @"Symbian|SymbOS|Series60|Series40|SYB-[0-9]+|\bS60\b" },
			{"WindowsMobileOS", @"Windows CE.*(PPC|Smartphone|Mobile|[0-9]{3}x[0-9]{3})|Window Mobile|Windows Phone [0-9.]+|WCE;" },
			{"WindowsPhoneOS", @"Windows Phone 10.0|Windows Phone 8.1|Windows Phone 8.0|Windows Phone OS|XBLWP7|ZuneWP7|Windows NT 6.[23]; ARM;" },
			{"iOS", @"\biPhone.*Mobile|\biPod|\biPad" },
			{"MeeGoOS", @"MeeGo" },
			{"MaemoOS", @"Maemo" },
			{"JavaOS", @"J2ME/|\bMIDP\b|\bCLDC\b" },
			{"webOS", @"webOS|hpwOS" },
			{"badaOS", @"\bBada\b" },
			{"BREWOS", @"BREW" }
        };
    }
}
