using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;

namespace DragonChanges.NewStuff
{
    internal class PowerfulWings
    {
        // edit
        internal const string feature = "PowerfulWings";
        internal const string featureguid = Guids.PowerfulWings;
        internal const string settingName = "powerfulwings";
        internal const string settingDescription = "Adds the feat Powerful Wings";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
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
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            var c = new PrerequisiteParametrizedFeature();
            c.m_Feature = ParametrizedFeatureRefs.WeaponFocus.Reference.Get().ToReference<BlueprintFeatureReference>();
            c.ParameterType = Kingmaker.Blueprints.Classes.Selection.FeatureParameterType.WeaponCategory;
            c.WeaponCategory = Kingmaker.Enums.WeaponCategory.Wing;
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.Feat)
                .AddPrerequisiteFullStatValue(stat: StatType.BaseAttackBonus,
                                              value: 8)
                .AddPrerequisiteFullStatValue(stat: StatType.Strength,
                                              value: 13)
                .AddComponent(c)
                .AddAdditionalLimb(ItemWeaponRefs.WingColossal2d8.Reference.Get())
                .AddAdditionalLimb(ItemWeaponRefs.WingColossal2d8.Reference.Get())
                .Configure();
        }
    }
}
