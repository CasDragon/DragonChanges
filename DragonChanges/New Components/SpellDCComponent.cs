using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintFact), false)]
    [TypeId("1b58faae-f925-4d08-a318-79fc5f4cff51")]
    internal class SpellDCComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public static StatType stat = StatType.Charisma;
        public ModifierDescriptor Descriptor = ModifierDescriptor.Inherent;


        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            evt.AddBonusCasterLevel(this.GetBonus(evt), this.Descriptor);
        }
        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
        public int GetBonus(RuleCalculateAbilityParams evt)
        {
            int statPermanent = base.Owner.Stats.GetStat(stat).CalculatePermanentValue();
            return statPermanent / 2 - 5;
        }
    }
}
