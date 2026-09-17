using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using UnityEngine;

namespace DragonChanges.NewStuff.MythicAbilities.MythicCaster;

internal static class MythicCasterFeature
{
    // edit
    internal const string feature = "mythiccasterbase";
    internal const string featureguid = Guids.MythicCasterBaseFeature;
    internal const string settingName = "mythiccaster";
    internal const string settingDescription = "Adds the new Mythic Caster mythic ability line";
    internal const string featurename = "Mythic Caster";
    internal const string featuredescription = "Unlock your casting potential with your mythic power! 3 times per day," +
                                " you can infuse your next spell with mythic power! This increases the user's Caster Level" +
                                " by 2x your Mythic Rank.";

    internal const string iconname = "Abilities.MonkeyTrance.png";
    
    internal const string resourcename = "mythiccasterbaseresource";
    internal const string resourceguid = Guids.MythicCasterBaseResource;

    // don't edit
    //[DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";

    //[DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";

    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname);

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
            MythicCasterAbility.ConfigureDummy();
            MythicCasterBuff.ConfigureDummy();
        }
    }

    public static void ConfigureDummy()
    {
        AbilityResourceConfigurator.New(resourcename, resourceguid)
            .Configure();
        FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .SetIcon(icon)
            .Configure();
    }

    public static BlueprintFeature ConfigureEnabled()
    {
        var resource = AbilityResourceConfigurator.New(resourcename, resourceguid)
            .SetMax(3)
            .SetMin(0)
            .SetMaxAmount(new BlueprintAbilityResource.Amount()
            {
                BaseValue = 3,
                IncreasedByLevel = false,
                IncreasedByLevelStartPlusDivStep = false,
                IncreasedByStat = false
            })
            .Configure();
        var x = FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(featuredescriptionkey)
            .SetIcon(icon)
            .SetGroups(FeatureGroup.MythicAbility)
            .AddRecommendationRequiresSpellbook()
            .AddFacts([MythicCasterAbility.ConfigureEnabled(resource)])
            .AddComponent(new AddAbilityResources()
            {
                Amount = 3,
                m_Resource = resource.ToReference<BlueprintAbilityResourceReference>(),
                RestoreAmount = true,
                RestoreOnLevelUp = true
            })
        .Configure();
        return x;
    }
}