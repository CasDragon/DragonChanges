﻿using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using DragonChanges.New_Archetypes.Swordmaster_Tengu.Features;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;

namespace DragonChanges.New_Archetypes.Swordmaster_Tengu
{
    internal class Swordmaster_Tengu
    {
        // edit
        internal static string archetypeprefix = "swordmaster-tengu";
        internal static string archetypeguid = Guids.tenguSwordmasterArchetype;

        internal static string featureselectionguid = Guids.TraceSelection;

        internal static string abilityresourceguid = Guids.TranceResource;

        // don't edit
        internal static string archetypename = $"{archetypeprefix}.archetype.name";
        internal static string archetypedescription = $"{archetypeprefix}.archetype.description";
        internal static string archetypeshortdescription = $"{archetypeprefix}.archetype.shortdescription";

        internal static string featureselectionn = "TranceSelection";
        internal static string featureselectionname = $"{featureselectionn}.name";
        internal static string featureselectiondescription = $"{featureselectionn}.description";

        internal static BlueprintAbilityResource abilityResource;
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("swordmastertengu"))
            {
                Main.log.Log($"{archetypeprefix} archetype enabled, configuring started");
                abilityResource = ConfigureAbilityResource();
                BlueprintFeatureSelection featureSelection = ConfigureTraceFeatureSelection();
                ConfigureArchetype(featureSelection);
                Main.log.Log($"{archetypeprefix} configuration done!");
                return;
            }
            else
            {
                Main.log.Log($"{archetypeprefix} archetype disabled, configuring dummies");
                ConfigureTraceFeatureSelectionDummy();
                ConfigureAbilityResourceDummy();
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            ArchetypeConfigurator.New(archetypeprefix, archetypeguid).Configure();
        }
        public static void ConfigureArchetype(BlueprintFeatureSelection featureSelection)
        {
            var removef = LevelEntryBuilder.New()
                .AddEntry(3, FeatureRefs.DangerSenseRogue.Reference.Get())
                .AddEntry(6, FeatureRefs.DangerSenseRogue.Reference.Get())
                .AddEntry(9, FeatureRefs.DangerSenseRogue.Reference.Get())
                .AddEntry(12, FeatureRefs.DangerSenseRogue.Reference.Get())
                .AddEntry(15, FeatureRefs.DangerSenseRogue.Reference.Get())
                .AddEntry(18, FeatureRefs.DangerSenseRogue.Reference.Get())
                .GetEntries();
            var addf = LevelEntryBuilder.New()
                .AddEntry(3, featureSelection)
                .AddEntry(6, featureSelection)
                .AddEntry(9, featureSelection)
                .AddEntry(12, featureSelection)
                .AddEntry(15, featureSelection)
                .AddEntry(18, featureSelection)
                .GetEntries();
            ArchetypeConfigurator.New(archetypeprefix, archetypeguid)
                .SetLocalizedName(archetypename)
                .SetLocalizedDescription(archetypedescription)
                .SetLocalizedDescriptionShort(archetypeshortdescription)
                .AddToRemoveFeatures(removef)
                .AddToAddFeatures(addf)
                .AddToRecommendedAttributes(Kingmaker.EntitySystem.Stats.StatType.Wisdom)
                .SetClass(CharacterClassRefs.RogueClass)
                .Configure();
        }
        public static BlueprintFeatureSelection ConfigureTraceFeatureSelectionDummy()
        {
            CraneTrace.ConfigureFeatureDummy();
            DragonTrance.ConfigureFeatureDummy();
            LeopardTrance.ConfigureFeatureDummy();
            MonkeyTrance.ConfigureFeatureDummy();
            SerpentTrance.ConfigureFeatureDummy();
            TigerTrance.ConfigureFeatureDummy();
            return FeatureSelectionConfigurator.New(featureselectionn, featureselectionguid)
                    .Configure();
        }
        public static BlueprintFeatureSelection ConfigureTraceFeatureSelection()
        {
            return FeatureSelectionConfigurator.New(featureselectionn, featureselectionguid)
                .SetDisplayName(featureselectionname)
                .SetDescription(featureselectiondescription)
                .AddToAllFeatures([CraneTrace.ConfigureFeature(),
                            DragonTrance.ConfigureFeature(),
                            LeopardTrance.ConfigureFeature(),
                            MonkeyTrance.ConfigureFeature(),
                            SerpentTrance.ConfigureFeature(),
                            TigerTrance.ConfigureFeature()])
                .SetRanks(6)
                .Configure();
        }
        public static BlueprintAbilityResource ConfigureAbilityResourceDummy()
        {
            return AbilityResourceConfigurator.New($"{archetypeprefix}.resource", abilityresourceguid)
                .Configure();
        }
        public static BlueprintAbilityResource ConfigureAbilityResource()
        {
            return AbilityResourceConfigurator.New($"{archetypeprefix}.resource", abilityresourceguid)
                .SetMaxAmount(ResourceAmountBuilder.New(4).IncreaseByStat(Kingmaker.EntitySystem.Stats.StatType.Wisdom).IncreaseByLevelStartPlusDivStep(startingLevel: 4))
                .SetUseMax(false)
                .SetMax(10)
                .Configure();
        }
    }
}
