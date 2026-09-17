using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewStuff.MythicAbilities.MythicCaster;

internal static class MythicCasterBuff
{
    // edit
    internal const string buff = "mythiccasterbasebuff";
    internal const string buffguid = Guids.MythicCasterBaseBuff;

    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(MythicCasterFeature.featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(MythicCasterFeature.featurenamekey)
            .SetDescription(MythicCasterFeature.featuredescriptionkey)
            .SetIcon(icon)
            .AddIncreaseCasterLevel(value: ContextValues.Rank(), descriptor:ModifierDescriptor.Mythic)
            .AddContextRankConfig(ContextRankConfigs.MythicLevel().WithMultiplyByModifierProgression(2))
            .AddAbilityUseTrigger(action: new ActionsBuilder().RemoveSelf())
            .Configure();
    }
}