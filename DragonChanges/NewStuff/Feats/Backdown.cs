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

internal static class Backdown
{
    // edit
    internal const string feature = "Backdown";
    internal const string featureguid = Guids.BackdownFeature;
    internal const string settingName = "backdown";
    internal const string settingDescription = "Adds new feat, Backdown";
    internal const string featurename = "Backdown";
    internal const string featuredescription = "After a successful Power Attack against a melee target, they must make a Will Save DC 10 + 1/2 of your character level + Strength modifier + Intimidate  value. If they fail the saving throw, they are flat-footed for 1 round. Back down is a mind-affecting fear effect.";

    internal const string iconname = "Abilities.Backdown.png";

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
            BackdownBuff.ConfigureDummy();
            BackdownProperty.ConfigureDummy();
            BackdownFlatfootBuff.ConfigureDummy();
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
            .AddPrerequisiteStatValue(stat: StatType.SkillPersuasion, value: 5, group: Prerequisite.GroupType.All)
            .AddPrerequisiteStatValue(stat: StatType.BaseAttackBonus, value: 4, group: Prerequisite.GroupType.All)
            .AddPrerequisiteFeature(FeatureRefs.PowerAttackFeature.ToString(), group: Prerequisite.GroupType.All)
            .AddBuffExtraEffects(BuffRefs.PowerAttackBuff.ToString(), extraEffectBuff:BackdownBuff.ConfigureEnabled(icon))
            .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
            .Configure();
        return x;
    }
}