using BlueprintCore.Blueprints.Configurators.Classes.Spells;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.EntitySystem.Stats;
using UnityEngine;

namespace DragonChanges.New_Classes.Redditor
{
    internal class RedditorSpellbook
    {
        // edit
        internal static string spellbookprefix = "redditor";
        internal static string spellbookguid = Guids.redditorspellbook;
        internal static string spellsperdayguid = Guids.redditorspellsperday;
        // don't edit
        internal static string spellbook = $"{spellbookprefix}.spellbook";
        internal static string spellbookname = $"{spellbook}.name";
        internal static string spellbookperday = $"{spellbook}.perday";
        public static void ConfigureDummy()
        {
            SpellbookConfigurator.New(spellbook, spellbookguid).Configure();
            SpellsTableConfigurator.New(spellbookperday, spellsperdayguid).Configure();
        }
        public static BlueprintSpellbook Configure()
        {
            BlueprintSpellbook x = SpellbookConfigurator.New(spellbook, spellbookguid)
                .SetName(spellbookname)
                .SetSpellsPerDay(GetSpellSlots())
                .SetSpellsKnown(SpellsTableRefs.SorcererSpellsKnownTable.Reference.Get())
                .SetSpellList(SpellListRefs.WizardSpellList.Reference.Get())
                .SetCastingAttribute(StatType.Charisma)
                .SetSpontaneous(true)
                .SetIsArcane(true)
                .SetAllSpellsKnown(true)
                .Configure();

            FeatureSelectMythicSpellbookConfigurator.For(FeatureSelectMythicSpellbookRefs.AngelIncorporateSpellbook)
                .AddToAllowedSpellbooks(x)
                .Configure();

            FeatureSelectMythicSpellbookConfigurator.For(FeatureSelectMythicSpellbookRefs.LichIncorporateSpellbookFeature)
                .AddToAllowedSpellbooks(x)
                .Configure();

            return x;
        }
        public static BlueprintSpellsTable GetSpellSlots()
        {
            var wizardSpellSlots = SpellsTableRefs.WizardSpellLevels.Reference.Get();
            return SpellsTableConfigurator.New(spellbookperday, spellsperdayguid)
                .SetLevels(wizardSpellSlots.Levels)
                .Configure();
        }
    }
}
