using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class SpiritOfWarBuff
{
    // edit
    internal const string buff = "spiritofwarbuff";
    internal const string buffguid = Guids.SpiritOfWarBuff;
    // don't edit
    [DragonLocalizedString(buffname, "Spirit of War")]
    internal const string buffname = $"{buff}.name";
    [DragonLocalizedString(buffdescription, "You gain the following benefits:\nYour speed increases by 10 feet.\nYou gain a +2 bonus to your AC and Reflex saves")]
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
            .AddStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                stat: StatType.Speed,
                value: 10)
            .AddStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                stat: StatType.AC,
                value: 2)
            .AddStatBonus(descriptor: ModifierDescriptor.UntypedStackable,
                stat: StatType.SaveReflex,
                value: 2)
            .AddContextRankConfig(ContextRankConfigs.CasterLevel())
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}