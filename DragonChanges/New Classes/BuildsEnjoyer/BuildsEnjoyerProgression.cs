using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.New_Classes.Redditor;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.New_Classes.BuildsEnjoyer
{
    internal class BuildsEnjoyerProgression
    {
        // edit
        internal static string progressionprefix = "buildsenjoyer";
        internal static string progressionguid = Guids.BuildsEnjoyerProgression;
        // don't edit
        internal static string progressionname = $"{progressionprefix}progression";

        public static BlueprintProgression ConfigureDummy()
        {
            return ProgressionConfigurator.New(progressionname, progressionguid).Configure();
        }
        public static BlueprintProgression ConfigureEnabled()
        {
            LevelEntryBuilder entries = new LevelEntryBuilder()
                .AddEntry(1,
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureRefs.InsultFeature.Reference.Get(),
                    FeatureRefs.WormtongueFeature.Reference.Get(),
                    FeatureRefs.SkaldProficiencies.Reference.Get(),
                    FeatureRefs.RayCalculateFeature.Reference.Get(),
                    FeatureRefs.TouchCalculateFeature.Reference.Get(),
                    FeatureRefs.InspiredRage.Reference.Get(),
                    FeatureRefs.RagingSong.Reference.Get(),
                    FeatureRefs.SkaldCantripsFeature.Reference.Get(),
                    FeatureSelectionRefs.SkaldFeatSelection.Reference.Get())
                .AddEntry(2,
                    FeatureRefs.SneakAttack.Reference.Get())
                .AddEntry(3,
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(4,
                    FeatureRefs.UncannyDodgeChecker.Reference.Get(),
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(5,
                    CHAToStat.ConfigureDEX())
                .AddEntry(6,
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(7,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureRefs.SKaldMovePerformance.Reference.Get())
                .AddEntry(8,
                    FeatureRefs.RagingSongSneakAttackFeature.Reference.Get(),
                    FeatureRefs.RagingSongSneakAttack1d6Feature.Reference.Get(),
                    FeatureRefs.ImprovedUncannyDodge.Reference.Get())
                .AddEntry(9,
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(10,
                    FeatureRefs.CausticRidiculeFeature.Reference.Get())
                .AddEntry(11,
                    CHAToStat.ConfigureSPEED())
                .AddEntry(12,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureRefs.SneakyTricksFeature.Reference.Get(),
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(13,
                    FeatureRefs.RagingSongSneakAttack2d6Feature.Reference.Get(),
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureRefs.SkaldSwiftPerformance.Reference.Get())
                .AddEntry(14,
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(15,
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(16,
                    FeatureSelectionRefs.FighterFeatSelection.Reference.Get())
                .AddEntry(17,
                    FeatureRefs.SneakAttack.Reference.Get())
                .AddEntry(18,
                    FeatureRefs.RagingSongSneakAttack3d6Feature.Reference.Get(),
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(19,
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(20,
                    FeatureRefs.PurtTheLivingFeature.Reference.Get());
            return ProgressionConfigurator.New(progressionname, progressionguid)
                .SetIsClassFeature(true)
                .SetLevelEntries(entries)
                .Configure();
        }
    }
}
