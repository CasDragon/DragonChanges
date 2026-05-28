using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class LimitlessHeavenSword
    {
        // edit
        internal const string feature = "LimitlessSwordOfHeaven";
        internal const string featureguid = Guids.LimitlessSwordOfHeaven;
        internal const string settingName = "limitlessheavensword";
        internal const string settingDescription = "Adds a new Mythic Ability, Limitless Sword of Heaven, which gives Sword of Heaven (Angel mythic ability) unlimited rounds.";
        internal const string featurename = "Limitless Sword of Heaven";
        internal const string featuredescription = "Your Angelic Sword knows neither limits. Benefit: The number of {g|Encyclopedia:Combat_Round}rounds{/g} per day for your Sword of Heaven ability is no longer limited.";
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
            BlueprintFeature x = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddPrerequisiteFeature(FeatureRefs.AngelSwordFeature.ToString())
                .AddToGroups(FeatureGroup.MythicAbility)
                .Configure();
            AbilityConfigurator.For(AbilityRefs.AngelSwordSwitchAbility)
                .EditComponent<AbilityResourceLogic>(c => c.ResourceCostDecreasingFacts = [.. c.ResourceCostDecreasingFacts, x.ToReference<BlueprintUnitFactReference>()])
                .Configure();
        }
    }
}
