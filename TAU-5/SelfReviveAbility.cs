// -----------------------------------------------------------------------
// <copyright file="SelfReviveAbility.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using InventorySystem.Items.Usables;
using Mistaken.API.Extensions;
using Mistaken.API.GUI;
using PlayerStatsSystem;

namespace Mistaken.TAU5
{
    /// <inheritdoc/>
    [CustomAbility]
    public class SelfReviveAbility : PassiveAbility
    {
        /// <inheritdoc/>
        public override string Name { get; set; } = "Self Revive";

        /// <inheritdoc/>
        public override string Description { get; set; } = "When player has SCP-500 and dies he will use SCP-500 instantly";

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            Exiled.Events.Handlers.Server.WaitingForPlayers += this.Server_WaitingForPlayers;
            Exiled.Events.Handlers.Player.Hurting += this.Player_Hurting;
        }

        /// <inheritdoc/>
        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            Exiled.Events.Handlers.Server.WaitingForPlayers -= this.Server_WaitingForPlayers;
            Exiled.Events.Handlers.Player.Hurting -= this.Player_Hurting;
        }

        private readonly HashSet<Player> revivedPlayers = new HashSet<Player>();

        private void Server_WaitingForPlayers()
        {
            this.revivedPlayers.Clear();
        }

        private void Player_Hurting(Exiled.Events.EventArgs.HurtingEventArgs ev)
        {
            if (this.revivedPlayers.Contains(ev.Target))
                return;

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
                case DamageType.Falldown:
                    return;
            }

            this.revivedPlayers.Add(ev.Target);
            ev.IsAllowed = false;
            ev.Target.Health = 50;
            ev.Target.ArtificialHealth = 0;
            ev.Handler.DealtHealthDamage = 1; // może być źle
            ev.Target.ReferenceHub.playerStats.DealDamage(ev.Handler.Base);
            var item = ev.Target.Items.First(x => x.Type == ItemType.SCP500);
            ev.Target.CurrentItem = item;
            (item.Base as Scp500).ServerOnUsingCompleted();
            ev.Target.SetGUI(nameof(SelfReviveAbility), PseudoGUIPosition.BOTTOM, "<b>Injected <color=yellow>SCP-500</color> to prevent death</b>", 5);
            this.revivedPlayers.Remove(ev.Target);
        }
    }
}
