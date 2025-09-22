using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using DragonLibrary.Utils;

namespace DragonChanges.NewStuff
{
    internal class MyThicFeat
    {
        // edit
        internal const string feature = "MyThicFeat";
        internal const string featureguid = Guids.MyThiccFeat;
        internal const string settingName = "kabmemefeature";
        internal const string settingDescription = "Adds a feature called 'My Thic Feat', a meme for Kab on Discord.";
        internal const string featurename = "My Thic Feat";
        internal const string featuredescription = "You are so THICCCCC that your CON score raises by 2!";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription, false)]
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
                .AddStatBonus(Kingmaker.Enums.ModifierDescriptor.Anomaly,
                    stat: Kingmaker.EntitySystem.Stats.StatType.Constitution,
                    value: 2)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.Feat)
                .Configure();
        }
    }
}
