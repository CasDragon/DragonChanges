using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using UnityEngine;

namespace DragonChanges.NewStuff
{
    internal class UndeadMount
    {
        internal const string UndeadUnit = "undeadmountunit";
        internal const string UndeadFeature = "UndeadHorse-feature";
        internal const string UndeadMountPortrait = "undeadmountportrait";
        internal const string settingName = "undeadmount";
        internal const string settingDescription = "Adds a new undead mount, and then adds it to mount selections";
        internal const string featurename = "Animal Companion - Undead Horse";
        internal const string featuredescription = "{g|Encyclopedia:Size}Size{/g}: Large\n{g|Encyclopedia:Speed}Speed{/g}: 50 ft.\n{g|Encyclopedia:Armor_Class}AC{/g}: +4 natural armor\n{g|Encyclopedia:Attack}Attacks{/g}: bite ({g|Encyclopedia:Dice}1d4{/g}), 2 hooves (1d6)\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 16, {g|Encyclopedia:Dexterity}Dex{/g} 13, {g|Encyclopedia:Constitution}Con{/g} 15, {g|Encyclopedia:Intelligence}Int{/g} 2, {g|Encyclopedia:Wisdom}Wis{/g} 12, {g|Encyclopedia:Charisma}Cha{/g} 6\nSpecial qualities: {g|Encyclopedia:Scent}scent{/g}\nAt 4th level, a horse gains Str +2 and Con +2 and its hoof attacks become primary.\nWhen riding a horse, you gain a +1 {g|Encyclopedia:Bonus}bonus{/g} to AC and on attack rolls against enemies of Medium size or smaller.";
        // 
        [DragonLocalizedString(UndeadFeatureName, featurename)]
        internal const string UndeadFeatureName = "undeadmountfeature.name";
        [DragonLocalizedString(UndeadFeatureDescription, featuredescription, true)]
        internal const string UndeadFeatureDescription = "undeadmountfeature.description";

        [DragonConfigure]
        [DragonSetting(SettingCategories.NewFeatures, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
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
                .AddFeatureToPet(FeatureRefs.UndeadType.Reference.Get())
                .SetRanks(1)
                .Configure();
        }
        public static BlueprintUnit CreateUndeadMount()
        {
            Main.log.Log("Creating undead mount unit");
            BlueprintUnit oghorse = UnitRefs.AnimalCompanionUnitHorse.Reference.Get();
            BlueprintUnit phorse = UnitRefs.AnimalCompanionUnitHorseArmyGraveKnight_Large.Reference.Get();
            //BlueprintPortrait portrait = CreateUndeadMountPortrait();
            return UnitConfigurator.New(UndeadUnit, Guids.UndeadMountUnit)
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
            if (SettingsAction.GetSetting<bool>(settingName))
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
