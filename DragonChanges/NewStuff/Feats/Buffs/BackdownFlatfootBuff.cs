using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewStuff.Feats.Buffs;

internal static class BackdownFlatfootBuff
{
    // edit
    internal const string buff = "BackdownFlatfootBuff";
    internal const string buffguid = Guids.BackdownFlatfootBuff;

    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(Backdown.featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintBuff ConfigureEnabled()
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(Backdown.featurenamekey)
            .SetDescription(Backdown.featuredescriptionkey)
            .SetFlags(BlueprintBuff.Flags.HiddenInUi)
            .AddCondition(UnitCondition.LoseDexterityToAC)
            .AddSpellDescriptorComponent(SpellDescriptor.MindAffecting | SpellDescriptor.Fear)
            .Configure();
    }
}