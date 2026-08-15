using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums;

namespace DragonChanges.NewStuff;

public class CastingStatToDamage
{
    // edit
    internal const string feature = "SpellcasterDelights-Damage";
    internal const string featureguid = Guids.CastingStatToDamageFeature;
    internal const string settingName = "spellcasterdelights";
    internal const string featurename = "Spellcaster Delights - Damage";
    internal const string featuredescription = "Your spells become even more powerful. Adds your spellcasting attribute bonus to your spell's damage.";
    // don't edit
    [DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";
    [DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";
    [DragonConfigure]
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
            .Configure();
    }
    public static void ConfigureEnabled()
    {
        FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(featuredescriptionkey)
            .AddStatBonusToSpellDamage(ContextValues.CasterStatBonus())
            .AddToGroups(FeatureGroup.Feat, FeatureGroup.WizardFeat)
            .AddRecommendationRequiresSpellbook()
            .AddFeatureTagsComponent(FeatureTag.Magic)
            .Configure();
    }
}