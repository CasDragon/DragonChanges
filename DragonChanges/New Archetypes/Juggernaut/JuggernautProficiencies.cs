using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;

namespace DragonChanges.New_Archetypes.Juggernaut
{
    internal class JuggernautProficiencies
    {
        // edit
        internal const string feature = "JuggernautProficiencies";
        internal const string featureguid = Guids.JuggernautProficiencies;
        internal const string featurename = "Juggernaut Proficiencies";
        internal const string featuredescription = "A Juggernaut is proficient with simple and martial weapons, and shields.";
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
        public static Kingmaker.Blueprints.Classes.BlueprintFeature ConfigureEnabled()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddFacts([FeatureRefs.SimpleWeaponProficiency.Reference.Get(),
                    FeatureRefs.MartialWeaponProficiency.Reference.Get(),
                    FeatureRefs.ShieldsProficiency.Reference.Get()])
                .SetIsClassFeature(true)
                .Configure();
        }
    }
}
