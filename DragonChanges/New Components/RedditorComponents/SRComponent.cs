using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Experience;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;

namespace DragonChanges.New_Components.RedditorComponents
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
            int num = Owner.Stats.GetStat(Stat).CalculatePermanentValue() / 2 - 5;
            if (AddCR)
            {
                Experience component = Owner.Blueprint.GetComponent<Experience>()!;
                if (component)
                {
                    num += component.CR;
                }
            }
            if (AddBonusToResistance)
            {
                Data.AppliedId = Owner.Ensure<UnitPartSpellResistance>().SetBonus(num);
                Data.AppliedId = Owner.Ensure<UnitPartSpellResistance>().AddResistance(0, Fact.UniqueId, null, null, null);
                return;
            }
            if (!AllSpellResistancePenaltyDoNotUse)
            {
                Data.AppliedId = Owner.Ensure<UnitPartSpellResistance>().AddResistance(num, Fact.UniqueId, null, null, null);
                return;
            }
            Owner.Ensure<UnitPartSpellResistance>().SetAllSRPenalty(num);
        }
        public override void OnTurnOff()
        {
            if (AddBonusToResistance && base.Data.AppliedId != null)
            {
                Owner.Ensure<UnitPartSpellResistance>().RemoveBonus(base.Data.AppliedId.Value);
                return;
            }
            if (!AllSpellResistancePenaltyDoNotUse)
            {
                if (base.Data.AppliedId != null)
                {
                    UnitPartSpellResistance? unitPartSpellResistance = Owner.Get<UnitPartSpellResistance>();
                    unitPartSpellResistance?.Remove(Data.AppliedId.Value);
                    Data.AppliedId = null;
                }
            }
            else
            {
                Owner.Ensure<UnitPartSpellResistance>().SetAllSRPenalty(0);
            }
        }
        public class ComponentData
        {
            public int? AppliedId;
        }
    }
}
