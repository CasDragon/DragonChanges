using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Patches;
using DragonChanges.Utils;
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace DragonChanges.NewStuff
{
    internal class TranceMaster
    {

        // edit
        internal const string feature = "TranceMaster";
        internal const string featureguid = Guids.TranceMaster;
        // don't edit
        internal const string featurename = $"{feature}.name";
        internal const string featuredescription = $"{feature}.description";
        [DragonConfigure]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>("swordmastertengu"))
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
