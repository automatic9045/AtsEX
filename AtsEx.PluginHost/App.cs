﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using UnembeddedResources;

namespace AtsEx.PluginHost
{
    public sealed class App
    {
        private class ResourceSet
        {
            private readonly ResourceLocalizer Localizer = ResourceLocalizer.FromResXOfType<App>("PluginHost");

            [ResourceStringHolder(nameof(Localizer))] public Resource<string> ProductName { get; private set; }
            [ResourceStringHolder(nameof(Localizer))] public Resource<string> ProductShortName { get; private set; }

            public ResourceSet()
            {
                ResourceLoader.LoadAndSetAll(this);
            }
        }

        private static readonly Lazy<ResourceSet> Resources = new Lazy<ResourceSet>();

        public static App Instance { get; private set; }

        public static bool IsInitialized { get; private set; } = false;

        static App()
        {
#if DEBUG
            _ = Resources.Value;
#endif
        }

        private App(Process targetProcess, Assembly bveAssembly, Assembly launcherAssembly, Assembly atsExAssembly)
        {
            Process = targetProcess;
            BveAssembly = bveAssembly;
            AtsExLauncherAssembly = launcherAssembly;
            AtsExAssembly = atsExAssembly;
            AtsExPluginHostAssembly = Assembly.GetExecutingAssembly();
            ExtensionDirectory = Path.Combine(Path.GetDirectoryName(AtsExAssembly.Location), "Extensions");

            AtsExVersion = AtsExAssembly.GetName().Version;
            BveVersion = BveAssembly.GetName().Version;
        }

        public static void CreateInstance(Process targetProcess, Assembly bveAssembly, Assembly launcherAssembly, Assembly atsExAssembly)
        {
            Instance = new App(targetProcess, bveAssembly, launcherAssembly, atsExAssembly);
            IsInitialized = true;
        }

        /// <summary>
        /// プロダクト名を取得します。
        /// </summary>
        public string ProductName { get; } = Resources.Value.ProductName.Value;

        /// <summary>
        /// 短いプロダクト名を取得します。
        /// </summary>
        public string ProductShortName { get; } = Resources.Value.ProductShortName.Value;


        /// <summary>
        /// 制御対象の BVE を実行している <see cref="System.Diagnostics.Process"/> を取得します。
        /// </summary>
        public Process Process { get; }

        /// <summary>
        /// 実行元の BVE の <see cref="Assembly"/> を取得します。
        /// </summary>
        public Assembly BveAssembly { get; }

        /// <summary>
        /// AtsEX Launcher の <see cref="Assembly"/> を取得します。
        /// </summary>
        public Assembly AtsExLauncherAssembly { get; }

        /// <summary>
        /// AtsEX の <see cref="Assembly"/> を取得します。
        /// </summary>
        public Assembly AtsExAssembly { get; }

        /// <summary>
        /// AtsEX プラグインホストの <see cref="Assembly"/> を取得します。
        /// </summary>
        public Assembly AtsExPluginHostAssembly { get; }

        /// <summary>
        /// AtsEX 拡張機能の配置先として使用するディレクトリを取得します。
        /// </summary>
        public string ExtensionDirectory { get; }

        /// <summary>
        /// AtsEX のバージョンを取得します。
        /// </summary>
        public Version AtsExVersion { get; }

        /// <summary>
        /// 実行元の BVE のバージョンを取得します。
        /// </summary>
        public Version BveVersion { get; }

        /// <summary>
        /// 常に <see cref="LaunchMode.InputDevice"/> を返します。
        /// </summary>
        [Obsolete]
        public LaunchMode LaunchMode { get; } = LaunchMode.InputDevice;
    }
}
