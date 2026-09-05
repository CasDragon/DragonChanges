using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class WarriorReflexesBuff
{
    // edit
    internal const string buff = "warriorreflexsbuff";
    internal const string buffguid = Guids.WarriorReflexesBuff;
    // don't edit
    [DragonLocalizedString(buffname, "Warrior’s Reflexes")]
    internal const string buffname = $"{buff}.name";
    [DragonLocalizedString(buffdescription, "Your allies become aware of what is about to happen in combat. This precognitive awareness grants each creature a +1 insight bonus to AC and Reflex saves.")]
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
            .AddStatBonus(descriptor: ModifierDescriptor.Insight,
                stat: StatType.AC,
                value: 1)
            .AddStatBonus(descriptor: ModifierDescriptor.Insight,
                stat: StatType.SaveReflex,
                value: 1)
            .AddContextRankConfig(ContextRankConfigs.CasterLevel())
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}