using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Assets;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.ResourceLinks;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class UndeadMount
    {
        internal static string UndeadUnit = "undeadmountunit";
        internal static string UndeadFeatureName = "undeadmountfeature.name";
        internal static string UndeadFeatureDescription = "undeadmountfeature.description";
        internal static string UndeadFeature = "undeadmountfeature";
        internal static string UndeadMountPortrait = "undeadmountportrait";

        public static void Configure()
        {

            if (Settings.GetSetting<bool>("undeadmount"))
            {
                Main.log.Log("Configuring undead mount");
                BlueprintUnit unit = CreateUndeadMount();
                BlueprintFeature feature = CreateUndeadMountFeature(unit);
                AddUndeadMountToSelections(feature);
            }
            else
            {
                Main.log.Log("Undead mount feature disabling, configuring dummies");
                FeatureConfigurator.New(UndeadFeature, Guids.UndeadMountFeature)
                    .Configure();
                UnitConfigurator.New(UndeadUnit, Guids.UndeadMountUnit)
                    .Configure();
            }
        }
        public static BlueprintFeature CreateUndeadMountFeature(BlueprintUnit undeadmountunit)
        {
            Main.log.Log("Creating undead mount feature");
            return FeatureConfigurator.New(UndeadFeature, Guids.UndeadMountFeature)
                .AddPet(pet: undeadmountunit,
                        type: Kingmaker.Enums.PetType.AnimalCompanion,
                        progressionType: Kingmaker.Enums.PetProgressionType.AnimalCompanion,
                        levelRank: FeatureRefs.AnimalCompanionRank.Reference.Get(),
                        upgradeFeature: FeatureRefs.AnimalCompanionUpgradeHorse.Reference.Get(),
                        upgradeLevel: 4)
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetDisplayName(UndeadFeatureName)
                .SetDescription(UndeadFeatureDescription)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .SetIcon("assets/icon/undeadhorse.png")
                .AddFeatureToPet(FeatureRefs.UndeadType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateUndeadMount()
        {
            Main.log.Log("Creating undead mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            //BlueprintPortrait portrait = CreateUndeadMountPortrait();
            return UnitConfigurator.New(UndeadUnit, Guids.UndeadMountUnit)
                .CopyFrom(oghorse)
                .SetPrefab("9b94fcd181ed5304e867b02c4faca9b8")
            //    .SetPortrait(portrait)
                .Configure();
        }
        public static BlueprintPortrait CreateUndeadMountPortrait()
        {
            PortraitData data = new PortraitData();
            // todo: fix this mess
            //data.m_FullLengthImage = "assets/portraits/undeadhorse/fulllength.png";
            //data.m_PetEyeImage = "assets/portraits/undeadhorse/eye.png";
            //data.m_HalfLengthImage = "assets/portraits/undeadhorse/medium.png";
            //data.m_PortraitImage = "assets/portraits/undeadhorse/small.png";
            return PortraitConfigurator.New(UndeadMountPortrait, Guids.UndeadMountPortrait)
                .SetData(data)
                .Configure();
        }
        public static void AddUndeadMountToSelections(BlueprintFeature mountfeature)
        {
            if (Settings.GetSetting<bool>("undeadmount"))
            {
                Main.log.Log("Patching various animal selections to include undead mount");
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
                FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                    .AddToAllFeatures(mountfeature)
                    .Configure();
            }
        }
    }
}
