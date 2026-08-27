using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;

namespace DragonChanges.New_Classes.DispelMage;

public class DispelMageClass
{// edit
    internal const string classprefix = "archmage";
    internal const string classguid = Guids.ArchmageClass;
    internal const string settingName = "archmage";
    internal const string settingDescription = "Enable the Discordant Archmage prestige class, which is focused on dispelling";
    // don't edit
    [DragonLocalizedString(classname, "Discordant Archmage")]
    internal const string classname = $"{classprefix}.name";
    [DragonLocalizedString(classdescription, "A Discordant Archmage is a mage who is focused on disrupting their enemies magic.")]
    internal const string classdescription = $"{classprefix}.description";
    [DragonLocalizedString(classshortdescription, "A Discordant Archmage is a mage who is focused on disrupting their enemies magic.")]
    internal const string classshortdescription = $"{classprefix}.shortdescription";
    //[DragonConfigure]
    //[DragonSetting(SettingCategories.NewClasses, settingName, settingDescription)]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(settingName))
        {
            Main.log.Log($"{classprefix} class enabled, configuring started");
            var progression = DispelMageProgression.ConfigureEnabled();
            ConfigureCharacterClass(progression);
            Main.log.Log($"{classprefix} configuration done!");
        }
        else
        {
            Main.log.Log($"{classprefix} class disabled, configuring dummies");
            DispelMageProgression.ConfigureDummy();
            ConfigureDummy();
        }
    }
    public static void ConfigureDummy()
    {
        CharacterClassConfigurator.New(classprefix, classguid)
            .SetLocalizedName(classname)
            .SetLocalizedDescription(LocalizedStringHelper.disabledcontentstring)
            .SetLocalizedDescriptionShort(LocalizedStringHelper.disabledcontentstring)
            .Configure();
    }
    public static void ConfigureCharacterClass(BlueprintProgression progression, BlueprintSpellbook? spellbook = null)
    {
        var x = CharacterClassConfigurator.New(classprefix, classguid)
            .SetLocalizedName(classname)
            .SetLocalizedDescription(classdescription)
            .SetLocalizedDescriptionShort(classshortdescription)
            .SetIsMythic(false)
            .SetPrestigeClass(true)
            .SetHitDie(DiceType.D8)
            .SetHideIfRestricted(false)
            .SetIsDivineCaster(false)
            .SetIsArcaneCaster(true)
            .SetBaseAttackBonus(StatProgressionRefs.BABLow.ToString())
            .SetFortitudeSave(StatProgressionRefs.SavesPrestigeLow.ToString())
            .SetReflexSave(StatProgressionRefs.SavesPrestigeLow.ToString())
            .SetWillSave(StatProgressionRefs.SavesPrestigeHigh.ToString())
            .SetPrimaryColor(0)
            .SetSecondaryColor(0)
            .SetDifficulty(1)
            .AddPrerequisiteNoClassLevel(CharacterClassRefs.AnimalClass.ToString())
            .AddToMaleEquipmentEntities("65e7ae8b40be4d64ba07d50871719259", "04244d527b8a1f14db79374bc802aaaa")
            .AddToFemaleEquipmentEntities("11266d19b35cb714d96f4c9de08df48e", "64abd9c4d6565de419f394f71a2d496f")
            .AddPrerequisiteIsPet(hideInUI: true, not: true)
            .SetSkillPoints(3)
            .AddToClassSkills(
                StatType.SkillKnowledgeWorld,
                StatType.SkillKnowledgeArcana,
                StatType.SkillUseMagicDevice)
            .AddToRecommendedAttributes(StatType.Intelligence)
            .AddPrerequisiteCasterType(true)
            .AddPrerequisiteStatValue(StatType.SkillKnowledgeArcana, 5, group: Prerequisite.GroupType.All)
            .SetProgression(progression);
        if (spellbook != null)
            x.SetSpellbook(spellbook);
        BlueprintCharacterClassReference cclass = x.Configure().ToReference<BlueprintCharacterClassReference>();
        BlueprintRoot root = BlueprintTool.Get<BlueprintRoot>("2d77316c72b9ed44f888ceefc2a131f6");
        root.Progression.m_CharacterClasses = CommonTool.Append(root.Progression.m_CharacterClasses, cclass);
    }
}