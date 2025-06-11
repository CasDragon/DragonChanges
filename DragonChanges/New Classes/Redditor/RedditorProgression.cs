using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.New_Classes.Redditor
{
    internal class RedditorProgression
    {
        // edit
        internal static string progressionprefix = "redditor";
        internal static string progressionguid = Guids.redditorprogression;
        // don't edit
        internal static string progressionname = $"{progressionprefix}progression";

        public static BlueprintProgression ConfigureDummy()
        {
            CHAToStat.ConfigureAC();
            CHAToStat.ConfigureINT();
            CHAToStat.ConfigureSTR();
            CHAToStat.ConfigureDEX();
            CHAToStat.ConfigureCON();
            CHAToStat.ConfigureWIS();
            CHAToStat.ConfigureCHA();
            CHAToStat.ConfigureSAVES();
            CHAToStat.ConfigureINIT();
            CHAToStat.ConfigureSPEED();
            CHAToStat.ConfigureBAB();
            CHAToStat.ConfigureDAM();
            CHAToStat.ConfigureSKILLS();
            CHAToStat.ConfigureCRIT();
            CHAToStat.ConfigureSR();
            CHAToStat.ConfigureDR();
            CHAToStat.ConfigureSpellPen();
            CHAToStat.ConfigureSpellDC();
            CHAToStat.ConfigureCHA_CAP();
            return ProgressionConfigurator.New(progressionname, progressionguid).Configure();
        }
        public static BlueprintProgression Configure()
        {
            LevelEntryBuilder entries = new LevelEntryBuilder()
                .AddEntry(1,
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get(),
                    FeatureRefs.FighterProficiencies.Reference.Get())
                .AddEntry(2,
                    CHAToStat.ConfigureAC(),
                    FeatureRefs.Bravery.Reference.Get(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(3,
                    CHAToStat.ConfigureINT(),
                    FeatureRefs.ArmorTraining.Reference.Get())
                .AddEntry(4,
                    CHAToStat.ConfigureSTR(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(5,
                    CHAToStat.ConfigureDEX(),
                    FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get())
                .AddEntry(6,
                    CHAToStat.ConfigureCON(),
                    FeatureRefs.Bravery.Reference.Get(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(7,
                    CHAToStat.ConfigureWIS(),
                    FeatureRefs.ArmorTraining.Reference.Get())
                .AddEntry(8,
                    CHAToStat.ConfigureCHA(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(9,
                    CHAToStat.ConfigureSAVES(),
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get(),
                    FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get())
                .AddEntry(10,
                    CHAToStat.ConfigureINIT(),
                    FeatureRefs.Bravery.Reference.Get(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(11,
                    CHAToStat.ConfigureSPEED(),
                    FeatureRefs.ArmorTraining.Reference.Get())
                .AddEntry(12,
                    CHAToStat.ConfigureBAB(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(13,
                    CHAToStat.ConfigureDAM(),
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get(),
                    FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get())
                .AddEntry(14,
                    CHAToStat.ConfigureSKILLS(),
                    FeatureRefs.Bravery.Reference.Get(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(15,
                    CHAToStat.ConfigureCRIT(),
                    FeatureRefs.ArmorTraining.Reference.Get())
                .AddEntry(16,
                    CHAToStat.ConfigureSR(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(17,
                    CHAToStat.ConfigureDR(),
                    FeatureSelectionRefs.WeaponTrainingRankUpSelection.Reference.Get(),
                    FeatureSelectionRefs.WeaponTrainingSelection.Reference.Get())
                .AddEntry(18,
                    CHAToStat.ConfigureSpellPen(),
                    FeatureRefs.Bravery.Reference.Get(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(19,
                    CHAToStat.ConfigureSpellDC(),
                    FeatureRefs.ArmorMastery.Reference.Get())
                .AddEntry(20,
                    CHAToStat.ConfigureCHA_CAP(),
                    FeatureSelectionRefs.WeaponMasterySelection.Reference.Get(),
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get());

            return ProgressionConfigurator.New(progressionname, progressionguid)
                .SetIsClassFeature(true)
                .SetLevelEntries(entries)
                .Configure();
        }
    }
}
