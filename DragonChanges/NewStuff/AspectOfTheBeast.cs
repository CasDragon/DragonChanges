using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using DragonChanges.Utils;

namespace DragonChanges.NewStuff
{
    internal class AspectOfTheBeast
    {
        // edit
        internal static string feature = "AspectOfTheBeast";
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
