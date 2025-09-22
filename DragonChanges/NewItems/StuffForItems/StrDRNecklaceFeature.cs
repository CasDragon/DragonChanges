
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class StrDRNecklaceFeature
    {
        // edit
        internal const string feature = "Str2DRNecklaceFeature";
        internal const string featureguid = Guids.StrToDRNecklaceFeature;
        internal const string settingName = "";
        internal const string settingDescription = "";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static BlueprintFeature ConfigureEnabled()
        {
            BlueprintFeature feat;
            if (ModCompat.tttbase)
            {
                feat = FeatureConfigurator.New(feature, featureguid)
                    .SetDisplayName(featurename)
                    .SetDescription(featuredescription)
                    .AddRecalculateOnStatChange(stat: StatType.Strength)
                    .SetHideInUI(true)
                    .AddTTTAddDamageResistancePhysical(value: ContextValues.Property(UnitProperty.StatBonusStrength))
                    .Configure();
            }
            else
            {
                feat = FeatureConfigurator.New(feature, featureguid)
                    .SetDisplayName(featurename)
                    .SetDescription(featuredescription)
                    .AddRecalculateOnStatChange(stat: StatType.Strength)
                    .SetHideInUI(true)
                    .AddDRComponent(stackable: true, value: ContextValues.Property(UnitProperty.StatBonusStrength), usePool: false)
                    .Configure();
            }
            return feat;
        }
    }
}
