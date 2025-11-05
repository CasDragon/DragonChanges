using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
using Owlcat.Runtime.Core.Physics.PositionBasedDynamics.Bodies;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class SnakeLad
    {
        // edit
        internal const string feature = "snakeladfeature";
        internal const string featureguid = Guids.snakeladfeature;
        internal const string settingName = "snakelad";
        internal const string settingDescription = "Adds a new Viper pet";
        internal const string featurename = "Animal Companion - Viper";
        internal const string featuredescription = "Viper pet";
        // don't edit
        [DragonLocalizedString(featurenamekey, featurename)]
        internal const string featurenamekey = $"{feature}.name";
        [DragonLocalizedString(featuredescriptionkey, featuredescription)]
        internal const string featuredescriptionkey = $"{feature}.description";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewAbilities, settingName, settingDescription)]
        public static void Configure()
        {
            /*if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{feature} feature enabled, configuring");
                ConfigureEnabled();
            }
            else
            {
                Main.log.Log($"{feature} disabled, configuring dummy");
                ConfigureDummy();
            }*/
            ConfigureDummy();
        }
        public static void ConfigureDummy()
        {
            FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(LocalizedStringHelper.disabledcontentstring)
                .Configure();
            CreateUnit();
        }
        public static void ConfigureEnabled()
        {
            BlueprintUnit y = CreateUnit();
            BlueprintFeature x = CreateFeature(y);
            AddToSelections(x);
        }

        public static BlueprintFeature CreateFeature(BlueprintUnit unit)
        {
            return FeatureConfigurator.New(feature, featureguid)
                .SetDisplayName(featurenamekey)
                .SetDescription(featuredescriptionkey)
                .AddPet(pet: unit,
                    type: Kingmaker.Enums.PetType.AnimalCompanion,
                    progressionType: Kingmaker.Enums.PetProgressionType.AnimalCompanion,
                    levelRank: FeatureRefs.AnimalCompanionRank.Reference.Get(),
                    upgradeFeature: FeatureRefs.AnimalCompanionUpgradeHorse.Reference.Get(),
                    upgradeLevel: 4,
                    useContextValueLevel: false,
                    forceAutoLevelup: false,
                    destroyPetOnDeactivate: false,
                    levelContextValue: new Kingmaker.UnitLogic.Mechanics.ContextValue()
                    )
                .AddPrerequisitePet(noCompanion: true)
                .AddBuffExtraEffects(checkedBuff: BuffRefs.MountedBuff.Reference.Get(),
                        extraEffectBuff: BuffRefs.AnimalCompanionFeatureHorseBuff.Reference.Get(),
                        useBaffContext: false)
                .SetReapplyOnLevelUp(true)
                .SetIsClassFeature(true)
                .Configure();
        }

        internal const string unit = "snakeladunit";
        internal const string unitguid = Guids.snakeladunit;
        internal const string unitname = "Animal Companion - Viper";
        internal const string unitdescription = "Viper pet";
        // don't edit
        [DragonLocalizedString(unitnamekey, unitname)]
        internal const string unitnamekey = $"{unit}.name";
        [DragonLocalizedString(unitdescriptionkey, unitdescription)]
        internal const string unitdescriptionkey = $"{unit}.description";
        public static BlueprintUnit CreateUnit()
        {
            BlueprintUnit horse = UnitRefs.AnimalCompanionUnitHorse.Reference.Get();
            return UnitConfigurator.New(unit, unitguid)
                .SetDisplayName(unitnamekey)
                .SetDescription(unitdescriptionkey)
                .SetPrefab(new BlueprintCore.Utils.Assets.AssetLink<Kingmaker.ResourceLinks.UnitViewLink>("9ef4ce6c6b41004428d67a7a82df61e4"))
                .SetPortrait(UnitRefs.CR3_UnicornStandard.Reference.Get().m_Portrait)
                .AddAdditionalLimb(ItemWeaponRefs.Tail1d4.Reference.Get())
                .AddClassLevels(characterClass: CharacterClassRefs.AnimalCompanionClass.Reference.Get(),
                    levels: 0,
                    raceStat: StatType.Constitution,
                    levelsStat: StatType.Strength,
                    skills: [StatType.SkillPerception])
                .AddAllowDyingCondition()
                .AddResurrectOnRest()
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.MainHand)
                .AddLockEquipmentSlot(slotType: LockEquipmentSlot.SlotType.OffHand)
                .AddFacts(facts: [FeatureRefs.HeadLocatorFeature.Reference.Get()])
                .SetGender(Gender.Male)
                .SetSize(Kingmaker.Enums.Size.Small)
                .SetIsLeftHanded(false)
                .SetAlignment(Kingmaker.Enums.Alignment.TrueNeutral)
                .SetFaction(FactionRefs.Neutrals.Reference.Get())
                .SetBrain("cf986dd7ba9d4ec46ad8a3a0406d02ae")
                .SetStrength(16)
                .SetDexterity(13)
                .SetConstitution(16)
                .SetIntelligence(11)
                .SetWisdom(10)
                .SetCharisma(18)
                .SetSpeed(new Kingmaker.Utility.Feet(50))
                .SetBaseAttackBonus(0)
                .SetMaxHP(0)
                .SetIsCheater(false)
                .SetIsFake(false)
                .SetSkills(horse.Skills)
                .SetAddFacts([UnitFactRefs.NaturalArmor4.Reference.Get(),
                                UnitFactRefs.ReducedReach.Reference.Get(),
                                FeatureRefs.TripDefenseFourLegs.Reference.Get(),
                                FeatureRefs.AnimalCompanionSlotFeature.Reference.Get(),
                                FeatureRefs.AnimalType.Reference.Get(),
                                FeatureRefs.AnimalCompanionNotUpgradedHorse.Reference.Get(),
                                FeatureRefs.AnimalCompanionScent30.Reference.Get()])
                .SetColor(new Color(0.15f, 0.15f, 0.15f, 1.0f))
                .SetVisual(horse.Visual)
                .SetBody(horse.Body)
                .SetFactionOverrides(new FactionOverrides())
                .Configure();
        }

        public static void AddToSelections(BlueprintFeature feature)
        {

            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionBase)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDivineHound)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomain)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDomainSeparatist)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionDruid)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionHunter)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionMadDog)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionPrimalDruid)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionRanger)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSacredHuntsmaster)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionUrbanHunter)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.AnimalCompanionSelectionWildlandShaman)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.CavalierMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.BeastRiderMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.ArcaneRiderMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.BloodriderMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.GhostRiderGhostMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.NomadMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.OrderOfThePawMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.PaladinDivineMountSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.SoheiMonasticMountHorseSelection)
                .AddToAllFeatures(feature)
                .Configure();
            FeatureSelectionConfigurator.For(FeatureSelectionRefs.OracleRevelationBondedMount)
                .AddToAllFeatures(feature)
                .Configure();
        }
    }
}
