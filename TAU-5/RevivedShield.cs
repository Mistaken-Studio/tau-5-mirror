// -----------------------------------------------------------------------
// <copyright file="SelfReviveAbility.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using InventorySystem.Items.Usables;
using Mistaken.API.Extensions;
using Mistaken.API.GUI;
using Mistaken.API.Shield;
using UnityEngine;

namespace Mistaken.TAU5
{

    /// <inheritdoc/>
    public class RevivedShield : Shield
    {
        /// <inheritdoc/>
        protected override int MaxShield => 1000;

        /// <inheritdoc/>
        protected override float ShieldRechargeRate => 500;

        /// <inheritdoc/>
        protected override float ShieldEffectivnes => 1;

        /// <inheritdoc/>
        protected override float TimeUntilShieldRecharge => 0f;

        /// <inheritdoc/>
        protected override void Start()
        {
            base.Start();
            this.Invoke(nameof(this.Disable), 15);
        }

        private void Disable()
        {
            Destroy(this);
        }
    }
}
