using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;

namespace DragonChanges.templates
{
    internal class FeatureTemplate
    {
        // edit
        internal static string feature = "feature";
        internal static string featureguid = Guids.AspectBeast;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
        //[DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(feature.ToLower()))
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
                .Configure();
        }
    }
}