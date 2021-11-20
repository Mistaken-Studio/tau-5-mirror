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
    public class SelfReviveAbility : PassiveAbility
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "Self Revive";

        /// <inheritdoc/>
        public override string Description { get; set; } = "When player has SCP-500 and dies he will use SCP-500 instantly";

        internal static SelfReviveAbility Register()
        {
            if (instance != null)
                return instance;
            instance = new SelfReviveAbility();
            instance.TryRegister();
            return instance;
        }

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Player.Hurting += this.Player_Hurting;
        }

        /// <inheritdoc/>
        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            Exiled.Events.Handlers.Player.Hurting -= this.Player_Hurting;
        }

        private static SelfReviveAbility instance;

        private void Player_Hurting(Exiled.Events.EventArgs.HurtingEventArgs ev)
        {
            if (!this.Check(ev.Target))
                return;

            if (ev.Target.Health + (ev.Target.ArtificialHealth * ev.Target.ReferenceHub.playerStats.NetworkArtificialNormalRatio) - ev.Amount >= 1)
                return;

            if (ev.HitInformation.Tool == DamageTypes.Wall)
                return;
            if (ev.HitInformation.Tool == DamageTypes.Nuke)
                return;
            if (ev.HitInformation.Tool == DamageTypes.Contain)
                return;
            if (ev.HitInformation.Tool == DamageTypes.Decont)
                return;
            if (ev.HitInformation.Tool == DamageTypes.Flying)
                return;
            if (ev.HitInformation.Tool == DamageTypes.FriendlyFireDetector)
                return;
            if (ev.HitInformation.Tool == DamageTypes.Pocket)
                return;
            if (ev.HitInformation.Tool == DamageTypes.Recontainment)
                return;

            if (ev.Target.HasItem(ItemType.SCP500))
            {
                ev.IsAllowed = false;
                ev.Target.Health = 2;
                ev.Target.ArtificialHealth = 0;
                ev.Target.Hurt(1, ev.DamageType, attackerId: ev.Attacker.Id);
                var item = ev.Target.Items.First(x => x.Type == ItemType.SCP500);
                ev.Target.CurrentItem = item;
                (item.Base as Scp500).ServerOnUsingCompleted();
                ev.Target.SetGUI(nameof(SelfReviveAbility), PseudoGUIPosition.BOTTOM, "<b>Injected <color=yellow>SCP-500</color> to prevent death</b>", 5);
            }
        }
    }
}
