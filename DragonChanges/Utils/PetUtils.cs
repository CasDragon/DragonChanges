using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;

namespace DragonChanges.Utils;

public static class PetUtils
{
    private static readonly string[] BaseGameSelections =
    [ 
        FeatureSelectionRefs.AnimalCompanionSelectionBase.ToString(), FeatureSelectionRefs.AnimalCompanionSelectionDivineHound.ToString(),
        FeatureSelectionRefs.AnimalCompanionSelectionDomain.ToString(), FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist.ToString(),
        FeatureSelectionRefs.AnimalCompanionSelectionDruid.ToString(), FeatureSelectionRefs.AnimalCompanionSelectionHunter.ToString(),
        FeatureSelectionRefs.AnimalCompanionSelectionMadDog.ToString(), FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid.ToString(),
        FeatureSelectionRefs.AnimalCompanionSelectionRanger.ToString(), FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster.ToString(),
        FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer.ToString(), FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter.ToString(),
        FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman.ToString(), FeatureSelectionRefs.CavalierMountSelection.ToString(),
        FeatureSelectionRefs.BeastRiderMountSelection.ToString(), FeatureSelectionRefs.ArcaneRiderMountSelection.ToString(),
        FeatureSelectionRefs.BloodriderMountSelection.ToString(), FeatureSelectionRefs.GhostRiderGhostMountSelection.ToString(),
        FeatureSelectionRefs.NomadMountSelection.ToString(), FeatureSelectionRefs.OrderOfThePawMountSelection.ToString(),
        FeatureSelectionRefs.PaladinDivineMountSelection.ToString(), FeatureSelectionRefs.SoheiMonasticMountHorseSelection.ToString(),
        FeatureSelectionRefs.OracleRevelationBondedMount.ToString(), FeatureSelectionRefs.SableMarineHippogriffCompanionSelection.ToString()
    ];
    private static readonly string[] TTTSelections =
    [
        TTTBaseRefs.DivineCommanderCompanionSelection,
        TTTBaseRefs.AnimalAllyFeatureSelection
    ];
    
    public static void AddPetToAll(BlueprintFeature pet, string[] guidExceptions)
    {
        AddPetToBaseGameSelection(pet, guidExceptions);
        AddPetToTTTSelection(pet, guidExceptions);
        AddPetToMCESelection(pet, guidExceptions);
    }
    public static void AddPetToAll(BlueprintFeature pet)
    {
        AddPetToAll(pet, []);
    }

    public static void AddPetToTTTSelection(BlueprintFeature pet)
    {
        AddPetToTTTSelection(pet, []);
    }
    public static void AddPetToTTTSelection(BlueprintFeature pet, string[] guidExceptions)
    {
        if (!ModCompat.tttbase) return;
        foreach (var selection in TTTSelections)
        {
            if (guidExceptions.Any(s => s == selection)) continue;
            FeatureSelectionConfigurator.For(selection)
                .AddToAllFeatures(pet)
                .Configure();
        }
    }

    public static void AddPetToMCESelection(BlueprintFeature pet)
    {
        if (!ModCompat.microscopic) return;
        FeatureSelectionConfigurator.For(MicroscopicContentExpansionRefs.AntipaladinServantSelection)
            .AddToAllFeatures(pet)
            .Configure();
    }
    public static void AddPetToMCESelection(BlueprintFeature pet, string[] guidExceptions)
    {
        if (!ModCompat.microscopic) return;
        if (guidExceptions.Any(s => s == MicroscopicContentExpansionRefs.AntipaladinServantSelection)) return;
        FeatureSelectionConfigurator.For(MicroscopicContentExpansionRefs.AntipaladinServantSelection)
            .AddToAllFeatures(pet)
            .Configure();
    }
    public static void AddPetToBaseGameSelection(BlueprintFeature pet)
    {
        AddPetToBaseGameSelection(pet, []);
    }
    public static void AddPetToBaseGameSelection(BlueprintFeature pet, string[] guidExceptions)
    {
        foreach (var selection in BaseGameSelections)
        {
            if (guidExceptions.Any(s => s == selection)) return;
            FeatureSelectionConfigurator.For(selection)
                .AddToAllFeatures(pet)
                .Configure();
        }
    }
}