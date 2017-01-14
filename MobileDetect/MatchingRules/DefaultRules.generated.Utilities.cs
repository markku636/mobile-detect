using System.Collections.Generic;

namespace MobileDetect.Rules
{
    public partial class DefaultRules
    {
        public static readonly Dictionary<string, string> Utilities = new Dictionary<string, string> {
            {"Bot", @"Googlebot|facebookexternalhit|AdsBot-Google|Google Keyword Suggestion|Facebot|YandexBot|YandexMobileBot|bingbot|ia_archiver|AhrefsBot|Ezooms|GSLFbot|WBSearchBot|Twitterbot|TweetmemeBot|Twikle|PaperLiBot|Wotbox|UnwindFetchor|Exabot|MJ12bot|YandexImages|TurnitinBot|Pingdom" },
			{"MobileBot", @"Googlebot-Mobile|AdsBot-Google-Mobile|YahooSeeker/M1A1-R2D2" },
			{"DesktopMode", @"WPDesktop" },
			{"TV", @"SonyDTV|HbbTV" },
			{"WebKit", @"(webkit)[ /]([\w.]+)" },
			{"Console", @"\b(Nintendo|Nintendo WiiU|Nintendo 3DS|PLAYSTATION|Xbox)\b" },
			{"Watch", @"SM-V700" }
        };
    }
}
