﻿using BlueprintCore.Actions.Builder;
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
        internal const string ability = "StorvalsThunder-ability";
        internal const string abilityguid = Guids.ThunderHammerAbility;
        // don't edit
        internal const string abilityname = $"{ability}.name";
        internal const string abilitydescription = $"{ability}.description";
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
                .AddContextSetAbilityParams(dC: ContextValues.Constant(20))
                .AddComponent(AbilityRefs.VrockStunningScreechAbility.Reference.Get().GetComponent<AbilitySpawnFx>())
                .SetType(AbilityType.Supernatural)
                .SetRange(AbilityRange.Close)
                .SetCanTargetPoint(false)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(false)
                .SetCanTargetEnemies(true)
                .SetShouldTurnToTarget(true)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetIcon(MicroAssetUtil.GetAssemblyResourceSprite("Abilities.StorvalsThunder.png"))
                .Configure();
        }
    }
}
