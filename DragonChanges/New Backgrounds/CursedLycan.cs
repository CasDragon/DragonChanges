using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;

namespace DragonChanges.New_Backgrounds
{
    internal class CursedLycan
    {
        // edit
        internal static string feature = "Wanderer";
        internal static string featureguid = Guids.CursedBackground;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>(feature.ToLower()))
            {
                Main.log.Log($"Cursed background feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"Cursed background disabled, configuring dummy");
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
                .AddToFeatureSelection(FeatureSelectionRefs.BackgroundsWandererSelection.Reference.Get())
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillPerception)
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillAthletics)
                .Configure();
        }
    }
}
