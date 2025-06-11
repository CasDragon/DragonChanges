using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.BPCoreExtensions;
using DragonChanges.New_Components;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace DragonChanges.NewItems.StuffForItems
{
    internal class StrDRNecklaceFeature
    {
        // edit
        internal const string feature = "Str2DRNecklaceFeature";
        internal const string featureguid = Guids.StrToDRNecklaceFeature;
        internal const string settingName = "";
        internal const string settingDescription = "";
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static BlueprintFeature ConfigureEnabled()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .SetHideInUI(true)
                .AddDRComponent(stackable: true, value: ContextValues.Property(UnitProperty.StatBonusStrength), usePool: false)
                .Configure();
        }
    }
}
