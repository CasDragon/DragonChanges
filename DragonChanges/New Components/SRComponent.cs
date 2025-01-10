using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Blueprints.Classes.Experience;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintFact), false)]
    [TypeId("4CA4807D-DD7B-4CCB-A495-69F12E5EF676")]
    internal class SRComponent : UnitFactComponentDelegate<AddSpellResistance.ComponentData>
    {

        public bool AddCR = false;
        //public ContextValue Value;
        public StatType Stat = StatType.Charisma;

        [InfoBox("I don't know what does it mean but\nif AllSpellResistancePenaltyDoNotUse == false then Value is Spell Resistance and\nif AllSpellResistancePenaltyDoNotUse == true then Value is Spell Resistance Penalty")]
        [HideIf("AddBonusToResistance")]
        public bool AllSpellResistancePenaltyDoNotUse = false;

        [HideIf("AllSpellResistancePenaltyDoNotUse")]
        public bool AddBonusToResistance = false;
        public override void OnTurnOn()
        {
            //ContextValue value = Value;
            //IFactContextOwner factContextOwner = base.Fact as IFactContextOwner;
            //int num = value.Calculate((factContextOwner != null) ? factContextOwner.Context : null);
            int num = base.Owner.Stats.GetStat(Stat).CalculatePermanentValue() / 2 - 5;
            if (AddCR)
            {
                Experience component = base.Owner.Blueprint.GetComponent<Experience>();
                if (component)
                {
                    num += component.CR;
                }
            }
            if (AddBonusToResistance)
            {
                base.Data.AppliedId = new int?(base.Owner.Ensure<UnitPartSpellResistance>().SetBonus(num));
                base.Data.AppliedId = new int?(base.Owner.Ensure<UnitPartSpellResistance>().AddResistance(0, base.Fact.UniqueId, null, null, null));
                return;
            }
            if (!AllSpellResistancePenaltyDoNotUse)
            {
                base.Data.AppliedId = new int?(base.Owner.Ensure<UnitPartSpellResistance>().AddResistance(num, base.Fact.UniqueId, null, null, null));
                return;
            }
            base.Owner.Ensure<UnitPartSpellResistance>().SetAllSRPenalty(num);
        }
        public override void OnTurnOff()
        {
            if (AddBonusToResistance && base.Data.AppliedId != null)
            {
                base.Owner.Ensure<UnitPartSpellResistance>().RemoveBonus(base.Data.AppliedId.Value);
                return;
            }
            if (!AllSpellResistancePenaltyDoNotUse)
            {
                if (base.Data.AppliedId != null)
                {
                    UnitPartSpellResistance unitPartSpellResistance = base.Owner.Get<UnitPartSpellResistance>();
                    if (unitPartSpellResistance != null)
                    {
                        unitPartSpellResistance.Remove(base.Data.AppliedId.Value);
                    }
                    base.Data.AppliedId = null;
                    return;
                }
            }
            else
            {
                base.Owner.Ensure<UnitPartSpellResistance>().SetAllSRPenalty(0);
            }
        }
        public class ComponentData
        {
            public int? AppliedId;
        }
    }
}
