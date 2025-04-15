﻿using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.Blueprints.Classes;
using JetBrains.Annotations;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintFeature), false)]
    [AllowedOn(typeof(BlueprintBuff), false)]
    [TypeId("F83FDDD5-8E68-4473-B72D-8E75BD59FE66")]
    public class CritMulti : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
    {
        [NotNull]
        public int bonus = 1;
        [NotNull]
        public ModifierDescriptor stackType = ModifierDescriptor.UntypedStackable;
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            evt.AdditionalCriticalMultiplier.Add(new Modifier(bonus, base.Fact, stackType));
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }
}
