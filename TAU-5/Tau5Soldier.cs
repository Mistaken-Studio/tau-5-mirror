// -----------------------------------------------------------------------
// <copyright file="Tau5Soldier.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
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
        public override KeycardPermissions BuiltInPermissions =>
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
        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>
        {
            SelfReviveAbility.Register(),
        };

        internal string Color => "#C00";

        /// <inheritdoc/>
        protected override bool KeepInventoryOnSpawn { get; set; } = false;

        /// <inheritdoc/>
        protected override bool KeepRoleOnDeath { get; set; } = false;

        /// <inheritdoc/>
        protected override bool RemovalKillsPlayer { get; set; } = true;

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
        public override bool SetLatestUnitName => true;

        /// <inheritdoc/>
        public override Dictionary<ItemType, ushort> Ammo => new Dictionary<ItemType, ushort>
        {
            { ItemType.Ammo556x45, 500 },
            { ItemType.Ammo9x19, 500 },
            { ItemType.Ammo12gauge, 100 },
        };

        /// <inheritdoc/>
        public override string DisplayName => "<color=#C00>Żołnierz Tau-5 Samsara</color>";

        /// <inheritdoc/>
        protected override void RoleAdded(Player player)
        {
            base.RoleAdded(player);
            player.ArtificialHealth = 200;
            Tau5Shield.Ini<Tau5Shield>(player);
            player.UnitName = RespawnManager.Singleton.NamingManager.AllUnitNames.Last().UnitName;

            MEC.Timing.CallDelayed(1.6f, () =>
            {
                var armor = player.Items.First(x => x.Type == ItemType.ArmorHeavy).Base as BodyArmor;
                armor.AmmoLimits = new BodyArmor.ArmorAmmoLimit[]
                {
                    new BodyArmor.ArmorAmmoLimit
                    {
                        AmmoType = ItemType.Ammo12gauge,
                        Limit = 100,
                    },
                    new BodyArmor.ArmorAmmoLimit
                    {
                        AmmoType = ItemType.Ammo44cal,
                        Limit = 100,
                    },
                    new BodyArmor.ArmorAmmoLimit
                    {
                        AmmoType = ItemType.Ammo556x45,
                        Limit = 500,
                    },
                    new BodyArmor.ArmorAmmoLimit
                    {
                        AmmoType = ItemType.Ammo762x39,
                        Limit = 500,
                    },
                    new BodyArmor.ArmorAmmoLimit
                    {
                        AmmoType = ItemType.Ammo9x19,
                        Limit = 500,
                    },
                };
            });
        }
    }
}
