using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using UnityEngine;

namespace DragonChanges.NewStuff.MythicAbilities;

internal static class MythicMindCorruption
{
    // edit
    internal const string feature = "mythicmindcorruption";
    internal const string featureguid = Guids.MindCorruption;
    internal const string settingName = "mythicmindcorruption";
    internal const string settingDescription = "New mythic ability Mythic Mind Corruption, which bypasses compulsion immunity";
    internal const string featurename = "Mythic Mind Corruption";
    internal const string featuredescription = "Your mythic power can affect even the strongest of minds, or those without minds! Your spells and abilities bypass Compulsion immunity";

    internal const string iconname = "Abilities.MythicMindCorruption.png";

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
            var feat = ConfigureEnabled();
            //PlaceInFeature(feat);
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
            .AddIgnoreSpellImmunity(spellDescriptor: SpellDescriptor.Compulsion)
            .SetGroups(FeatureGroup.MythicAbility)
            .Configure();
        return x;
    }

    private static void PlaceInFeature(BlueprintFeature feat)
    {
        FeatureConfigurator.For(FeatureRefs.ImmunityToCompulsion)
            .EditComponent<BuffDescriptorImmunity>(c =>
                c.m_IgnoreFeature = feat.ToReference<BlueprintUnitFactReference>())
            .EditComponent<SpellImmunityToSpellDescriptor>(c =>
                c.m_CasterIgnoreImmunityFact = feat.ToReference<BlueprintUnitFactReference>())
            .Configure();
    }
}