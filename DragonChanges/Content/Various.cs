﻿using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
namespace DragonChanges.Content
{
    internal class Various
    {
        [DragonConfigure]
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
        [DragonConfigure]
        public static void CarrierofDisease()
        {
            Main.log.Log("Adding Carrier of Disease to Extra Hex");
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraWitchHexSelection)
                .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.Reference.Get())
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraShamanHexSelection)
                .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.Reference.Get())
                .Configure();
        }
    }
}
