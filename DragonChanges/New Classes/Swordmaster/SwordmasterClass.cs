using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;

namespace DragonChanges.New_Classes.Swordmaster
{
    internal class SwordmasterClass
    {
        // edit
        internal static string classprefix = "swordmaster";
        internal static string classguid = Guids.SwordMasterClass;
        // don't edit
        internal static string classname = $"{classprefix}.class.name";
        internal static string classdescription = $"{classprefix}.class.description";
        internal static string classshortdescription = $"{classprefix}.class.shortdescription";
        [DragonConfigure]
        public static void Configure()
        {
            if (Settings.GetSetting<bool>("swordmaster"))
            {
                Main.log.Log($"{classprefix} class enabled, configuring started");
                BlueprintProgression progression = SwordmasterProgression.Configure();
                ConfigureCharacterClass(progression);
                Main.log.Log($"{classprefix} configuration done!");
            }
            else
            {
                Main.log.Log($"{classprefix} class disabled, configuring dummies");
                SwordmasterProgression.ConfigureDummy();
                ConfigureDummy();
            }
        }
        public static void ConfigureDummy()
        {
            CharacterClassConfigurator.New(classprefix, classguid).Configure();
        }
        public static BlueprintCharacterClass ConfigureCharacterClass(BlueprintProgression progression)
        {
            BlueprintCharacterClass cclass = CharacterClassConfigurator.New(classprefix, classguid)
                .SetLocalizedName(classname)
                .SetLocalizedDescription(classdescription)
                .SetLocalizedDescriptionShort(classshortdescription)
                .SetIsMythic(false)
                .SetPrestigeClass(false)
                .SetHideIfRestricted(false)
                .SetIsDivineCaster(false)
                .SetIsArcaneCaster(false)
                .SetBaseAttackBonus(StatProgressionRefs.BABFull.Reference.Get())
                .SetFortitudeSave(StatProgressionRefs.SavesHigh.Reference.Get())
                .SetReflexSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetWillSave(StatProgressionRefs.SavesLow.Reference.Get())
                .SetStartingItems(
                    ItemWeaponRefs.ColdIronMasterworkHeavyCrossbow.Reference.Get(),
                    ItemArmorRefs.LeatherStandard.Reference.Get(),
                    ItemEquipmentUsableRefs.PotionOfCureLightWounds.Reference.Get(),
                    ItemEquipmentUsableRefs.ScrollOfMageArmor.Reference.Get(),
                    ItemEquipmentUsableRefs.ScrollOfMageShield.Reference.Get())
                .SetProgression(progression)
                .SetPrimaryColor(0)
                .SetSecondaryColor(0)
                .SetDifficulty(1)
                .AddPrerequisiteNoClassLevel(CharacterClassRefs.AnimalClass.Reference.Get())
                .AddPrerequisiteIsPet(hideInUI: true, not: true)
                .AddToFemaleEquipmentEntities("11266d19b35cb714d96f4c9de08df48e", "64abd9c4d6565de419f394f71a2d496f")
                .AddToMaleEquipmentEntities("65e7ae8b40be4d64ba07d50871719259", "04244d527b8a1f14db79374bc802aaaa")
                .SetHitDie(DiceType.D8)
                .SetStartingGold(70)
                .SetSkillPoints(2)
                .SetClassSkills(StatType.SkillAthletics,
                                StatType.SkillKnowledgeWorld,
                                StatType.SkillPerception,
                                StatType.SkillPersuasion)
                .SetRecommendedAttributes(StatType.Dexterity, StatType.Constitution)
                .Configure();
            BlueprintCharacterClassReference classref = cclass.ToReference<BlueprintCharacterClassReference>();
            BlueprintRoot root = BlueprintTool.Get<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
            root.Progression.m_CharacterClasses = CommonTool.Append(root.Progression.m_CharacterClasses, classref);
            return cclass;
        }
    }
}
