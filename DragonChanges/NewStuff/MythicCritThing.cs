using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.New_Components;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class MythicCritThing
    {
        // edit
        internal static string feature = "GraveSingerPlus";
        internal static string featureguid = Guids.MythicCritThingy;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(feature.ToLower()))
            {
                Main.log.Log($"{feature} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid).Configure();
        }
        public static void ConfigureEnabled()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddComponent(new CritMulti())
                .AddComponent(new CritRange())
                .AddToGroups(FeatureGroup.MythicFeat)
                .AddPrerequisiteFeature(ParametrizedFeatureRefs.ImprovedCritical.Reference.Get())
                .AddPrerequisiteFeature(ParametrizedFeatureRefs.ImprovedCriticalMythicFeat.Reference.Get())
                .Configure();
        }
    }
}
