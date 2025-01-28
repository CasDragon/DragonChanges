using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonChanges.Patches;
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace DragonChanges.NewStuff
{
    internal class TranceMaster
    {

        // edit
        internal static string feature = "TranceMaster";
        internal static string featureguid = Guids.TranceMaster;
        // don't edit
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("swordmastertengu"))
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
                .AddToGroups(Kingmaker.Blueprints.Classes.FeatureGroup.MythicAbility)
                .AddPrerequisiteArchetypeLevel(
                    archetype: Guids.tenguSwordmasterArchetype,
                    characterClass: CharacterClassRefs.RogueClass.Reference.Get())
                .AddIncreaseActivatableAbilityGroupSize((ActivatableAbilityGroup)ActivatableAbilityGroupPatch.DCActivatableAbilityGroup.TenguSwordmasterTrance)
                .Configure();
        }
    }
}
