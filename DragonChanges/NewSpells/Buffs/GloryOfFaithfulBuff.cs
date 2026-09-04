using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class GloryOfFaithfulBuff
{
    // edit
    internal const string buff = "gloryoffaithfulbuff";
    internal const string buffguid = Guids.GloryOfFaithBuff;
    internal const string settingName = "gloryofaithful";
    // don't edit
    [DragonLocalizedString(buffname, "Glory of the Faithful")]
    internal const string buffname = $"{buff}.name";
    [DragonLocalizedString(buffdescription, "You are infused with the power and glory of all those faithful to your god, both living and dead. You gain a divine bonus to attack, damage, and AC equal to your caster level.")]
    internal const string buffdescription = $"{buff}.description";
    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(buffname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(buffname)
            .SetDescription(buffdescription)
            .AddStatBonusAbilityValue(descriptor: ModifierDescriptor.Sacred,
                stat: StatType.AC,
                ContextValues.Rank())
            .AddStatBonusAbilityValue(descriptor: ModifierDescriptor.Sacred,
                stat: StatType.AdditionalDamage,
                ContextValues.Rank())
            .AddStatBonusAbilityValue(descriptor: ModifierDescriptor.Sacred,
                stat: StatType.AdditionalAttackBonus,
                ContextValues.Rank())
            .AddContextRankConfig(ContextRankConfigs.CasterLevel())
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}