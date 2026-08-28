using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace DragonChanges.Utils;

public class PetUtils
{
    public static void AddPetToAll(BlueprintFeature pet)
    {
        AddPetToBaseGameSelection(pet);
        AddPetToTTTSelection(pet);
        AddPetToMCESelection(pet);
    }

    public static void AddPetToTTTSelection(BlueprintFeature pet)
    {
        if (!ModCompat.tttbase) return;
        FeatureSelectionConfigurator.For(TTTBaseRefs.DivineCommanderCompanionSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(TTTBaseRefs.AnimalAllyFeatureSelection)
            .AddToAllFeatures(pet)
            .Configure();
    }

    public static void AddPetToMCESelection(BlueprintFeature pet)
    {
        if (!ModCompat.microscopic) return;
        FeatureSelectionConfigurator.For(MicroscopicContentExpansionRefs.AntipaladinServantSelection)
            .AddToAllFeatures(pet)
            .Configure();
    }
    public static void AddPetToBaseGameSelection(BlueprintFeature pet)
    {
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
            .AddToAllFeatures(pet)
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
            .AddToAllFeatures(pet)
            .Configure();
    }
}