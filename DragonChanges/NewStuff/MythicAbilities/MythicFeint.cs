using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using UnityEngine;

namespace DragonChanges.NewStuff.MythicAbilities;

internal static class MythicFeint
{
    // edit
    internal const string feature = "mythicfeintaoe";
    internal const string featureguid = Guids.FeintAoe;
    internal const string settingName = "mythicfeintaoe";
    internal const string settingDescription = "Adds a new Mythic Ability, Mythic Feint, which is an AoE Feint";
    internal const string featurename = "Mythic Feint";
    internal const string featuredescription = "Using your mythic powers, you can increase the number of people you can feint at one time!";

    internal const string iconname = "Abilities.MythicFeint.png";

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
            MythicFeintAbility.ConfigureDummy();
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
            .SetGroups(FeatureGroup.MythicAbility)
            .AddPrerequisiteFeaturesFromList(amount: 1,
                features: [FeatureRefs.Feint.Reference.Get().ToReference<BlueprintFeatureReference>(),
                    FeatureRefs.DivaAdvance.Reference.Get().ToReference<BlueprintFeatureReference>()])
            .AddFacts([MythicFeintAbility.ConfigureEnabled()])
            .Configure();
        return x;
    }
}