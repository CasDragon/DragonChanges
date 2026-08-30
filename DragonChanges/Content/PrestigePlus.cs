using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using DragonChanges.Utils;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using TabletopTweaks.Core.UMMTools;

namespace DragonChanges.Content;

public class PrestigePlus
{
    const string DJSettingName = "pp-monk-styles";
    const string DJSettingDescription = "Remove Monk requirement from Prestige Plus combat styles";
    [DragonConfigure]
    [DragonSetting(SettingCategories.ModCompatability, DJSettingName, DJSettingDescription, false)]
    public static void RemoveMonkPrereqForStyles()
    {
        if (!ModCompat.pp) return;
        if (!SettingsAction.GetSetting<bool>(DJSettingName)) return;
        // Grabbing Style
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.GrabbingStyle));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.GrabbingDrag));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.GrabbingDrag));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.GrabbingMaster));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.GrabbingMaster));
        // Jabbing Style
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.JabbingStyle));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.JabbingDancer));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.JabbingMaster));
        // Kraken Style
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.KrakenStyle));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.KrakenWrack));
        // Snapping Turtle Style
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.SnappingTurtleStyle));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.SnappingTurtleClutch));
        DragonHelpers.RemoveComponent<PrerequisiteClassLevel>(BlueprintTool.Get<BlueprintFeature>(PrestigePlusRefs.SnappingTurtleShell));
    }
    
    [DragonConfigure]
    public static void BladeLash()
    {
        if (!ModCompat.pp) return;
        Main.log.Log("Adding Blade Lash to Warpriest/Inquist/MagicDeceiver spell list");
        BlueprintAbilityReference x = new() {deserializedGuid = BlueprintGuid.Parse(PrestigePlusRefs.NewSpellUseBladeLash)};
        SpellListConfigurator.For(SpellListRefs.WarpriestSpelllist)
            .AddToSpellsByLevel(new SpellLevelList(1) { m_Spells = [x] })
            .Configure();
        SpellListConfigurator.For(SpellListRefs.InquisitorSpellList)
            .AddToSpellsByLevel(new SpellLevelList(1) { m_Spells = [x] })
            .Configure();
        SpellListConfigurator.For(SpellListRefs.MagicDeceiverSpellList)
            .AddToSpellsByLevel(new SpellLevelList(1) { m_Spells = [x] })
            .Configure();
    }
}
