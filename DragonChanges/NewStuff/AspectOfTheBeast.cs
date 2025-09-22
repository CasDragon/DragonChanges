using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using DragonChanges.Utils;
using DragonLibrary.Utils;

namespace DragonChanges.NewStuff
{
    internal class AspectOfTheBeast
    {
        // edit
        internal const string feature = "AspectOfTheBeast";
        internal const string featureguid = Guids.AspectBeast;
        internal const string settingName = "aspectofthebeast";
        internal const string settingDescription = "Enable the Aspect Of the Beast feature";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        //[DragonConfigure]
        //[DragonSetting(settingCategories.NewFeatures, settingName, settingDescription)]
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
            ParametrizedFeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            ParametrizedFeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.Feat)
                .AddPrerequisiteFeature(Guids.CursedBackground)
                .SetParameterType(Kingmaker.Blueprints.Classes.Selection.FeatureParameterType.Custom)
                .Configure();
        }
    }
}
