using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class PresenceOfMind
    {        // edit
        internal const string feature = "PresenceofMind";
        internal const string featureguid = Guids.presenceofmind;
        internal const string settingName = "presenceofmind";
        internal const string settingDescription = "Adds the 3rd party feat, Presence of Mind";
        internal const string featurename = "Presence of Mind";
        internal const string featuredescription = "You add your Intelligence modifier to initiative checks. This is in addition to other modifiers to initiative checks, like the bonus provided by a high Dexterity or the Improved Initiative feat.";
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
                .AddDerivativeStatBonus(baseStat: StatType.Initiative,
                    descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable,
                    derivativeStat: StatType.Intelligence)
                .AddRecalculateOnStatChange(stat: StatType.Intelligence)
                .AddToGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .Configure();
        }
    }
}
