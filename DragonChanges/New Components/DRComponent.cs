using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.Items;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintFact), false)]
    [AllowMultipleComponents]
    [TypeId("A9675332-F963-4F5F-AA97-7E6E85E0D8D1")]
    internal class DRComponent : AddDamageResistanceBase
    {
        public StatType Stat = StatType.Charisma;
        public bool m_IsStackable = true;

        public DamageEnergyTypeFlag m_ExcludedTypes;
        public override bool Bypassed(AddDamageResistanceBase.ComponentRuntime runtime, BaseDamage damage, ItemEntityWeapon weapon)
        {
            return false;
        }
        public override int CalculateValue(AddDamageResistanceBase.ComponentRuntime runtime)
        {
            //Main.log.Log("Calculating DR Value ");
            var x = runtime.Owner.Stats.GetStat(this.Stat).CalculatePermanentValue() / 2 - 5;
            //Main.log.Log("DR Value is - " + x.ToString());
            return x;
        }
        public override bool IsStackable
        {
            get
            {
                return this.m_IsStackable;
            }
        }
    }
}
