using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonChanges.Content;

public class Unnerfs
{

    [DragonConfigure]
    public static void Jinx()
    {
        Main.log.Log("Unnerfing Jinx");
        var enchant = WeaponEnchantmentRefs.JinxEnchantment.Reference.Get();
        DragonHelpers.RemoveComponent<AdditionalDiceOnAttack>(enchant);
        WeaponEnchantmentConfigurator.For(enchant)
            .AddInitiatorAttackWithWeaponTrigger(onlyHit: true, action:
                ActionsBuilder.New().Conditional(
                    ConditionsBuilder.New().HasBuffImmunityWithDescriptor(spellDescriptor: SpellDescriptor.Hex),
                    ifTrue: ActionsBuilder.New().DealWeaponDamage()))
            .Configure();
    }

    [DragonConfigure]
    public static void WideSweep()
    {
        if (ModCompat.IsModEnabled("EbonsUnfixerMod")) return;
        Main.log.Log("Unnerfing Wide Sweep");
        var actions = ActionsBuilder.New().Conditional(
                ConditionsBuilder.New().IsCaster(true).IsMainTarget(true),
                ifTrue: ActionsBuilder.New().DealWeaponDamage())
            .Build();
        var wideSweep = AbilityRefs.WideSweepAbility.Reference.Get();
        AbilityConfigurator.For(wideSweep)
            .EditComponent<AbilityEffectRunAction>(c => c.Actions = actions)
            .Configure();
    }
}