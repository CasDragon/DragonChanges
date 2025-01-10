using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Classes.Redditor
{
    internal class RedditorClass
    {
        // edit
        internal static string classprefix = "redditor";
        internal static string classguid = Guids.redditorcharacterclass;
        // don't edit
        internal static string classname = $"{classprefix}.name";
        internal static string classdescription = $"{classprefix}.description";
        internal static string classshortdescription = $"{classprefix}.shortdescription";
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("redditor"))
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
