using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.EntitySystem.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class PowerfulWings
    {
        // edit
        internal static string feature = "PowerfulWings";
        internal static string featureguid = Guids.PowerfulWings;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
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
                .AddPrerequisiteFullStatValue(stat: StatType.BaseAttackBonus,
                                              value: 8)
                .AddPrerequisiteFullStatValue(stat: StatType.Strength,
                                              value: 13)
                .AddPrerequisiteFeature(FeatureRefs.WeaponFocusWing.Reference.Get())
                .AddAdditionalLimb(ItemWeaponRefs.WingColossal2d8.Reference.Get())
                .AddAdditionalLimb(ItemWeaponRefs.WingColossal2d8.Reference.Get())
                .Configure();
        }
    }
}
