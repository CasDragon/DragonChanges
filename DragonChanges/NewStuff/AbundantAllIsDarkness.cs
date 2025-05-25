using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;

namespace DragonChanges.NewStuff
{
    internal class AbundantAllIsDarkness
    {
        // edit
        internal const string feature = "AbundantAIS";
        internal const string featureguid = Guids.AbundantAIS;
        const string settingName = "aid";
        const string settingDescription = "Adds a new Mythic Ability for Tortured Crusader's All Is Darkness ability";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MythicAbility)
                .AddIncreaseResourceAmountBySharedValue(resource: AbilityResourceRefs.AllIsDarknessResource.Reference.Get(),
                    value: ContextValues.Rank())
                .AddContextRankConfig(ContextRankConfigs.MythicLevel().WithDiv2Progression())
                .AddPrerequisiteFeature(FeatureRefs.TorturedCrusaderAllIsDarknessFeature.Reference.Get())
                .SetIcon(FeatureRefs.AbundantSmiteChaos.Reference.Get().Icon)
                .Configure();
        }
    }
}
