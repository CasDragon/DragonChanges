using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public static class BluntImpactBuff
{
    internal const string buff = "bluntimpactbuff";
    internal const string buffguid = Guids.BluntImpactBuff;
    
    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(BluntImpact.abilityname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(BluntImpact.abilityname)
            .SetDescription(BluntImpact.abilitydescription)
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}