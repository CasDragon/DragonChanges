using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics;

namespace DragonChanges.NewAbilities
{
    internal class StorvalsThunderAbility
    {
        // edit
        internal static string ability = "StorvalsThunder-ability";
        internal static string abilityguid = Guids.ThunderHammerAbility;
        // don't edit
        internal static string abilityname = $"{ability}.name";
        internal static string abilitydescription = $"{ability}.description";
        //[DragonConfigure]
        public static BlueprintAbility ConfigureDummy()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .AddAbilityEffectRunAction(
                    ActionsBuilder.New()
                        .SavingThrow(type: SavingThrowType.Fortitude,
                            onResult:
                            ActionsBuilder.New().ConditionalSaved(
                                failed: 
                                    ActionsBuilder.New()
                                        .ApplyBuff(
                                            buff: BuffRefs.Stunned.Reference.Get(),
                                            durationValue: ContextDuration.Fixed(value: 1, rate: DurationRate.Rounds, isExtendable: false),
                                            asChild: true
                                        )
                            )
                        )
                    )
                .AddContextRankConfig(ContextRankConfigs.CasterLevel())
                .AddComponent(AbilityRefs.VrockStunningScreechAbility.Reference.Get().GetComponent<AbilitySpawnFx>())
                .SetType(AbilityType.Supernatural)
                .SetRange(AbilityRange.Close)
                .SetCanTargetPoint(false)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(false)
                .SetCanTargetEnemies(true)
                .SetShouldTurnToTarget(true)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("StorvalsThunder.png"))
                .Configure();
        }
    }
}
