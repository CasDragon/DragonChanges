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
using Kingmaker.Blueprints.Root;
using Owlcat.Runtime.Core.Utils;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class UnicornMount
    {
        internal static string UnicornUnit = "UnicornMount";
        internal static string UnicornFeatureName = "unicornmountfeature.name";
        internal static string UnicornFeatureDescription = "unicornmountfeature.description";
        internal static string UnicornFeature = "unicornmountfeature";
        internal static string UnicornMountPortrait = "unicornmountportrait";
        readonly static string unicornprefab = UnitRefs.CR3_UnicornStandard.Reference.Get().Prefab.AssetId;

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
            var x = new Kingmaker.UnitLogic.Mechanics.ContextValue();
            x.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple;
            x.Value = 0;
            x.ValueRank = Kingmaker.Enums.AbilityRankType.Default;
            x.Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None;
            x.m_AbilityParameter = Kingmaker.UnitLogic.Mechanics.AbilityParameterType.Level;
            return FeatureConfigurator.New(UnicornFeature, Guids.UnicornMountFeature)
                .AddPet(pet: unicornmountunit,
                        type: Kingmaker.Enums.PetType.AnimalCompanion,
                        progressionType: Kingmaker.Enums.PetProgressionType.AnimalCompanion,
                        levelRank: FeatureRefs.AnimalCompanionRank.Reference.Get(),
                        upgradeFeature: FeatureRefs.AnimalCompanionUpgradeHorse.Reference.Get(),
                        upgradeLevel: 4,
                        useContextValueLevel: false,
                        forceAutoLevelup: false,
                        destroyPetOnDeactivate: false,
                        levelContextValue: x)
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetDisplayName(UnicornFeatureName)
                .SetDescription(UnicornFeatureDescription)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                //.SetIcon("assets/icons/unicornmount.png")
                .AddFeatureToPet(FeatureRefs.MagicalBeastType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateUnicornMount()
        {
            Main.log.Log("Creating unicorn mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            return UnitConfigurator.New(UnicornUnit, Guids.UnicornMountUnit)
                .CopyFrom(oghorse)
                .SetPrefab(UnitRefs.CR3_UnicornStandard.Reference.Get().Prefab)
                .SetType(UnitTypeRefs.Unicorn.Reference.Get())
                .SetPortrait(UnitRefs.CR3_UnicornStandard.Reference.Get().m_Portrait)
                .AddAdditionalLimb(ItemWeaponRefs.GoreLarge1d8.Reference.Get())
                .SetIntelligence(11)
                .SetCharisma(18)
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

        [HarmonyPatch(typeof(OwlcatModificationsManager))]
        public static class PatchUnicornOnLoad
        {
            [HarmonyPostfix]
            [HarmonyPatch(nameof(OwlcatModificationsManager.OnResourceLoaded))]
            public static void OnResourceLoaded_UnicornPatch(object resource, string guid)
            {
                if (guid != unicornprefab)
                    return;
                if (resource is UnitEntityView view)
                {
                    Main.log.Log("Start patching unicorn prefab for mounting");
                    PatchUnicornAsset(view);
                    Main.log.Log("Finised patching unicorn prefab");
                }
            }
            public static Transform CreateMountBone(Transform parent, string type, Vector3 posOffset, Vector3? rotOffset = null)
            {
                var offsetBone = new GameObject($"Saddle_{type}_parent");
                offsetBone.transform.SetParent(parent);
                offsetBone.transform.localPosition = posOffset;
                if (rotOffset.HasValue)
                    offsetBone.transform.localEulerAngles = rotOffset.Value;

                var target = new GameObject($"Saddle_{type}");
                target.transform.SetParent(offsetBone.transform);

                return target.transform;
            }

            public static void PatchUnicornAsset(UnitEntityView view)
            {
                var offsets = view.gameObject.AddComponent<MountOffsets>();

                offsets.Root = view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02");
                offsets.RootBattle = view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02");

                offsets.PelvisIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02"),
                    "Pelvis",
                    new Vector3(0f, 0.361f, 0.065f),
                    new Vector3(0.7602f, 180f, 0f));
                offsets.LeftFootIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02"),
                    "LeftFoot",
                    new Vector3(0.425f, -0.3506f, -0.1074f),
                    new Vector3(334.9193f, 94.9215f, 322.0144f));
                offsets.RightFootIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02"),
                    "RightFoot",
                    new Vector3(-0.425f, -0.3506f, -0.1074f),
                    new Vector3(11.3555f, 92.1214f, 144.7181f));
                offsets.LeftKneeIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02"),
                    "LeftKnee",
                    new Vector3(0.386f, 0.0652f, -0.275f),
                    new Vector3(359.9774f, 0f, 149.1742f));
                offsets.RightKneeIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_02"),
                    "RightKnee",
                    new Vector3(-0.386f, 0.0652f, -0.275f),
                    new Vector3(359.9774f, 0f, 337.0312f));

                offsets.Hands = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Head_00"),
                    "Hands",
                    new Vector3(0f, 0.5108f, -0.5856f),
                    new Vector3(359.9774f, 0f, 337.0312f));


                var offsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
                offsetConfig.name = "Unicorn_MountConfig";

                offsetConfig.offsets = [
                        new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = BlueprintRoot.Instance.Progression.m_CharacterRaces.ToList(),
                    RootPosition = new Vector3(0f, 0f, 0.5f),
                    RootBattlePosition = new Vector3(0f, 0f, 0.5f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = Vector3.zero,
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = Vector3.zero,
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = Vector3.zero,
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0.15f, -0.4f, 1.2f),

                    PelvisPositionWeight = 0.9f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                }
                    ];
                offsets.OffsetsConfig = offsetConfig;
                var horse = ResourcesLibrary.TryGetResource<UnitEntityView>(UnitRefs.AnimalCompanionUnitHorse_Large.Reference.Get().Prefab.AssetId);
                var horseobj = UnityEngine.Object.Instantiate(horse.GetComponent<MountOffsets>());
                offsets.LargeOffsetsConfig = horseobj.LargeOffsetsConfig;
                offsets.MediumOffsetsConfig = horseobj.MediumOffsetsConfig;
            }
        }
    }
}
