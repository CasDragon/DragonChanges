using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintFeature), false)]
    [AllowedOn(typeof(BlueprintBuff), false)]
    [TypeId("7F59444B-8623-45EA-8456-5B768E6587C1")]
    [Serializable]
    public class CritRange : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
    {
        [SerializeField]
        public int bonus = 1;
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            evt.CriticalEdgeBonus += bonus;
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }
}
