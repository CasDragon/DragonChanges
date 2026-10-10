using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewStuff.Feats.Buffs;

internal static class ImprovedCombatExpertiseBuff
{
    // edit
    internal const string buff = "ImprovedCombatExpertiseBuff";
    internal const string buffguid = Guids.ImprovedCombatExpertiseBuff;

    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(ImprovedCombatExpertise.featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(ImprovedCombatExpertise.featurenamekey)
            .SetDescription(ImprovedCombatExpertise.featuredescriptionkey)
            .SetIcon(icon)
            .Configure();
    }
}