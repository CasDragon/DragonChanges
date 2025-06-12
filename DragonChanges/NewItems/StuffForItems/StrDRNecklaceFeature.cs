using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.BPCoreExtensions;
using DragonChanges.New_Components;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
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
            BlueprintFeature feat;
            if (ModCompat.tttbase)
            {
                feat = FeatureConfigurator.New(feature, featureguid)
                    .SetDisplayName(featurename)
                    .SetDescription(featuredescription)
                    .AddRecalculateOnStatChange(stat: StatType.Strength)
                    .SetHideInUI(true)
                    .AddTTAddDamageResistancePhysicalTest(value: ContextValues.Property(UnitProperty.StatBonusStrength), stackable: true)
                    .Configure();
            }
            else
            {
                feat = FeatureConfigurator.New(feature, featureguid)
                    .SetDisplayName(featurename)
                    .SetDescription(featuredescription)
                    .AddRecalculateOnStatChange(stat: StatType.Strength)
                    .SetHideInUI(true)
                    .AddDRComponent(stackable: true, value: ContextValues.Property(UnitProperty.StatBonusStrength), usePool: false)
                    .Configure();
            }
            return feat;
        }
    }
}
