using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.BPCoreExtensions;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UI.SettingsUI;
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
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            AttackStatReplacementForWeaponGroup strcomponet = new AttackStatReplacementForWeaponGroup();
            strcomponet.FighterGroupFlag = WeaponFighterGroupFlags.Thrown;
            strcomponet.ReplacementStat = StatType.Strength;
            FeatureConfigurator.New(feature, featureguid)
                .AddWorkingAttackStatReplacementForWeaponGroup(ReplacementStat: StatType.Strength, FighterGroupFlag: WeaponFighterGroupFlags.Thrown)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                //.AddComponent(strcomponet)
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
