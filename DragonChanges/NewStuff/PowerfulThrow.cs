using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace DragonChanges.NewStuff
{
    internal class PowerfulThrow
    {
        // edit
        internal const string feature = "PowerfulThrow";
        internal const string featureguid = Guids.PowerfulThrowFeature;
        internal const string settingName = "powerfulthrow";
        internal const string settingDescription = "Adds the feat Powerful Throw\"";
        internal const string featurename = "Powerful Throw";
        internal const string featuredescription = "You may use your {g}Strength{/g} modifier in place of your {g}Dexterity{/g} modifier for your attack rolls when making a ranged attack with thrown weapons. You may also use the Power Attack feat instead of the Deadly Aim feat when attacking with thrown weapons.";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription, true)]
        internal const string featuredescriptionkey = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{feature} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(feature, featureguid)
                .AddWorkingAttackStatReplacementForWeaponGroup(ReplacementStat: StatType.Strength, FighterGroupFlag: WeaponFighterGroupFlags.Thrown)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddPrerequisiteFeature(FeatureRefs.PowerAttackFeature.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, 1)
                .AddRecommendationStatComparison(higherStat: StatType.Strength, lowerStat: StatType.Dexterity, diff: 4)
                .AddRecommendationStatMiminum(minimalValue: 14, stat: StatType.Strength, goodIfHigher: true)
                .AddToGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .SetIsClassFeature(true)
                .Configure();
            var component = TTTHelpers.CreateCopy<AddInitiatorAttackWithWeaponTrigger>(BuffRefs.PowerAttackBuff.Reference.Get().GetComponent<AddInitiatorAttackWithWeaponTrigger>());
            component.Group = WeaponFighterGroup.Thrown;
            component.CheckWeaponGroup = true;
            component.CheckWeaponRangeType = false;
            BuffConfigurator.For(BuffRefs.PowerAttackBuff)
                .AddComponent(component)
                .Configure();
            var bcomponent = TTTHelpers.CreateCopy<WeaponParametersAttackBonus>(BuffRefs.PowerAttackBuffEffect.Reference.Get().GetComponent<WeaponParametersAttackBonus>());
            bcomponent.Ranged = true;
            var acomponent = TTTHelpers.CreateCopy<WeaponParametersDamageBonus>(BuffRefs.PowerAttackBuffEffect.Reference.Get().GetComponent<WeaponParametersDamageBonus>());
            acomponent.Ranged = true;
            BuffConfigurator.For(BuffRefs.PowerAttackBuffEffect)
                .AddComponent(bcomponent)
                .AddComponent(acomponent)
                .Configure();
        }
    }
}
