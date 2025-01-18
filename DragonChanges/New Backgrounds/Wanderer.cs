using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Backgrounds
{
    internal class Wanderer
    {
        // edit
        internal static string feature = "Wanderer";
        internal static string featureguid = Guids.WandererBackground;
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
            FeatureConfigurator.New(feature, featureguid, Kingmaker.Blueprints.Classes.FeatureGroup.BackgroundSelection)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillPerception)
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillAthletics)
                .Configure();
        }
    }
}
