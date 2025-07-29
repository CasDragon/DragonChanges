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
                    FeatureRefs.RangerProficiencies.Reference.Get(),
                    FeatureRefs.MonkFlurryOfBlowstUnlock.Reference.Get(),
                    FeatureRefs.ImprovedUnarmedStrike.Reference.Get(),
                    FeatureRefs.MonkACBonusUnlock.Reference.Get(),
                    FeatureRefs.StunningFist.Reference.Get(),
                    FeatureRefs.MonkUnarmedStrike.Reference.Get(),
                    FeatureRefs.MonkWeaponProficiency.Reference.Get(),
                    FeatureSelectionRefs.SableMarineHippogriffCompanionSelection.Reference.Get(),
                    FeatureSelectionRefs.MonkBonusFeatSelectionLevel1.Reference.Get(),
                    FeatureSelectionRefs.SkaldFeatSelection.Reference.Get())
                .AddEntry(2,
                    FeatureRefs.Evasion.Reference.Get(),
                    FeatureSelectionRefs.RangerStyleSelection2.Reference.Get(),
                    FeatureSelectionRefs.MonkBonusFeatSelectionLevel1.Reference.Get(),
                    FeatureRefs.SneakAttack.Reference.Get())
                .AddEntry(3,
                    FeatureRefs.MonkFastMovementUnlock.Reference.Get(),
                    FeatureRefs.KiStrikeMagic.Reference.Get(),
                    FeatureRefs.KiPowerDrunkenFeature.Reference.Get(),
                    FeatureRefs.DrunkenCombatFeature.Reference.Get(),
                    FeatureRefs.SableMarineStrongestWingsFeature.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemySelection.Reference.Get(),
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(4,
                    FeatureRefs.UncannyDodgeChecker.Reference.Get(),
                    FeatureRefs.DismissAreaEffectFeature.Reference.Get(),
                    FeatureRefs.MonkUnarmedStrikeLevel4.Reference.Get(),
                    FeatureRefs.StunningFistFatigueFeature.Reference.Get(),
                    FeatureRefs.DrunkenTechniqueFirewaterBreathFeature.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(5,
                    FeatureRefs.KiPowerDrunkenFeature.Reference.Get(),
                    FeatureSelectionRefs.MonkStyleStrike.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemySelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemyRankUp.Reference.Get())
                .AddEntry(6,
                    FeatureRefs.DrunkenTechniqueCaydenTrickFeature.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureSelectionRefs.MonkBonusFeatSelectionLevel6.Reference.Get(),
                    FeatureSelectionRefs.RangerStyleSelection6.Reference.Get(),
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(7,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureRefs.KiStrikeColdIronSilver.Reference.Get(),
                    FeatureRefs.DrunkenCourageFeature.Reference.Get(),
                    FeatureRefs.SKaldMovePerformance.Reference.Get())
                .AddEntry(8,
                    FeatureRefs.KiPowerDrunkenFeature.Reference.Get(),
                    FeatureRefs.RagingSongSneakAttackFeature.Reference.Get(),
                    FeatureRefs.RagingSongSneakAttack1d6Feature.Reference.Get(),
                    FeatureRefs.MonkUnarmedStrikeLevel8.Reference.Get(),
                    FeatureRefs.StunningFistSickenedFeature.Reference.Get(),
                    FeatureRefs.DrunkenMasterDrinklevelupFeature.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureRefs.ImprovedUncannyDodge.Reference.Get())
                .AddEntry(9,
                    FeatureRefs.ImprovedEvasion.Reference.Get(),
                    FeatureRefs.DrunkenResilienceFeature.Reference.Get(),
                    FeatureSelectionRefs.MonkStyleStrike.Reference.Get(),
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(10,
                    FeatureRefs.KiStrikeLawful.Reference.Get(),
                    FeatureSelectionRefs.MonkBonusFeatSelectionLevel10.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureSelectionRefs.RangerStyleSelection10.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemySelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemyRankUp.Reference.Get(),
                    FeatureRefs.CausticRidiculeFeature.Reference.Get())
                .AddEntry(11,
                    FeatureRefs.MonkFlurryOfBlowstLevel11Unlock.Reference.Get(),
                    FeatureRefs.DrunkenCourage11thFeature.Reference.Get(),
                    FeatureRefs.DrunkenPowerFeature.Reference.Get(),
                    FeatureRefs.Quarry.Reference.Get())
                .AddEntry(12,
                    FeatureRefs.SneakAttack.Reference.Get(),
                    FeatureRefs.SneakyTricksFeature.Reference.Get(),
                    FeatureRefs.SableMarineFeatheredConfusionFeature.Reference.Get(),
                    FeatureRefs.MonkUnarmedStrikeLevel12.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(13,
                    FeatureRefs.RagingSongSneakAttack2d6Feature.Reference.Get(),
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureSelectionRefs.MonkStyleStrike.Reference.Get(),
                    FeatureRefs.SkaldSwiftPerformance.Reference.Get())
                .AddEntry(14,
                    FeatureRefs.DrunkenPowerFeature.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureSelectionRefs.MonkBonusFeatSelectionLevel10.Reference.Get(),
                    FeatureSelectionRefs.RangerStyleSelection10.Reference.Get(),
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(15,
                    FeatureSelectionRefs.FavoriteEnemySelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemyRankUp.Reference.Get())
                .AddEntry(16,
                    FeatureRefs.KiStrikeAdamantine.Reference.Get(),
                    FeatureRefs.MonkUnarmedStrikeLevel16.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get())
                .AddEntry(17,
                    FeatureSelectionRefs.MonkStyleStrike.Reference.Get(),
                    FeatureRefs.SneakAttack.Reference.Get())
                .AddEntry(18,
                    FeatureRefs.DrunkenPowerFeature.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureSelectionRefs.MonkBonusFeatSelectionLevel10.Reference.Get(),
                    FeatureSelectionRefs.RangerStyleSelection10.Reference.Get(),
                    FeatureRefs.RagingSongSneakAttack3d6Feature.Reference.Get(),
                    FeatureSelectionRefs.SkaldRagePowerSelection.Reference.Get())
                .AddEntry(19,
                    FeatureRefs.MindGamesFeature.Reference.Get(),
                    FeatureRefs.ImprovedQuarry.Reference.Get(),
                    FeatureSelectionRefs.ProvocateurTalentSelection.Reference.Get())
                .AddEntry(20,
                    FeatureRefs.MonkUnarmedStrikeLevel20.Reference.Get(),
                    FeatureRefs.DrunkenCapstoneFeature.Reference.Get(),
                    FeatureSelectionRefs.DrunkenKiSelection.Reference.Get(),
                    FeatureRefs.SableMarineSableStrikeFeature.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemySelection.Reference.Get(),
                    FeatureSelectionRefs.FavoriteEnemyRankUp.Reference.Get(),
                    FeatureRefs.PurtTheLivingFeature.Reference.Get());
            return ProgressionConfigurator.New(progressionname, progressionguid)
                .SetIsClassFeature(true)
                .SetLevelEntries(entries)
                .SetClasses([Guids.BuildsEnjoyerClass])
                .Configure();
        }
    }
}
