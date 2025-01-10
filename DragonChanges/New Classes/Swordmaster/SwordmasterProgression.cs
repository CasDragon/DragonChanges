using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Classes.Swordmaster
{
    internal class SwordmasterProgression
    {// edit
        internal static string progressionprefix = "swordmaster";
        internal static string progressionguid = Guids.SwordMasterProgression;
        // don't edit
        internal static string progressionname = $"{progressionprefix}.progression.name";

        public static void ConfigureDummy()
        {
            ProgressionConfigurator.New(progressionname, progressionguid).Configure();
        }
        public static BlueprintProgression Configure()
        {
            LevelEntryBuilder entries = new LevelEntryBuilder();
            BlueprintProgression prog = ProgressionConfigurator.New(progressionname, progressionguid)
                .SetIsClassFeature(true)
                .SetLevelEntries(entries)
                .Configure();
            return prog;
        }
    }
}
