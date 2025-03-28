using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [TypeId("d3942470-7eff-4819-b12d-73072e9d7581")]
    internal class SpellPenComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public static StatType stat = StatType.Charisma;
        public static BlueprintUnitFactReference m_RequiredFact;
        public ModifierDescriptor Descriptor = ModifierDescriptor.Inherent;
        public bool CheckFact;

        public BlueprintUnitFact RequiredFact
        {
            get
            {
                BlueprintUnitFactReference requiredFact = m_RequiredFact;
                if (requiredFact == null)
                {
                    return null;
                }
                return requiredFact.Get();
            }
        }
        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt)
        {
            if (!this.CheckFact || evt.Initiator.Descriptor.HasFact(this.RequiredFact))
            {
                int statPermanent = base.Owner.Stats.GetStat(stat).CalculatePermanentValue();
                int num = statPermanent / 2 - 5;
                evt.AddSpellPenetration(num, this.Descriptor);
            }
        }
        public void OnEventDidTrigger(RuleSpellResistanceCheck evt)
        {
        }

    }
}
