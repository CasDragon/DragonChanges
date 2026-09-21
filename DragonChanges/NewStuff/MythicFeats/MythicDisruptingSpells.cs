using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.NewStuff.Feats;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using UnityEngine;

namespace DragonChanges.NewStuff.MythicFeats;

internal static class MythicDisruptingSpells
{
    // edit
    internal const string feature = "mythicdisruptingspells";
    internal const string featureguid = Guids.MythicDisruptingSpells;
    internal const string featurename = "Mythic Disrupting Spells";
    internal const string featuredescription = "The disrupting quality of your magic is increased by your mythic power. Upon hitting a target with a spell, dispel 5 buffs or debuffs from the target, based on your spell DC. Note this will affect both allies and enemies.";

    internal const string iconname = "Abilities.feature.png";

    // don't edit
    [DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";

    [DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";

    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname);

    [DragonConfigure]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(DisruptingSpells.settingName))
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
            .SetGroups(FeatureGroup.MythicFeat)
            .Configure();
        return x;
    }
}