using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Assets;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Modding;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.SettingsUI;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;
using Owlcat.Runtime.Core.Utils;
using System.Linq;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class UnicornMount
    {
        internal const string UnicornUnit = "UnicornMount";
        internal const string UnicornFeature = "UnicornMount-feature";
        internal const string UnicornMountPortrait = "unicornmountportrait";
        internal const string settingName = "unicornmount";
        internal const string settingDescription = "Adds a new unicorn mount, and then adds it to mount selections";
        internal const string featurename = "Animal Companion - Unicorn";
        internal const string featuredescription = "{g|Encyclopedia:Size}Size{/g}: Large\n{g|Encyclopedia:Speed}Speed{/g}: 50 ft.\n{g|Encyclopedia:Armor_Class}AC{/g}: +4 natural armor\n{g|Encyclopedia:Attack}Attacks{/g}: bite ({g|Encyclopedia:Dice}1d4{/g}), 2 hooves (1d6)\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 16, {g|Encyclopedia:Dexterity}Dex{/g} 13, {g|Encyclopedia:Constitution}Con{/g} 15, {g|Encyclopedia:Intelligence}Int{/g} 2, {g|Encyclopedia:Wisdom}Wis{/g} 12, {g|Encyclopedia:Charisma}Cha{/g} 6\nSpecial qualities: {g|Encyclopedia:Scent}scent{/g}\nAt 4th level, a horse gains Str +2 and Con +2 and its hoof attacks become primary.\nWhen riding a horse, you gain a +1 {g|Encyclopedia:Bonus}bonus{/g} to AC and on attack rolls against enemies of Medium size or smaller.";
        //
        [DragonLocalizedString(UnicornFeatureName, featurename)]
        internal const string UnicornFeatureName = "unicornmountfeature.name"; 
        [DragonLocalizedString(UnicornFeatureDescription, featuredescription, true)]
        internal const string UnicornFeatureDescription = "unicornmountfeature.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
            var x = new ContextValue();
            x.ValueType = ContextValueType.Simple;
            x.Value = 0;
            x.ValueRank = Kingmaker.Enums.AbilityRankType.Default;
            x.Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None;
            x.m_AbilityParameter = AbilityParameterType.Level;
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
                .AddFeatureToPet(FeatureRefs.MagicalBeastType.Reference.Get())
                .Configure();
        }
        public static BlueprintUnit CreateUnicornMount()
        {
            Main.log.Log("Creating unicorn mount unit");
            BlueprintUnit oghorse = UnitRefs.AnimalCompanionUnitHorse.Reference.Get();
            BlueprintUnit phorse = UnitRefs.CR3_UnicornStandard.Reference.Get();
            //BlueprintPortrait portrait = CreateUndeadMountPortrait();
            return UnitConfigurator.New(UnicornUnit, Guids.UnicornMountUnit)
                .CopyFrom(oghorse, typeof(AddClassLevels), typeof(CMDBonusAgainstManeuvers))
                .AddAllowDyingCondition()
                .AddResurrectOnRest()
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.MainHand)
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.OffHand)
                .AddFacts(facts: [FeatureRefs.HeadLocatorFeature.Reference.Get()])
                .SetType(phorse.Type)
                .SetGender(Gender.Male)
                .SetSize(Kingmaker.Enums.Size.Large)
                .SetIsLeftHanded(false)
                .SetAlignment(Kingmaker.Enums.Alignment.TrueNeutral)
                .SetFaction(FactionRefs.Neutrals.Reference.Get())
                .SetBrain("cf986dd7ba9d4ec46ad8a3a0406d02ae")
                .SetPrefab(phorse.Prefab.AssetId)
                .SetStrength(16)
                .SetDexterity(13)
                .SetIntelligence(2)
                .SetWisdom(10)
                .SetCharisma(15)
                .SetConstitution(6)
                .SetSpeed(new Kingmaker.Utility.Feet(50))
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
                .SetFactionOverrides(new FactionOverrides())
                .SetAlternativeBrains()
                .SetAdditionalTemplates()
                .Configure();
        }
        public static void AddUnicornMountToSelections(BlueprintFeature mountfeature)
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
            internal static string unicornprefab = UnitRefs.CR3_UnicornStandard.Reference.Get().Prefab.AssetId;

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
                //offsets = horseobj;
                //offsets.OffsetsConfig = horseobj.OffsetsConfig;
                offsets.MediumOffsetsConfig = horseobj.MediumOffsetsConfig;
                offsets.LargeOffsetsConfig = horseobj.LargeOffsetsConfig;
                offsets.MediumOffsetsConfig = horseobj.MediumOffsetsConfig;
            }
        }
    }
}
