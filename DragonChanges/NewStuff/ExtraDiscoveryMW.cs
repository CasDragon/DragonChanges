using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;

namespace DragonChanges.NewStuff;

public class ExtraDiscoveryMW
{
    // edit
    internal const string feature = "extra-discovery-mw";
    internal const string featureguid = Guids.ExtraDiscoveryMW;
    internal const string settingName = "extra-discovery-mw";
    internal const string settingDescription = "Adds new feat, Extra Discovery (Mutation Warrior)";
    internal const string featurename = "Extra Discovery (Mutation Warrior)";
    internal const string featuredescription = "You have made a new alchemical discovery. You gain one additional discovery. You must meet the prerequisites for this discovery. This {g|Encyclopedia:Feat}feat{/g} can be taken up to 3 times.";
    // don't edit
    [DragonLocalizedString(featurenamekey, featurename)]
    internal const string featurenamekey = $"{feature}.name";
    [DragonLocalizedString(featuredescriptionkey, featuredescription)]
    internal const string featuredescriptionkey = $"{feature}.description";
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
        FeatureSelectionConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static void ConfigureEnabled()
    {
        var basegame = FeatureSelectionRefs.ExtraDiscoverySelection.Reference.Get();
        var x = FeatureSelectionConfigurator.New(feature, featureguid)
            .SetDisplayName(featurenamekey)
            .SetDescription(featuredescriptionkey)
            .AddPrerequisiteFeature(FeatureSelectionRefs.MutationWarriorDiscoverySelection.ToString())
            .AddPrerequisiteCondition(group: Prerequisite.GroupType.All, 
                condition: new OrAndLogic(){ 
                ConditionsChecker = ConditionsBuilder.New()
                    .BuffRank(fact: featureguid, useFactInsteadBuff: true, negate: true, rankValue: ContextValues.Constant(3))
                    .Build(),
                }, 
                uIText: basegame.GetComponent<PrerequisiteCondition>()!.UIText)
            .SetIcon(basegame.Icon)
            .SetGroup(FeatureGroup.Discovery)
            .AddToGroups(FeatureGroup.Feat)
            .SetRanks(3)
            .SetIsClassFeature(true)
            .SetAllFeatures(
                FeatureRefs.FeralMutagenFeature.ToString(),
                FeatureRefs.FeralWings.ToString(),
                FeatureRefs.GrandMutagenFeature.ToString(),
                FeatureRefs.GreaterMutagenFeature.ToString(),
                FeatureRefs.NauseatingFlesh.ToString(),
                FeatureRefs.PreserveOrgans.ToString(),
                FeatureRefs.SpontaneousHealingFeature.ToString()
            )
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.BasicFeatSelection)
            .AddToAllFeatures(x)
            .Configure();
    }
}