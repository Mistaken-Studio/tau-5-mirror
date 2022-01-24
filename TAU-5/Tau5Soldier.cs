// -----------------------------------------------------------------------
// <copyright file="Tau5Soldier.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using InventorySystem.Items.Armor;
using Mistaken.API;
using Mistaken.API.CustomRoles;
using Mistaken.API.Extensions;
using Mistaken.API.GUI;
using Mistaken.RoundLogger;
using Respawning;

namespace Mistaken.TAU5
{
    /// <inheritdoc/>
    public class Tau5Soldier : MistakenCustomRole
    {
        /// <inheritdoc/>
        public override MistakenCustomRoles CustomRole => MistakenCustomRoles.TAU_5;

        /// <inheritdoc/>
        public override RoleType Role { get; set; } = RoleType.NtfCaptain;

        /// <inheritdoc/>
        public override int MaxHealth { get; set; } = 500;

        /// <inheritdoc/>
        public override string Name { get; set; } = "Żołnierz Tau-5 Samsara";

        /// <inheritdoc/>
        public override string Description { get; set; } = "Twoje zadanie: <color=red>Zneutralizować wszystko poza personelem fundacji</color><br><b>Karta O5 jest wbudowana w twoją rękę</b>, więc <color=yellow>możesz otwierać <b>wszystkie</b> drzwi nie używając karty</color>";

        /// <inheritdoc/>
        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>
        {
            SelfReviveAbility.Register(),
        };

        /// <inheritdoc/>
        protected override KeycardPermissions BuiltInPermissions =>
            KeycardPermissions.ExitGates |
            KeycardPermissions.AlphaWarhead |
            KeycardPermissions.Intercom |
            KeycardPermissions.Checkpoints |
            KeycardPermissions.ArmoryLevelOne |
            KeycardPermissions.ArmoryLevelTwo |
            KeycardPermissions.ArmoryLevelThree |
            KeycardPermissions.ContainmentLevelOne |
            KeycardPermissions.ContainmentLevelTwo |
            KeycardPermissions.ContainmentLevelThree;

        /// <inheritdoc/>
        protected override bool KeepInventoryOnSpawn { get; set; } = false;

        /// <inheritdoc/>
        protected override bool KeepRoleOnDeath { get; set; } = false;

        /// <inheritdoc/>
        protected override bool RemovalKillsPlayer { get; set; } = false;

        /// <inheritdoc/>
        protected override List<string> Inventory { get; set; } = new List<string>
        {
            ItemType.GunE11SR.ToString(),
            ItemType.GunShotgun.ToString(),
            ItemType.ArmorHeavy.ToString(),
            ItemType.SCP500.ToString(),
            ItemType.Radio.ToString(),
            ItemType.Medkit.ToString(),
            ItemType.Medkit.ToString(),
            ItemType.GrenadeHE.ToString(),
        };

        /// <inheritdoc/>
        protected override bool SetLatestUnitName => true;

        /// <inheritdoc/>
        protected override bool InfiniteAmmo => true;

        /// <inheritdoc/>
        protected override Dictionary<ItemType, ushort> Ammo => new Dictionary<ItemType, ushort>
        {
            { ItemType.Ammo556x45, 1 },
            { ItemType.Ammo9x19, 1 },
            { ItemType.Ammo12gauge, 1 },
            { ItemType.Ammo44cal, 1 },
            { ItemType.Ammo762x39, 1 },
        };

        /// <inheritdoc/>
        protected override string DisplayName => "<color=#C00>Żołnierz Tau-5 Samsara</color>";

        /// <inheritdoc/>
        protected override void RoleAdded(Player player)
        {
            base.RoleAdded(player);
            player.ArtificialHealth = 200;
            player.EnableEffect<Invigorated>();
            player.ChangeEffectIntensity<MovementBoost>(50);
            Tau5Shield.Ini<Tau5Shield>(player);
        }
    }
}
