using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using DragonChanges.NewItems.StuffForItems;
using DragonChanges.NewStuff;
using DragonChanges.Utils;
using DragonLibrary.Utils;
using Kingmaker.Blueprints.Items.Equipment;

namespace DragonChanges.NewItems;

public static class LevelToABRing
{
    // edit
    internal const string item = "level2abring";
    internal const string itemguid = Guids.level2abring;
    internal const string itemsettingname = "memerings";
    // don't edit
    [DragonLocalizedString(itemname, "The RingTM")]
    internal const string itemname = $"{item}.name";
    [DragonLocalizedString(itemdescription, "Wearing this ring grants the user additional attack bonus based on their character level as a morale bonus.")]
    internal const string itemdescription = $"{item}.description";
    [DragonConfigure]
    public static void Configure()
    {
        if (SettingsAction.GetSetting<bool>(itemsettingname))
        {
            Main.log.Log($"{item} item enabled, configuring");
            ConfigureEnabled();
        }
        else
        {
            Main.log.Log($"{item} disabled, configuring dummy");
            ConfigureDummy();
        }
    }

    private static void ConfigureDummy()
    {
        ItemEquipmentRingConfigurator.New(item, itemguid).Configure();
        LevelToABRingFeature.ConfigureDummy();
    }

    private static void ConfigureEnabled()
    {
        BlueprintItemEquipmentRing ring = ItemEquipmentRingConfigurator.New(item, itemguid)
            .SetDisplayNameText(itemname)
            .SetDescriptionText(itemdescription)
            .SetIcon(ItemEquipmentRingRefs.CopperRing.Reference.Get().Icon)
            .SetCost(35000)
            .SetWeight(1)
            .SetDestructible(false)
            .SetInventoryEquipSound("ArmorPlateEquip")
            .SetInventoryPutSound("RingPut")
            .SetInventoryTakeSound("RingTake")
            .SetCR(10)
            .AddFactToEquipmentWielder(LevelToABRingFeature.ConfigureEnabled())
            .Configure();
        AneviaVendor.AddItem(ring, AneviaVendor.ItemType.Ring);
    }
}