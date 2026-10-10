using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff.Feats.Buffs;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;
using UnityEngine;

namespace DragonChanges.NewStuff.Feats;

internal static class ImprovedCombatExpertise
{
    // edit
    internal const string feature = "ImprovedCombatExpertise";
    internal const string featureguid = Guids.ImprovedCombatExpertiseFeature;
    internal const string settingName = "improvedcombatexpertise";
    internal const string settingDescription = "Adds new feat, Improved Combat Expertise";
    internal const string featurename = "Improved Combat Expertise";
    internal const string featuredescription = "When you use Combat Expertise, reduce the number you subtract from melee attack rolls and combat maneuvers by 2.";

    internal const string iconname = "Abilities.ImprovedCombatExpertise.png";

    // don't edit
    //[DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";

    //[DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";

    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname);

    //[DragonConfigure]
    //[DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
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
            ImprovedCombatExpertiseBuff.ConfigureDummy();
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
            .AddPrerequisiteStatValue(stat: StatType.Intelligence, value: 13, group: Prerequisite.GroupType.All)
            .AddPrerequisiteStatValue(stat: StatType.BaseAttackBonus, value: 8, group: Prerequisite.GroupType.All)
            .AddPrerequisiteFeature(FeatureRefs.CombatExpertiseFeature.ToString(), group: Prerequisite.GroupType.All)
            .AddBuffExtraEffects(BuffRefs.PowerAttackBuff.ToString(), extraEffectBuff: ImprovedCombatExpertiseBuff.ConfigureEnabled(icon))
            .Configure();
        return x;
    }
}