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
    const string itemsttingname = "addexistingitems";
    const string itemsettingdescription = "Adds various items from the base game/dlc that people have asked me about to Anevia vendor.";
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, itemsttingname, itemsettingdescription)]
    public static void ItemStuffs()
    {
        if (!SettingsAction.GetSetting<bool>(itemsttingname)) return;
        Main.log.Log("Adding Walls of Sanctuary to anevia vendor");
        AneviaVendor.AddItem(ItemShieldRefs.WallsOfTheSanctuaryShieldItem.Reference.Get());
        Main.log.Log("Adding Helmet of Guiding Light to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentHeadRefs.HelmetOfTheGuidingLight.Reference.Get());
        Main.log.Log("Adding The Priceless Woe to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.DLC3_NahyndrianVorpalBladeWeaponItem.Reference.Get());
        Main.log.Log("Add Butchers Cleaver (DLC3) to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.ButcherCleaver_Item.Reference.Get());
        Main.log.Log("Add Martyrs Blade to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.MartyrsBladeItem.Reference.Get());
        Main.log.Log("Add OpressorBastardSword to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.TheOpressorBastardSwordItem.Reference.Get());
        Main.log.Log("Add JagannathKhanda to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.JagannathKhanda.Reference.Get());
        Main.log.Log("Add GreaterMagicWeaponScroll to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentUsableRefs.ScrollOfMagicWeaponGreaterPrimary.Reference.Get(), 99);
        Main.log.Log("Add PawsOfTheBearGodItem to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentGlovesRefs.PawsOfTheBearGodItem.Reference.Get());
        Main.log.Log("Add RuinItem to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.RuinItem.Reference.Get());
        Main.log.Log("Add SerpentPrinceFauchItem to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.SerpentPrinceFauchItem.Reference.Get());
    }

    const string baphttingname = "baphitembuff";
    const string baphsettingdescription = "Buffs  Call of Fiery Things to work on SLAs.";
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, baphttingname, baphsettingdescription)]
    public static void BuffBaphometFireCloth_AnimalisticFireFeature()
    {
        if (!SettingsAction.GetSetting<bool>(itemsttingname)) return;
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
