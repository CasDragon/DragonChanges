using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class SpellFailureRingFeature2
    {
        // edit
        internal const string feature = "SpellFailureRingFeature2";
        internal const string featureguid = Guids.SpellFailureRingFeature2;
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
                .AddArcaneSpellFailureIncrease(-25, toShield: true)
                .SetHideInUI(true)
                .Configure();
        }
    }
}
