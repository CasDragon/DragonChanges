using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.ModRefs;
using DragonLibrary.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Mechanics.Components;
namespace DragonChanges.Content;
internal class Various
{
    const string settingName = "hippogriff";
    const string settingDescription = "Adds the Hippogriff animal companion to other pet lists";
    [DragonConfigure]
    [DragonSetting(SettingCategories.ModCompatability, settingName, settingDescription)]
    public static void PatchHippogriff()
    {
        if (!SettingsAction.GetSetting<bool>(settingName)) return;
        Main.log.Log("Patching various animal selections to include Hippogriff");
        BlueprintFeature hippo = FeatureRefs.SableMarineHippogriffCompanionFeature.Reference.Get();
        PetUtils.AddPetToAll(hippo, [FeatureSelectionRefs.SableMarineHippogriffCompanionSelection.ToString()]);
    }
    const string codsettingname = "carrierofdisease";
    const string codsettingdescription = "Adds the Plague hex to Extra Hex and regular Hex selections for Shaman";
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, codsettingname, codsettingdescription)]
    public static void CarrierofDisease()
    {
        if (!SettingsAction.GetSetting<bool>(codsettingname)) return;
        Main.log.Log("Adding Carrier of Disease to Extra Hex");
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.ExtraShamanHexSelection)
            .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.ToString())
            .Configure();
        FeatureSelectionConfigurator.For(FeatureSelectionRefs.ShamanHexSelection)
            .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.ToString())
            .Configure();
        if (!ModCompat.tttbase) return;
        FeatureSelectionConfigurator.For(TTTBaseRefs.ExtraHexShaman)
            .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.ToString())
            .Configure();
        FeatureSelectionConfigurator.For(TTTBaseRefs.ExtraHexWitch)
            .AddToAllFeatures(FeatureSelectionRefs.PlagueHexSelection.ToString())
            .Configure();
    }

    const string baphttingname = "baphitembuff";
    const string baphsettingdescription = "Buffs  Call of Fiery Things to work on SLAs.";
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, baphttingname, baphsettingdescription)]
    public static void BuffBaphometFireCloth_AnimalisticFireFeature()
    {
        if (!SettingsAction.GetSetting<bool>(baphttingname)) return;
        Main.log.Log("Buffing Call of Fiery Things to work on SLAs.");
        BlueprintFeature x = FeatureRefs.BaphometFireCloth_AnimalisticFireFeature.Reference.Get();
        AddOutgoingDamageTrigger comp = x.GetComponent<AddOutgoingDamageTrigger>()!;
        AddOutgoingDamageTrigger y = (AddOutgoingDamageTrigger)TTTHelpers.ObjectDeepCopier.Clone(comp);
        y.m_AbilityType = Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.Supernatural;
        AddOutgoingDamageTrigger z = (AddOutgoingDamageTrigger)TTTHelpers.ObjectDeepCopier.Clone(comp);
        z.m_AbilityType = Kingmaker.UnitLogic.Abilities.Blueprints.AbilityType.SpellLike;
        FeatureConfigurator.For(x)
            .AddComponent(y)
            .AddComponent(z)
            .Configure();
    }
}
