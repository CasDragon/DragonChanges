using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.templates
{
    internal class FeatureTemplate
    {
        // edit
        internal const string feature = "";
        internal const string featureguid = Guids.Template;
        internal const string settingName = "";
        internal const string settingDescription = "";
        internal const string featurename = "";
        internal const string featuredescription = "";
        // don't edit
        //[DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        //[DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        //[DragonConfigure]
        //[DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
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
        public static BlueprintFeature ConfigureEnabled()
        {
            var x = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .Configure();
            return x;
        }
    }
}