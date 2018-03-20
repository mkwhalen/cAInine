using System;
using CAInine.Apps.Application.Bootstrap;

namespace CAInine.Apps.Platforms.Droid.Bootstrap
{
    /// <summary>
    /// Inversion of control configuration for the Android application.
    /// Allows for choosing which services, providers, repositories, etc that
    /// this app should use and inject.
    /// </summary>
    public class AndroidIoCConfig : IoCConfig
    {
        public AndroidIoCConfig()
        {
        }
    }
}
