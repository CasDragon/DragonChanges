using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;

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
                            weaponSubcategory: WeaponSubCategory.Thrown))
                    .AddPrerequisiteFeature(FeatureRefs.PowerAttackFeature.Reference.Get())
                    .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 1)
                    .AddRecommendationStatComparison(higherStat: StatType.Strength, lowerStat: StatType.Dexterity,  diff: 4)
                    .AddRecommendationStatMiminum(minimalValue: 14, stat: StatType.Strength, goodIfHigher: true)
                    .AddToGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .SetIsClassFeature(true)
                    .Configure();
                var component = TTTHelpers.CreateCopy<AddInitiatorAttackWithWeaponTrigger>(BuffRefs.PowerAttackBuff.Reference.Get().GetComponent<AddInitiatorAttackWithWeaponTrigger>());
                component.Category = WeaponCategory.ThrowingAxe;
                BuffConfigurator.For(BuffRefs.PowerAttackBuff)
                    .AddComponent(component)
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
