using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;

namespace DragonChanges.New_Classes.Redditor
{
    internal class RedditorClass
    {
        // edit
        internal const string classprefix = "redditor";
        internal const string classguid = Guids.redditorcharacterclass;
        internal const string settingName = "redditor";
        internal const string settingDescription = "Enable the Redditor class  (which is a meme class I made, don't take too seriously)";
        internal const string classname1 = "Redditor";
        internal const string classdescription1 = "Reddit's own character class. CHA TO EVERYTHING! \nBasically Fighter class with +CHA to all stats and CHA spellcasting. (This is a meme class I made, don't take it too seriously)";
        internal const string classshortdescription1 = "Reddit's own character class. CHA TO EVERYTHING!";
        // don't edit
        [DragonLocalizedString(classname, classname1)]
        internal const string classname = $"{classprefix}.name";
        [DragonLocalizedString(classdescription, classdescription1)]
        internal const string classdescription = $"{classprefix}.description";
        [DragonLocalizedString(classshortdescription, classshortdescription1)]
        internal const string classshortdescription = $"{classprefix}.shortdescription";
        [DragonConfigure]
        [DragonSetting(SettingCategories.NewClasses, settingName, settingDescription)]
        public static void Configure()
        {
            if (SettingsAction.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{classprefix} class enabled, configuring started");
                BlueprintProgression progression = RedditorProgression.Configure();
                BlueprintSpellbook spellbook = RedditorSpellbook.Configure();
                ConfigureCharacterClass(progression, spellbook);
                Main.log.Log($"{classprefix} configuration done!");
            }
            else
            {
                Main.log.Log($"{classprefix} class disabled, configuring dummies");
                RedditorSpellbook.ConfigureDummy();
                RedditorProgression.ConfigureDummy();
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            CharacterClassConfigurator.New(classprefix, classguid).Configure();
        }
        public static void ConfigureCharacterClass(BlueprintProgression progression, BlueprintSpellbook spellbook = null)
        {
            var x = CharacterClassConfigurator.New(classprefix, classguid)
                .SetLocalizedName(classname)
                .SetLocalizedDescription(classdescription)
                .SetLocalizedDescriptionShort(classshortdescription)
                .SetIsMythic(false)
                .SetPrestigeClass(false)
                .SetHitDie(DiceType.D8)
                .SetHideIfRestricted(false)
                .SetIsDivineCaster(true)
                .SetIsArcaneCaster(false)
                .SetBaseAttackBonus(StatProgressionRefs.BABFull.Reference.Get())
                .SetFortitudeSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetReflexSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetWillSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetStartingGold(500)
                .SetStartingItems(
                    ItemWeaponRefs.ColdIronMasterworkHeavyCrossbow.Reference.Get(),
                    ItemArmorRefs.LeatherStandard.Reference.Get(),
                    ItemEquipmentUsableRefs.PotionOfCureLightWounds.Reference.Get(),
                    ItemEquipmentUsableRefs.ScrollOfMageArmor.Reference.Get(),
                    ItemEquipmentUsableRefs.ScrollOfMageShield.Reference.Get())
                .SetPrimaryColor(0)
                .SetSecondaryColor(0)
                .SetDifficulty(1)
                .AddPrerequisiteNoClassLevel(CharacterClassRefs.AnimalClass.Reference.Get())
                .AddToMaleEquipmentEntities("65e7ae8b40be4d64ba07d50871719259", "04244d527b8a1f14db79374bc802aaaa")
                .AddToFemaleEquipmentEntities("11266d19b35cb714d96f4c9de08df48e", "64abd9c4d6565de419f394f71a2d496f")
                .AddPrerequisiteIsPet(hideInUI: true, not: true)
                .SetSkillPoints(2)
                .AddToClassSkills(
                    StatType.SkillMobility,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillUseMagicDevice,
                    StatType.SkillAthletics,
                    StatType.SkillLoreNature,
                    StatType.SkillLoreReligion,
                    StatType.SkillPerception,
                    StatType.SkillPersuasion,
                    StatType.SkillStealth,
                    StatType.SkillThievery)
                .AddToRecommendedAttributes(StatType.Charisma)
                .AddToSignatureAbilities(Guids.chatocha)
                .SetProgression(progression);
            if (spellbook != null)
                x.SetSpellbook(spellbook);
            BlueprintCharacterClass cclass = x.Configure();
            BlueprintCharacterClassReference classref = cclass.ToReference<BlueprintCharacterClassReference>();
            BlueprintRoot root = BlueprintTool.Get<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
            root.Progression.m_CharacterClasses = CommonTool.Append(root.Progression.m_CharacterClasses, classref);
        }
    }
}
