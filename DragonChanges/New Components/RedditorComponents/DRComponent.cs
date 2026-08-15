using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Items;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.FactLogic;
using UnityEngine;

namespace DragonChanges.New_Components.RedditorComponents
{
    [AllowedOn(typeof(BlueprintFact), false)]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [TypeId("A9675332-F963-4F5F-AA97-7E6E85E0D8D1")]
    [Serializable]
    internal class DRComponent : AddDamageResistanceBase
    {
        [SerializeField]
        public StatType Stat = StatType.Charisma;
        [SerializeField]
        public bool m_IsStackable = true;
        [SerializeField]
        public int Multiplier = 1;

        public override bool Bypassed(AddDamageResistanceBase.ComponentRuntime runtime, BaseDamage damage, ItemEntityWeapon weapon)
        {
            return false;
        }
        public override int CalculateValue(AddDamageResistanceBase.ComponentRuntime runtime)
        {
            int x = runtime.Owner.Stats.GetStat(this.Stat).CalculatePermanentValue() / 2 - 5;
            x *= Multiplier;
            return x;
        }
        public override bool IsStackable => m_IsStackable;
    }
}
