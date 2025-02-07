using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Assets;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Items;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Sound;
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
                        upgradeLevel: 4,
                        useContextValueLevel: false,
                        forceAutoLevelup: false,
                        destroyPetOnDeactivate: false)
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetDisplayName(UndeadFeatureName)
                .SetDescription(UndeadFeatureDescription)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .SetIcon("assets/icons/undeadhorse.png")
                .AddFeatureToPet(FeatureRefs.UndeadType.Reference.Get())
                .SetRanks(1)
                .Configure();
        }
        public static BlueprintUnit CreateUndeadMount()
        {
            Main.log.Log("Creating undead mount unit");
            BlueprintUnit oghorse = TTTHelpers.CreateCopy<BlueprintUnit>(UnitRefs.AnimalCompanionUnitHorse.Reference.Get());
            //BlueprintPortrait portrait = CreateUndeadMountPortrait();
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
            UnitVisualParams visual = new UnitVisualParams();
            visual.BloodType = Kingmaker.Visual.HitSystem.BloodType.Common;
            visual.FootprintType = Kingmaker.Enums.FootprintType.AnimalHoof;
            visual.FootprintScale = 0.7f;
            visual.IsNotUseDismember = false;
            visual.IsBlockChangeClothColor = false;
            visual.m_Barks = BlueprintTool.GetRef<BlueprintUnitAsksListReference>("2108e4bdab276c6428c0bf7ce98cfbf7");
            visual.ReachFXThresholdBonus = 0.0f;
            visual.DefaultArmorSoundType = ArmorSoundType.Flesh;
            visual.FootstepSoundSizeType = FootstepSoundSizeType.BootMedium;
            visual.FootSoundType = FootSoundType.HardPaw;
            visual.FootSoundSize = Kingmaker.Enums.Size.Medium;
            visual.BodySoundType = BodySoundType.Flesh;
            visual.BodySoundSize = Kingmaker.Enums.Size.Medium;
            visual.NoFinishingBlow = false;
            visual.ImportanceOverride = 0;
            visual.SilentCaster = true;
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
            return UnitConfigurator.New(UndeadUnit, Guids.UndeadMountUnit)
                //.CopyFrom(oghorse)
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
                .SetPrefab("9b94fcd181ed5304e867b02c4faca9b8")
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
                .SetSkills(skills)
                .SetAddFacts([UnitFactRefs.NaturalArmor4.Reference.Get(),
                                UnitFactRefs.ReducedReach.Reference.Get(),
                                FeatureRefs.TripDefenseFourLegs.Reference.Get(),
                                FeatureRefs.AnimalCompanionSlotFeature.Reference.Get(),
                                FeatureRefs.AnimalType.Reference.Get(),
                                FeatureRefs.AnimalCompanionNotUpgradedHorse.Reference.Get(),
                                FeatureRefs.AnimalCompanionScent30.Reference.Get()])
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 1.0f))
                .SetVisual(visual)
                .SetBody(body)
                .SetFactionOverrides(new FactionOverrides())
                .SetAlternativeBrains()
                .SetAdditionalTemplates()
                .SetPortrait(UnitRefs.ArmyPlaguedHorse.Reference.Get().m_Portrait)
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
