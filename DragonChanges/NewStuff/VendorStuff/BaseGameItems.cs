using BlueprintCore.Blueprints.References;
using DragonLibrary.Utils;

namespace DragonChanges.NewStuff.VendorStuff;

public class BaseGameItems
{
    const string itemsttingname = "addexistingitems";
    const string itemsettingdescription = "Adds various items from the base game/dlc that people have asked me about to Anevia vendor.";
    [DragonConfigure]
    [DragonSetting(SettingCategories.Various, itemsttingname, itemsettingdescription)]
    public static void ItemStuffs()
    {
        if (!SettingsAction.GetSetting<bool>(itemsttingname)) return;
        Main.log.Log("Adding Walls of Sanctuary to anevia vendor");
        AneviaVendor.AddItem(ItemShieldRefs.WallsOfTheSanctuaryShieldItem.Reference.Get(), AneviaVendor.ItemType.Armor);
        Main.log.Log("Adding Helmet of Guiding Light to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentHeadRefs.HelmetOfTheGuidingLight.Reference.Get(), AneviaVendor.ItemType.Helmet);
        Main.log.Log("Adding The Priceless Woe to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.DLC3_NahyndrianVorpalBladeWeaponItem.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add Butchers Cleaver (DLC3) to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.ButcherCleaver_Item.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add Martyrs Blade to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.MartyrsBladeItem.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add OpressorBastardSword to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.TheOpressorBastardSwordItem.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add JagannathKhanda to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.JagannathKhanda.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add GreaterMagicWeaponScroll to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentUsableRefs.ScrollOfMagicWeaponGreaterPrimary.Reference.Get(), AneviaVendor.ItemType.Scroll, 99);
        Main.log.Log("Add PawsOfTheBearGodItem to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentGlovesRefs.PawsOfTheBearGodItem.Reference.Get(), AneviaVendor.ItemType.Armor);
        Main.log.Log("Add RuinItem to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.RuinItem.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add SerpentPrinceFauchItem to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.SerpentPrinceFauchItem.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add LionsClaw to anevia vendor");
        AneviaVendor.AddItem(ItemWeaponRefs.LionsClaw.Reference.Get(), AneviaVendor.ItemType.Weapon);
        Main.log.Log("Add AcerbicRingItem to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentRingRefs.AcerbicRingItem.Reference.Get(), AneviaVendor.ItemType.Ring);
        Main.log.Log("Add RingOfBigBoom to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentRingRefs.RingOfBigBoom.Reference.Get(), AneviaVendor.ItemType.Ring);
        Main.log.Log("Add ColdIronArrowsQuiverItem to anevia vendor");
        AneviaVendor.AddItem(ItemEquipmentUsableRefs.ColdIronArrowsQuiverItem.Reference.Get(), AneviaVendor.ItemType.Arrows, 10);
    }
}