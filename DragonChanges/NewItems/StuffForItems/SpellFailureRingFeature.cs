using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class SpellFailureRingFeature
    {
        // edit
        internal const string feature = "SpellFailureRingFeature";
        internal const string featureguid = Guids.SpellFailureRingFeature;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static Kingmaker.Blueprints.Classes.BlueprintFeature ConfigureEnabled()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddArcaneSpellFailureIncrease(-25)
                .SetHideInUI(true)
                .Configure();
        }
    }
}
