﻿// -----------------------------------------------------------------------
// <copyright file="RevivedShield.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using Mistaken.API.Shield;
using UnityEngine;

namespace Mistaken.TAU5
{
    /// <inheritdoc/>
    public class RevivedShield : Shield
    {
        /// <inheritdoc/>
        protected override int MaxShield => this.maxShield;

        /// <inheritdoc/>
        protected override float ShieldRechargeRate => 500;

        /// <inheritdoc/>
        protected override float ShieldEffectivnes => 1;

        /// <inheritdoc/>
        protected override float TimeUntilShieldRecharge => 0f;

        /// <inheritdoc/>
        protected override float ShieldDropRateOnOverflow => 200f;

        /// <inheritdoc/>
        protected override void Start()
        {
            base.Start();
            this.StartCoroutine(this.Disable());

            foreach (var item in this.gameObject.GetComponents<Shield>())
            {
                if (item != this && item.enabled)
                {
                    item.enabled = false;
                    this.disabledShields.Add(item);
                }
            }
        }

        private readonly List<Shield> disabledShields = new List<Shield>();
        private int maxShield = 1000;

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(15);
            this.maxShield = 0;
            while (this.Player.ArtificialHealth > 0)
                yield return new WaitForSeconds(.1f);

            foreach (var item in this.disabledShields)
                item.enabled = true;
            this.disabledShields.Clear();
            Destroy(this);
        }
    }
}
