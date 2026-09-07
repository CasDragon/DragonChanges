using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class DragonScalesBuff
{
    // edit
    internal const string buff = "dragonscalesbuff";
    internal const string buffguid = Guids.DragonScalesBuff;
    
    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(DragonScales.abilityname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(DragonScales.abilityname)
            .SetDescription(DragonScales.abilitydescription)
            .AddStatBonus(descriptor: ModifierDescriptor.NaturalArmorEnhancement,
                stat: StatType.AC,
                value: 3)
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}