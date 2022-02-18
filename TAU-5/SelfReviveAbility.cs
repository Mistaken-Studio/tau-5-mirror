// -----------------------------------------------------------------------
// <copyright file="SelfReviveAbility.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using Exiled.API.Enums;
using Exiled.CustomRoles.API.Features;
using InventorySystem.Items.Usables;
using Mistaken.API.Extensions;
using Mistaken.API.GUI;
using PlayerStatsSystem;

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

            if (!ev.Target.HasItem(ItemType.SCP500))
                return;

            if (!ev.Target.WillDie(ev.Handler.Base))
                return;

            switch (ev.Handler.Type)
            {
                case DamageType.Crushed:
                case DamageType.Warhead:
                case DamageType.Recontainment:
                case DamageType.Decontamination:
                case DamageType.FriendlyFireDetector:
                case DamageType.PocketDimension:
                    return;
            }

            ev.IsAllowed = false;
            ev.Target.Health = 50;
            ev.Target.ArtificialHealth = 0;
            ev.Handler.Amount = 1;
            ev.Target.ReferenceHub.playerStats.DealDamage(ev.Handler.Base);
            var item = ev.Target.Items.First(x => x.Type == ItemType.SCP500);
            ev.Target.CurrentItem = item;
            (item.Base as Scp500).ServerOnUsingCompleted();
            ev.Target.SetGUI(nameof(SelfReviveAbility), PseudoGUIPosition.BOTTOM, "<b>Injected <color=yellow>SCP-500</color> to prevent death</b>", 5);
        }
    }
}
