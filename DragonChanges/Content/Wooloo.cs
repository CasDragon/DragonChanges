using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;

namespace DragonChanges.Content
{
    internal class Wooloo
    {
        [DragonConfigure]
        public static void AddWoolooToSelections()
        {
            if (ModCompat.wooloo)
            {
                Main.log.Log("Enabling Wooloo companion");

                Main.log.Log("Patching various animal selections to include Nightmare horse (Microscopic)");
                string nightmare = "c611377a9e9224b45aefabb7564350f6";
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                    .AddToAllFeatures(nightmare)
                    .Configure();
                if (ModCompat.tttbase)
                {
                    FeatureSelectionConfigurator.For("d9b99d9c48d2425894b565733e96c7e3")
                        .AddToAllFeatures(nightmare)
                        .Configure();
                }
            }
        }
    }
}
