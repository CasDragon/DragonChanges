using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.New_Classes.Swordmaster
{
    internal class SwordmasterProgression
    {
        // edit
        internal static string progressionprefix = "swordmaster";
        internal static string progressionguid = Guids.SwordMasterProgression;
        // don't edit
        internal static string progressionname = $"{progressionprefix}.progression.name";

        public static void ConfigureDummy()
        {
            LevelEntryBuilder entries = new LevelEntryBuilder()
                .AddEntry(0, Features.Proficiencies.ConfigureDummy());
            ProgressionConfigurator.New(progressionname, progressionguid)
                .SetLevelEntries(entries)
                .Configure();
        }
        public static BlueprintProgression Configure()
        {
            LevelEntryBuilder entries = new LevelEntryBuilder()
                .AddEntry(0, Features.Proficiencies.Configure());
            BlueprintProgression prog = ProgressionConfigurator.New(progressionname, progressionguid)
                .SetIsClassFeature(true)
                .SetLevelEntries(entries)
                .Configure();
            return prog;
        }
    }
}
