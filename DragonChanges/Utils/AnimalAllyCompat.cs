using System;
using System.Linq;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace DragonChanges.Utils
{
    internal static class AnimalAllyCompat
    {
        internal const string TTTAnimalAllySelection = "d9b99d9c48d2425894b565733e96c7e3";
        internal const string TTTAnimalAllyPetSelection = "ecf97b544d584edb8bb0ba9e7de20751";

        internal static void AddToAnimalAlly(BlueprintFeature feature)
        {
            if (!ModCompat.tttbase || feature == null)
                return;

            FeatureSelectionConfigurator.For(TTTAnimalAllySelection)
                .AddToAllFeatures(feature)
                .Configure();

            FeatureSelectionConfigurator.For(TTTAnimalAllyPetSelection)
                .AddToAllFeatures(feature)
                .Configure();
        }

        internal static void AddToAnimalAlly(string featureGuid)
        {
            if (!ModCompat.tttbase || string.IsNullOrEmpty(featureGuid))
                return;

            FeatureSelectionConfigurator.For(TTTAnimalAllySelection)
                .AddToAllFeatures(featureGuid)
                .Configure();

            FeatureSelectionConfigurator.For(TTTAnimalAllyPetSelection)
                .AddToAllFeatures(featureGuid)
                .Configure();
        }

        internal static BlueprintFeature FindByBlueprintName(
            BlueprintFeatureSelection selection,
            string nameFragment)
        {
            if (selection == null || string.IsNullOrEmpty(nameFragment))
                return null;

            return selection.AllFeatures.FirstOrDefault(
                feature => feature != null
                    && !string.IsNullOrEmpty(feature.name)
                    && feature.name.IndexOf(nameFragment, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
