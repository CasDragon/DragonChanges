using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class FlashOfInsightBuff
{
    // edit
    internal const string buff = "flashofinsightbuff";
    internal const string buffguid = Guids.FlashOfInsightBuff;
    
    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(FlashOfInsight.abilityname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(FlashOfInsight.abilityname)
            .SetDescription(FlashOfInsight.abilitydescription)
            .AddStatBonusAbilityValue(descriptor: ModifierDescriptor.Insight,
                stat: StatType.AdditionalAttackBonus,
                ContextValues.Rank())
            .AddContextRankConfig(ContextRankConfigs.CasterLevel()
                .WithStartPlusDivStepProgression(3, 1))
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}