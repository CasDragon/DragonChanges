using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Classes.Swordmaster.Features
{
    internal class Proficiencies
    {
        // edit
        internal static string featureprefix = "swordmaster.prociencies";
        internal static string featureguid = Guids.SwordmasterProficiences;
        // don't edit
        internal static string featurename = $"{featureprefix}.name";
        internal static string featuredescription = $"{featureprefix}.description";

        public static BlueprintFeature ConfigureDummy()
        {
            return FeatureConfigurator.New(featurename, featureguid)
                .Configure();
        }
        public static BlueprintFeature Configure()
        {
            return FeatureConfigurator.New(featureprefix, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddFeatureOnApply(FeatureRefs.SimpleWeaponProficiency.Reference.Get())
                .AddFeatureOnApply(FeatureRefs.MartialWeaponProficiency.Reference.Get())
                .AddFeatureOnApply(FeatureRefs.QuarterstaffProficiency.Reference.Get())
                .AddFeatureOnApply(FeatureRefs.LightArmorProficiency.Reference.Get())
                .Configure();
        }
    }
}
