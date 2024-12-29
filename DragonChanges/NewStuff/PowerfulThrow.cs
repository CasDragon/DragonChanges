using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;

namespace DragonChanges.NewStuff
{
    internal class PowerfulThrow
    {
        internal static string feature = "powerfulthrow";
        internal static string featurename = "powerfulthrow.name";
        internal static string featuredescription = "powerfulthrow.description";
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("powerfulthrow"))
            {
                Main.log.Log("Creating Powerful Throw feature");
                FeatureConfigurator.New(feature, Guids.PowerfulThrowFeature)
                    .SetDisplayName(featurename)
                    .SetDescription(featuredescription)
                    .AddAttackStatReplacementFixed(new BlueprintCore.Blueprints.Components.Replacements.AttackStatReplacementFixed(
                            replacementStat: StatType.Strength,
                            weaponSubcategory: Kingmaker.Enums.WeaponSubCategory.Thrown))
                    .AddRecommendationStatComparison(higherStat: StatType.Strength, lowerStat: StatType.Dexterity,  diff: 4)
                    .AddRecommendationStatMiminum(minimalValue: 14, stat: StatType.Strength, goodIfHigher: true)
                    .AddToGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .SetIsClassFeature(true)
                    .Configure();
                ActivatableAbilityConfigurator.For(ActivatableAbilityRefs.PowerAttackToggleAbility)
                    .Configure();
                BuffConfigurator.For(BuffRefs.PowerAttackBuff)
                    .Configure();
            }
            else
            {
                Main.log.Log("Powerful Throw is disabled, configuring dummy");
                FeatureConfigurator.New(feature, Guids.PowerfulThrowFeature)
                    .SetDisplayName(featurename)
                    .SetDescription(featuredescription)
                    .Configure();
            }
        }
    }
}
