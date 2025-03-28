using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintFact), false)]
    [TypeId("CE9B8477-90C8-4D9A-8626-A00F432C4B7B")]
    internal class CritComponent : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public StatType stat = StatType.Charisma;

        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            int statPermanent = base.Owner.Stats.GetStat(stat).CalculatePermanentValue();
            evt.CriticalEdgeBonus += statPermanent / 2 - 5;
        }
        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }
}
