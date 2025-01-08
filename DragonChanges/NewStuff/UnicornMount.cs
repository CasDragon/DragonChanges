using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Kingmaker.Modding;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;

namespace DragonChanges.NewStuff
{
    internal class UnicornMount
    {
        internal static string UnicornUnit = "unicornmountunit";
        internal static string UnicornFeatureName = "unicornmountfeature.name";
        internal static string UnicornFeatureDescription = "unicornmountfeature.description";
        internal static string UnicornFeature = "unicornmountfeature";
        internal static string UnicornMountPortrait = "unicornmountportrait";

        public static void Configure()
        {

            if (Settings.GetSetting<bool>("unicornmount"))
            {
                Main.log.Log("Configuring unicorn mount");
                BlueprintUnit unit = CreateUnicornMount();
                BlueprintFeature feature = CreateUnicornMountFeature(unit);
                AddUnicornMountToSelections(feature);
            }
            else
            {
                Main.log.Log("Unicorn mount feature disabling, configuring dummies");
                FeatureConfigurator.New(UnicornFeature, Guids.UnicornMountFeature)
                    .Configure();
                UnitConfigurator.New(UnicornUnit, Guids.UnicornMountUnit)
                    .Configure();
            }
        }
        public static BlueprintFeature CreateUnicornMountFeature(BlueprintUnit unicornmountunit)
        {
            Main.log.Log("Creating unicorn mount feature");
            return FeatureConfigurator.New(UnicornFeature, Guids.UnicornMountFeature)
                .AddPet(pet: unicornmountunit,
                        type: Kingmaker.Enums.PetType.AnimalCompanion,
                        progressionType: Kingmaker.Enums.PetProgressionType.AnimalCompanion,
                        levelRank: FeatureRefs.AnimalCompanionRank.Reference.Get(),
                        upgradeFeature: FeatureRefs.AnimalCompanionUpgradeHorse.Reference.Get(),
                        upgradeLevel: 4)
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetDisplayName(UnicornFeatureName)
                .SetDescription(UnicornFeatureDescription)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .SetIcon(null)
                //.SetIcon("assets/icons/unicornhorse.png")
                .AddFeatureToPet(FeatureRefs.MagicalBeastType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateUnicornMount()
        {
            Main.log.Log("Creating unicorn mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            //BlueprintPortrait portrait = CreateUnicornMountPortrait();
            return UnitConfigurator.New(UnicornUnit, Guids.UnicornMountUnit)
                .CopyFrom(oghorse)
                .SetPrefab(UnitRefs.CR3_UnicornStandard.Reference.Get().Prefab)
                .SetType(UnitTypeRefs.Unicorn.Reference.Get())
                .SetPortrait(UnitRefs.CR3_UnicornStandard.Reference.Get().m_Portrait)
            //    .SetPortrait(portrait)
                .Configure();
        }
        public static BlueprintPortrait CreateUnicornMountPortrait()
        {
            PortraitData data = new PortraitData();
            // todo: fix this mess
            //data.m_FullLengthImage = "assets/portraits/unicornhorse/fulllength.png";
            //data.m_PetEyeImage = "assets/portraits/unicornhorse/eye.png";
            //data.m_HalfLengthImage = "assets/portraits/unicornhorse/medium.png";
            //data.m_PortraitImage = "assets/portraits/unicornhorse/small.png";
            return PortraitConfigurator.New(UnicornMountPortrait, Guids.UnicornMountPortrait)
                .SetData(data)
                .Configure();
        }
        public static void AddUnicornMountToSelections(BlueprintFeature mountfeature)
        {
            if (Settings.GetSetting<bool>("unicornmount"))
            {
                Main.log.Log("Patching various animal selections to include unicorn mount");
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

        //[HarmonyPatch(typeof(OwlcatModificationsManager))]
        [HarmonyPatch(typeof(OwlcatModificationsManager), nameof(OwlcatModificationsManager.OnResourceLoaded))]
        public static class PatchUnicornOnLoad
        {
            /*[HarmonyPostfix]
            [HarmonyPatch(nameof(OwlcatModificationsManager.OnResourceLoaded))]
            public static void OnResourceLoaded_UnicornPatch(object resource, string guid)*/
            [HarmonyPrefix]
            public static void Prefix(object resource, string guid)
            {
                if (guid != "2f17956095cd04b49a8f4c0bc88bb9ca")
                    return;
                Main.log.Log("Found unicorn prefab");
                if (resource is UnitEntityView view)
                {
                    Main.log.Log("Start patching unicorn prefab for mounting");
                    PatchUnicornAsset(view);
                    Main.log.Log("Finised patching unicorn prefab");
                }
            }
            public static void PatchUnicornAsset(UnitEntityView view)
            {
                MountOffsets offsets = view.gameObject.AddComponent<MountOffsets>();
                var horse = ResourcesLibrary.TryGetResource<UnitEntityView>("2f17956095cd04b49a8f4c0bc88bb9ca");
                if (horse is UnitEntityView)
                {
                    offsets = UnityEngine.Object.Instantiate(horse.GetComponent<MountOffsets>(), offsets.transform);
                }
                else
                {
                    Main.log.Log("Couldn't find horse prefab to steal from");
                }
            }
        }
    }
}
