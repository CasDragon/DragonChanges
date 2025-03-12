using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using HarmonyLib;

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
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                    FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                        .AddToAllFeatures("4bfe297b1d2c47d18aa770ec5c132194")
                        .Configure();
                }
                else
                {
                    Main.log.Log("Nightmare patch (Microscopic) is enabled but Microscopic isn't detected, skipping patch");
                }
            }
        }
    }
}
