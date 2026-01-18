using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Modding;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;
using Owlcat.Runtime.Core.Utils;
using System.Linq;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class GriffonMount
    {
        internal const string GriffonUnit = "GriffonMount";
        internal const string GriffonFeature = "GriffonMount-feature";
        internal const string GriffonMountPortrait = "griffonmountportrait";
        internal const string settingName = "griffonmount";
        internal const string settingDescription = "Adds a new griffon mount, and then adds it to mount selections";
        internal const string featurename = "Animal Companion - Griffon";
        internal const string featuredescription = "MOUNTING IS VISUALLY BROKEN\nTODO";
        //
        [DragonLocalizedString(GriffonFeatureName, featurename)]
        internal const string GriffonFeatureName = "griffonmountfeature.name";
        [DragonLocalizedString(GriffonFeatureDescription, featuredescription, true)]
        internal const string GriffonFeatureDescription = "griffonmountfeature.description";

        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
            var x = new ContextValue();
            x.ValueType = ContextValueType.Simple;
            x.Value = 0;
            x.ValueRank = Kingmaker.Enums.AbilityRankType.Default;
            x.Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None;
            x.m_AbilityParameter = AbilityParameterType.Level;
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
            Polymorph phorse = BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>();
            return UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                .CopyFrom(oghorse, typeof(AddClassLevels), typeof(CMDBonusAgainstManeuvers))
                .SetPrefab(phorse.m_Prefab.AssetId)
                .SetType(UnitTypeRefs.EagleGiant.Reference.Get())
                .SetPortrait(phorse.m_Portrait)
                .AddSecondaryAttacks(ItemWeaponRefs.Talon1d4.Reference.Get(), ItemWeaponRefs.Talon1d4.Reference.Get())
                .SetStrength(10)
                .SetDexterity(17)
                .SetConstitution(14)
                .SetIntelligence(5)
                .SetWisdom(13)
                .SetCharisma(8)
                .AddClassSkill(StatType.SkillAthletics)
                .AddClassSkill(StatType.SkillPerception)
                .SetSpeed(new Feet(50))
                .AddAllowDyingCondition()
                .AddResurrectOnRest()
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.MainHand)
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.OffHand)
                .AddFacts(facts: [FeatureRefs.HeadLocatorFeature.Reference.Get()])
                .SetGender(Gender.Male)
                .SetSize(Kingmaker.Enums.Size.Large)
                .SetIsLeftHanded(false)
                .SetAlignment(Kingmaker.Enums.Alignment.TrueNeutral)
                .SetFaction(FactionRefs.Neutrals.Reference.Get())
                .SetBrain("cf986dd7ba9d4ec46ad8a3a0406d02ae")
                .SetBaseAttackBonus(0)
                .SetMaxHP(0)
                .SetIsCheater(false)
                .SetIsFake(false)
                .SetSkills(oghorse.Skills)
                .SetAddFacts([UnitFactRefs.NaturalArmor4.Reference.Get(),
                                UnitFactRefs.ReducedReach.Reference.Get(),
                                FeatureRefs.TripDefenseFourLegs.Reference.Get(),
                                FeatureRefs.AnimalCompanionSlotFeature.Reference.Get(),
                                FeatureRefs.AnimalType.Reference.Get(),
                                FeatureRefs.AnimalCompanionNotUpgradedHorse.Reference.Get(),
                                FeatureRefs.AnimalCompanionScent30.Reference.Get()])
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 1.0f))
                .SetVisual(oghorse.Visual)
                .SetBody(oghorse.Body)
                .SetFactionOverrides(new FactionOverrides())
                .SetAlternativeBrains()
                .SetAdditionalTemplates()
                .Configure();
        }
        public static void AddGriffonMountToSelections(BlueprintFeature mountfeature)
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
            internal static string griffonprefab = BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Prefab.AssetId;

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
                var horse = ResourcesLibrary.TryGetResource<UnitEntityView>(UnitRefs.SableMarineAnimalCompanionHippogriff.Reference.Get().Prefab.AssetId);
                var horseobj = UnityEngine.Object.Instantiate(horse.GetComponent<MountOffsets>());
                //offsets.OffsetsConfig = horseobj.OffsetsConfig;
                offsets.MediumOffsetsConfig = horseobj.MediumOffsetsConfig;
                offsets.LargeOffsetsConfig = horseobj.LargeOffsetsConfig;
                offsets.MediumOffsetsConfig = horseobj.MediumOffsetsConfig;
            }
        }
    }
}
