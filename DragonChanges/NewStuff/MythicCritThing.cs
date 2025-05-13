using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.New_Components;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.NewStuff
{
    internal class MythicCritThing
    {
        // edit
        internal const string feature = "GraveSingerPlus";
        internal const string featureguid = Guids.MythicCritThingy;
        internal const string settingName = "gravesingerplus";
        internal const string settingDescription = "Adds the feat GraveSingerPlus";
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
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddComponent(new CritMulti())
                .AddComponent(new CritRange())
                .AddToGroups(FeatureGroup.MythicFeat)
                .AddPrerequisiteFeature(ParametrizedFeatureRefs.ImprovedCritical.Reference.Get())
                .AddPrerequisiteFeature(ParametrizedFeatureRefs.ImprovedCriticalMythicFeat.Reference.Get())
                .Configure();
        }
    }
}
