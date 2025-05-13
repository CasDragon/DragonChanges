using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
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
        internal const string GriffonFeatureName = "griffonmountfeature.name";
        internal const string GriffonFeatureDescription = "griffonmountfeature.description";
        internal const string GriffonFeature = "GriffonMount-feature";
        internal const string GriffonMountPortrait = "griffonmountportrait";
        internal static string griffonprefab = BuffRefs.ShifterWildShapeGriffonBuff.Reference.Get().GetComponent<Polymorph>().m_Prefab.AssetId;
        internal const string settingName = "griffonmount";
        internal const string settingDescription = "Adds a new griffon mount, and then adds it to mount selections";

        [DragonConfigure]
        [DragonSetting(settingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
            SelectionEntry entry1 = new SelectionEntry();
            entry1.IsParametrizedFeature = false;
            entry1.IsFeatureSelectMythicSpellbook = false;
            entry1.m_Selection = FeatureSelectionRefs.BasicFeatSelection.Reference.Get().ToReference<BlueprintFeatureSelectionReference>();
            entry1.m_Features = [FeatureRefs.Dodge.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.Toughness.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                ParametrizedFeatureRefs.WeaponFocus.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.IronWill.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.CombatReflexes.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.CriticalFocus.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                ParametrizedFeatureRefs.ImprovedCritical.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.GreatFortitude.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.StaggeringCriticalFeature.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.Mobility.Reference.Get().ToReference<BlueprintFeatureReference>()];
            entry1.ParamSpellSchool = Kingmaker.Blueprints.Classes.Spells.SpellSchool.None;
            entry1.ParamWeaponCategory = Kingmaker.Enums.WeaponCategory.UnarmedStrike;
            entry1.Stat = StatType.Unknown;
            SelectionEntry entry2 = new SelectionEntry();
            entry2.IsParametrizedFeature = true;
            entry2.IsFeatureSelectMythicSpellbook = false;
            entry2.m_Selection = FeatureSelectionRefs.BasicFeatSelection.Reference.Get().ToReference<BlueprintFeatureSelectionReference>();
            entry2.m_Features = [FeatureRefs.Dodge.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.Toughness.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                ParametrizedFeatureRefs.WeaponFocus.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.IronWill.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.CombatReflexes.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.CriticalFocus.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                ParametrizedFeatureRefs.ImprovedCritical.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.GreatFortitude.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.PowerAttackFeature.Reference.Get().ToReference<BlueprintFeatureReference>()];
            entry2.m_ParametrizedFeature = ParametrizedFeatureRefs.WeaponFocus.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>();
            entry2.ParamSpellSchool = Kingmaker.Blueprints.Classes.Spells.SpellSchool.None;
            entry2.ParamWeaponCategory = Kingmaker.Enums.WeaponCategory.Hoof;
            entry2.Stat = StatType.Unknown;
            SelectionEntry entry3 = new SelectionEntry();
            entry3.IsParametrizedFeature = true;
            entry3.IsFeatureSelectMythicSpellbook = false;
            entry3.m_Selection = FeatureSelectionRefs.BasicFeatSelection.Reference.Get().ToReference<BlueprintFeatureSelectionReference>();
            entry3.m_Features = [FeatureRefs.Dodge.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.Toughness.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                ParametrizedFeatureRefs.WeaponFocus.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.IronWill.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.CombatReflexes.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.CriticalFocus.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                ParametrizedFeatureRefs.ImprovedCritical.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.GreatFortitude.Reference.Get().ToReference<BlueprintFeatureReference>(),
                                FeatureRefs.PowerAttackFeature.Reference.Get().ToReference<BlueprintFeatureReference>()];
            entry3.m_ParametrizedFeature = ParametrizedFeatureRefs.ImprovedCritical.Reference.Get().ToReference<BlueprintParametrizedFeatureReference>();
            entry3.ParamSpellSchool = Kingmaker.Blueprints.Classes.Spells.SpellSchool.None;
            entry3.ParamWeaponCategory = Kingmaker.Enums.WeaponCategory.UnarmedStrike;
            entry3.Stat = StatType.Unknown;
            ContextValue cmdvalue = new ContextValue();
            cmdvalue.ValueType = ContextValueType.Simple;
            cmdvalue.Value = 4;
            cmdvalue.ValueRank = Kingmaker.Enums.AbilityRankType.Default;
            cmdvalue.ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage;
            cmdvalue.Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None;
            cmdvalue.PropertyName = Kingmaker.Enums.ContextPropertyName.Value1;
            cmdvalue.m_AbilityParameter = AbilityParameterType.Level;
            BlueprintUnit.UnitBody body = new BlueprintUnit.UnitBody();
            body.DisableHands = false;
            body.m_EmptyHandWeapon = ItemWeaponRefs.WeaponEmptyHand.Reference.Get().ToReference<BlueprintItemWeaponReference>();
            body.m_PrimaryHand = ItemWeaponRefs.Bite1d4Large.Reference.Get().ToReference<BlueprintItemEquipmentHandReference>();
            body.m_PrimaryHandAlternative1 = ItemWeaponRefs.Bite1d4Large.Reference.Get().ToReference<BlueprintItemEquipmentHandReference>();
            body.m_PrimaryHandAlternative2 = ItemWeaponRefs.Bite1d4Large.Reference.Get().ToReference<BlueprintItemEquipmentHandReference>();
            body.m_PrimaryHandAlternative3 = ItemWeaponRefs.Bite1d4Large.Reference.Get().ToReference<BlueprintItemEquipmentHandReference>();
            body.ActiveHandSet = 0;
            BlueprintUnit.UnitSkills skills = new BlueprintUnit.UnitSkills();
            skills.Acrobatics = 0;
            skills.Physique = 0;
            skills.Diplomacy = 0;
            skills.Thievery = 0;
            skills.LoreNature = 0;
            skills.Perception = 0;
            skills.Stealth = 0;
            skills.UseMagicDevice = 0;
            skills.LoreReligion = 0;
            skills.KnowledgeArcana = 0;
            return UnitConfigurator.New(GriffonUnit, Guids.GriffonMountUnit)
                //.CopyFrom(oghorse)
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
                .AddClassSkill(StatType.SkillAthletics)
                .AddClassSkill(StatType.SkillPerception)
                .SetSpeed(new Feet(50))
                .AddClassLevels(characterClass: CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    levels: 0,
                    raceStat: StatType.Constitution,
                    levelsStat: StatType.Strength,
                    skills: [StatType.SkillPerception],
                    selections: [entry1, entry2, entry3])
                .AddAllowDyingCondition()
                .AddResurrectOnRest()
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.MainHand)
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.OffHand)
                .AddCMDBonusAgainstManeuvers(descriptor: Kingmaker.Enums.ModifierDescriptor.Racial,
                    value: cmdvalue,
                    maneuvers: [CombatManeuver.Trip, CombatManeuver.BullRush, CombatManeuver.Pull])
                .AddFacts(facts: [FeatureRefs.HeadLocatorFeature.Reference.Get()])
                .SetType(UnitTypeRefs.PlagueBeast_Horse.Reference.Get())
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
                .SetSkills(skills)
                .SetAddFacts([UnitFactRefs.NaturalArmor4.Reference.Get(),
                                UnitFactRefs.ReducedReach.Reference.Get(),
                                FeatureRefs.TripDefenseFourLegs.Reference.Get(),
                                FeatureRefs.AnimalCompanionSlotFeature.Reference.Get(),
                                FeatureRefs.AnimalType.Reference.Get(),
                                FeatureRefs.AnimalCompanionNotUpgradedHorse.Reference.Get(),
                                FeatureRefs.AnimalCompanionScent30.Reference.Get()])
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 1.0f))
                .SetVisual(oghorse.Visual)
                .SetBody(body)
                .SetFactionOverrides(new FactionOverrides())
                .SetAlternativeBrains()
                .SetAdditionalTemplates()
                .Configure();
        }
        public static void AddGriffonMountToSelections(BlueprintFeature mountfeature)
        {
            if (NewSettings.GetSetting<bool>(settingName))
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
