using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.NewItems.StuffForItems;

internal static class LevelToABRingFeature
{
    // edit
    private const string feature = "LevelToABRingFeature";
    private const string featureguid = Guids.LevelToABRingFeature;

    public static void ConfigureDummy()
    {
        FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(LevelToABRing.itemname)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }

    public static BlueprintFeature ConfigureEnabled()
    {
        var x = FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(LevelToABRing.itemname)
            .SetDescription(LevelToABRing.itemdescription)
            .AddContextStatBonus(StatType.AdditionalAttackBonus,
                ContextValues.Property(UnitProperty.Level, true),
                ModifierDescriptor.Morale)   
            .Configure();
        return x;
    }
}