using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using DragonLibrary.BPCoreExtensions;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic;
using BlueprintCore.Blueprints.References;

namespace DragonChanges.NewAbilities
{
    internal class ChargingThrowAbiliy
    {
        // edit
        private const string ability = "ChargingHurler";
        private const string abilityguid = Guids.chargingthrowability;
        // don't edit
        [DragonLocalizedString(abilityname, "Charging Hurler")]
        private const string abilityname = $"{ability}.name";
        [DragonLocalizedString(abilitydescription, "You can use the charge rules to make a thrown weapon attack. All the parameters of a charge apply, except that you must only move closer to your opponent, and you must end your movement within 30 feet of that opponent. If you do, you can make a single thrown weapon attack against that opponent, gaining the +2 bonus on the attack roll and taking a –2 penalty to your AC until the start of your next turn.")]
        private const string abilitydescription = $"{ability}.description";
        public static BlueprintAbility ConfigureDummy()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
        }
        public static BlueprintAbility ConfigureEnabled()
        {
            return AbilityConfigurator.New(ability, abilityguid)
                .SetDisplayName(abilityname)
                .SetDescription(abilitydescription)
                .AddChargingThrow()
                .AddAbilityRequirementHasCondition(not: true,
                    conditions: [UnitCondition.Fatigued, UnitCondition.DifficultTerrain, UnitCondition.Entangled],
                    mountConditions: [UnitCondition.Fatigued, UnitCondition.DifficultTerrain, UnitCondition.Entangled])
                .AddAbilityCasterHasNoFacts(facts: [BuffRefs.Exhausted.ToString(), BuffRefs.CorruptionLevel2.ToString(), BuffRefs.DLC2_Player_Wounded_Buff.ToString()],
                    factsPets: [BuffRefs.Exhausted.ToString(), BuffRefs.CorruptionLevel2.ToString()])
                .AddAbilityIsFullRoundInTurnBased(fullRoundIfTurnBased: true)
                .AddAbilityRequirementCanMove()
                .AddHideDCFromTooltip()
                .SetIcon(AbilityRefs.ChargeAbility.Reference.Get().Icon!)
                .SetType(AbilityType.Physical)
                .SetRange(AbilityRange.DoubleMove)
                .SetCanTargetPoint(false)
                .SetCanTargetFriends(false)
                .SetCanTargetSelf(false)
                .SetCanTargetEnemies(true)
                .SetShouldTurnToTarget(true)
                .SetNeedEquipWeapons(true)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Immediate)
                .Configure();
        }
    }
}
