using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Modding;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.Utility;
using Kingmaker.Blueprints.Root;
using Owlcat.Runtime.Core.Utils;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class GriffonMount
    {
        internal static string GriffonUnit = "GriffonMount";
        internal static string GriffonFeatureName = "griffonmountfeature.name";
        internal static string GriffonFeatureDescription = "griffonmountfeature.description";
        internal static string GriffonFeature = "griffonmountfeature";
        internal static string GriffonMountPortrait = "griffonmountportrait";
        internal static string griffonprefab = BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Prefab.AssetId;

        public static void Configure()
        {

            if (Settings.GetSetting<bool>("griffonmount"))
            {
                Main.log.Log("Configuring griffon mount");
                BlueprintUnit unit = CreateGriffonMount();
                BlueprintFeature feature = CreateGriffonMountFeature(unit);
                AddGriffonMountToSelections(feature);
            }
            else
            {
                Main.log.Log("Griffon mount feature disabling, configuring dummies");
                FeatureConfigurator.New(GriffonFeature, Guids.GriffonMountFeature)
                    .Configure();
                UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                    .Configure();
            }
        }
        public static BlueprintFeature CreateGriffonMountFeature(BlueprintUnit griffonmountunit)
        {
            Main.log.Log("Creating griffon mount feature");
            var x = new Kingmaker.UnitLogic.Mechanics.ContextValue();
            x.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Simple;
            x.Value = 0;
            x.ValueRank = Kingmaker.Enums.AbilityRankType.Default;
            x.Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None;
            x.m_AbilityParameter = Kingmaker.UnitLogic.Mechanics.AbilityParameterType.Level;
            return FeatureConfigurator.New(GriffonFeature, Guids.GriffonMountFeature)
                .AddPet(pet: griffonmountunit,
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
                .SetDisplayName(GriffonFeatureName)
                .SetDescription(GriffonFeatureDescription)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                //.SetIcon("assets/icons/griffonmount.png")
                .AddFeatureToPet(FeatureRefs.MagicalBeastType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateGriffonMount()
        {
            Main.log.Log("Creating griffon mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            return UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                .CopyFrom(oghorse)
                .SetPrefab(griffonprefab)
                .SetType(UnitTypeRefs.EagleGiant.Reference.Get())
                .SetPortrait(BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Portrait)
                .AddSecondaryAttacks(ItemWeaponRefs.Talon1d4.Reference.Get(), ItemWeaponRefs.Talon1d4.Reference.Get())
                .SetStrength(10)
                .SetDexterity(17)
                .SetConstitution(14)
                .SetIntelligence(5)
                .SetWisdom(13)
                .SetCharisma(8)
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillAthletics)
                .AddClassSkill(Kingmaker.EntitySystem.Stats.StatType.SkillPerception)
                .SetSpeed(new Feet(50))
                .Configure();
        }
        public static void AddGriffonMountToSelections(BlueprintFeature mountfeature)
        {
            if (Settings.GetSetting<bool>("griffonmount"))
            {
                Main.log.Log("Patching various animal selections to include griffon mount");
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
                if (guid != griffonprefab)
                    return;
                if (resource is UnitEntityView view)
                {
                    Main.log.Log("Start patching griffon prefab for mounting");
                    PatchUnicornAsset(view);
                    Main.log.Log("Finised patching griffon prefab");
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

                offsets.Root = view.Pelvis.FindChildRecursive("Chest_M");
                offsets.RootBattle = view.Pelvis.FindChildRecursive("Chest_M");
                offsets.Root.position = new Vector3(0.238000005f, -0.31400007f, 0.0269999616f);
                offsets.RootBattle.position = new Vector3(0.238000005f, -0.31400007f, 0.0269999616f);

                offsets.PelvisIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Chest_M"),
                    "Pelvis",
                    new Vector3(-0.003f, 0.016f, 0.001f));
                    //new Vector3(0.7602f, 180f, 0f));
                offsets.LeftFootIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Chest_M"),
                    "LeftFoot",
                    new Vector3(-0.194f, 0.28f, 0.394f),
                    new Vector3(0.0351804f, -0.1939332f, 0.9660344f));
                offsets.RightFootIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Chest_M"),
                    "RightFoot",
                    new Vector3(0.177f, 0.335f, -0.323f),
                    new Vector3(0.04588f, -0.35238f, -0.14885775f));
                offsets.LeftKneeIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Chest_M"),
                    "LeftKnee",
                    new Vector3(-0.243f, 0.066f, 0.204f));
                    //new Vector3(359.9774f, 0f, 149.1742f));
                offsets.RightKneeIkTarget = CreateMountBone(view.Pelvis.FindChildRecursive("Chest_M"),
                    "RightKnee",
                    new Vector3(-0.011f, 0.181f, -0.359f));
                    //new Vector3(359.9774f, 0f, 337.0312f));

                offsets.Hands = CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Head_01"),
                    "Hands",
                    new Vector3(-0.265f, -0.24f, -0.103f));
                    //new Vector3(359.9774f, 0f, 337.0312f));


                var offsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
                offsetConfig.name = "Griffon_MountConfig";

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
                /*var horse = ResourcesLibrary.TryGetResource<UnitEntityView>(UnitRefs.AnimalCompanionUnitHorse_Large.Reference.Get().Prefab.AssetId);
                var horseobj = UnityEngine.Object.Instantiate(horse.GetComponent<MountOffsets>());
                offsets.LargeOffsetsConfig = horseobj.LargeOffsetsConfig;
                offsets.MediumOffsetsConfig = horseobj.MediumOffsetsConfig;*/
            }
        }
    }
}
