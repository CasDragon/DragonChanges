using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.BPCoreExtensions;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.NewStuff;

public class CastingStatToDC
{
    // edit
    internal const string feature = "SpellcasterDelights";
    internal const string featureguid = Guids.CastingStatToDCFeature;
    internal const string settingName = "spellcasterdelights";
    internal const string settingDescription = "Adds new features that add your casting stat to damage/dc";
    internal const string featurename = "Spellcaster Delights - DC";
    internal const string featuredescription = "Your spells become even more hard to resist. Adds your spellcasting attribute bonus to your spell's DC.";
    // don't edit
    [DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";
    [DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";
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
            .Configure();
    }
    public static void ConfigureEnabled()
    {
        FeatureConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(featuredescriptionkey)
            .AddCasterStatToDC(descriptor: ModifierDescriptor.UntypedStackable)
            .AddToGroups(FeatureGroup.Feat, FeatureGroup.WizardFeat)
            .AddRecommendationRequiresSpellbook()
            .AddFeatureTagsComponent(FeatureTag.Magic)
            .Configure();
    }
}