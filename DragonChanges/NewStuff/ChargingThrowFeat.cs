using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewAbilities;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class ChargingThrowFeat
    {
        // edit
        internal const string feature = "ChargingHurler-feat";
        internal const string featureguid = Guids.chargingthrowfeat;
        internal const string settingName = "charginghurler";
        internal const string settingDescription = "Adds the feat Charging Hurler";
        internal const string featurename = "Charging Hurler";
        internal const string featuredescription = "You can use the charge rules to make a thrown weapon attack. All the parameters of a charge apply, except that you must only move closer to your opponent, and you must end your movement within 30 feet of that opponent. If you do, you can make a single thrown weapon attack against that opponent, gaining the +2 bonus on the attack roll and taking a –2 penalty to your AC until the start of your next turn.";
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
                BlueprintAbility x = ChargingThrow.ConfigureEnabled();
                ConfigureEnabled(x);
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ChargingThrow.ConfigureDummy();
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
        public static void ConfigureEnabled(BlueprintAbility ability)
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddPrerequisiteFeature(FeatureRefs.PointBlankShot.ToString())
                .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .AddFacts([ability])
                .Configure();
        }
    }
}
