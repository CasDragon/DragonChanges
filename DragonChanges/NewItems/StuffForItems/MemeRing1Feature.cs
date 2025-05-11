using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class MemeRing1Feature
    {
        // edit
        internal const string feature = "MemeRing1Feature";
        internal const string featureguid = Guids.ChaMemeRingACFeature;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static BlueprintFeature ConfigureEnabled()
        {
            BlueprintFeature thing = FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddDerivativeStatBonus(baseStat: StatType.Charisma,
                    descriptor: ModifierDescriptor.Profane,
                    derivativeStat: StatType.AC)
                .AddRecalculateOnStatChange(stat: StatType.Charisma)
                .SetHideInUI(true)
                .Configure();
            return thing;
        }
    }
}
