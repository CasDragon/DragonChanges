using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;

namespace DragonChanges.Content;

public class DragonDisciple
{
    private const string settingName = "ddfullspellcasting";
    private const string settingDescription = "Buffs Dragon Disciple class to have full spellcasting";
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, settingName, settingDescription)]
    public static void FullSpellcasting()
    {
        if (!SettingsAction.GetSetting<bool>(settingName)) return;
        Main.log.Log("Allowing DragonDisciple class to have full spellcasting");
        var cclass = CharacterClassRefs.DragonDiscipleClass.Reference.Get();
        DragonHelpers.RemoveComponent<SkipLevelsForSpellProgression>(cclass);
        ProgressionConfigurator.For(ProgressionRefs.DragonDiscipleProgression)
            .AddToLevelEntries(1, FeatureSelectionRefs.DragonDiscipleSpellbookSelection.ToString())
            .RemoveFromLevelEntries(2, FeatureSelectionRefs.DragonDiscipleSpellbookSelection.ToString())
            .Configure();
    }
    private const string settingNameMSS = "ddmastershapeshifter";
    private const string settingDescriptionMSS = "Buffs Dragon Disciple class to allow MasterShapeshifter making Dragonform free";

    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, settingNameMSS, settingDescriptionMSS)]
    public static void FreeShapeshiting()
    {
        if (!SettingsAction.GetSetting<bool>(settingNameMSS)) return;
        Main.log.Log("Allowing DragonDisciple class to have free shapeshifting");
        var reference = FeatureRefs.MasterShapeshifter.Reference.Get().ToReference<BlueprintUnitFactReference>();
         Blueprint<BlueprintReference<BlueprintAbility>>[] spells = 
            [AbilityRefs.FormOfTheDragonIBlackDragonDisciple, AbilityRefs.FormOfTheDragonIBlueDragonDisciple,
            AbilityRefs.FormOfTheDragonIBrassDragonDisciple, AbilityRefs.FormOfTheDragonIBronzeDragonDisciple,
            AbilityRefs.FormOfTheDragonICopperDragonDisciple, AbilityRefs.FormOfTheDragonIGoldDragonDisciple,
            AbilityRefs.FormOfTheDragonIGreenDragonDisciple, AbilityRefs.FormOfTheDragonIIBlackDragonDisciple,
            AbilityRefs.FormOfTheDragonIIBlueDragonDisciple, AbilityRefs.FormOfTheDragonIIBrassDragonDisciple,
            AbilityRefs.FormOfTheDragonIIBronzeDragonDisciple, AbilityRefs.FormOfTheDragonIICopperDragonDisciple,
            AbilityRefs.FormOfTheDragonIIGoldDragonDisciple, AbilityRefs.FormOfTheDragonIIGreenDragonDisciple,
            AbilityRefs.FormOfTheDragonIIRedDragonDisciple, AbilityRefs.FormOfTheDragonIISilverDragonDisciple,
            AbilityRefs.FormOfTheDragonIIWhiteDragonDisciple, AbilityRefs.FormOfTheDragonIRedDragonDisciple,
            AbilityRefs.FormOfTheDragonISilverDragonDisciple, AbilityRefs.FormOfTheDragonIWhiteDragonDisciple];
        foreach (var spell in spells)
            AbilityConfigurator.For(spell)
                .EditComponent<AbilityResourceLogic>(c => DoThing(c, reference))
                .Configure();
    }

    private static void DoThing(AbilityResourceLogic comp, BlueprintUnitFactReference reference)
    {
        if (comp.ResourceCostDecreasingFacts.Contains(reference)) return;
        comp.ResourceCostDecreasingFacts = [.. comp.ResourceCostDecreasingFacts, reference];
    }
}