using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using UnityEngine;

namespace DragonChanges.NewStuff.Feats;

internal static class MuscleReaction
{
    // edit
    internal const string feature = "MuscleReaction";
    internal const string featureguid = Guids.MuscleReaction;
    internal const string settingName = "musclereaction";
    internal const string settingDescription = "New feat, Battlefield Intuition. STR to Init, 3rd party feat";
    internal const string featurename = "Muscle Reaction";
    internal const string featuredescription = "You add your Strength modifier to initiative checks. This is in addition to other modifiers to initiative checks, like the bonus provided by a high Dexterity or the Improved Initiative feat.";

    internal const string iconname = "Abilities.feature.png";

    // don't edit
    [DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";

    [DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";

    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname);

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
        FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .SetIcon(icon)
            .Configure();
    }

    public static BlueprintFeature ConfigureEnabled()
    {
        var x = FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(featuredescriptionkey)
            .SetIcon(icon)
            .AddDerivativeStatBonus(baseStat: StatType.Strength,
                descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable,
                derivativeStat: StatType.Initiative)
            .AddRecalculateOnStatChange(stat: StatType.Strength)
            .AddToGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            .Configure();
        return x;
    }
}