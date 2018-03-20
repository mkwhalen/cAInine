using System;
using CAInine.Apps.Application.Bootstrap;

namespace CAInine.Apps.Platforms.iOS.Bootstrap
{
    /// <summary>
    /// Inversion of control configuration for the iOS application.
    /// Allows for choosing which services, providers, repositories, etc that
    /// this app should use and inject.
    /// </summary>
    public class iOSIoCConfig : IoCConfig
    {
        public iOSIoCConfig()
        {
        }
    }
}
