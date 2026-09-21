using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using UnityEngine;

namespace DragonChanges.NewStuff.Feats;

internal static class DisruptingSpells
{
    // edit
    internal const string feature = "disruptingspells";
    internal const string featureguid = Guids.DisruptingSpells;
    internal const string settingName = "disruptingspells";
    internal const string settingDescription = "Adds new feature, Disrupting Spells, which causes spells to dispel 1 buff/debuff from an enemy on hit.";
    internal const string featurename = "Disrupting Spells";
    internal const string featuredescription = "Your magic takes on a quality which distrupts other's magic. Upon hitting a target with a spell, dispel 1 buff or debuff from the target, based on your spell DC. Note this will affect both allies and enemies.";

    internal const string iconname = "Abilities.feature.png";

    // don't edit
    [DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";

    [DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";

    internal static readonly Sprite icon = MicroAssetUtil.GetAssemblyResourceSprite(iconname);

    [DragonConfigure]
    [DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
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
            .AddAbilityUseTrigger(checkAbilityType: true, type: AbilityType.Spell,
                actionsOnAllTargets: true, actionsOnTarget: true,
                action: ActionsBuilder.New().Conditional(
                        ConditionsBuilder.New().IsCaster(true),
                        ActionsBuilder.New().Conditional(ConditionsBuilder.New().CasterHasFact(Guids.MythicDisruptingSpells),
                            ActionsBuilder.New().DispelMagic(ContextActionDispelMagic.BuffType.All, RuleDispelMagic.CheckType.DC, 
                            ContextValues.Constant(10), countToRemove: ContextValues.Constant(5)),
                            ActionsBuilder.New().DispelMagic(ContextActionDispelMagic.BuffType.All, RuleDispelMagic.CheckType.DC, 
                                ContextValues.Constant(10), countToRemove: ContextValues.Constant(1)))))
            .SetGroups(FeatureGroup.WizardFeat, FeatureGroup.Feat)
            .SetIcon(icon)
            .Configure();
        return x;
    }
}