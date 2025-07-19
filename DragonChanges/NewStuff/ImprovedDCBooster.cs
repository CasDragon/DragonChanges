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
    internal class ImprovedDCBooster
    {
        // edit
        internal const string feature = "ScholarOfNethys";
        internal const string featureguid = Guids.ScholarOfNethys;
        internal const string featurename = "Scholar Of Nethys";
        internal const string featuredescription = "By studying the teachings of Nethys, your spells have an increased difficulty class, with an additional bonus of 2.";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintFeature ConfigureEnabled(BlueprintFeature prereq)
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddIncreaseAllSpellsDC(descriptor: Kingmaker.Enums.ModifierDescriptor.UntypedStackable, spellsOnly: true,
                    value: ContextValues.Constant(2))
                .AddToGroups(FeatureGroup.Feat)
                .AddRecommendationRequiresSpellbook()
                .AddFeatureTagsComponent(FeatureTag.Magic)
                .AddPrerequisiteFeature(prereq)
                .Configure();
        }
    }
}
