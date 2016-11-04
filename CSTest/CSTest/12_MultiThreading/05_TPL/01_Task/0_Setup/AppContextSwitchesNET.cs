using System;
using System.Runtime.CompilerServices;

namespace CSTest._12_MultiThreading._05_TPL._01_Task._0_Setup
{
    internal static class AppContextSwitchesNET
    {
        private static int _noAsyncCurrentCulture;
        public static bool NoAsyncCurrentCulture
        {
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetCachedSwitchValue(AppContextDefaultValuesNET.SwitchNoAsyncCurrentCulture, ref _noAsyncCurrentCulture);
            }
        }

        private static int _throwExceptionIfDisposedCancellationTokenSource;
        public static bool ThrowExceptionIfDisposedCancellationTokenSource
        {
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetCachedSwitchValue(AppContextDefaultValuesNET.SwitchThrowExceptionIfDisposedCancellationTokenSource, ref _throwExceptionIfDisposedCancellationTokenSource);
            }
        }

        private static int _preserveEventListnerObjectIdentity;
        public static bool PreserveEventListnerObjectIdentity
        {
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return GetCachedSwitchValue(AppContextDefaultValuesNET.SwitchPreserveEventListnerObjectIdentity, ref _preserveEventListnerObjectIdentity);
            }
        }

        //
        // Implementation details
        //

        private static bool DisableCaching { get; set; }

        static AppContextSwitchesNET()
        {
#if CS6
            bool isEnabled;
            if (AppContext.TryGetSwitch(@"TestSwitch.LocalAppContext.DisableCaching", out isEnabled))
            {
                DisableCaching = isEnabled;
            }
#endif
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool GetCachedSwitchValue(string switchName, ref int switchValue)
        {
            if (switchValue < 0) return false;
            if (switchValue > 0) return true;

            return GetCachedSwitchValueInternal(switchName, ref switchValue);
        }

        private static bool GetCachedSwitchValueInternal(string switchName, ref int switchValue)
        {
            bool isSwitchEnabled = false;
#if CS6
            AppContext.TryGetSwitch(switchName, out isSwitchEnabled);
#endif
            if (DisableCaching)
            {
                return isSwitchEnabled;
            }

            switchValue = isSwitchEnabled ? 1 /*true*/ : -1 /*false*/;
            return isSwitchEnabled;
        }
    }

    internal static class AppContextDefaultValuesNET
    {

        internal static readonly string SwitchNoAsyncCurrentCulture = "Switch.System.Globalization.NoAsyncCurrentCulture";
        internal static readonly string SwitchThrowExceptionIfDisposedCancellationTokenSource = "Switch.System.Threading.ThrowExceptionIfDisposedCancellationTokenSource";
        internal static readonly string SwitchPreserveEventListnerObjectIdentity = "Switch.System.Diagnostics.EventSource.PreserveEventListnerObjectIdentity";


        // This is a partial method. Platforms can provide an implementation of it that will set override values
        // from whatever mechanism is available on that platform. If no implementation is provided, the compiler is going to remove the calls
        // to it from the code
        // We are going to have an implementation of this method for the Desktop platform that will read the overrides from app.config, registry and
        // the shim database. Additional implementation can be provided for other platforms.
        static void PopulateOverrideValuesPartial()
        {
        }

        static void PopulateDefaultValuesPartial(string platformIdentifier, string profile, int version)
        {
            // When defining a new switch  you should add it to the last known version.
            // For instance, if you are adding a switch in .NET 4.6 (the release after 4.5.2) you should defined your switch
            // like this:
            //    if (version <= 40502) ...
            // This ensures that all previous versions of that platform (up-to 4.5.2) will get the old behavior by default
            // NOTE: When adding a default value for a switch please make sure that the default value is added to ALL of the existing platforms!
            // NOTE: When adding a new if statement for the version please ensure that ALL previous switches are enabled (ie. don't use else if)
            switch (platformIdentifier)
            {
                case ".NETCore":
                case ".NETFramework":
                    {
                        if (version <= 40502)
                        {
                            //AppContext.DefineSwitchDefault(SwitchNoAsyncCurrentCulture, true);
                            //AppContext.DefineSwitchDefault(SwitchThrowExceptionIfDisposedCancellationTokenSource, true);
                        }
                        break;
                    }
                case "WindowsPhone":
                case "WindowsPhoneApp":
                    {
                        if (version <= 80100)
                        {
                            //AppContext.DefineSwitchDefault(SwitchNoAsyncCurrentCulture, true);
                            //AppContext.DefineSwitchDefault(SwitchThrowExceptionIfDisposedCancellationTokenSource, true);
                        }
                        break;
                    }
            }

            // At this point we should read the overrides if any are defined
            PopulateOverrideValuesPartial();
        }
    }
}
