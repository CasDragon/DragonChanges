using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace DragonChanges.NewStuff
{
    internal class GreaterDCBooster
    {
        // edit
        internal const string feature = "ProfessorOfNethys";
        internal const string featureguid = Guids.ProfessorOfNethys;
        internal const string featurename = "Greater Potent Spellcasting";
        internal const string featuredescription = "Your spells are extremely difficult to resist.\nAdd +4 to the Difficulty Class for all saving throws against spells you cast. This bonus stacks with the bonuses from Potent Spellcasting and Improved Potent Spellcasting.";
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
                    value: ContextValues.Constant(4))
                .AddToGroups(FeatureGroup.Feat)
                .AddRecommendationRequiresSpellbook()
                .AddFeatureTagsComponent(FeatureTag.Magic)
                .AddPrerequisiteFeature(prereq)
                .Configure();
        }
    }
}
