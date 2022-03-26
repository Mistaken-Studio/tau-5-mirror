// -----------------------------------------------------------------------
// <copyright file="PluginHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace Mistaken.TAU5
{
    /// <inheritdoc/>
    public class PluginHandler : Plugin<Config>
    {
        /// <inheritdoc/>
        public override string Author => "Mistaken Devs";

        /// <inheritdoc/>
        public override string Name => "Tau-5";

        /// <inheritdoc/>
        public override string Prefix => "MTau5";

        /// <inheritdoc/>
        public override PluginPriority Priority => PluginPriority.Default;

        /// <inheritdoc/>
        public override Version RequiredExiledVersion => new Version(5, 0, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;

            // new Handler(this);

            // API.Diagnostics.Module.OnEnable(this);
            Events.Handlers.CustomEvents.LoadedPlugins += this.CustomEvents_LoadedPlugins;
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            // API.Diagnostics.Module.OnDisable(this);
            Events.Handlers.CustomEvents.LoadedPlugins -= this.CustomEvents_LoadedPlugins;
            base.OnDisabled();
        }

        internal static PluginHandler Instance { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether Custom Hierarchy plugin is available.
        /// </summary>
        internal static bool CustomHierarchyAvailable { get; set; } = false;

        private void CustomEvents_LoadedPlugins()
        {
            if (Exiled.Loader.Loader.Plugins.Any(x => x.Name == "CustomHierarchy"))
                CustomHierarchyIntegration.EnableCustomHierarchyIntegration();
        }
    }
}
