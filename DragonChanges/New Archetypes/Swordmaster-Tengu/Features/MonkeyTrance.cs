using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using DragonChanges.Patches;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;

namespace DragonChanges.New_Archetypes.Swordmaster_Tengu.Features
{
    internal class MonkeyTrance
    {
        // edit
        internal static string featureprefix = "swordmaster-tengu.monkeytrance";
        internal static string featuretype = "feature";
        internal static string featureguid = Guids.MonkeyTranceFeature;
        // don't edit
        internal static string feature = "MonkeyTrance";
        internal static string featurename = $"{feature}.name";
        internal static string featuredescription = $"{feature}.description";

        public static BlueprintFeature ConfigureFeatureDummy()
        {
            ConfigureAbilityDummy();
            ConfigureBuffDummy();
            return FeatureConfigurator.New(feature, featureguid)
                .Configure();
        }
        public static BlueprintFeature ConfigureFeature()
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurename)
                .SetDescription(featuredescription)
                .AddFacts(new() { ConfigureAbility() })
                .SetIsClassFeature(true)
                .Configure();
        }
        // edit
        internal static string abilityprefix = "swordmaster-tengu.monkeytrance";
        internal static string abilitytype = "ability";
        internal static string abilityguid = Guids.MonkeyTranceAbility;
        // don't edit
        internal static string ability = $"{abilityprefix}.{abilitytype}";
        internal static string abilityname = $"{ability}.name";
        internal static string abilitydescription = $"{ability}.description";

        public static BlueprintActivatableAbility ConfigureAbilityDummy()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .Configure();
        }
        public static BlueprintActivatableAbility ConfigureAbility()
        {
            return ActivatableAbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .AddRestrictionHasUnitCondition(Kingmaker.UnitLogic.UnitCondition.Fatigued, invert: true)
                .AddAbilityResources(resource: Swordmaster_Tengu.abilityResource)
                .SetDeactivateIfCombatEnded(false)
                .SetDeactivateImmediately(true)
                .SetDeactivateIfOwnerUnconscious(true)
                .SetOnlyInCombat(false)
                .SetActivationType(AbilityActivationType.WithUnitCommand)
                .SetActivateWithUnitCommand(UnitCommand.CommandType.Swift)
                .SetGroup((ActivatableAbilityGroup)ActivatableAbilityGroupPatch.DCActivatableAbilityGroup.TenguSwordmasterTrance)
                .SetBuff(ConfigureBuff())
                .SetIcon("Assets/Modifications/DragonChanges 1/MonkeyTrance.png".ToLower())
                .Configure();
        }
        // edit
        internal static string buffprefix = "swordmaster-tengu.monkeytrance";
        internal static string bufftype = "buff";
        internal static string buffguid = Guids.MonkeyTranceBuff;
        // don't edit
        internal static string buff = $"{buffprefix}.{bufftype}";
        internal static string buffname = $"{buff}.name";
        internal static string buffdescription = $"{buff}.description";

        public static BlueprintBuff ConfigureBuffDummy()
        {
            return BuffConfigurator.New(buff, buffguid)
                .Configure();
        }
        public static BlueprintBuff ConfigureBuff()
        {
            return BuffConfigurator.New(buff, buffguid)
                .SetDisplayName(buffname)
                .SetDescription(buffdescription)
                .AddFeatureIfHasFact(checkedFact: FeatureRefs.ImprovedTrip.Reference.Get(), feature: FeatureRefs.ImprovedTrip.Reference.Get(), not: true)
                .AddFeatureIfHasFact(checkedFact: FeatureRefs.FurysFall.Reference.Get(), feature: FeatureRefs.FurysFall.Reference.Get(), not: true)
                .SetIsClassFeature(true)
                .SetStacking(StackingType.Prolong)
                .SetRanks(0)
                .SetTickEachSecond(false)
                .SetFrequency(Kingmaker.UnitLogic.Mechanics.DurationRate.Rounds)
                .SetIcon("Assets/Modifications/DragonChanges 1/MonkeyTrance.png".ToLower())
                .Configure();
        }
    }
}
