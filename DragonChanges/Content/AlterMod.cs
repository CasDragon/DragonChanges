using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;

namespace DragonChanges.Content
{
    internal class AlterMod
    {
        [DragonConfigure]
        public static void PatchHorse()
        {
            if (Settings.GetSetting<bool>("mc-microscopic-horse"))
            {
                if (ModCompat.microscopic)
                {
                    Main.log.Log("Patching various animal selections to include Nightmare horse (Microscopic)");
                    string nightmare = "4bfe297b1d2c47d18aa770ec5c132194";
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
                        .AddToAllFeatures(nightmare )
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
                else
                {
                    Main.log.Log("Nightmare patch (Microscopic) is enabled but Microscopic isn't detected, skipping patch");
                }
            }
        }
    }
}
