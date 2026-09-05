using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.NewSpells.Buffs;

public class BloodSlugsBuff
{
    // edit
    internal const string buff = "bloodslugsbuff";
    internal const string buffguid = Guids.BloodSlugsBuff;
    // don't edit
    [DragonLocalizedString(buffname, "Blood Slugs")]
    internal const string buffname = $"{buff}.name";
    [DragonLocalizedString(buffdescription, "The slugs appear on the subject’s body and immediately attempt to penetrate their skin. Targets must make a Fortitude save for each blood slug affecting them. Each failed saving throw deals 1 point of Constitution damage and reduces the target’s speed by 5 feet (minimum 5)")]
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
        var actions = new ActionsBuilder();
        var damage = new ActionsBuilder()
            .ConditionalSaved(
                failed: new ActionsBuilder()
                    .DealDamageToAbility(
                        StatType.Constitution,
                        ContextDice.Value(DiceType.Zero, ContextValues.Constant(0), ContextValues.Constant(1)),
                        false, setFactAsReason: true));
        for (int i = 0; i <= 10; i++)
        {
            actions.SavingThrow(
                SavingThrowType.Fortitude,
                onResult:
                damage);
        }
        var x = BuffConfigurator.New(buff, buffguid)
            .SetDisplayName(buffname)
            .SetDescription(buffdescription)
            .SetRanks(100)
            .AddBuffActions(newRound: actions)
            .SetFlags(BlueprintBuff.Flags.Harmful, BlueprintBuff.Flags.IsFromSpell)
            .SetIcon(icon)
            .Configure();
        return x;
    }
}