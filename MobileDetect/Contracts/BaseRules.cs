﻿using System;
using System.Collections.Generic;

namespace MobileDetect.Contracts
{
    /// <remarks>This will not detect anything.</remarks>
    public abstract class BaseRules
    {
        public virtual string GetUserAgent(Dictionary<string, string> requestHeaders)
        {
            if (requestHeaders == null)
                return null;

            foreach (var header in requestHeaders)
            {
                if ("User-Agent".Equals(header.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return header.Value;
                }
            }
            return null;
        }

        public virtual bool HasKnownMobileHeaders(Dictionary<string, string> requestHeaders) => false;

        public virtual bool HasKnownTabletHeaders(Dictionary<string, string> requestHeaders) => false;

        public virtual bool HasKnownMobileUserAgent(string userAgent) => false;

        public virtual bool HasKnownTabletUserAgent(string userAgent) => false;
    }
}
