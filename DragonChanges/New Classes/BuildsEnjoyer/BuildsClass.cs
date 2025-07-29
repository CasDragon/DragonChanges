using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.New_Classes.Redditor;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;

namespace DragonChanges.New_Classes.BuildsEnjoyer
{
    internal class BuildsClass
    {
        // edit
        internal const string classprefix = "buildsenjoyer";
        internal const string classguid = Guids.BuildsEnjoyerClass;
        internal const string settingName = "buildsenjoyer";
        internal const string settingDescription = "Enable the Builds Enjoyer class (which is a meme class I made, don't take too seriously)";
        // don't edit
        [DragonLocalizedString(classname, "BuildsEnjoyer")]
        internal const string classname = $"{classprefix}.name";
        [DragonLocalizedString(classdescription, "Builds Enjoyer combines Inciter, Dronk, and Marine archetypes into one handy class!")]
        internal const string classdescription = $"{classprefix}.description";
        [DragonLocalizedString(classshortdescription, "Builds Enjoyer combines Inciter, Drunken Monk, and Sable Marine archetypes into one handy class! Also gives Magic Deceiver spellbook at KC's request!")]
        internal const string classshortdescription = $"{classprefix}.shortdescription";
        [DragonConfigure]
        [DragonSetting(settingCategories.NewClasses, settingName, settingDescription, false)]
        public static void Configure()
        {
            if (NewSettings.GetSetting<bool>(settingName))
            {
                Main.log.Log($"{classprefix} class enabled, configuring started");
                BlueprintProgression progression = BuildsEnjoyerProgression.ConfigureEnabled();
                ConfigureCharacterClass(progression, SpellbookRefs.MagicDeceiverSpellbook.Reference.Get());
                Main.log.Log($"{classprefix} configuration done!");
            }
            else
            {
                Main.log.Log($"{classprefix} class disabled, configuring dummies");
                BuildsEnjoyerProgression.ConfigureDummy();
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
                .AddDlcCondition(DlcRewardRefs.Dlc6Reward.Reference.Get())
                .SetIsMythic(false)
                .SetPrestigeClass(false)
                .SetHitDie(DiceType.D8)
                .SetHideIfRestricted(false)
                .SetIsDivineCaster(true)
                .SetIsArcaneCaster(false)
                .SetBaseAttackBonus(StatProgressionRefs.BABMedium.Reference.Get())
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
                .SetSkillPoints(3)
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
                .AddToRecommendedAttributes(StatType.Charisma, StatType.Strength)
                .SetProgression(progression);
            if (spellbook != null)
                x.SetSpellbook(spellbook);
            BlueprintCharacterClassReference cclass = x.Configure().ToReference<BlueprintCharacterClassReference>();
            BlueprintRoot root = BlueprintTool.Get<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
            root.Progression.m_CharacterClasses = CommonTool.Append(root.Progression.m_CharacterClasses, cclass);
        }
    }
}
