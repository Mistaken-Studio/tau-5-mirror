// -----------------------------------------------------------------------
// <copyright file="Tau5Shield.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Mistaken.API.Shield;

namespace Mistaken.TAU5
{
    /// <inheritdoc/>
    public class Tau5Shield : Shield
    {
        /// <inheritdoc/>
        protected override int MaxShield => 200;

        /// <inheritdoc/>
        protected override float ShieldRechargeRate => 5;

        /// <inheritdoc/>
        protected override float ShieldEffectivnes => .8f;

        /// <inheritdoc/>
        protected override float TimeUntilShieldRecharge => 30;

        /// <inheritdoc/>
        protected override float ShieldDropRateOnOverflow => 0f;
    }
}
