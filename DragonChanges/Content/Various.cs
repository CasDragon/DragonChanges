using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.Content
{
    internal class Various
    {
        public static void PatchPairedOpportunist()
        {
            if (Settings.GetSetting<bool>("hippogriff"))
            {
                if (ModCompat.cop)
                {
                    Main.log.Log("CO-Paired Opportunist setting enabled, patching selections to add it");
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierTacticianFeatSelection)
                        .AddToAllFeatures("41df43af78bc477aa33ae57d86ba8928")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.DevilbanePriestTeamworkFeatSelection)
                        .AddToAllFeatures("41df43af78bc477aa33ae57d86ba8928")
                        .Configure();
                    return;
                }
                else
                {
                    Main.log.Log("CO-Paired Opportunist setting enabled, but CO+ isn't detected, skipping");
                    return;
                }
            }
            else
            {
                Main.log.Log("CO-Paired Opportunist setting disabled, skipping");
            }
        }
        public static void PatchHippogriff()
        {
            if (Settings.GetSetting<bool>("hippogriff"))
            {
                Main.log.Log("Patching various animal selections to include Hippogriff");
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                    .AddToAllFeatures(FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get())
                    .Configure();
            }
        }
    }
}
