using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MobileDetect.Contracts;

namespace MobileDetect.MatchingRules
{
    public partial class DefaultRules: BaseRules
    {
        #region Static stuff

        private static volatile object _instanceLock;
        private static DefaultRules _instance;

        public static readonly RegexOptions ExpressionOptions = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
        public static readonly Dictionary<string, Regex> BrowsersExpressions;
        public static readonly Dictionary<string, Regex> OsExpressions;
        public static readonly Dictionary<string, Regex> PhoneExpressions;
        public static readonly Dictionary<string, Regex> TabletExpressions;

        static DefaultRules()
        {
            _instanceLock = new object();
            BrowsersExpressions = GetExpression(browserRegexStrings, ExpressionOptions);
            OsExpressions = GetExpression(osRegexStrings, ExpressionOptions);
            PhoneExpressions = GetExpression(phonesRegexStrings, ExpressionOptions);
            TabletExpressions = GetExpression(tabletRegexStrings, ExpressionOptions);
        }

        public static DefaultRules Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DefaultRules(userAgentHeaders, mobileHeaders, PhoneExpressions, TabletExpressions, OsExpressions, BrowsersExpressions);
                        }
                    }
                }
                return _instance;
            }
        }

        private static Dictionary<string, Regex> GetExpression(Dictionary<string, string> stringExpressions, RegexOptions options)
        {
            return stringExpressions.ToDictionary(kvp => kvp.Key, kvp => new Regex(kvp.Value, options));
        }
        
        #endregion Static stuff

        #region BaseRules Implementation

        private readonly string[] _userAgentHeaders;
        private readonly Dictionary<string, string[]> _knownMobileHeaders;
        private readonly Dictionary<string, Regex> _phoneRules;
        private readonly Dictionary<string, Regex> _tabletsRules;
        private readonly Dictionary<string, Regex> _osRules;
        private readonly Dictionary<string, Regex> _browserRules;

        public DefaultRules(
            string[] userAgentHeaders,
            Dictionary<string, string[]> knownMobileHeaders,
            Dictionary<string, Regex> phonesRules,
            Dictionary<string, Regex> tabletRules,
            Dictionary<string, Regex> osRules,
            Dictionary<string, Regex> browserRules)
            : base()
        {
            if (userAgentHeaders == null)
                throw new ArgumentNullException(nameof(userAgentHeaders));

            if (knownMobileHeaders == null)
                throw new ArgumentNullException(nameof(knownMobileHeaders));

            if (phonesRules == null)
                throw new ArgumentNullException(nameof(phonesRules));

            if (tabletRules == null)
                throw new ArgumentNullException(nameof(tabletRules));

            if (osRules == null)
                throw new ArgumentNullException(nameof(osRules));

            if (browserRules == null)
                throw new ArgumentNullException(nameof(browserRules));


            _userAgentHeaders = userAgentHeaders;
            _knownMobileHeaders = knownMobileHeaders;

            _phoneRules = phonesRules;
            _tabletsRules = tabletRules;
            _osRules = osRules;
            _browserRules = browserRules;
        }

        public override string GetUserAgent(Dictionary<string, string> requestHeaders)
        {
            if (requestHeaders == null)
                return null;

            var sb = new StringBuilder();
            foreach (var userAgentHeader in _userAgentHeaders)
            {
                var key = GetDictionaryKeyOrdinalIgnoreCase(requestHeaders?.Keys, userAgentHeader);
                if (key == null)
                    continue;

                var headerValue = requestHeaders[key];
                if (String.IsNullOrEmpty(headerValue))
                    continue;

                sb.Append(headerValue);
                sb.Append(" ");
            }

            var userAgent = sb.ToString().Trim();
            return String.IsNullOrEmpty(userAgent)
                ? null
                : userAgent;
        }

        public override bool HasKnownMobileHeaders(Dictionary<string, string> requestHeaders)
        {
            if (IsCustomHeaderTrue(requestHeaders, "CloudFront-Is-Mobile-Viewer"))
                return true;

            if (_knownMobileHeaders == null)
                return false; //no headers means not mobile

            foreach (var mobileHeader in _knownMobileHeaders)
            {
                var key = GetDictionaryKeyOrdinalIgnoreCase(requestHeaders?.Keys, mobileHeader.Key);
                if (key == null)
                    continue;
                
                if (mobileHeader.Value == null || mobileHeader.Value.Length == 0)
                    return true; // the current header does not require a specific value, it's existence means it's mobile

                var headerValue = requestHeaders[key];
                if (headerValue == null)
                    return false;

                foreach (var mobileHeaderValue in mobileHeader.Value)
                {
                    if (headerValue.IndexOf(mobileHeaderValue, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        return true;
                    }
                }
            }

            return base.HasKnownMobileHeaders(requestHeaders);
        }

        public override bool HasKnownTabletHeaders(Dictionary<string, string> requestHeaders)
        {
            if (IsCustomHeaderTrue(requestHeaders, "CloudFront-Is-Tablet-Viewer"))
                return true;

            return base.HasKnownTabletHeaders(requestHeaders);
        }

        public override bool HasKnownMobileUserAgent(string userAgent)
        {
            if (userAgent == null)
                return false;

            if (AnyMatch(_phoneRules, userAgent))
                return true;

            if (AnyMatch(_tabletsRules, userAgent))
                return true;

            if (AnyMatch(_osRules, userAgent))
                return true;

            if (AnyMatch(_browserRules, userAgent))
                return true;

            return base.HasKnownMobileUserAgent(userAgent);
        }

        public override bool HasKnownTabletUserAgent(string userAgent)
        {
            if (userAgent == null)
                return false;

            if (AnyMatch(_tabletsRules, userAgent))
                return true;

            return base.HasKnownTabletUserAgent(userAgent);
        }

        private static bool IsCustomHeaderTrue(Dictionary<string, string> requestHeaders, string headerName)
        {
            if (headerName == null)
                return false;

            var key = GetDictionaryKeyOrdinalIgnoreCase(requestHeaders?.Keys, headerName);
            if (key == null)
                return false;

            return Boolean.TrueString.Equals(requestHeaders[key], StringComparison.OrdinalIgnoreCase);
        }

        private static bool AnyMatch(Dictionary<string, Regex> rules, string testString)
        {
            if (rules != null)
            {
                foreach (var rule in rules.Where(w => w.Value != null))
                {
                    if (rule.Value.IsMatch(testString))
                        return true;
                }
            }
            return false;
        }

        private static string GetDictionaryKeyOrdinalIgnoreCase(ICollection<string> dictionaryKeys, string key)
        {
            if (key == null)
                return null;

            return dictionaryKeys?.FirstOrDefault(f => key.Equals(f, StringComparison.OrdinalIgnoreCase));
        }

        #endregion BaseRules Implementation
    }
}
