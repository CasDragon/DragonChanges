using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.FactLogic;

namespace DragonChanges.Content;

public class AnyMeansNecessary
{
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, "anymeansnecessary", "Buff the Hell Knight exclusive Mythic Ability, Any Means Necessary, to not be utter trash")]
    public static void Configure()
    {
        if (!SettingsAction.GetSetting<bool>("anymeansnecessary")) return;
        var feat = FeatureConfigurator.For(FeatureRefs.FreeFromEvil)
            .AddEnergyDamageImmunity(DamageEnergyType.Fire)
            .AdditionalDiceOnDamage(energyType: DamageEnergyType.Fire, 
                diceValue: ContextDice.Value(DiceType.D8, ContextValues.Constant(2)))
            .Configure();
        DragonHelpers.RemoveComponent<AddOutgoingPhysicalDamageProperty>(feat);
    }
}