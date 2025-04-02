using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using JetBrains.Annotations;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Utility;
using BlueprintCore.Utils;

namespace DragonChanges.New_Components
{
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [TypeId("4A25A5B0-E180-42BF-81E0-45334182EDCB")]
    internal class SpellToBook : UnitFactComponentDelegate, IUnitSubscriber, ISubscriber
    {
        [NotNull]
        public BlueprintAbility spell;

        [NotNull]
        public int spelllevel;

        public override void OnTurnOn()
        {
            AddToKnown();
        }

        public override void OnTurnOff()
        {
            RemoveFromKnown();
        }
        private void AddToKnown()
        {
            foreach (ClassData classData in this.Owner.Progression.Classes)
            {
                if (classData.Spellbook != null && !classData.Spellbook.IsMythic)
                {
                    Spellbook spellbook = this.Owner.Descriptor.GetSpellbook(classData.Spellbook);
                    if (spellbook != null)
                    {
                        var sblvl = spellbook.GetMaxSpellLevel();
                        if (sblvl < spelllevel)
                        {
                            return;
                        }
                        else
                        {
                            AddKnownTemporary(spellbook, spelllevel, spell);
                        }
                    }
                }
            }
        }
        private void RemoveFromKnown()
        { 
            foreach (ClassData classData in this.Owner.Progression.Classes)
            {
                if (classData.Spellbook != null && !classData.Spellbook.IsMythic)
                {
                    Spellbook spellbook = this.Owner.Descriptor.GetSpellbook(classData.Spellbook);
                    if (spellbook != null)
                    {
                        AbilityData abilityData = spellbook.SureKnownSpells(spelllevel).FirstItem((AbilityData s) => s.Blueprint == spell);
                        spellbook.RemoveTemporarySpell(abilityData);
                    }
                }
            }
        }
        private static AbilityData AddKnownTemporary(Spellbook sb, int spellLevel, BlueprintAbility blueprint)
        {
            AbilityData abilityData = sb.SureKnownSpells(spellLevel).FirstItem((AbilityData s) => s.Blueprint == blueprint);
            if (abilityData == null)
            {
                abilityData = new AbilityData(blueprint, sb, spellLevel)
                {
                    IsTemporary = true
                };
                sb.SureKnownSpells(spellLevel).Add(abilityData);
                sb.AddKnownSpellLevel(blueprint, spellLevel);
            }

            return abilityData;
        }
    }
}
