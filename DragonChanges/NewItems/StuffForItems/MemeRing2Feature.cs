using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.New_Components;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class MemeRing2Feature
    {
        // edit
        internal const string feature = "MemeRing2Feature";
        internal const string featureguid = Guids.MemeRing2Feature;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static BlueprintFeature ConfigureEnabled()
        {
            BlueprintFeature thing = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddComponent(new CritComponent() { stat= StatType.Constitution })
                .AddRecalculateOnStatChange(stat: StatType.Constitution)
                .SetHideInUI(true)
                .Configure();
            return thing;
        }
    }
}
