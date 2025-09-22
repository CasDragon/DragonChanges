using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Utility;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class RangedCleaveBracersFeature
    {
        // edit
        internal const string feature = "RangedCleaveBracersFeature";
        internal const string featureguid = Guids.RangedCleaveBracersFeature;
        internal const string featurename = "not implemented";
        internal const string featuredescription = "not implemented";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static Kingmaker.Blueprints.Classes.BlueprintFeature ConfigureEnabled()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddRangedCleave(range: new Feet(15))
                .SetHideInUI(true)
                .Configure();
        }
    }
}
