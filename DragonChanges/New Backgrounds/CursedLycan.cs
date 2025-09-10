using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.New_Backgrounds
{
    internal class CursedLycan
    {
        // edit
        internal const string feature = "Wanderer";
        internal const string featureguid = Guids.CursedBackground;
        internal const string settingName = "wanderer";
        internal const string settingDescription = "Enable the Lycanthropy background";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewBackgrounds, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"Cursed background feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"Cursed background disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            BlueprintFeature x = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddToFeatureSelection(FeatureSelectionRefs.BackgroundsWandererSelection.Reference.Get())
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillPerception)
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillMobility)
                .Configure();

            if (ModCompat.homebrewarchetypes)
            {
                FeatureSelectionConfigurator.For("2d153d12aab2d1d47b8d1015f83f5894")
                    .AddPrerequisiteFeature(x)
                    .Configure();
            }
        }
    }
}
