using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace DragonChanges.NewStuff
{
    internal class DCBoosterFeat
    {
        // edit
        internal const string feature = "StudentOfNethys";
        internal const string featureguid = Guids.StudentOfNethys;
        internal const string settingName = "dcboosting";
        internal const string settingDescription = "Feat line that grants increased DC by the caster.";
        internal const string featurename = "Student Of Nethys";
        internal const string featuredescription = "By studying the teachings of Nethys, your spells have an increased difficulty class, with a bonus of 2.";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{feature} feature enabled, configuring");
                BlueprintFeature feat = ConfigureEnabled();
                BlueprintFeature feat2 = ImprovedDCBooster.ConfigureEnabled(feat);
                GreaterDCBooster.ConfigureEnabled(feat2);
                MythicDCBooster.ConfigureEnabled(feat2);
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ConfigureDummy();
                ImprovedDCBooster.ConfigureDummy();
                GreaterDCBooster.ConfigureDummy();
                MythicDCBooster.ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintFeature ConfigureEnabled()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddIncreaseAllSpellsDC(descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable, spellsOnly: true, 
                    value: ContextValues.Constant(2))
                .AddToGroups(FeatureGroup.Feat)
                .AddRecommendationRequiresSpellbook()
                .AddFeatureTagsComponent(FeatureTag.Magic)
                .Configure();
        }
    }
}
