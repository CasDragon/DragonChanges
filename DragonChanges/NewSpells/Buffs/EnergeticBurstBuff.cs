using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class EnergeticBurstBuff
{
    // edit
    internal const string buff = "energeticburstbuff";
    internal const string buffguid = Guids.EnergeticBurstBuff;
    
    public static void ConfigureDummy()
    {
        BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(EnergeticBurst.abilityname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static BlueprintBuff ConfigureEnabled(Sprite icon)
    {
        return BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(EnergeticBurst.abilityname)
            .SetDescription(EnergeticBurst.abilitydescription)
            .AddStatBonus(descriptor: ModifierDescriptor.Sacred,
                stat: StatType.SaveFortitude,
                value: 1)
            .AddStatBonus(descriptor: ModifierDescriptor.Sacred,
                stat: StatType.SaveReflex,
                value: 1)
            .AddStatBonus(descriptor: ModifierDescriptor.Sacred,
                stat: StatType.SaveWill,
                value: 1)
            .AddTemporaryHitPointsFromAbilityValue(descriptor: ModifierDescriptor.Sacred,
                value: ContextValues.Constant(10))
            .SetIcon(icon)
            .SetFlags(BlueprintBuff.Flags.IsFromSpell)
            .Configure();
    }
}