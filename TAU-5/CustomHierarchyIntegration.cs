// -----------------------------------------------------------------------
// <copyright file="CustomHierarchyIntegration.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;
using Mistaken.API.CustomRoles;
using static Mistaken.CustomHierarchy.HierarchyHandler;

#pragma warning disable SA1118 // Parameter should not span multiple lines

namespace Mistaken.TAU5
{
    internal static class CustomHierarchyIntegration
    {
        internal static void EnableCustomHierarchyIntegration()
        {
            var tau = MistakenCustomRoles.TAU_5.Get();
            Log.Debug("Enabling CustomHierarchy integration.", PluginHandler.Instance.Config.VerbouseOutput);

            CustomPlayerComperers.Add(
                "tau5_comparer",
                (
                    int.MaxValue,
                    (Player p1, Player p2) =>
                    {
                        if (!(p1.Role.Team == Team.MTF && p2.Role.Team == Team.MTF))
                            return CompareResult.NO_ACTION;
                        if (tau.Check(p1) || tau.Check(p2))
                            return CompareResult.SAME_RANK;

                        return CompareResult.NO_ACTION;
                    }));

            Log.Debug("Enabled CustomHierarchy integration.", PluginHandler.Instance.Config.VerbouseOutput);
        }
    }
}
