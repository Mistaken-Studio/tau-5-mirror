﻿// -----------------------------------------------------------------------
// <copyright file="PluginHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
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
        public override Version RequiredExiledVersion => new Version(4, 1, 2);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;

            // new Handler(this);
            new Tau5Soldier().TryRegister();

            // API.Diagnostics.Module.OnEnable(this);
            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            // API.Diagnostics.Module.OnDisable(this);
            base.OnDisabled();
        }

        internal static PluginHandler Instance { get; private set; }
    }
}
